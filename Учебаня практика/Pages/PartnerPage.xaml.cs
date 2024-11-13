using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Учебаня_практика.Properties;

namespace Учебаня_практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для PartnerPage.xaml
    /// </summary>
    public partial class PartnerPage : Page
    {
        private readonly BDEntities m_entities;
        private readonly ObservableCollection<ProductsInOrder> m_productsInOrder;
        private readonly ObservableCollection<ProductsInOrder> m_allProductsInOrder; // все доступные продукты
        private ICollectionView _productsView; // Представление данных для фильтрации

        private string _searchText = "";  // Храним текст для поиска
        private string _selectedType = "";  // Храним выбранный тип продукта

        public PartnerPage()
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
            m_productsInOrder = new ObservableCollection<ProductsInOrder>();
            m_allProductsInOrder = new ObservableCollection<ProductsInOrder>(); // Инициализация для всех продуктов
            ProductsInOrder.ItemsSource = m_productsInOrder;
            ProductsInOrder2.ItemsSource = m_allProductsInOrder;
            _productsView = CollectionViewSource.GetDefaultView(m_allProductsInOrder);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            m_allProductsInOrder.Clear();
            foreach (var product in m_entities.Products)
            {
                var productInOrder = new ProductsInOrder()
                {
                    Product = product,
                    ProductArticle = product.Article,
                    Cost = (int)(product.MinCostForPartner) // Задайте начальную стоимость
                };

                // Добавляем продукт в коллекцию всех продуктов
                m_allProductsInOrder.Add(productInOrder);
            }

            // Устанавливаем типы продуктов в комбобокс
            TypeProduct.ItemsSource = m_entities.Products
                .Select(p => p.ProductType.Name) // Извлекаем типы продуктов
                .Distinct() // Оставляем только уникальные
                .ToList();
            // Устанавливаем все продукты в Products
            Products.ItemsSource = m_entities.Products.ToList();
            UpdateOrderList();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }

            if (!int.TryParse(Count.Text, out var count) || count < 1)
            {
                MessageBox.Show("Неверное количество");
                return;
            }

            var product = Products.SelectedItem as Product;
            int unitCost = (int)(product.MinCostForPartner);
            int totalCost = count * unitCost;

            // Изначальная скидка и цена со скидкой
            int discountPercentage = 0;
            int costAfterDiscount = totalCost;

            // Создаем продукт с изначальной скидкой (0%) и начальной ценой со скидкой
            var productInOrder = new ProductsInOrder()
            {
                Amount = count,
                ProductArticle = product.Article,
                Product = product,
                Cost = totalCost,
                Skidka = discountPercentage,
                CostAfter = costAfterDiscount
            };

            m_productsInOrder.Add(productInOrder);

            // Пересчитываем общие скидки и цены со скидкой для всех продуктов в заказе
            UpdateDiscounts();
        }

        // Метод для пересчета скидки и цены со скидкой для всех товаров в m_productsInOrder
        private void UpdateDiscounts()
        {
            // Подсчитываем общее количество товаров
            int totalAmount = m_productsInOrder.Sum(item => item.Amount);

            // Определяем процент скидки на основе общего количества
            int discountPercentage = 0;
            if (totalAmount >= 100)
                discountPercentage = 20;
            else if (totalAmount >= 50)
                discountPercentage = 10;
            else if (totalAmount >= 20)
                discountPercentage = 5;

            // Перерасчитываем скидку и цену со скидкой для каждого товара
            foreach (var item in m_productsInOrder)
            {
                item.Skidka = discountPercentage;
                item.CostAfter = item.Cost - (item.Cost * discountPercentage / 100);
            }
            // Обновляем представление DataGrid
            ProductsInOrder.Items.Refresh();
        }



        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_productsInOrder.Count < 1)
            {
                MessageBox.Show("Добавьте продукты");
                return;
            }

            // Проверяем, что текущий пользователь установлен
            if (Session.currentUser == null)
            {
                MessageBox.Show("Не удалось определить текущего пользователя");
                return;
            }

           var orderDto = new Order()
            {
                UserID = Session.currentUser.ID, // Используем ID текущего пользователя
                Date = DateTime.Now,
                Status = false,
                Oplata = false
            };

            try
            {
                // Добавляем новый заказ в базу
                var order = m_entities.Orders.Add(orderDto);

                // Перебираем все товары в заказе
                foreach (var item in m_productsInOrder)
                {
                    item.OrderID = order.ID;

                    // Убедитесь, что Skidka и CostAfter правильно установлены для сохранения
                    m_entities.ProductsInOrders.Add(new ProductsInOrder
                    {
                        OrderID = order.ID,
                        ProductArticle = item.ProductArticle,
                        Product = item.Product,
                        Amount = item.Amount,
                        Cost = item.Cost,
                        Skidka = item.Skidka,   
                        CostAfter = item.CostAfter 
                    });
                }

                // Сохраняем изменения в базе данных
                m_entities.SaveChanges();

                // Очищаем список добавленных продуктов
                m_productsInOrder.Clear();

                // Обновляем отображение заказов
                UpdateOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var order = Orders.SelectedItem as Order;
            RemoveOrderButton.IsEnabled = order != null && !order.Status && !order.Oplata;
            Correct.IsEnabled = order != null && !order.Oplata ;
        }

        private void RemoveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var order = Orders.SelectedItem as Order;
            try
            {
                m_entities.ProductsInOrders.RemoveRange(order.ProductsInOrders);
                m_entities.Orders.Remove(order);
                m_entities.SaveChanges();
                UpdateOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateOrderList()
        {
            Orders.ItemsSource = m_entities.Orders
                .Where(order => order.UserID == Session.currentUser.ID)
                .ToList();
        }

        private void NameFind_TextChanged(object sender, EventArgs e)
        {
            // Получаем введенный текст для поиска
            _searchText = NameFind.Text?.ToLower();
            ApplyFilters();
        }

        private void TypeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный тип продукта
            _selectedType = TypeProduct.SelectedItem as string;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            // Обновляем фильтр для представления данных с учётом обоих условий
            _productsView.Filter = item =>
            {
                var productInOrder = item as ProductsInOrder;

                // Фильтрация по названию и типу продукта
                return (string.IsNullOrEmpty(_searchText) || productInOrder.Product.Name.ToLower().Contains(_searchText)) &&
                       (string.IsNullOrEmpty(_selectedType) || productInOrder.Product.ProductType.Name == _selectedType);
            };
        }

        private void Correct_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что выделен заказ
            var order = Orders.SelectedItem as Order;
            if (order == null)
            {
                MessageBox.Show("Выберите заказ для изменения оплаты.");
                return;
            }

            try
            {
                // Меняем статус оплаты на true
                order.Oplata = true;

                // Сохраняем изменения в базе данных
                m_entities.SaveChanges();

                // Обновляем список заказов
                UpdateOrderList();

                MessageBox.Show("Статус оплаты успешно изменен на 'Оплачено'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении статуса оплаты: {ex.Message}");
            }
        }
    }
}
