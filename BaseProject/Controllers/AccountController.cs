using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    /// <summary>
    /// 登陆controller
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser<long>> _userManager;
        private readonly SignInManager<IdentityUser<long>> _signInManager;
        public AccountController(
            UserManager<IdentityUser<long>> userManager,
            SignInManager<IdentityUser<long>> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 提交注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password)
        {
            IdentityUser<long> user = new IdentityUser<long>(userName);
            IdentityResult identityResult = await _userManager.CreateAsync(user, password);
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home", new { });
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            await _signInManager.PasswordSignInAsync(userName, password, false, false);
            return RedirectToAction("Index", "Home", new { });
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            return View();
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { });
        }
    }
}
