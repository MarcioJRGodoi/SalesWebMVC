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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public async void Insert(Seller seller)
        {
            await _context.Seller.AddAsync(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(dp => dp.Department).FirstOrDefault(sl => sl.Id == id);
        }

        public void Delete(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();

        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(sl => sl.Id == seller.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }catch(DbUpdateConcurrencyException ex)
            {
                throw new DbConcirrencyException(ex.Message);
            }
        }
    }
}
