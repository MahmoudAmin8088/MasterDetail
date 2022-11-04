import { OrderComponent } from './orders/order/order.component';
import { OrdersComponent } from './orders/orders.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from './services/auth-guard.service';
import { NoAccessComponent } from './no-access/no-access.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
{
  path:'',
  component:HomeComponent
},  
{
  path:'login',
  component:LoginComponent
},
{ path:'home',
   component:HomeComponent
},
{
  path:'signup',
  component:SignUpComponent
},
{
  path:'orders',
  component:OrdersComponent,
  canActivate:[AuthGuardService]

},
{
path:'order',children:[
 
  
  {path:'',component:OrderComponent,canActivate:[AuthGuardService] },
  {path:'edit/:id',component:OrderComponent,canActivate:[AuthGuardService]},

]
},
{
  path:'no-access',
  component:NoAccessComponent
},
{
  path:'**',
  component:NotFoundComponent
}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
