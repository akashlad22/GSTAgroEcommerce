using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroEcommerceLibrary.Seller
{
    public class BALSeller
    {
        SqlConnection con = new SqlConnection("Data Source=AKASH\\SQLEXPRESS;Initial Catalog=GSTAgroE-Commerce;Integrated Security=True");

        public void SellerReg(string SellerCode, string SellerFullName, string BusinessName, int BusinessPinCode, string EmailId, string Password, string MobileNo, string AlternateMobileNo, string Gender, int CityId, int PinCode, DateTime DOB, DateTime RegistrationDate /*string RejectionReason, DateTime SellerApprovalDate,*/ /*int StatusId*/)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "SaveSellerReg");
            cmd.Parameters.AddWithValue("@SellerCode", SellerCode);
            cmd.Parameters.AddWithValue("@SellerFullName", SellerFullName);
            cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
            cmd.Parameters.AddWithValue("@BusinessPinCode", BusinessPinCode);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            cmd.Parameters.AddWithValue("@AlternateMobileNo", AlternateMobileNo);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@CityId", CityId);
            cmd.Parameters.AddWithValue("@Pincode", PinCode);
            cmd.Parameters.AddWithValue("@DOB", DOB);
            cmd.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
            //   cmd.Parameters.AddWithValue("@RejectionReason", RejectionReason);
            //   cmd.Parameters.AddWithValue("@SellerApprovalDate", SellerApprovalDate);
            // cmd.Parameters.AddWithValue("@StatusId", StatusId);

            cmd.ExecuteNonQuery();
            con.Close();

        }
        public DataSet GetCity(int stateId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "City");
            cmd.Parameters.AddWithValue("@stateId", stateId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

            con.Close();
        }

        //public DataSet CityBind()
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "City");
        //    SqlDataAdapter adpt = new SqlDataAdapter();
        //    adpt.SelectCommand = cmd;
        //    DataSet ds = new DataSet();
        //    adpt.Fill(ds);
        //    return ds;
        //    con.Close();
        //}
        public DataSet GetCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Country");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;



            con.Close();
        }
        public DataSet GetState(int CountryId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "State");
            cmd.Parameters.AddWithValue("@CountryId", CountryId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;



            con.Close();
        }
        public SqlDataReader SellerCode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "SellerCode");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();

        }
        public void SellerRegAdd(string UserCode, string Home, string Office, string Other)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "SaveSellerAdd");
            cmd.Parameters.AddWithValue("@UserCode", UserCode);
            cmd.Parameters.AddWithValue("@Home", Home);
            cmd.Parameters.AddWithValue("@Office", Office);
            cmd.Parameters.AddWithValue("@Other", Other);
            // cmd.Parameters.AddWithValue("", StatusId);



            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void SellerAddDoc(string UserCode, string AadharNo, string PanCardNo, string AadharImage, string PanImage, string BusinessName, string GSTIN, int BusinessPinCode, string BusinessAadharNo, string BusinessPanNo, string BusinessAdharImage, string BusinessPanImage)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAgroSeller", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "SaveSellerDoc");
            cmd.Parameters.AddWithValue("@SellerCode", UserCode);
            cmd.Parameters.AddWithValue("@AadharNo", AadharNo);
            cmd.Parameters.AddWithValue("@PanCardNo", PanCardNo);
            cmd.Parameters.AddWithValue("@AadharImage", AadharImage);
            cmd.Parameters.AddWithValue("@PanImage", PanImage);
            cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
            cmd.Parameters.AddWithValue("@GSTIN", GSTIN);
            cmd.Parameters.AddWithValue("@BusinessPinCode", BusinessPinCode);
            cmd.Parameters.AddWithValue("@BusinessAadharNo", BusinessAadharNo);
            cmd.Parameters.AddWithValue("@BusinessPanNo", BusinessPanNo);
            cmd.Parameters.AddWithValue("@BusinessAdharImage", BusinessAdharImage);
            cmd.Parameters.AddWithValue("@BusinessPanImage", BusinessPanImage);
            cmd.ExecuteNonQuery();
            con.Close();


        }
    }
}
