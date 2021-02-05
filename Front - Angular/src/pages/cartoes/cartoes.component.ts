import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/components/alert-dialog/alert-dialog.component';

import { UserProvider } from '../../providers/user/user';
import { CartoesProvider } from '../../providers/cartoes/cartoes';

@Component({
  selector: 'app-home',
  templateUrl: './cartoes.component.html'
})
export class CartoesComponent implements OnInit {

  public loading = true;
  public user:any;
  public cartoes:any;

  public CVV = 0;
  public NUCARTAO = 0;

  constructor(
    public dialog: MatDialog,
    private router: Router,
    private cartoesProvider: CartoesProvider,
    private userProvider: UserProvider
    ) {

      this.dialog.open(AlertDialogComponent,{
        data: {
          tittle: 'Carregando conteúdo',
          msg:"Por favor, aguarde."
        },
        disableClose:true
      });

      this.getUserData();
   }

   onCardNuChangeText(text: any){
    this.NUCARTAO = +text.target.value;
  }
  onCardCVVChangeText(text: any){
    this.CVV = +text.target.value;
  }

  getUserData(){
    this.userProvider.getLoggedUser().subscribe(
      (data:any) => {

        this.user = data;

        this.getCartao();

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

  getCartao(){
    this.cartoesProvider.getCartaoPorAluno().subscribe(
      (data:any) => {

        this.cartoes = data;

        this.loading = false;
        this.dialog.closeAll();

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
      if(!this.CVV||!this.NUCARTAO||this.CVV<100||this.NUCARTAO<1000000000000000){
        this.dialog.open(AlertDialogComponent,{
          data: {
            tittle: 'Falha',
            msg:"Código de verificação e número do cartão são necessários.",
            showButton: true
          },
        });
        return;
      }
      this.loading = true;
      let headers = {
        "id_aluno": this.user["id"],
        "nuCartao": this.NUCARTAO,
        "cdverificacao": this.CVV
      }

      this.cartoesProvider.cadastrarCartao(headers).subscribe(
        () => {

          this.getCartao();

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

  onPagarMatriculaClick(cartao:any){
    console.log(cartao)
      let headers = {
        "dtPagamento": "05/02/2021",
        "vlPagamento": 100,
        "id_aluno": this.user["id"],
        "id_cartao": cartao["id"]
      }

      this.cartoesProvider.pagarMatricula(headers).subscribe(
        () => {

          this.dialog.open(AlertDialogComponent,{
            data: {
              tittle: 'Pronto!',
              msg:"Matrícula paga com sucesso.",
              showButton: true
            },
          });

        },
        error => {

          this.dialog.open(AlertDialogComponent,{
            data: {
              tittle: 'Falha',
              msg:error.error,
              showButton: true
            },
          });
        }
      );

  }

  ngOnInit(): void {
  }

}
