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

namespace Учебаня_практика.windows
{
    /// <summary>
    /// Логика взаимодействия для EditStoreWindow.xaml
    /// </summary>
    public partial class EditStoreWindow : Window
    {
        private readonly BDEntities m_entities;
        private readonly Store m_currentStore;

        public EditStoreWindow(Store store)
        {
            InitializeComponent();
            m_entities = BDEntities.GetInstance();
            m_currentStore = store;
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var material = (Materials.SelectedItem as Material);
            if (material is null)
            {
                MessageBox.Show("Выберите материал");
                return;
            }

            if (!int.TryParse(Count.Text, out var count) || count < 1)
            {
                MessageBox.Show("Некорректное количество");
                return;
            }

            var materialInStore = new MaterialsInStore()
            {
                StoreID = m_currentStore.ID,
                MaterialID = material.ID,
                Amount = count
            };

            try
            {
                m_entities.MaterialsInStores.Add(materialInStore);
                m_entities.SaveChanges();
                MaterialsInStore.ItemsSource = m_entities.MaterialsInStores
                .Where(item => item.StoreID == m_currentStore.ID)
                .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaterialsInStore.ItemsSource = m_currentStore.MaterialsInStores;
            Materials.ItemsSource = m_entities.Materials.ToList();
        }
    }
}
