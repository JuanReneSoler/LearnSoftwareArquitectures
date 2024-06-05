import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-log-in-form',
  templateUrl: './log-in-form.component.html',
  styleUrls: ['./log-in-form.component.css'],
})
export class LogInFormComponent implements OnInit {
  constructor(private service: AuthService) {}

  viewModel: FormGroup;

  ngOnInit(): void {
    this.viewModel = {
      user: ['', Validators.required],
    };
  }

  async onSubmit(event: Event) {
    event.preventDefault();
    await this.service.logIn(this.viewModel).then((res) => {
      console.log(res);
      //localStorage.setItem('token', `Bearer ${res.token}`);
    });
  }
}
