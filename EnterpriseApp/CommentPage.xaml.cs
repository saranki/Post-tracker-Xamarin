using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseApp.Data;
using EnterpriseApp.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnterpriseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentPage : ContentPage
    {
        public ObservableCollection<Comment> CommentList { get; }

        DataRetriever dataRetriever;
        public int? authorId;

        public CommentPage()
        {
            InitializeComponent();
        }

        public CommentPage(Post post) : this()
        {
            BindingContext = post;
            authorId = post.UserId;

            Title = "Comments";

            CommentList = new ObservableCollection<Comment>();
            dataRetriever = new DataRetriever();

            CommentsLoaderIndicator.IsRunning = true;
            CommentsLoaderIndicator.IsVisible = true;

            CommentListView.ItemsSource = CommentList;

            CommentListView.ItemSelected += CommentListView_ItemSelected;

            LoadComments(post.Id);

            AddToolbarItem();
        }

        private void AddToolbarItem()
        {
            var toolBarItem = new ToolbarItem
            {
                Icon = "@drawable/author.png"
            };

            toolBarItem.Clicked += OnAuthorIconClicked;
            ToolbarItems.Add(toolBarItem);
        }

        private void OnAuthorIconClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage(authorId));
        }

        private async void LoadComments(int? id)
        {
            CommentList.Clear();

            List<Comment> comments = await dataRetriever.GetCommentAsync(id);

            foreach (Comment c in comments)
            {
                CommentList.Add(c);

                if (CommentList.Count > 0)
                {
                    CommentsLoaderIndicator.IsRunning = false;
                    CommentsLoaderIndicator.IsVisible = false;
                }
            }
        }

        private void CommentListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Comment comment = e.SelectedItem as Comment;
                string mailto = "mailto:" + comment.Email;

                Device.OpenUri(new Uri(mailto));
            }

            //Clear Selection
            CommentListView.SelectedItem = null;
        }
    }
}