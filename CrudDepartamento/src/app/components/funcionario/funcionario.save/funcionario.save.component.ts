import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Funcionario } from '../../../models/Funcionario';
import { FuncionariosService } from '../../../services/funcionarios.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Departamento } from '../../../models/Departamento';
import { DepartamentosService } from '../../../services/departamentos.service';

@Component({
  selector: 'app-funcionario.save',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './funcionario.save.component.html',
  styleUrl: './funcionario.save.component.css',
  providers: [FuncionariosService]
})
export class FuncionarioSaveComponent implements OnInit {
  formulario: any;
  tituloFormulario: String;
  funcionario: Funcionario;
  departamentos: Departamento[];

  imagens = [
    { nome: 'iconHomem.png' },
    { nome: 'iconMulher.png' }
  ];

  constructor(private formBuilder: FormBuilder, private router: Router, private funcionarioService: FuncionariosService, private departamentoService: DepartamentosService, private Router: ActivatedRoute) { }

  ngOnInit(): void {
    this.Router.queryParams.subscribe(params => {
      if (params['id'] != null) {
        this.ExibirFormularioAtualizacao(params['id']);
      } else {
        this.ExibirFormularioCadastro();
      }
      this.departamentoService.read().subscribe(resultado => {
        this.departamentos = resultado;
      });
    });
  }


  ExibirFormularioCadastro() {
    this.tituloFormulario = "Novo Funcionário";

    this.formulario = this.formBuilder.group({
      nome: [null, Validators.required],
      foto: [null, Validators.required],
      rg: [null, Validators.required],
      departamento: [null, Validators.required]
    });
  }

  ExibirFormularioAtualizacao(id: number) {
    this.funcionarioService.findById(id).subscribe(resultado => {
      this.tituloFormulario = `Atualizar ${resultado.nome}`;

      this.formulario = this.formBuilder.group({
        Id: [resultado.id],
        nome: [resultado.nome, Validators.required],
        foto: [resultado.foto, Validators.required],
        rg: [resultado.rg, Validators.required],
        departamento: [resultado.departamento, Validators.required]
      });
    });
  }

  EnviarFormulario(): void {
    this.funcionario = this.formulario.value;

    if (this.formulario.valid) {
      if (this.formulario.get('Id')) {
        this.funcionario.id = this.formulario.get('Id').value;
        this.funcionarioService.update(this.funcionario).subscribe(resultado => {
          alert('Funcionario atualizado com sucesso');
          this.router.navigate(['funcionarioHome']);
        })
      } else {
        this.funcionarioService.create(this.funcionario).subscribe(resultado => {
          alert('Funcionario salvo com sucesso');
          this.router.navigate(['funcionarioHome']);
        });
      }
      this.formulario.reset();
    } else {
      alert("Formulário inválido. Por favor, preencha todos os campos corretamente.");
    }
  }

  Voltar(): void {
    this.router.navigate(['funcionarioHome']);
  }
  
  compareDepartamentos(departamento1: Departamento, departamento2: Departamento): boolean {
    return departamento1 && departamento2 ? departamento1.id === departamento2.id : departamento1 === departamento2;
  }
}
