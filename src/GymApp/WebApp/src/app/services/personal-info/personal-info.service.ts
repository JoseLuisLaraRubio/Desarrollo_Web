import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PersonalInfo } from "./data";
import { Nullable } from "@customTypes/nullable";

@Injectable({
  providedIn: "root",
})
export class PersonalInfoService {
  constructor(private readonly _httpClient: HttpClient) {}

  public getPersonalInfo(): Observable<Nullable<PersonalInfo>> {
    const apiPath = this.getApiPath();

    return this._httpClient.get<PersonalInfo>(apiPath);
  }

  public updatePersonalInfo(personalInfo: PersonalInfo): Observable<void> {
    const apiPath = this.getApiPath();

    return this._httpClient.put<void>(apiPath, personalInfo);
  }

  private getApiPath(path: string = ""): string {
    return `api/personal-info/${path}`;
  }
}
