import { Authentication } from '../types/authentication';

export const getIsLoggedIn = () => {
  const authObject = JSON.parse(
    localStorage.getItem('cityAppAuth')!
  ) as Authentication;

  if (authObject && authObject.jwt && authObject.username && authObject.role) {
    return true;
  }
  return false;
};
