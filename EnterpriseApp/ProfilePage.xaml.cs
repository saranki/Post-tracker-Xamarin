using EnterpriseApp.Data;
using EnterpriseApp.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Author author =  await dataRetriever.GetAuthorAsync(id);
            BindingContext = author;
        }
    }
}