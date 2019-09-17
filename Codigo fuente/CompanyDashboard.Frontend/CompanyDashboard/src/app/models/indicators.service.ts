import { Injectable } from '@angular/core';
import { AreasService } from './areas.service';

@Injectable({
  providedIn: 'root'
})
export class IndicatorsService {
  id: number;
  area: AreasService;
  order: number;
  name: string;
  greenText: string;
  yellowText: string;
  redText: string;
  green: boolean;
  yellow: boolean;
  red: boolean;
  visible: boolean;
  

  constructor() {
  }
}
