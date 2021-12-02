using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TMS.Models;

namespace TMS.Abstract
{
    public interface IApplicationRepository
    {
        User CheckLogin(string username, string password);

        List<vw_vehicle_master> vw_Vehicle_Masters();
        List<vw_vehicle_owner> vw_Vehicle_Owners();
        int SaveOwner(VehicleOwner vo);
        bool DeleteOwner(int id);
        VehicleOwner VehicleOwner(string id);
        int SaveVehicle(VehicleMasterModel vm);
        bool DeleteVehicle(int id);
        VehicleMaster VehicleMaster(string id);
        List<SelectListItem> ownerlist();
        List<SelectListItem> vehiclelist(int id);
        List<SelectListItem> challanlist();
        int SaveChallan(ChallanModel challanModel);
        int SaveUploadedChallan(List<ChallanModel> challanModels);
        Chalan_Detail Chalan_Detail(int id);
        List<vw_challan_detail> vw_Challan_Details();
        bool DeleteChallan(int id);
        string InvNumber();
        string ChalanNumber();
        Chalan_Detail BindChalanDetail(string id);
        List<SelectListItem> GetPAN();
        List<SelectListItem> GetOwner();
        bool UpdateChallanDetail(List<ChallanUpdateModel> challanUpdateModels);
        bool makepayment(List<PaymentModel> data);
    }
}
