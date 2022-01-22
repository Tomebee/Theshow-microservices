import { useAuth } from "oidc-react";
import {
  FC,
} from "react";
import { Link } from "react-router-dom";
import { FastButton } from "../../../App";
import { Logo } from "../../atoms/Logo";
import "./header.css";

export const Header: FC = () => {
  const { userData, signIn, signOut } = useAuth();
  const onButtonClick = () => {
    signIn();
  };

  return (
    <header>
      <div className="header-inner">
        {!userData ? (
          <FastButton onClick={onButtonClick} appearance="accent">
            Zaloguj się
          </FastButton>
        ) : (
          <>
            <fast-button onClick={signOut} style={{ marginLeft: '1rem' }}>Wyloguj</fast-button>
            <Link to="/reservations">
              <fast-button appearance="accent">Moje rezerwacje</fast-button>
            </Link>
          </>
        )}
      </div>
      <Logo />
    </header>
  );
};
