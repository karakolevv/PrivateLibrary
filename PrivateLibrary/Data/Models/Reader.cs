using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateLibrary.Data.Models
{
    public class Reader
    {
        [Key]
        public string Id { get; set; } = default!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;
    }
}
