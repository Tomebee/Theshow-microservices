import { FC, DetailedHTMLProps, HTMLProps, HTMLAttributes } from 'react';
import classNames from 'classnames';
import './input.css';

export const Input: FC<HTMLProps<HTMLInputElement>> = ({
  className,
  ...props
}) => {
  return <input className={classNames('input', className)} {...props} />;
};
