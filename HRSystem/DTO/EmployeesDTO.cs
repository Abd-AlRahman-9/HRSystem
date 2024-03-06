using HRDomain.Entities;

namespace HRSystem.DTO
{
    public class EmployeesDTO
    {
        public string EmployeeName { get; set; }
        public string Manager {  get; set; }
        public string Department { get; set; }
        public string NationalID { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public long Phone { get; set; }
        public int VacationsCredit { get; set; }
        public decimal Salary { get; set; }
        public DateOnly HiringDate { get; set; }
    }
}
