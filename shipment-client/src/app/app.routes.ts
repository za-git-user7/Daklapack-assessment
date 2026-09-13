import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ShipmentComponent } from './pages/shipment/shipment.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'shipments', component: ShipmentComponent },
];
