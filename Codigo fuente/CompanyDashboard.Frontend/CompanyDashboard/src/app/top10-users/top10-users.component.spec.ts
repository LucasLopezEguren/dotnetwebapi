import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Top10UsersComponent } from './top10-users.component';

describe('Top10UsersComponent', () => {
  let component: Top10UsersComponent;
  let fixture: ComponentFixture<Top10UsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Top10UsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Top10UsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
