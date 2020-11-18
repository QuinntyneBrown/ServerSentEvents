import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OrdersModule } from './orders/orders.module';
import { SharedModule } from './_shared/shared.module';
import { CoreModule } from './_core/core.module';
import { baseUrl } from './_core/constants';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    OrdersModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: baseUrl, useValue: 'https://localhost:44346/' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
