import { HttpClient } from '@angular/common/http';
import { Order } from './order.model';
import { Injectable } from '@angular/core';
import { OrderItem } from './order-item.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
formData!:Order;
orderItems!:OrderItem[];

uri='https://localhost:7299/api/order/create'
  constructor(private http:HttpClient) { }

  saveOrUpdateOrder(){
    debugger;
    var body={
      ...this.formData,
      OrderItems:this.orderItems
    };
    return this.http.post(this.uri,body);
  }
}
