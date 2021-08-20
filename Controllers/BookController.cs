using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mvcday1.Applications.Book.Commands.CreateBookCommand;
using Mvcday1.Applications.Book.Commands.EditBookCommand;
using Mvcday1.Applications.Book.Queries.GetBookDetailQuery;
using Mvcday1.Applications.Book.Queries.GetBookEditQuery;
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

        public async Task<IActionResult> Detail(GetBookDetailQuery query)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var model = await _mediator.Send(query);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = await _mediator.Send(new GetCategoryQuery());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _mediator.Send(new GetCategoryQuery());
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(GetBookEditQuery query)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            ViewData["Categories"] = await _mediator.Send(new GetCategoryQuery());
            ViewData["Info"] = await _mediator.Send(query);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] GetBookEditQuery query, EditBookCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _mediator.Send(new GetCategoryQuery());
                ViewData["Info"] = await _mediator.Send(query);
                return View(command);
            }
            await _mediator.Send(command);
            ViewData["Categories"] = await _mediator.Send(new GetCategoryQuery());
            ViewData["Info"] = await _mediator.Send(query);
            return View();
        }
    }
}