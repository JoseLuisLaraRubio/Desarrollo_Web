import { Component, HostListener, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  Result,
  RoutineTracking,
  RoutineTrackingService,
} from '@services/routine-tracking';
import { NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { Color } from '@swimlane/ngx-charts';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-routine-tracking',
  templateUrl: './routine-tracking.page.html',
  styleUrls: ['./routine-tracking.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, NgxChartsModule, UserNavBarComponent],
})
export class RoutineTrackingPage implements OnInit {
  routineTrackingData: RoutineTracking[] = [];
  repetitionsData: any[] = [];
  weightData: any[] = [];
  view: [number, number] = [0, 0];

  barChartOptions = {
    barWidth: 20,
    barPadding: 20,
  };

  constructor(
    private routineService: RoutineTrackingService,
    private route: ActivatedRoute,
  ) {}

  colorSchemeReps: Color = {
    name: 'custom-red',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#FF9999', '#FF6666', '#FF3333', '#FF0000', '#CC0000'],
  };

  colorSchemeWeight: Color = {
    name: 'custom-blue',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: [
      '#CCE5FF',
      '#99CCFF',
      '#66B2FF',
      '#3399FF',
      '#007FFF',
      '#0059B2',
      '#003D80',
    ],
  };

  ngOnInit() {
    this.getparamWorkId();
    this.updateChartSize();
  }

  getparamWorkId() {
    this.route.paramMap.subscribe((params) => {
      const workoutsId = params.get('workoutsId');
      console.log('workoutsId:', workoutsId);
      if (workoutsId) {
        const workoutIdsArray = workoutsId.split(',');
        if (workoutIdsArray.length > 0) {
          this.fetchRoutineData(workoutIdsArray);
        } else {
          console.error('No se recibieron IDs válidos en la URL');
        }
      } else {
        console.error('workoutsId not found in URL parameters');
      }
    });
  }

  fetchRoutineData(workoutIds: string[]) {
    const allResults: Result[] = [];

    workoutIds.forEach((id, index) => {
      this.routineService.getRoutineTracking(id).subscribe(
        (response: RoutineTracking[]) => {
          response.forEach((routine) => {
            allResults.push(...routine.results);
          });

          if (index === workoutIds.length - 1) {
            console.log('Todos los resultados recibidos:', allResults);
            this.processChartData(allResults);
          }
        },
        (error) => {
          console.error('Error fetching routine data:', error);
        },
      );
    });
  }

  processChartData(allResults: Result[]) {
    this.repetitionsData = [];
    this.weightData = [];

    allResults.forEach((result) => {
      let maxRepetitions = 0;
      let maxWeight = 0;
      let maxRepetitionsSetIndex = -1;
      let maxWeightSetIndex = -1;

      result.sets.forEach((set, index) => {
        if (set.repetitions > maxRepetitions) {
          maxRepetitions = set.repetitions;
          maxRepetitionsSetIndex = index;
        }

        if (set.weight > maxWeight) {
          maxWeight = set.weight;
          maxWeightSetIndex = index;
        }
      });

      const uniqueExerciseName = `${result.exercise.name}-Set`;

      if (maxRepetitionsSetIndex !== -1) {
        this.repetitionsData.push({
          name: uniqueExerciseName,
          value: maxRepetitions,
          series: `Set ${maxRepetitionsSetIndex + 1}`,
          date: this.generateRandomDate(),
        });
      }

      if (maxWeightSetIndex !== -1) {
        console.log('Agregando peso:', {
          name: uniqueExerciseName,
          value: maxWeight,
          series: `Set ${maxWeightSetIndex + 1}`,
        });
        this.weightData.push({
          name: uniqueExerciseName,
          value: maxWeight,
          series: `Set ${maxWeightSetIndex + 1}`,
        });
      }
    });

    console.log('Datos de repeticiones:', this.repetitionsData);
    console.log('Datos de peso:', this.weightData);
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.updateChartSize();
  }

  updateChartSize() {
    const width = window.innerWidth;
    const height = window.innerHeight;
    this.view = [width * 0.7, height * 0.5];
  }

  generateRandomDate(): string {
    const start = new Date();
    const end = new Date(start.getTime() - 30 * 24 * 60 * 60 * 1000);

    const randomTimestamp =
      start.getTime() - Math.random() * (start.getTime() - end.getTime());
    const randomDate = new Date(randomTimestamp);

    return randomDate.toISOString().split('T')[0];
  }
}
