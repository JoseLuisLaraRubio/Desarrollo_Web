import { RegisterRequest } from "./register-request";

export interface AdminRegisterRequest extends RegisterRequest {
  adminCode: string;
}
