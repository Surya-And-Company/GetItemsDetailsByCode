import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageProfileComponent } from './manage-profile/manage-profile.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', redirectTo: 'manage' },
      { path: 'manage', component: ManageProfileComponent }
    ],
  },
];

@NgModule({
  declarations: [
    ManageProfileComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ProfileModule { }
