import { OrderService } from './../../shared/order.service';
import { NgForm } from '@angular/forms';
import { ItemService } from './../../shared/item.service';
import { Component, Inject, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA ,MatDialogRef} from'@angular/material/dialog';
import { OrderItem } from 'src/app/shared/order-item.model';
import { Item } from 'src/app/shared/item.model';

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.css']
})
export class OrderItemsComponent implements OnInit {
  formData!:OrderItem;
  itemList:Item[]=[];
  isValid:boolean=true;
  constructor(@Inject(MAT_DIALOG_DATA) public data :any,
  public dialogRef:MatDialogRef<OrderItemsComponent>,private service:ItemService,
  private Orderservice:OrderService ) { }

  ngOnInit(): void {

    this.getAllItem();

    if(this.data.orderItemIndex == null)
    this.formData = {
      OrderItemID:0,
      OrderID:this.data.OrderID,
      ItemID:0,
      ItemName:'',
      Price:0,
      Quantity:0,
      Total:0 
    }

    else{
    this.formData =Object.assign({}, this.Orderservice.orderItems[this.data.orderItemIndex]);
    }
    console.log(this.Orderservice.orderItems);
  }

  getAllItem(){
    this.service.GetAll().subscribe(res=>{
      this.itemList=res;
      console.log(this.itemList);
      
    });
  }
  updatePrice(ctrl:any){
    if(ctrl.selectedIndex == 0){
      this.formData.Price=0;
      this.formData.ItemName='';

    }
  
    else{
      this.formData.Price =this.itemList[ctrl.selectedIndex-1].price;
      this.formData.ItemName =this.itemList[ctrl.selectedIndex-1].name;
    }
    this.updateTotal();
  }

  updateTotal(){
    this.formData.Total=parseFloat((this.formData.Quantity * this.formData.Price).toFixed(2));
  }

  onSubmit(form:NgForm){
    if(this.vaildateForm(form.value)){
      if(this.data.orderItemIndex == null)
        this.Orderservice.orderItems.push(form.value);
      else  
      this.Orderservice.orderItems[this.data.orderItemIndex] = form.value;
      this.dialogRef.close();
    }
  }

  vaildateForm(formData:OrderItem){
    this.isValid=true;
    if(formData.ItemID == 0)
      this.isValid=false;
    else if(formData.Quantity == 0)
      this.isValid = false;
    return this.isValid;
  }
}
