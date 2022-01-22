import { FC } from 'react';
import './teaserimage.css';

interface TeaserImageProps {
  imageUrl: string;
}

export const TeaserImage: FC<TeaserImageProps> = ({ imageUrl }) => {
  return <section className='teaser-image' style={{ backgroundImage: `url(${imageUrl})` }}></section>;
};
