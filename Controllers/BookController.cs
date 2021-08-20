using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mvcday1.Applications.Book.Commands.CreateBookCommand;
using Mvcday1.Applications.Book.Queries.GetBookQuery;
using Mvcday1.Applications.Book.Queries.GetCategoryQuery;

namespace Mvcday1.Controllers
{
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(GetBookQuery query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = await _mediator.Send(new GetCategoryQuery());
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command) {
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }
    }
}