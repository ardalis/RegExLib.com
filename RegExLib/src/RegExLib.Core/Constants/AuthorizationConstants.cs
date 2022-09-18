﻿namespace RegExLib.Core.Constants
{
  public class AuthorizationConstants
  {
    public static class Roles
    {
      public const string ADMINISTRATORS = "Administrators";
    }

    public const string DEFAULT_PASSWORD = "Pass@word1";

    // TODO: Change this to an environment variable
    public const string JWT_SECRET_KEY = "SecretKeyOfDoomThatMustBeAMinimumNumberOfBytes";
  }
}
