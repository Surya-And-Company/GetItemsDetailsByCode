import { Action, createReducer, on } from '@ngrx/store';
import { setErrorMessage, setLoadingSpinner, setSucccessMessage } from './shared.actions';
import { initialState, SharedState } from './shared.state';



export const reducer = createReducer(
  initialState,
  on(setLoadingSpinner, (state, action) => {
    return {
      ...state,
      showLoading: action.status,
    };
  }),
  on(setErrorMessage, (state, action) => {
    return {
      ...state,
      errorMessage: action.message,
    };
  }),
  on(setSucccessMessage, (state, action) => {
    return {
      ...state,
      errorMessage: action.message,
    };
  })
);

export function SharedReducer(state: SharedState | undefined, action: Action) {
  return reducer(state, action);
}

