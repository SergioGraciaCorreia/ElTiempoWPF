﻿using ElTiempoWPF.Models;
using ElTiempoWPF.Services;
using ElTiempoWPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ElTiempoWPF.ViewModels;

namespace ElTiempoWPF
{

	// prueba de cambio
	public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			DataContext = new MainViewModel();
			


		}
		



	}
}