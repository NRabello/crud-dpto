import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuncionarioSaveComponent } from './funcionario.save.component';

describe('FuncionarioSaveComponent', () => {
  let component: FuncionarioSaveComponent;
  let fixture: ComponentFixture<FuncionarioSaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FuncionarioSaveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FuncionarioSaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
