import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerBodyComponent } from './manager-body.component';

describe('ManagerBodyComponent', () => {
  let component: ManagerBodyComponent;
  let fixture: ComponentFixture<ManagerBodyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagerBodyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
