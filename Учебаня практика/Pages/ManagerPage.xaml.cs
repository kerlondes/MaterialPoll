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
using Учебаня_практика.windows;

namespace Учебаня_практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private BDEntities m_entities;

        public ManagerPage()
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Orders.ItemsSource = m_entities.Orders.ToList();
            Partners.ItemsSource = m_entities.Partners.ToList();
            SalePlaces.ItemsSource = m_entities.SalePlaces.ToList();
            SalePlaceType.ItemsSource = m_entities.SalePlaceTypes.ToList();
            Stores.ItemsSource = m_entities.Stores.ToList();
            Sales.ItemsSource = m_entities.Sales.ToList();
            Products.ItemsSource = m_entities.Products.ToList();
        }


        private void AddSalePlaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SalePlaceName.Text))
            {
                MessageBox.Show("Заполните название");
                return;
            }

            if (SalePlaceType.SelectedItem == null)
            {
                MessageBox.Show("Укажите тип");
                return;
            }

            var place = new SalePlace() { Name = SalePlaceName.Text, TypeID = (SalePlaceType.SelectedItem as SalePlaceType).ID };
            m_entities.SalePlaces.Add(place);
            m_entities.SaveChanges();
            MessageBox.Show("Успешно");

            SalePlaces.ItemsSource = m_entities.SalePlaces.ToList();
        }

        private void AddPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrEditPartnerWindow();
            window.Closed += UpdatePartners;
            window.Show();
        }

        private void UpdatePartners(object sender, EventArgs e)
        {
            Partners.ItemsSource = m_entities.Partners.ToList();
        }

        private void UpdateOrders(object sender, EventArgs e)
        {
            Orders.ItemsSource = m_entities.Orders.ToList();
        }

        private void EditPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrEditPartnerWindow(Partners.SelectedItem as Partner);
            window.Closed += UpdatePartners;
            window.Show();
        }

        private void Partners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditPartnerButton.IsEnabled = Partners.SelectedItem != null;
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditOrderWindow(Orders.SelectedItem as Order);
            window.Closed += UpdateOrders;
            window.Show();
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditOrderButton.IsEnabled = Orders.SelectedItem != null;
        }

        private void EditStoreButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditStoreWindow(Stores.SelectedItem as Store);
            window.Show();
        }

        private void Stores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditStoreButton.IsEnabled = Stores.SelectedItem != null;
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditProductButton.IsEnabled = Products.SelectedItem != null;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrEditProductWindow();
            window.Closed += UpdateProducts;
            window.Show();
        }

        private void UpdateProducts(object sender, EventArgs e)
        {
            Products.ItemsSource = m_entities.Products.ToList();
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrEditProductWindow(Products.SelectedItem as Product);
            window.Closed += UpdateProducts;
            window.Show();
        }

        private void AddSaleButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateSaleWindow();
            window.Closed += UpdateSales;
            window.Show();
        }

        private void UpdateSales(object sender, EventArgs e)
        {
            Sales.ItemsSource = m_entities.Sales.ToList();
        }
    }
}
