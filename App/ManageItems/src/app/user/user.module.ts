import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddComponent } from './add/add.component';
import { UpdateComponent } from './update/update.component';
import { DetailsComponent } from './details/details.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', redirectTo: 'manage' },
      { path: 'manage', component: DetailsComponent },
      { path: 'add', component: AddComponent },
      { path: 'update/:id', component: UpdateComponent },
    ],    
  },
];

@NgModule({
  declarations: [AddComponent, UpdateComponent, DetailsComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class UserModule {}
