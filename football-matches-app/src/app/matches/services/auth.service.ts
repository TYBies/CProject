import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<any>;
  private apiUrl = 'http://localhost:8080/api/User'; 
  //private apiUrl = 'https://localhost:5001/api/User'; 

  constructor(private http: HttpClient) {
    const currentUserJson = this.isBrowser() ? localStorage.getItem('currentUser') : null;
    this.currentUserSubject = new BehaviorSubject<any>(currentUserJson ? JSON.parse(currentUserJson) : null);
  }

  private isBrowser(): boolean {
    return typeof window !== 'undefined' && typeof window.localStorage !== 'undefined';
  }

  public get currentUserValue() {
    return this.currentUserSubject.value;
  }

  async login(username: string, password: string): Promise<any> {
    try {
      const response = await firstValueFrom(
        this.http.post<{ token: string, message: string }>(`${this.apiUrl}/login`, { username, password })
      );
      const user = { username, token: response.token };
      if (this.isBrowser()) {
        localStorage.setItem('currentUser', JSON.stringify(user));
      }
      this.currentUserSubject.next(user);
      console.log('User logged in:', user);
      return user;
    } catch (error: unknown) {
      console.error('Login error', error);
      if (error instanceof HttpErrorResponse) {
        throw new Error(error.error.message || 'Login failed');
      }
      throw new Error('An unknown error occurred during login');
    }
  }

  logout() {
    if (this.isBrowser()) {
      localStorage.removeItem('currentUser');
    }
    this.currentUserSubject.next(null);
  }

  async register(username: string, password: string): Promise<string> {
    try {
      const response = await firstValueFrom(
        this.http.post<{ message: string }>(`${this.apiUrl}/register`, { username, password })
      );
      return response.message;
    } catch (error: unknown) {
      console.error('Registration error', error);
      if (error instanceof HttpErrorResponse) {
        throw new Error(error.error.message || 'Registration failed');
      }
      throw new Error('An unknown error occurred during registration');
    }
  }

  isLoggedIn(): boolean {    
    return !!this.currentUserValue && !!this.currentUserValue.token;
  }

  getToken(): string | null {
    const currentUser = this.currentUserValue;
    return currentUser ? currentUser.token : null;
  }
}