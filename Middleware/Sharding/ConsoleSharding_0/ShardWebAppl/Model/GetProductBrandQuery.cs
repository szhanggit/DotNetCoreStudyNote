using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShardWebAppl.Model
{
    public class GetProductBrandQuery
    {
        [Required(ErrorMessage = "ProductId parameter required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ProductId { get; set; }
    }
}
