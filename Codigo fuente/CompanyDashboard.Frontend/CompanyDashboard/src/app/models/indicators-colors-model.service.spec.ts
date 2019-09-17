import { TestBed } from '@angular/core/testing';

import { IndicatorsColorsModelService } from './indicators-colors-model.service';

describe('IndicatorsColorsModelService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IndicatorsColorsModelService = TestBed.get(IndicatorsColorsModelService);
    expect(service).toBeTruthy();
  });
});
