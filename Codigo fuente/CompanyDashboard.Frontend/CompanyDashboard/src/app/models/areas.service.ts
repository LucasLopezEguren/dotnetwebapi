import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AreasService {
  id: number;
  name: string;
  dataSource: string;

  constructor() {
  }
}
