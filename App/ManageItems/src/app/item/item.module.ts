import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddItemComponent } from './add-item/add-item.component';
import { ManageItemComponent } from './manage-item/manage-item.component';


const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', redirectTo: 'add' },
      { path: 'add', component: AddItemComponent },
      { path: 'manage', component:  ManageItemComponent}
    ],
  },
];


@NgModule({
  declarations: [AddItemComponent,ManageItemComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class ItemModule { }
