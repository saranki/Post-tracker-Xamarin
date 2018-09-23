using EnterpriseApp.Data;
using EnterpriseApp.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;


namespace EnterpriseApp
{
    public partial class PostPage : ContentPage
    {
        public ObservableCollection<Post> PostList { get; }

        DataRetriever dataRetriever;

        public PostPage()
        {
            InitializeComponent();

            Title = "Posts";

            dataRetriever = new DataRetriever();
            PostList = new ObservableCollection<Post>();

            PostsLoaderIndicator.IsRunning = true;
            PostsLoaderIndicator.IsVisible = true;


            PostListView.ItemsSource = PostList;

            LoadData();

            PostListView.ItemSelected += PostListView_ItemSelected;
        }

        private async void LoadData()
        {
            PostList.Clear();
            try {
                List<Post> posts = await dataRetriever.GetPostsAsync();

                foreach (Post p in posts)
                {
                    PostList.Add(p);

                    if (PostList.Count > 10)
                    {
                        PostsLoaderIndicator.IsRunning = false;
                        PostsLoaderIndicator.IsVisible = false;
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                PostsLoaderIndicator.IsRunning = false;
                PostsLoaderIndicator.IsVisible = false;

                await DisplayAlert("Alert", "Please check your internet connection", "OK");

            }
            
        }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                CommentPage comment = new CommentPage(e.SelectedItem as Post);
                Navigation.PushAsync(comment);
            }
            //Clear Selection
            PostListView.SelectedItem = null;
        }

        private void PostListView_Refreshing(object sender, EventArgs e)
        {
            LoadData();
            PostListView.EndRefresh();
        }
    }
}
