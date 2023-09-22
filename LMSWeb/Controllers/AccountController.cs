using LMS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LMSWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using LMS.DataAccess;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authorization;
using LMS.DataAccess.Repository.IRepository;

namespace LMSWeb.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        //private readonly IUnitOfWork _unitOfWork;

        //public AccountController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}


        public AccountController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost(User u)
        {
            if (u.UserName != "")
            {
                if (IsValidUser(u.UserName, u.Password))
                {
                    var user = _applicationDbContext.Users.SingleOrDefault(o => o.UserName == u.UserName);

                    //var cookie = Microsoft.AspNetCore.Http.

                    //CookieOptions langCookie = new CookieOptions(); 

                    //langCookie.Expires = DateTime.Now.AddYears(1);

                    // Kullanıcı doğrulama başarılı, rolüne göre yönlendirme yapabilirsiniz
                    if (user.Role == UserRole.Admin)
                    {
                        // Admin ise, admin sayfasına yönlendirin
                        return RedirectToAction("AdminPage", "Home");
                    }
                    else if (user.Role == UserRole.Instructor)
                    {
                        // Eğitmen ise, eğitmen sayfasına yönlendirin
                        return RedirectToAction("InstructorPage", "Home");
                    }
                    else if (user.Role == UserRole.User)
                    {
                        // Kullanıcı ise, kullanıcı sayfasına yönlendirin
                        return RedirectToAction("UserPage", "Home");
                    }
                    return RedirectToAction("Index", "Course");

                }
                // Kullanıcı adı veya şifre geçersizse veya kullanıcı bulunamazsa, hatalı kullanıcı adı veya şifre mesajı gönderin
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
            }

            return View("Index");
        }

        private bool IsValidUser(string username, string password)
        {

            // Kullanıcıyı veritabanından username (kullanıcı adı) ile bulun
            var user = _applicationDbContext.Users.SingleOrDefault(u => u.UserName == username);

            if (user != null)
            {
                // Kullanıcının şifresini doğrulayın
                bool isPasswordValid = user.Password == password;

                if (isPasswordValid)
                {
                    // Kullanıcı doğruysa, burada kullanıcının rolüne göre işlem yapabilirsiniz
                    return true; // Kullanıcı doğrulandı
                }
            }
            // Kullanıcı adı veya şifre geçersizse veya kullanıcı bulunamazsa, giriş başarısızdır
            return false;

        }





        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////777
        //public async Task<IActionResult> Index([FromForm] User loginUser)
        //{
        //    try
        //    {
        //        //var user = _context.Users.FirstOrDefault(e => e.UserName == loginUser.UserName && e.Password == loginUser.Password);
        //        var user = _unitOfWork.User.Get(e => e.UserName == loginUser.UserName && e.Password == loginUser.Password);
        //        if (user == null)
        //            return Redirect("Account"); // Invalid username or password.
        //        // Defining Cookies
        //        List<Claim> claims = new List<Claim>();
        //        claims.Add(new Claim("id", user.UserId.ToString()));
        //        claims.Add(new Claim("username", user.UserName));
        //        claims.Add(new Claim("password", user.Password));
        //        claims.Add(new Claim("role", user.Role.ToString()));
        //        var claimsIdentity = new ClaimsIdentity(claims, "user");
        //        var principal = new ClaimsPrincipal(claimsIdentity);
        //        // Creating Cookie
        //        await HttpContext.SignInAsync("user", principal);

        //        return Redirect("Home");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        #region Calıştırmadığım
        //private bool IsValidUser(ApplicationDbContext _applicationDbContext, string your_name, string your_pass)
        //{
        //    // Kullanıcıyı veritabanından username (kullanıcı adı) ile bulun
        //    var user = _applicationDbContext.Users.SingleOrDefault(u => u.UserName == your_name);

        //    if (user != null)
        //    {
        //        // Kullanıcının şifresini doğrulayın
        //        bool isPasswordValid = user.Password == your_pass;

        //        if (isPasswordValid)
        //        {
        //            // Kullanıcı doğruysa, burada kullanıcının rolüne göre işlem yapabilirsiniz
        //            return true; // Kullanıcı doğrulandı
        //        }
        //    }

        //    // Kullanıcı adı veya şifre geçersizse veya kullanıcı bulunamazsa, giriş başarısızdır
        //    return false;

        //    //
        //    // user tablosunda bu isimde kişi var mı, varsa kişiyi komple getir.
        //    // Yoksa hatalı kulanıcı mesajı gönder. 
        //    // varsa gelen kişinin şifresiyle ekranda girilen şifreyi karşılaştır.
        //    // şifre yanlışsa hatalı kullanıcı adı veya şifre mesajı
        //    // doğruysa öğretmen mi öğrenci mi kontrolu yapılır.
        //    // öğretmense öğretmen safasına öğrenciyse öğrenci sayfasını aç


        //}

        #endregion



        public IActionResult Kayit()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Kayit")]
        public IActionResult Kayit(KayitRequest model)
        {
            if (ModelState.IsValid)
            {


                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
            }

            var user = new User
            {
                UserName = model.name,
                Email = model.email,
                Password = model.pass,




            };



            _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();


            //return View("Index");

            return RedirectToAction("Index", "Account");

        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public async Task<IActionResult> Kayit([FromForm] User loginUser)
        //{
        //    try
        //    {
        //        var user = _unitOfWork.User.Get(e => e.UserName == loginUser.UserName);
        //        if (user != null)
        //        {
        //            return Redirect("Account"); // This user is already exists.
        //        }
        //        else
        //        {
        //            if (ModelState.IsValid) // User field is intentionally nullable for now can't solve the ModelState.IsValid - User field is required problem
        //            {
        //                _unitOfWork.User.Add(loginUser);
        //                _unitOfWork.Save();
        //                var newUser = _unitOfWork.User.Get(u => u.UserName == loginUser.UserName && u.Password == loginUser.Password);
        //                List<Claim> claims = new List<Claim>();
        //                claims.Add(new Claim("id", newUser.UserId.ToString()));
        //                claims.Add(new Claim("username", newUser.UserName));
        //                claims.Add(new Claim("password", newUser.Password));
        //                claims.Add(new Claim("role", newUser.Role.ToString()));
        //                var claimsIdentity = new ClaimsIdentity(claims, "user");
        //                var principal = new ClaimsPrincipal(claimsIdentity);
        //                // Creating Cookie
        //                await HttpContext.SignInAsync("user", principal);
        //                TempData["success"] = "User created successfully";
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                return View("Kayit");
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public async Task<IActionResult> Logout()
        //{
        //    if (User.Identity.IsAuthenticated)
        //        await HttpContext.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}


