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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Register()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Login()
        {
            return View();
        }
        /// <summary>
        /// 提交注册
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SubmitRegister(string userName, string password)
        {
            IdentityUser user = new IdentityUser(userName);
            IdentityResult identityResult = await _userManager.CreateAsync(user, password);
            await _signInManager.SignInAsync(user, false);
            return Redirect("/Home/Index");
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
    }
}
