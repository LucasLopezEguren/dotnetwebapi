import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class IndicatorsLogModelService {
  indicador: string;
  escondido: number;
  constructor(indicador:string, escondido:number) { 
    this.indicador = indicador;
    this.escondido = escondido;
  }
}
