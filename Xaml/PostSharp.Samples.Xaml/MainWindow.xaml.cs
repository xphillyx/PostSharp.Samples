using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PostSharp.Samples.Xaml
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CustomerModel customer = new CustomerModel
        {
            FirstName = "Jan",
            LastName = "Novak",
            Addresses = new ObservableCollection<AddressModel>
            {
                new AddressModel
                {
                    Line1 = "Saldova 1G",
                    Town = "Prague"
                },
                new AddressModel
                {
                    Line1 = "Tyrsova 25",
                    Town = "Brno"
                },
                new AddressModel
                {
                    Line1 = "Pivorarka 154",
                    Town = "Pilsen"
                }
            }
        };

        public MainWindow()
        {
           

            InitializeComponent();

               // Create initial data.
            var customerViewModel = new CustomerViewModel { Customer = customer };

            customerViewModel.Customer.PrincipalAddress = customerViewModel.Customer.Addresses[0];

          
            DataContext = customerViewModel;
        }

         
        private void ExecuteSave()
        {
            var openFileDialog = new Microsoft.Win32.SaveFileDialog();

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                Save(openFileDialog.FileName);
            }
        }


          private void Save(string path)
        {
            customer.Save(path);
        }
    }
}