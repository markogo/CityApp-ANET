export interface User {
  username: string;
  role: Role;
}

export enum Role {
  DEFAULT = 'ROLE_USER',
  EDITOR = 'ROLE_ALLOW_EDIT',
  ADMIN = 'ROLE_ADMIN',
}
