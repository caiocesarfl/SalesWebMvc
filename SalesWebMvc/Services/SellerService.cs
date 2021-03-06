﻿using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService (SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync ()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync (Seller seller)
        {
            await _context.AddAsync(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync (int id)
        {
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);
        }
        
        public async Task RemoveAsync (int id)
        {
            try {
                var Seller =  await _context.Seller.FindAsync(id);
                _context.Seller.Remove(Seller);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e)
            {
                throw new IntegrityException ("Can't delete selller because he/she has sales"); 
            }
        }

        public async Task UpdateAsync (Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(item=>item.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException ("Id not found");
            }
            try {
                _context.Update(seller);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException (e.Message);
            }
        }
    }
}
