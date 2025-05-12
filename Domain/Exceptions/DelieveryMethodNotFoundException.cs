using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DelieveryMethodNotFoundException(int Id):NotFoundException($"Method with Id:{Id} Is Not Found!!")
    {
    }
}
