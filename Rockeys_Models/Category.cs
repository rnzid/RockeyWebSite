using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rockys_Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        /*[DisplayName("Display Order")]*/
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Display order must be greather than zero")]
        public int DisplayOrder { get; set; }
    }
}
