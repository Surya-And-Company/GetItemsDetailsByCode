import { Inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { getRole, isAuthenticated } from '../auth/state/auth.selectors';
import { Role } from '../models/role';
import { AppState } from '../store/app.state';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild  {
  constructor(private router: Router, private store: Store<AppState>) {}
  
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
      debugger;
    const allowedUserRoles = this.getRoutePermissions(childRoute);
    return this.store.select(getRole).pipe(
      map((role) => {
        if (!this.areUserRolesAllowed(role, allowedUserRoles)) {
          this.router.navigate(['unauthorize']);
        }
        return true;
      })
    );
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
      debugger;
    return this.store.select(isAuthenticated).pipe(
      map((authenticate) => {
        if (!authenticate) {
          this.router.navigate(['login']);
        }
        return true;
      })
    );
  }

  private getRoutePermissions(route: ActivatedRouteSnapshot): Role[] | null {
    if (route.data && route.data.userRoles) {
      return route.data.userRoles as Role[];
    }
    return null;
  }

  private areUserRolesAllowed(
    userRole: Role | any,
    allowedUserRoles: Role[] | any
  ): boolean {
    for (const allowedRole of allowedUserRoles) {
      if (userRole.toLowerCase() === allowedRole.toLowerCase()) {
        return true;
      }
    }
    return false;
  }
}
