/* eslint-disable @typescript-eslint/ban-types */
import { useAuth } from "oidc-react";
import {
  createContext,
  FC,
  PropsWithChildren,
  useContext,
  useEffect,
  useState,
} from "react";
import { Movie } from "../models/movie";
import { MovieShowcase } from "../models/movieShowcase";
import { UserReservation } from "../models/userReservation";
import { AuthContext } from "./authContext";

export interface IMovieContext {
  movies?: Movie[];
  makeReservation: (movieShowcase: MovieShowcase) => Promise<string>;
  getReservations: () => Promise<UserReservation[] | undefined>;
}

export const MovieContext = createContext<IMovieContext>({
  makeReservation: (movieShowcase: MovieShowcase) => Promise.resolve(""),
  getReservations: async () => Promise.resolve(undefined),
});

export const MovieContextProvider: FC<PropsWithChildren<{}>> = ({
  children,
}) => {
  const { user } = useContext(AuthContext);
  const [movies, setMovies] = useState<Movie[]>([]);
  const { userData } = useAuth();
  useEffect(() => {
    (async () => {
      const response = await fetch(
        `${process.env.REACT_APP_API_BASE_URL}/api/1.0/movies`
      );
      if (response.ok) {
        const responseMovies = (await response.json()) as Movie[];
        setMovies(responseMovies);
      }
    })();
  }, []);

  const makeReservation = async (movieShowcase: MovieShowcase) => {
    const token = userData?.access_token;
    const response = await fetch(
      `${process.env.REACT_APP_API_BASE_URL}/api/1.0/reservations`,
      {
        method: "POST",
        body: JSON.stringify({ movieShowcaseId: movieShowcase.id }),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.ok) {
      const id = (await response.text()) as string;
      return id;
    }
    return undefined;
  };

  const getReservations = async () => {
    const token = userData?.access_token;
    const response = await fetch(
      `${process.env.REACT_APP_API_BASE_URL}/api/1.0/reservations`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.ok) {
      const reservationsResponse = (await response.json()) as UserReservation[];
      return reservationsResponse;
    }
    return undefined;
  };

  return (
    <MovieContext.Provider value={{ movies, makeReservation, getReservations }}>
      {children}
    </MovieContext.Provider>
  );
};
