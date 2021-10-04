import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddItemComponent } from './add-item/add-item.component';
import { ManageItemComponent } from './manage-item/manage-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DirectivesModule } from '../shared/directives/directives.module';
import { EffectsModule } from '@ngrx/effects';
import { ItemEffects } from './state/item.effects';


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
    RouterModule.forChild(routes),
    EffectsModule.forFeature([ItemEffects]),
  ]
})
export class ItemModule { }
