import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Departamento } from '../models/Departamento'; 


const httpOptions = {
  headers : new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class DepartamentosService {
  url = 'http://localhost:5278/departamento'
  urlCreate = this.url + '/create'
  urlRead = this.url + '/read'
  urlUpdate = this.url + '/update'
  urlDelete = this.url + '/delete?id='
  urlFindById = this.url + '/findById?id='
  constructor(private http: HttpClient) { }

  read(): Observable<Departamento[]>{
    return this.http.get<Departamento[]>(this.urlRead)
  }

  findById(idDepartamento: number): Observable<Departamento>{
    return this.http.get<Departamento>(`${this.urlFindById}${idDepartamento}`);
  }

  create(departamento: Departamento) : Observable<any>{
    return this.http.post<Departamento>(this.urlCreate, departamento, httpOptions);
  }

  update(departamento: Departamento) : Observable<any>{
    return this.http.put<Departamento>(this.urlUpdate, departamento, httpOptions);
  }

  delete (idDepartamento: number) : Observable<any>{
    return this.http.delete<number>(`${this.urlDelete}${idDepartamento}`, httpOptions);
  }
}
