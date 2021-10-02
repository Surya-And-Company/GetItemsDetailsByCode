import { Role } from "./role";

export class User {
  constructor(
    private token: string,
    private name: string,
    private profileImagePath: string,
    private expireDate: Date,
    private role: Role
  ) {}

  get expirationDate() {
    return this.expireDate;
  }

  get userToken() {
    return this.token;
  }

  get userDetails() {
    return { name: this.name, profileImagePath: this.profileImagePath };
  }

  get userRole() {
    return this.role;
  }
}