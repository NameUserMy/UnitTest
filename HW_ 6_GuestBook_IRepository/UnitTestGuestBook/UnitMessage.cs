using HW__6_GuestBook_IRepository.Controllers;
using HW__6_GuestBook_IRepository.Data.Repository;
using HW__6_GuestBook_IRepository.Models;
using HW__6_GuestBook_IRepository.Services;
using Microsoft.AspNetCore.Mvc;

using Moq;


namespace UnitTestGuestBook
{
    public class UnitMessage
    {
        [Fact]
        public void IsView()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc = new Mock<Encryption>();
            var controller = new MessageController(mok.Object);
            //Act
            var view = controller.MessageSend();
            //Assert
            Assert.IsType<ViewResult>(view);
        }

        [Fact]
        public async Task MessageSend()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc = new Mock<Encryption>();
            var controller = new MessageController(mok.Object);
            //Act
            JsonResult act = await controller.MessageSend(new ViewModelPublish { Message = "Mymessage", Theme = "MyTheme" }) as JsonResult;
            //Assert
            Assert.Equal("All done", act.Value.ToString());

        }


        [Fact]
        public async Task MessageSaveDb()
        {
            //Arrange
            var mok = new Mock<IRepository>();
            var mokEnc = new Mock<Encryption>();
            var controller = new MessageController(mok.Object);
            //Act
            await controller.MessageSend(new ViewModelPublish { Message = "My message", Theme = "MyTheme" });
            //Assert
            mok.Verify(r => r.SaveChangeAsync());

        }
    }
}
