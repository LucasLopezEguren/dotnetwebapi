import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAreaFormComponent } from './admin-area-form.component';

describe('AdminAreaFormComponent', () => {
  let component: AdminAreaFormComponent;
  let fixture: ComponentFixture<AdminAreaFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminAreaFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminAreaFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
