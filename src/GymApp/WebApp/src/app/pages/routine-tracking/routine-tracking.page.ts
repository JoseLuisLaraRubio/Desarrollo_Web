import { Component, HostListener, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoutineTracking, RoutineTrackingService } from '@services/routine-tracking';
import { NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { Color } from '@swimlane/ngx-charts';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';
import { ActivatedRoute } from '@angular/router'; // Para obtener parámetros de la URL
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

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
    private route: ActivatedRoute
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
    domain: ['#CCE5FF', '#99CCFF', '#66B2FF', '#3399FF', '#007FFF', '#0059B2', '#003D80'],
  };

  ngOnInit() {
    this.getparamWorkId();
    this.updateChartSize();
  }

  getparamWorkId() {
    this.route.paramMap.subscribe((params) => {
      const workoutsId = params.get('workoutsId');
      if (workoutsId) {
        const id = +workoutsId;
        if (!isNaN(id)) {
          this.fetchRoutineData(id);
        } else {
          console.error('Invalid workoutsId:', workoutsId);
        }
      } else {
        console.error('workoutsId not found in URL parameters');
      }
    });
  }

  fetchRoutineData(workoutsId: number) {
    this.routineService.getRoutineTracking(workoutsId).subscribe(
      (data) => {
        this.routineTrackingData = data;
        this.processChartData();
      },
      (error) => {
        console.error('Error fetching routine data:', error);
        this.routineTrackingData = [];
      }
    );
  }

  processChartData() {
    this.repetitionsData = [];
    this.weightData = [];

    this.routineTrackingData.forEach((routine) => {
      routine.results.forEach((result) => {
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
            date: routine.date,
          });
        }

        if (maxWeightSetIndex !== -1) {
          this.weightData.push({
            name: uniqueExerciseName,
            value: maxWeight,
            series: `Set ${maxWeightSetIndex + 1}`,
            date: routine.date,
          });
        }
      });
    });
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
}
