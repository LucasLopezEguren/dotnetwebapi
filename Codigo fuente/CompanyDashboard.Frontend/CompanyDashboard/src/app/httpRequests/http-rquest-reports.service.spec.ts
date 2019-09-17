import { TestBed } from '@angular/core/testing';

import { HttpRquestReportsService } from './http-rquest-reports.service';

describe('HttpRquestReportsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HttpRquestReportsService = TestBed.get(HttpRquestReportsService);
    expect(service).toBeTruthy();
  });
});
