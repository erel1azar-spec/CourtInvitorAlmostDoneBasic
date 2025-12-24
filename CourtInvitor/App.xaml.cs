namespace CourtInvitor;

using CourtInvitor.ModelsLogic;
using System;

public partial class App : Application
{
    public User user = new();
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();

    }

}