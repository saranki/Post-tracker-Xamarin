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

    }

}
