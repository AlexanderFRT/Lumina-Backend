import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfRecoveryComponent } from './conf-recovery.component';

describe('ConfRecoveryComponent', () => {
  let component: ConfRecoveryComponent;
  let fixture: ComponentFixture<ConfRecoveryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfRecoveryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConfRecoveryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
