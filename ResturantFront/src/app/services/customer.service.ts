import { DataService } from './data.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CustomerService extends DataService {

  constructor(HttpClient:HttpClient,Auth:AuthService) {
    super('https://localhost:7299/api/customer',HttpClient,Auth);
   }
}
