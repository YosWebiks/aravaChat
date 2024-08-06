using aravaChat.DAL;
using aravaChat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace aravaChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            if (ValidateRequest(Request.Cookies))
            {
                return RedirectToAction("Chat");
            }

            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            Data.Get.Users.Add(user);
            Data.Get.SaveChanges();

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            if (ValidateRequest(Request.Cookies))
            {
                return RedirectToAction("Chat");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(User userFromReq)
        {
            User? user = Data.Get.Users
                .FirstOrDefault(u => u.user_name == userFromReq.user_name && u.password == userFromReq.password);
            if (user == null)
            {
                return Unauthorized();
            }
            Response.Cookies.Append("user_name", user.user_name);
            Response.Cookies.Append("password", user.password);

            return RedirectToAction(nameof(Chat));
        }
        public IActionResult Chat()
        {
            if (!ValidateRequest(Request.Cookies))
            {
                return RedirectToAction("Login");
            }

            return View(Data.Get.Messages.ToList());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Chat(Message message)
        {
            if (!ValidateRequest(Request.Cookies))
            {
                return RedirectToAction("Login");
            }
            return View(Data.Get.Messages.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public bool ValidateRequest(IRequestCookieCollection cookies)
        {
            string? user_name = cookies["user_name"];
            string? password = cookies["password"];

            if (user_name == null || password == null)
            {
                return false;
            }

            User? user = Data.Get.Users.FirstOrDefault(u => u.user_name == user_name && u.password == password);

            return user != null;
        }
    }

}
