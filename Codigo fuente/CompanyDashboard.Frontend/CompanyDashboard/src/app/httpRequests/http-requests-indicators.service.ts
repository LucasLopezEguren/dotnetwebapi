import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from "@angular/http";
import { Observable, throwError } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';
import { IndicatorsColorsModelService } from '../models/indicators-colors-model.service';

@Injectable({
  providedIn: 'root'
})
export class HttpRequestsIndicatorsService {
  private WEB_API_URL: string = 'https://127.0.0.1:5001/api/indicator';

  constructor(private _httpService: Http) { }

  getHttpColors(id:number): Observable<Array<string>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let COLOR_WEB_API_URL = this.WEB_API_URL + "/" + id + "/evaluation"
    console.log("COLOR_WEB_API_URL", COLOR_WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.get(COLOR_WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<string>>response.json()),
      );
  }
}
