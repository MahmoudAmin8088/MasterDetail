import { ItemService } from './../../services/item.service';
import { OrderItemService } from './../../services/order-item.service';
import { Item } from './../../item';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.css']
})
export class OrderItemsComponent implements OnInit {
OrderItemForm!:FormGroup;
Items!:Item[];
  constructor( @Inject(MAT_DIALOG_DATA) public data :any, public dialogRef:MatDialogRef<OrderItemsComponent>,private fb:FormBuilder,private OrderItemService:OrderItemService,private ItemService:ItemService) { }

  ngOnInit(): void {
     this.GOrderItemForm();
      this.AddOrEditOrderItem();
      this.GetItems();
    }  

  GOrderItemForm(){
    this.OrderItemForm=this.fb.group({
      orderItemId:[0],
      orderId:this.data.OrderID,
      itemId:[0,Validators.required],
      itemName:[''],
      price:['',Validators.required],
      quantity:[null,Validators.required],
      total:['',Validators.required]
    });
  }
  AddOrEditOrderItem(){
    if(this.data.orderItemIndex== null)
      this.GOrderItemForm();
    else{
    let  editItem = this.OrderItemService.OrderItems[this.data.orderItemIndex];
    this.OrderItemForm.patchValue(editItem);
    }
     
  }
 
  GetItems(){
    this.ItemService.GetAll().subscribe(res=>{
      this.Items=res;
      
    });
  }

  updatePrice(ctrl:any){
    if(ctrl.selectedIndex == 0){
      this.OrderItemForm.controls['price'].setValue(0);
      this.OrderItemForm.controls['itemName'].setValue('');
    }
    else{
      this.OrderItemForm.controls['price'].setValue(this.Items[ctrl.selectedIndex-1].price);
      this.OrderItemForm.controls['itemName'].setValue(this.Items[ctrl.selectedIndex-1].name);
    }
  }  
  updateTotal(){
      let value =this.Quantity?.value * this.price?.value;
      this.OrderItemForm.controls['total'].setValue(value);
  }
  onSubmit(){
    if(this.data.orderItemIndex == null)
      this.OrderItemService.OrderItems.push(this.OrderItemForm.value);
    else  
    this.OrderItemService.OrderItems[this.data.orderItemIndex] = this.OrderItemForm.value;
      
      this.dialogRef.close();
  }

get ItemId(){
  return this.OrderItemForm.get('itemId');
}
get Quantity(){
  return this.OrderItemForm.get('quantity');
}
get price(){
  return this.OrderItemForm.get('price');
}


}
