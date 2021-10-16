import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddItemComponent } from './add-item/add-item.component';
import { ManageItemComponent } from './manage-item/manage-item.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DirectivesModule } from '../shared/directives/directives.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TooltipModule  } from 'ngx-bootstrap/tooltip';

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
    ReactiveFormsModule,
    CommonModule,
    TooltipModule,
    DirectivesModule,
    PaginationModule,
    RouterModule.forChild(routes),
  ]
})
export class ItemModule { }
