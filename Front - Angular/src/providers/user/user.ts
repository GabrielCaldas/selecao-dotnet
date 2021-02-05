import { ApiProvider } from '../api/api';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class UserProvider {
  private token: string;
  constructor(public api: ApiProvider) {
      this.token = "";
  }

  register(params?: any){
    return this.api.post('Aluno', params, {});
  }

  getLoggedUser(params?: any){

    let session : any;
    session = localStorage.getItem('session');

    session = JSON.parse(session);

    let token = session["Token"];
    let Id_aluno = session["Id_User"];

    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + token);

    return this.api.get('Aluno/'+Id_aluno, params, {headers});
  }
}
