import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUsersFormComponent } from './admin-users-form.component';

describe('AdminUsersFormComponent', () => {
  let component: AdminUsersFormComponent;
  let fixture: ComponentFixture<AdminUsersFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminUsersFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminUsersFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
