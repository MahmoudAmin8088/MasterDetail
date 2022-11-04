import { OrderItem } from './../order-item';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Order } from '../order';

@Injectable({
  providedIn: 'root'
})
export class ItemService extends DataService {
  constructor(HttpClient:HttpClient,Auth:AuthService) { 
    super('https://localhost:7299/api/item',HttpClient,Auth);
  }
}
