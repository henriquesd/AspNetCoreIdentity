using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using static AspNetCoreIdentity.Extensions.CustomAuthorization;
using AspNetCoreIdentity.Extensions;
using System;

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
            throw new Exception("Erro");

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

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimGravar()
        {
            return View("Secret");
        }

        [ClaimsAuthorize("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
