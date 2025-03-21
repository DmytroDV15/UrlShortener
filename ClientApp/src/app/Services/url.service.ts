import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Url } from '../Interfaces/url.inteface';
import { urlInfoModel } from '../Interfaces/urlInfoModel.interface';

@Injectable({
  providedIn: 'root',
})
export class UrlService {
  private apiUrl = 'https://localhost:7217/api/Url';

  constructor(private http: HttpClient) {}

  getAllUrls(): Observable<Url[]> {
    return this.http.get<Url[]>(this.apiUrl);
  }

  shortenUrl(url: string, currentUser: number): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/shorten`, {
      currentUserId: currentUser,
      originalUrl: url,
    });
  }

  deleteUrl(urlId: number, currentUser: number): Observable<string> {
    return this.http.delete<string>(
      `${this.apiUrl}/${urlId}/${currentUser}`,
      {},
    );
  }

  infoUrl(urlId: number): Observable<urlInfoModel> {
    return this.http.get<urlInfoModel>(`${this.apiUrl}/info/${urlId}`, {});
  }
}
