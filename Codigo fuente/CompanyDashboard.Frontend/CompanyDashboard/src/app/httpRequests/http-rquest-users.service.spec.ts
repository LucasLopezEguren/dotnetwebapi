import { TestBed } from '@angular/core/testing';

import { HttpRquestUsersService } from './http-rquest-users.service';

describe('HttpRquestUsersService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HttpRquestUsersService = TestBed.get(HttpRquestUsersService);
    expect(service).toBeTruthy();
  });
});
