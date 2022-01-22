import React, { ChangeEvent, FC, useContext, useState } from 'react';
import { Input } from '../components/atoms/Input';
import { Redirect } from 'react-router-dom';
import { AuthContext } from '../contexts/authContext';
import { UserTemplate } from '../templates/UserTemplate';
import './addmovie.css';

export const AddMovie: FC = () => {
  const [name, setName] = useState<string | undefined>();
  const [shortDescription, setShortDescription] = useState<string | undefined>();
  const [description, setDescription] = useState<string | undefined>();
  const [imageUrl, setImageUrl] = useState<string | undefined>();
  const [category, setCategory] = useState<string | undefined>();
  const [error, setError] = useState<string>('');
  const { user } = useContext(AuthContext);
  const redirect = () => <Redirect to='/' />;
  if (!user || !user.roles?.find((x) => x == 'Administrator')) {
    return redirect();
  }

  const onChange = (setAction: any) => (e: ChangeEvent<HTMLInputElement>) => {
      const { value } = e.target;
      setAction(value);
  }

  const addMovie = async () => {
    const response = await fetch('https://localhost:44386/api/1.0/movies', {
      method: 'POST',
      body: JSON.stringify({ 
          name,
          shortDescription,
          description,
          imageUrl,
          movieCategory: 0
       }),
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${user?.token}`,
      },
    });

    if(response.status == 400) {
        setError('Film o tym tytule jest już dodany')
    }
  };

  return (
    <UserTemplate>
      <div className='add-movie-wrapper'>
        <div>Nazwa filmu</div>
        <div>
          <Input className='add-input' onChange={onChange(setName)}/>
        </div>
        <div>Krótki opis</div>
        <div>
          <Input className='add-input' onChange={onChange(setShortDescription)}/>
        </div>
        <div>Opis</div>
        <div>
          <Input className='add-input' onChange={onChange(setDescription)}/>
        </div>
        <div>Adres url do miniaturki</div>
        <div>
          <Input className='add-input' onChange={onChange(setImageUrl)}/>
        </div>
        <div>Kategoria</div>
        <div>
          <Input className='add-input' onChange={onChange(setCategory)}/>
        </div>
        {error && <div>{error}</div>}
        <fast-button onClick={addMovie}>
            Dodaj
        </fast-button>
      </div>
    </UserTemplate>
  );
};
