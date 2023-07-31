import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit{

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  constructor(private route: ActivatedRoute, private flightService: FlightService,
              private router: Router){

  }

  ngOnInit(): void {
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
    console.log("Response Error");
    console.log("Status: ", err.status);
    console.log("Status Text:", err.statusText);
    console.log(err);
  }

}
