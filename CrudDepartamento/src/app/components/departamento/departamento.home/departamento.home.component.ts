import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Departamento } from '../../../models/Departamento';
import { DepartamentosService } from '../../../services/departamentos.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { Funcionario } from '../../../models/Funcionario';
import { FuncionariosService } from '../../../services/funcionarios.service';

@Component({
  selector: 'app-departamento',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule, ModalModule],
  templateUrl: './departamento.home.component.html',
  styleUrls: ['./departamento.home.component.css'],
  providers: [DepartamentosService, HttpClient, BsModalService, FuncionariosService]
})
export class DepartamentoHomeComponent implements OnInit {
  formulario: any;
  tituloFormulario: String;
  departamentos: Departamento[];
  departamento: Departamento;
  funcionarios: Funcionario[];

  visibilidadeTabela: boolean = true;
  visibilidadeFormulario: boolean = false;

  modalRef: BsModalRef;

  constructor(private formBuilder: FormBuilder, private departamentoService: DepartamentosService, private modalService: BsModalService,
    private funcionariosService: FuncionariosService, private router: Router) { }

  ngOnInit(): void {
    this.departamentoService.read().subscribe(resultado => {
      this.departamentos = resultado;
    });
  }

  ExibirFormularioCadastro(): void {
    this.router.navigate(['departamentoSave']);
  }

  ExibirFormularioAtualizacao(departamento: Departamento): void {
    this.router.navigate(['departamentoSave'], {
      queryParams: { id: departamento.id }
    });
  }

  CarregarDepartamentos(): void {
    this.departamentoService.read().subscribe(resultado => {
      this.departamentos = resultado;
    })
  }

  Voltar(): void {
    this.visibilidadeTabela = true;
    this.visibilidadeFormulario = false;
    this.CarregarDepartamentos();
  }

  ExibirConfirmacaoExclusao(departamento: Departamento, conteudoModal: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(conteudoModal);
    this.departamento = departamento;
  }

  ExcluirDepartamento(departamento: Departamento): void {
    this.funcionariosService.findByDepartment(departamento.id).subscribe(resultado => {
      if (resultado == null) {
        this.departamentoService.delete(departamento.id).subscribe(resultado => {
          this.modalRef.hide();
          alert("Departamento excluído com sucesso!")
          this.CarregarDepartamentos();
        });
      } else {
        this.modalRef.hide();
        alert(`Não foi possível excluir o ${departamento.nome} pois contém funcionários!`)
        this.CarregarDepartamentos();
      }
    })
  }

  NavegarParaFuncionarios(): void {
    this.router.navigate(['funcionarioHome'])
  }

  ExibirFuncionariosDepartamento(departamento: Departamento, funcionariosDepartamento: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(funcionariosDepartamento);
    this.departamento = departamento;
    this.funcionariosService.findByDepartment(departamento.id).subscribe(resultado => {
      this.funcionarios = resultado;
    })
  }
}
