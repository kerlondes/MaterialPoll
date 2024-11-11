using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;

namespace Учебаня_практика.Pages
{
    public partial class AnalystPage : Page
    {
        private string _searchText = "";  // Храним текст для поиска
        private readonly BDEntities m_entities;
        private ICollectionView _authHistoryView; // Представление данных для фильтрации AuthHistory

        public AnalystPage()
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Supplies.ItemsSource = m_entities.MaterialsInStores.ToList();
            // Загружаем все заказы и устанавливаем их в DataGrid
            Orders.ItemsSource = m_entities.Orders.ToList();

            var producedOrders = m_entities.Orders
            .Where(order => order.Proizweden == true)
            .ToList();

            // Считаем общее количество произведённых продуктов по этим заказам
            int totalProducedAmount = producedOrders
                .SelectMany(order => order.ProductsInOrders) // Получаем все продукты, связанные с каждым заказом
                .Sum(product => product.Amount); // Суммируем количество продуктов в каждом заказе

            // Отображаем общее количество произведённых продуктов в Label
            ProducedLabel.Content = $"Произведённых продуктов: {totalProducedAmount}";

            // Инициализация данных для AuthHistory с именем пользователя вместо ID
            var authHistoryData = m_entities.AuthHistories
                .Select(auth => new
                {
                    UserName = m_entities.Users
                        .Where(user => user.ID == auth.UserID)
                        .Select(user => user.FIO)
                        .FirstOrDefault(),
                    Date = auth.Date
                })
                .ToList();

            // Установка источника данных и представления для AuthHistory
            AuthHistory.ItemsSource = authHistoryData;
            _authHistoryView = CollectionViewSource.GetDefaultView(authHistoryData);

            Sales.ItemsSource = m_entities.Sales.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var start = StartDate.SelectedDate;
            var end = EndDate.SelectedDate;

            var result = m_entities.Sales
                .Where(sale => start != null ? sale.Date >= start : end != null ? sale.Date <= end : true)
                .ToList();

            Sales.ItemsSource = result;
        }

        private void NameFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Получаем введенный текст для поиска
            _searchText = NameFind.Text?.ToLower();

            // Обновляем фильтр для представления данных в AuthHistory
            _authHistoryView.Filter = item =>
            {
                var authItem = item as dynamic; // Поскольку это анонимный тип, используем dynamic
                return string.IsNullOrEmpty(_searchText) ||
                       authItem.UserName?.ToLower().Contains(_searchText) == true;
            };
        }
    }
}
