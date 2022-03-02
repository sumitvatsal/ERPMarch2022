using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolErp.Models;
using schoolERP_BLL;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace SchoolErp.Controllers.WebApi
{

    public class MasterAPIController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/MasterAPI/saveCountry")]
        [System.Web.Http.HttpPost]
        public string saveCountry(CountyMaster country)
        {
            sqlHelper obj = new sqlHelper();
            string b = AdminMaster.savecountry(country);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }
        }


        [System.Web.Http.Route("api/MasterAPI/SaveRecord")]
        [System.Web.Http.HttpPost]
        public int SaveRecord(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];

                if (a == "saveRegions")
                {


                    var id = Convert.ToInt32(val[1]);
                    var name = val[2].ToString().Trim();
                    var loginuser = val[3].ToString().Trim();
                    var dated = DateTime.Now;
                    var active = Convert.ToInt32(val[4]);
                    int SchoolID = Convert.ToInt32(val[5]);

                    if (id == 0)
                    {
                        var check = db.Regions.Where(s => s.Name == name && s.InsertUserId == loginuser && s.InsertDate == dated && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Regions cta = new SchoolErp.Regions();
                            cta.Name = name;
                            cta.InsertUserId = loginuser;
                            cta.InsertDate = dated;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Regions.Add(cta);
                            avi = 1;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    else
                    {
                        var check = db.Regions.Where(s => s.Name == name && s.UpdateUserId == loginuser && s.UpdateDate == dated && s.DeleteDate == null).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Regions.SingleOrDefault(b => b.Id == id);
                            result.Name = name;
                            result.UpdateUserId = loginuser;
                            result.UpdateDate = dated;

                            avi = 2;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    db.SaveChanges();

                }


                if (a == "Banks")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Bank = val[2].ToString().Trim();
                    var Ac = val[3].ToString().Trim();
                    var Number = val[4].ToString().Trim();
                    var Branch = val[5].ToString().Trim();
                    var Image = val[6].ToString().Trim();
                    var dated = DateTime.Now;
                    var loginuser = val[7].ToString().Trim();
                    var active = Convert.ToInt32(val[8]);
                    int SchoolID = Convert.ToInt32(val[9]);
                    if (id == 0)
                    {
                        var check = db.Banks.Where(s => s.BankName == Bank && s.ACName == Ac && s.ACNumber == Number && s.Branch == Branch &&  s.SignaturePicture == Image && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Banks cta = new SchoolErp.Banks();
                            cta.BankName = Bank;
                            cta.ACName = Ac;
                            cta.ACNumber = Number;
                            cta.Branch = Branch;
                            cta.SignaturePicture = Image;
                            cta.InsertDate = dated;
                            cta.InsertUserId = loginuser;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Banks.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        var check = db.Banks.Where(s => s.BankName == Bank && s.ACName == Ac && s.ACNumber == Number && s.Branch == Branch && s.SignaturePicture == Image && s.UpdateDate == dated && s.UpdateUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Banks.SingleOrDefault(b => b.Id == id);
                            result.BankName = Bank;
                            result.ACName = Ac;
                            result.ACNumber = Number;
                            result.Branch = Branch;
                            result.SignaturePicture = Image;
                            result.UpdateDate = dated;
                            result.UpdateUserId = loginuser;
                            result.IsActive = active;
                            result.UpdateDate = dated;

                        }
                        else
                        {
                        }
                    }
                    db.SaveChanges();

                }

                else if (a == "Territories")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Territory = val[2].ToString().Trim();
                    var RegionID = Convert.ToInt32(val[3]);
                    var dated = DateTime.Now;
                    var loginuser = val[4].ToString().Trim();
                    var active = Convert.ToInt32(val[5]);
                    int SchoolID = Convert.ToInt32(val[6]);

                    if (id == 0)
                    {
                        var check = db.Territories.Where(s => s.TerritoryDescription == Territory && s.RegionId == RegionID && s.InsertUserId == loginuser && s.InsertDate == dated && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Territories cta = new SchoolErp.Territories();
                            cta.TerritoryDescription = Territory;
                            cta.RegionId = RegionID;
                            cta.InsertDate = dated;
                            cta.InsertUserId = loginuser;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Territories.Add(cta);
                            avi = 1;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    else
                    {
                        var check = db.Territories.Where(s => s.TerritoryDescription == Territory && s.RegionId == RegionID && s.UpdateUserId == loginuser && s.UpdateDate == dated && s.DeleteDate == null).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Territories.SingleOrDefault(b => b.Id == id);
                            result.TerritoryDescription = Territory;
                            result.RegionId = RegionID;
                            result.UpdateUserId = loginuser;
                            result.UpdateDate = dated;
                            avi = 2;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    db.SaveChanges();

                }


                else if (a == "Services")
                {
                    saveServices(val);
                }
                else if (a == "Shippers")
                {
                    saveShippers(val);
                }

                else if (a == "Warehouses")
                {
                    saveWarehouses(val);
                }

                else if (a == "CustomerGroups")
                {
                    saveCustomerGroups(val);
                }
                else if (a == "SupplierGroups")
                {
                    saveSupplierGroups(val);
                }
                else if (a == "ExpenseTypes")
                {
                    saveExpenseTypes(val);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (avi == -1)
            {
                return -1;
            }
            else if (avi == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }

        public void saveServices(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Service = val[2].ToString().Trim();
            var Charge = Convert.ToDouble(val[3]);
            var Desc = val[4].ToString().Trim();
            var Tax = Convert.ToDouble(val[5]);
            var dated = DateTime.Now;
            var loginuser = val[6].ToString().Trim();
            var active = Convert.ToInt32(val[7]);
            int SchoolID = Convert.ToInt32(val[8]);


            if (id == 0)
            {
                var check = db.Services.Where(s => s.ServiceName == Service && s.Charge == Charge && s.Description == Desc && s.Tax == Tax && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                if (check == null)
                {

                    Services cta = new SchoolErp.Services();
                    cta.ServiceName = Service;
                    cta.Charge = Charge;
                    cta.Description = Desc;
                    cta.Tax = Tax;
                    cta.InsertDate = dated;
                    cta.InsertUserId = loginuser;
                    cta.IsActive = active;
                    cta.SchoolID = SchoolID;
                    db.Services.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.Services.Where(s => s.ServiceName == Service && s.Charge == Charge && s.Description == Desc && s.Tax == Tax && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                if (check == null)
                {

                    var result = db.Services.SingleOrDefault(b => b.Id == id);
                    result.ServiceName = Service;
                    result.Charge = Charge;
                    result.Description = Desc; ;
                    result.Tax = Tax;
                    result.UpdateUserId = loginuser; ;
                    result.UpdateDate = dated;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }

        public void saveShippers(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Company = val[2].ToString().Trim();
            var Phone = val[3].ToString().Trim();
            var dated = DateTime.Now;
            var loginuser = val[4].ToString().Trim();
            var active = Convert.ToInt32(val[5]);
            int SchoolID = Convert.ToInt32(val[6]); 

            if (id == 0)
            {
                var check = db.Shippers.Where(s => s.CompanyName == Company && s.Phone == Phone  && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                if (check == null)
                {

                    Shippers cta = new SchoolErp.Shippers();
                    cta.CompanyName = Company;
                    cta.Phone = Phone;
                    cta.InsertDate = dated;
                    cta.InsertUserId = loginuser;
                    cta.IsActive = active;
                    cta.SchoolID = SchoolID;
                    db.Shippers.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.Shippers.Where(s => s.CompanyName == Company && s.Phone == Phone  && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                if (check == null)
                {

                    var result = db.Shippers.SingleOrDefault(b => b.Id == id);
                    result.CompanyName = Company;
                    result.Phone = Phone;
                    result.UpdateUserId = loginuser; ;
                    result.UpdateDate = dated;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }
        public void saveWarehouses(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Name = val[2].ToString().Trim();
            var Country = Convert.ToInt32(val[3]);
            var State = Convert.ToInt32(val[4]);
            var City = Convert.ToInt32(val[5]);
            var Phone = val[6].ToString().Trim();
            var Address = val[7].ToString().Trim();
            var Desc = val[8].ToString().Trim();
            int SchoolID = Convert.ToInt32(val[9]);

            if (id == 0)
            {
                var check = db.Warehouses.Where(s => s.Name == Name && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Phone == Phone && s.Address == Address && s.Description == Desc).FirstOrDefault();
                if (check == null)
                {

                    Warehouses cta = new SchoolErp.Warehouses();
                    cta.Name = Name;
                    cta.Phone = Phone;
                    cta.CountryId = Country;
                    cta.StateId = State;
                    cta.CityId = City;
                    cta.Address = Address;
                    cta.Description = Desc;
                    cta.SchoolID = SchoolID;
                    db.Warehouses.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.Warehouses.Where(s => s.Name == Name && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Description == Desc && s.Phone == Phone && s.Address == Address).FirstOrDefault();
                if (check == null)
                {

                    var result = db.Warehouses.SingleOrDefault(b => b.Id == id);
                    result.Name = Name;
                    result.Phone = Phone;
                    result.StateId = State; ;
                    result.CountryId = Country;
                    result.CityId = City;
                    result.Address = Address; ;
                    result.Description = Desc;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }

        public void saveCustomerGroups(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Name = val[2].ToString().Trim();
            var Desc = val[3].ToString().Trim();
            var dated = DateTime.Now;
            var loginuser = val[4].ToString().Trim();
            var active = Convert.ToInt32(val[5]);
            int SchoolID = Convert.ToInt32(val[6]);

            if (id == 0)
            {
                var check = db.CustomerGroups.Where(s => s.Name == Name && s.Description == Desc && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                if (check == null)
                {

                    CustomerGroups cta = new SchoolErp.CustomerGroups();
                    cta.Name = Name;
                    cta.Description = Desc;
                    cta.InsertDate = dated;
                    cta.InsertUserId = loginuser;
                    cta.IsActive = active;
                    cta.SchoolID = SchoolID;
                    db.CustomerGroups.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.CustomerGroups.Where(s => s.Name == Name && s.Description == Desc && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                if (check == null)
                {

                    var result = db.CustomerGroups.SingleOrDefault(b => b.Id == id);
                    result.Name = Name;
                    result.Description = Desc;
                    result.UpdateUserId = loginuser; ;
                    result.UpdateDate = dated;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }

        public void saveSupplierGroups(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Name = val[2].ToString().Trim();
            var Desc = val[3].ToString().Trim();
            var dated = DateTime.Now;
            var loginuser = val[4].ToString().Trim();
            var active = Convert.ToInt32(val[5]);
            int SchoolID = Convert.ToInt32(val[6]);

            if (id == 0)
            {
                var check = db.SupplierGroups.Where(s => s.Name == Name && s.Description == Desc && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                if (check == null)
                {

                    SupplierGroups cta = new SchoolErp.SupplierGroups();
                    cta.Name = Name;
                    cta.Description = Desc;
                    cta.InsertDate = dated;
                    cta.InsertUserId = loginuser;
                    cta.IsActive = active;
                    cta.SchoolID = SchoolID;
                    db.SupplierGroups.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.SupplierGroups.Where(s => s.Name == Name && s.Description == Desc && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                if (check == null)
                {

                    var result = db.SupplierGroups.SingleOrDefault(b => b.Id == id);
                    result.Name = Name;
                    result.Description = Desc;
                    result.UpdateUserId = loginuser; ;
                    result.UpdateDate = dated;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }

        public void saveExpenseTypes(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Type = val[2].ToString().Trim();
            var dated = DateTime.Now;
            var loginuser = val[3].ToString().Trim();
            var active = Convert.ToInt32(val[4]);
            int SchoolID = Convert.ToInt32(val[5]);

            if (id == 0)
            {
                var check = db.ExpenseTypes.Where(s => s.Type == Type  && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                if (check == null)
                {

                    ExpenseTypes cta = new SchoolErp.ExpenseTypes();
                    cta.Type = Type;
                    cta.InsertDate = dated;
                    cta.InsertUserId = loginuser;
                    cta.IsActive = active;
                    cta.SchoolID = SchoolID;
                    db.ExpenseTypes.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.ExpenseTypes.Where(s => s.Type == Type && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                if (check == null)
                {

                    var result = db.ExpenseTypes.SingleOrDefault(b => b.Id == id);
                    result.Type = Type;
                    result.UpdateUserId = loginuser; ;
                    result.UpdateDate = dated;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }


        [System.Web.Http.Route("api/MasterAPI/RegionsList")]
        [System.Web.Http.HttpPost]
        public List<RegionsList> RegionsList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[3]);

            List<RegionsList> list = new List<RegionsList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select * from regions where DeleteDate is  NULL and SchoolID='" + SchoolID + "'");
                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                //left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                //left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId order by ep.Id desc");

                foreach (DataRow dr in dt.Rows)
                {
                    RegionsList usr = new RegionsList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/MasterAPI/TeritoriesList")]
        [System.Web.Http.HttpPost]
        public List<TeritoriesList> TeritoriesList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<TeritoriesList> list = new List<TeritoriesList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select te.id, regions.Name,regions.id As ID1, TE.TerritoryDescription from Territories TE INNER JOIN Regions ON te.RegionId=Regions.Id where te.DeleteDate  is  NULL and te.SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    TeritoriesList usr = new TeritoriesList();
                    usr.ID = dr["Id"].ToString();
                    usr.ID1 = dr["ID1"].ToString();
                    usr.TerritoryDescription = dr["TerritoryDescription"].ToString();
                    usr.region = dr["Name"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/MasterAPI/serviceList")]
        [System.Web.Http.HttpPost]
        public List<ServiceList> serviceList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<ServiceList> list = new List<ServiceList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from services where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    ServiceList usr = new ServiceList();
                    usr.ID = dr["Id"].ToString();
                    usr.ServiceName = dr["ServiceName"].ToString();
                    usr.Charge = dr["Charge"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    usr.Tax = dr["Tax"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/MasterAPI/ShipperList")]
        [System.Web.Http.HttpPost]
        public List<ShipperList> ShipperList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<ShipperList> list = new List<ShipperList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from Shippers where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    ShipperList usr = new ShipperList();
                    usr.ID = dr["Id"].ToString();
                    usr.CompanyName = dr["CompanyName"].ToString();
                    usr.Phone = dr["Phone"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/MasterAPI/WarehousesList")]
        [System.Web.Http.HttpPost]
        public List<WarehousesList> WarehousesList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);

            List<WarehousesList> list = new List<WarehousesList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select Wh.Id,Wh.Name ,Wh.Description,tblCountry.CountryName,tblCountry.CountryID,tblState.StateId,tblcity.Id as CityID,tblState.StateName,tblcity.CityName,Wh.Phone,Wh.Address from Warehouses Wh
                                              INNER JOIN tblCountry ON Wh.CountryId=tblCountry.CountryID
                                              INNER JOIN tblState ON Wh.StateId=tblState.StateId
                                              INNER JOIN tblCity ON Wh.CityId=tblCity.Id where  Wh.SchoolID='" + SchoolID + "' ");
                foreach (DataRow dr in dt.Rows)
                {
                    WarehousesList usr = new WarehousesList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Description = dr["Description"].ToString();
                    usr.Country = dr["CountryName"].ToString();
                    usr.State = dr["StateName"].ToString();
                    usr.City = dr["CityName"].ToString();
                    usr.Phone = dr["Phone"].ToString();
                    usr.Address = dr["Address"].ToString();
                    usr.Countryid = dr["CountryID"].ToString();
                    usr.Stateid = dr["StateId"].ToString();
                    usr.Cityid = dr["CityId"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/MasterAPI/CustmerList")]
        [System.Web.Http.HttpPost]
        public List<custmerList> CustmerList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<custmerList> list = new List<custmerList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from CustomerGroups where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    custmerList usr = new custmerList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/MasterAPI/SupplierList")]
        [System.Web.Http.HttpPost]
        public List<SupplierList> SupplierList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<SupplierList> list = new List<SupplierList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from SupplierGroups where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    SupplierList usr = new SupplierList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/MasterAPI/BankList")]
        [System.Web.Http.HttpPost]
        public List<BankList> BankList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<BankList> list = new List<BankList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from Banks where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    BankList usr = new BankList();
                    usr.ID = dr["Id"].ToString();
                    usr.BankName = dr["BankName"].ToString();
                    usr.ACName = dr["ACName"].ToString();
                    usr.ACnumber = dr["ACNumber"].ToString();
                    usr.Branch = dr["Branch"].ToString();
                    usr.Image = dr["SignaturePicture"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }



        [System.Web.Http.Route("api/MasterAPI/ExpenseTypesList")]
        [System.Web.Http.HttpPost]
        public List<ExpenseTypesList> ExpenseTypesList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[3]);

            List<ExpenseTypesList> list = new List<ExpenseTypesList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from ExpenseTypes where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    ExpenseTypesList usr = new ExpenseTypesList();
                    usr.ID = dr["Id"].ToString();
                    usr.Type = dr["Type"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/MasterAPI/DeleteRecord")]
        [System.Web.Http.HttpPost]
        public int DeleteRecord(List<string> val)
        {
            int id = Convert.ToInt32(val[0]);
            string userid = val[1].ToString().Trim();
            string type = val[2].ToString().Trim();

            try
            {
                //if (type == "Bank")
                //{
                //    var employer = new tblBankMaster { ID = id };
                //    db.Entry(employer).State = EntityState.Deleted;
                //}
                if (type == "Regions")
                {
                    //var employer = new tblFeeCategory { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Regions.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }

                else if (type == "Territories")
                {
                    //var employer = new tblFeeCategory { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Territories.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }

                else if (type == "Banks")
                {
                    //var employer = new tblFeeCategory { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Banks.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }
                else if (type == "Services")
                {
                    //var employer = new tblFeeStructure { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Services.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();


                }
                else if (type == "Shippers")
                {
                    //var employer = new tblFeeStructureConfig { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Shippers.SingleOrDefault(a => a.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();
                }
                else if (type == "SupplierGroups")
                {
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.SupplierGroups.SingleOrDefault(a => a.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();
                }

                else if (type == "Warehouses")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Warehouses.SingleOrDefault(a => a.Id == idd);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;
                    
                    db.SaveChanges();
                }
                else if (type == "CustomerGroups")
                {
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.CustomerGroups.SingleOrDefault(a => a.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();
                }

                else if (type == "ExpenseTypes")
                {
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.ExpenseTypes.SingleOrDefault(a => a.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();
                }
                db.SaveChanges();




                return 1;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }


        [System.Web.Http.Route("api/MasterAPI/showtest")]
        [System.Web.Http.HttpPost]
        public string showtest(CountyMaster country)
        {
            sqlHelper obj = new sqlHelper();
            string b = AdminMaster.savecountry(country);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }
        }


        [System.Web.Http.Route("api/MasterAPI/getAllCountry")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllCountry()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select * from tblCountry where IsDeleted is null";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                if (bool.Parse(dr["status"].ToString()))
                {
                    usr.Statuss = "Activate";
                }
                else
                {
                    usr.Statuss = "Deactivate";
                }
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/MasterAPI/getCountryById")]
        [System.Web.Http.HttpPost]
        public CountyMaster getCountryById(string id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();


            string query = "select * from tblCountry where CountryID=" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            // SqlDataAdapter adap = new SqlDataAdapter(cmd);
            // DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            CountyMaster usr = new CountyMaster();
            if (dr.Read())
            {


                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }
            con.Close();

            return usr;






        }


        [System.Web.Http.Route("api/MasterAPI/getCountryByIdd")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getCountryByIdd(List<string> aa)
        {
            int idd = Convert.ToInt32(aa[0]);
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select * from tblCountry where IsDeleted is null and CountryID='" + idd + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                if (bool.Parse(dr["status"].ToString()))
                {
                    usr.Statuss = "Activate";
                }
                else
                {
                    usr.Statuss = "Deactivate";
                }
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/MasterAPI/deleteCountryById")]
        [System.Web.Http.HttpPost]
        public string deleteCountryById(string id)
        {
            bool b = AdminMaster.deleteCountryById(id);
            if (b)
            {
                return "Country Deleted Successfully";
            }
            else
            {
                return "Country Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/MasterAPI/saveState")]
        [System.Web.Http.HttpPost]
        public string saveState(StateMaster statee)
        {
            string b = AdminMaster.saveState(statee);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }
        }



        [System.Web.Http.Route("api/MasterAPI/getAllCountryforState")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllCountryforState()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select CountryID,CountryName from tblCountry where Status=1 and isdeleted is null order by LOWER( CountryName)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }
        [System.Web.Http.Route("api/MasterAPI/getstatebyCountryIdNEW")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getstatebyCountryIdNEW(string id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select * from tblState where  Status=1 and isdeleted is null and countryId='" + id + "' ORDER BY LOWER( StateName)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["StateName"].ToString();
                usr.Id = dr["StateId"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();


        }

        [System.Web.Http.Route("api/MasterAPI/getstatebyCountryId")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getstatebyCountryId(string id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select * from tblState where  Status=1 and isdeleted is null and countryId='" + id + "' ORDER BY LOWER( StateName)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["StateName"].ToString();
                usr.Id = dr["StateId"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();


        }

        [System.Web.Http.Route("api/MasterAPI/saveCities")]
        [System.Web.Http.HttpPost]
        public string saveCities(CityMaster citiess)
        {
            string b = AdminMaster.saveCities(citiess);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/MasterAPI/saveCourse")]
        [System.Web.Http.HttpPost]
        public string saveCourse(CourseMaster course)
        {
            string b = AdminMaster.saveCourse(course);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }







        [System.Web.Http.Route("api/MasterAPI/getallCourses")]
        [System.Web.Http.HttpPost]
        public CourseMaster[] getallCourses(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<CourseMaster> list = new List<CourseMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            if (typeuser == 2)
            {
                string query = "select a.Id,a.SchoolID,b.School,CourseName,CourseDesc,Code,MinimumAttendencPer,AttendencType,TotalWorkingDay,SyllabusName,Status,a.DateCreated from tblcourses a left outer join tblSchoolDetails b on a.SchoolID=b.ID where a.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CourseMaster usr = new CourseMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["CourseName"].ToString();
                    usr.Desc = dr["CourseDesc"].ToString();
                    usr.Code = dr["Code"].ToString();
                    usr.MiniumuAttendePer = dr["MinimumAttendencPer"].ToString();
                    usr.AttendenceType = dr["AttendencType"].ToString();
                    usr.TotlWorkingDay = dr["TotalWorkingDay"].ToString();
                    usr.Syllabus = dr["SyllabusName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "DeActive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["SchoolID"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                string query = "select a.Id,a.SchoolID,b.School,CourseName,CourseDesc,Code,MinimumAttendencPer,AttendencType,TotalWorkingDay,SyllabusName,a.Status,a.DateCreated from tblcourses a left outer join tblSchoolDetails b on a.SchoolID = b.ID left outer join tblEmployee em on em.SchoolID = a.SchoolID where em.UserID = '" + loginuser + "' and em.IsDeleted is null and a.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CourseMaster usr = new CourseMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["CourseName"].ToString();
                    usr.Desc = dr["CourseDesc"].ToString();
                    usr.Code = dr["Code"].ToString();
                    usr.MiniumuAttendePer = dr["MinimumAttendencPer"].ToString();
                    usr.AttendenceType = dr["AttendencType"].ToString();
                    usr.TotlWorkingDay = dr["TotalWorkingDay"].ToString();
                    usr.Syllabus = dr["SyllabusName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "DeActive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["SchoolID"].ToString();
                    list.Add(usr);
                }

            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/MasterAPI/editcourseById")]
        [System.Web.Http.HttpPost]
        public CourseMaster editcourseById(string id)
        {

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select Id,CourseName,CourseDesc,Code,MinimumAttendencPer,AttendencType,TotalWorkingDay,SyllabusName,Status,DateCreated from tblcourses where id=" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            CourseMaster usr = new CourseMaster();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["CourseName"].ToString();
                usr.Desc = dr["CourseDesc"].ToString();
                usr.Code = dr["Code"].ToString();
                usr.MiniumuAttendePer = dr["MinimumAttendencPer"].ToString();
                usr.AttendenceType = dr["AttendencType"].ToString();
                usr.TotlWorkingDay = dr["TotalWorkingDay"].ToString();
                usr.Syllabus = dr["SyllabusName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                usr.ID = dr["ID"].ToString();
            }
            dr.Close();
            con.Close();
            return usr;

        }

        [System.Web.Http.Route("api/MasterAPI/deleteCourseById")]
        [System.Web.Http.HttpPost]
        public string deleteCourseById(string id)
        {
            bool b = AdminMaster.deleteCourseById(id);
            if (b)
            {
                return "Class Deleted Successfully";
            }
            else
            {
                return "Class Not Deleted Successfully";
            }

        }



        [System.Web.Http.Route("api/MasterAPI/getallCities")]
        [System.Web.Http.HttpPost]
        public CityMaster[] getallCities()
        {
            List<CityMaster> list = new List<CityMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = @"select c.Id,CityName,cu.CountryName,s.StateName,c.Status  from tblCity c
inner join tblCountry cu on cu.CountryID = c.CountryId
left outer join tblState s on s.StateId = c.StateId where c.IsDeleted is null and s.isdeleted is null and cu.isdeleted is null";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CityMaster usr = new CityMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["CityName"].ToString();
                usr.StateId = dr["StateName"].ToString();
                usr.CountryId = dr["CountryName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                if (bool.Parse(dr["Status"].ToString()))
                {
                    usr.Statuss = "Activate";
                }
                else
                {
                    usr.Statuss = "Deactivate";
                }

                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/MasterAPI/editCityById")]
        [System.Web.Http.HttpPost]
        public CityMaster editCityById(string id)
        {

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = @"select * from tblCity where id=" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            CityMaster usr = new CityMaster();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["CityName"].ToString();
                usr.CountryId = dr["CountryId"].ToString();
                usr.StateId = dr["StateId"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }
            con.Close();
            return usr;

        }




        [System.Web.Http.Route("api/MasterAPI/deleteCityById")]
        [System.Web.Http.HttpPost]
        public string deleteCityById(string id)
        {
            bool b = AdminMaster.deleteCityById(id);
            if (b)
            {
                return "City Deleted Successfully";
            }
            else
            {
                return "City Not Deleted Successfully";
            }

        }
        [System.Web.Http.Route("api/MasterAPI/getAllState")]
        [System.Web.Http.HttpPost]
        public StateMaster[] getAllState()
        {
            List<StateMaster> list = new List<StateMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = @"select StateId,StateName,c.CountryName,s.status from tblState s
                        inner join tblCountry c on c.CountryID = s.countryId where s.IsDeleted is null and c.isdeleted is null order by LOWER( StateName)  ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StateMaster usr = new StateMaster();
                usr.Id = dr["StateId"].ToString();
                usr.Name = dr["StateName"].ToString();
                usr.CountryName = dr["CountryName"].ToString();
                usr.Status = bool.Parse(dr["status"].ToString());
                if (bool.Parse(dr["status"].ToString()))
                {
                    usr.Statuss = "Activate";
                }
                else
                {
                    usr.Statuss = "Deactivate";
                }
                list.Add(usr);
            }
            con.Close();

            return list.ToArray();


        }

        [System.Web.Http.Route("api/MasterAPI/editStateById")]
        [System.Web.Http.HttpPost]
        public StateMaster editStateById(string id)
        {

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = @"select * from tblState where StateId=" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            StateMaster usr = new StateMaster();
            if (dr.Read())
            {
                usr.Id = dr["StateId"].ToString();
                usr.Name = dr["StateName"].ToString();
                usr.CountryName = dr["countryId"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }
            con.Close();
            return usr;

        }

        [System.Web.Http.Route("api/MasterAPI/deleteStateById")]
        [System.Web.Http.HttpPost]
        public string deleteStateById(string id)
        {
            bool b = AdminMaster.deleteStateById(id);
            if (b)
            {
                return "State Deleted Successfully";
            }
            else
            {
                return "State Not Deleted Successfully";
            }

        }
        [System.Web.Http.Route("api/MasterAPI/getAllClassesBYSchool")]
        [System.Web.Http.HttpPost]
        public CourseMaster[] getAllClassesBYSchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);

            List<CourseMaster> list = new List<CourseMaster>();
            sqlHelper obj = new sqlHelper();

            DataTable dt = obj.getDataTable("select Id,CourseName from tblcourses where Status=1 and IsDeleted is null and SchoolID='" + SchoolID + "' "); /*order by LOWER(CourseName)*/
            foreach (DataRow dr in dt.Rows)
            {
                CourseMaster usr = new CourseMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["CourseName"].ToString();
                list.Add(usr);
            }





            return list.ToArray();


        }

        [System.Web.Http.Route("api/MasterAPI/getAllClasses")]
        [System.Web.Http.HttpPost]
        public CourseMaster[] getAllClasses()
        {

            List<CourseMaster> list = new List<CourseMaster>();
            sqlHelper obj = new sqlHelper();

            DataTable dt = obj.getDataTable("select Id,CourseName from tblcourses where Status=1 and IsDeleted is null ");
            foreach (DataRow dr in dt.Rows)
            {
                CourseMaster usr = new CourseMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["CourseName"].ToString();
                list.Add(usr);
            }




            return list.ToArray();


        }

        [System.Web.Http.Route("api/MasterAPI/saveBatch")]
        [System.Web.Http.HttpPost]
        public string saveBatch(BatchMaster batch)
        {
            string b = AdminMaster.saveBatch(batch);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/MasterAPI/markactive")]
        [System.Web.Http.HttpPost]
        public string markactive(BatchMaster batch)
        {
            string b = AdminMaster.markactive(batch);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }



        [System.Web.Http.Route("api/MasterAPI/getAllBatches")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllBatches(List<string> aa)
        {

            string log = aa[0];
            int usertype = Convert.ToInt32(aa[1]);
            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();
            if (usertype == 2)
            {
                DataTable dt = obj.getDataTable(@"select b.School,a.DateFrom,a.DateTo,a.Description,a.Status,a.CurrActive,a.ID,a.SchoolID,a.CurrActive from tblAcademicYear a left outer join tblschooldetails b ON b.ID=a.SchoolID where a.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["ID"].ToString();
                    usr.Name = dr["Description"].ToString();
                    usr.Class = Convert.ToDateTime(dr["DateFrom"]).ToString("yyyy") + " - " + Convert.ToDateTime(dr["DateTo"]).ToString("yyyy");
                    usr.StartDate = ((DateTime)dr["DateFrom"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["DateTo"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //  usr.maxNoOfStudent = dr["MaxNoOfStudent"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.StatusShow = "Active";
                    }
                    else
                    {
                        usr.StatusShow = "DeActive";
                    }
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    usr.CurrActive = bool.Parse(dr["CurrActive"].ToString());
                    if (usr.CurrActive == true)
                    {
                        usr.CurrActiveShow = "Active";
                    }
                    else
                    {
                        usr.CurrActiveShow = "DeActive";
                    }
                    //if (usr.CurrActive == "True")
                    //{
                    //    usr.CurrActive = "Active";
                    //}
                    list.Add(usr);
                }
            }

            else
            {
                DataTable dt = obj.getDataTable(@"select b.School,a.DateFrom,a.DateTo,a.Description,a.Status,a.ID,a.SchoolID,a.CurrActive
 from tblAcademicYear a
  left outer join tblschooldetails b ON b.ID=a.SchoolID 
  left outer join tblEmployee em on a.SchoolID =em.SchoolID 
  where em.UserID='" + log + "' and em.IsDeleted is null and a.IsDeleted is null ");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["ID"].ToString();
                    usr.Name = dr["Description"].ToString();
                    usr.Class = Convert.ToDateTime(dr["DateFrom"]).ToString("yyyy") + " - " + Convert.ToDateTime(dr["DateTo"]).ToString("yyyy");
                    usr.StartDate = ((DateTime)dr["DateFrom"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["DateTo"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    //  usr.maxNoOfStudent = dr["MaxNoOfStudent"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.StatusShow = "Active";
                    }
                    else
                    {
                        usr.StatusShow = "DeActive";
                    }
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    usr.CurrActive = bool.Parse(dr["CurrActive"].ToString());
                    if (usr.CurrActive == true)
                    {
                        usr.CurrActiveShow = "Active";
                    }
                    else
                    {
                        usr.CurrActiveShow = "DeActive";
                    }
                    list.Add(usr);
                }
            }

            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/editBatchById")]
        [System.Web.Http.HttpPost]
        public BatchMaster editBatchById(string id)
        {
            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader("select CurrActive,DateFrom,DateTo,Description,Status,ID,SchoolID from tblAcademicYear where ID=" + id);

            BatchMaster usr = new BatchMaster();
            if (dr.Read())
            {
                usr.Id = dr["ID"].ToString();
                usr.Name = dr["Description"].ToString();
                //  usr.Class = dr["ClassId"].ToString();
                usr.StartDate = ((DateTime)dr["DateFrom"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.EndDate = ((DateTime)dr["DateTo"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                // usr.maxNoOfStudent = dr["MaxNoOfStudent"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                usr.SchoolID = Convert.ToInt32(dr["ID"]);
                usr.CurrActive = bool.Parse(dr["CurrActive"].ToString());
            }

            return usr;

        }

        [System.Web.Http.Route("api/MasterAPI/deleteBatchById")]
        [System.Web.Http.HttpPost]
        public string deleteBatchById(string id)
        {
            bool b = AdminMaster.deleteBatchById(id);
            if (b)
            {
                return "Batch Deleted Successfully";
            }
            else
            {
                return "Batch Not Deleted Successfully";
            }

        }



        [System.Web.Http.Route("api/MasterAPI/saveSection")]
        [System.Web.Http.HttpPost]
        public string saveSection(SectionMaster Section)
        {
            string b = AdminMaster.saveSection(Section);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }



        [System.Web.Http.Route("api/MasterAPI/getAllSections")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllSections(List<string> aa)
        {
            string log = aa[0];
            int usertype = Convert.ToInt32(aa[1]);

            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();
            if (usertype == 2)
            {
                DataTable dt = obj.getDataTable(@"select sc.School,s.SchoolID, s.Id,SectionName,c.CourseName,s.Status,s.ClassId from tblSections s
                                         inner join tblcourses c on c.Id=s.ClassId
										 inner join tblSchoolDetails sc on sc.ID= s.SchoolID where s.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["SectionName"].ToString();
                    usr.Class = dr["CourseName"].ToString();
                    usr.classid = Convert.ToInt32(dr["Classid"]);
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.StatusShow = "Active";
                    }
                    else
                    {
                        usr.StatusShow = "DeActive";
                    }
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select sc.School,s.SchoolID, s.Id,SectionName,c.CourseName,s.Status,s.Classid from tblSections s
                                         inner join tblcourses c on c.Id=s.ClassId
										 inner join tblSchoolDetails sc on sc.ID= s.SchoolID 
										 left outer join tblEmployee em on s.SchoolID =em.SchoolID
                                         where em.UserID='" + log + "' and em.IsDeleted is null  and s.IsDeleted is null ");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["SectionName"].ToString();
                    usr.Class = dr["CourseName"].ToString();
                    usr.classid = Convert.ToInt32(dr["Classid"]);
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.StatusShow = "Active";
                    }
                    else
                    {
                        usr.StatusShow = "DeActive";
                    }
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }

            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/editSectionById")]
        [System.Web.Http.HttpPost]
        public SectionMaster editSectionById(string id)
        {
            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader("select Id,SectionName,ClassId,Status  from tblSections where id=" + id);

            SectionMaster usr = new SectionMaster();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["SectionName"].ToString();
                usr.Class = dr["ClassId"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }

            return usr;

        }


        [System.Web.Http.Route("api/MasterAPI/deleteSectionById")]
        [System.Web.Http.HttpPost]
        public string deleteSectionById(string id)
        {
            bool b = AdminMaster.deleteSectionById(id);
            if (b)
            {
                return "Section Deleted Successfully";
            }
            else
            {
                return "Section Not Deleted Successfully";
            }

        }



        [System.Web.Http.Route("api/MasterAPI/saveDesignation")]
        [System.Web.Http.HttpPost]
        public string saveDesignation(CountyMaster country)
        {
            string b = AdminMaster.saveDesignation(country);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }




        [System.Web.Http.Route("api/MasterAPI/getAllDesignation")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllDesignation(List<string> aa)
        {

            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select DesigID,Designation,Status,sd.School,SD.ID from tblDesignation DS
                                             inner join tblSchoolDetails Sd on Sd.ID=DS.SchoolID where DS.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["DesigID"].ToString();
                    usr.Name = dr["Designation"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["ID"]);
                    list.Add(usr);
                }
            }
            else if (typeuser == 6)
            {
                DataTable dt = obj.getDataTable(@"select DS.DesigID,Designation,DS.Status,sd.School,SD.ID from tblDesignation DS
      inner join tblSchoolDetails Sd on Sd.ID=DS.SchoolID 
	   where Sd.SchoolCode ='" + loginuser + "' and DS.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["DesigID"].ToString();
                    usr.Name = dr["Designation"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["ID"]);
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select DS.DesigID,Designation,DS.Status,sd.School,SD.ID from tblDesignation DS
      inner join tblSchoolDetails Sd on sd.ID=DS.SchoolID
	  left outer join tblEmployee em on em.SchoolID=DS.SchoolID
	   where em.UserID='" + loginuser + "' and em.IsDeleted is null and DS.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    BatchMaster usr = new BatchMaster();
                    usr.Id = dr["DesigID"].ToString();
                    usr.Name = dr["Designation"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["ID"]);
                    list.Add(usr);
                }
            }

            return list.ToArray();


        }



        [System.Web.Http.Route("api/MasterAPI/editDesignationById")]
        [System.Web.Http.HttpPost]
        public CountyMaster editDesignationById(string id)
        {
            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader("select sc.School,ld.SchoolID, DesigID,Designation,Status from tblDesignation ld left outer join tblschoolDetails sc on ld.SchoolID = sc.ID where DesigID=" + id);

            CountyMaster usr = new CountyMaster();
            if (dr.Read())
            {
                usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                usr.Id = dr["DesigID"].ToString();
                usr.Name = dr["Designation"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }

            return usr;

        }



        [System.Web.Http.Route("api/MasterAPI/deleteDesignationById")]
        [System.Web.Http.HttpPost]
        public string deleteDesignationById(string id)
        {
            bool b = AdminMaster.deleteDesignationById(id);
            if (b)
            {
                return "Designation Deleted Successfully";
            }
            else
            {
                return "Designation Not Deleted Successfully";
            }

        }


        [System.Web.Http.Route("api/MasterAPI/saveCasts")]
        [System.Web.Http.HttpPost]
        public string saveCasts(CastMaster cast)
        {
            string b = AdminMaster.saveCasts(cast);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }



        [System.Web.Http.Route("api/MasterAPI/getAllCasts")]
        [System.Web.Http.HttpPost]
        public CastMaster[] getAllCasts()
        {
            List<CastMaster> list = new List<CastMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select CatId,CategoryName,Status from tblCastCategory where IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                CastMaster usr = new CastMaster();
                usr.Id = dr["CatId"].ToString();
                usr.Name = dr["CategoryName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                if (usr.Status == true)
                {
                    usr.Statusss = "Activate";
                }
                else
                {
                    usr.Statusss = "Deactivate";
                }
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/editCastById")]
        [System.Web.Http.HttpPost]
        public CountyMaster editCastById(string id)
        {
            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader("select CatId,CategoryName,Status from tblCastCategory where CatId=" + id);

            CountyMaster usr = new CountyMaster();
            if (dr.Read())
            {
                usr.Id = dr["CatId"].ToString();
                usr.Name = dr["CategoryName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }

            return usr;

        }


        [System.Web.Http.Route("api/MasterAPI/deleteCastById")]
        [System.Web.Http.HttpPost]
        public string deleteCastById(string id)
        {
            bool b = AdminMaster.deleteCastById(id);
            if (b)
            {
                return "Cast Category Deleted Successfully";
            }
            else
            {
                return "Cast Category Not Deleted Successfully";
            }

        }



        [System.Web.Http.Route("api/MasterAPI/saveStatus")]
        [System.Web.Http.HttpPost]
        public string saveStatus(StatusMaster status)
        {
            string b = AdminMaster.saveStatus(status);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }


        [System.Web.Http.Route("api/MasterAPI/getAllStatus")]
        [System.Web.Http.HttpPost]
        public CastMaster[] getAllStatus()
        {
            List<CastMaster> list = new List<CastMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select StatusID,Status,stStatus from tblStatus where IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                CastMaster usr = new CastMaster();
                usr.Id = dr["StatusID"].ToString();
                usr.Name = dr["Status"].ToString();
                usr.Status = bool.Parse(dr["stStatus"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/deleteStatusById")]
        [System.Web.Http.HttpPost]
        public string deleteStatusById(string id)
        {
            bool b = AdminMaster.deleteStatusById(id);
            if (b)
            {
                return "Status Deleted Successfully";
            }
            else
            {
                return "Status Not Deleted Successfully";
            }

        }


        [System.Web.Http.Route("api/MasterAPI/saveRoles")]
        [System.Web.Http.HttpPost]
        public string saveRoles(RoleMaster role)
        {
            string b = AdminMaster.saveRoles(role);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }



        [System.Web.Http.Route("api/MasterAPI/getAllRoles")]
        [System.Web.Http.HttpPost]
        public RoleMaster[] getAllRoles()
        {
            List<RoleMaster> list = new List<RoleMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select Role_id,RoleName,Status from tblRoleMaster where IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                RoleMaster usr = new RoleMaster();
                usr.Id = dr["Role_id"].ToString();
                usr.Name = dr["RoleName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/deleteRolesById")]
        [System.Web.Http.HttpPost]
        public string deleteRolesById(string id)
        {
            bool b = AdminMaster.deleteRolesById(id);
            if (b)
            {
                return "Status Deleted Successfully";
            }
            else
            {
                return "Status Not Deleted Successfully";
            }

        }



        [System.Web.Http.Route("api/MasterAPI/saveDepartment")]
        [System.Web.Http.HttpPost]
        public string saveDepartment(DepartmentMaster depart)
        {
            string b = AdminMaster.saveDepartment(depart);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }
        //SchoolSms
        //[System.Web.Http.Route("api/MasterAPI/saveSchool")]
        //[System.Web.Http.HttpPost]
        //public string saveSchool(SchoolSMSMASTER SMS)
        //{
        //    string b = AdminMaster.saveSchool(SMS);
        //    if (b != "")
        //    {
        //        return b;
        //    }
        //    else
        //    {

        //        return "";
        //    }


        //}
        //

        [System.Web.Http.Route("api/MasterAPI/getAllDepartment")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllDepartment(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();


            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            if (typeuser == 2)
            {
                string query = @"select DepartmentId,DepartmentName,Status,SD.School,sd.ID from tblDepartmnet D
                             left join tblSchooldetails SD on D.SchoolID=SD.ID where D.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    DepartmentMaster usr = new DepartmentMaster();
                    usr.Id = dr["DepartmentId"].ToString();
                    usr.Name = dr["DepartmentName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["ID"].ToString();
                    list.Add(usr);
                }
            }
            else if (typeuser == 6)
            {
                string query = @" select DepartmentId,DepartmentName,D.Status,SD.School,sd.ID from tblDepartmnet D
                             left join tblSchooldetails SD on D.SchoolID=SD.ID
							 where sd.SchoolCode='" + loginuser + "' and  D.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    DepartmentMaster usr = new DepartmentMaster();
                    usr.Id = dr["DepartmentId"].ToString();
                    usr.Name = dr["DepartmentName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["ID"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                string query = @"select DepartmentId,DepartmentName,D.Status,SD.School,sd.ID from tblDepartmnet D
                             left join tblSchooldetails SD on D.SchoolID=SD.ID
							 left outer join tblEmployee em on em.SchoolID=D.SchoolID
							  where em.UserID='" + loginuser + "' and em.IsDeleted is null and D.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    DepartmentMaster usr = new DepartmentMaster();
                    usr.Id = dr["DepartmentId"].ToString();
                    usr.Name = dr["DepartmentName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";

                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }

                    usr.School = dr["School"].ToString();
                    usr.ID = dr["ID"].ToString();
                    list.Add(usr);
                }
            }
            con.Close();

            return list.ToArray();


        }



        [System.Web.Http.Route("api/MasterAPI/deleteDepartmentById")]
        [System.Web.Http.HttpPost]
        public string deleteDepartmentById(string id)
        {
            bool b = AdminMaster.deleteDepartmentById(id);
            if (b)
            {
                return "Department Deleted Successfully";
            }
            else
            {
                return "Department Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/MasterAPI/saveQualfication")]
        [System.Web.Http.HttpPost]
        public string saveQualfication(QualficationMaster qual)
        {
            string b = AdminMaster.saveQualfication(qual);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/MasterAPI/getAllQualfication")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllQualfication(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select b.ID,a.SchoolID ,a.QualificationId,b.School,a.QualificationName,a.Status from tblQualifications a left join tblSchoolDetails b on b.ID=a.SchoolID where a.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    DepartmentMaster usr = new DepartmentMaster();
                    usr.Id = dr["QualificationId"].ToString();
                    usr.Name = dr["QualificationName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["ID"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            else if (typeuser == 6)
            {
                DataTable dt = obj.getDataTable(@"select b.ID,a.SchoolID ,a.QualificationId,b.School,a.QualificationName,a.Status from tblQualifications a
		                                          left join tblSchoolDetails b on b.ID=a.SchoolID
		                                          where b.SchoolCode='" + loginuser + "' and  a.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    DepartmentMaster usr = new DepartmentMaster();
                    usr.Id = dr["QualificationId"].ToString();
                    usr.Name = dr["QualificationName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["ID"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select b.ID,a.SchoolID ,a.QualificationId,b.School,a.QualificationName,a.Status from tblQualifications a
					 left join tblSchoolDetails b on b.ID=a.SchoolID
					 left outer join tblEmployee em on em.SchoolID =a.SchoolID
					  where em.UserID='" + loginuser + "' and em.IsDeleted is null and  a.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    DepartmentMaster usr = new DepartmentMaster();
                    usr.Id = dr["QualificationId"].ToString();
                    usr.Name = dr["QualificationName"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statuss = "Active";
                    }
                    else
                    {
                        usr.Statuss = "Deactive";
                    }
                    usr.School = dr["School"].ToString();
                    usr.ID = dr["ID"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }


            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/deleteQualficationById")]
        [System.Web.Http.HttpPost]
        public string deleteQualficationById(string id)
        {
            bool b = AdminMaster.deleteQualficationById(id);
            if (b)
            {
                return "Qualification Deleted Successfully";
            }
            else
            {
                return "Qualification Not Deleted Successfully";
            }

        }


        [System.Web.Http.Route("api/MasterAPI/saveCategoryCast")]
        [System.Web.Http.HttpPost]
        public string saveCategoryCast(CategoryMaster cat)
        {
            string b = AdminMaster.saveCategoryCast(cat);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/MasterAPI/getAllCastCategory")]
        [System.Web.Http.HttpPost]
        public CategoryMaster[] getAllCastCategory()
        {
            List<CategoryMaster> list = new List<CategoryMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select CasteId,CasteName,Status from tblCaste where IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                CategoryMaster usr = new CategoryMaster();
                usr.Id = dr["CasteId"].ToString();
                usr.Name = dr["CasteName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                if (dr["Status"].ToString() == "true")
                {
                    usr.Statusss = "Activate";
                }
                else
                {
                    usr.Statusss = "Deactivate";
                }
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/deleteCategoryById")]
        [System.Web.Http.HttpPost]
        public string deleteCategoryById(string id)
        {
            bool b = AdminMaster.deleteCategoryById(id);
            if (b)
            {
                return "Cast Category Deleted Successfully";
            }
            else
            {
                return "Cast Category Not Deleted Successfully";
            }

        }


        [System.Web.Http.Route("api/MasterAPI/saveStream")]
        [System.Web.Http.HttpPost]
        public string saveStream(StreamMaster stream)
        {
            string b = AdminMaster.saveStream(stream);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/MasterAPI/getAllStream")]
        [System.Web.Http.HttpPost]
        public StreamMaster[] getAllStream(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<StreamMaster> list = new List<StreamMaster>();
            sqlHelper obj = new sqlHelper();
            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select sc.School,s.SchoolID, s.Id,Class,c.CourseName,StreamName,s.Status from tblStream s
                                                inner join tblCourses c on c.Id=s.Class
												left outer join tblSchoolDetails sc on sc.ID= s.SchoolID
												where s.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    StreamMaster usr = new StreamMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["StreamName"].ToString();
                    usr.Class = dr["CourseName"].ToString();
                    usr.ClassId = dr["Class"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.StatusShow = "Active";
                    }
                    else
                    {
                        usr.StatusShow = "DeActive";
                    }
                    usr.SchoolID = Convert.ToInt32(dr["Schoolid"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }

            else
            {
                DataTable dt = obj.getDataTable(@" select sc.School,s.SchoolID, s.Id,Class,c.CourseName,StreamName,s.Status from tblStream s
                                                inner join tblCourses c on c.Id=s.Class
												left outer join tblSchoolDetails sc on sc.ID= s.SchoolID
												left outer join tblEmployee em on s.SchoolID =em.SchoolID
                                                where em.UserID='" + loginuser + "' and em.IsDeleted is null and s.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    StreamMaster usr = new StreamMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["StreamName"].ToString();
                    usr.Class = dr["CourseName"].ToString();
                    usr.ClassId = dr["Class"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.StatusShow = "Active";
                    }
                    else
                    {
                        usr.StatusShow = "DeActive";
                    }
                    usr.SchoolID = Convert.ToInt32(dr["Schoolid"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }

            return list.ToArray();


        }


        [System.Web.Http.Route("api/MasterAPI/deleteStreamById")]
        [System.Web.Http.HttpPost]
        public string deleteStreamById(string id)
        {
            bool b = AdminMaster.deleteStreamById(id);
            if (b)
            {
                return "Stream Deleted Successfully";
            }
            else
            {
                return "Stream Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/MasterAPI/saveDocument")]
        [System.Web.Http.HttpPost]
        public string saveDocument(DocumentMaster doc)
        {
            string b = AdminMaster.saveDocument(doc);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }



        [System.Web.Http.Route("api/MasterAPI/getAllDocument")]
        [System.Web.Http.HttpPost]
        public DocumentMaster[] getAllDocument(List<string> aa)
        {
            List<DocumentMaster> list = new List<DocumentMaster>();
            sqlHelper obj = new sqlHelper();

            string loginuser = aa[0];
            int usertype = Convert.ToInt32(aa[1]);
            if (usertype == 1 || usertype == 3)
            {
                DataTable dt = obj.getDataTable(@"select Sd.School,d.SchoolID,d.Id,DocumentName,DocumentDesc,ut.Name UserName,d.userId,d.Status from  tblDocument d
                                              inner join tblUserType ut on ut.id=d.userId
											  join tblSchoolDetails Sd on Sd.ID=d.SchoolID
											  join tblEmployee em on em.SchoolID = Sd.ID
											  where em.UserID='" + loginuser + "' and em.IsDeleted is null and d.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    DocumentMaster usr = new DocumentMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["DocumentName"].ToString();
                    usr.desc = dr["DocumentDesc"].ToString();
                    usr.userType = dr["UserName"].ToString();
                    usr.userId = dr["userId"].ToString();

                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "Deactivate";
                    }

                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }
            else if (usertype == 6)
            {
                DataTable dt = obj.getDataTable(@"select Sd.School,d.SchoolID,d.Id,DocumentName,DocumentDesc,ut.Name UserName,d.userId,d.Status from  tblDocument d
                                              inner join tblUserType ut on ut.id=d.userId
											  join tblSchoolDetails Sd on Sd.ID=d.SchoolID
											  
											  where Sd.SchoolCode='" + loginuser + "' and d.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    DocumentMaster usr = new DocumentMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["DocumentName"].ToString();
                    usr.desc = dr["DocumentDesc"].ToString();
                    usr.userType = dr["UserName"].ToString();
                    usr.userId = dr["userId"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "Deactivate";
                    }
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select d.Id,DocumentName,DocumentDesc,ut.Name UserName,userId,d.Status from  tblDocument d
                                              inner join tblUserType ut on ut.id=d.userId
											  where d.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    DocumentMaster usr = new DocumentMaster();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["DocumentName"].ToString();
                    usr.desc = dr["DocumentDesc"].ToString();
                    usr.userType = dr["UserName"].ToString();
                    usr.userId = dr["userId"].ToString();

                    usr.Status = bool.Parse(dr["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "Deactivate";
                    }
                    list.Add(usr);
                }
            }


            return list.ToArray();


        }



        [System.Web.Http.Route("api/MasterAPI/deleteDocumentById")]
        [System.Web.Http.HttpPost]
        public string deleteDocumentById(string id)
        {
            bool b = AdminMaster.deleteDocumentById(id);
            if (b)
            {
                return "Documents Deleted Successfully";
            }
            else
            {
                return "Documents Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/MasterAPI/getallUserType")]
        [System.Web.Http.HttpPost]
        public roleType[] getallUserType()
        {
            // var r= db.tblUserTypes.ToList();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            List<roleType> list = new List<roleType>();

            string query = "select * from tblUserType where status=1";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleType usr = new roleType();
                usr.Id = dt.Rows[i]["id"].ToString();
                usr.Name = dt.Rows[i]["Name"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();



        }

        [System.Web.Http.Route("api/MasterAPI/getallUserTypeforSchool")]
        [System.Web.Http.HttpPost]
        public roleType[] getallUserTypeforSchool()
        {
            // var r= db.tblUserTypes.ToList();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            List<roleType> list = new List<roleType>();

            //string query = "select * from tblUserType where status=1";
            string query = "select * from tblUserType where ( id=1 or id=3 or id=4 ) and status=1";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleType usr = new roleType();
                usr.Id = dt.Rows[i]["id"].ToString();
                usr.Name = dt.Rows[i]["Name"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();



        }



        [System.Web.Http.Route("api/MasterAPI/saveCodeGeneration")]
        [System.Web.Http.HttpPost]
        public string saveCodeGeneration(CodeGenMaster doc)
        {
            string b = AdminMaster.saveCodeGeneration(doc);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }





        [System.Web.Http.Route("api/MasterAPI/getAllDocumentNo")]
        [System.Web.Http.HttpPost]
        public CodeGenMaster[] getAllDocumentNo(List<string> aa)

        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            List<CodeGenMaster> list = new List<CodeGenMaster>();

            if (typeuser == 2)
            {
                string query = "select DN.id,DN.SchoolID,SD.School,DN.UserType,Dn.Perfix,dn.Suffix,dn.DocumentNo,dn.StartSeries,dn.Serprator,dn.LastSeries,dn.Status from tblDocumentNo DN left join tblSchooldetails SD on DN.SchoolID = SD.ID where DN.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CodeGenMaster usr = new CodeGenMaster();
                    usr.Id = dt.Rows[i]["id"].ToString();
                    usr.DocType = dt.Rows[i]["UserType"].ToString();
                    usr.Prefix = dt.Rows[i]["Perfix"].ToString();
                    usr.Suffix = dt.Rows[i]["Suffix"].ToString();
                    usr.DocNo = dt.Rows[i]["DocumentNo"].ToString();
                    usr.StartSeries = dt.Rows[i]["StartSeries"].ToString();
                    usr.Seprator = dt.Rows[i]["Serprator"].ToString();
                    usr.LastSeries = dt.Rows[i]["LastSeries"].ToString();
                    usr.Status = bool.Parse(dt.Rows[i]["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "DeActive";

                    }
                    usr.SchoolID = dt.Rows[i]["SchoolID"].ToString();
                    usr.School = dt.Rows[i]["School"].ToString();
                    list.Add(usr);
                }
            }
            else if (typeuser == 6)
            {
                string query = "select DN.id,DN.SchoolID,SD.School,DN.UserType,Dn.Perfix,dn.Suffix,dn.DocumentNo,dn.StartSeries,dn.Serprator,dn.LastSeries,dn.Status from tblDocumentNo DN  left join tblSchooldetails SD on DN.SchoolID = SD.ID  where SD.SchoolCode = '" + loginuser + "' and DN.IsDeleted is null and SD.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CodeGenMaster usr = new CodeGenMaster();
                    usr.Id = dt.Rows[i]["id"].ToString();
                    usr.DocType = dt.Rows[i]["UserType"].ToString();
                    usr.Prefix = dt.Rows[i]["Perfix"].ToString();
                    usr.Suffix = dt.Rows[i]["Suffix"].ToString();
                    usr.DocNo = dt.Rows[i]["DocumentNo"].ToString();
                    usr.StartSeries = dt.Rows[i]["StartSeries"].ToString();
                    usr.Seprator = dt.Rows[i]["Serprator"].ToString();
                    usr.LastSeries = dt.Rows[i]["LastSeries"].ToString();
                    usr.Status = bool.Parse(dt.Rows[i]["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "DeActive";

                    }
                    usr.SchoolID = dt.Rows[i]["SchoolID"].ToString();
                    usr.School = dt.Rows[i]["School"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                string query = "select DN.id,DN.SchoolID,SD.School,DN.UserType,Dn.Perfix,dn.Suffix,dn.DocumentNo,dn.StartSeries,dn.Serprator,dn.LastSeries,dn.Status from tblDocumentNo DN  left join tblSchooldetails SD on DN.SchoolID = SD.ID left outer join tblEmployee em on em.SchoolID = Dn.SchoolID where em.UserID = '" + loginuser + "' and em.IsDeleted is null and DN.IsDeleted is null";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CodeGenMaster usr = new CodeGenMaster();
                    usr.Id = dt.Rows[i]["id"].ToString();
                    usr.DocType = dt.Rows[i]["UserType"].ToString();
                    usr.Prefix = dt.Rows[i]["Perfix"].ToString();
                    usr.Suffix = dt.Rows[i]["Suffix"].ToString();
                    usr.DocNo = dt.Rows[i]["DocumentNo"].ToString();
                    usr.StartSeries = dt.Rows[i]["StartSeries"].ToString();
                    usr.Seprator = dt.Rows[i]["Serprator"].ToString();
                    usr.LastSeries = dt.Rows[i]["LastSeries"].ToString();
                    usr.Status = bool.Parse(dt.Rows[i]["Status"].ToString());
                    if (usr.Status == true)
                    {
                        usr.Statusshow = "Active";
                    }
                    else
                    {
                        usr.Statusshow = "DeActive";

                    }
                    usr.SchoolID = dt.Rows[i]["SchoolID"].ToString();
                    usr.School = dt.Rows[i]["School"].ToString();
                    list.Add(usr);
                }
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/MasterAPI/deleteCodeDocumentById")]
        [System.Web.Http.HttpPost]
        public string deleteCodeDocumentById(string id)
        {
            bool b = AdminMaster.deleteCodeDocumentById(id);
            if (b)
            {
                return "Documents Code Deleted Successfully";
            }
            else
            {
                return "Documents Code Not Deleted Successfully";
            }

        }
        [System.Web.Http.Route("api/MasterAPI/saveSubjectsDetails")]
        [System.Web.Http.HttpPost]
        public string saveSubjectsDetails(SubjectMaster sub)
        {
            sqlHelper obj = new sqlHelper();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            if (string.IsNullOrEmpty(sub.Id) || sub.Id == "0")
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSubject where IsDeleted is null and SchoolID='" + sub.SchoolID + "'  and subject='" + sub.Name + "' and classid ='" + sub.classid + "'"));
                if (chk == 0)
                {
                    tblSubject usr = new tblSubject();
                    usr.Subject = sub.Name;
                    usr.SubCode = sub.Code;
                    usr.Description = sub.Desc;
                    usr.Status = sub.Status;
                    usr.SchoolID = sub.SchoolID;
                    usr.classid = sub.classid;
                    db.tblSubjects.Add(usr);
                    db.SaveChanges();
                    return "Subject Saved Successfully";
                }
                else
                {
                    return "Entered subject name already exist!!";
                }
            }
            else
            {
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSubject where IsDeleted is null and SchoolID='" + sub.SchoolID + "'  and subject='" + sub.Name + "'  and ID<>'" + sub.Id + "' and classid='" + sub.classid + "'"));
                if (chk == 0)
                {
                    Int64 idd = Convert.ToInt64(sub.Id);
                    var usr = db.tblSubjects.SingleOrDefault(s => s.ID == idd);
                    usr.Subject = sub.Name;
                    usr.SubCode = sub.Code;
                    usr.Description = sub.Desc;
                    usr.Status = sub.Status;
                    usr.SchoolID = sub.SchoolID;
                    usr.classid = sub.classid;
                    // db.tblSubjects.Add(usr);
                    db.SaveChanges();
                    return "Subject Updated Successfully";
                }
                else
                {
                    return "Entered subject name already exist!!";
                }


            }
        }

        [System.Web.Http.Route("api/MasterAPI/getAllSubjects")]
        [System.Web.Http.HttpPost]
        public SubjectMaster[] getAllSubjects(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);

            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<SubjectMaster> list = new List<SubjectMaster>();
            if (typeuser == 2)
            {
                var result = (from a in db.tblSubjects
                              join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                              join cl in db.tblCourses on a.classid equals cl.Id
                              where a.IsDeleted == null
                              select new
                              {
                                  model = a,
                                  SchoolName = s.School,
                                  classname = cl.CourseName
                              }
                        ).ToList();

                //var result = db.tblSubjects.OrderByDescending(s=>s.ID).ToList();

                foreach (var a in result)
                {
                    SubjectMaster usr = new SubjectMaster();

                    usr.Id = a.model.ID.ToString();
                    usr.Name = a.model.Subject;
                    usr.Code = a.model.SubCode;
                    usr.Desc = a.model.Description;
                    usr.Status = a.model.Status;
                    if (usr.Status == true)
                    {
                        usr.Color = "#00a65a";
                        usr.stStatus = "Active";
                    }

                    else
                    {
                        usr.Color = "#dd4b39";
                        usr.stStatus = "Deactive";
                    }
                    usr.School = a.SchoolName;
                    usr.SchoolID = Convert.ToInt32(a.model.SchoolID);
                    usr.classid = Convert.ToInt32(a.model.classid);
                    usr.classname = a.classname;

                    list.Add(usr);
                }

            }
            else
            {
                var result = (from a in db.tblSubjects
                              join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                              join em in db.tblEmployees on a.SchoolID equals em.SchoolID
                              join cl in db.tblCourses on a.classid equals cl.Id
                              where a.IsDeleted == null && em.UserID == loginuser && em.IsDeleted == null
                              select new
                              {
                                  model = a,
                                  SchoolName = s.School,
                                  classname = cl.CourseName
                              }
                        ).ToList();

                //var result = db.tblSubjects.OrderByDescending(s=>s.ID).ToList();

                foreach (var a in result)
                {
                    SubjectMaster usr = new SubjectMaster();

                    usr.Id = a.model.ID.ToString();
                    usr.Name = a.model.Subject;
                    usr.Code = a.model.SubCode;
                    usr.Desc = a.model.Description;
                    usr.Status = a.model.Status;
                    if (usr.Status == true)
                    {
                        usr.Color = "#00a65a";
                        usr.stStatus = "Active";
                    }

                    else
                    {
                        usr.Color = "#dd4b39";
                        usr.stStatus = "Deactive";
                    }
                    usr.School = a.SchoolName;
                    usr.SchoolID = Convert.ToInt32(a.model.SchoolID);
                    usr.classid = Convert.ToInt32(a.model.classid);
                    usr.classname = a.classname;
                    list.Add(usr);
                }
            }


            return list.ToArray();
        }

        [System.Web.Http.Route("api/MasterAPI/deleteSubjectById")]
        [System.Web.Http.HttpPost]
        public string deleteSubjectById(Int64 id)
        {
            Int64 idd = Convert.ToInt64(id);
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //var sub = new tblSubject { ID = id };
            //db.Entry(sub).State = System.Data.Entity.EntityState.Deleted;
            var aa = db.tblSubjects.SingleOrDefault(s => s.ID == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            db.SaveChanges();
            return "Subject Deleted Successfully";
        }

        [System.Web.Http.Route("api/MasterAPI/getClassTeachersBySchool")]
        [System.Web.Http.HttpPost]
        public ClassTeacher[] getClassTeachersBySchool(List<string> aa)
        {
            List<ClassTeacher> list = new List<Models.ClassTeacher>();
            try
            {
                int SchoolID = Convert.ToInt32(aa[0]);
                int count = 0;
                var result = (from c in db.tblClassTeacherAllocations
                              join cls in db.tblCourses on c.ClassID equals cls.Id
                              join sec in db.tblSections on c.SectionID equals sec.Id
                              join e in db.tblEmployees on c.intEmpID equals e.Id
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              where c.SchoolID == SchoolID /*&& e.IsDeleted==null*/
                              orderby (e.FirstName)
                              select new
                              {
                                  model = c,
                                  CourseNm = cls.CourseName,
                                  SectionNm = sec.SectionName,
                                  PicPath = e.Image,
                                  Name = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                                  EmpID = e.Empcode,
                                  SchoolName = s.School
                              }).ToList();

                foreach (var m in result)
                {
                    count++;
                    ClassTeacher ct = new Models.ClassTeacher();
                    ct.countID = count;
                    ct.intEmpID = m.model.intEmpID;
                    ct.EmpID = m.EmpID;
                    ct.classNm = m.CourseNm + " - " + m.SectionNm;
                    ct.ClassID = m.model.ClassID;
                    ct.SectionID = m.model.SectionID;
                    ct.sectionNm = m.SectionNm;
                    ct.ID = m.model.ID;
                    ct.School = m.SchoolName;
                    ct.Name = m.Name;
                    ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                    if (m.PicPath != "" && m.PicPath != null)
                    {
                        ct.Picpath = m.PicPath;
                    }
                    else
                    {
                        ct.Picpath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }

                    ct.Status = m.model.Status;
                    if (ct.Status == 0)
                    {
                        ct.statusNm = "Deactive";
                        ct.style = "btn btn-block btn-danger btn-sm";
                    }
                    else
                    {
                        ct.statusNm = "Active";
                        ct.style = "btn btn-block btn-success btn-sm";
                    }
                    list.Add(ct);
                }
            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/MasterAPI/getClassTeachers")]
        [System.Web.Http.HttpPost]
        public ClassTeacher[] getClassTeachers()
        {
            List<ClassTeacher> list = new List<Models.ClassTeacher>();
            try
            {
                int count = 0;
                var result = (from c in db.tblClassTeacherAllocations
                              join cls in db.tblCourses on c.ClassID equals cls.Id
                              join sec in db.tblSections on c.SectionID equals sec.Id
                              join e in db.tblEmployees on c.intEmpID equals e.Id
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              select new
                              {
                                  model = c,
                                  CourseNm = cls.CourseName,
                                  SectionNm = sec.SectionName,
                                  PicPath = e.Image,
                                  Name = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                                  EmpID = e.Empcode,
                                  SchoolName = s.School
                              }).ToList();

                foreach (var m in result)
                {
                    count++;
                    ClassTeacher ct = new Models.ClassTeacher();
                    ct.countID = count;
                    ct.intEmpID = m.model.intEmpID;
                    ct.EmpID = m.EmpID;
                    ct.classNm = m.CourseNm + " - " + m.SectionNm;
                    ct.ClassID = m.model.ClassID;
                    ct.SectionID = m.model.SectionID;
                    ct.sectionNm = m.SectionNm;
                    ct.ID = m.model.ID;
                    ct.School = m.SchoolName;
                    ct.Name = m.Name;
                    ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                    if (m.PicPath != "" && m.PicPath != null)
                    {
                        ct.Picpath = m.PicPath;
                    }
                    else
                    {
                        ct.Picpath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }

                    ct.Status = m.model.Status;
                    if (ct.Status == 0)
                    {
                        ct.statusNm = "Inactive";
                        ct.style = "btn btn-block btn-danger btn-sm";
                    }
                    else
                    {
                        ct.statusNm = "Active";
                        ct.style = "btn btn-block btn-success btn-sm";
                    }
                    list.Add(ct);
                }
            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/MasterAPI/GetCTbySchool")]
        [System.Web.Http.HttpPost]
        public ClassTeacher[] GetCTbySchool(List<string> aa)
        {
            List<ClassTeacher> list = new List<ClassTeacher>();

            try
            {

                int SchoolID = Convert.ToInt32(aa[0]);
                int desigid = 0;
                //designation
                var desination = db.tblDesignations.Where(d => d.SchoolID == SchoolID && d.Designation.Contains("Driver") && d.IsDeleted == null).FirstOrDefault();
                if (desination != null)
                {
                    desigid = desination.DesigID;
                }
                else
                {
                    desigid = 0;
                }

                var result = (from e in db.tblEmployees
                              where e.Status == true && e.SchoolID == SchoolID && e.IsDeleted == null && e.DesigID != desigid
                              orderby (e.FirstName)
                              select new
                              {
                                  model = e
                              }).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    ClassTeacher items = new ClassTeacher();
                    items.Name = a.model.FirstName + " " + a.model.MiddleName + " " + a.model.LastName + "(" + a.model.Empcode + ")";
                    items.EmpID = a.model.Empcode;
                    items.ID = a.model.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/MasterAPI/GetCT")]
        [System.Web.Http.HttpPost]
        public ClassTeacher[] GetCT()
        {
            List<ClassTeacher> list = new List<ClassTeacher>();

            try
            {
                var result = (from e in db.tblEmployees
                              where e.Status == true
                              select new
                              {
                                  model = e
                              }).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    ClassTeacher items = new ClassTeacher();
                    items.Name = a.model.FirstName + " " + a.model.MiddleName + " " + a.model.LastName + "(" + a.model.Empcode + ")";
                    items.EmpID = a.model.Empcode;
                    items.ID = a.model.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/MasterAPI/AllocateClassTeacher")]
        [System.Web.Http.HttpPost]
        public ClassTeacher AllocateClassTeacher(ClassTeacher[] val)
        {
            ClassTeacher ct = new Models.ClassTeacher();
            try
            {
                var a = val.SingleOrDefault();

                if (a.ID == 0)
                {
                    var chk = db.tblClassTeacherAllocations.Any(x => x.ClassID == a.ClassID && x.SectionID == a.SectionID && x.Status == 1);
                    if (!chk)
                    {
                        tblClassTeacherAllocation cta = new SchoolErp.tblClassTeacherAllocation();
                        cta.ClassID = a.ClassID;
                        cta.SectionID = a.SectionID;
                        cta.EmpID = a.EmpID;
                        cta.Status = a.Status;
                        cta.intEmpID = a.intEmpID;
                        cta.SchoolID = Convert.ToInt32(a.SchoolID);
                        db.tblClassTeacherAllocations.Add(cta);
                        db.SaveChanges();
                        ct.statusNm = "Allocation completed succesfully";
                    }
                    else
                    {
                        ct.statusNm = "Class Teacher already allocated for this class";
                        ct.ID = -1;
                    }
                }
                else
                {
                    var result = db.tblClassTeacherAllocations.SingleOrDefault(b => b.ID == a.ID);
                    result.EmpID = a.EmpID;
                    result.intEmpID = a.intEmpID;
                    result.Status = a.Status;
                    result.SchoolID = Convert.ToInt32(a.SchoolID);
                    db.SaveChanges();
                    ct.statusNm = "Details Edited succesfully";
                }
                ct.ID = a.ID;
            }
            catch (Exception)
            {
                ct.statusNm = "Some error!! Allocation unsuccessful";
                ct.ID = -1;
                return ct;
            }

            return ct;
        }

        //[System.Web.Http.Route("api/MasterAPI/CheckCTavailability")]
        //[System.Web.Http.HttpPost]
        //public bool CheckCTavailability(List<string> val)
        //{



        //    int empID = Convert.ToInt32(val[0]);
        //    int recordID = Convert.ToInt32(val[1]);
        //    int Course = Convert.ToInt32(val[2]);
        //    int Section = Convert.ToInt32(val[3]);
        //    int SchoolID = Convert.ToInt32(val[4]);
        //    bool chk;

        //    if (recordID == 0)
        //    {
        //        chk = db.tblClassTeacherAllocations.Any(x => x.ClassID == Course && x.SectionID == Section && x.Status == 1 && x.SchoolID == SchoolID);
        //    }
        //    else
        //    {
        //        chk = db.tblClassTeacherAllocations.Any(x => x.ClassID == Course && x.SectionID==Section && x.Status == 1 && x.SchoolID == SchoolID && x.ID==recordID);
        //        if (chk==true )
        //        {
        //            chk = false;
        //        }
        //        else
        //        {
        //            chk = db.tblClassTeacherAllocations.Any(x => x.ClassID == Course && x.SectionID == Section && x.Status == 1 && x.SchoolID == SchoolID);
        //        }
        //    }

        //    return chk;
        //}

        [System.Web.Http.Route("api/MasterAPI/CheckCTavailability")]
        [System.Web.Http.HttpPost]
        public bool CheckCTavailability(List<string> val)
        {
            int empID = Convert.ToInt32(val[0]);
            int recordID = Convert.ToInt32(val[1]);
            int SchoolID = Convert.ToInt32(val[2]);
            bool chk;

            if (recordID == 0)
            {
                chk = db.tblClassTeacherAllocations.Any(x => x.intEmpID == empID && x.Status == 1 && x.SchoolID == SchoolID);
            }
            else
            {
                chk = db.tblClassTeacherAllocations.Any(x => x.intEmpID == empID && x.ID != recordID && x.Status == 1 && x.SchoolID == SchoolID);
            }

            return chk;
        }



        [System.Web.Http.Route("api/MasterAPI/GetAllSchoolDetail")]
        [System.Web.Http.HttpPost]
        public SchoolDetails[] GetAllSchoolDetail()
        {
            sqlHelper obj = new sqlHelper();
            List<SchoolDetails> list = new List<SchoolDetails>();


            DataTable dt = obj.sp_GetDataTableNoParam("Sp_GetSchoolDeatils");
            foreach (DataRow dr in dt.Rows)
            {
                SchoolDetails Sch = new SchoolDetails();

                Sch.ID = dr["ID"].ToString();
                Sch.School = dr["School"].ToString();
                Sch.SchoolCode = dr["SchoolCode"].ToString();
                Sch.Address = dr["Address"].ToString();
                Sch.Board = dr["Board"].ToString();
                Sch.Pincode = dr["Pincode"].ToString();
                Sch.Phone = dr["Phone"].ToString();
                Sch.Fax = dr["Fax"].ToString();
                Sch.Mobile = dr["Mobile"].ToString();
                Sch.Email = dr["Email"].ToString();
                Sch.Website = dr["Website"].ToString();
                Sch.Country = dr["CountryName"].ToString();
                Sch.State = dr["StateName"].ToString();
                Sch.City = dr["CityName"].ToString();
                Sch.District = dr["District"].ToString();


                if (dr["LogoPic"].ToString() == "")
                {

                    Sch.ImageUpload = "/Images/School/SchoolImage/No_Photo_Available.jpg";
                }
                else
                {
                    Sch.ImageUpload = dr["LogoPic"].ToString();
                }

                list.Add(Sch);
            }
            return list.ToArray();
        }



        //[System.Web.Http.Route("api/MasterAPI/deleteSchoolById")]
        //[System.Web.Http.HttpPost]
        //public string deleteSchoolById(string id)
        //{
        //    bool b = AdminMaster.deleteSchoolById(id);
        //    if (b)
        //    {
        //        return "School Deleted Successfully";
        //    }
        //    else
        //    {
        //        return "School Not Deleted Successfully";
        //    }

        //}

    }
}
