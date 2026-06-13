import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIntervention } from './create-intervention';

describe('CreateIntervention', () => {
  let component: CreateIntervention;
  let fixture: ComponentFixture<CreateIntervention>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateIntervention]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateIntervention);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
