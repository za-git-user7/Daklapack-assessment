export interface Shipment {
    shipmentId: number;
    trackingId: string;
    status: string;
    weight: number;
    destination: string;
    origin: string;
    createdAt: string;
    arrivedAt: string;
}
