using CityDriveManager.Services;
using CityDriveManager.UI;

namespace CityDriveManager
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager dataManager = new DataManager();
            Menu menu = new Menu(dataManager);
            menu.Show();
        }
    }
}
