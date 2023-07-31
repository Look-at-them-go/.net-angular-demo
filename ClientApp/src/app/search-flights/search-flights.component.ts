import { Time } from '@angular/common';
import { Component } from '@angular/core';
import { FlightService } from '../api/services';
import { TimePlaceRm } from '../api/models';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService){

  }

  search(){
    // this is async, subscribe listener waits for the result
    this.flightService.searchFlight({})
      .subscribe(response => this.searchResult = response,
      this.handleError);
  }

  private handleError(err: any){
    console.log("Response Error. Status: ", err.status);
    console.log("Status TextL", err.statusText)
    console.log(err);
  }

}
