import { Action, createReducer, on } from '@ngrx/store';
import { loginSuccess,  autoLogout } from './auth.actions';
import { AuthState, initialState } from './auth.state';

export const reducer = createReducer(
  initialState,
  on(loginSuccess, (state, action) => {
    return {
      ...state,
      user: action.user,
    };
  }),
  on(autoLogout, (state) => {
    return {
      ...state,
      user: null,
    };
  })
);

export function AuthReducer(state: AuthState | undefined, action: Action) {
  return reducer(state, action);
}