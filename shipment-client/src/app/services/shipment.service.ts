import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedShipments } from '../models/paged-shipments';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShipmentService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl: string = `${environment.apiUrl}/shipment`;

  getShipments(
    pageNumber: number,
    pageSize: number,
  ): Observable<PagedShipments> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);
    return this.http.get<PagedShipments>(this.apiUrl, { params });
  }
}
