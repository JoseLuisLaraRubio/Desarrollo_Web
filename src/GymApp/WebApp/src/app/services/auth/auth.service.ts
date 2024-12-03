import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { catchError, Observable, of } from "rxjs";

import {
  LoginRequest,
  RegisterRequest,
  AdminRegisterRequest,
  UserInfoResponse,
} from "./";
import { Nullable } from "@customTypes/.";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private readonly _httpClient: HttpClient) {}

  public registerAdmin(request: AdminRegisterRequest): Observable<void> {
    const apiPath: string = this.getApiPath("register-admin");

    return this._httpClient.post<void>(apiPath, request);
  }

  public registerUser(request: RegisterRequest): Observable<void> {
    const apiPath: string = this.getApiPath("register");

    return this._httpClient.post<void>(apiPath, request);
  }

  public getAdminRegisterStatus(): Observable<boolean> {
    const apiPath: string = this.getApiPath("admin-register-status");

    const adminRegisterStatus = this._httpClient.get<boolean>(apiPath);

    return adminRegisterStatus;
  }

  public login(request: LoginRequest): Observable<void> {
    const apiPath: string = this.getApiPath("login");

    return this._httpClient.post<void>(apiPath, request);
  }

  public logout(): Observable<void> {
    const apiPath: string = this.getApiPath("logout");

    return this._httpClient.post<void>(apiPath, {});
  }

  public getUserInfo(): Observable<Nullable<UserInfoResponse>> {
    const apiPath: string = this.getApiPath("manage/info");

    const userInfo = this._httpClient.get<UserInfoResponse>(apiPath).pipe(
      catchError(() => {
        return of(null);
      }),
    );

    return userInfo;
  }

  private getApiPath(path: string): string {
    return `api/account/${path}`;
  }
}
