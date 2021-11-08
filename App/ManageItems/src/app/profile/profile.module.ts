import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageProfileComponent } from './manage-profile/manage-profile.component';
import { RouterModule, Routes } from '@angular/router';
import { UpdateProfileComponent } from './update-profile/update-profile.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', redirectTo: 'manage' },
      { path: 'manage', component: ManageProfileComponent },
      { path: 'update', component: UpdateProfileComponent },
    ],
  },
];

@NgModule({
  declarations: [
    ManageProfileComponent,
    UpdateProfileComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ProfileModule { }
