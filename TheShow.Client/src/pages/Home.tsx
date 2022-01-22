import { FC, useContext } from 'react';
import { MovieContext } from '../contexts/movieContext';
import { MoviesGrid } from '../templates/MoviesGrid';
import { UserTemplate } from '../templates/UserTemplate';
import { useAuth } from 'oidc-react';

export const Home: FC = () => {
  const { movies } = useContext(MovieContext);
  const { userData, userManager } = useAuth();
  userManager.getUser().then(console.log);
  return (
    <UserTemplate>
      <MoviesGrid movies={movies}>
        <h1>Najnowsze filmy</h1>
      </MoviesGrid>
    </UserTemplate>
  );
};
