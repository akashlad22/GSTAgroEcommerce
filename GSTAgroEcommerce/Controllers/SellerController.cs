using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroEcommerceLibrary.Seller;

namespace GSTAgroEcommerce.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        string Code;
        // GET: Seller
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            Country_Bind();
            // CityBind();
            // BALSeller obj = new BALSeller();
            // DateTime DOB = Obj.DOB;
            // obj.SellerReg(Obj.SellerCode, Obj.SellerFullName, Obj.BusinessName, Obj.BusinessPinCode, Obj.EmailId, Obj.Password, Obj.MobileNo, Obj.AlternateMobileNo, Obj.Gender, Obj.CityId, Obj.PinCode, DOB, Obj.RegistrationDate, /*Obj.RejectionReason, Obj.SellerApprovalDate,*/ Obj.StatusId);

            //// obj.SellerRegAdd(Obj.SellerCode, Obj.Home, Obj.Office, Obj.Other);
            //// obj.SellerAddDoc(Obj.SellerCode, Obj.AadharNo, Obj.PanCardNo, Obj.AadharImage
            //// , Obj.PanImage, Obj.BusinessName, Obj.GSTIN, Obj.BusinessPinCode, Obj.BusinessAadharNo, Obj.BusinessPanNo, Obj.BusinessAdharImage, Obj.BusinessPanImage);


            // return RedirectToAction("Registration");
            return View();

        }
        [HttpPost]
        public ActionResult Registration(Seller Obj, HttpPostedFileBase Photo, HttpPostedFileBase AdharImage, HttpPostedFileBase Pancard, HttpPostedFileBase businessAdhar, HttpPostedFileBase BusinessPan)
        {
            Seller_Code();
            Obj.SellerCode = Code;
            DateTime DOB = Obj.DOB;
            Obj.RegistrationDate = DateTime.Now;
            if (Photo != null && Photo.ContentLength > 0)
            {
                string image = Path.GetFileName(Photo.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Content/Images/DocImages"), image);
                Photo.SaveAs(imgpath);

            }
            if (AdharImage != null && AdharImage.ContentLength > 0)
            {
                string adharimage = Path.GetFileName(AdharImage.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Content/Images/DocImages"), adharimage);
                AdharImage.SaveAs(imgpath);
                Obj.AadharImage = adharimage;
            }
            if (Pancard != null && Pancard.ContentLength > 0)
            {
                string panimage = Path.GetFileName(Pancard.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Content/Images/DocImages"), panimage);
                Pancard.SaveAs(imgpath);
                Obj.PanImage = panimage;
            }
            if (businessAdhar != null && businessAdhar.ContentLength > 0)
            {
                string badharimage = Path.GetFileName(businessAdhar.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Content/Images/DocImages"), badharimage);
                businessAdhar.SaveAs(imgpath);
                Obj.BusinessAdharImage = badharimage;
            }
            if (BusinessPan != null && BusinessPan.ContentLength > 0)
            {
                string bPanimag = Path.GetFileName(BusinessPan.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Content/Images/DocImages"), bPanimag);
                BusinessPan.SaveAs(imgpath);
                Obj.BusinessPanImage = bPanimag;
            }

            BALSeller obj = new BALSeller();

            obj.SellerReg(Obj.SellerCode, Obj.SellerFullName, Obj.BusinessName, Obj.BusinessPinCode, Obj.EmailId, Obj.Password, Obj.MobileNo, Obj.AlternateMobileNo, Obj.Gender, Obj.CityId, Obj.PinCode, DOB, Obj.RegistrationDate /*Obj.RejectionReason, Obj.SellerApprovalDate,*/ /*Obj.StatusId*/);
            obj.SellerRegAdd(Obj.SellerCode, Obj.Home, Obj.Office, Obj.Other);
            obj.SellerAddDoc(Obj.SellerCode, Obj.AadharNo, Obj.PanCardNo, Obj.AadharImage
                , Obj.PanImage, Obj.BusinessName, Obj.GSTIN, Obj.BusinessPinCode, Obj.BusinessAadharNo, Obj.BusinessPanNo, Obj.BusinessAdharImage, Obj.BusinessPanImage);
            return RedirectToAction("Registration");
            //return View();
        }
        public JsonResult City_Bind(int State_Id)

        {

            BALSeller obj = new BALSeller();
            DataSet ds = obj.GetCity(State_Id);
            List<SelectListItem> CityList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CityList.Add(new SelectListItem
                {
                    Text = dr["cityName"].ToString(),
                    Value = dr["CityId"].ToString()
                });
            }
            // ViewBag.StateName = StatesList;
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult CityBind()
        //{
        //    BALSeller objuser = new BALSeller();
        //    DataSet ds = new DataSet();
        //    ds = objuser.CityBind();
        //    List<SelectListItem>CityList= new List<SelectListItem>();
        //    foreach(DataRow dr in ds.Tables[0].Rows)
        //    {
        //        CityList.Add(new SelectListItem
        //        {
        //            Text = dr["CityName"].ToString(),
        //            Value = dr["CityId"].ToString()
        //        });
        //    }
        //    ViewBag.CityId = new SelectList(CityList, "Value", "Text");
        //    return View();
        //}
        public void Country_Bind()
        {
            BALSeller obj = new BALSeller();
            DataSet ds = obj.GetCountry();
            List<SelectListItem>
                CountryList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CountryList.Add(new SelectListItem
                {
                    Text = dr["CountryName"].ToString(),
                    Value = dr["CountryId"].ToString()
                });
            }
            ViewBag.Country = CountryList;
        }
        public JsonResult State_Bind(int Country_Id)
        {
            Seller Obj = new Seller();
            BALSeller obj = new BALSeller();
            DataSet ds = obj.GetState(Country_Id);
            List<SelectListItem>
                StatesList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StatesList.Add(new SelectListItem
                {
                    Text = dr["StateName"].ToString(),
                    Value = dr["StateId"].ToString()
                });
            }
            // ViewBag.StateName = StatesList;
            return Json(StatesList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Seller_Code()
        {
            Seller obj = new Seller();
            BALSeller obj1 = new BALSeller();
            SqlDataReader dr;
            dr = obj1.SellerCode();
            while (dr.Read())
            {
                int SellerCode = Convert.ToInt32(dr["Id"].ToString());
                SellerCode = SellerCode + 1;
                String Id = "S0";
                Code = Id + SellerCode;
            }
            dr.Close();
            return View(obj);


        }
    }
}