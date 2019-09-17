import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IndicatorVisibilityComponent } from './indicator-visibility.component';

describe('IndicatorVisibilityComponent', () => {
  let component: IndicatorVisibilityComponent;
  let fixture: ComponentFixture<IndicatorVisibilityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IndicatorVisibilityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IndicatorVisibilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
