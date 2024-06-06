import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-log-in-form',
  templateUrl: './log-in-form.component.html',
  styleUrls: ['./log-in-form.component.css'],
})
export class LogInFormComponent {
  viewModel: FormGroup;

  constructor(
    private service: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.viewModel = this.fb.group({
      user: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  async onSubmit(event: Event) {
    event.preventDefault();
    await this.service.logIn(this.viewModel.value).then((res) => {
      localStorage.setItem('token', `Bearer ${res.token}`);
      this.router.navigate(['/home']);
    });
  }
}
