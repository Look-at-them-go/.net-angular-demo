import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FlightService } from '../api/services';
import { BookDto, FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit{

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  form = this.fb.group({
    number: [1, Validators.compose([Validators.required, Validators.min(1), Validators.max(254)])]
  })

  constructor(private route: ActivatedRoute, private flightService: FlightService,
              private router: Router, private authService: AuthService,
              private fb: FormBuilder){

  }

  ngOnInit(): void {
    if (!this.authService.currentUser){
      this.router.navigate(['/register-passenger']);
    }
    this.route.paramMap
      .subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight = (flightId: string | null) => {this.flightId = flightId ?? 'not passed';
    this.flightService.findFlight({id: this.flightId}).subscribe(flight => this.flight = flight, this.handleError);
  }


  private handleError = (err: any) => {
    if(err.status == 404){
      this.router.navigate(['/search-flights']);
      alert("Flight not found");
    }

    if(err.status == 409){
      console.log("err: " + err + " message: " +  err.message);
      alert(JSON.parse(err.error).message);
    }

    console.log("Response Error");
    console.log("Status: ", err.status);
    console.log("Status Text:", err.statusText);
    console.log(err);
  }

  book(){

    if(this.form.invalid){
      return;
    }

    console.log(`booking ${this.form.get('number')?.value} passengers for the flight: ${this.flight.id}`)
    
    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value ?? 1
    };

    this.flightService.bookFlight({body: booking})
      .subscribe( x => this.router.navigate(['/my-booking']), this.handleError);

  }

  get number(){
    return this.form.controls.number;
  }

}
