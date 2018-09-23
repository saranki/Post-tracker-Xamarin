using EnterpriseApp.Data;
using EnterpriseApp.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnterpriseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        DataRetriever dataRetriever;

        public ProfilePage ()
		{
			InitializeComponent ();
		}

        public ProfilePage(int? authorId) : this()
        {
            Title = "Author " + authorId + "'s Profile";
            dataRetriever = new DataRetriever();           

            LoadAuthorDetails(authorId);
        }

        private async void LoadAuthorDetails(int? id)
        {
            try
            {
                Author author = await dataRetriever.GetAuthorAsync(id);
                BindingContext = author;
            }
            catch (System.Net.WebException e)
            {
                await DisplayAlert("Alert", "Please check your internet connection", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}