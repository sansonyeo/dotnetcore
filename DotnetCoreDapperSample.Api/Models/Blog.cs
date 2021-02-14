using System.ComponentModel.DataAnnotations;

namespace DotnetCoreDapperSample.Api.Models
{
    public class Blog
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
