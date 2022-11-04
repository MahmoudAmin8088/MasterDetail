import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpOptions ={
    headers: new HttpHeaders({
      'Content-Type':'application/json',
     // Authorization: `Bearer ${this.Tokens}`
    })
  }
  baseUrI='https://localhost:7299/api/auth';
  constructor(private Http:HttpClient) { }

  Login(credentials:any){
    return this.Http.post(this.baseUrI+'/login',JSON.stringify(credentials),this.httpOptions).pipe(
      map(res=>{
        let result:any = res;
        if(result && result.token){
          localStorage.setItem('token',result.token);
          return true;
        }
        return false;
      })
    );
  }
  SignUp(credentials:any){
    debugger;
    return this.Http.post(this.baseUrI +'/register',JSON.stringify(credentials),this.httpOptions).pipe(
      map(res=>{
        let result :any = res;
        if(result)
          return true;
     
        return false;
      })
    );
  }

  LogOut(){
    localStorage.removeItem('token');
  }

  get Tokens(){
    let token = localStorage.getItem('token');
    return token;
  }

  IsLoggedIn(){
    //let token = localStorage.getItem('token');
    let token =this.Tokens;

    if(!token)
      return false;

    let jwtHelper = new JwtHelperService();
    let isExpired = jwtHelper.isTokenExpired(token);
    return !isExpired;
  }

  get CurrentUser(){
    //let token = localStorage.getItem('token');
    let token =this.Tokens;
    return new JwtHelperService().decodeToken(token!);
  }

  GetUserRole(){
    const token = this.Tokens;
    if(!token)
      return;
    let jwtHelper = new JwtHelperService();
    let tokenData =jwtHelper.decodeToken(token);

    let role = tokenData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    return role;
  }
  

}
