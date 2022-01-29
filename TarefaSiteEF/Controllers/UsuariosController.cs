using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TarefaSiteEF.Data;
using Tarefas.Dominio.Models;
using TarefaSiteEF.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TarefaSiteEF.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly TarefaSiteEFContext _context;

        public UsuariosController(TarefaSiteEFContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel loginViewModel)
        {
            try
            {
                if(this.ModelState.IsValid)
                {
                    Usuario usuario = await _context.Usuario
                        .FirstOrDefaultAsync(m => m.Email == loginViewModel.Email);
                    
                    if(usuario == null || usuario.Senha != loginViewModel.Senha)
                    {
                        ViewBag.ExisteErro = true;
                        this.ModelState.AddModelError("usuario_senha_invalido", "Usuário ou senha inválidos");
                        return View(loginViewModel);
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim("FullName", usuario.Nome),
                        new Claim(ClaimTypes.Role, "Usuario"),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.Now.AddMinutes(10),
                        IssuedUtc = DateTime.Now,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Tarefas");
                }
                else
                {
                    ViewBag.ExisteErro = true;
                }
                return View(loginViewModel);
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        
        [HttpGet]
        public IActionResult Nova()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Nova(NovoUsuarioViewModel novoUsuarioViewModel)
        {
            try
            {
                if(this.ModelState.IsValid)
                {
                    if(novoUsuarioViewModel.Senha != novoUsuarioViewModel.ConfirmarSenha)
                    {
                        ViewBag.ExisteErro = true;
                        this.ModelState.AddModelError("senha_diferente_confirma_senha", "Senha e confirmar senha devem ser iguais");
                    }

                    Usuario usuario = await _context.Usuario
                        .FirstOrDefaultAsync(m => m.Email == novoUsuarioViewModel.Email);

                    if(usuario != null)
                    {
                        ViewBag.ExisteErro = true;
                        this.ModelState.AddModelError("usuario_Existente", "Email já existente");
                        return View(novoUsuarioViewModel);
                    }

                    Usuario novoUsuario = new Usuario(novoUsuarioViewModel.Nome, novoUsuarioViewModel.Email, novoUsuarioViewModel.Senha);
                    _context.Add(novoUsuario);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ExisteErro = true;
                }
                return View(novoUsuarioViewModel);
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult>Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message
            };
            return View(viewModel);
        }
    }
}
