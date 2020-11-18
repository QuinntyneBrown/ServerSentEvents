import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { HeadersInterceptor } from './headers.interceptor';
import { RedirectService } from './redirect.service';
import { LocalStorageService } from './local-storage.service';

@NgModule({
  declarations: [],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HeadersInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    AuthGuard,
    AuthService,
    LocalStorageService,
    RedirectService
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ]
})
export class CoreModule { }
