using System.Security.Cryptography;

namespace NetCoreApi.Security.Token
{
    public static class RSAHelper
    {
        public static RSAParameters GenerateKey()
        {
            using var key = new RSACryptoServiceProvider(2048);
            return key.ExportParameters(true);
        }
    }
}
