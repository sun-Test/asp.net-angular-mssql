import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiServiceService } from '../api-service.service';
import { WsServiceService } from '../ws-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  private readonly CREATE_USER: string = 'create-user';
  private email: string ='';
  
  constructor(private fb: FormBuilder, private _apiService: ApiServiceService,
    private _wsService: WsServiceService,
    private route: ActivatedRoute,
    private router: Router) { 
      this.form = new FormGroup({email: new FormControl(null, [Validators.required, Validators.email, Validators.minLength(5)])});

    }

  ngOnInit(): void {

  }

  async onSubmit() {
    if(this.form.invalid){
      return;
    }
    console.log('email', this.form.value.email);
  }
  async onRegister() {
    if(this.form.invalid){
      return;
    }
    console.log('register for', this.form.value.email);

    this._apiService.newUser({ email: this.form.value.email as string}).subscribe(data => {
      console.log('new user rigistered ', data);
    });
  }
  async onLogin() {
    if(this.form.invalid){
      return;
    }
    console.log('login for', this.form.value.email);

    this._apiService.login({ email: this.form.value.email as string})
    .subscribe(
      data => {
          console.log('logged in ', data);
          this.router.navigate(['/users', data.email]);
        },
      error => {
        confirm("user wrong, pls rigister first");
      }
    );
  }
}