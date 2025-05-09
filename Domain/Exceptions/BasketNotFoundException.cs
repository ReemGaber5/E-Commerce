using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFoundException(string key):NotFoundException($"Basket with key{key} Is NOT FOUND!")
    {
    }
}
