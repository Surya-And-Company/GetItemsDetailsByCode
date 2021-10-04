import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item.model';

@Injectable({
  providedIn: 'root',
})
export class ItemService {
  constructor(private http: HttpClient) {}

  add(item: Item): Observable<any> {
    return this.http.post(`${environment.serviceUrlv1}Item/AddItem`, item);
  }

  getItem(code: string): Observable<Item> {
    return this.http.get<Item>(
      `${environment.serviceUrlv1}Item/GetItem/${code}`
    );
  }

  getItems(
    date: Date | null,
    status: boolean | null,
    pageSize: number,
    page: number
  ): Observable<Item[]> {
    return this.http.get<Item[]>(
      `${environment.serviceUrlv1}Item/GetItems/${date}/${status}/${pageSize}/${page}`
    );
  }
}
