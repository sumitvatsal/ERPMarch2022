using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolErp.Models;
using schoolERP_BLL;
using System.Security.Cryptography;
using System.Text;


namespace SchoolErp.Controllers.WebApi
{
    public class ParentsAPIController : ApiController
    {

        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/ParentsAPI/ChangeStParentPwd")]
        [System.Web.Http.HttpPost]
        public Student ChangeStParentPwd(List<string> val)
        {
            Student std = new Models.Student();
            int id = Convert.ToInt32(val[0]);
            string pwd = val[1];
            string oldpwd = val[2];
            std.SPwd = "";
            std.Extra10 = "0";

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pwd));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilderPPWD = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWD.Append(result[i].ToString("x2"));
            }

            string ParentsPassword = strBuilderPPWD.ToString();


            try
            {
                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set PPwd = '" + ParentsPassword + "' where ID=" + id);
                if (noOfRowUpdated > 0)
                {
                    std.Extra10 = noOfRowUpdated.ToString();
                    std.SPwd = pwd;
                    std.FirstName = "Password successfully updated";
                }
                return std;
            }
            catch (Exception e)
            {
                std.FirstName = "Error!!";
                return std;
                throw e;
            }
        }


       
        [System.Web.Http.Route("api/ParentsAPI/updateParentProfileDetails")]
        [System.Web.Http.HttpPost]
        public string updateParentProfileDetails(Student Em)
        {
            int ID = Em.ID;
            var result = db.TBLStudents.SingleOrDefault(x => x.ID == ID);
            result.FDOB = Em.FDOB;
            result.FNationality = Em.FNationality;
            result.FQualification = Int32.Parse(Em.FQualification);
            result.FJob = Em.FJob;
            result.FOfficeAddress = Em.FOfficeAddress;
            result.FDesig = Em.FDesig;
            result.FIncome = Em.FIncome;
            result.FMobile = Em.FMobile;
            result.Fmail = Em.Fmail;
            result.FAdharNo = Em.FAdharNo;
            result.MDOB = Em.MDOB;
            result.MNationality = Em.MNationality;
            result.MQualification = Int32.Parse(Em.MQualification);
            result.MJob = Em.MJob;
            result.MOfficeAddress = Em.MOfficeAddress;
            result.MDesig = Em.MDesig;
            result.MIncome = Em.MIncome;
            result.Mmobile = Em.Mmobile;
            result.Mmail = Em.Mmail;
            result.MAdharNo = Em.MAdharNo;
            db.SaveChanges();
            return "Information Updated Successfullly";   
         }
    }
}
