import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { FulfillmentRequest } from '../model/fullfillment-request';

@Component({
  selector: 'app-pending-requests-component',
  templateUrl: './pending-requests.component.html',
  styleUrls: ['./pending-requests.component.css']
})
export class PendingRequestsComponent {
  public requests: FulfillmentRequest[];
  public error: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<FulfillmentRequest[]>(baseUrl + 'pending-requests').subscribe(result => {
      this.requests = result;
    }, error => this.error = error);
  }
}

