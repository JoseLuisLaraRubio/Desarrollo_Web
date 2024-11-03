import { BehaviorSubject, Observable, switchMap, tap } from "rxjs";

export class ObservableCollection<T> {
  private readonly subject: BehaviorSubject<T[]> = new BehaviorSubject<T[]>([]);

  public readonly items$: Observable<T[]> = this.subject.asObservable();

  public getItems(): readonly T[] {
    return this.subject.value;
  }

  public loadItems(observable: Observable<T[]>): Observable<T[]> {
    return observable.pipe(
      tap((items) => this.subject.next(items)),
      switchMap(() => this.items$),
    );
  }

  public add(item: T): void {
    this.subject.next([...this.subject.value, item]);
  }

  public removeAt(index: number): void {
    const array = this.subject.value;

    if (index < 0 || index >= array.length) {
      throw new RangeError(`Index "${index}" is out of bounds.`);
    }

    array.splice(index, 1);
    this.subject.next(array);
  }

  public removeFirstWhere(
    predicate: (value: T, index: number, obj: T[]) => boolean,
  ): void {
    const removeIndex = this.subject.value.findIndex(predicate);
    if (removeIndex < 0) {
      throw new RangeError(`No element matching the predicate was found.`);
    }

    this.removeAt(removeIndex);
  }
}
