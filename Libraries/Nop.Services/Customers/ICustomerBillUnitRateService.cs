﻿using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Customers
{
    public partial interface ICustomerBillUnitRateService
    {
        CustomerBillUnitRate GetBillingRateInformation();
    }
}
