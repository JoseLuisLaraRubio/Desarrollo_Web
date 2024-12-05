import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
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

  public getQuizStatus(): Observable<boolean> {
    if (localStorage.getItem("quizStatus") === "true") {
      return of(true);
    }
    const apiPath = this.getApiPath("status");

    return this.httpClient.get<boolean>(apiPath);
  }

  private getApiPath(path: string = ""): string {
    return `api/quiz/${path}`;
  }
}
