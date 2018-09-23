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
        static int? postId;

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
            postId = post.Id;
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
            try
            {
                Navigation.PushAsync(new ProfilePage(authorId));
            }
            catch (System.Net.WebException ex)
            {
                CommentsLoaderIndicator.IsRunning = false;
                CommentsLoaderIndicator.IsVisible = false;

                DisplayAlert("Alert", "Please check your internet connection", "OK");

            }

        }

        private async void LoadComments(int? id)
        {
            CommentList.Clear();

            try
            {
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
            catch (System.Net.WebException e)
            {
                CommentsLoaderIndicator.IsRunning = false;
                CommentsLoaderIndicator.IsVisible = false;
                await DisplayAlert("Alert", "Please check your internet connection", "OK");

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

        private void CommentListView_Refreshing(object sender, EventArgs e)
        {
            LoadComments(postId);
            CommentListView.EndRefresh();
        }
    }
}