﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Customer
{
    public class CustomerMetersModel
    {
        public long meterId { get; set; }
        public string customerName { get; set; }
        public string location { get; set; }
    }
}