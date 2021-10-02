import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { autoLogout } from 'src/app/auth/state/auth.actions';
import { isAdmin, isAuthenticated } from 'src/app/auth/state/auth.selectors';
import { AppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isAuthenticated: Observable<boolean>;
  isAdmin: Observable<boolean>;
  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.isAuthenticated = this.store.select(isAuthenticated);
    this.isAdmin = this.store.select(isAdmin);
  }

  onLogout(event: Event) {
    debugger;
    event.preventDefault();
    this.store.dispatch(autoLogout());
  }
}
