using System;
using System.Collections.Generic;
using TextActor.ViewModels;
using TextActor.Views;
using Xamarin.Forms;

namespace TextActor
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
