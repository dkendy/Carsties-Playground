using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using SearchService.Models;

namespace SearchService.Data;

public class SearchDbContext: DbContext
{
    
    public DbSet<ItemEFcore> Itens { get; set; }

    public SearchDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating( ModelBuilder modelBuilder){
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ItemEFcore>().ToCollection("Itens");

    }
 
}
