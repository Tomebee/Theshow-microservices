import React, { FC, useContext, useState } from "react";
import { MovieShowcase } from "../../../models/movieShowcase";
import { parseISO, format } from "date-fns";
import "./movieshowcases.css";
import { MovieContext } from "../../../contexts/movieContext";
import { FastButton, FastDialog, FastOption, FastSelect } from "../../../App";
import { useAuth } from "oidc-react";

interface MovieShowcasesProps {
  showcases?: MovieShowcase[];
  onReservationMade: (id: string) => Promise<any>;
}

export const MovieShowcases: FC<MovieShowcasesProps> = ({
  showcases,
  onReservationMade,
}) => {
  const [selectedShowcaseId, setSelectedShowcaseId] = useState<
    string | undefined
  >(showcases ? showcases[0]?.id : undefined);
  const { userData } = useAuth();
  const { makeReservation } = useContext(MovieContext);

  const onSelectChange = (id: string) => () => {
    setSelectedShowcaseId(id);
  };

  const onReserveClick = async () => {
    const showcase = showcases?.find((x) => x.id == selectedShowcaseId);
    if (showcase) {
      const id = await makeReservation(showcase);
      await onReservationMade(id.replaceAll('"',''));
    }
  };

  return (
    <section>
      {showcases && showcases.length && (
        <FastSelect value={selectedShowcaseId}>
          {showcases.map((x) => (
            <FastOption
              selected={selectedShowcaseId === x.id}
              key={x.id}
              value={x.id}
              onClick={onSelectChange(x.id)}
            >
              {`${format(parseISO(x.date), "dd.MM.yyyy hh:mm")}`}
            </FastOption>
          ))}
        </FastSelect>
      )}
      {userData && (
        <FastButton
          appearance="accent"
          onClick={onReserveClick}
          {...(selectedShowcaseId ? {} : { disabled: true })}
          style={{ marginLeft: "1rem" }}
        >
          Rezerwuj
        </FastButton>
      )}
    </section>
  );
};
