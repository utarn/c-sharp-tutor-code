using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvcday1.Applications.Member.Commands;
using Mvcday1.Applications.Member.Commands.UserLoginCommand;
using Mvcday1.Applications.Member.Commands.UserLogoutCommand;
using Mvcday1.Applications.Member.Commands.UserRegisterCommand;
using Mvcday1.Applications.Member.Queries.GetSignInChallengeQuery;

namespace Mvcday1.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMediator _mediator;
        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            try
            {
                await _mediator.Send(command);
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError("Password", e.Message);
                return View(command);
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Login(string? returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            var result = await _mediator.Send(command);
            return RedirectToAction(result.ActionName, result.ControllerName);
        }

        public async Task<IActionResult> Logout(UserLogoutCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public async Task<IActionResult> SocialLogin(GetSignInChallengeQuery query)
        {
            var challenge = await _mediator.Send(query);
            return challenge;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SocialLoginCallback([FromQuery] SocialLoginCommand command)
        {
            var redirect = await _mediator.Send(command);
            if (redirect.PathName != null)
            {
                return Redirect(redirect.PathName);
            }
            else
            {
                return RedirectToAction(redirect.ActionName, redirect.ControllerName);
            }
        }
    }

}