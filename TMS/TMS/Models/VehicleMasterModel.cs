using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Models
{
    public class VehicleMasterModel
    {
        public int? VehicleId { get; set; }
        public int OwnerId { get; set; }
        public string VehicleNo { get; set; }
        public List<SelectListItem> OwnerList { get; set; }
    }
}