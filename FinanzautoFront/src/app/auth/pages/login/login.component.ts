import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login',
  standalone: true,  
  imports: [ReactiveFormsModule, CommonModule ],
  templateUrl: './login.component.html',
  styleUrls: []  
})
export class LoginComponent implements OnInit {

  form!: FormGroup;  
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const { username, password } = this.form.value;

    this.authService.login(username!, password!).subscribe({
      next: () => this.router.navigate(['/products']),
      error: () => this.errorMessage = 'Usuario o contraseña incorrectos'
    });
  }
}
