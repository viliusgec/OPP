using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Client;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ClientTests
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public async Task SingletonTest()
        {
            HubConnection connection;
            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();
            await connection.StartAsync();
            Assert.IsNotNull(connection);
        }
    }
}
