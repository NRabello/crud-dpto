import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartamentoSaveComponent } from './departamento.save.component';

describe('DepartamentoCreateComponent', () => {
  let component: DepartamentoSaveComponent;
  let fixture: ComponentFixture<DepartamentoSaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartamentoSaveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartamentoSaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
