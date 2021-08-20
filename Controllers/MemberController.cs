using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        

    }
}