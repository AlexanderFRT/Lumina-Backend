import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Modelo2Component } from './modelo-2.component';

describe('Modelo2Component', () => {
  let component: Modelo2Component;
  let fixture: ComponentFixture<Modelo2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Modelo2Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(Modelo2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
