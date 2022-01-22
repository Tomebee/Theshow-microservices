import { FC, PropsWithChildren, useEffect, useState } from "react";
import { FastDialog } from "../../../App";
import { Movie } from "../../../models/movie";
import { Category } from "../../atoms/Category";
import { TeaserImage } from "../../atoms/TeaserImage";
import { Title } from "../../atoms/Title";
import { MovieShowcases } from "../MovieShowcases";
import {
  Elements,
  CardNumberElement,
  CardExpiryElement,
  CardCvcElement,
  PaymentElement,
  CardElement,
  useStripe,
  useElements,
} from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import "./moviedetails.css";
import { useAuth } from "oidc-react";
import { CheckoutForm } from "../../organisms/CheckoutForm";

type MovieDetailsProps = {} & Movie;

const stripePromise = loadStripe(
  "pk_test_51KJePaLPO0ry25Lwu3OzYJ8ZRJFKtx1rUvzTLxpSr6MG12oAW4szvWZHQnsluW9aMCRauBK9I83X0POmCjWyrSPn00E1f31Ard",
  {
    locale: "pl",
  }
);

export const MovieDetails: FC<PropsWithChildren<MovieDetailsProps>> = ({
  name,
  description,
  movieCategory,
  imageUrl,
  showcases,
  children,
}) => {
  const { userData } = useAuth();
  const [paymentDialog, togglePaymentDialog] = useState(false);
  const [paymentRequestSecret, setPaymentRequestSecret] = useState<
    string | undefined
  >(undefined);
  const createPaymentIntent = async (reservationId: string) => {
    const token = userData?.access_token;
    const response = await fetch(
      `${process.env.REACT_APP_API_BASE_URL}/api/1.0/payments/${reservationId}`,
      {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.ok) {
      const secret = (await response.text()) as string;
      setPaymentRequestSecret(secret);
    }
  };

  const handleCancel = () => {
    setPaymentRequestSecret(undefined);
  };

  const onReservationMade = async (id: string) => {
    togglePaymentDialog(true);
    await createPaymentIntent(id);
  };

  return (
    <article className="movie">
      <div className="movie-wrapper">
        <aside>
          <Title>{name}</Title>
          <Category category={movieCategory} />
          <p>{description}</p>
          <MovieShowcases
            onReservationMade={onReservationMade}
            showcases={showcases}
          />
          {children}
        </aside>
        <aside>
          <TeaserImage imageUrl={imageUrl} />
        </aside>
      </div>
      {paymentRequestSecret && (
        <Elements
          stripe={stripePromise}
          options={{
            clientSecret: paymentRequestSecret,
            appearance: {
              theme: "night",
              variables: {
                fontFamily: "Montserrat, sans-serif",
              },
            },
          }}
        >
          <FastDialog>
            <section className="stripe-checkout">
              <CheckoutForm onCancelClick={handleCancel} />
            </section>
          </FastDialog>
        </Elements>
      )}
    </article>
  );
};
