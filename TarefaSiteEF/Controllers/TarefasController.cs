using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarefas.Dominio.Models;
using Tarefas.Dominio.Repositorio;
using TarefaSiteEF.Data;
using TarefaSiteEF.HttpContext;
using TarefaSiteEF.ViewModels;

namespace TarefaSiteEF.Controllers
{
    public class TarefasController : Controller
    {
        private readonly TarefaSiteEFContext _context;
        private readonly ITarefaRepositorio _tarefaRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IUserContext _userContext;

        public TarefasController(TarefaSiteEFContext context, ITarefaRepositorio tarefaRepositorio, ICategoriaRepositorio categoriaRepositorio, IUserContext userContext)
        {
            _context = context;
            _tarefaRepositorio = tarefaRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _userContext = userContext;
        }
        
        public IActionResult Index()
        {
            try
            {
                string email = _userContext.GetUserEmail();

                List<Tarefa> tarefas = _tarefaRepositorio.Buscar(email);
                List<Categoria> categorias = _categoriaRepositorio.Buscar();
                List<ListaTarefasViewModel> tarefasViewModels = new List<ListaTarefasViewModel>();

                foreach(var tarefa in tarefas)
                {
                    ListaTarefasViewModel tarefaViewModel = new ListaTarefasViewModel();

                    tarefaViewModel.Id = tarefa.Id;
                    tarefaViewModel.Descricao = tarefa.Descricao;
                    tarefaViewModel.Data = tarefa.Data;
                    tarefaViewModel.Notificacao = tarefa.Notificacao;

                    foreach(var categoria in categorias)
                    {
                        if(categoria.Id == tarefa.IdCategoria)
                        {
                            tarefaViewModel.Descricao = categoria.Descricao;
                        }
                    }

                }

                return View(tarefasViewModels);
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
