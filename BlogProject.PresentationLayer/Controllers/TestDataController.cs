using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogProject.DataAccessLayer.Concrete;
using BlogProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class TestDataController : Controller
    {
        private readonly Context _context;

        public TestDataController(Context context)
        {
            _context = context;
        }

        public IActionResult SeedData()
        {
            try
            {
                // Kategorileri ekle (7 adet)
                if (!_context.Categories.Any())
                {
                    var categories = new[]
                    {
                        new Category { CategoryName = "Teknoloji", CategoryDescription = "Teknoloji haberleri ve gelişmeleri", CategoryStatus = true },
                        new Category { CategoryName = "Yazılım", CategoryDescription = "Yazılım geliştirme ve programlama", CategoryStatus = true },
                        new Category { CategoryName = "Seyahat", CategoryDescription = "Seyahat ve gezi yazıları", CategoryStatus = true },
                        new Category { CategoryName = "Spor", CategoryDescription = "Spor haberleri ve analizleri", CategoryStatus = true },
                        new Category { CategoryName = "Eğitim", CategoryDescription = "Eğitim ve öğretim konuları", CategoryStatus = true },
                        new Category { CategoryName = "Sağlık", CategoryDescription = "Sağlık ve yaşam tarzı", CategoryStatus = true },
                        new Category { CategoryName = "Kültür-Sanat", CategoryDescription = "Kültür ve sanat haberleri", CategoryStatus = true }
                    };
                    _context.Categories.AddRange(categories);
                    _context.SaveChanges();
                }

                // Writer'ları ekle (3 adet)
                if (!_context.Writers.Any())
                {
                    var writers = new[]
                    {
                        new Writer 
                        { 
                            WriterName = "Ahmet Yılmaz", 
                            WriterAbout = "10 yıllık deneyime sahip teknoloji yazarı", 
                            WriterImage = "/writerimages/writer1.jpg",
                            WriterEmail = "ahmet@blog.com",
                            WriterPassword = "123456",
                            WriterStatus = true
                        },
                        new Writer 
                        { 
                            WriterName = "Ayşe Demir", 
                            WriterAbout = "Yazılım geliştirme ve eğitim konularında uzman", 
                            WriterImage = "/writerimages/writer2.jpg",
                            WriterEmail = "ayse@blog.com",
                            WriterPassword = "123456",
                            WriterStatus = true
                        },
                        new Writer 
                        { 
                            WriterName = "Mehmet Kaya", 
                            WriterAbout = "Seyahat ve kültür yazarı", 
                            WriterImage = "/writerimages/writer3.jpg",
                            WriterEmail = "mehmet@blog.com",
                            WriterPassword = "123456",
                            WriterStatus = true
                        }
                    };
                    _context.Writers.AddRange(writers);
                    _context.SaveChanges();
                }

                // Kategorileri ve Writer'ları al
                var categoryList = _context.Categories.ToList();
                var writerList = _context.Writers.ToList();

                if (!categoryList.Any() || !writerList.Any())
                {
                    return Json(new { success = false, message = "Kategori veya Writer bulunamadı!" });
                }

                // Blog'ları ekle (28 adet)
                if (!_context.Blogs.Any())
                {
                    var blogs = new List<Blog>();
                    var random = new Random();
                    var blogTitles = new[]
                    {
                        "Yapay Zeka ve Gelecek", "ASP.NET Core ile Modern Web Geliştirme", "İstanbul'un Gizli Köşeleri",
                        "Futbol Dünyasından Son Haberler", "Online Eğitim Platformları", "Sağlıklı Yaşam İpuçları",
                        "Modern Sanat Akımları", "Blockchain Teknolojisi", "React ile Frontend Geliştirme",
                        "Kapadokya'da Bir Hafta", "Basketbol Turnuvaları", "Uzaktan Eğitim Avantajları",
                        "Beslenme ve Diyet", "Ressamlar ve Eserleri", "Cloud Computing Nedir?",
                        "C# Programlama Dili", "Ege Bölgesi Gezisi", "Tenis Dünyası",
                        "Yabancı Dil Öğrenme Yöntemleri", "Mental Sağlık", "Müze Ziyaretleri",
                        "DevOps Kültürü", "Python ile Veri Analizi", "Karadeniz Turu",
                        "Olimpiyat Oyunları", "STEM Eğitimi", "Yoga ve Meditasyon", "Sinema ve Film"
                    };

                    var blogContents = new[]
                    {
                        "Yapay zeka teknolojisi her geçen gün hayatımızı daha fazla etkiliyor. Bu yazıda yapay zekanın gelecekteki rolünü inceliyoruz.",
                        "ASP.NET Core, modern web uygulamaları geliştirmek için güçlü bir framework. Bu yazıda temel kavramları öğreneceksiniz.",
                        "İstanbul'un bilinmeyen güzelliklerini keşfedin. Şehrin saklı köşelerinde gezinti yapın.",
                        "Futbol dünyasından en son haberler ve transfer dedikoduları. Takımların durumu ve maç analizleri.",
                        "Online eğitim platformları sayesinde her yerden öğrenebilirsiniz. En iyi platformları keşfedin.",
                        "Sağlıklı bir yaşam için önemli ipuçları. Beslenme, egzersiz ve yaşam tarzı önerileri.",
                        "Modern sanat akımları ve çağdaş sanatçılar. Sanat dünyasından son gelişmeler.",
                        "Blockchain teknolojisi finans dünyasını değiştiriyor. Kripto paralar ve NFT'ler hakkında bilgiler.",
                        "React ile modern frontend uygulamaları geliştirin. Component yapısı ve state management.",
                        "Kapadokya'nın büyüleyici manzaraları ve tarihi yerleri. Balon turu ve yeraltı şehirleri.",
                        "Basketbol dünyasından son haberler. NBA ve EuroLeague analizleri.",
                        "Uzaktan eğitimin avantajları ve dezavantajları. Online öğrenme deneyimleri.",
                        "Sağlıklı beslenme ve diyet önerileri. Besin değerleri ve kalori hesaplamaları.",
                        "Ünlü ressamlar ve eserleri. Sanat tarihinden önemli isimler.",
                        "Cloud computing nedir ve nasıl çalışır? Bulut teknolojileri hakkında detaylı bilgi.",
                        "C# programlama dili ile başlangıç seviyesinden ileri seviyeye kadar öğrenme rehberi.",
                        "Ege bölgesinin tarihi ve doğal güzellikleri. Antik kentler ve plajlar.",
                        "Tenis dünyasından son haberler. Grand Slam turnuvaları ve oyuncu profilleri.",
                        "Yabancı dil öğrenmenin en etkili yöntemleri. Dil öğrenme teknikleri ve ipuçları.",
                        "Mental sağlık ve psikolojik iyi oluş. Stres yönetimi ve mindfulness.",
                        "Türkiye'deki önemli müzeler ve koleksiyonlar. Kültürel mirasımız.",
                        "DevOps kültürü ve uygulamaları. CI/CD pipeline ve otomasyon.",
                        "Python ile veri analizi ve makine öğrenmesi. Pandas ve NumPy kütüphaneleri.",
                        "Karadeniz bölgesinin doğal güzellikleri. Yeşil vadiler ve yaylalar.",
                        "Olimpiyat oyunları tarihi ve önemli anlar. Sporcuların başarı hikayeleri.",
                        "STEM eğitimi ve çocukların geleceği. Bilim, teknoloji, mühendislik ve matematik.",
                        "Yoga ve meditasyonun faydaları. Zihinsel ve fiziksel sağlık için pratikler.",
                        "Sinema dünyasından son filmler ve yönetmenler. Film analizleri ve eleştirileri."
                    };

                    for (int i = 0; i < 28; i++)
                    {
                        var category = categoryList[random.Next(categoryList.Count)];
                        var writer = writerList[random.Next(writerList.Count)];
                        
                        blogs.Add(new Blog
                        {
                            BlogTitle = blogTitles[i],
                            BlogContent = blogContents[i] + " " + GenerateRandomContent(),
                            BlogThumnailImage = "/blogimages/thumbnail" + (i % 5 + 1) + ".jpg",
                            BlogImage = "/blogimages/blog" + (i % 5 + 1) + ".jpg",
                            BlogCreateDate = DateTime.Now.AddDays(-random.Next(1, 90)),
                            BlogStatus = true,
                            CategoryId = category.CategoryId,
                            WriterId = writer.WriterId
                        });
                    }
                    _context.Blogs.AddRange(blogs);
                    _context.SaveChanges();
                }

                // Blog'ları al
                var blogList = _context.Blogs.ToList();

                // Comment'leri ekle (her blog için 1-3 arası)
                if (!_context.Comments.Any() && blogList.Any())
                {
                    var comments = new List<Comment>();
                    var random = new Random();
                    var commentUsernames = new[] { "Ali Veli", "Zeynep Yıldız", "Can Öz", "Elif Şahin", "Burak Demir", "Selin Aydın", "Emre Kılıç", "Deniz Yılmaz" };
                    var commentTitles = new[] { "Harika bir yazı!", "Çok faydalı bilgiler", "Teşekkürler", "Güzel paylaşım", "Devamını bekliyorum", "Çok beğendim" };
                    var commentContents = new[] 
                    { 
                        "Bu yazı gerçekten çok bilgilendirici. Teşekkürler!",
                        "Harika bir içerik. Devamını bekliyorum.",
                        "Çok faydalı bilgiler paylaşılmış. Emeğinize sağlık.",
                        "Güzel bir yazı olmuş. Daha fazla içerik bekliyoruz.",
                        "Bu konuda daha fazla bilgi almak isterim.",
                        "Yazınızı çok beğendim. Başarılar dilerim."
                    };

                    foreach (var blog in blogList)
                    {
                        int blogCommentCount = random.Next(1, 4);
                        for (int i = 0; i < blogCommentCount; i++)
                        {
                            comments.Add(new Comment
                            {
                                CommentUsername = commentUsernames[random.Next(commentUsernames.Length)],
                                CommentTitle = commentTitles[random.Next(commentTitles.Length)],
                                CommentContent = commentContents[random.Next(commentContents.Length)],
                                CommentDate = DateTime.Now.AddDays(-random.Next(1, 30)),
                                CommentStatus = true,
                                BlogScore = random.Next(3, 6),
                                BlogId = blog.BlogId
                            });
                        }
                    }
                    _context.Comments.AddRange(comments);
                    _context.SaveChanges();
                }

                // About ekle (1 adet)
                if (!_context.Abouts.Any())
                {
                    var about = new About
                    {
                        AboutDetail1 = "Blog sitemize hoş geldiniz! Burada teknoloji, yazılım, seyahat, spor ve daha birçok konuda kaliteli içerikler bulabilirsiniz. Amacımız okuyucularımıza değerli bilgiler sunmak ve güncel haberleri paylaşmaktır.",
                        AboutDetail2 = "Ekibimiz alanında uzman yazarlardan oluşmaktadır. Her yazı titizlikle hazırlanmakta ve okuyucularımızın beğenisine sunulmaktadır. Sizlerden gelen geri bildirimler bizim için çok değerlidir.",
                        AboutImage1 = "/aboutimages/about1.jpg",
                        AboutImage2 = "/aboutimages/about2.jpg",
                        AboutMapLocation = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3009.0!2d28.9744!3d41.0082",
                        AboutStatus = true
                    };
                    _context.Abouts.Add(about);
                    _context.SaveChanges();
                }

                // Contact ekle (1 adet)
                if (!_context.Contacts.Any())
                {
                    var contact = new Contact
                    {
                        ContactUsername = "Test Kullanıcı",
                        ContactEmail = "test@example.com",
                        ContactSubject = "Genel Bilgi",
                        ContactMessage = "Sitenizi çok beğendim. Daha fazla içerik bekliyorum. Başarılar dilerim!",
                        ContactDate = DateTime.Now.AddDays(-5),
                        ContactStatus = true
                    };
                    _context.Contacts.Add(contact);
                    _context.SaveChanges();
                }

                // BlogRayting ekle (her blog için)
                var blogListForRating = _context.Blogs.ToList();
                if (!_context.BlogRaytings.Any() && blogListForRating.Any())
                {
                    var random = new Random();
                    var blogRaytings = new List<BlogRayting>();
                    
                    foreach (var blog in blogListForRating)
                    {
                        // Her blog için 1 rating kaydı (toplam puan ve oy sayısı)
                        int ratingCount = random.Next(5, 21); // 5-20 arası oy
                        int totalScore = ratingCount * random.Next(3, 6); // Her oy 3-5 arası puan
                        
                        blogRaytings.Add(new BlogRayting
                        {
                            BlogId = blog.BlogId,
                            BlogTotalScore = totalScore,
                            BlogRaytingCount = ratingCount
                        });
                    }
                    _context.BlogRaytings.AddRange(blogRaytings);
                    _context.SaveChanges();
                }

                // Newsletter ekle (10-15 adet)
                if (!_context.Newsletters.Any())
                {
                    var newsletters = new[]
                    {
                        new Newsletter { Email = "ahmet.yilmaz@example.com", EmailStaus = true },
                        new Newsletter { Email = "ayse.demir@example.com", EmailStaus = true },
                        new Newsletter { Email = "mehmet.kaya@example.com", EmailStaus = true },
                        new Newsletter { Email = "zeynep.yildiz@example.com", EmailStaus = true },
                        new Newsletter { Email = "can.oz@example.com", EmailStaus = true },
                        new Newsletter { Email = "elif.sahin@example.com", EmailStaus = true },
                        new Newsletter { Email = "burak.demir@example.com", EmailStaus = true },
                        new Newsletter { Email = "selin.aydin@example.com", EmailStaus = true },
                        new Newsletter { Email = "emre.kilic@example.com", EmailStaus = true },
                        new Newsletter { Email = "deniz.yilmaz@example.com", EmailStaus = true },
                        new Newsletter { Email = "fatma.arslan@example.com", EmailStaus = true },
                        new Newsletter { Email = "ali.veli@example.com", EmailStaus = true },
                        new Newsletter { Email = "murat.celik@example.com", EmailStaus = false },
                        new Newsletter { Email = "seda.aksoy@example.com", EmailStaus = true },
                        new Newsletter { Email = "onur.tek@example.com", EmailStaus = true }
                    };
                    _context.Newsletters.AddRange(newsletters);
                    _context.SaveChanges();
                }

                var categoryCount = _context.Categories.Count();
                var blogCount = _context.Blogs.Count();
                var writerCount = _context.Writers.Count();
                var commentCount = _context.Comments.Count();
                var aboutCount = _context.Abouts.Count();
                var contactCount = _context.Contacts.Count();
                var blogRaytingCount = _context.BlogRaytings.Count();
                var newsletterCount = _context.Newsletters.Count();

                return Json(new 
                { 
                    success = true, 
                    message = "Test verileri başarıyla eklendi!",
                    data = new
                    {
                        categories = categoryCount,
                        blogs = blogCount,
                        writers = writerCount,
                        comments = commentCount,
                        abouts = aboutCount,
                        contacts = contactCount,
                        blogRaytings = blogRaytingCount,
                        newsletters = newsletterCount
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message, innerException = ex.InnerException?.Message });
            }
        }

        private string GenerateRandomContent()
        {
            var random = new Random();
            var sentences = new[]
            {
                "Bu konuda daha fazla bilgi edinmek için araştırmalar devam ediyor.",
                "Uzmanlar bu konuda farklı görüşlere sahip.",
                "Gelecekte bu alanda önemli gelişmeler bekleniyor.",
                "Bu konu hakkında daha detaylı bilgi için takipte kalın.",
                "Araştırmalar bu alanda önemli sonuçlar ortaya koyuyor."
            };
            return sentences[random.Next(sentences.Length)];
        }
    }
}

