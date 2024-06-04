﻿using CKK.Logic.Models;
using System.Collections;

namespace CKK.DB.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetByName(string name);
        List<Product> SortByAsc(string sortValue);
        List<Product> SortByDesc(string sortValue);
        public List<Product> SearchFor(string searchTerm);
    }
}
