using Microsoft.Extensions.DependencyInjection;
using mnaupariS6.Views;

namespace mnaupariS6
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new vEstudiante())
    );
        }
    }
}