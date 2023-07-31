import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  currentUser?: User;

  constructor() { }

  loginUser(user: User){
    console.log("user with " + user.email + " logged in");
    this.currentUser = user;
  }

}


interface User {
  email: string;
}