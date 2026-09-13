import { Shipment } from "./shipment";

export interface PagedShipments {
    items: Shipment[];
    pageNumber: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
}
