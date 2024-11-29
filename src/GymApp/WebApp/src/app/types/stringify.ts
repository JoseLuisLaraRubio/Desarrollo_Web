export type Stringify<T> = {
  [K in keyof T as T[K] extends Function ? never : K]: string;
};
