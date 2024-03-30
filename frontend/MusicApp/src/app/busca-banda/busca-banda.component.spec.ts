import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuscaBandaComponent } from './busca-banda.component';

describe('BuscaBandaComponent', () => {
  let component: BuscaBandaComponent;
  let fixture: ComponentFixture<BuscaBandaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BuscaBandaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BuscaBandaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
