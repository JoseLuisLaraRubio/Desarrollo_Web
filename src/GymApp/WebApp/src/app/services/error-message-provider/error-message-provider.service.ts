import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";

interface ProblemDetails {
  title: string;
  errors: { [key: string]: string[] };
}

@Injectable({
  providedIn: "root",
})
export class ErrorMessageProviderService {
  public formatErrorResponse(response: HttpErrorResponse): string {
    if (!this.isProblemDetails(response.error)) {
      return response.error?.title ?? "Unknown error";
    }

    const details = response.error;
    const formattedTitle = this.formatDetailsTitle(details.title);
    const formattedErrors = Object.values(details.errors).join("\n");

    return `${formattedTitle}\n\n${formattedErrors}`;
  }

  private formatDetailsTitle(title: string) {
    return (title.endsWith(".") ? title.slice(0, -1) : title) + ":";
  }

  private isProblemDetails(error: object): error is ProblemDetails {
    return (
      typeof error === "object" &&
      error !== null &&
      "title" in error &&
      "errors" in error &&
      typeof error.title === "string" &&
      typeof error.errors === "object"
    );
  }
}
