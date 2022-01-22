import { FC, useContext, useState } from "react";
import { useRouteMatch } from "react-router";
import { Redirect } from "react-router-dom";
import { MovieDetails } from "../components/molecules/MovieDetails";
import { MovieContext } from "../contexts/movieContext";
import { UserTemplate } from "../templates/UserTemplate";
import { AuthProvider, useAuth } from "oidc-react";

interface MovieRouteMatch {
  movieId?: string;
}

export const Movie: FC = () => {
  const {
    params: { movieId },
  } = useRouteMatch<MovieRouteMatch>();
  const { movies } = useContext(MovieContext);
  const redirect = () => <Redirect to="/" />;
  if (!movieId) {
    return redirect();
  }
  const { signIn } = useAuth();

  const movie = movies?.find((x) => x.id == movieId);
  // setTimeout(signIn, 3000);
  // const LoadingComponent = ()=> <div>My loader</div>
  // return <>

  //   secret ;)
  // </>
  if (movie) {
    return (
      <UserTemplate>
        <MovieDetails {...movie} />
      </UserTemplate>
    );
  } else {
    return redirect();
  }
};
