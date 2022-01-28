using Food.Controllers;
using Food.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFood
{
    public class TestFoodRepo
    {
        [Fact]
        public void IndextTest()
        {
            //Arrange
            var mock = new Mock<IDishRepository>();     //ZMIEŃ
            var customer = new DishController(mock.Object);  //ZMIEŃ CONTROLLER

            //Art
            var resultController = customer.Index();

            //Assert
            resultController.Should().NotBeNull();
            resultController.Should().BeOfType<ViewResult>();
            resultController.Should().BeAssignableTo<IActionResult>();
        }
    }
}
