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
    /// Логика взаимодействия для CreateOrEditUserWindow.xaml
    /// </summary>
    public partial class CreateOrEditUserWindow : Window
    {
        private readonly BDEntities entities;
        private User CurrentEditableUser;

        public CreateOrEditUserWindow(User user = null)
        {
            InitializeComponent();

            entities = BDEntities.GetInstance();

            this.Title = user == null ? "Добавить пользователя" : "Редактировать пользователя";

            SaveButton.Visibility = user == null ? Visibility.Hidden : Visibility.Visible;
            AddButton.Visibility = user != null ? Visibility.Hidden : Visibility.Visible;

            CurrentEditableUser = user;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var roles = entities.Roles.ToList();
            var partners = entities.Partners.ToList();

            Role.ItemsSource = roles;
            Partner.ItemsSource = partners;

            if (CurrentEditableUser == null) return;

            FIO.Text = CurrentEditableUser.FIO;
            Role.SelectedIndex = roles.FindIndex(role => role.ID == CurrentEditableUser.RoleID);
            Partner.SelectedIndex = partners.FindIndex(partner => partner.ID == CurrentEditableUser.PartnerID);
            Passport.Text = CurrentEditableUser.Passport;
            Login.Text = CurrentEditableUser.Login;
            Birthday.SelectedDate = CurrentEditableUser.Birthday;
            HaveFamily.IsChecked = CurrentEditableUser.HaveFamily;
            Health.IsChecked = CurrentEditableUser.Health;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (!ValidateFields()) return;

            CurrentEditableUser.FIO = FIO.Text;
            CurrentEditableUser.RoleID = (Role.SelectedItem as Role).ID;
            CurrentEditableUser.PartnerID = (Partner.SelectedItem as Partner)?.ID;
            CurrentEditableUser.Passport = Passport.Text;
            CurrentEditableUser.Login = Login.Text;
            CurrentEditableUser.Birthday = (DateTime)Birthday.SelectedDate;
            CurrentEditableUser.HaveFamily = HaveFamily.IsChecked ?? false;
            CurrentEditableUser.Health = Health.IsChecked ?? true;

            try
            {
                entities.Users.AddOrUpdate(CurrentEditableUser);
                entities.SaveChanges();

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

            CurrentEditableUser = new User();
            if (!ValidateFields()) return;


            CurrentEditableUser.FIO = FIO.Text;
            CurrentEditableUser.RoleID = (Role.SelectedItem as Role).ID;
            CurrentEditableUser.PartnerID = (Partner.SelectedItem as Partner)?.ID;
            CurrentEditableUser.Passport = Passport.Text;
            CurrentEditableUser.Login = Login.Text;
            CurrentEditableUser.Birthday = (DateTime)Birthday.SelectedDate;
            CurrentEditableUser.HaveFamily = HaveFamily.IsChecked ?? false;
            CurrentEditableUser.Health = Health.IsChecked ?? true;

            try
            {
                entities.Users.AddOrUpdate(CurrentEditableUser);
                entities.SaveChanges();

                MessageBox.Show("Успешно");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(FIO.Text))
            {
                MessageBox.Show("Заполните ФИО");
                return false;
            }

            if (Role.SelectedItem is null)
            {
                MessageBox.Show("Укажите роль");
                return false;
            }

            if (string.IsNullOrEmpty(Passport.Text))
            {
                MessageBox.Show("Заполните пасспорт");
                return false;
            }

            if (string.IsNullOrEmpty(Login.Text))
            {
                MessageBox.Show("Заполните логин");
                return false;
            }

            if (string.IsNullOrEmpty(CurrentEditableUser.Password) || string.IsNullOrEmpty(Password.Text))
            {
                MessageBox.Show("Заполните пароль");
                return false;
            }

            if (Birthday.SelectedDate is null)
            {
                MessageBox.Show("Укажите дату рождения");
                return false;
            }

            return true;
        }
    }
}
