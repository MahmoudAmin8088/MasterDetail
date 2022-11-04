import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmedValidator } from '../ConfirmedValidator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  myForm!:FormGroup;
  invalidSignUp!: boolean;
  constructor(private authService:AuthService ,private router:Router,private fb:FormBuilder) { }

  ngOnInit(): void {
    this.GForm();
  }

  GForm(){
    this.myForm=this.fb.group({
      fullName: ['',Validators.required],
      userName:['',Validators.required],
      email:['',Validators.required],
      password:['',Validators.required],
      confirmPassword:['',Validators.required]
    }
    );
  }

  SignUp(){
    this.authService.SignUp(this.myForm.value)
    .subscribe(result=>{
      if(result)
        this.router.navigate(['/login']);
      else  
      this.invalidSignUp=true;
    });
  }

  
   get fullName(){
    return this.myForm.get('fullName');
   }
   get userName(){
    return this.myForm.get('userName');
   }
   get email(){
    return this.myForm.get('email');
   }
   get password(){
    return this.myForm.get('password');
   }
   get Cpassword(){
    return this.myForm.get('confirmPassword');
   }



}
