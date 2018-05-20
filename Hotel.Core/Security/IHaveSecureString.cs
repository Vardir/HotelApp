using System.Security;

namespace HotelsApp.Core.Security
{
    public interface IHaveSecureString
    {
        SecureString SecureString { get; }
    }
}