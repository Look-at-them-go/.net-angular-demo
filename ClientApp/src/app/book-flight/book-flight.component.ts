import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit{

  flightId: string = 'not loaded'

  constructor(private route: ActivatedRoute){

  }

  ngOnInit(): void {
    this.route.paramMap
      .subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight = (flightId: string | null) => {this.flightId = flightId ?? 'not passed';}

}
