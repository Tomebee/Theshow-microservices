import { FC } from 'react';
import { MovieCategory } from '../../models/movieCategory';
import { formatCategory } from '../../utils/category';

interface CategoryProps {
    category?: MovieCategory;
}

export const Category: FC<CategoryProps> = ({ category }) => {
    return <div>
        Kategoria: {formatCategory(category)}
    </div>
}