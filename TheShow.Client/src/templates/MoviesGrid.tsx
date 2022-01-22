import { FC, PropsWithChildren } from 'react';
import { MovieCard } from '../components/molecules/MovieCard';
import { Movie } from '../models/movie';
import './moviesgrid.css';

interface MoviesGridProps {
  movies?: Movie[];
}

export const MoviesGrid: FC<PropsWithChildren<MoviesGridProps>> = ({ children, movies }) => {
  return (
    <section className='movies-grid-wrapper'>
      {children}
      <div className='movies-grid'>{movies && movies.length && movies.map((m) => <MovieCard key={m.id} {...m} />)}</div>
    </section>
  );
};
