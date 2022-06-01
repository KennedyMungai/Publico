using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Publico.Models;

using Publico.Models;
using Publico.Data;

namespace Publico.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    public readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        ViewBag.CurrentUserName = currentUser.UserName;
        var messages = await _context.Messages.ToListAsync();
        return View();
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

    public async Task<IActionResult> Create(Message message)
    {
        if(ModelState.IsValid)
        {
            message.UserName = User.Identity.Name;
            var sender = await _userManager.GetUserAsync(User);
            message.UserID = sender.Id;
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        return Error();
    }
}
