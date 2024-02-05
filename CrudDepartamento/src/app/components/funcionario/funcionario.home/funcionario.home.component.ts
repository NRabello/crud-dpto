import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { FuncionariosService } from '../../../services/funcionarios.service';
import { Funcionario } from '../../../models/Funcionario';
import { DepartamentosService } from '../../../services/departamentos.service';
import { Departamento } from '../../../models/Departamento';
import { Router } from '@angular/router';


@Component({
  selector: 'app-funcionario',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule],
  templateUrl: './funcionario.home.component.html',
  styleUrls: ['./funcionario.home.component.css'],
  providers: [FuncionariosService, BsModalService, DepartamentosService]
})
export class FuncionarioHomeComponent implements OnInit {
  formulario: any;
  tituloFormulario: String;
  funcionarios: Funcionario[];
  departamentos: Departamento[];
  funcionario: Funcionario;
  modalRef: BsModalRef;


  constructor(private formBuilder: FormBuilder, private funcionarioService: FuncionariosService, private departamentoService: DepartamentosService, private modalService: BsModalService,
    private router: Router) { }

  ngOnInit(): void {
    this.funcionarioService.read().subscribe(resultado => {
      this.funcionarios = resultado;
    });
    this.departamentoService.read().subscribe(resultado => {
      this.departamentos = resultado;
    });
  }

  ExibirFormularioCadastro(): void {
    this.router.navigate(['funcionarioSave']);
  }

  ExibirFormularioAtualizacao(funcionario: Funcionario): void {
    this.router.navigate(['funcionarioSave'], {
      queryParams: { id: funcionario.id }
    });
  }

  CarregarFuncionarios(): void {
    this.funcionarioService.read().subscribe(resultado => {
      this.funcionarios = resultado;
    })
  }

  ExibirConfirmacaoExclusao(funcionario: Funcionario, conteudoModal: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(conteudoModal);

    this.funcionario = {
      id: funcionario.id,
      nome: funcionario.nome,
      foto: funcionario.foto,
      rg: funcionario.rg,
      departamento: funcionario.departamento
    };
  }

  ExcluirFuncionario(funcionario: Funcionario): void {
    this.funcionarioService.delete(funcionario.id).subscribe(resultado => {
      this.modalRef.hide();
      alert("Funcionario excluido com sucesso")
      this.CarregarFuncionarios();
    });
  }

  NavegarParaDepartamentos(): void {
    this.router.navigate(["departamentoHome"]);
  }

  compareDepartamentos(departamento1: Departamento, departamento2: Departamento): boolean {
    return departamento1 && departamento2 ? departamento1.id === departamento2.id : departamento1 === departamento2;
  }
}
