using EReaderNow.Data.Service;

namespace EReaderNow
{
    public class AddStartup
    {
        public static IConfigurationRoot AppConfiguration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("addDB.json").Build();
    }
}