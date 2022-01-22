import { FC, PropsWithChildren } from 'react';
import './title.css';
import classNames from 'classnames';

interface TitleProps {
  center?: boolean;
}

export const Title: FC<PropsWithChildren<TitleProps>> = ({ children, center }) => {
  return (
    <h1
      className={classNames('title', {
        'title--center': !!center,
      })}
    >
      {children}
    </h1>
  );
};
