import { TestBed } from '@angular/core/testing';

import { IndicatorsLogModelService } from './indicators-log-model.service';

describe('IndicatorsLogModelService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IndicatorsLogModelService = TestBed.get(IndicatorsLogModelService);
    expect(service).toBeTruthy();
  });
});
