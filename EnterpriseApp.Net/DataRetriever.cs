using EnterpriseApp.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EnterpriseApp.Net
{
    public class DataRetriever
    {
        public DataRetriever()
        {
            
        }

        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://jsonplaceholder.typicode.com/posts");

                if (!string.IsNullOrEmpty(json))
                {
                    posts = JsonConvert.DeserializeObject<Post[]>(json).ToList();
                }
            }
            return posts;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var asyncTask = Task.Run(() => GetPosts());
            List<Post> result = await asyncTask;
            return result;
        }

        public List<Comment> GetComments(int? id)
        {
            List<Comment> comments = new List<Comment>();

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://jsonplaceholder.typicode.com/posts/" + id + "/comments");

                if (!string.IsNullOrEmpty(json))
                {
                    comments = JsonConvert.DeserializeObject<Comment[]>(json).ToList();
                }
            }
            return comments;
        }

        public async Task<List<Comment>> GetCommentAsync(int? id)
        {
            var asyncTask = Task.Run(() => GetComments(id));
            List<Comment> result = await asyncTask;
            return result;
        }

        public Author GetAuthor(int? id)
        {
            Author author = new Author();

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://jsonplaceholder.typicode.com/users/" + id);

                if (!string.IsNullOrEmpty(json))
                {
                    author = JsonConvert.DeserializeObject<Author>(json);
                }
            }
            return author;
        }

        public async Task<Author> GetAuthorAsync(int? id)
        {
            var asyncTask = Task.Run(() => GetAuthor(id));
            Author result = await asyncTask;
            return result;
        }

    }

}
