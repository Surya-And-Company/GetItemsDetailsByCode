import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Role } from './models/role';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'login',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'unauthorize',
    loadChildren: () => import('./unauthorize/unauthorize.module').then((m) => m.UnauthorizeModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'item',
    loadChildren: () => import('./item/item.module').then((m) => m.ItemModule),
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    data: {
      userRoles: [Role.User, Role.Admin],
    },
  },
  {
    path: 'user',
    loadChildren: () => import('./user/user.module').then((m) => m.UserModule),
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    data: {
      userRoles: [Role.Admin],
    },
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./profile/profile.module').then((m) => m.ProfileModule),
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    data: {
      userRoles: [Role.Admin, Role.User],
    },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
