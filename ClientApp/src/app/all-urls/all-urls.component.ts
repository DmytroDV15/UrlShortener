import { Component, OnInit } from '@angular/core';
import { UrlService } from '../Services/url.service';
import { Url } from '../Interfaces/url.inteface';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-all-urls',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatIconModule,
    RouterOutlet,
    RouterLink,
  ],
  templateUrl: './all-urls.component.html',
  styleUrl: './all-urls.component.scss',
})
export class AllUrlsComponent implements OnInit {
  displayedColumns: string[] = ['originalUrl', 'shortUrl', 'info', 'actions'];
  urls: Url[] = [];
  newUrl: string = '';

  constructor(private urlService: UrlService) {}

  get isLoggedIn(): boolean {
    const userId = sessionStorage.getItem('userID');
    return userId !== undefined && userId !== '' && userId !== null;
  }

  ngOnInit(): void {
    this.urlService.getAllUrls().subscribe((data) => {
      this.urls = data;
    });
  }

  onShortUrlClick(shortUrl: string, event: MouseEvent) {
    const apiUrl = `http://localhost:5218/api/${shortUrl}`;
    if (event.button === 0) {
      window.open(apiUrl, '_blank');
    } else if (event.button === 2) {
      navigator.clipboard
        .writeText(apiUrl)
        .then(() => alert('Copied to clipboard: ' + apiUrl));
    }
  }

  shortenUrl(): void {
    if (!this.newUrl.trim()) {
      alert('URL cannot be empty.');
      return;
    }

    const userId = sessionStorage.getItem('userID');
    if (!userId) {
      alert('User not logged in.');
      return;
    }

    const currentUser = Number(userId);
    this.urlService.shortenUrl(this.newUrl, currentUser).subscribe({
      next: (result: any) => {
        this.newUrl = '';

        this.urlService.getAllUrls().subscribe((data) => {
          this.urls = data;
        });

        if (result?.message) {
          alert(result.message);
        }
      },

      error: (exception) => {
        if (exception.error?.message) {
          alert(exception.error.message);
        }
      },
    });
  }

  deleteUrl(urlId: number): void {
    const userId = sessionStorage.getItem('userID');
    if (!userId) {
      alert('User not logged in.');
      return;
    }

    const currentUser = Number(userId);
    this.urlService.deleteUrl(urlId, currentUser).subscribe({
      next: () => {
        this.urlService.getAllUrls().subscribe((data) => {
          this.urls = data;
        });
      },
    });
  }
}
