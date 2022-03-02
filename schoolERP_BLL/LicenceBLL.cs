using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace schoolERP_BLL
{
    public class LicenceBLL
    {
        public static string saveLicence(Licence licence)
        {
            sqlHelper obj = new sqlHelper();

            if (licence.Flag == "1")
            {
                string[] cols1 = { "active" };
                object[] vals1 = { "0" };
                obj.updateValIntoTable("licence_details", cols1, vals1, "school_id", licence.SchoolID);

                licence.LicenceNo = "LICENCE" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                licence.Active = "1";
                string[] cols = { "school_id", "licence_no", "no_of_students", "per_student_charge", "valid_to", "active", "created_by", "created_on" };
                object[] vals = { licence.SchoolID, licence.LicenceNo, licence.NoofStudent, licence.Charges, licence.ValidTo, licence.Active, licence.UserName, DateTime.Now };
                obj.insertValIntoTable("licence_details", cols, vals);
                return "Licence saved successfully..";
            }
            else
            {
                //licence.ValidTo=
                string[] cols = { "school_id", "no_of_students", "per_student_charge", "valid_to", "updated_by", "updated_on" };
                object[] vals = { licence.SchoolID, licence.NoofStudent, licence.Charges, licence.ValidTo, licence.UserName, DateTime.Now };
                obj.updateValIntoTable("licence_details", cols, vals, "Id", licence.Id);
                return "Licence updated successfully..";
            }
        }
        


        public static bool deleteLicence(string id, string usrname)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("update licence_details set deleted_on='" + DateTime.Now + "' , deleted_by='" + usrname + "', active=0 where id=" + id, con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("update licence_details set active=1 where id=( select max(id) from licence_details where deleted_on is null and school_id = (select  school_id from licence_details where id= " + id + "))", con);
                cmd1.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string savepaymentdetals(string usrname, string totalamount, int id)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                DateTime renewdate = DateTime.Now;
                DateTime newvalidto = renewdate.AddDays(30);
                string[] cols = { "licence_id", "total_amount", "renewal_date", "created_by", "created_on"};
                object[] vals = { id, totalamount, renewdate, usrname, DateTime.Now };
                obj.insertValIntoTable("payment_details", cols, vals);

                string[] cols1 = { "renewal_date", "valid_to" };
                object[] vals1 = { renewdate, newvalidto };
                obj.updateValIntoTable("licence_details", cols1, vals1, "Id", id);
                return "1";
            }
            catch
            {
                return null;
            }
        }

    }
}
