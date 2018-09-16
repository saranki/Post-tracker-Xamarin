using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseApp.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnterpriseApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentPage : ContentPage
	{
        private Post post;

        public CommentPage ()
		{
			InitializeComponent ();
		}

        public CommentPage(Post post)
        {
            this.post = post;
        }
    }
}