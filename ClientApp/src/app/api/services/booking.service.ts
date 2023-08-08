/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';

import { BookingRm } from '../models/booking-rm';

@Injectable({ providedIn: 'root' })
export class BookingService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `listBooking()` */
  static readonly ListBookingPath = '/Booking/{email}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `listBooking$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking$Plain$Response(
    params: {
      email: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<BookingRm>>> {
    const rb = new RequestBuilder(this.rootUrl, BookingService.ListBookingPath, 'get');
    if (params) {
      rb.path('email', params.email, {});
    }

    return this.http.request(
      rb.build({ responseType: 'text', accept: 'text/plain', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<BookingRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `listBooking$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking$Plain(
    params: {
      email: string;
    },
    context?: HttpContext
  ): Observable<Array<BookingRm>> {
    return this.listBooking$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<BookingRm>>): Array<BookingRm> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `listBooking()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking$Response(
    params: {
      email: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<BookingRm>>> {
    const rb = new RequestBuilder(this.rootUrl, BookingService.ListBookingPath, 'get');
    if (params) {
      rb.path('email', params.email, {});
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'text/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<BookingRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `listBooking$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  listBooking(
    params: {
      email: string;
    },
    context?: HttpContext
  ): Observable<Array<BookingRm>> {
    return this.listBooking$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<BookingRm>>): Array<BookingRm> => r.body)
    );
  }

}
