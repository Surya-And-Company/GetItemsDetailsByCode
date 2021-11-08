import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { setLoadingSpinner } from 'src/app/store/shared/shared.actions';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit {

  updateForm: FormGroup;
  submitAttempt = false;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<AppState>,
    ) { }

  ngOnInit(): void {
    //get user details from API
    this.store.dispatch(setLoadingSpinner({ status: true }));

    this.updateForm = this.createUpdateForm();
  }

  createUpdateForm(){
    return this.formBuilder.group({
      mobileNo : ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      name : ['', [Validators.required]],
      profileImage : ['']
    });
  }

}
