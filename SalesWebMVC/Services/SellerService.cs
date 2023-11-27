using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

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
            seller.Department = _context.Department.FirstOrDefault();
            await _context.Seller.AddAsync(seller);
            _context.SaveChanges();
        }
    }
}
