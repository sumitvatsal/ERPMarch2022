using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace schoolERP_BLL
{
    public class AdminMaster
    {


        public static string savecountry(CountyMaster country)
        {
            sqlHelper obj = new sqlHelper();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = null;
            con.Open();
            if (string.IsNullOrEmpty(country.Id))
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcountry where IsDeleted is null and countryname='" + country.Name + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand("insert into tblCountry(CountryName,Status) values('" + country.Name + "','" + country.Status + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Country Added Successfully";
                }
                else
                {
                    return "Entered country name already exist!!";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcountry where IsDeleted is null and countryname='" + country.Name + "' and CountryID<>'" + country.Id + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand("update tblCountry set CountryName='" + country.Name + "',Status='" + country.Status + "' where CountryID=" + country.Id, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Country Updated Successfully";
                }
                else
                {
                    return "Entered country name already exist!!";
                }
            }
        }

        public static bool deleteCountryById(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblCountry where CountryID=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblCountry set IsDeleted=1,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where  CountryID=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Update tblstate set IsDeleted=1,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where  CountryID=" + id, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update tblcity set IsDeleted=1,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where  CountryID=" + id, con);
                cmd2.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string saveState(StateMaster state)
        {
            sqlHelper obj = new sqlHelper();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = null;
            con.Open();
            if (string.IsNullOrEmpty(state.Id))
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblState where isdeleted is null and StateName='" + state.Name + "' and CountryID='" + state.CountryName + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand("insert into tblState(StateName,status,countryId) values('" + state.Name + "','" + state.Status + "','" + state.CountryName + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "State Added Successfully";
                }
                else
                {
                    return "Entered state name already exist!!";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblState where isdeleted is null and StateName='" + state.Name + "' and CountryID='" + state.CountryName + "'  and StateId<>'" + state.Id + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand("update tblState set StateName='" + state.Name + "',Status='" + state.Status + "',countryId=" + state.CountryName + " where StateId=" + state.Id, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "State Updated Successfully";
                }
                else
                {
                    return "Entered state name already exist!!";
                }
            }
        }

        public static string saveCities(CityMaster cities)
        {
            sqlHelper obj = new sqlHelper();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = null;
            con.Open();
            if (string.IsNullOrEmpty(cities.Id))
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcity where isdeleted is null and cityname='" + cities.Name + "' and StateId='" + cities.StateId + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand("insert into tblCity(CityName,CountryId,StateId,Status) values('" + cities.Name + "','" + cities.CountryId + "','" + cities.StateId + "','" + cities.Status + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "City Added Successfully";
                }
                else
                {
                    return "Entered city name already exist!!";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcity where isdeleted is null and cityname='" + cities.Name + "' and StateId='" + cities.StateId + "'  and Id<>'" + cities.Id + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand("update tblCity set CityName='" + cities.Name + "',CountryId='" + cities.CountryId + "',StateId=" + cities.StateId + ",Status='" + cities.Status + "' where Id=" + cities.Id, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "City Updated Successfully";
                }
                else
                {
                    return "Entered city name already exist!!";
                }
            }
        }
        public static string saveCourse(CourseMaster cities)
        {
            sqlHelper obj = new sqlHelper();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = null;
            con.Open();
            if (string.IsNullOrEmpty(cities.Id))
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCourses where IsDeleted is null and CourseName='" + cities.Name + "' and SchoolID='" + cities.ID + "'"));
                if (chk == 0)
                {
                    cmd = new SqlCommand(@"insert into tblCourses(CourseName,CourseDesc,Code,MinimumAttendencPer,AttendencType,
                                                               TotalWorkingDay,SyllabusName,Status,SchoolID)
                                      values('" + cities.Name + "','" + cities.Desc + "','" + cities.Code + "','" + cities.MiniumuAttendePer
                                          + "','" + cities.AttendenceType + "','" + cities.TotlWorkingDay + "','" + cities.Syllabus + "','" + cities.Status + "','" + cities.ID + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Class saved Successfully";
                }
                else
                {
                    return "Entered class name already exist!!";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCourses where IsDeleted is null and CourseName='" + cities.Name + "' and SchoolID='" + cities.ID + "' and Id<>'" + cities.Id + "'"));
                if (chk == 0)
                {
                    string query = "update tblcourses set CourseName='" + cities.Name + "',CourseDesc='" + cities.Desc + "',Code='" + cities.Code + "',MinimumAttendencPer='" + cities.MiniumuAttendePer
                        + "',AttendencType='" + cities.AttendenceType + "',TotalWorkingDay='" + cities.TotlWorkingDay + "',SyllabusName='" + cities.Syllabus + "',Status='" + cities.Status + "',SchoolID='" + cities.ID + "' where Id=" + cities.Id;
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Class Updated Successfully";
                }
                else
                {
                    return "Entered class name already exist!!";
                }
            }


            // con.Close();

        }



        public static bool deleteCourseById(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblcourses where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblcourses set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static bool deleteCityById(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblCity where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblCity set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static bool deleteStateById(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblState where StateId=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblState set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where StateId=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update tblcity set isDeleted = 1,Deleted_on = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' where StateId =" + id , con);
                cmd1.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception )
            {

                throw;
            }
        }


        public static string saveBatch(BatchMaster batch)
        {
            sqlHelper obj = new sqlHelper();



            string[] cols = { "Description", "DateFrom", "DateTo", "Status", "SchoolID", "CurrActive" };
            object[] vals = { batch.Name, batch.StartDate, batch.EndDate, batch.Status, batch.SchoolID, batch.CurrActive };
            if (string.IsNullOrEmpty(batch.Id))
            {
                if (batch.CurrActive==true)
                {
                     string chk =obj.ExecuteScaler("select CurrActive  from tblAcademicYear where SchoolID='" + batch.SchoolID + "' and CurrActive=1 and IsDeleted is null ");
                    if (chk !=null)
                    {
                        return "already have one current Active Academic Year";
                    }
                    else
                    {
                        obj.insertValIntoTable("tblAcademicYear", cols, vals);
                        return "Academic Year Added Successfully";
                    }
                   
                }
                else 

                {
                    obj.insertValIntoTable("tblAcademicYear", cols, vals);
                    return "Academic Year Added Successfully";
                    
                }
               
            }
            else
            {     
                if (batch.CurrActive == true)
                {
                    string chk = obj.ExecuteScaler("select CurrActive  from tblAcademicYear where SchoolID='" + batch.SchoolID + "' and CurrActive=1 and ID!='" + batch.Id + "' and IsDeleted is null");
                    if (chk !=null)
                    {
                        return "Already have one Current Active Academic Year";
                    }
                    else
                    {
                        obj.updateValIntoTable("tblAcademicYear", cols, vals, "ID", batch.Id);
                        return "Academic Year Updated Successfully";
                    }
                   
                }
                else
                {
                    obj.updateValIntoTable("tblAcademicYear", cols, vals, "ID", batch.Id);
                    return "Academic Year Updated Successfully";
                }
                //else
                //{
                //    return "Another Academic Year is Alraedy Active ";
                //}




            }
        }
        

        public static bool deleteBatchById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //  SqlCommand cmd = new SqlCommand("delete from tblAcademicYear where ID=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblAcademicYear set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where ID=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string saveSection(SectionMaster sec)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "SectionName", "ClassId", "Status", "SchoolID" };
            object[] vals = { sec.Name, sec.Class, sec.Status, sec.SchoolID };
            if (string.IsNullOrEmpty(sec.Id))
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSections where SectionName='" + sec.Name + "' and  ClassId='"+sec.Class+"' and IsDeleted is null and SchoolID='" + sec.SchoolID + "'"));
                if (chk == 0)
                {
                    obj.insertValIntoTable("tblSections", cols, vals);
                    return "Section Added Successfully";
                }
                else
                {
                    return "Entered section name already exist!!";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSections where SectionName='" + sec.Name + "' and and  ClassId='" + sec.Class + "' and IsDeleted is null and SchoolID='" + sec.SchoolID + "' and Id<>'" + sec.Id + "'"));
                if (chk == 0)
                {
                    obj.updateValIntoTable("tblSections", cols, vals, "Id", sec.Id);
                    return "Section Updated Successfully";
                }
                else
                {
                    return "Entered section name already exist!!";
                }
            }
        }

        public static bool deleteSectionById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblSections where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblSections set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string saveDesignation(CountyMaster desc)
        {
            sqlHelper obj = new sqlHelper();

            string[] cols = { "Designation", "Status", "SchoolID" };
            object[] vals = { desc.Name, desc.Status, desc.SchoolID };
            if (string.IsNullOrEmpty(desc.Id))
            {
                string exists = obj.ExecuteScaler(" select Designation from tblDesignation where Designation='" + desc.Name + "' and SchoolID='" + desc.SchoolID + "' and IsDeleted is null");
                if (exists == desc.Name)
                {
                    return "Desgination " + desc.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblDesignation", cols, vals);
                    return "Designation Added Successfully";
                }

            }
            else
            {
                obj.updateValIntoTable("tblDesignation", cols, vals, "DesigID", desc.Id);

                return "Designation Updated Successfully"; ;
            }



        }
        public static bool deleteDesignationById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblDesignation where DesigID=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblDesignation set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where DesigID=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }



        public static string saveCasts(CastMaster cast)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "CategoryName", "Status" };
            object[] vals = { cast.Name, cast.Status };
            if (string.IsNullOrEmpty(cast.Id))
            {
                string exists = obj.ExecuteScaler(" select CategoryName from tblCastCategory where CategoryName='" + cast.Name + "' and IsDeleted is null ");
                if (exists == cast.Name)
                {
                    return "Caste Category " + cast.Name + " already exists";
                }
                else
                {
                    obj.insertValIntoTable("tblCastCategory", cols, vals);
                    return "Caste Category Added Successfully";
                }
            }
            else
            {
                int exists = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCastCategory where CategoryName='" + cast.Name + "' and IsDeleted is null and CatId<>'" + cast.Id + "'"));
                if (exists == 0)
                {
                    obj.updateValIntoTable("tblCastCategory", cols, vals, "CatId", cast.Id);
                    return "Caste Category Updated Successfully";
                }
                else
                {
                    return "Caste Category " + cast.Name + " already exists";
                }
            }
        }

        public static bool deleteCastById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //   SqlCommand cmd = new SqlCommand("delete from tblCastCategory where CatId=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblCastCategory set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where CatId=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public static string saveStatus(StatusMaster sta)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "Status", "stStatus" };
            object[] vals = { sta.Name, sta.Status };
            if (string.IsNullOrEmpty(sta.Id))
            {
                string exists = obj.ExecuteScaler(" select Status from tblStatus where Status='" + sta.Name + "' and IsDeleted is null");
                if (exists == sta.Name)
                {
                    return "Status " + sta.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblStatus", cols, vals);
                    return "Status Inserted Successfully";
                }
            }
            else
            {
                int exists = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblStatus where Status='" + sta.Name + "' and IsDeleted is null and StatusID<>'" + sta.Id + "'"));
                if (exists == 0)
                {
                    obj.updateValIntoTable("tblStatus", cols, vals, "StatusID", sta.Id);
                    return "Status Updated Successfully"; ;
                }
                else
                {
                    return "Status " + sta.Name + " already Exists";
                }
            }

        }


        public static bool deleteStatusById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblStatus where StatusID=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblStatus set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where StatusID=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }



        public static string saveRoles(RoleMaster role)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "RoleName", "Status" };
            object[] vals = { role.Name, role.Status };
            if (string.IsNullOrEmpty(role.Id))
            {
                string exists = obj.ExecuteScaler(" select RoleName from tblRoleMaster where RoleName='" + role.Name + "' and IsDeleted is null ");
                if (exists == role.Name)
                {
                    return "Role " + role.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblRoleMaster", cols, vals);
                    return "Role Inserted Successfully";
                }
            }
            else
            {
                int exists = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblRoleMaster where RoleName='" + role.Name + "' and IsDeleted is null and Role_id<>'" + role.Id + "'"));
                if (exists == 0)
                {
                    obj.updateValIntoTable("tblRoleMaster", cols, vals, "Role_id", role.Id);
                    return "Role Updated Successfully";
                }
                else
                {
                    return "Role " + role.Name + " already Exists";
                }


            }

        }


        public static bool deleteRolesById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblRoleMaster where Role_id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblRoleMaster set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Role_id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string saveDepartment(DepartmentMaster depart)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "DepartmentName", "Status", "SchoolID" };
            object[] vals = { depart.Name, depart.Status, depart.School };
            if (string.IsNullOrEmpty(depart.Id))
            {
                string exists = obj.ExecuteScaler("select DepartmentName from tblDepartmnet where DepartmentName='" + depart.Name + "' and SchoolID='" + depart.School + "' and IsDeleted is null ");
                if (exists == depart.Name)
                {
                    return "Department " + depart.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblDepartmnet", cols, vals);
                    return "Department Added Successfully";
                }
            }
            else
            {
                obj.updateValIntoTable("tblDepartmnet", cols, vals, "DepartmentId", depart.Id);
                return "Department Updated Successfully"; ;
            }

        }

        //public static string saveSchool(SchoolSMSMASTER depart)
        //{
        //    sqlHelper obj = new sqlHelper();
        //    string[] cols = { "DepartmentName", "Status", "SchoolID" };
        //    object[] vals = { depart.Name, depart.Status, depart.School };
        //    if (string.IsNullOrEmpty(depart.Id))
        //    {
        //        string exists = obj.ExecuteScaler("select DepartmentName from tblDepartmnet where DepartmentName='" + depart.Name + "' and SchoolID='" + depart.School + "' and IsDeleted is null ");
        //        if (exists == depart.Name)
        //        {
        //            return "Department " + depart.Name + " already Exists";
        //        }
        //        else
        //        {
        //            obj.insertValIntoTable("tblDepartmnet", cols, vals);
        //            return "Department Added Successfully";
        //        }
        //    }
        //    else
        //    {
        //        obj.updateValIntoTable("tblDepartmnet", cols, vals, "DepartmentId", depart.Id);
        //        return "Department Updated Successfully"; ;
        //    }

        //}
        public static bool deleteDepartmentById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //  SqlCommand cmd = new SqlCommand("delete from tblDepartmnet where DepartmentId=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblDepartmnet set IsDeleted = 1, Deleted_on = '" + DateTime.Now + "' where DepartmentId=" + id, con);

                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string saveQualfication(QualficationMaster qual)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "SchoolID", "QualificationName", "Status" };
            object[] vals = { qual.SchoolID, qual.Name, qual.Status };
            if (string.IsNullOrEmpty(qual.Id))
            {
                string exists = obj.ExecuteScaler("select QualificationName from tblQualifications where QualificationName='" + qual.Name + "' and SchoolID='" + qual.SchoolID + "' and IsDeleted is null");
                if (exists == qual.Name)
                {
                    return "Qualfication " + qual.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblQualifications", cols, vals);
                    return "Qualfication Inserted Successfully";
                }
            }
            else
            {
                obj.updateValIntoTable("tblQualifications", cols, vals, "QualificationId", qual.Id);
                return "Qualfication Updated Successfully"; ;
            }

        }


        public static bool deleteQualficationById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblQualifications where QualificationId=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblQualifications set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where QualificationId=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string saveCategoryCast(CategoryMaster cat)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "CasteName", "Status" };
            object[] vals = { cat.Name, cat.Status };
            if (string.IsNullOrEmpty(cat.Id))
            {
                string exists = obj.ExecuteScaler(" select CasteName from tblCaste where CasteName='" + cat.Name + "' and IsDeleted is null");
                if (exists == cat.Name)
                {
                    return "Caste Category " + cat.Name + " already exists";
                }
                else
                {
                    obj.insertValIntoTable("tblCaste", cols, vals);
                    return "Caste Category Inserted Successfully";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCaste where CasteName='" + cat.Name + "' and IsDeleted is null and CasteId<>'" + cat.Id + "'"));
                if (chk == 0)
                {
                    obj.updateValIntoTable("tblCaste", cols, vals, "CasteId", cat.Id);
                    return "Caste Category Updated Successfully";
                }
                else
                {
                    return "Caste Category " + cat.Name + " already exists";
                }
            }

        }


        public static bool deleteCategoryById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblCaste where CasteId=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblCaste set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where CasteId=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string saveStream(StreamMaster stream)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "Class", "StreamName", "Status", "SchoolID" };
            object[] vals = { stream.Class, stream.Name, stream.Status, stream.SchoolID };
            if (string.IsNullOrEmpty(stream.Id))
            {
                string exists = obj.ExecuteScaler(" select StreamName from tblStream where StreamName='" + stream.Name + "' and Class='"+stream.Class+"' and IsDeleted is null and SchoolID='"+ stream.SchoolID + "'");
                if (exists == stream.Name)
                {
                    return "Stream " + stream.Name + " already exist!!";
                }
                else
                {
                    obj.insertValIntoTable("tblStream", cols, vals);
                    return "Stream Added Successfully";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblStream where StreamName='" + stream.Name + "' and Class='" + stream.Class + "'  and SchoolID='" + stream.SchoolID + "' and Id<>'" + stream.Id + "' and IsDeleted is null"));
                if (chk == 0)
                {
                    obj.updateValIntoTable("tblStream", cols, vals, "Id", stream.Id);
                    return "Stream Updated Successfully";
                }
                else
                {
                    return "Stream " + stream.Name + " already exist!!";
                }
            }

        }


        public static bool deleteStreamById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //SqlCommand cmd = new SqlCommand("delete from tblStream where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblStream set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string saveDocument(DocumentMaster doc)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "DocumentName", "DocumentDesc", "userId", "Status","SchoolID" };
            object[] vals = { doc.Name, doc.desc, doc.userType, doc.Status ,doc.SchoolID};
            if (string.IsNullOrEmpty(doc.Id))
            {
                string exists = obj.ExecuteScaler(" select DocumentName from tblDocument where DocumentName='" + doc.Name + "' and userId='" + doc.userType + "' and SchoolID='"+ doc.SchoolID + "' " );
                if (exists == doc.Name)
                {
                    return "Document " + doc.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblDocument", cols, vals);
                    return "Document Added Successfully";
                }
            }
            else
            {
                obj.updateValIntoTable("tblDocument", cols, vals, "Id", doc.Id);
                return "Document Updated Successfully"; ;
            }

        }

        public static bool deleteDocumentById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //   SqlCommand cmd = new SqlCommand("delete from tblDocument where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblDocument set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string saveCodeGeneration(CodeGenMaster doc)
        {
            sqlHelper obj = new sqlHelper();
            //if ( doc.DocType== "Student")
            //{

            //}
            //else if (doc.DocType == "Student")
            string[] cols = { "UserType", "Perfix", "Suffix", "DocumentNo", "StartSeries", "Serprator", "Status", "SchoolID" };
            object[] vals = { doc.DocType, doc.Prefix, doc.Suffix, doc.DocNo, doc.StartSeries, doc.Seprator, doc.Status, doc.SchoolID };
            if (string.IsNullOrEmpty(doc.Id))
            {
                string exists = obj.ExecuteScaler(" select UserType from tblDocumentNo where UserType='" + doc.DocType + "' and SchoolID='" + doc.SchoolID + "' and IsDeleted is Null");
                if (exists == doc.DocType)
                {
                    return "Details for selected school and user type already Exists!!";
                }
                else
                {
                    obj.insertValIntoTable("tblDocumentNo", cols, vals);
                    return "Document Code Added Successfully";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblDocumentNo where UserType='" + doc.DocType + "' and SchoolID='" + doc.SchoolID + "' and IsDeleted is Null and Id<>'" + doc.Id + "'"));
                if (chk == 0)
                {
                    obj.updateValIntoTable("tblDocumentNo", cols, vals, "Id", doc.Id);
                    return "Document Code Updated Successfully";
                }
                else
                {
                    return "Details for selected school and user type already Exists!!";
                }


            }

        }

        public static bool deleteCodeDocumentById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //SqlCommand cmd = new SqlCommand("delete from tblDocumentNo where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblDocumentNo set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool deleteSchoolById(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblSchoolDetails where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblSchoolDetails set IsDeleted=1 ,Deleted_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string markactive(BatchMaster batch)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblSchoolDetails where Id=" + id, con);
                SqlCommand cmd = new SqlCommand(@"update tblAcademicYear set CurrActive=null where SchoolID='" + batch.SchoolID + "'", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand(@"update tblAcademicYear set CurrActive=1 where ID='" + batch.Id + "'", con);
                cmd1.ExecuteNonQuery();
                con.Close();
                return "Details saved successfully..";

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
