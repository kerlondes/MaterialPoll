using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace Учебаня_практика.windows
{
    /// <summary>
    /// Логика взаимодействия для CreateOrEditProductWindow.xaml
    /// </summary>
    public partial class CreateOrEditProductWindow : Window
    {
        private readonly BDEntities m_entites;
        private readonly Product m_currentProduct;


        public CreateOrEditProductWindow(Product product = null)
        {
            InitializeComponent();
            m_entites = BDEntities.GetInstance();

            this.Title = product == null ? "Добавить продукт" : "Редактировать продукт";

            SaveButton.Visibility = product == null ? Visibility.Hidden : Visibility.Visible;
            AddButton.Visibility = product != null ? Visibility.Hidden : Visibility.Visible;

            m_currentProduct = product;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;

            var type = ProductTypes.SelectedItem as ProductType;

            var product = new Product()
            {
                Article = m_currentProduct.Article,
                TypeID = type.ID,
                Name = Name.Text,
                MinCostForPartner = double.Parse(MinCost.Text),
                Cost = double.Parse(Cost.Text),
                Logo = m_currentProduct.Logo,
                QualityCertificate = m_currentProduct.QualityCertificate,
                StandardNumber = m_currentProduct.StandardNumber,
                WeightWithoutPack = m_currentProduct.WeightWithoutPack,
                WeightWithPack = m_currentProduct.WeightWithPack,
                Description = m_currentProduct.Description,
                PackageSize = m_currentProduct.PackageSize,

            };

            try
            {
                m_entites.Products.AddOrUpdate(product);
                m_entites.SaveChanges();
                MessageBox.Show("Успешно");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;

            var type = ProductTypes.SelectedItem as ProductType;
            var lastProduct = m_entites.Products.ToList().LastOrDefault();
            var article = 0;
            var random = new Random();

            if (lastProduct != null)
            {
                article = lastProduct.Article + 1;
            }


            var product = new Product()
            {
                Article = article,
                TypeID = type.ID,
                Name = Name.Text,
                MinCostForPartner = double.Parse(MinCost.Text),
                Cost = double.Parse(Cost.Text),
                Logo = "",
                QualityCertificate = "",
                StandardNumber = random.Next(100000, 999999),
                WeightWithoutPack = random.Next(1, 100),
                WeightWithPack = random.Next(1, 100),
                Description = "",
                PackageSize = "",

            };

            try
            {
                m_entites.Products.AddOrUpdate(product);
                m_entites.SaveChanges();
                MessageBox.Show("Успешно");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Validate()
        {

            if (string.IsNullOrEmpty(Name.Text))
            {
                MessageBox.Show("Укажите имя");
                return false;
            }

            if (ProductTypes.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип");
                return false;
            }

            if (!double.TryParse(MinCost.Text, out var minCost) || minCost < 1)
            {
                MessageBox.Show("Некорректная мин. цена");
                return false;
            }

            if (!double.TryParse(Cost.Text, out var cost) || cost < 1)
            {
                MessageBox.Show("Некорректная цена");
                return false;
            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var types = m_entites.ProductTypes.ToList();
            ProductTypes.ItemsSource = types;

            if (m_currentProduct == null) return;
            Name.Text = m_currentProduct.Name;
            ProductTypes.SelectedIndex = types.FindIndex(a => a.ID == m_currentProduct.TypeID);
            MinCost.Text = m_currentProduct.MinCostForPartner.ToString();
            Cost.Text = m_currentProduct.Cost.ToString();
        }
    }
}
