using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NoteApp.Models;
using NoteApp.Utilities;
using WpfApp3.ViewModel;
using WpfApp3.ViewModels;



namespace WpfApp3
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

    }
}
