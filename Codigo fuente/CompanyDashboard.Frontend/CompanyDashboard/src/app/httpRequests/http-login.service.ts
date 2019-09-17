import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from "@angular/http";
import { Observable, throwError } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { SessionModelService } from '../models/session-model.service';

@Injectable({
  providedIn: 'root'
})

export class HttpLoginService {
  private WEB_API_URL: string = 'https://localhost:5001/api/session';
  corsHeaders: HttpHeaders;
  constructor(private _httpService: Http) {
    this.corsHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Allow-Origin': '/'
    });
   }

  httpLogin(username: string, password: string): Observable<string> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    const requestOptions = new RequestOptions({
      headers: myHeaders
    });
    let session = new SessionModelService(username,password);
    const body = JSON.parse(JSON.stringify(session));
    console.log("WEB_API_URL", this.WEB_API_URL);
    console.log("request", requestOptions);
    console.log("body", body);
    return this._httpService.post(this.WEB_API_URL, body , requestOptions)
      .pipe(
        map((response: Response) => <string>response.json()),
      );
  }

  private handleError(error: Response) {
    console.error(error);
    return throwError(error.json().error || 'Server error');
  }
}
