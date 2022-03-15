using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUserContext _userContext;

        public TarefasController(TarefaSiteEFContext context, ITarefaRepositorio tarefaRepositorio, ICategoriaRepositorio categoriaRepositorio, IUsuarioRepositorio usuarioRepositorio,IUserContext userContext)
        {
            _context = context;
            _tarefaRepositorio = tarefaRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _userContext = userContext;
        }
        
        [HttpGet]
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
                    tarefasViewModels.Add(tarefaViewModel);
                }

                return View(tarefasViewModels);
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
            
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            return BuscarTarefa(id);
        }
        
        [HttpGet]
        public IActionResult Nova()
        {
            try
            {
                TarefaViewModel tarefaViewModel = new TarefaViewModel();
                tarefaViewModel.Categorias = BuscarCategorias();
                tarefaViewModel.Data = DateTime.Now;
                tarefaViewModel.Hora = DateTime.Now;

                return View(tarefaViewModel);
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Nova(TarefaViewModel tarefaViewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    DateTime dataHora = new DateTime(tarefaViewModel.Data.Year, tarefaViewModel.Data.Month, tarefaViewModel.Data.Day, tarefaViewModel.Hora.Hour, tarefaViewModel.Hora.Minute, 0);

                    string email = _userContext.GetUserEmail();
                    Usuario usuario = _usuarioRepositorio.Buscar(email);

                    Tarefa tarefa = new Tarefa(tarefaViewModel.Id, tarefaViewModel.Descricao, dataHora, tarefaViewModel.Notificacao, tarefaViewModel.IdCategoria.Value, usuario);
                    _tarefaRepositorio.Inserir(tarefa);

                    return RedirectToAction("Inserir");
                }
                else
                {
                    ViewBag.ExisteErro = true;
                    tarefaViewModel.Categorias = BuscarCategorias();

                    return View(tarefaViewModel);
                }
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        
        [HttpGet]
        public IActionResult Inserir()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            return BuscarTarefa(id);
        }
        
        [HttpPost]
        public IActionResult Editar(TarefaViewModel tarefaViewModel)
        {
            try
            {
                DateTime dataHora = new DateTime(tarefaViewModel.Data.Year, tarefaViewModel.Data.Month, tarefaViewModel.Data.Day, tarefaViewModel.Hora.Hour, tarefaViewModel.Hora.Minute, 0);

                string email = _userContext.GetUserEmail();
                Usuario usuario = _usuarioRepositorio.Buscar(email);
                
                Tarefa tarefa = new Tarefa(tarefaViewModel.Id, tarefaViewModel.Descricao, dataHora, tarefaViewModel.Notificacao, tarefaViewModel.IdCategoria.Value, usuario);
                _tarefaRepositorio.Atualizar(tarefa, email);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }
        
        public IActionResult Deletar(int id)
        {
            try
            {
                string email = _userContext.GetUserEmail();
                _tarefaRepositorio.Excluir(id, email);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        
        public IActionResult Detalhes(int id)
        {
            return BuscarTarefa(id);
        }
        
        private IActionResult BuscarTarefa(int id)
        {
            try
            {
                string email = _userContext.GetUserEmail();

                Tarefa tarefa = _tarefaRepositorio.Buscar(id, email);

                TarefaViewModel tarefaViewModel = new TarefaViewModel();
                tarefaViewModel.Categorias = BuscarCategorias();

                if(tarefa == null)
                {
                    ViewBag.ExisteErro = true;
                    this.ModelState.AddModelError("Tarefa_Nao_Encontrada", "Tarefa não encontrada");
                    return View(tarefaViewModel);
                }

                tarefaViewModel.Id = tarefa.Id;
                tarefaViewModel.Descricao = tarefa.Descricao;
                tarefaViewModel.Data = tarefa.Data;
                tarefaViewModel.Hora = tarefa.Data;
                tarefaViewModel.Notificacao = tarefa.Notificacao;
                tarefaViewModel.IdCategoria = tarefa.IdCategoria;
                tarefaViewModel.Categorias = BuscarCategorias();

                return View(tarefaViewModel);
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
       
        private List<SelectListItem> BuscarCategorias()
        {
            List<Categoria> categorias = _categoriaRepositorio.Buscar();

            List<SelectListItem> listItems = new List<SelectListItem>();

            for(int i = 0; i < categorias.Count; i++)
            {
                Categoria categoria = categorias[i];
                SelectListItem item = new SelectListItem(categoria.Descricao, categoria.Id.ToString());
                listItems.Add(item);
            }

            return listItems;
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
