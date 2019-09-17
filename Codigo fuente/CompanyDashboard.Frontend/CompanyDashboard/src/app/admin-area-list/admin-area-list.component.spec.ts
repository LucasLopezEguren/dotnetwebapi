import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAreaListComponent } from './admin-area-list.component';

describe('AdminAreaListComponent', () => {
  let component: AdminAreaListComponent;
  let fixture: ComponentFixture<AdminAreaListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminAreaListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminAreaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
