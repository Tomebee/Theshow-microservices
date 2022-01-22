import { useAuth } from "oidc-react";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Redirect } from "react-router-dom";
import { FastProgressRing } from "../App";
import { UserTemplate } from "../templates/UserTemplate";
import queryString from "query-string";
import "./oauth2callback.css";

interface PaymentCompleteRouteMatch {
  payment_intent?: string;
  payment_intent_client_secret?: string;
  redirect_status?: string;
}

export const PaymentComplete = () => {
  const params: PaymentCompleteRouteMatch = queryString.parse(
    window.location.search
  );
  const { userData } = useAuth();
  const [signalShot, setSignalShot] = useState(false);
  useEffect(() => {
    if (params) {
      shot();
    }
  }, []);

  const shot = async () => {
    const token = userData?.access_token;
    const response = await fetch(
      `${process.env.REACT_APP_API_BASE_URL}/api/1.0/payments?intentId=${params.payment_intent}`,
      {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.status > 200 && response.status < 300) {
      setTimeout(() => {
        setSignalShot(true);
      }, 1500);
    }
  };

  if (signalShot) {
    return <Redirect to="/" />;
  }

  return (
    <UserTemplate>
      <div className="oauth2-callback">
        <h3>Sukces! Daj nam jeszcze chwilkę ...</h3>
        <FastProgressRing></FastProgressRing>
      </div>
    </UserTemplate>
  );
};
