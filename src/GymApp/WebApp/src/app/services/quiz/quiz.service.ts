import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Answers, Quiz } from "./data";

@Injectable({
  providedIn: "root",
})
export class QuizService {
  constructor(private readonly httpClient: HttpClient) {}

  public getQuiz(): Observable<Quiz> {
    const apiPath = this.getApiPath();

    return this.httpClient.get<Quiz>(apiPath);
  }

  public postAnswers(answers: Answers): Observable<void> {
    const apiPath = this.getApiPath();

    return this.httpClient.post<void>(apiPath, answers);
  }

  private getApiPath(path: string = ""): string {
    return `api/quiz/${path}`;
  }
}
