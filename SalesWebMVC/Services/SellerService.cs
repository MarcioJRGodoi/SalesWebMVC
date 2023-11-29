using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAll()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task Insert(Seller seller)
        {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindById(int id)
        {
            return await _context.Seller.Include(dp => dp.Department).FirstOrDefaultAsync(sl => sl.Id == id);
        }

        public async Task Delete(int id)
        {
            try 
            {
                var seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
            

        }

        public async Task Update(Seller seller)
        {
            if (!await _context.Seller.AnyAsync(sl => sl.Id == seller.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConcirrencyException(ex.Message);
            }
        }
    }
}
