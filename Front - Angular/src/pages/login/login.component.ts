import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AutenticacaoProvider } from '../../providers/autenticacao/autenticacao';
import {MatDialog} from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/components/alert-dialog/alert-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LOGINComponent implements OnInit {

  public loading = false;
  private login = null ;
  private password = null;

  constructor(
    private router: Router,
    public dialog: MatDialog,
    private autenticacaoProvider: AutenticacaoProvider
    ) {


  }

  ngOnInit(): void {
  }

  onLoginChangeText(text: any){
    this.login = text.target.value;
  }

  onPasswordChangeText(text: any){
    this.password = text.target.value;
  }

  onLoginClick(){

    if(!this.loading){
      if(!this.login||!this.password){
        this.dialog.open(AlertDialogComponent,{
          data: {
            tittle: 'Falha',
            msg:"Login e senha são necessários.",
            showButton: true
          },
        });
        return;
      }
      this.loading = true;
      let headers = {
        "email": this.login,
        "password": this.password
      }

      this.autenticacaoProvider.login(headers).subscribe(
        (data:any) => {

          this.loading = false;
          if (data['Token']) {
            localStorage.setItem('session', JSON.stringify(data));
            this.router.navigate(['/home'],{});
          }
          else {
            console.log(data)
          }
        },
        error => {

          this.loading = false;
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

  onRegisterClick(){
    this.router.navigate(['/register'],{});
  }

}
