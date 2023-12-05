using VistaBlog.Data;
using VistaBlog.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace VistaBlog.Utilites
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                }, "Admin@0011").Wait();

                var appUser = _context.ApplicationUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }


                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                        Title = "About Us",
                        Description = "About Us: Our Story\r\nOnce upon a digital time, in the vast expanse of the internet, a group of passionate minds embarked on a whimsical journey to create a haven for ideas, stories, and musings. And thus, [Your Blog Name] was born, a virtual realm where words dance and thoughts take flight.\r\n\r\nThe Genesis\r\nOur story began when a wordsmith, a tech wizard, and a design virtuoso found themselves converging in the vast cyberspace. Bonded by a shared love for expression and creativity, they envisioned a place where the ordinary could become extraordinary, and every click could open the door to new worlds.\r\n\r\nThe Odyssey\r\nNavigating through the ever-expanding cosmos of the internet, we set sail on a digital odyssey, collecting fragments of inspiration from every corner. From the rolling hills of literature to the pixelated landscapes of technology, we gathered the building blocks of our digital dreams.\r\n\r\nThe Eureka Moment\r\nOne fateful night, under the glow of countless pixels, the Eureka moment arrived. It was clear; our blog would not be just another collection of words on a screen. It would be a sanctuary for those seeking refuge in stories, a playground for the curious, and a canvas for the imaginative.\r\n\r\nOur Manifesto\r\nWe believe in the magic of words, the alchemy that turns letters into emotions and sentences into symphonies. We celebrate the diversity of ideas, inviting dreamers, thinkers, and creators to join us in our quest to explore the infinite possibilities of the written word.\r\n\r\nThe Fellowship\r\n[Your Blog Name] is not just a blog; it's a fellowship of minds. We invite you, dear reader, to embark on this adventure with us. Whether you're a seasoned wordsmith or a curious wanderer, there's a place for you in our digital tapestry.\r\n\r\nJoin Us on the Journey\r\nAs we continue to weave our narrative through the binary code and pixels, we extend an invitation for you to be a part of our story. Share your thoughts, dive into the discussions, and let the symphony of ideas resonate in this corner of the internet we call home.\r\n\r\nWelcome to VistaBlog, where imagination knows no bounds, and every click is a step into the extraordinary.",
                        Slug = "about"
                    },
                    new Page()
                    {
                        Title = "Contact Us",
                        Description = "Our email: vistablog@gmail.com",
                        Slug = "contact"
                    },
                    new Page()
                    {
                        Title = "Privacy Policy",
                        Description = "1. Information We Collect\r\n1.1 Personal Information\r\nContact Information: When you comment on our blog or subscribe to our newsletter, we may collect your name and email address.\r\n\r\nUser Content: We may collect any information you voluntarily share in comments or other user-generated content.\r\n\r\n1.2 Non-Personal Information\r\nLog Data: We collect information that your browser sends whenever you visit our blog. This may include your IP address, browser type, the pages you visit, and other statistics.\r\n\r\nCookies: We use cookies to collect information about your preferences and to personalize your experience on our blog.\r\n\r\n2. How We Use Your Information\r\nWe may use the collected information for the following purposes:\r\n\r\nTo improve and personalize your experience on our blog.\r\nTo respond to your comments and inquiries.\r\nTo send periodic emails related to blog updates, promotions, or other relevant information.\r\n3. How We Protect Your Information\r\nWe implement a variety of security measures to maintain the safety of your personal information. However, please be aware that no method of transmission over the internet or electronic storage is 100% secure.\r\n\r\n4. Disclosure of Information\r\nWe do not sell, trade, or otherwise transfer your personal information to outside parties. This does not include trusted third parties who assist us in operating our blog, as long as they agree to keep this information confidential.\r\n\r\n5. Third-Party Links\r\nOur blog may contain links to third-party websites. We have no control over the content and practices of these sites and are not responsible for their privacy policies.\r\n\r\n6. Your Choices\r\nYou may choose to disable cookies through your browser settings. However, this may affect your ability to interact with our blog.\r\n\r\n7. Changes to This Privacy Policy\r\nWe may update this Privacy Policy from time to time. The date of the latest revision will be indicated at the top of the page.\r\n\r\n8. Contact Us\r\nIf you have any questions or concerns about this Privacy Policy, please contact us at vistablog@gmail.com.\r\n\r\nBy using our blog, you consent to this Privacy Policy.",
                        Slug = "privacy"
                    }
                };

                _context.Pages!.AddRange(listOfPages);

                var setting = new Setting
                {
                    SiteName = "Site Name",
                    Title = "Site Title",
                    ShortDescription = "Short Description of site"
                };

                _context.Settings!.Add(setting);
                _context.SaveChanges();

            }
        }
    }
}