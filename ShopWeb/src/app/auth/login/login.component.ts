import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class LoginComponent {
  fb = inject(FormBuilder);
  http = inject(HttpClient);
  loginForm!: FormGroup;
  errorMessage = '';

  constructor(
    // private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.buildForm();
  }

  //criando o formulário de autenticação
form = this.fb.group({
email: ['', [Validators.required, Validators.email]],
senha: ['', [Validators.required, Validators.minLength(8)]]
});


  private buildForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    const { email, password } = this.loginForm.value;
    this.auth.login(email!, password!).subscribe({
      next: () => this.router.navigate(['/admin']),
      error: () => this.errorMessage = 'Login inválido. Verifique suas credenciais.'
    });
  }
}
