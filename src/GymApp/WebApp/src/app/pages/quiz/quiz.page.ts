import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';

interface Quiz{
  questions: Question[];
}

interface Question{
  question: string;
  options: string[];
}

interface Answers{
  answersIndices: number[];
}

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.page.html',
  styleUrls: ['./quiz.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class QuizPage implements OnInit {

  constructor(private router: Router, private readonly http: HttpClient) { }

  quiz: Quiz | undefined;
  currentQuestion: Question | undefined;
  currentQuestionIndex:number = 0;

  progressPercentage:number = 0;

  answers: Answers = { answersIndices: [] };
  currentAnswerIndex:number = 0;

  ngOnInit() {
    this.http.get<Quiz>('/api/quiz').subscribe(
      (response) => {
        this.quiz = response;
        this.currentQuestion = this.quiz.questions[0];
        this.currentQuestionIndex = 0;
      }
    )
  }

  continue(){
    if(this.quiz == undefined) return;

    if(this.currentQuestionIndex < this.quiz.questions.length - 1)
    {
      this.currentQuestionIndex ++;
      this.progressPercentage = ((this.currentQuestionIndex) / this.quiz.questions.length)*100;
      this.answers?.answersIndices.push(this.currentAnswerIndex);
      this.currentQuestion = this.quiz.questions[this.currentQuestionIndex];
    }
    else if(this.currentQuestionIndex == this.quiz.questions.length - 1)
    {
      this.answers?.answersIndices.push(this.currentAnswerIndex);
      console.log(this.answers);
      this.http.post('/api/quiz', this.answers).subscribe(
        (response) => this.router.navigate(['/overview'])
      );
    }
  }

}
