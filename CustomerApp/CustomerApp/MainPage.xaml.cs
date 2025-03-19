using CustomerApp.Exceptions;
using CustomerApp.Models;
using CustomerApp.Services;
using System.Threading.Tasks;

namespace CustomerApp
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _localDbService;
        private int _editCustomerId;

        public MainPage(LocalDbService localDbService)
        {
            InitializeComponent();

            _localDbService = localDbService;
            Task.Run(async () => ListView.ItemsSource = await _localDbService.GetCustomers());
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (_editCustomerId == 0)
            {
                try
                {
                    await _localDbService.Create(new Customer
                    {
                        CustomerName = NameEntryField.Text,
                        Email = EmailEntryField.Text,
                        Mobile = MobileEntryField.Text,
                    });
                }
                catch (CustomException)
                {
                    await DisplayAlert("Error", "Error occured while creating new customer.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
            {
                try
                {
                    await _localDbService.Update(new Customer
                    {
                        Id = _editCustomerId,
                        CustomerName = NameEntryField.Text,
                        Email = EmailEntryField.Text,
                        Mobile = MobileEntryField.Text,
                    });

                    _editCustomerId = 0;
                }
                catch (CustomException)
                {
                    await DisplayAlert("Error", "Error occured while updating customer.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }

            NameEntryField.Text = string.Empty;
            EmailEntryField.Text = string.Empty;
            MobileEntryField.Text = string.Empty;

            ListView.ItemsSource = await _localDbService.GetCustomers();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var customer = (Customer)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    try
                    {
                        _editCustomerId = customer.Id;
                        NameEntryField.Text = customer.CustomerName;
                        EmailEntryField.Text = customer.Email;
                        MobileEntryField.Text = customer.Mobile;
                    }
                    catch
                    {
                        // Do something...
                    }

                    break;
                case "Delete":
                    try
                    {
                        var choice = await DisplayActionSheet("Are you sure you want to delete this record?", "Cancel", null, "Yes", "No");

                        switch (choice)
                        {
                            case "Yes":
                                await _localDbService.Delete(customer);
                                ListView.ItemsSource = await _localDbService.GetCustomers();

                                break;
                            case "Delete":
                                break;
                        }
                    }
                    catch
                    {
                        // Do something...
                    }

                    break;
            }
        }
    }

}
