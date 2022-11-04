import { OrderItem } from './order-item';
export interface Order{
    orderId:number;
    orderNo:string;
    customerId:number;
    pMethod:string;
    gTotal:number;
}