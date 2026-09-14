import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ShipmentService } from '../../services/shipment.service';
import { Subscription } from 'rxjs';
import { PagedShipments } from '../../models/paged-shipments';
import { NavBarComponent } from '../../components/nav-bar/nav-bar.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Shipment } from '../../models/shipment';

@Component({
  selector: 'app-shipment',
  imports: [
    CommonModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatIconModule,
    NavBarComponent,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatProgressBarModule,
    MatPaginator
],
  templateUrl: './shipment.component.html',
  styleUrl: './shipment.component.css',
})
export class ShipmentComponent implements OnInit, OnDestroy {
  private readonly shipmentService = inject(ShipmentService);
  private subscription?: Subscription;
  dataSource = new MatTableDataSource<Shipment>([]);
  isLoading!: boolean;
  errorMessage: string | null = null;
  pageNumber: number = 1;
  pageSize: number = 10;
  totalItems: number = 0;
  totalPages: number = 0;
  filter: string = '';

  readonly tableColumns: string[] = [
    'trackingId',
    'status',
    'weight',
    'origin',
    'destination',
    'createdAt',
    'arrivedAt'
  ];

  ngOnInit(): void {
    this.getShipments();
  }

  getShipments(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.subscription?.unsubscribe();
    this.subscription = this.shipmentService.getShipments({
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      filter: this.filter
    })
        .subscribe({
          next: (data: PagedShipments) => {
            this.dataSource.data = data.items;
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
          }
        })
  }

  firstPage(): void {
    this.pageNumber = 1;
    this.getShipments();
  }

  lastPage(): void {
    this.pageNumber = this.totalPages;
    this.getShipments();
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.getShipments();
  }

  applyFilter(): void {
    this.filter = this.filter.trim();
    this.pageNumber = 1;
    this.getShipments();
  }

  clearFilter(): void {
    this.filter = '';
    this.pageNumber = 1;
    this.getShipments();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
