using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LoginWebApp.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using LoginWebApp.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Linq;

namespace LoginWebApp.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;
        public AccountController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string date)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email,model.Password); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email,model.Password); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName,string passWord)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim("dateNow", DateTime.Now.ToString()),
                new Claim("password",passWord)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public IActionResult ShowEvents()
        {
            //var result = DateTime.ParseExact(Test2, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            var userEmail = User.Identity.Name;
            var users = db.Users.ToList();
            var userId = users.Where(x => x.Email == userEmail).ToList().Last();
            var eventItem = db.EventItem.ToList();
            var userEvents = db.UserEvents.ToList();
            var events = from or in userEvents
                         join o in eventItem on or.EventItemId equals o.Id
                         where or.UserId == userId.Id
                         select new ViewModels.EventItem(o.Id, o.Name,o.Start,o.End, o.Status);
            var ordersViewModel = new EventItemModel();
            ordersViewModel.ListEvents = events;
            return View(ordersViewModel);
        }

        [HttpPost]
        public IActionResult AddEvent(EventItemModel model)
        {
            var defaultData = DateTime.Parse("01.01.0001 0:00:00");
            if (String.IsNullOrEmpty(model.Name) || defaultData == model.Start || defaultData == model.End)
            {
                ModelState.AddModelError("", "Некоректно заполнены данные");
                return View("ShowEvents",model);
            }
             else
            {
                db.EventItem.Add(new EventItem { Name = model.Name, Start = model.Start, End = model.End, Status = true });
                db.SaveChanges();
                var idItem = db.EventItem.ToList().Where(x => x.Name == model.Name);
                var userEmail = User.Identity.Name;
                var userId = db.Users.ToList().Where(x => x.Email == userEmail);
                var userEvents = new UserEvents();
                db.UserEvents.Add(new UserEvents { EventItem = idItem.First(), User = userId.First() });
                db.SaveChanges();

                return Redirect("/Account/ShowEvents");
            }   
        }

        [HttpPost]
        public IActionResult UpdateEvent(EventItemModel model)
        {
            var defaultData = DateTime.Parse("01.01.0001 0:00:00");
            if (String.IsNullOrEmpty(model.Name) || defaultData == model.Start || defaultData == model.End)
            {
                ModelState.AddModelError("", "Некоректно заполнены данные");
                return View("ShowEvents", model);
            }
            else
            {
                var eventItem = db.EventItem.Find(model.Id);
                eventItem.Name = model.Name;
                eventItem.Start = model.Start;
                eventItem.End = model.End;
                db.EventItem.Update(eventItem);
                db.SaveChanges();
                return Redirect("/Account/ShowEvents");
            }
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int Id)
        {
            var eventItem = db.EventItem.Find(Id);
            db.EventItem.Remove(eventItem);
            var userEvent = db.UserEvents.ToList().Where(x => x.EventItemId == eventItem.Id).First();
            db.UserEvents.Remove(userEvent);
            db.SaveChanges();
            return Redirect("/Account/ShowEvents");
        }

        [HttpPatch]
        public IActionResult PatchEvent(int Id)
        {
            var eventItem = db.EventItem.Find(Id);
            eventItem.Status = !eventItem.Status;
            db.EventItem.Update(eventItem);
            db.SaveChanges();
            return Redirect("/Account/ShowEvents");
        }
    }
}