import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Item } from 'src/app/models/item.model';
import { FileuploadService } from 'src/app/services/fileupload.service';
import { AppState } from 'src/app/store/app.state';
import {
  setErrorMessage,
  setLoadingSpinner,
  setSucccessMessage,
} from 'src/app/store/shared/shared.actions';
import { ItemService } from '../item.service';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css'],
})
export class AddItemComponent implements OnInit {
  itemForm: FormGroup;
  submitAttempt = false;
  selectedFiles: [];

  constructor(
    private formBuilder: FormBuilder,
    private itemService: ItemService,
    private fileUploadService: FileuploadService,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {
    this.itemForm = this.createItemFrom();
  }

  createItemFrom() {
    return this.formBuilder.group({
      code: ['', [Validators.required]],
      brand: ['', [Validators.required]],
      name: ['', [Validators.required]],
      sellingPrice: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.maxLength(250)]],
      images: ['', [Validators.required]],
    });
  }

  get f() {
    return this.itemForm.controls;
  }

  selectFiles(event: any) {
    this.selectedFiles = event.target.files;
  }

  onSubmit() {
    this.submitAttempt = true;
    if (this.itemForm.invalid) {
      return;
    }
    this.store.dispatch(setLoadingSpinner({ status: true }));

    let item: Item = {
      name: this.f.name.value,
      code: this.f.code.value,
      brand: this.f.brand.value,
      sellingPrice: this.f.sellingPrice.value,
      description: this.f.description.value,
      imagePath: []
    };

    this.fileUploadService.upload({ ...this.selectedFiles }).subscribe(
      (data: string[]) => {
        item.imagePath = data;
        this.addItem({ ...item });
      },
      (error) => {
        this.store.dispatch(setLoadingSpinner({ status: false }));
        this.store.dispatch(
          setErrorMessage({ message: 'Unable to upload file.' })
        );
      }
    );
  }

  addItem(item: Item) {
    this.itemService.add(item).subscribe(
      () => {
        this.store.dispatch(setLoadingSpinner({ status: false }));
        this.store.dispatch(
          setSucccessMessage({ message: 'Successfully added' })
        );
        this.submitAttempt = false;
        this.itemForm.reset();
      },
      (error) => {
        this.store.dispatch(setLoadingSpinner({ status: false }));
        this.store.dispatch(setErrorMessage({ message: 'Adding item failed' }));
      }
    );
  }
}
