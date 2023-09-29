namespace Forum.Data.Seeding
{
    using Models;

    class PostSeeder
    {
        internal Post[] GeneratePosts()
        {
            ICollection<Post> posts = new HashSet<Post>();
            Post currentPost;

            currentPost = new Post()
            {
                Title = "My first post",
                Content = "At this post the squirrel ate the acorn."
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My second post",
                Content = "At this post the shark ate the person."
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My third post",
                Content = "It would be weird if the shark ate acorns or..."
            };
            posts.Add(currentPost);

            return posts.ToArray();
        }
    }
}
