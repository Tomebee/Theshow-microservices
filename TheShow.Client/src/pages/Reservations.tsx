import { FC, useContext, useEffect, useState } from "react";
import { MovieContext } from "../contexts/movieContext";
import { UserReservation } from "../models/userReservation";
import { UserTemplate } from "../templates/UserTemplate";
import { Reservations as ReservationsComp } from '../components/molecules/Reservations';

export const Reservations: FC = () => {
    const { getReservations } = useContext(MovieContext);
    const [reservations, setReservations] = useState<UserReservation[] | undefined>();

    useEffect(() => {
        (async () => {
            setReservations(await getReservations());
        })();
    }, [])
    return <UserTemplate>
        {reservations && <ReservationsComp reservations={reservations}/>}
    </UserTemplate>
}