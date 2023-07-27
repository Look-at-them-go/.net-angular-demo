import { Time } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

  searchResult: FlightRm[] = [
    {
      airline: "American Airlines",
      remainingSeats: 500,
      departure: {time: Date.now().toString(), place: "Los Angeles"},
      arrival: {time: Date.now().toString(), place: "Istanbul"},
      price: "350",
    },
    {
      airline: "Deutsche BA",
      remainingSeats: 60,
      departure: {time: Date.now().toString(), place: "Munchen"},
      arrival: {time: Date.now().toString(), place: "Schiphol"},
      price: "600",
    },
    {
      airline: "British Airways",
      remainingSeats: 60,
      departure: {time: Date.now().toString(), place: "London"},
      arrival: {time: Date.now().toString(), place: "Rome"},
      price: "600",
    },
  ]

}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}