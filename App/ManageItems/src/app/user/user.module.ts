import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddComponent } from './add/add.component';
import { UpdateComponent } from './update/update.component';
import { DetailsComponent } from './details/details.component';



@NgModule({
  declarations: [
    AddComponent,
    UpdateComponent,
    DetailsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class UserModule { }
