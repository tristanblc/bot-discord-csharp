using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary.Git
{
    public class GitUser
    {
      

        public int node_id { get; init; }

        public string login { get; init; }

        public string avatar_url{ get; init; }

        public string blog { get; init; }

        public string location { get; init; }

        public int public_repos { get; init; }

        public DateTime created_at { get; init; }


        public GitUser(int node_id, string login, string avatar_url, string blog, string location, int public_repos, DateTime created_at)
        {
            this.node_id = node_id;
            this.login = login;
            this.avatar_url = avatar_url;
            this.blog = blog;
            this.location = location;
            this.public_repos = public_repos;
            this.created_at = created_at;
        }

    }
}
