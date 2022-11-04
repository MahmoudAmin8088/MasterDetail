import { Customer } from './../../shared/customer.model';
import { CustomerService } from './../../shared/customer.service';
import { OrderItemsComponent } from './../order-items/order-items.component';
import { OrderService } from './../../shared/order.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { isNull } from '@angular/compiler/src/output/output_ast';
import { MatDialog,MatDialogConfig} from '@angular/material/dialog';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
Customer:Customer[]=[];
isValid:boolean=true;
  constructor(public service:OrderService ,private dialog:MatDialog,public cust:CustomerService) { }

  ngOnInit(): void {
    this.resetForm();
    this.getAllCustomer();
  }

  resetForm(form?:NgForm){
    if(form!= null)
    form.resetForm();
    this.service.formData ={
      OrderID:0,
      OrderNo:Math.floor(100000+Math.random()*900000).toString(),
      CustomerID:0,
      PMethod:'',
      GTotal:0
    };
    this.service.orderItems=[];
  }

  AddOrEditOrderItem(orderItemIndex:any,OrderID:any){
    const config = new MatDialogConfig();
    config.autoFocus= true;
    config.disableClose=true;
    config.width="50%";
    config.data={orderItemIndex,OrderID};
    const dialogRef = this.dialog.open(OrderItemsComponent, config).afterClosed().subscribe(res=>{
      this.UpdateGrandTotal();
    });
  }

  OnDeleteOrderItem(OrderItemID:number,i:number){
    this.service.orderItems.splice(i,1);
    this.UpdateGrandTotal();
  }

  UpdateGrandTotal(){
    
     this.service.formData.GTotal=this.service.orderItems.reduce((prev,curr)=>{
      return prev+curr.Total;
    },0);
    this.service.formData.GTotal=parseFloat((this.service.formData.GTotal).toFixed(2));

  }

  getAllCustomer(){
    this.cust.GetAll().subscribe(res=>{
      this.Customer=res;
      console.log(this.Customer);
    });

  }

  onSubmit(form:NgForm){
    if(this.validateForm()){
      this.service.saveOrUpdateOrder().subscribe(res=>{
        this.resetForm();
      })
    }
  }

  validateForm(){
    this.isValid = true;
    if(this.service.formData.CustomerID == 0)
      this.isValid=false;
    else if(this.service.orderItems.length ==0)
      this.isValid= false;
    return this.isValid;    
  }

}
