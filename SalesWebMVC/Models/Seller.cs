using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatorio")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ser entra {2} ate {1} caracteres")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Insira um email valido")]
        [Required(ErrorMessage = "{0} é obrigatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatorio")]
        [Display(Name = "Data de Aniversario")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Range(100.0, 50000, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        [Required(ErrorMessage = "{0} é obrigatorio")]
        [Display(Name = "Salario base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public Department? Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        public int DepartmentId { get; set; }

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department? department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {

            return Sales.Where(x => x.Date >= initial && x.Date >= final)
                .Sum(x => x.Amount);
        }
    }
}
