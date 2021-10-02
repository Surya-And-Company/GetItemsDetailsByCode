import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { setLoadingSpinner } from 'src/app/store/shared/shared.actions';
import { loginStart } from '../state/auth.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitAttempt = false;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {
    this.loginForm = this.createLoginFrom();
  }

  createLoginFrom() {
    return this.formBuilder.group({
      mobileNo: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(15),
        ],
      ],
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  async onClick() {
    this.submitAttempt = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.store.dispatch(setLoadingSpinner({ status: true }));
    let mobile = this.f.mobileNo.value;
    let password = this.f.password.value;

    this.store.dispatch(loginStart({ mobile, password }));
  }
}
