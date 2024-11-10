using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Учебаня_практика.Properties;

namespace Учебаня_практика.windows
{
    /// <summary>
    /// Логика взаимодействия для CreateSaleWindow.xaml
    /// </summary>
    public partial class CreateSaleWindow : Window
    {
        private readonly BDEntities m_entities;
        private readonly ObservableCollection<ProductsInSale> m_productsInSale;

        public CreateSaleWindow()
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
            m_productsInSale = new ObservableCollection<ProductsInSale>();

            ProductsInSale.ItemsSource = m_productsInSale;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Place.ItemsSource = m_entities.SalePlaces.ToList();
            Partner.ItemsSource = m_entities.Users.Where(user => user.PartnerID != null).ToList();
            Products.ItemsSource = m_entities.Products.ToList();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var product = Products.SelectedItem as Product;
            if (product == null)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }

            if (!int.TryParse(Count.Text, out int count) || count < 1)
            {
                MessageBox.Show("Некорректное кол-во");
                return;
            }

            var productInSale = new ProductsInSale()
            {
                Amount = count,
                ProductArticle = product.Article,
                Product = product,

            };
            m_productsInSale.Add(productInSale);
        }

        private void CreateSaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_productsInSale.Count < 1)
            {
                MessageBox.Show("Добавьте продукты");
                return;
            }

            var place = Place.SelectedItem as SalePlace;
            if (place == null)
            {
                MessageBox.Show("Выбирете место реализации");
                return;

            }

            var partner = Partner.SelectedItem as User;
            if (partner == null)
            {
                MessageBox.Show("Выбирете партёра");
                return;
            }



            var saleDto = new Sale()
            {
                Date = DateTime.Now,
                UserID = partner.ID,
                PlaceID = place.ID
            };

            try
            {
                var sale = m_entities.Sales.Add(saleDto);
                foreach (var item in m_productsInSale)
                {
                    item.SaleID = sale.ID;
                    m_entities.ProductsInSales.Add(item);
                }
                m_entities.SaveChanges();
                MessageBox.Show("Успешно");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
