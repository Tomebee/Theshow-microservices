import { FC } from 'react';
import { Link } from 'react-router-dom';
import { Movie } from '../../../models/movie';
import './moviecard.css';

type MovieCardProps = {} & Movie;

export const MovieCard: FC<MovieCardProps> = ({ id, imageUrl, name }) => {
  return (
    <Link to={`movies/${id}`}>
      <article className='movie-card'>
          <image className='movie-card__image' style={{ backgroundImage: `url(${imageUrl})` }}>
          </image>
          <h3 className='movie-card__title'>{name}</h3>
      </article>
    </Link>
  );
};
