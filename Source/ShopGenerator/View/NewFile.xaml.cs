using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StarWarsShopGenerator.View
{
	/// <summary>
	/// Logique d'interaction pour Window1.xaml
	/// </summary>
	public partial class NewFileWindow : Window
	{
		public bool Success { get; set; }
		public string FileName { get { return TxtInput.Text; } set { TxtInput.Text = value; } }

		public NewFileWindow()
		{
			InitializeComponent();
		}

		private void CancelText()
		{
			Success = false;
			TxtInput.Text = "";
			Close();
		}

		private void Validate()
		{
			Success = true;
			Close();
		}

		private void Button_Cancel(object sender, RoutedEventArgs e)
		{
			CancelText();
		}

		private void Button_OK(object sender, RoutedEventArgs e)
		{
			Validate();
		}

		private void TxtInput_TouchEnter(object sender, TouchEventArgs e)
		{
			Validate();
		}

		private void TxtInput_TouchLeave(object sender, TouchEventArgs e)
		{
			CancelText();
		}
	}
}
