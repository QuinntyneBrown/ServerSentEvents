import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LocalStorageService } from './local-storage.service';
import { map } from 'rxjs/operators';
import { accessTokenKey, baseUrl } from './constants';

@Injectable()
export class AuthService {
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _httpClient: HttpClient,
    private _localStorageService: LocalStorageService
  ) {}

  public logout() {
    this._localStorageService.put({ name: accessTokenKey, value: null });
  }

  public tryToLogin(options: { username: string; password: string }) {
    return this._httpClient.post<any>(`${this._baseUrl}api/users/token`, options).pipe(
      map(response => {
        this._localStorageService.put({ name: accessTokenKey, value: response.accessToken });
        return response.accessToken;
      })
    );
  }
}
