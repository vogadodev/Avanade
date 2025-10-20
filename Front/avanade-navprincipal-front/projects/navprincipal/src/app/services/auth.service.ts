// src/app/services/auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

// Nomes dos seus cookies
const AUTH_TOKEN_COOKIE = 'avanade.auth_token';
const REFRESH_TOKEN_COOKIE = 'avanade.refresh_token';

// Endereços do seu API Gateway
const AUTH_API_URL = '/api/auth'; // Ex: /api/auth/login, /api/auth/refresh

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    // Tentar carregar o usuário se o token existir ao iniciar
    if (this.getAuthToken()) {
      // Você pode decodificar o token aqui se precisar
    }
  }

  /**
   * Verifica se o usuário está "logado" (se o token de autenticação existe)
   */
  public isLoggedIn(): boolean {
    return this.getCookie(AUTH_TOKEN_COOKIE) !== null;
  }

  /**
   * Pega o token de autenticação (JWT) do cookie.
   */
  public getAuthToken(): string | null {
    return this.getCookie(AUTH_TOKEN_COOKIE);
  }

  /**
   * Pega o token de refresh do cookie.
   */
  private getRefreshToken(): string | null {
    return this.getCookie(REFRESH_TOKEN_COOKIE);
  }

  /**
   * Lógica de Login
   */
  login(email: string, senha: string): Observable<any> {
    return this.http.post<any>(`${AUTH_API_URL}/login`, { email, senha }).pipe(
      tap((response) => {
        this.saveTokens(response.authToken, response.refreshToken);
        this.currentUserSubject.next(response.user);
      }),
    );
  }

  /**
   * Lógica de Logout
   */
  logout(): void {
    // Remove os cookies
    this.deleteCookie(AUTH_TOKEN_COOKIE);
    this.deleteCookie(REFRESH_TOKEN_COOKIE);

    this.currentUserSubject.next(null);
    this.router.navigate(['/']);
  }

  /**
   * Chamado pelo Interceptor quando o token de autenticação (JWT) expira.
   */
  refreshToken(): Observable<any> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      this.logout();
      return throwError(() => new Error('Refresh token não encontrado.'));
    }

    return this.http.post<any>(`${AUTH_API_URL}/refresh`, { refreshToken }).pipe(
      tap((response) => {
        this.saveTokens(response.authToken, response.refreshToken);
      }),
      catchError((err) => {
        this.logout();
        return throwError(() => err);
      }),
    );
  }

  /**
   * Salva os tokens nos cookies.
   */
  private saveTokens(authToken: string, refreshToken: string): void {
    // 1. Salva o Token de Autenticação (JWT)
    // Este é um cookie de SESSÃO (sem 'expires')
    this.setCookie(AUTH_TOKEN_COOKIE, authToken);

    // 2. Salva o Refresh Token
    // Este é o cookie de 7 dias (conforme sua regra)
    this.setCookie(REFRESH_TOKEN_COOKIE, refreshToken, 7);
  }

  // --- FUNÇÕES HELPER DE COOKIE NATIVAS ---

  /**
   * Define um cookie usando document.cookie
   * @param name Nome do cookie
   * @param value Valor do cookie
   * @param days (Opcional) Dias para expirar. Se omitido, é um cookie de sessão.
   */
  private setCookie(name: string, value: string, days?: number): void {
    let expiresStr = '';
    if (days) {
      const date = new Date();
      date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
      expiresStr = `; expires=${date.toUTCString()}`;
    }
    // NOTA: 'secure' fará com que os cookies não sejam definidos em http://localhost.
    // Comente 'secure' durante o desenvolvimento local se não usar HTTPS.
    document.cookie = `${name}=${value || ''}${expiresStr}; path=/; secure; samesite=Lax`;
  }

  /**
   * Lê um cookie específico do document.cookie
   * @param name Nome do cookie a ser procurado
   */
  private getCookie(name: string): string | null {
    const nameEQ = name + '=';
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1, c.length);
      }
      if (c.indexOf(nameEQ) === 0) {
        return c.substring(nameEQ.length, c.length);
      }
    }
    return null;
  }

  /**
   * Deleta um cookie definindo sua data de expiração no passado.
   * @param name Nome do cookie a ser deletado
   */
  private deleteCookie(name: string): void {
    // Path, Secure e SameSite devem ser os mesmos de quando o cookie foi definido.
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; secure; samesite=Lax`;
  }
}
