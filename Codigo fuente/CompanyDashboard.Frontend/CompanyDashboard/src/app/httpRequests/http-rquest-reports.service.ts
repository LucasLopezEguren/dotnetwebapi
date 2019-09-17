
import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from "@angular/http";
import { Observable, throwError } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';
import { UsersService } from '../models/users.service';
import { UserModelService } from '../models/user-model.service';
import { IndicatorsService } from '../models/indicators.service';
import { UserLogModelService } from '../models/user-log-model.service';
import { IndicatorsLogModelService } from '../models/indicators-log-model.service';

@Injectable({
  providedIn: 'root'
})
export class HttpRquestReportsService {

  private WEB_API_URL: string = 'https://127.0.0.1:5001/api/user';

  constructor(private _httpService: Http) { }

  getHttpReportUsuarios(): Observable<Array<UserLogModelService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let URL = this.WEB_API_URL+"/reporteUsuarios"
    console.log("URL", URL);
    console.log("request", requestOptions);
    return this._httpService.get(URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<UserLogModelService>>response.json()),
      );
  }

  getHttpReportIndicadores(): Observable<Array<IndicatorsLogModelService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let URL = this.WEB_API_URL+"/reporteIndicadores"
    console.log("URL", URL);
    console.log("request", requestOptions);
    return this._httpService.get(URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<IndicatorsLogModelService>>response.json()),
      );
  }

}