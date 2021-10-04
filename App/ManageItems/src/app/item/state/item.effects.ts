import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, tap } from 'rxjs/operators';
import { FileuploadService } from 'src/app/services/fileupload.service';
import { ItemService } from 'src/app/services/item.service';
import { AppState } from 'src/app/store/app.state';
import {
  setSucccessMessage,
  setLoadingSpinner,
  setErrorMessage,
} from 'src/app/store/shared/shared.actions';
import { addItem, addItemSuccess, getItems, loadItemsSuccess, uploadFile } from './item.actions';

@Injectable()
export class ItemEffects {
  constructor(
    private actions$: Actions,
    private itemService: ItemService,
    private fileService: FileuploadService,
    private store: Store<AppState>,
    private router: Router
  ) {}

  addItem$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(addItem),
      exhaustMap((action) => {
        return this.itemService.add(action.item).pipe(
          map(() => {
            this.store.dispatch(setLoadingSpinner({ status: false }));
            this.store.dispatch(
              setSucccessMessage({ message: 'Record added successfully' })
            ); 
            this.router.navigate(['item']);
             return addItemSuccess({redirect : true});
          }),
          catchError((errResp) => {
            this.store.dispatch(setLoadingSpinner({ status: false }));
            const errorMessage = errResp.message;
            return of(setErrorMessage({ message: errorMessage }));
          })
        );
      })
    );
  });

  getItems$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(getItems),
      exhaustMap((action) => {
        return this.itemService
          .getItems(action.date, action.status, action.pageSize, action.page)
          .pipe(
            map((items) => {
              return loadItemsSuccess({ items });
            })
          );
      })
    );
  });

  uploadFile$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(uploadFile),
      exhaustMap((action) => {
        return this.fileService.upload(action.files).pipe(
          map((paths) => {
            action.item.imagePath = paths;
            return addItem({ item: action.item });
          })
        );
      })
    );
  });
}
