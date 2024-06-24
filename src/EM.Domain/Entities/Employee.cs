using EM.Domain.Common;

namespace EM.Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public string Name { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = default!;
    }
}