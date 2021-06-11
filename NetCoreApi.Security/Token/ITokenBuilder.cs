using System;
namespace NetCoreApi.Security.Token
{
    public interface ITokenBuilder
    {
        string Build(string name, string[] roles, DateTime expireDate);
    }
}
