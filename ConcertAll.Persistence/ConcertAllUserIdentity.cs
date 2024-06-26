using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConcertAll.Persistence
{
    public class ConcertAllUserIdentity : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; } = default!;
        [StringLength(100)]
        public string LastName { get; set; } = default!;
        public int Age { get; set; } = default!;
        public DocumentTypeEnum DocumentType { get; set; }
        [StringLength(20)]
        public string DocumentNumber { get; set; } = default!;
    }

    public enum DocumentTypeEnum : short 
    { 
        Dni,
        Passport
    }
}
