using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroEcommerceLibrary.Buyer
{

    public class BALBuyer
    {
        SqlConnection con = new SqlConnection("Data Source=AKASH\\SQLEXPRESS;Initial Catalog=GSTAgroE-Commerce;Integrated Security=True");

        public SqlDataReader LogIn(string email,string password)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "login");
            cmd.Parameters.AddWithValue("@emailid", email);
            cmd.Parameters.AddWithValue("@password", password);
            //cmd.Parameters.AddWithValue("@buyerfullname", buyername);
            //cmd.Parameters.AddWithValue("@buyercode", buyercode);

            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            con.Close();

        }
        public DataSet SearchData(string prosearch)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SerarchWithImage");
            cmd.Parameters.AddWithValue("@productname", prosearch);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet CategoeryShow()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {

                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetCategory");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
            con.Close();


        }
        ///All Product Show\\\
        public DataSet ShowAllProducts()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ShowAllProducts");
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        //public SqlDataReader ShowCatProducts(int id) 
        //{
        //    if (con.State == System.Data.ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "ShowCatProd");
        //    cmd.Parameters.AddWithValue("@categoryid", id);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    return dr;
        //    con.Close();
        //}
        public DataSet ShowCatProducts(int id)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ShowCatProd");
            cmd.Parameters.AddWithValue("@categoryid", id);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }

        public DataSet checkout(string buyercode)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Checkout");
            cmd.Parameters.AddWithValue("@usercode", buyercode);

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        //////View Product Details
        public  DataSet ViewProductDetails(string productcode) 
        {
            if (con.State==System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ViewProductDetails");
            cmd.Parameters.AddWithValue("@productcode", productcode);
            SqlDataAdapter adpt =new SqlDataAdapter(cmd);
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }


        //Address binding

        public DataSet GetCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getCountry");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }
        public DataSet GetState(int countryid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getState");
            cmd.Parameters.AddWithValue("@countryid", countryid);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }
        public DataSet GetCity(int stateid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getCity");
            cmd.Parameters.AddWithValue("@stateid", stateid);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }

        /// //GET CATEGORY

        public DataSet GetCategory()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetCategory");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }

        //-------------------Indrajeet----------------------//
        public DataSet WishList(string BuyerCode)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "WishList");
            cmd.Parameters.AddWithValue("@BuyerCode", BuyerCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

            con.Close();
        }
        public void AddCart(string BuyerCode, string ProductCode)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Cart");
            cmd.Parameters.AddWithValue("@BuyerCode", BuyerCode);
            cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmd.ExecuteNonQuery();
            con.Close();




        }
        public void DeleteWishlist(string ProductCode, int Isdelete)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroBuyer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RemoveWishList");
            cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmd.Parameters.AddWithValue("@IsDelete", Isdelete);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
