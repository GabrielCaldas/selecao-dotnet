import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/components/alert-dialog/alert-dialog.component';

import { UserProvider } from '../../providers/user/user';
import { CursoProvider } from '../../providers/cursos/cursos';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HOMEComponent implements OnInit {

  public loading = true;
  public user:any;
  public cursos:any;

  constructor(
    public dialog: MatDialog,
    private router: Router,
    private cursosProvider: CursoProvider,
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

  getUserData(){
    this.userProvider.getLoggedUser().subscribe(
      (data:any) => {

        this.user = data;

        this.getCursos();

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

  getCursos(){
    this.cursosProvider.getCursos().subscribe(
      (data:any) => {

        this.cursos = data;

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


  onMatricularClick(curso:any){

    
      let headers = {
        "id_curso": curso["id"],
        "id_aluno": this.user["id"]
      }

      this.cursosProvider.matricularCurso(headers).subscribe(
        () => {

          this.dialog.open(AlertDialogComponent,{
            data: {
              tittle: 'Pronto!',
              msg:"Matrícula realizada com sucesso!",
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
