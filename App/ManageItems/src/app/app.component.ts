import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { autoLogin } from './auth/state/auth.actions';
import { isAuthenticated } from './auth/state/auth.selectors';
import { AppState } from './store/app.state';
import { setErrorMessage, setSucccessMessage } from './store/shared/shared.actions';
import {
  getErrorMessage,
  getLoading,
  getSuccessMessage,
} from './store/shared/shared.selectors';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Login';
  showLoading: Observable<boolean> | undefined;
  errorMessage: Observable<string> | undefined;
  successMessage: Observable<string> | undefined;
  isAuthenticated: Observable<boolean> | undefined;
  constructor(private store: Store<AppState>) {}

  ngOnInit() {
    debugger;
    this.store.dispatch(autoLogin());
    this.showLoading = this.store.select(getLoading);
    this.errorMessage = this.store.select(getErrorMessage);
    this.successMessage = this.store.select(getSuccessMessage);
    this.isAuthenticated = this.store.select(isAuthenticated);
    
  }

   onClear(){
    this.store.dispatch(setSucccessMessage({ message: '' }));
    this.store.dispatch(setErrorMessage({ message:''}));
   }

}
