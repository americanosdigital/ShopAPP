import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class RegisterComponent {
  fb = inject(FormBuilder);
  auth = inject(AuthService);
  router = inject(Router);
  registerForm!: FormGroup;
  errorMessage = '';
  selectedFile: File | null = null;

  constructor() {
    this.buildForm();
  }

  private buildForm(): void {
    this.registerForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      document: ['', [Validators.required]],
      role: ['', [Validators.required]],
      imageFile: [null] // O Angular não valida arquivos diretamente
    });
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    if (this.registerForm.invalid) return;

    const formData = new FormData();
    formData.append('fullName', this.registerForm.get('fullName')?.value);
    formData.append('email', this.registerForm.get('email')?.value);
    formData.append('password', this.registerForm.get('password')?.value);
    formData.append('document', this.registerForm.get('document')?.value);
    formData.append('role', this.registerForm.get('role')?.value);
    if (this.selectedFile) {
      formData.append('imageFile', this.selectedFile);
    }

    this.auth.register(formData).subscribe({
      next: () => this.router.navigate(['/auth/login']),
      error: () => this.errorMessage = 'Erro ao registrar. Tente novamente.'
    });
  }
}
