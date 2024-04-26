import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerificarcuentaComponent } from './verificarcuenta.component';

describe('VerificarcuentaComponent', () => {
  let component: VerificarcuentaComponent;
  let fixture: ComponentFixture<VerificarcuentaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VerificarcuentaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VerificarcuentaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
