using System.Security;
using System.Xml.Linq;
using HotelsApp.Core.Security;

namespace HotelsApp.Core.DBTools
{
    public class ConnectionInfo
    {
        public string DataBase { get; }
        public string User { get; }
        public string Source { get; }
        public string Driver { get; }
        public string Server { get; set; }
        public string Provider { get; }
        public SecureString Password { get; }

        public string GetConnectionString()
        {
            switch (DataBase)
            {
                case "access":
                    return $"Driver={Driver};Dbq={Source};Uid={User};Pwd={Password.Unsecure()};";
                case "access2":
                    return $"Provider={Provider};Data source={Source};User Id={User};Password={Password.Unsecure()};Persist Security Info=False;";
                case "sql":
                    return $"Data Source={Server};Initial Catalog={Source};User ID={User};Password= {Password.Unsecure()};Integrated Security=True";
                case "mysql":
                    return $"Driver={Driver};Server={Server};Uid={User};Pwd={Password.Unsecure()};Database={Source};";
                default:
                    return string.Empty;
            }
        }

        public ConnectionInfo(string xmlConfig)
        {
            var xDoc = XDocument.Load(xmlConfig);
            var connection = xDoc.Element("root").Element("connection");
            if (connection == null)
                throw new XmlSyntaxException();
            DataBase = connection.Attribute("db")?.Value;
            User = connection.Attribute("user")?.Value;
            Source = connection.Attribute("source")?.Value;
            Driver = connection.Attribute("driver")?.Value;
            Provider = connection.Attribute("provider")?.Value;
            Server = connection.Attribute("server")?.Value;
            Password = new SecureString().Secure(connection.Attribute("pass")?.Value);
        }
    }
}