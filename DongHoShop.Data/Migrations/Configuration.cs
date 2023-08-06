namespace DongHoShop.Data.Migrations
{
    using DongHoShop.Common;
    using DongHoShop.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DongHoShop.Data.DongHoShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DongHoShop.Data.DongHoShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateProductSample(context);
            CreateSlideSample(context);
            CreatePageSample(context);
            CreateContactDetailSample(context);
            CreateConfigTitle(context);
            CreateFooter(context);
            CreateUserSample(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


        }
        public void CreateFooter(DongHoShop.Data.DongHoShopDbContext context)
        {
            if(context.Footers.Count(x=>x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                context.Footers.Add(new Footer()
                {
                    ID = CommonConstants.DefaultFooterId,
                    Content = content
                });
            }
        }
        public void CreateConfigTitle(DongHoShop.Data.DongHoShopDbContext context)
        {
            if(!context.SystemConfigs.Any(x=>x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ DONGHOSHOP",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ DONGHOSHOP",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ DONGHOSHOP",
                });
            }
           

        }
        public void CreateUserSample(DongHoShop.Data.DongHoShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DongHoShopDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DongHoShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "nguyen",
                Email = "nguyen@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Thach Nguyen"
            };
            if(manager.Users.Count(x=>x.UserName == "nguyen") == 0)
            {
                manager.Create(user, "123456");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("nguyen@gmail.com");
                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }
           
        }
        private void CreateProductCategorySample(DongHoShop.Data.DongHoShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listproductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name = "Đồng hồ 1", Alias = "dong-ho-1", Status = true },
                    new ProductCategory() { Name = "Đồng hồ 2", Alias = "dong-ho-2", Status = true },
                    new ProductCategory() { Name = "Đồng hồ 3", Alias = "dong-ho-3", Status = true },
                    new ProductCategory() { Name = "Đồng hồ 4", Alias = "dong-ho-4", Status = true }
                };
                context.ProductCategories.AddRange(listproductCategory);
                context.SaveChanges();
            }

        }
        private void CreateProductSample(DongHoShop.Data.DongHoShopDbContext context)
        {
            //if (context.Products.Count() == 3)
            //{
            //    List<Product> listproduct = new List<Product>()
            //    {
            //        new Product()
            //        {
            //            Name = "Smart Watches 1",
            //            Alias = "smart-watches-1",
            //            Image = "/Assets/client/images/images/p-1.png",
            //            CategoryID = 1,
            //            Price = 1000000,
            //            Quantity = 10,
            //            OriginalPrice = 900000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 2",
            //            Alias = "smart-watches-2",
            //            Image = "/Assets/client/images/images/p-2.png",
            //            CategoryID = 2,
            //            Price = 12000000,
            //            Quantity = 10,
            //            OriginalPrice = 1000000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 3",
            //            Alias = "smart-watches-3",
            //            Image = "/Assets/client/images/images/p-3.png",
            //            CategoryID = 3,
            //            Price = 13000000,
            //            Quantity = 10,
            //            OriginalPrice = 11000000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 4",
            //            Alias = "smart-watches-4",
            //            Image = "/Assets/client/images/images/p-4.png",
            //            CategoryID = 1,
            //            Price = 14000000,
            //            Quantity = 10,
            //            OriginalPrice = 12000000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 5",
            //            Alias = "smart-watches-5",
            //            Image = "/Assets/client/images/images/p-5.png",
            //            CategoryID = 2,
            //            Price = 1000000,
            //            Quantity = 10,
            //            OriginalPrice = 900000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 6",
            //            Alias = "smart-watches-6",
            //            Image = "/Assets/client/images/images/p-5.png",
            //            CategoryID = 3,
            //            Price = 1000000,
            //            Quantity = 10,
            //            OriginalPrice = 900000,
            //            Status = true
            //        },
            //          new Product()
            //        {
            //            Name = "Smart Watches 6",
            //            Alias = "smart-watches-6",
            //            Image = "/Assets/client/images/images/p-6.png",
            //            CategoryID = 1,
            //            Price = 1000000,
            //            Quantity = 10,
            //            OriginalPrice = 900000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 7",
            //            Alias = "smart-watches-7",
            //            Image = "/Assets/client/images/images/p-7.png",
            //            CategoryID = 1,
            //            Price = 1000000,
            //            Quantity = 10,
            //            OriginalPrice = 900000,
            //            Status = true
            //        },
            //        new Product()
            //        {
            //            Name = "Smart Watches 8",
            //            Alias = "smart-watches-8",
            //            Image = "/Assets/client/images/images/p-8.png",
            //            CategoryID = 2,
            //            Price = 1000000,
            //            Quantity = 10,
            //            OriginalPrice = 900000,
            //            Status = true
            //        }

            //};
            //    context.Products.AddRange(listproduct);
            //    context.SaveChanges();
            //}

        }
        private void CreateSlideSample(DongHoShop.Data.DongHoShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide()
                    {
                        Name = "Slide 1",
                        DisplayOrder = 1,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bnr-1.jpg",
                        Content = @"<h3>Big
							<span>Save</span>
						</h3>
						<p>Get flat
							<span>10%</span> Cashback</p>
						<a class=""button2"" href=""san-pham.html"">Shop Now </a>"
                    },
                    new Slide()
                    {
                        Name = "Slide 2",
                        DisplayOrder = 1,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bnr-2.jpg",
                        Content =@"<h3>Healthy
							<span>Saving</span>
						</h3>
						<p>Get Upto
							<span>30%</span> Off</p>
						<a class=""button2"" href=""san-pham.html"">Shop Now </a>"
                    },
                        new Slide()
                    {
                        Name = "Slide 3",
                        DisplayOrder = 1,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bnr-3.jpg",
                        Content =@"<h3>Big
							<span>Deal</span>
						</h3>
						<p>Get Best Offer Upto
							<span>20%</span>
						</p>
						<a class=""button2"" href=""san-pham.html"">Shop Now </a>"
                    }
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
        private void CreatePageSample(DongHoShop.Data.DongHoShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "Giới thiệu",
                    Alias = "gio-thieu",
                    Content = @"Năm 2011, SHOPDONGHO thành lập và mở ra cửa hàng bán lẻ đầu tiên. Cho đến nay, 
                            SHOPDONGHO không ngừng mang đến những lựa chọn kết hợp giữa thương hiệu, thiết kế
                            và giá cả thể hiện phong cách và cá tính của người Việt.",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
        private void CreateContactDetailSample(DongHoShop.Data.DongHoShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var contactDetail = new ContactDetail()
                {
                    Name = "Shop đồng hồ Duye Nguyễn",
                    Address = "Khu dân cư 30 đường Nguyễn Văn Linh, phường Hưng Lợi, Ninh Kiều - Cần Thơ",
                    Phone = "0386998454",
                    Email = "duyenguyen@gmail.com.vn",
                    Lat = 10.023279,
                    Lng = 105.762753,
                    Status = true
                };
                context.ContactDetails.Add(contactDetail);
                context.SaveChanges();
            }
        }

    }
}
