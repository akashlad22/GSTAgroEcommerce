using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.WebPages;
using AgroEcommerceLibrary.Buyer;
using System.Web.Security;
using System.Threading.Tasks;

namespace GSTAgroEcommerce.Controllers
{
    public class BuyerController : Controller
    {
        string BuyerCode = "B011";
        string Code;
        BALBuyer obj = new BALBuyer();
        SqlConnection con = new SqlConnection("Data Source=AKASH\\SQLEXPRESS;Initial Catalog=GSTAgroE-Commerce;Integrated Security=True");
        // GET: Buyer
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string emailid, string password)
        {
            Buyer obj1 = new Buyer();
            obj1.EmailId = emailid;
            obj1.Password = password;
            SqlDataReader dr;
            dr = obj.LogIn(obj1.EmailId, obj1.Password);
            if (dr.Read())
            {
                FormsAuthentication.SetAuthCookie(emailid, true);
                obj1.BuyerCode = dr["BuyerCode"].ToString();
                obj1.EmailId = dr["EmailId"].ToString();
                obj1.Password = dr["Password"].ToString();
                obj1.BuyerFullName = dr["BuyerFullName"].ToString();

                Session["BuyerCode"] = obj1.BuyerCode.ToString();
                Session["EmailId"] = obj1.EmailId.ToString();
                Session["Password"] = obj1.Password.ToString();
                Session["BuyerFullName"] = obj1.BuyerFullName.ToString();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.LogInErrorMessage = "Incorrect Login Details";
            }


            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
     

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchProducts(string prosearch)
        {
            DataSet ds = new DataSet();
            ds = obj.SearchData(prosearch);
            Buyer objU = new Buyer();
            List<Buyer> productlist = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                productlist.Add(new Buyer
                {
                    ProductName = dr["ProductName"].ToString(),
                    ProductCode = dr["ProductCode"].ToString(),
                    MRP = Convert.ToInt32(dr["MRP"].ToString()),
                    MainImage = dr["MainImage"].ToString(),

                    //ProductId =Convert.ToInt32(dr["ProductId"].ToString()),

                });
                objU.products = productlist;
            }

            return View(objU);
        }
        public ActionResult ShowProdDetails(string productcode)
        {
           Buyer obj1PD = new Buyer();
            obj1PD.ProductCode = productcode;
            DataSet ds = new DataSet();
            ds = obj.ViewProductDetails(obj1PD.ProductCode);
            List<Buyer> prodDetails=new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Buyer objU = new Buyer();
                objU.ProductCode = dr["ProductCode"].ToString();
                objU.ProductName = dr["ProductName"].ToString();
                objU.MainImage = dr["MainImage"].ToString();
                objU.Image1 = dr["Image1"].ToString();
                objU.MRP = Convert.ToInt32(dr["MRP"].ToString());
                objU.Description = dr["Description"].ToString();
                objU.ProductWeight = dr["ProductWeight"].ToString();
                objU.IsproductExpirable = dr["IsproductExpirable"].ToString() == "True";
                objU.IsProductReturnable = dr["IsProductReturnable"].ToString() == "True";
                prodDetails.Add(objU);
            }
            obj1PD.products = prodDetails;
            return View(obj1PD);
        }



        public ActionResult SideBarShop(int? id, string prosearch)
        {
            DataSet ds = new DataSet();
            ds = obj.CategoeryShow();
            Buyer objU = new Buyer();
            List<Buyer> categorylist = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                categorylist.Add(new Buyer
                {
                    CategoryName = dr["CategoryName"].ToString(),
                    CategoryId = Convert.ToInt32(dr["CategoryId"].ToString())

                });
                objU.category = categorylist;

            }
            if (id == null)
            {
                DataSet ds1 = new DataSet();
                ds1 = obj.ShowAllProducts();
                // Buyer objU = new Buyer();

                List<Buyer> prodlist = new List<Buyer>();
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    prodlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),

                    });
                }

                objU.products = prodlist;
            }
            else
            {
                DataSet ds2 = new DataSet();
                ds2 = obj.ShowCatProducts((int)id);
                // Buyer objU = new Buyer();
                List<Buyer> prodlist = new List<Buyer>();
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    prodlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),

                    });
                }

                objU.products = prodlist;
            }
            if (prosearch != null)
            {
                DataSet ds3 = new DataSet();
                ds3 = obj.SearchData(prosearch);
                //Product objU = new Product();
                List<Buyer> productlist = new List<Buyer>();
                foreach (DataRow dr in ds3.Tables[0].Rows)
                {
                    productlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),

                        //ProductId =Convert.ToInt32(dr["ProductId"].ToString()),

                    });
                }
                objU.products = productlist;
            }

            return View(objU);
        }
        //public ActionResult GetMainCategoery()
        //{
        //    DataSet ds = new DataSet();
        //     ds = obj.CategoeryShow();
        //    Buyer objget = new Buyer();
        //    List<Buyer> categorylist = new List<Buyer>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        categorylist.Add(new Buyer
        //        {
        //            CategoryName = dr["CategoryName"].ToString(),
        //            CategoryId = Convert.ToInt32(dr["CategoryId"].ToString())

        //        });
        //        objget.category = categorylist;

        //    }
        //    return View(objget);
        //}
        public ActionResult ShowCategoeryProducts(int id)
        {

            DataSet ds = new DataSet();
            ds = obj.ShowCatProducts(id);
            Buyer objU = new Buyer();
            List<Buyer> prodlist = new List<Buyer>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                prodlist.Add(new Buyer
                {
                    ProductName = dr["ProductName"].ToString(),
                    //ProductCode = dr["ProductCode"].ToString(),
                    MRP = Convert.ToInt32(dr["MRP"].ToString()),
                    MainImage = dr["MainImage"].ToString(),

                });
            }

            objU.CatProd = prodlist;
            return PartialView("_ShowCategoeryProducts", objU);
        }
        public ActionResult Checkout()
        {
            if (Session["BuyerCode"] != null)
            {
                string buyercode = Session["BuyerCode"].ToString();
                // string buyercode = "B004";
                BALBuyer objbal = new BALBuyer();
                DataSet ds = new DataSet();
                ds = objbal.checkout(buyercode);

                List<Buyer> chrckoutlist = new List<Buyer>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    chrckoutlist.Add(new Buyer
                    {
                        ProductName = dr["ProductName"].ToString(),
                        //ProductCode = dr["ProductCode"].ToString(),
                        MRP = Convert.ToInt32(dr["MRP"].ToString()),
                        MainImage = dr["MainImage"].ToString(),
                        EmailId = dr["EmailId"].ToString(),
                        BuyerFullName = dr["BuyerFullName"].ToString(),
                        Home = dr["Home"].ToString(),
                        Office = dr["Office/Business"].ToString(),
                        Other = dr["Other"].ToString(),
                        ProductQuantity = Convert.ToInt32(dr["ProductQuantity"].ToString()),

                    });
                }
                return View(chrckoutlist);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult AddAddress()
        {
            GetCountry();

            return View();
        }
        ///.......... Address binding.................//

        public void GetCountry()
        {
            BALBuyer objB = new BALBuyer();
            DataSet ds = objB.GetCountry();
            List<SelectListItem> countrylist = new List<SelectListItem>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                countrylist.Add(new SelectListItem
                {
                    Text = item["CountryName"].ToString(),
                    Value = item["CountryId"].ToString()

                });

                ViewBag.country = countrylist;
            }


        }
        [HttpGet]
        public JsonResult GetState(int countryid)
        {
            BALBuyer objB = new BALBuyer();
            //User objA = new User();
            DataSet ds = objB.GetState(countryid);
            List<SelectListItem> Statelist = new List<SelectListItem>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Statelist.Add(new SelectListItem
                {
                    Text = item["StateName"].ToString(),
                    Value = item["StateId"].ToString()

                });


            }
            return Json(Statelist, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Get_City(int stateid)
        {
            BALBuyer objB = new BALBuyer();
            //User objA = new User();
            DataSet ds = objB.GetCity(stateid);
            List<SelectListItem> Citylist = new List<SelectListItem>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Citylist.Add(new SelectListItem
                {
                    Text = item["CityName"].ToString(),
                    Value = item["CityId"].ToString()

                });


            }
            return Json(Citylist, JsonRequestBehavior.AllowGet);
        }

        ///GET CATEGORY

        //public void GetMainCategoery() 
        //{
        //    BALBuyer objB = new BALBuyer();
        //    DataSet ds = objB.GetCategory();
        //    List<SelectListItem> categorylist = new List<SelectListItem>();
        //    foreach (DataRow item in ds.Tables[0].Rows)
        //    {
        //        categorylist.Add(new SelectListItem
        //        {
        //            Text = item["CategoryName"].ToString(),
        //            Value = item["CategoryId"].ToString()

        //        });

        //        ViewBag.Category = categorylist;
        //    }
        //}

        //public JsonResult ShowCategoeryProducts(int categoryid)
        //{
        //    // Gallery(categoryid);
        //    DataSet ds = new DataSet();
        //    ds = obj.ShowCatProducts(categoryid);
        //    List<Buyer> prodlist = new List<Buyer>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        prodlist.Add(new Buyer
        //        {
        //            ProductName = dr["ProductName"].ToString(),
        //            //ProductCode = dr["ProductCode"].ToString(),
        //            MRP = Convert.ToInt32(dr["MRP"].ToString()),
        //            MainImage = dr["MainImage"].ToString(),

        //        });
        //    }

        //    return Json(prodlist,JsonRequestBehavior.AllowGet);
        //}

        public ActionResult WishListGrid()
        {


            BALBuyer objUser = new BALBuyer();
            DataSet ds = new DataSet();
            ds = objUser.WishList(BuyerCode);
            Buyer objDetails = new Buyer();
            List<Buyer> LstUserDt1 = new List<Buyer>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Buyer obj = new Buyer();

                obj.MainImage = (ds.Tables[0].Rows[i]["MainImage"].ToString());
                obj.ProductCode = (ds.Tables[0].Rows[i]["ProductCode"].ToString());
                obj.ProductName = (ds.Tables[0].Rows[i]["ProductName"].ToString());
                obj.MRP = Convert.ToInt32(ds.Tables[0].Rows[i]["MRP"].ToString());

                LstUserDt1.Add(obj);

            }
            objDetails.ListUser = LstUserDt1;

            return View(objDetails);
        }

        public ActionResult ADDCart(string id)
        {

            BALBuyer obj = new BALBuyer();
            obj.AddCart(BuyerCode, id);
            return RedirectToAction("WishListGrid");
        }
        [HttpGet]
        public ActionResult Delete(Buyer obj, string id)
        {
            obj.Isdelete = 1;

            BALBuyer obj1 = new BALBuyer();
            obj1.DeleteWishlist(id, obj.Isdelete);
            return RedirectToAction("WishListGrid");
            // ViewBag.message("Delete sucessfulluy");
        }
    }


    //public ActionResult ShowCategoeryProducts(int id)
    //{
    //   // Gallery(id);
    //    Buyer obj1 = new Buyer();
    //    obj1.CategoryId = id;

    //    SqlDataReader dr;
    //    dr = obj.ShowCatProducts(obj1.CategoryId);
    //    while (dr.Read())
    //    {
    //        // obj1.Id = Convert.ToInt32(dr["id"].ToString());
    //        obj1.ProductName = dr["ProductName"].ToString();
    //        obj1.MainImage = dr["MainImage"].ToString();
    //        obj1.MRP = Convert.ToInt32(dr["MRP"].ToString());

    //    }
    //    dr.Close();
    //    return View(obj1);
    //}


    ///----------Indrajeer-------------------//
        
}