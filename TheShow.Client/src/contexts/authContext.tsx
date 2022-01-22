import React, { createContext, FC, PropsWithChildren, useState } from 'react';
import { User } from '../models/user';
import jwt_decode from 'jwt-decode';

export interface IAuthContext {
  user?: User;
  isLoggedIn: () => boolean;
  login: (username: string, password: string) => Promise<User | undefined>;
}

export const AuthContext = createContext<IAuthContext>({
  login: (_: string, __: string) => Promise.resolve<User | undefined>(undefined),
  isLoggedIn: () => false,
});

const STORAGE_KEY = 'THESHOW_USER_DATA';

export const AuthContextProvider: FC<PropsWithChildren<{}>> = ({ children }) => {
  const [user, setUser] = useState<User | undefined>();

  React.useEffect(() => {
    const userFromStorage = localStorage.getItem(STORAGE_KEY);
    const parsedUser = userFromStorage ? JSON.parse(userFromStorage) as User : undefined;
    if(parsedUser) {
      setUser(parsedUser);
    }
  }, [])

  const login = async (username: string, password: string) => {
    const response = await fetch(`${process.env.REACT_APP_API_BASE_URL}/api/1.0/auth/login`, {
      body: JSON.stringify({ username, password }),
      headers: {
        Accept: 'application/json',
        'Content-type': 'application/json',
      },
      method: 'POST',
    });

    if (response.ok) {
      const authResponse = (await response.json()) as {
        token: string;
      };
      const user = jwt_decode<any>(authResponse.token);
      const userMapped: User = {
        email: user.email,
        firstName: user.given_name,
        roles: (user.role as string).split(','),
        name: user.unique_name,
        token: authResponse.token
      };
      setUser(userMapped);
      localStorage.setItem(STORAGE_KEY,JSON.stringify(userMapped));
      return user;
    }
    return undefined;
  };
  const isLoggedIn = () => !!user;
  return <AuthContext.Provider value={{ login, user, isLoggedIn }}>{children}</AuthContext.Provider>;
};
