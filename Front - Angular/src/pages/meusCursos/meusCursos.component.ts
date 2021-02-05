import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { AlertDialogComponent } from 'src/components/alert-dialog/alert-dialog.component';

import { UserProvider } from '../../providers/user/user';
import { CursoProvider } from '../../providers/cursos/cursos';

@Component({
  selector: 'app-home',
  templateUrl: './meusCursos.component.html'
})
export class MeusCursosComponent implements OnInit {

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

      this.cursos = [];

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
    this.cursosProvider.getCursosPorAluno().subscribe(
      (data:any) => {

        this.cursosProvider.getCursos().subscribe(
          (result:any) => {


            result = Array.of(result)[0];
            console.log(result)
            for (let i = 0; i < result.length; i++) {
              for (let j = 0; j < data.length; j++) {
                if(result[i]["id"]==data[j]["id_curso"]){
                  this.cursos.push(result[i]);

                }

              }
            }


            this.loading = false;
            this.dialog.closeAll();
          }
        )

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

  ngOnInit(): void {
  }

}
