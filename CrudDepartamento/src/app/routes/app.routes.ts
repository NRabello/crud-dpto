import { Routes } from '@angular/router';
import { DepartamentoHomeComponent } from '../components/departamento/departamento.home/departamento.home.component';
import { FuncionarioHomeComponent } from '../components/funcionario/funcionario.home/funcionario.home.component' ;
import { DepartamentoSaveComponent } from '../components/departamento/departamento.save/departamento.save.component';
import { FuncionarioSaveComponent } from '../components/funcionario/funcionario.save/funcionario.save.component';

export const routes: Routes = [
    {path: 'departamentoHome', component: DepartamentoHomeComponent},
    {path: 'funcionarioHome', component: FuncionarioHomeComponent},
    {path: 'departamentoSave', component: DepartamentoSaveComponent},
    {path: 'funcionarioSave', component: FuncionarioSaveComponent},
];
