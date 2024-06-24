using EM.Domain.Common;

namespace EM.Domain.Entities
{
    public class Department : AuditableEntity
    {
        public string Name { get; set; } = default!;
    }
}