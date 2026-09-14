import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedShipments } from '../models/paged-shipments';
import { environment } from '../environments/environment';
import { ShipmentQuery } from '../models/shipment-query';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl: string = `${environment.apiUrl}/shipment`;

  getShipments(query: ShipmentQuery): Observable<PagedShipments> {
    let params = new HttpParams()
                        .set('pageNumber', String(query.pageNumber))
                        .set('pageSize', String(query.pageSize));

    if (query.filter) {
      params = params.set('filter', query.filter);
    }

    return this.http.get<PagedShipments>(this.apiUrl, { params });
  }
}
