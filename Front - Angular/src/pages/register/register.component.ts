import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/components/alert-dialog/alert-dialog.component';


import { AutenticacaoProvider } from '../../providers/autenticacao/autenticacao';
import { UserProvider } from '../../providers/user/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class REGISTERComponent implements OnInit {

  public loading = false;
  private name = null ;
  private login = null ;
  private password = null;

  constructor(
      private router: Router,
      public dialog: MatDialog,
      private autenticacaoProvider: AutenticacaoProvider,
      private userProvider: UserProvider
    ) {

    }

  ngOnInit(): void {
  }

  onNameChangeText(text: any){
    this.name = text.target.value;
  }
  onLoginChangeText(text: any){
    this.login = text.target.value;
  }

  onPasswordChangeText(text: any){
    this.password = text.target.value;
  }

  doLogin(){

    let headers = {
      "email": this.login,
      "password": this.password
    }

    this.autenticacaoProvider.login(headers).subscribe(
      (data:any) => {

        this.loading = false;
        if (data['Token']) {
          this.router.navigate(['/home'],{});
        }
        else {
          console.log(data)
        }
      },
      error => {

        this.loading = false;
        console.log(error)
        this.dialog.open(AlertDialogComponent,{
          data: {
            tittle: 'Falha',
            msg:error.error,
            showButton: true
          },
        });
      }
    )

  }

  onRegisterClick(){

    if(!this.loading){
      if(!this.login||!this.password||!this.name){
        this.dialog.open(AlertDialogComponent,{
          data: {
            tittle: 'Falha',
            msg:"Nome, Login e senha são necessários.",
            showButton: true
          },
        });
        return;
      }
      this.loading = true;
      let headers = {
        "name": this.name,
        "email": this.login,
        "password": this.password
      }

      this.userProvider.register(headers).subscribe(
        () => {

          this.doLogin();

        },
        error => {

          this.loading = false;
          console.log(error)
          this.dialog.open(AlertDialogComponent,{
            data: {
              tittle: 'Falha',
              msg:error.error,
              showButton: true
            },
          });
        }
      )
    }
  }
}
