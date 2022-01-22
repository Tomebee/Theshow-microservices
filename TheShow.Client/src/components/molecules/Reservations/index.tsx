import { FC, useContext } from "react";
import { UserReservation } from "../../../models/userReservation";
import { parseISO, format } from "date-fns";
import { MovieContext } from "../../../contexts/movieContext";
import { MovieShowcase } from "../../../models/movieShowcase";
import "./reservations.css";

interface ReservationsProps {
  reservations: UserReservation[];
}

export const Reservations: FC<ReservationsProps> = ({ reservations }) => {
  const { movies } = useContext(MovieContext);

  const getMovie = (showcase: MovieShowcase) => {
    return movies?.find((x) => x.showcases?.find((y) => y.id == showcase.id));
  };

  return (
    <section className="reservations">
      <div className="reservations-row">
        <div className="reservations-column">Data</div>
        <div className="reservations-column">Godzina</div>
        <div className="reservations-column">Film</div>
        <div className="reservations-column"></div>
      </div>
      {reservations.map((x) => (
        <div className="reservations-row" key={x.movieShowcase.id}>
          <div className="reservations-column">{`${format(
            parseISO(x.movieShowcase.date),
            "dd.MM.yyyy"
          )}`}</div>
          <div className="reservations-column">{`${format(
            parseISO(x.movieShowcase.date),
            "hh:mm"
          )}`}</div>
          <div className="reservations-column">
            {getMovie(x.movieShowcase)?.name}
          </div>
          <div className="reservations-column">
            {x.paymentStatus == "succeeded" ? (
              <>Zapłacone</>
            ) : (
              <fast-button appearance="accent">Zapłać - 80 PLN</fast-button>
            )}
          </div>
        </div>
      ))}
    </section>
  );
};
