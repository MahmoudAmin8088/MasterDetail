import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { OrderService } from './../services/order.service';
import { Component, OnInit } from '@angular/core';
import { AppError } from '../common/app-error';
import { NotFoundError } from '../common/not-found-error';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orderList:any;
  constructor(private OrderService:OrderService,private router:Router,private tostar:ToastrService) { }

  ngOnInit(): void {
    this.GetOrders();
  }

  GetOrders(){
    return this.OrderService.GetAll().subscribe(res=>{
      this.orderList=res;
    });
  }
  DeleteOrder(id:any){
    debugger
    this.OrderService.Delete(id).subscribe(res=>{
      this.GetOrders();
      this.tostar.warning("Deleted successfuly","Resturant App");

    }
    );
  }

  openForEdit(id:number){
    this.router.navigate(['/order/edit/'+id]);
  }
  Delete(id:any){
    if(confirm('Are You Sure To Delete This Order ?'))
  this.DeleteOrder(id);
  }

}
