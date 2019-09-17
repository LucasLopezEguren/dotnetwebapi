import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class IndicatorsColorsModelService {
  greenText: string;
  yellowText: string;
  redText: string;
  green: boolean;
  yellow: boolean;
  red: boolean;
  constructor() { }
}
