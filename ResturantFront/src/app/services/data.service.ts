import { AuthService } from './auth.service';
import { HttpHeaders,HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { BadInput } from '../common/bad-input';
import { AppError } from '../common/app-error';
import { NotFoundError } from '../common/not-found-error';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
       Authorization: `Bearer ${this.Auth.Tokens}`
    })
  }
  constructor(@Inject(String) private baseURL:string,private HttpClient:HttpClient,private Auth:AuthService) { }


  GetAll():Observable<any[]>{
    return this.HttpClient.get<any[]>(this.baseURL,this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  Get(id:number):Observable<any>{
    return this.HttpClient.get<any>(this.baseURL +'/GetOrder/'+id ).pipe(
      catchError(this.handleError)
    );
  }

  Create(orderDto:any):Observable<any>{
    return this.HttpClient.post<any>(this.baseURL +'/Create',JSON.stringify(orderDto),this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  Update(orderDto:any):Observable<any>{
    return this.HttpClient.put<any>(this.baseURL +'/Update',JSON.stringify(orderDto),this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

  Delete(id :number){
    return this.HttpClient.delete<any>(this.baseURL+'/'+ id, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }




  private handleError(error:Response){
    if(error.status === 400)
      return throwError(new BadInput(error));
    if(error.status === 404)
      return throwError(new NotFoundError());
    return throwError(new AppError(error)); 
  }



}
