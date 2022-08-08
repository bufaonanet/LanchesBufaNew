using LanchesBufaNew.Models;

namespace LanchesBufaNew.Repositories.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> Categorias { get; }
}