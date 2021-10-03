import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Role } from 'src/app/models/role';
import { AuthState } from './auth.state';

export const AUTH_STATE_NAME = 'auth';

const getAuthState = createFeatureSelector<AuthState>(AUTH_STATE_NAME);

export const isAuthenticated = createSelector(getAuthState, (state) => {
  return state.user ? true : false;
});

export const getToken = createSelector(getAuthState, (state) => {
  return state.user ? state.user.userToken : null;
});

export const getUser = createSelector(getAuthState, (state) => {
  return state.user ? state.user.userDetails : null;
});

export const isAdmin = createSelector(getAuthState, (state) => {
  return state.user?.userRole === Role.Admin ? true : false;
});

export const getRole = createSelector(getAuthState, (state) => {
  return state.user?.userRole;
});
