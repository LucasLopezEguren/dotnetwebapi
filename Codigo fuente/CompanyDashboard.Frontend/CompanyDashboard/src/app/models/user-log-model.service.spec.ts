import { TestBed } from '@angular/core/testing';

import { UserLogModelService } from './user-log-model.service';

describe('UserLogModelService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserLogModelService = TestBed.get(UserLogModelService);
    expect(service).toBeTruthy();
  });
});
