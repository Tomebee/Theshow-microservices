/* eslint-disable no-unused-expressions */
import {
  provideFASTDesignSystem,
  fastCard,
  fastButton,
  fastProgressRing,
  fastSelect,
  fastOption,
  fastDialog,
} from "@microsoft/fast-components";
import "./App.css";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import { Elements } from "@stripe/react-stripe-js";
import { Home } from "./pages/Home";
import { MovieContextProvider } from "./contexts/movieContext";
import React, { useEffect, useState } from "react";
import { Movie } from "./pages/Movie";
import { Reservations } from "./pages/Reservations";
import { AddMovie } from "./pages/AddMovie";
import { provideReactWrapper } from "@microsoft/fast-react-wrapper";
import { AuthProvider, AuthProviderProps } from "oidc-react";
import { loadStripe } from "@stripe/stripe-js";
import { OAuth2Callback } from "./pages/OAuth2Callback";
import { PaymentComplete } from './pages/PaymentComplete';


const { wrap } = provideReactWrapper(React, provideFASTDesignSystem());

export const FastCard = wrap(fastCard());
export const FastButton = wrap(fastButton());
export const FastProgressRing = wrap(fastProgressRing());
export const FastSelect = wrap(fastSelect());
export const FastOption = wrap(fastOption());
export const FastDialog = wrap(fastDialog());

function App() {
  const [needRefresh, setNeedRefresh] = useState(false);

  const oidcConfig: AuthProviderProps = {
    authority: process.env.REACT_APP_AUTHORITY_URL,
    clientId: process.env.REACT_APP_AUTHORITY_CLIENT_ID,
    clientSecret: process.env.REACT_APP_AUTHORITY_CLIENT_SECRET,
    redirectUri: "http://theshow.local/oauth2-callback",
    autoSignIn: false,
    loadUserInfo: true,
    scope: "openid profile api-core.read api-core.write",
  };

  return (
    <AuthProvider {...oidcConfig}>
      <MovieContextProvider>
        
          <Router>
            <Switch>
              <Route exact path="/">
                <Home />
              </Route>
              <Route path="/movies/:movieId">
                <Movie />
              </Route>
              <Route path="/reservations" exact>
                <Reservations />
              </Route>
              <Route path="/add-movie" exact>
                <AddMovie />
              </Route>
              <Route path="/oauth2-callback">
                <OAuth2Callback />
              </Route>
              <Route path="/payment-complete">
                <PaymentComplete/>
              </Route>
            </Switch>
          </Router>
      </MovieContextProvider>
    </AuthProvider>
  );
}

export default App;
