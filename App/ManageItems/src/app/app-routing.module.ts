import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren : () => import('./auth/auth.module').then((m) => m.AuthModule)
  },
  {
    path: 'login',
    loadChildren : () => import('./auth/auth.module').then((m) => m.AuthModule)
  }
  ,
  {
    path: 'item',
    loadChildren : () => import('./item/item.module').then((m) => m.ItemModule)
  },  
  {
    path: 'user',
    loadChildren : () => import('./user/user.module').then((m) => m.UserModule)
  }
  ,  
  {
    path: 'profile',
    loadChildren : () => import('./profile/profile.module').then((m) => m.ProfileModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
