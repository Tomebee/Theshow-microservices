import { MovieCategory } from '../models/movieCategory';

const translations = {
  [0]: 'Akcja',
  [1]: 'Thriller',
  [2]: 'Dramat',
};

export const formatCategory: (category?: MovieCategory) => string | undefined = (category?: MovieCategory) => {
  if (category === undefined) {
    return undefined;
  }
  const translation = translations[category];
  return translation ?? undefined;
};
