import { MovieShowcase } from "./movieShowcase";

export type UserReservation = {
    userId: string;
    paymentStatus?: string;
    movieShowcase: MovieShowcase;
}