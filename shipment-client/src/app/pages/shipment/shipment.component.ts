import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { Subscription } from 'rxjs';
import { PagedShipments } from '../../models/paged-shipments';
import { NavBarComponent } from '../../components/nav-bar/nav-bar.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { Shipment } from '../../models/shipment';

@Component({
  selector: 'app-shipment',
  imports: [
    CommonModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatIconModule,
    NavBarComponent,
  ],
  templateUrl: './shipment.component.html',
  styleUrl: './shipment.component.css',
})
export class ShipmentComponent implements OnInit, OnDestroy {
  private readonly shipmentService = inject(ShipmentService);
  private subscription?: Subscription;
  shipments: Shipment[] = [];
  isLoading!: boolean;
  errorMessage: string | null = null;
  pageNumber: number = 1;
  pageSize: number = 10;
  totalItems: number = 0;
  totalPages: number = 0;

  readonly tableColumns: string[] = [
    'trackingId',
    'status',
    'weight',
    'origin',
    'destination',
    'createdAt',
    'arrivedAt',
  ];

  ngOnInit(): void {
    this.getShipments();
  }

  getShipments(): void {
    this.isLoading = true;
    this.subscription = this.shipmentService
      .getShipments(this.pageNumber, this.pageSize)
      .subscribe({
        next: (data: PagedShipments) => {
          this.shipments = data.items;
          this.pageNumber = data.pageNumber;
          this.pageSize = data.pageSize;
          this.totalItems = data.totalItems;
          this.totalPages = data.totalPages;
          this.isLoading = false;
        },
        error: (err) => {
          this.isLoading = false;
          debugger;
          this.errorMessage = 'An error occurred retrieving shipments';
          console.error(this.errorMessage, err);
        },
      });
  }

  initialPage(): void {
    this.pageNumber = 1;
    this.getShipments();
  }

  nextPage(): void {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.getShipments();
    }
  }

  previousPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.getShipments();
    }
  }

  lastPage(): void {
    this.pageNumber = this.totalPages;
    this.getShipments();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
