import { createAction, props } from '@ngrx/store';
import { Item } from 'src/app/models/item.model';

export const ADD_ITEM = '[item page] add item';
export const ADDED_ITEM_SUCCESS = '[item page] add item success';
export const GET_ITEM = '[item page] get item';
export const GET_ITEMS = '[item page] get items';
export const GET_ITEMS_SUCCESS = '[item page] get items success';
export const UPLOAD_FILE = '[item page] upload file';
export const UPLOAD_FILE_SUCCESS = '[item page] upload file success';

export const addItem = createAction(ADD_ITEM, props<{ item: Item }>());

export const addItemSuccess = createAction(ADDED_ITEM_SUCCESS, props<{ redirect : boolean }>());


export const getItem = createAction(GET_ITEM, props<{ code: string }>());

export const getItems = createAction(
  GET_ITEMS,
  props<{
    date: Date | null;
    status: boolean | null;
    pageSize: number;
    page: number;
  }>()
);

export const loadItemsSuccess = createAction(
  GET_ITEMS_SUCCESS,
  props<{ items: Item[] }>()
);

export const uploadFile = createAction(
  UPLOAD_FILE,
  props<{ files: []; item: Item }>()
);

