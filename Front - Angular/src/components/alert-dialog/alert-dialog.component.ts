import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';


@Component({
  selector: 'app-alert-dialog',
  templateUrl: './alert-dialog.component.html',
  template: 'passed in {{ data.tittle , data.msg , data.showButton}}',
})
export class AlertDialogComponent {
  public showButtom = true;
  constructor(@Inject(MAT_DIALOG_DATA) public data: {tittle: string, msg: string, showButton: boolean}) { }
}