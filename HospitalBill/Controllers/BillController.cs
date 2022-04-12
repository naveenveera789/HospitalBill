using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalBill.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        IBillBL billBL;
        public BillController(IBillBL billBL)
        {
            this.billBL = billBL;
        }

        [HttpPost]
        public ActionResult AddPatientDetails(BillModel billModel)
        {
            try
            {
                this.billBL.AddPatientDetails(billModel);
                return this.Ok(new { success = true, message = $"Bill No : {billModel.BillNo} added successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult GetBillData(int? BillNo)
        {
            try
            {
                if (BillNo == null)
                {
                    return this.BadRequest(new { success = false, message = "Enter the Bill No" });
                }
                var result = this.billBL.GetBillData(BillNo);
                return this.Ok(new { success = true, message = $"The table data for BillNo {result.BillNo} is ", response = result });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult GetAllBillData()
        {
            try
            {
                List<BillModel> billModels = new List<BillModel>();
                billModels = this.billBL.GetAllBillData().ToList();
                if (billModels != null)
                {
                    return this.Ok(new { success = true, message = $"All Bills list : ", response = billModels });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = $"There are no bills in the list" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        public ActionResult UpdatePatientDetails(BillModel billModel)
        {
            try
            {
                this.billBL.UpdatePatientDetails(billModel);
                return this.Ok(new { success = true, message = $"Bill {billModel.BillNo} updated succesfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete]
        public ActionResult DeletePatientDetails(int? BillNo)
        {
            try
            {
                if (BillNo == null)
                {
                    return this.BadRequest(new { success = false, message = "Enter the Bill No" });
                }
                var result = this.billBL.GetBillData(BillNo);
                if (result != null)
                {
                    this.billBL.DeletePatientDetails(result);
                    return this.Ok(new { success = true, message = $"Bill {result.BillNo} deleted succesfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = $"Bill {result.BillNo} not deleted" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
