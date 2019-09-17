import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Top10hideIndicatorsComponent } from './top10hide-indicators.component';

describe('Top10hideIndicatorsComponent', () => {
  let component: Top10hideIndicatorsComponent;
  let fixture: ComponentFixture<Top10hideIndicatorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Top10hideIndicatorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Top10hideIndicatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
