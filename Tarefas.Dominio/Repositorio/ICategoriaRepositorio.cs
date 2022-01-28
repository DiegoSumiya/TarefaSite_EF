﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Dominio.Models;

namespace Tarefas.Dominio.Repositorio
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> BuscarAsync();
    }
}
