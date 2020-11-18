import { Component, OnInit, OnDestroy } from '@angular/core';
import { OrdersService } from '../orders.service';
import { Observable, Subject } from 'rxjs';
import { Order } from '../order';
import { MatTableDataSource } from '@angular/material/table';
import { map, takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();
  
  public columnsToDisplay: string[] = [

    'edit'
  ];

  public dataSource$ = this.ordersService.get().pipe(
    takeUntil(this._destroyed),
    map(x => new MatTableDataSource(x))
  );

  constructor(
    private ordersService: OrdersService,
    private router: Router
  ) { }

  public handleEditClick(order: Order): void {
    this.router.navigateByUrl(`orders/edit/${order.orderId}`);
  }

  public handleCreateClick(): void {
    this.router.navigateByUrl('orders/create');
  }
  
  ngOnDestroy(): void {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
