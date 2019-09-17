import { TestBed } from '@angular/core/testing';

import { SessionModelService } from './session-model.service';

describe('SessionModelService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SessionModelService = TestBed.get(SessionModelService);
    expect(service).toBeTruthy();
  });
});
