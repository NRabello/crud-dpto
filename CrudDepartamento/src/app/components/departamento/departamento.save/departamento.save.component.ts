import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Departamento } from '../../../models/Departamento';
import { DepartamentosService } from '../../../services/departamentos.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-departamento.save',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './departamento.save.component.html',
  styleUrl: './departamento.save.component.css',
  providers: [DepartamentosService]
})
export class DepartamentoSaveComponent implements OnInit{
  formulario: any;
  tituloFormulario: String;
  departamento: Departamento;

  constructor(private formBuilder: FormBuilder, private departamentoService: DepartamentosService, private router: Router, private Router: ActivatedRoute){}

  ngOnInit(){
    this.Router.queryParams.subscribe(params => {
      if (params['id'] != null) {
        this.ExibirFormularioAtualizacao(params['id']);
      } else {
        this.ExibirFormularioCadastro();
      }
    });
  }

  ExibirFormularioCadastro(){
    this.tituloFormulario = "Novo Departamento";

    this.formulario = this.formBuilder.group({
      nome: [null, Validators.required],
      sigla: [null, Validators.required]
    });
  }

  ExibirFormularioAtualizacao(id: number): void {
    this.departamentoService.findById(id).subscribe(resultado => {
      this.tituloFormulario = `Atualizar ${resultado.nome}`;

      this.formulario = this.formBuilder.group({
        Id: [resultado.id],
        nome: [resultado.nome, Validators.required],
        sigla: [resultado.sigla, Validators.required]
      });
    });
  }

  EnviarFormulario(): void {
    this.departamento = this.formulario.value;

    if (this.departamento.sigla.length > 3) {
      alert("A sigla deve conter 3 ou menos caracteres");
    } else {
      if (this.formulario.valid) {
        if (this.formulario.get('Id')) {
          this.departamento.id = this.formulario.get('Id').value;
          this.departamentoService.update(this.departamento).subscribe(resultado => {
            alert('Departamento atualizado com sucesso');
            this.router.navigate(['departamentoHome']);
          })
        } else {
          this.departamentoService.create(this.departamento).subscribe(resultado => {
            alert('Departamento salvo com sucesso');
            this.router.navigate(['departamentoHome']);
          });
        }
        this.formulario.reset();
      } else {
        alert("Formulário inválido. Por favor, preencha todos os campos corretamente.");
      }
    }
  }

  Voltar(): void {
    this.router.navigate(['departamentoHome']);
  }
}
