using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Customers
{
    public class CustomerProductStatusModel : BaseNopModel
    {
        public Guid DeviceId { get; set; }
        public bool Status { get; set; }
    }
}