﻿using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService (SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll ()
        {
            return _context.Seller.ToList();
        }

        public void Insert (Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById (int id)
        {
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);
        }
        
        public void Remove (int id)
        {
            var Seller = _context.Seller.Find(id);
            _context.Seller.Remove(Seller);
            _context.SaveChanges();
        }
    }
}
