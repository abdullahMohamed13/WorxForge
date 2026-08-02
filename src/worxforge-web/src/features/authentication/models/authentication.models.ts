import type { UserRole } from "../../../types/app.types";

export interface RegisterRequest {
  email: string;
  username: string;
  password: string;
  passwordConfirm: string;
}

export interface LoginRequest {
  emailOrUsername: string;
  password: string;
}

export interface VerifyEmailRequest {
  token: string;
}

export interface RequestPasswordResetRequest {
  email: string;
}

export interface ResetPasswordRequest {
  token: string;
  newPassword: string;
  newPasswordConfirm: string;
}

export interface AuthToken {
  accessToken: string;
  expiresAt: string;
}

export interface UserSession {
  userId: string;
  username: string;
  displayName: string;
  email: string;
  emailVerified: boolean;
  role: UserRole;
  avatarUrl?: string;
}
