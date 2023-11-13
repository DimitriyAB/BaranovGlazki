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
        int CounteRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Agent> CurrentPagelist = new List<Agent>();
        List<Agent> TableList;
        public ProductPage()
        {
            InitializeComponent();

            var currentServices = Baranov_glazkiEntities.GetContext().Agent.ToList();

            ServiceListView.ItemsSource = currentServices;

            ComboTypeSort.SelectedIndex = 0;
            ComboTypeAgTy.SelectedIndex = 0;

            UpdateProduct();
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
            || p.Phone.Replace("+", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Contains(TBoxSearch.Text)
            || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

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

            TableList = currentProduct;

            ChangePage(0, 0);
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
            UpdateProduct();
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }
        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPagelist.Clear();
            CounteRecords = TableList.Count;

            if (CounteRecords % 10 > 0)
            {
                CountPage = CounteRecords / 10 + 1;
            }
            else
            {
                CountPage = CounteRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CounteRecords ? CurrentPage * 10 + 10 : CounteRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPagelist.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CounteRecords ? CurrentPage * 10 + 10 : CounteRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPagelist.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CounteRecords ? CurrentPage * 10 + 10 : CounteRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPagelist.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }

            if (Ifupdate)
            {
                PageListBox.Items.Clear();

                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CounteRecords ? CurrentPage * 10 + 10 : CounteRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CounteRecords.ToString();


                ServiceListView.ItemsSource = CurrentPagelist;
                ServiceListView.Items.Refresh();
            }

        }


        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }
    }
}
