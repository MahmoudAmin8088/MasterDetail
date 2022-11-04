import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  Uri='https://localhost:7299/api/Customer';

  constructor(private HttpClient:HttpClient) { }
  GetAll():Observable<any[]>{
    return this.HttpClient.get<any[]>(this.Uri);
    }
}
