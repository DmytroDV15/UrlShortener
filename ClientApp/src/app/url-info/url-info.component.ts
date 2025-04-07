import { Component, OnInit } from '@angular/core';
import { UrlService } from '../Services/url.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { urlInfoModel as UrlInfo } from '../Interfaces/urlInfoModel.interface';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-url-info',
  imports: [CommonModule, MatCardModule, MatButtonModule],
  templateUrl: './url-info.component.html',
  styleUrl: './url-info.component.scss',
})
export class UrlInfoComponent implements OnInit {
  urlInfo: UrlInfo | undefined;
  window: any;
  constructor(
    private urlService: UrlService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.urlService.infoUrl(Number(id)).subscribe((data) => {
      this.urlInfo = data;
    });
  }
}
