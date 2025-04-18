import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllUrlsComponent } from './all-urls.component';

describe('AllUrlsComponent', () => {
  let component: AllUrlsComponent;
  let fixture: ComponentFixture<AllUrlsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllUrlsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AllUrlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
