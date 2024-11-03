import { map, Observable, OperatorFunction } from "rxjs";

export function mapCollection<T, R>(
  mapper: (item: T) => R,
): OperatorFunction<T[], R[]> {
  return (source: Observable<T[]>) => {
    return source.pipe(map((items: T[]) => items.map(mapper)));
  };
}
