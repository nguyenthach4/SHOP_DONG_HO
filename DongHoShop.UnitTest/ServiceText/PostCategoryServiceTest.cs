using DongHoShop.Data.Infrastructure;
using DongHoShop.Data.Repositories;
using DongHoShop.Model.Models;
using DongHoShop.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHoShop.UnitTest.ServiceText
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockIunitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategory;


        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockIunitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mockIunitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory(){ID =1 ,Name="DM1",Status =true},
                new PostCategory(){ID =2 ,Name="DM2",Status =true},
                new PostCategory(){ID =3 ,Name="DM3",Status =true},
            };

        }
        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

            var result = _categoryService.GetAll() as List<PostCategory>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);

        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory();
            int id = 1;
            postCategory.Name = "Test";
            postCategory.Alias = "Test";
            postCategory.Status = true;

            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
              {
                  p.ID = 1;
                  return p;
              });
            var result = _categoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
