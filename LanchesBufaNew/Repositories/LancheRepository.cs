using LanchesBufaNew.Context;
using LanchesBufaNew.Models;
using LanchesBufaNew.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesBufaNew.Repositories;

public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext _context;

    public LancheRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Lanche> Lanches =>
        _context.Lanches.Include(l => l.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos =>
        _context.Lanches
        .Where(l => l.IsLanchePreferido)
        .Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
    }
}