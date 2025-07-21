using IMDBMovieProject.Business.Abstract;
using IMDBMovieProject.DataAccess.Data;
using IMDBMovieProject.Entities;
using IMDBMovieProject.WebApi.Models;
using Microsoft.AspNetCore.Authentication; //login
using Microsoft.AspNetCore.Authorization;//login
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IMDBMovieProject.WebApi.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DataBaseContext _context;

        //public AccountController(DataBaseContext context)
        //{
        //    _context = context;
        //}

        private readonly IService<AppUser> _service;

        public AccountController(IService<AppUser> service)
        {
            _service = service;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            AppUser user= await _service.GetAsync (x=>x.UserGuid.ToString() == 
            HttpContext.User.FindFirst("UserGuid").Value);
            if (user is null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel
            {
                Email = user.Email,
                Password = user.Password,
                Id= user.Id,
                Name = user.Name,
                Phone = user.Phone,
                SurName = user.SurName
            };
            return View(model);
        }
        [HttpPost,Authorize]
        public async Task<IActionResult> IndexAsync(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() ==
HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null)
                    {
                        user.SurName = model.SurName;
                        user.Phone = model.Phone;
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.Email = model.Email;
                        _service.Update(user);
                        var sonuc = _service.SaveChanges();
                    }
                    
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");


                }

            }
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = await _service.GetAsync(p =>
                        p.Email == loginViewModel.Email &&
                        p.Password == loginViewModel.Password && p.IsActive);

                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız: E-posta, şifre hatalı veya hesap aktif değil.");
                        return View();
                    }
                    else
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()), //  BU SATIR
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "Customer"),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim("UserGuid", account.UserGuid.ToString()),
                    new Claim("UserId", account.Id.ToString()),
                };

                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        var userPrincipal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(userPrincipal);

                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + hata.Message);
                }
            }

            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(AppUser appUser)
        {
            appUser.IsActive = false;
            appUser.IsAdmin = true;
            if (ModelState.IsValid)
            {
                _service.Add(appUser);
                await _service.SaveChangesAsync();
                return RedirectToAction("Index");
            }  
            return View(appUser);
        }
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
