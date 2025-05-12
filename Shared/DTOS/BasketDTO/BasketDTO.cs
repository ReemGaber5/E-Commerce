using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDTO
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTTO> Items { get; set; } = [];

    }
}
