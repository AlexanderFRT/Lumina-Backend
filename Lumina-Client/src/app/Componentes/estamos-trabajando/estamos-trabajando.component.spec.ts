import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstamosTrabajandoComponent } from './estamos-trabajando.component';

describe('EstamosTrabajandoComponent', () => {
  let component: EstamosTrabajandoComponent;
  let fixture: ComponentFixture<EstamosTrabajandoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EstamosTrabajandoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EstamosTrabajandoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
