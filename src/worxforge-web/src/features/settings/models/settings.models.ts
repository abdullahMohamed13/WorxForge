export interface UpdateEmailRequest {
  newEmail: string;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
  newPasswordConfirm: string;
}

export interface DeleteAccountRequest {
  confirmed: boolean;
}
