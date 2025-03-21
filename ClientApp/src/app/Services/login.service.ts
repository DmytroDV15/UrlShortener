import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { loginModel } from '../Interfaces/loginModel.interface';
import { userModel } from '../Interfaces/userModel.interface';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = 'https://localhost:7217/api/Login';

  constructor(private http: HttpClient) {}

  login(user: loginModel): Observable<any> {
    return this.http.post<any>(this.apiUrl, user).pipe(
      tap((result: userModel) => {
        sessionStorage.setItem('userID', result.id.toString());
      }),
    );
  }
}
