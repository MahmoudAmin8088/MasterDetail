import { Router, ActivatedRoute } from '@angular/router';
import { OrderItemService } from './../../services/order-item.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from './../../services/order.service';
import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/customer';
import { CustomerService } from 'src/app/services/customer.service';
import { MatDialog,MatDialogConfig} from '@angular/material/dialog';
import { OrderItemsComponent } from '../order-items/order-items.component';
import { Order } from 'src/app/order';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  Customers:Customer[]=[];
  CustomerForm!:FormGroup;
  OrderForm!:FormGroup;
  Order!:Order;
  constructor(public service:OrderService,private dialog:MatDialog,
    public cust:CustomerService,private fb:FormBuilder, public OrderItemService:OrderItemService,
    private router:Router , private toster:ToastrService,
    private currentRouter:ActivatedRoute) { }

  ngOnInit(): void {
    this.deleteList();
    this.GorderForm();
    this.GetCustomer();

    let id = this.currentRouter.snapshot.paramMap.get('id'); 
    if(id !== null)
      this.GetOrder(parseInt(id));
  }

  
  

  //Order
  GorderForm(){
    this.OrderForm=this.fb.group({
      orderId:[0],
      orderNo:Math.floor(100000+Math.random()*900000).toString(),
      customerId:[null,Validators.required],
      pMethod:[null,Validators.required],
      gTotal:[,Validators.required],
    });
  }

 
  AddOrder(){
    var orderDto={
       ...this.Order= this. OrderForm.value,
       OrderItems:this.OrderItemService.OrderItems
    };

    this.service.Create(orderDto).subscribe(res=>{
      let items = this.OrderItemService.OrderItems.length;
      this.OrderItemService.OrderItems.splice(0,items);
      this.OrderForm.reset();
      this.toster.success('Order Added','Resturant App.');
      this.router.navigate(['/orders']);
      
    });
  }
  deleteList(){
    let items = this.OrderItemService.OrderItems.length;
    this.OrderItemService.OrderItems.splice(0,items);
  }
  updateOrder(){
    var orderDto={
       ...this.Order= this. OrderForm.value,
       OrderItems:this.OrderItemService.OrderItems
    };

    this.service.Update(orderDto).subscribe(res=>{
      let items = this.OrderItemService.OrderItems.length;
      this.OrderItemService.OrderItems.splice(0,items);
      this.OrderForm.reset();
      this.toster.success('Order updated','Resturant App.');
      this.router.navigate(['/orders']);
      
    });
  }
  GetOrder(id:number){
    debugger
    this.service.Get(id).subscribe(res=>{
       this.OrderForm.patchValue(res.order);
     
      this.OrderItemService.OrderItems=res.orderDetails;
    });
  }
  
  AddOrEditOrderItem(orderItemIndex:any,OrderID:number){
    const config = new MatDialogConfig();
    config.autoFocus= true;
    config.disableClose=true;
    config.width="50%";
    config.data={orderItemIndex,OrderID};
    const dialogRef = this.dialog.open(OrderItemsComponent, config).afterClosed().subscribe(res=>{
      
      this.UpdateGrandTotal();
      
    });
  }

  OnDeleteOrderItem(i:number){
    this.OrderItemService.OrderItems.splice(i,1);
    this.UpdateGrandTotal();
  }

  UpdateGrandTotal(){
   let GrandTotal=this.OrderItemService.OrderItems.reduce((prev,curr)=>{
    return prev +(curr).total;
        },0);
    this.OrderForm.controls['gTotal'].patchValue(GrandTotal.toFixed(2));
  }



  //Customer
  GcustomerForm(){
    this.CustomerForm=this.fb.group({
      customerId:[null], 
      customerName:['',Validators.required]
    });
  }

  GetCustomer(){
    this.cust.GetAll().subscribe(res=>{
      this.Customers=res;      
    });
  }
 

  

  onSubmit(){
    debugger
    if(this.OrderID == 0)
      this.AddOrder();
    else
      this.updateOrder();  
  }

  get Customer(){
    return this.OrderForm.get('customerId');
  }
  get pMethod(){
    return this.OrderForm.get('pMethod');
  }
  get OrderID(){
    return this.OrderId?.value;
  }
  
  get OrderId(){
    return this.OrderForm.get('orderId');
  }
 
  get GTotal(){
    return this.OrderForm.get('gTotal')?.value;
  }
  

}
