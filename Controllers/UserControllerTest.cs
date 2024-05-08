using NUnit.Framework;
using Moq;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _users;

        [SetUp]
        public void Setup()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "User1", Email = "user1@example.com" },
                new User { Id = 2, Name = "User2", Email = "user2@example.com" },
                new User { Id = 3, Name = "User3", Email = "user3@example.com" }
            };

            _controller = new UserController();
            UserController.userlist = _users;
        }

        [Test]
        public void Index_ReturnsCorrectView_WithUsers()
        {
            var result = _controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            Assert.AreEqual("Index", result.ViewName);
            CollectionAssert.AreEqual(_users, model);
        }

        [Test]
        public void Details_ReturnsCorrectView_WithUser()
        {
            var result = _controller.Details(1) as ViewResult;
            var model = result.Model as User;

            Assert.AreEqual("Details", result.ViewName);
            Assert.AreEqual(_users[0], model);
        }

        [Test]
        public void Create_Post_RedirectsToIndex_WithNewUser()
        {
            var newUser = new User { Name = "User4", Email = "user4@example.com" };
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.Contains(newUser, UserController.userlist);
        }

        [Test]
        public void Edit_Post_RedirectsToIndex_WithUpdatedUser()
        {
            var updatedUser = new User { Name = "UpdatedUser", Email = "updateduser@example.com" };
            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(updatedUser.Name, UserController.userlist.First(u => u.Id == 1).Name);
            Assert.AreEqual(updatedUser.Email, UserController.userlist.First(u => u.Id == 1).Email);
        }

        [Test]
        public void Delete_Post_RedirectsToIndex_WithoutDeletedUser()
        {
            var result = _controller.Delete(1, null) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(UserController.userlist.FirstOrDefault(u => u.Id == 1));
        }
    }
}
