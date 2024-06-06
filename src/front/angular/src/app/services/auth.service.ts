import { Injectable } from '@angular/core';
import { env } from '../../../env';

const api = env.apiUrl + 'Auth';

interface IAuthProps {
  user: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  async logIn(auth: IAuthProps): Promise<{ token: string }> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(auth),
    }).then((res) => res.json());
  }
}
