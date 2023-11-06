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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaranovGlazki
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();

            var currentServices = Baranov_glazkiEntities.GetContext().Agent.ToList();

            ServiceListView.ItemsSource = currentServices;

            ComboTypeSort.SelectedIndex = 0;
            ComboTypeAgTy.SelectedIndex = 0;

            UpdateProduct();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }              
        private void ComboTypeAgTy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboTypeSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct() ;
        }

        private void UpdateProduct()
        {
            var currentProduct = Baranov_glazkiEntities.GetContext().Agent.ToList();


            if (ComboTypeAgTy.SelectedIndex == 0)
            {
                currentProduct = currentProduct.ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (p.AgentTypeID == 3)).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (p.AgentTypeID == 5)).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (p.AgentTypeID == 1)).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 4)
            {
                currentProduct = currentProduct.Where(p => (p.AgentTypeID == 2)).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 5)
            {
                currentProduct = currentProduct.Where(p => (p.AgentTypeID == 4)).ToList();
            }
            if (ComboTypeAgTy.SelectedIndex == 6)
            {
                currentProduct = currentProduct.Where(p => (p.AgentTypeID == 6)).ToList();
            }

            currentProduct = currentProduct.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())
            ||p.Phone.Replace("+","").Replace(" ","").Replace("(","").Replace(")","").Replace("-","").Contains(TBoxSearch.Text)
            ||p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (ComboTypeSort.SelectedIndex == 0)
            {
                currentProduct = currentProduct.ToList();
            }
            if (ComboTypeSort.SelectedIndex == 1)
            { 
                currentProduct = currentProduct.OrderBy(p => p.Title).ToList();
            }
            if (ComboTypeSort.SelectedIndex == 2)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboTypeSort.SelectedIndex == 3)
            {
                
            }
            if (ComboTypeSort.SelectedIndex == 4)
            {

            }
            if (ComboTypeSort.SelectedIndex == 5)
            {
                currentProduct = currentProduct.OrderBy(p => p.Priority).ToList();
            }
            if (ComboTypeSort.SelectedIndex == 6)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.Priority).ToList();
            }

            ServiceListView.ItemsSource = currentProduct;
        }
    }
}
