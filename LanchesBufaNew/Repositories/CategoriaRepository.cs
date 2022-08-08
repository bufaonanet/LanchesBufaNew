﻿using LanchesBufaNew.Context;
using LanchesBufaNew.Models;
using LanchesBufaNew.Repositories.Interfaces;

namespace LanchesBufaNew.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Categorias => _context.Categorias;
}