import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject,tap } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'auth_token';
  private apiUrl = '/api/login'
  private _isLogged = new BehaviorSubject<boolean>(this.validateToken());

  isLoggedIn$ = this._isLogged.asObservable();
  constructor(private httpCliente: HttpClient) { }

    login(username: string, password: string) {
    return this.httpCliente.post<{token: string}>(`${this.apiUrl}`, {username, password})
      .pipe(
        tap(response => {
          localStorage.setItem(this.tokenKey, response.token);
          this._isLogged.next(true);
        })
      );
  }

    getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
private validateToken(): boolean {
  if (typeof window === 'undefined') {
    return false;
  }
  return !!localStorage.getItem(this.tokenKey);
}

}
