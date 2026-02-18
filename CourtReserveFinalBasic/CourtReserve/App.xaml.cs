using CourtReserve.ModelsLogic;
using CourtReserve.Views;
namespace CourtReserve
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
