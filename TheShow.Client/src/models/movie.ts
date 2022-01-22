import { MovieCategory } from "./movieCategory";
import { MovieShowcase } from "./movieShowcase";

export type Movie = {
    id: string;
    name: string;
    shortDescription: string;
    description: string;
    imageUrl: string;
    movieCategory: MovieCategory;
    showcases?: MovieShowcase[];
}