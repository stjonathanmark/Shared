namespace Shared.Payments
{
    public static class CredentialKeys
    {
        public static class Stripe
        {
            public const string SecretKey = "SecretKey";
            public const string PublishableKey = "PublishableKey";
        }

        public static class AuthorizeNet
        {
            public const string Username = "Username";
            public const string PrivateKey = "PrivateKey";
        }
    }
}
