import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserLogModelService {
  usuario: string;
  ingresos: number;
  constructor(usu:string, ingresos:number) { 
    this.usuario = usu;
    this.ingresos = ingresos;
  }
}
