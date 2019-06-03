using NSpecifications;

namespace ASpecInvestigation.Models
{
    public class Post
    {
        public static ASpec<Post> ContainsFirst => new Spec<Post>(s => s.Title.Contains("First"));

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}