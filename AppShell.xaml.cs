using GetHome.Views;

namespace GetHome;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Pages de détail (poussées sur la pile de navigation)
        Routing.RegisterRoute("filtres", typeof(FiltresPage));
        Routing.RegisterRoute("resultats", typeof(ResultatsPage));
        Routing.RegisterRoute("detail", typeof(DetailAnnoncePage));
        Routing.RegisterRoute("publier", typeof(PublierPage));
    }
}
