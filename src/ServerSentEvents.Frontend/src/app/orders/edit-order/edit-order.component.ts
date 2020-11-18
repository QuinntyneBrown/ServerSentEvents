import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrdersService } from '../orders.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Order } from '../order';
import { takeUntil, map } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.scss']
})
export class EditOrderComponent implements OnInit, OnDestroy {

  public order: Order = {} as Order;
  private readonly _destroyed: Subject<void> = new Subject();

  public form = new FormGroup({     
    total: new FormControl(null, [Validators.required]),      
  });
  
  constructor(
    private ordersService: OrdersService
  ) { }

  ngOnInit(): void {
    this.ordersService.get().subscribe();
  }

  public handleSaveClick(): void {
    const order: Order = {} as Order;

    this.order.total = this.form.value.total;

    this.ordersService.save({ order: this.order }).pipe(
      takeUntil(this._destroyed)
    ).subscribe(
      () => this.form.reset(),
      errorResponse => {
        // handle error
      }
    );

  }

  public handleCancelClick(): void {

  }

  ngOnDestroy(): void {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
