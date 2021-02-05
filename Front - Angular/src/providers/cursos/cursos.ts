import { ApiProvider } from '../api/api';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class CursoProvider {
  private token: string;
  constructor(public api: ApiProvider) {
      this.token = "";
  }

  getCursos(params?: any){

    return this.api.get('Curso', params, {});

  }
  getCursosPorAluno(params?: any){
    let session : any;
    session = localStorage.getItem('session');

    session = JSON.parse(session);

    let token = session["Token"];
    let Id_aluno = session["Id_User"];

    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);

    return this.api.get('Matriculas/Aluno/'+Id_aluno, params, {headers});

  }
  matricularCurso(params?: any){
    let session : any;
    session = localStorage.getItem('session');

    session = JSON.parse(session);

    let token = session["Token"];

    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);

    return this.api.post('Matriculas', params, {headers});

  }

}
