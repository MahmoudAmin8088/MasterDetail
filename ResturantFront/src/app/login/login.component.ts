import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin!: boolean; 
  myForm!:FormGroup;

  constructor(private authService:AuthService,private fb:FormBuilder,private router:Router,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.GForm();
  }

  GForm(){
    this.myForm=this.fb.group({
      email:['',Validators.required],
      password:['',Validators.required]
    });
  }

  Login(){
    this.authService.Login(this.myForm.value).subscribe(response=>{
      if(response){
        let returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
        this.router.navigate([returnUrl ||'/home']);
      }
      else
      this.invalidLogin = true; 
    })
  }

  get email(){
    return this.myForm.get('email');
  }
  get password(){
    return this.myForm.get('password');
  }

}
