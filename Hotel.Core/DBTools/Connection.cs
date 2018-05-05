using System.Security;
using System.Xml.Linq;
using Hotel.Core.Security;

namespace Hotel.Core.DBTools
{
    public class Connection
    {
        public string DataBase { get; }
        public string User { get; }
        public string Path { get; }
        public string Provider { get; }
        public SecureString Password { get; }

        public Connection(string xmlConfig)
        {
            var xDoc = XDocument.Load(xmlConfig);
            var connection = xDoc.Element("root").Element("connection");
            if (connection == null)
                throw new XmlSyntaxException();
            DataBase = connection.Attribute("db")?.Value;
            User = connection.Attribute("user")?.Value;
            Path = connection.Attribute("source")?.Value;
            Provider = connection.Attribute("provider")?.Value;
            Password = new SecureString().Secure(connection.Attribute("pass").Value);
        }
    }
}