import { ApiProvider } from '../api/api';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class CartoesProvider {
  private token: string;
  constructor(public api: ApiProvider) {
      this.token = "";
  }
  getCartaoPorAluno(params?: any){
    let session : any;
    session = localStorage.getItem('session');

    session = JSON.parse(session);

    let token = session["Token"];
    let Id_aluno = session["Id_User"];

    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);

    return this.api.get('AlunoCartao/'+Id_aluno, params, {headers});

  }

  cadastrarCartao(params?: any){
    let session : any;
    session = localStorage.getItem('session');

    session = JSON.parse(session);

    let token = session["Token"];
    let Id_aluno = session["Id_User"];

    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);

    return this.api.post('AlunoCartao', params, {headers});

  }
  
  pagarMatricula(params?: any){

    return this.api.post('AlunoPGT', params, {});

  }
}
