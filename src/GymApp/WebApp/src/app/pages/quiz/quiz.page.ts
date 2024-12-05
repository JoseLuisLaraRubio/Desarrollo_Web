import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { QuizService } from "@services/quiz/quiz.service";
import { Answers, Question, Quiz } from "@services/quiz/data";

@Component({
  selector: "app-quiz",
  templateUrl: "./quiz.page.html",
  styleUrls: ["./quiz.page.scss"],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class QuizPage implements OnInit {
  constructor(
    private readonly router: Router,
    private readonly quizService: QuizService,
  ) {}

  quiz: Quiz | undefined;
  currentQuestion: Question | undefined;
  currentQuestionIndex: number = 0;

  progressPercentage: number = 0;

  answers: Answers = { answersIndices: [] };
  currentAnswerIndex: number = 0;

  ngOnInit() {
    this.quizService.getQuiz().subscribe((response) => {
      this.quiz = response;
      this.currentQuestion = this.quiz.questions[0];
      this.currentQuestionIndex = 0;
    });
  }

  continue() {
    if (this.quiz == undefined) return;

    if (this.currentQuestionIndex < this.quiz.questions.length - 1) {
      this.currentQuestionIndex++;
      this.progressPercentage =
        (this.currentQuestionIndex / this.quiz.questions.length) * 100;
      this.answers?.answersIndices.push(this.currentAnswerIndex);
      this.currentQuestion = this.quiz.questions[this.currentQuestionIndex];
    } else if (this.currentQuestionIndex == this.quiz.questions.length - 1) {
      this.answers?.answersIndices.push(this.currentAnswerIndex);
      console.log(this.answers);

      this.quizService
        .postAnswers(this.answers)
        .subscribe((response) => this.router.navigate(["/overview"]));
    }
  }
}
