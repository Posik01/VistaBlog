using VistaBlog.Models;

namespace VistaBlog.ViewModels
{
    public class HomeVM
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? ThumbnailUrl { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
