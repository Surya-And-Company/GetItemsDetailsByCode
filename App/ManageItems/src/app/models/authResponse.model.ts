import { Role } from "./role";

export interface AuthResponse {
  token: string;
  name: string;
  profileImagePath: string;
  expireDate: Date;
  role: Role;
}
