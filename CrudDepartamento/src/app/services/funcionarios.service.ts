import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Funcionario } from '../models/Funcionario';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class FuncionariosService {
  url = 'http://localhost:5278/funcionario'
  urlCreate = this.url + '/create'
  urlRead = this.url + '/read'
  urlUpdate = this.url + '/update'
  urlDelete = this.url + '/delete?id='
  urlFindById = this.url + '/findById?id='
  urlFindByDepartment = this.url + '/findByDepartment?id='
  constructor(private http: HttpClient) { }

  read(): Observable<Funcionario[]> {
    return this.http.get<Funcionario[]>(this.urlRead)
  }

  findById(id: number): Observable<Funcionario> {
    return this.http.get<Funcionario>(`${this.urlFindById}${id}`);
  }

  create(funcionario: Funcionario): Observable<any> {
    return this.http.post<Funcionario>(this.urlCreate, funcionario, httpOptions);
  }

  update(funcionario: Funcionario): Observable<any> {
    return this.http.put<Funcionario>(this.urlUpdate, funcionario, httpOptions);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<number>(`${this.urlDelete}${id}`, httpOptions);
  }

  findByDepartment(id: number): Observable<any>{
    return this.http.get<Funcionario>(`${this.urlFindByDepartment}${id}`);
  }
}
