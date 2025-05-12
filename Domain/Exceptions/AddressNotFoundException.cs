﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AddressNotFoundException(string username):NotFoundException($"User : {username} Has No Address! ")
    {
    }
}
