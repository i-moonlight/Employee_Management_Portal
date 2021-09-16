export class AppSettings {
}

export class LocalStorageKeys {
  public static readonly User = 'currentUser';
  public static readonly Key = 'key';
}

export class Pattern {
  public static readonly USERNAME_PATTERN = /^[\S][\w\d]{6,16}$/;
  public static readonly EMAIL_PATTERN = /^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
  public static readonly PASSWORD_PATTERN = /^((?!.*[\s])(?=.*[!@#$%^&*])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{12,25})$/;
}
