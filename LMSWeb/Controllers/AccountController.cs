using LMS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LMSWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using LMS.DataAccess;
using System.Security.Claims;

namespace LMSWeb.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;



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

        //public async Task<IActionResult> Index([FromForm] IndexRequest ındexRequest)
        //{
        //    try
        //    {
        //        var user = _applicationDbContext.Users.FirstOrDefault(e => e.UserName == ındexRequest.your_name && e.Password == ındexRequest.your_pass);
        //        if (user == null)
        //            return Redirect("Account"); // Invalid email or password.
        //        // Defining Cookie
        //        List<Claim> claims = new List<Claim>();
        //        claims.Add(new Claim("username", ındexRequest.your_name));
        //        claims.Add(new Claim("password", ındexRequest.your_pass));
        //        claims.Add(new Claim("role", user.Role.ToString()));
        //        var claimsIdentity = new ClaimsIdentity(claims, "user");
        //        var principal = new ClaimsPrincipal(claimsIdentity);
        //        // Creating Cookie
        //        await HttpContext.SignInAsync("user", principal);

        //        return RedirectToAction("Index", "Course");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}





        public IActionResult IndexPost(IndexRequest model)
        {
            if (ModelState.IsValid)
            {
                var username = model.your_name;
                var password = model.your_pass;

                if (IsValidUser(username, password))
                    //{
                    //    // Kullanıcı doğrulama başarılı, rolüne göre yönlendirme yapabilirsiniz
                    //    if (user.Role == UserRole.Admin)
                    //    {
                    //        // Admin ise, admin sayfasına yönlendirin
                    //        return RedirectToAction("AdminPage", "Home");
                    //    }
                    //    else if (user.Role == UserRole.Instructor)
                    //    {
                    //        // Eğitmen ise, eğitmen sayfasına yönlendirin
                    //        return RedirectToAction("InstructorPage", "Home");
                    //    }
                    //    else if (user.Role == UserRole.User)
                    //    {
                    //        // Kullanıcı ise, kullanıcı sayfasına yönlendirin
                    //        return RedirectToAction("UserPage", "Home");
                    //    }
                    return RedirectToAction("Index", "Course");


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


            return View("Index");
        }






    }
}





//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace LMSWeb.Controllers
//{
//    public class AccountController : Controller
//    {
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]


//        //public async Task<IActionResult> Indexsignin()
//        //{
//        //    var your_name = Request.Form["your_name"];
//        //    var your_pass = Request.Form["your_pass"];

//        //    // Örnek bir kullanıcı doğrulama mantığı, gerçek bir veritabanı sorgusu kullanmalısınız
//        //    if (IsValidUser(your_name, your_pass))
//        //    {
//        //        var claims = new List<Claim>
//        //        {
//        //            new Claim(ClaimTypes.Name, your_name)
//        //            // Kullanıcıya özgü diğer bilgileri burada ekleyebilirsiniz
//        //        };

//        //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//        //        var principal = new ClaimsPrincipal(identity);

//        //        var authProperties = new AuthenticationProperties
//        //        {
//        //            IsPersistent = true // Oturumu kalıcı yapmak için
//        //        };

//        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

//        //        return RedirectToAction("Index", "Home"); // Başarılı oturum açma işleminden sonra yönlendirme yapabilirsiniz
//        //    }

//        //    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
//        //    return View("Index");
//        //}

//        //private bool IsValidUser(string username, string password)
//        //{
//        //    // Kullanıcı doğrulama mantığını burada uygulayın
//        //    // Örneğin, veritabanından kullanıcıyı kontrol edebilirsiniz

//        //    // Örnek olarak basit bir kontrol:
//        //    return username == "demo" && password == "demo123";
//        //}

//        public IActionResult Kayit()
//        {
//            return View();
//        }
//    }
//}















//YÖNTEM2

//    using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using LMSWeb.Models.ViewModels;

//namespace LMSWeb.Controllers
//{
//    public class AccountController : Controller
//    {
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Index(IndexRequest model)
//        {
//            if (ModelState.IsValid)
//            {
//                var name = model.Name;
//                var password = model.Password;

//                // Kullanıcı doğrulama mantığını burada uygulayabilirsiniz.
//                // Örneğin, veritabanından kullanıcıyı kontrol edebilirsiniz.

//                if (IsValidUser(name, password))
//                {
//                    // Başarılı oturum açma işleminden sonra yönlendirme yapabilirsiniz
//                    return RedirectToAction("Index", "Home");
//                }

//                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
//            }

//            return View("Index");
//        }

//        private bool IsValidUser(string username, string password)
//        {
//            // Kullanıcı doğrulama mantığını burada uygulayın
//            // Örneğin, veritabanından kullanıcıyı kontrol edebilirsiniz

//            // Örnek olarak basit bir kontrol:
//            return username == "demo" && password == "demo123";
//        }

//        public IActionResult Kayit()
//        {
//            return View();
//        }
//    }
//}


//YÖNTEM2.1
//    using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using LMSWeb.Models.ViewModels;

//namespace LMSWeb.Controllers
//{
//    public class AccountController : Controller
//    {
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ActionName("Index")]
//        public IActionResult IndexPost(IndexRequest model)
//        {
//            if (ModelState.IsValid)
//            {
//                var name = model.Name;
//                var password = model.Password;

//                // Kullanıcı doğrulama mantığını burada uygulayabilirsiniz.
//                // Örneğin, veritabanından kullanıcıyı kontrol edebilirsiniz.

//                if (IsValidUser(name, password))
//                {
//                    // Başarılı oturum açma işleminden sonra yönlendirme yapabilirsiniz
//                    return RedirectToAction("Index", "Home");
//                }

//                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
//            }

//            return View("Index");
//        }

//        private bool IsValidUser(string username, string password)
//        {
//            // Kullanıcı doğrulama mantığını burada uygulayın
//            // Örneğin, veritabanından kullanıcıyı kontrol edebilirsiniz

//            // Örnek olarak basit bir kontrol:
//            return username == "demo" && password == "demo123";
//        }

//        public IActionResult Kayit()
//        {
//            return View();
//        }
//    }
//}