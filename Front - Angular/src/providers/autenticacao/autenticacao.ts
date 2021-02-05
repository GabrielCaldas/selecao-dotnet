import { ApiProvider } from '../api/api';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class AutenticacaoProvider {
  private token: string;
  constructor(public api: ApiProvider) {
      this.token = "";
  }

  login(params?: any){
    let headers = new HttpHeaders().set('Access-Control-Allow-Origin', "*");
    return this.api.post('login', params,{ headers });
  }
  /*
  authSocial(params?: any) {
    return this.api.post('oauth/token', params);
  }*/

  /*cadastraFeirante(params?: any) {
    let headers = new HttpHeaders().set('Authorization', 'Bearer ' + this.token);
    let request = this.api.post('feirantes', params, { headers })
    return request;
  }*/
}
