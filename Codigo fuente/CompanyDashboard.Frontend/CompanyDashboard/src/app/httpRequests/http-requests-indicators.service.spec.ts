import { TestBed } from '@angular/core/testing';

import { HttpRequestsIndicatorsService } from './http-requests-indicators.service';

describe('HttpRequestsIndicatorsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HttpRequestsIndicatorsService = TestBed.get(HttpRequestsIndicatorsService);
    expect(service).toBeTruthy();
  });
});
