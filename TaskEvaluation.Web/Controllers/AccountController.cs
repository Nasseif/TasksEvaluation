using Castle.Core.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;
using TaskEvaluation.Web.Services;
using IEmailSender = TaskEvaluation.Web.Services.IEmailSender;

namespace TaskEvaluation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IEmailBodyBuilder _emailBodyBuilder;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IEmailBodyBuilder emailBodyBuilder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _emailBodyBuilder = emailBodyBuilder;
        }
        #region Registration
        public IActionResult RegisterCompleted()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDTO registerdto)
        {
            if (!ModelState.IsValid) return View(registerdto);

            var user = await _userManager.FindByEmailAsync(registerdto.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerdto);
            }

            var newUser = new ApplicationUser()
            {
                UserName = registerdto.Email,
                FullName=registerdto.FullName,
                Email = registerdto.Email,
                PhoneNumber = registerdto.MobileNumber
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerdto.Password);

            if (newUserResponse.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return View("RegisterCompleted");
            }
            return View(registerdto);
        }

        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }
        #endregion


        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = model.Email }, protocol: HttpContext.Request.Scheme);

            if (string.IsNullOrEmpty(model.Email))
            {
                // Log the error or handle it appropriately
                ModelState.AddModelError(string.Empty, "Email address is invalid.");
                return View(model);
            }

            var placeHolders = new Dictionary<string, string>()
            {
                    { "imageUrl", "https://res.cloudinary.com/dzqc5nfai/image/upload/v1717798788/qkp9tphwwlqkrmkdukpx.svg" },
                    { "header", $"Hey {user.UserName}, thanks for joining up!" },
                    { "body", "Please confirm your email" },
                    { "url", $"{HtmlEncoder.Default.Encode(callbackUrl!)}" },
                    { "linkTitle", "Active Account" }
            };

            var body = _emailBodyBuilder.GetEmailBody("email", placeHolders);

            // Add logging before sending email
            Console.WriteLine($"Sending email to: {user.Email}");
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException(nameof(user.Email), "User email address cannot be null or empty.");
            }

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", body);

            return RedirectToAction("ForgotPasswordConfirmation");
        }



        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = "Your password has been changed.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync();    
            return RedirectToAction("Login", "Account");
        }
    } 
}