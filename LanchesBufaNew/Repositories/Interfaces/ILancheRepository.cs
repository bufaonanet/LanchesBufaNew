using LanchesBufaNew.Models;

namespace LanchesBufaNew.Repositories.Interfaces;

public interface ILancheRepository
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> LanchesPreferidos { get; }
    Lanche GetLancheById(int lancheId);

}