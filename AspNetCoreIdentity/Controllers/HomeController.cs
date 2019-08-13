using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentity.Controllers
{
    // O "Authorize" implementa a necessidade do usuário estar autenticado na aplicação.
    // (Autenticação significa o usuário ter feito login e ter sido autorizado pela aplicação)
    // Pode por em cada um dos métodos da Controller, ou mover para toda a Controller.
    // Para abrir exceções (que podem ser acessados por anônimos), utilize o "AllowAnonymous";
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        // Autorização significa que além de estar autenticado, o usuário tem que poder fazer aquilo que ele está tentando fazer.
        // Autorização vem um nível depois da autenticação.
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin, Gestor")]
        public IActionResult Secret()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
