export interface User {
  username: string;
  role: Role;
};

export enum Role {
  DEFAULT = "ROLE_DEFAULT",
  EDITOR = "ROLE_ALLOW_EDIT",
  ADMIN = "ROLE_ADMIN",
};