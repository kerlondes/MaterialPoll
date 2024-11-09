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
    /// Логика взаимодействия для CreateOrEditPartnerWindow.xaml
    /// </summary>
    public partial class CreateOrEditPartnerWindow : Window
    {
        private readonly BDEntities entities;
        private Partner CurrentEditablePartner;

        public CreateOrEditPartnerWindow(Partner partner = null)
        {
            InitializeComponent();

            entities = BDEntities.GetInstance();

            this.Title = partner == null ? "Добавить партнёра" : "Редактировать партнёра";

            SaveButton.Visibility = partner == null ? Visibility.Hidden : Visibility.Visible;
            AddButton.Visibility = partner != null ? Visibility.Hidden : Visibility.Visible;

            CurrentEditablePartner = partner;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var types = entities.PartnerTypes.ToList();

            Type.ItemsSource = types;

            if (CurrentEditablePartner == null) return;

            Name.Text = CurrentEditablePartner.Name;
            Type.SelectedIndex = types.FindIndex(type => type.ID == CurrentEditablePartner.TypeID);
            Director.Text = CurrentEditablePartner.Director;
            Email.Text = CurrentEditablePartner.Email;
            PhoneNumber.Text = CurrentEditablePartner.PhoneNumber;
            Address.Text = CurrentEditablePartner.Address;
            INN.Text = CurrentEditablePartner.INN.ToString();
            Rating.Text = CurrentEditablePartner.Rating.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields()) return;

            var partner = new Partner() { ID = CurrentEditablePartner.ID };
            partner.Name = Name.Text;
            partner.TypeID = (Type.SelectedItem as PartnerType).ID;
            partner.Director = Director.Text;
            partner.Email = Email.Text;
            partner.PhoneNumber = PhoneNumber.Text;
            partner.Address = Address.Text;
            partner.INN = double.Parse(PhoneNumber.Text);
            partner.Rating = int.Parse(Rating.Text);


            try
            {
                entities.Partners.AddOrUpdate(partner);
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

            if (!ValidateFields()) return;

            var partner = new Partner();
            partner.Name = Name.Text;
            partner.TypeID = (Type.SelectedItem as PartnerType).ID;
            partner.Director = Director.Text;
            partner.Email = Email.Text;
            partner.PhoneNumber = PhoneNumber.Text;
            partner.Address = Address.Text;
            partner.INN = double.Parse(PhoneNumber.Text);
            partner.Rating = int.Parse(Rating.Text);

            try
            {
                entities.Partners.AddOrUpdate(partner);
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
            if (string.IsNullOrEmpty(Name.Text))
            {
                MessageBox.Show("Заполните название");
                return false;
            }

            if (Type.SelectedItem is null)
            {
                MessageBox.Show("Укажите тип");
                return false;
            }

            if (string.IsNullOrEmpty(Director.Text))
            {
                MessageBox.Show("Заполните ФИО директора");
                return false;
            }
            if (string.IsNullOrEmpty(Email.Text))
            {
                MessageBox.Show("Заполните почту");
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumber.Text))
            {
                MessageBox.Show("Заполните номер телефона");
                return false;
            }
            if (!double.TryParse(INN.Text, out var _))
            {
                MessageBox.Show("Заполните ИНН");
                return false;
            }

            if (!int.TryParse(Rating.Text, out var _))
            {
                MessageBox.Show("Заполните рейтинг");
                return false;
            }

            return true;
        }

    }
}
