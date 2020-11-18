import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders/orders.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { OrdersService } from './orders.service';
import { SharedModule } from '../_shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [OrdersComponent, EditOrderComponent],
  providers: [
    OrdersService
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    OrdersComponent, 
    EditOrderComponent
  ]
})
export class OrdersModule { }
