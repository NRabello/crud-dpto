import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartamentoHomeComponent } from './departamento.home.component';

describe('DepartamentoComponent', () => {
  let component: DepartamentoHomeComponent;
  let fixture: ComponentFixture<DepartamentoHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartamentoHomeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartamentoHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
