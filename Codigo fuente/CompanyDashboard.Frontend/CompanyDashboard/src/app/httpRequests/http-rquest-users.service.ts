import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from "@angular/http";
import { Observable, throwError } from "rxjs";
import { map, tap, catchError } from 'rxjs/operators';
import { UsersService } from '../models/users.service';
import { UserModelService } from '../models/user-model.service';
import { IndicatorsService } from '../models/indicators.service';

@Injectable({
  providedIn: 'root'
})
export class HttpRquestUsersService {
  private WEB_API_URL: string = 'https://127.0.0.1:5001/api/user';

  constructor(private _httpService: Http) { }

  getHttpUsers(): Observable<Array<UsersService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    const requestOptions = new RequestOptions({ headers: myHeaders });
    console.log("WEB_API_URL", this.WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.get(this.WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<UsersService>>response.json()),
      );
  }

  putHttpUsers(user: UsersService): Observable<Array<UsersService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let PUT_WEB_API_URL = this.WEB_API_URL + "/" + user.id;
    let body = JSON.parse(JSON.stringify(user));
    console.log("PUT_WEB_API_URL", PUT_WEB_API_URL);
    console.log("request", requestOptions);
    console.log("user body: ", body);
    return this._httpService.put(PUT_WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <Array<UsersService>>response.json()),
      );
  }

  putHttpShowHideIndicator(user:number,indicator:number,hide:boolean): Observable<string> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    var ruta = "";
    if (hide){
      ruta = "hideIndicator";
    } else {
      ruta = "showIndicator";
    }
    let PUT_WEB_API_URL = this.WEB_API_URL + "/" + user + "/" + ruta + "/" + indicator;
    let body = JSON.parse(JSON.stringify(user));
    console.log("PUT_WEB_API_URL", PUT_WEB_API_URL);
    console.log("request", requestOptions);
    console.log("user body: ", body);
    return this._httpService.put(PUT_WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <string>response.json()),
      );
  }

  postHttpUsers(user: UserModelService): Observable<Array<UsersService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    console.log("user", JSON.stringify(user));
    let body = JSON.parse(JSON.stringify(user));
    console.log("user body: " + body);
    console.log("this.WEB_API_URL", this.WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.post(this.WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <Array<UsersService>>response.json()),
      );
  }

  deleteHttpUsers(user: UsersService): Observable<Array<UsersService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let DELETE_WEB_API_URL = this.WEB_API_URL + "/" + user.id;
    console.log("DELETE_WEB_API_URL", DELETE_WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.put(DELETE_WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<UsersService>>response.json()),
      );
  }

  getHttpUserByName(username:string): Observable<UsersService> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let GET_WEB_API_URL = this.WEB_API_URL + "/" + username;
    console.log("WEB_API_URL", GET_WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.get(GET_WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <UsersService>response.json()),
      );
  }

  getHttpUserIndicators(user:UsersService): Observable<Array<IndicatorsService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let GET_WEB_API_URL = this.WEB_API_URL + "/" + user.id + "/indicators";
    console.log("WEB_API_URL", GET_WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.get(GET_WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<IndicatorsService>>response.json()),
      );
  }

  getHttpUserVisibleIndicators(user:UsersService): Observable<Array<IndicatorsService>> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let GET_WEB_API_URL = this.WEB_API_URL + "/" + user.id + "/visibleIndicators";
    console.log("WEB_API_URL", GET_WEB_API_URL);
    console.log("request", requestOptions);
    return this._httpService.get(GET_WEB_API_URL, requestOptions)
      .pipe(
        map((response: Response) => <Array<IndicatorsService>>response.json()),
      );
  }

  putHttpReorder(user: UsersService, order: number[]): Observable<string> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'application/json');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let PUT_WEB_API_URL = this.WEB_API_URL + "/" + user.id + "/indicator";
    let body = JSON.parse(JSON.stringify(order));
    console.log("PUT_WEB_API_URL", PUT_WEB_API_URL);
    console.log("request", requestOptions);
    console.log(" body: ", body);
    return this._httpService.put(PUT_WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <string>response.json()),
      );
  }

  putHttpRename(user: number, indicator: number, nombre: string): Observable<string> {
    const myHeaders = new Headers();
    myHeaders.append('Accept', 'Content-type');
    myHeaders.append('Authorization', JSON.parse(localStorage.getItem('token')));
    const requestOptions = new RequestOptions({ headers: myHeaders });
    let PUT_WEB_API_URL = this.WEB_API_URL + "/" + user + "/indicator/" + indicator;
    let body = JSON.parse(JSON.stringify(nombre));
    console.log("PUT_WEB_API_URL", PUT_WEB_API_URL);
    console.log("request", requestOptions);
    console.log(" body: ", body);
    return this._httpService.put(PUT_WEB_API_URL, body, requestOptions)
      .pipe(
        map((response: Response) => <string>response.json()),
      );
  }

  private handleError(error: Response) {
    console.error(error);
    return throwError(error.json().error || 'Server error');
  }
}
