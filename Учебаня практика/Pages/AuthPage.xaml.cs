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
using static System.Collections.Specialized.BitVector32;

namespace Учебаня_практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            m_mainWindow = Application.Current.MainWindow as MainWindow;
        }
        private BDEntities m_entities = BDEntities.GetInstance();
        private readonly MainWindow m_mainWindow;



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(m_mainWindow.mainFrame.CanGoBack.ToString());

        }
        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (Password.Visibility == Visibility.Visible)
            {
                // Если видим PasswordBox, показываем TextBox с текстом
                PasswordTextBox.Text = Password.Password;
                Password.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                EyeIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri("/icon/glas.jpg", System.UriKind.Relative)); // Открытый глаз
            }
            else
            {
                // Если видим TextBox, переключаем обратно на PasswordBox с символами
                Password.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                Password.Visibility = Visibility.Visible;
                EyeIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri("/icon/neglas.jpg", System.UriKind.Relative)); // Закрытый глаз
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Login.Text))
            {
                MessageBox.Show("Введите логин");
                return;
            }

            var user = m_entities.Users
            .Where(a => a.Login == Login.Text)
            .FirstOrDefault();

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            if (user.Password != Password.Password)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            Session.currentUser = user;
            m_entities.AuthHistories.Add(new AuthHistory() { UserID = user.ID, Date = DateTime.Now });

            switch (user.Role.Name)
            {
                case "Менеджер":
                    m_mainWindow.NavigateToManagerPage();
                    break;
                case "Мастер":
                    m_mainWindow.NavigateToMasterPage();
                    break;
                case "Партнёр":
                    m_mainWindow.NavigateToPartnerPage();
                    break;
                case "Кадры":
                    m_mainWindow.NavigateToStaffPage();
                    break;
            }

        }
    }
}
