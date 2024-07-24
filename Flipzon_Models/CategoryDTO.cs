using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flipzon_Models
{
    public class CategoryDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage="Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please give any description")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
