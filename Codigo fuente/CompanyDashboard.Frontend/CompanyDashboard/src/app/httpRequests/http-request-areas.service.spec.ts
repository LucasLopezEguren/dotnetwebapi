import { TestBed } from '@angular/core/testing';

import { HttpRequestAreasService } from './http-request-areas.service';

describe('HttpRequestAreasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HttpRequestAreasService = TestBed.get(HttpRequestAreasService);
    expect(service).toBeTruthy();
  });
});
