export class User {
    constructor(
      private token: string,
      private name: string,
      private profileImagePath : string,
      private expiratioDate: Date
    ) {}
  
    get expireDate() {
      return this.expiratioDate;
    }
  
    get userToken() {
      return this.token;
    }

    get userDetails() {
      return {name: this.name, profileImagePath : this.profileImagePath};
    }
}