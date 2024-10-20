using HW__6_GuestBook_IRepository.Data.Repository;
using HW__6_GuestBook_IRepository.Models;
using HW__6_GuestBook_IRepository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HW__6_GuestBook_IRepository.Controllers
{
    public class RegistrationController : Controller
    {


        private readonly ILogger<RegistrationController>? _logger;
        private readonly Encryption? _encryption;
        public readonly IRepository? _repository;
        public RegistrationController(IRepository repository, Encryption encryption) { 
        
            _repository = repository;
            _encryption = encryption;
            
        }
        public IActionResult UserRegistration()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public async Task <IActionResult> CheckLogin(string login)
        {
            return Json(await _repository.IsLoginAsync(login));

        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckNickName(string nickName)
        {
            return Json(await _repository.IsNickNameAsync(nickName));

        }



        [HttpPost]
        public async Task<ActionResult> UserRegistration(ViewModelRegistration incomminUser)
        {

            if (incomminUser == null)
            {
                return Json(new { response = "Somthing wrone" });
            }



          
            if (incomminUser.Login.ToLower() == "login" || incomminUser.NickName.ToLower() == "nickName"
                || incomminUser.Password.ToLower() == "password")
            {

                ModelState.AddModelError("", "The data is not correct");
            }

            if (ModelState.IsValid)
            {

                _encryption.Pass = incomminUser.Password;
                _encryption.HashPass();
                User user = new User();
                user.Password = _encryption.PassDb;
                user.Salt = _encryption.SaltDb;
                user.Login = incomminUser.Login;
                user.NickName = incomminUser.NickName;

              await _repository.CreateUserAsync(user);
              await   _repository.SaveChangeAsync();


                return Json(new { response = "Alldone" });


            }
            return Problem("Something wrong");
        }


      
    }
}
