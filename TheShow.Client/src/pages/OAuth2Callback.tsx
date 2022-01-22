import { useAuth } from "oidc-react";
import { FC, useEffect, useState } from "react";
import { Redirect } from "react-router-dom";
import { FastProgressRing } from "../App";
import { UserTemplate } from "../templates/UserTemplate";
import "./oauth2callback.css";

export const OAuth2Callback: FC = () => {
  const [redirect, setRedirect] = useState(false);

  if (redirect) {
    return <Redirect to="/" />;
  }
  setTimeout(() => {
    setRedirect(true);
  }, 3000);
  return (
    <UserTemplate full={false}>
      <div className="oauth2-callback">
        <h3>Logowanie ...</h3>
        <FastProgressRing></FastProgressRing>
      </div>
    </UserTemplate>
  );
};
