using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class User
    {
        public int Id { get; set; }

        public string? name { get; set; }

        [Required]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string? password { get; set; }

        [Range(1, 5, ErrorMessage = "Valor de level deve estar entre 1 e 5.")]
        public int level { get; set; }
    }
}
