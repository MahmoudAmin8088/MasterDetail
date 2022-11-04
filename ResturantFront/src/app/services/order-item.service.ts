import { DataService } from './data.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderItem } from '../order-item';

@Injectable({
  providedIn: 'root'
})
export class OrderItemService extends DataService {
  
  OrderItems:OrderItem[]=[];

  constructor(HttpClient:HttpClient,Auth:AuthService) {
    super('https://localhost:7299/api/orderitem',HttpClient,Auth);
   }
}
