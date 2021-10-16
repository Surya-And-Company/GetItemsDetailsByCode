import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item.model';
import { ItemsRespose } from '../models/itemResponse.Model';

@Injectable({
  providedIn: 'root',
})
export class ItemService {
  constructor(private http: HttpClient) {}

  add(item: Item): Observable<any> {
    return this.http.post(`${environment.serviceUrlv1}Item/AddItem`, item);
  }

  deleteUndeleteImage(id: string, path: string){
    return this.http.post(`${environment.serviceUrlv1}Item/DeleteAndUndeleteImage`, {itemId: id, path: path});    
  }

  approveAndDisapproveItem(id: string){
    return this.http.post(`${environment.serviceUrlv1}Item/ApproveAndDisapproveItem/`+id,{});    
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
  ): Observable<ItemsRespose> {
    return this.http.post<ItemsRespose>(
      `${environment.serviceUrlv1}Item/GetItems`,
      { date: date, status: status, pageSize: pageSize, page: page }
    );
  }
}
