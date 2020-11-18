import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '../_core/constants';
import { HttpClient } from '@angular/common/http';
import { Order } from './order';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Order[]> {
    return this._client.get<{ orders: Order[] }>(`${this._baseUrl}api/orders`)
      .pipe(
        map(x => x.orders)
      );
  }

  public getById(options: { id: number }): Observable<Order> {
    return this._client.get<{ order: Order }>(`${this._baseUrl}api/orders/${options.id}`)
      .pipe(
        map(x => x.order)
      );
  }

  public remove(options: { order: Order }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/orders/${options.order.orderId}`);
  }

  public save(options: { order: Order }): Observable<{ id: number }> {
    return this._client.post<{ id: number }>(`${this._baseUrl}api/orders`, { order: options.order });
  }  
}
