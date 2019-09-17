import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from "@angular/http";
import { Observable, throwError } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';
import { AreasService } from '../models/areas.service';

@Injectable({
  providedIn: 'root'
})
export class HttpRequestAreasService {
  private WEB_API_URL: string = 'https://localhost:5001/api/area';

  constructor(private _httpService: Http) { }

  getHttpAreas(): Observable<Array<AreasService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    console.log("WEB_API_URL", this.WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.get(this.WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<AreasService>>response.json()),
      );
  }

  putHttpAreas(area: AreasService): Observable<Array<AreasService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let PUT_WEB_API_URL = this.WEB_API_URL + "/" + area.id;
    let body = JSON.parse(JSON.stringify(area));
    console.log("PUT_WEB_API_URL", PUT_WEB_API_URL);
    console.log("request", requestOptions);
    console.log("area body: ", body);
    return this._httpService.put(PUT_WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <Array<AreasService>>response.json()),
      );
  }

  postHttpAreas(area: AreasService): Observable<Array<AreasService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    console.log("area", JSON.stringify(area));
    let body = JSON.parse(JSON.stringify(area));
    console.log("area body: " + body);
    console.log("this.WEB_API_URL", this.WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.post(this.WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <Array<AreasService>>response.json()),
      );
  }

  deleteHttpAreas(area: AreasService): Observable<AreasService> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let DELETE_WEB_API_URL = this.WEB_API_URL + "/" + area.id;
    console.log("DELETE_WEB_API_URL", DELETE_WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.put(DELETE_WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <AreasService>response.json()),
      );
  }

  private handleError(error: Response) {
    console.error(error);
    return throwError(error.json().error || 'Server error');
  }
}
