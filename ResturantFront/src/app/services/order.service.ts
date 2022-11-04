import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from './data.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../order';

@Injectable({
  providedIn: 'root'
})
export class OrderService extends DataService {
  constructor(HttpClient:HttpClient,Auth:AuthService,private fb:FormBuilder) 
  {
    super('https://localhost:7299/api/order',HttpClient,Auth);
   }

  

}
