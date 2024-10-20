using AutoMapper;
using HW__6_GuestBook_IRepository.Controllers;
using HW__6_GuestBook_IRepository.Data.Repository;
using HW__6_GuestBook_IRepository.Models;
using HW__6_GuestBook_IRepository.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTestGuestBook
{
    public class UnitRegistration
    {
        [Fact]
        public async Task RegistrationUserJsonType()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc=new Mock<Encryption>();
            var controller=new RegistrationController(mok.Object,mokEnc.Object);
            //Act
           var act = await controller.UserRegistration(null);
           //Assert
           Assert.IsType<JsonResult>(act);
        }

        [Fact]
        public async Task RegistrationUserJsonTypeSuccessful()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc = new Mock<Encryption>();
            var controller = new RegistrationController(mok.Object, mokEnc.Object);
            //Act
            JsonResult act = await controller.UserRegistration(new ViewModelRegistration { Password = "Ma123fff", Login = "Ma123fff", NickName = "MyNick" }) as JsonResult;
            //Assert
            Assert.Equal( "{ response = Alldone }",act.Value.ToString());
        }


        [Fact]
        public async Task CreateUserAsyncNullReference()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc = new Mock<Encryption>();
            mok.Setup(s => s.CreateUserAsync(new User { Password = "Pass", Login = "loggin" })).Returns(CreateUserAsync(new User { Password = "Pass", Login = "loggin" }));
            var controller = new RegistrationController(mok.Object, mokEnc.Object);
            //Act
            var act = ()=> CreateUserAsync(null);
            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }


      

        [Fact]
        public async Task SaveChangeInDB()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc = new Mock<Encryption>();
            var controller = new RegistrationController(mok.Object, mokEnc.Object);
            //Act
             var blah= await controller.UserRegistration(new ViewModelRegistration { Password = "Ma123fff", Login = "Ma123fff", NickName = "MyNick" });
            //Assert
             mok.Verify(r=>r.SaveChangeAsync());
        }


        private Task CreateUserAsync(User user) {
            if (user != null)
            {

                return Task.CompletedTask;

            }
            else
            {
                throw new NullReferenceException();
            }
        }

        
    }
}