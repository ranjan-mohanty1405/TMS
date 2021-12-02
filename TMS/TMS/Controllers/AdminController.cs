using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Models;
using System.Data;
using System.Data.Entity;
using TMS.Abstract;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace EmployeeManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private readonly IApplicationRepository applicationRepository;

        public AdminController(IApplicationRepository appRepository)
        {
            applicationRepository = appRepository;
        }
        public ActionResult dashboard()
        {
            try
            {
                if (Session["username"] != null )
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }

        #region-------------------------Master------------------------------
        [HttpGet]
        public ActionResult ownermaster(string id)
        {
            try
            {
                if (Session["username"] != null)
                {
                    if (id != null && id != "")
                    {
                        var data = applicationRepository.VehicleOwner(id);
                        ViewBag.data = applicationRepository.vw_Vehicle_Owners();
                        return View(data);
                    }
                    else
                    {
                        ViewBag.data = applicationRepository.vw_Vehicle_Owners();
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult ownermaster(VehicleOwner vo)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int data = applicationRepository.SaveOwner(vo);
                    if(data==1)
                    {
                        TempData["Message"] = "Owner detail added successfully";
                        return RedirectToAction("ownermaster", "Admin");
                    }
                    else if(data==2)
                    {
                        TempData["Message"] = "Owner detail updated successfully";
                        return RedirectToAction("ownermaster", "Admin");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "PAN name is already in use for another user";
                        return RedirectToAction("ownermaster", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }      
        [HttpPost]
        public ActionResult deleteowner(int id)
        {
            var data = applicationRepository.DeleteOwner(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult vehiclemaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult vehiclemaster(string id)
        {
            VehicleMasterModel vm = new VehicleMasterModel();
            try
            {
                if (Session["username"] != null)
                {
                    if (id != null && id != "")
                    {
                        var data = applicationRepository.VehicleMaster(id);
                        vm.VehicleId = data.VehicleId;
                        vm.VehicleNo = data.VehicleNo;
                        vm.OwnerId = data.OwnerId;
                        vm.OwnerList = applicationRepository.ownerlist();
                        ViewBag.data = applicationRepository.vw_Vehicle_Masters();
                        return View(vm);
                    }
                    else
                    {
                        vm.OwnerList = applicationRepository.ownerlist();

                        ViewBag.data = applicationRepository.vw_Vehicle_Masters();
                        return View(vm);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult vehiclemaster(VehicleMasterModel vm)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int data = applicationRepository.SaveVehicle(vm);
                    if (data == 1)
                    {
                        TempData["Message"] = "Vehicle detail added successfully";
                        return RedirectToAction("vehiclemaster", "Admin");
                    }
                    else if(data == 2)
                    {
                        TempData["Message"] = "Vehicle detail updated successfully";
                        return RedirectToAction("vehiclemaster", "Admin");
                    }     
                    else
                    {
                        TempData["WarningMessage"] = "Vehicle no already in use";
                        return RedirectToAction("vehiclemaster", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult deletevehicle(int id)
        {
            var data = applicationRepository.DeleteVehicle(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region------------------------Transaction--------------------------
        [HttpGet]
        public ActionResult challan(int? id)
        {
            ChallanModel cd = new ChallanModel();
            try
            {
                if (Session["username"] != null)
                {
                    if (id != null && id != 0)
                    {
                        int chid = Convert.ToInt32(id);
                        var cm = applicationRepository.Chalan_Detail(chid);
                        cd.ChalanId = chid;
                        cd.Inv_Number = cm.Inv_Number;
                        cd.Challan_No = cm.Challan_No;
                        cd.Chalan_Date = cm.Chalan_Date;
                        cd.TP_Number = cm.TP_Number;
                        cd.Truck_Number = cm.Truck_Number;
                        cd.Load_Quantity = cm.Load_Quantity;
                        cd.UL_Quantity = cm.UL_Quantity;
                        cd.Rate = cm.Rate;
                        cd.Off_Expenses = cm.Off_Expenses;
                        cd.Cash_Adv = cm.Cash_Adv;
                        cd.Bank_Adv = cm.Bank_Adv;
                        cd.HSD_Adv = cm.HSD_Adv;
                        cd.Slip_No = cm.Slip_No;
                        cd.Petrol_Pump_Name = cm.Petrol_Pump_Name;
                        cd.Driver_Welfare = cm.Driver_Welfare;
                        cd.Status = cm.Status;
                        cd.Consignor = cm.Consignor;
                        cd.Consignee = cm.Consignee;
                        cd.Billing_Party = cm.Billing_Party;
                        cd.Loading_Point = cm.Loading_Point;
                        cd.Unloading_Point = cm.Unloading_Point;
                        cd.Truck_Owner_Name = cm.Truck_Owner_Name;
                        cd.Address = cm.Address;
                        cd.Driver_Name = cm.Driver_Name;
                        cd.DL_Number = cm.DL_Number;
                        cd.Contact_No = cm.Contact_No;
                        cd.Ref_Number = cm.Ref_Number;
                        cd.Receive_Date = cm.Receive_Date;
                        cd.Truck_Owner_Name = cm.Truck_Owner_Name;
                        //cd.OwnerList = applicationRepository.ownerlist();
                        //int ownerid = Convert.ToInt32(cm.Truck_Owner_Name);
                        //cd.VehicleList = applicationRepository.vehiclelist(ownerid);
                        return View("challan",cd);
                    }
                    else
                    {
                        cd.Challan_No = applicationRepository.ChalanNumber();
                        cd.Inv_Number = applicationRepository.InvNumber();
                        //cd.OwnerList = applicationRepository.ownerlist();
                        //cd.VehicleList = applicationRepository.vehiclelist(0);
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult UpdateChallan(List<ChallanUpdateModel> Arraylist)
        {
            var data = applicationRepository.UpdateChallanDetail(Arraylist);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetTPNumber(string prefix)
        {
            List<SelectListItem> Contact = applicationRepository.challanlist();
            var challanlist = (from c in Contact
                                  where c.Text.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase)
                                  select new
                                  {
                                      label = c.Text,
                                      val = c.Value
                                  }).ToList();
            return Json(challanlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPAN(string prefix)
        {
            List<SelectListItem> Contact = applicationRepository.GetPAN();
            var challanlist = (from c in Contact
                               where c.Text.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase)
                               select new
                               {
                                   label = c.Text,
                                   val = c.Value
                               }).ToList();
            return Json(challanlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetOwner(string prefix)
        {
            List<SelectListItem> Contact = applicationRepository.GetOwner();
            var challanlist = (from c in Contact
                               where c.Text.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase)
                               select new
                               {
                                   label = c.Text,
                                   val = c.Value
                               }).ToList();
            return Json(challanlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult challan(ChallanModel cm)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int data = applicationRepository.SaveChallan(cm);
                    if (data == 2)
                    {
                        TempData["Message"] = "Challan detail update successfully";
                        return RedirectToAction("challan", "Admin");
                    }
                    else
                    {
                        TempData["Message"] = "Challan detail is not valid";
                        return RedirectToAction("challan", "Admin");
                    }                   
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult UploadChallanList(FormCollection formCollection)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
                {
                    var resultlist = new List<ChallanModel>();
                    if (Request != null)
                    {
                        HttpPostedFileBase file = Request.Files["importFile"];
                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 1].Value.ToString() != "")
                                    {
                                        var challan = new ChallanModel();
                                        challan.TP_Number = workSheet.Cells[rowIterator, 1].Value.ToString();
                                        challan.Truck_Number = workSheet.Cells[rowIterator, 2].Value.ToString();
                                        challan.UL_Quantity = Convert.ToDecimal(workSheet.Cells[rowIterator, 3].Value.ToString());
                                        challan.Load_Quantity = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value.ToString());
                                        challan.Rate = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value.ToString());
                                        
                                        resultlist.Add(challan);
                                    }
                                }
                            }
                        }
                    }
                    var result = applicationRepository.SaveUploadedChallan(resultlist);
                    if (result == 1)
                    {
                        TempData["Message"] = "Challan list uploaded successfully";

                        return RedirectToAction("challanlist", "Admin");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "Something went wrong";

                        return RedirectToAction("challanlist", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WaringMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult BindVehicle(int id)
        {
            var owner = applicationRepository.vw_Vehicle_Owners().Where(x => x.OwnerId == id).FirstOrDefault();
            var data = applicationRepository.vehiclelist(id);
            if (owner != null && data.ToList().Count > 0)
            {
                return Json(new { data, owner.Adress }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult challanlist()
        {
            ChallanSearchModel mdl = new ChallanSearchModel();           
            
            try
            {
                if (Session["username"] != null)
                {
                    ViewBag.data = applicationRepository.vw_Challan_Details().Where(x=>x.Bank_Adv==0&&x.Cash_Adv==0&&x.HSD_Adv==0&&x.Off_Expenses==0).ToList();
                    return View(mdl);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult challanlist(ChallanSearchModel mdl)
        {
            try
            {
                if (Session["username"] != null)
                {
                    var data=applicationRepository.vw_Challan_Details();
                    if (mdl.fromdate != null && mdl.todate != null)
                    {
                        DateTime fromdate = Convert.ToDateTime(mdl.fromdate);
                        DateTime todate = Convert.ToDateTime(mdl.todate);

                        data = data.Where(x=>x.Receive_Date>=fromdate&&x.Receive_Date<=todate).ToList();
                    }
                    if (mdl.cash == "0")
                    {
                        decimal cash = Convert.ToDecimal(mdl.cash);
                        data = data.Where(x => x.Cash_Adv== cash).ToList();
                    }
                    if (mdl.bank == "0")
                    {
                        decimal bank = Convert.ToDecimal(mdl.bank);
                        data = data.Where(x => x.Bank_Adv == bank).ToList();
                    }
                    if (mdl.hsd == "0")
                    {
                        decimal hsd = Convert.ToDecimal(mdl.hsd);
                        data = data.Where(x => x.HSD_Adv == hsd).ToList();
                    }
                    if (mdl.ofc == "0")
                    {
                        decimal ofc = Convert.ToDecimal(mdl.ofc);
                        data = data.Where(x => x.Off_Expenses == ofc).ToList();
                    }
                    ViewBag.data = data;
                    return View(mdl);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult deletechallan(int id)
        {
            var data = applicationRepository.DeleteChallan(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BindChalanDetail(string id)
        {
            ChallanModel cd = new ChallanModel();
            var cm = applicationRepository.BindChalanDetail(id);


            cd.ChalanId = cm.ChalanId;
            cd.Inv_Number = cm.Inv_Number;
            cd.Challan_No = cm.Challan_No;
            cd.Chalan_Date = cm.Chalan_Date;
            cd.TP_Number = cm.TP_Number;
            cd.Truck_Number = cm.Truck_Number;
            cd.Load_Quantity = cm.Load_Quantity;
            cd.UL_Quantity = cm.UL_Quantity;
            cd.Rate = cm.Rate;
            cd.Off_Expenses = cm.Off_Expenses;
            cd.Cash_Adv = cm.Cash_Adv;
            cd.Bank_Adv = cm.Bank_Adv;
            cd.HSD_Adv = cm.HSD_Adv;
            cd.Slip_No = cm.Slip_No;
            cd.Petrol_Pump_Name = cm.Petrol_Pump_Name;
            cd.Driver_Welfare = cm.Driver_Welfare;
            cd.Status = cm.Status;
            cd.Consignor = cm.Consignor;
            cd.Consignee = cm.Consignee;
            cd.Billing_Party = cm.Billing_Party;
            cd.Loading_Point = cm.Loading_Point;
            cd.Unloading_Point = cm.Unloading_Point;
            cd.Truck_Owner_Name = cm.Truck_Owner_Name;
            cd.Address = cm.Address;
            cd.Driver_Name = cm.Driver_Name;
            cd.DL_Number = cm.DL_Number;
            cd.Contact_No = cm.Contact_No;
            cd.Ref_Number = cm.Ref_Number;
            cd.Receive_Date = cm.Receive_Date;
            cd.Truck_Owner_Name = cm.Truck_Owner_Name;

            return PartialView("_challanPartial", cd);

        }
        [HttpPost]
        public ActionResult BindChalanDetailss(string id)
        {
            ChallanModel cd = new ChallanModel();
            var cm = applicationRepository.BindChalanDetail(id);

            cd.ChalanId = cm.ChalanId;
            cd.Inv_Number = cm.Inv_Number;
            cd.Challan_No = cm.Challan_No;
            cd.Chalan_Date = cm.Chalan_Date;
            cd.TP_Number = cm.TP_Number;
            cd.Truck_Number = cm.Truck_Number;
            cd.Load_Quantity = cm.Load_Quantity;
            cd.UL_Quantity = cm.UL_Quantity;
            cd.Rate = cm.Rate;
            cd.Off_Expenses = cm.Off_Expenses;
            cd.Cash_Adv = cm.Cash_Adv;
            cd.Bank_Adv = cm.Bank_Adv;
            cd.HSD_Adv = cm.HSD_Adv;
            cd.Slip_No = cm.Slip_No;
            cd.Petrol_Pump_Name = cm.Petrol_Pump_Name;
            cd.Driver_Welfare = cm.Driver_Welfare;
            cd.Status = cm.Status;
            cd.Consignor = cm.Consignor;
            cd.Consignee = cm.Consignee;
            cd.Billing_Party = cm.Billing_Party;
            cd.Loading_Point = cm.Loading_Point;
            cd.Unloading_Point = cm.Unloading_Point;
            cd.Truck_Owner_Name = cm.Truck_Owner_Name;
            cd.Address = cm.Address;
            cd.Driver_Name = cm.Driver_Name;
            cd.DL_Number = cm.DL_Number;
            cd.Contact_No = cm.Contact_No;
            cd.Ref_Number = cm.Ref_Number;
            cd.Receive_Date = cm.Receive_Date;
            cd.Truck_Owner_Name = cm.Truck_Owner_Name;

            return PartialView("_paymentPartial", cd);

        }
        [HttpGet]
        public ActionResult payment()
        {
            try
            {
                if (Session["username"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult payment(ChallanSearchModel mdl)
        {
            try
            {
                if (Session["username"] != null)
                {
                    var data = applicationRepository.vw_Challan_Details();
                    if (mdl.fromdate != null && mdl.todate != null&&mdl.owner != "" &&mdl.pan!="")
                    {
                        DateTime fromdate = Convert.ToDateTime(mdl.fromdate);
                        DateTime todate = Convert.ToDateTime(mdl.todate);

                        data = data.Where(x => x.Receive_Date >= fromdate && x.Receive_Date <= todate&&x.Truck_Owner_Name==mdl.owner&&x.Address==mdl.pan&&(x.IsPaid==null||x.IsPaid==false)&&x.Receive_Date!=null).ToList();
                    }
                    else if ((mdl.fromdate == null || mdl.todate == null) && mdl.owner != "" && mdl.pan != "")
                    {
                        DateTime fromdate = Convert.ToDateTime(mdl.fromdate);
                        DateTime todate = Convert.ToDateTime(mdl.todate);

                        data = data.Where(x => x.Truck_Owner_Name == mdl.owner && x.Address == mdl.pan && (x.IsPaid == null || x.IsPaid == false) && x.Receive_Date != null).ToList();
                    }
                    else if (mdl.fromdate == null && mdl.todate == null && mdl.owner != "" && mdl.pan == "")
                    {
                        DateTime fromdate = Convert.ToDateTime(mdl.fromdate);
                        DateTime todate = Convert.ToDateTime(mdl.todate);

                        data = data.Where(x => x.Truck_Owner_Name == mdl.owner && (x.IsPaid == null || x.IsPaid == false) && x.Receive_Date != null).ToList();
                    }

                    ViewBag.data = data;
                    ViewBag.qty = data.Sum(x => x.UL_Quantity);
                    ViewBag.cash = data.Sum(x => x.Cash_Adv);
                    ViewBag.bank = data.Sum(x => x.Bank_Adv);
                    ViewBag.hsd = data.Sum(x => x.HSD_Adv);
                    ViewBag.ofc = data.Sum(x => x.Off_Expenses);
                    ViewBag.tds = data.Sum(x => x.TDS);
                    ViewBag.freight = data.Sum(x => x.Freight);
                    ViewBag.topaid = ViewBag.freight-(ViewBag.cash+ ViewBag.bank+ ViewBag.hsd+ ViewBag.ofc+ ViewBag.tds);
                    return View(mdl);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult makepayment(List<PaymentModel> data)
        {
            return Json(applicationRepository.makepayment(data), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}