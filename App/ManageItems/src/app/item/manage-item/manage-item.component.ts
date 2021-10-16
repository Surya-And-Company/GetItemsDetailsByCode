import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { ItemResponse, ItemsRespose } from 'src/app/models/itemResponse.Model';
import { FileuploadService } from 'src/app/services/fileupload.service';
import { AppState } from 'src/app/store/app.state';
import {
  setErrorMessage,
  setLoadingSpinner,
} from 'src/app/store/shared/shared.actions';
import { ItemService } from '../item.service';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { isAdmin } from 'src/app/auth/state/auth.selectors';

@Component({
  selector: 'app-manage-item',
  templateUrl: './manage-item.component.html',
  styleUrls: ['./manage-item.component.css'],
})
export class ManageItemComponent implements OnInit {
  isAdmin: Observable<boolean>;
  items: ItemResponse[] = [];
  totalItems = 0;
  page: number = 1;
  date: Date | null = null;
  status: boolean | null = null;
  pageSize: number = 15;
  imgPath: string = '';

  constructor(
    private store: Store<AppState>,
    private itemService: ItemService
  ) {}

  ngOnInit(): void {
    this.isAdmin = this.store.select(isAdmin);
    this.imgPath = environment.imageUrl;
    this.loadItems();
  }

  onExpand(event: any) {
    if (event.target.children.length == 1) {
      if (event.target.children[0].innerText == 'add_circle_outline') {
        event.target.children[0].innerText = 'remove_circle_outline';
      } else {
        event.target.children[0].innerText = 'add_circle_outline';
      }
    } else {
      if (event.target.innerText == 'add_circle_outline') {
        event.target.innerText = 'remove_circle_outline';
      } else {
        event.target.innerText = 'add_circle_outline';
      }
    }
  }

  pageChanged(e: PageChangedEvent) {
    debugger;
  }

  loadItems() {
    this.store.dispatch(setLoadingSpinner({ status: true }));
    this.itemService
      .getItems(this.date, this.status, this.pageSize, this.page)
      .subscribe(
        (response: ItemsRespose) => {
          this.totalItems = response.totalRecord;
          this.items = response.items;
          this.store.dispatch(setLoadingSpinner({ status: false }));
        },
        (error) => {
          this.store.dispatch(setLoadingSpinner({ status: false }));
          console.log(error);
        }
      );
  }

  deleteImage(path: string, id: string) {
    this.store.dispatch(setLoadingSpinner({ status: true }));
    this.itemService.deleteUndeleteImage(id, path).subscribe(
      () => {
        let isDeleted = this.items
          .find((x) => x.id == id)
          ?.itemImages?.find((y) => y.path == path)?.isDeleted;
        let isUpdated = false;
        this.items
          .filter((x) => x.id == id)
          .forEach((x) => {
            x.itemImages?.forEach((y) => {
              if (y.path == path) {
                y.isDeleted = !isDeleted;
                isUpdated = true;
                return;
              }
            });
            if (isUpdated) {
              return;
            }
          });
        this.store.dispatch(setLoadingSpinner({ status: false }));
      },
      (error) => {
        this.store.dispatch(setLoadingSpinner({ status: false }));
        this.store.dispatch(
          setErrorMessage({ message: 'Unable to delete item' })
        );
      }
    );
  }

  approve(id: string) {
    this.store.dispatch(setLoadingSpinner({ status: true }));
    this.itemService.approveAndDisapproveItem(id).subscribe(
      () => {
        let isApproved = this.items.find((x) => x.id == id)?.isApproved;
        this.items
          .filter((x) => x.id == id)
          .forEach((y) => {
            y.isApproved = !isApproved;
            return;
          });
        this.store.dispatch(setLoadingSpinner({ status: false }));
      },
      (error) => {
        this.store.dispatch(setLoadingSpinner({ status: false }));
        this.store.dispatch(setErrorMessage({ message: 'Unable to update' }));
      }
    );
  }
}
