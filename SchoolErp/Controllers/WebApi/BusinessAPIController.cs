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
    public class BusinessAPIController : ApiController
    {

        SCHOOLERPEntities db = new SCHOOLERPEntities();

        [System.Web.Http.Route("api/BusinessAPI/SaveRecord")]
        [System.Web.Http.HttpPost]
        public int SaveRecord(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];

                if (a == "NewCustomers")
                {


                    var id = Convert.ToInt32(val[1]);
                    var name = val[2].ToString().Trim();
                    var Cuntact = val[3].ToString().Trim();
                    var Title = val[4].ToString().Trim();
                    var Country = Convert.ToInt32(val[5]);
                    var State = Convert.ToInt32(val[6]);
                    var City = Convert.ToInt32(val[7]);
                    var Group = Convert.ToInt32(val[8]);
                    var Address = val[9].ToString().Trim();
                    var Regin = Convert.ToInt32(val[10]);
                    var Postal = val[11].ToString().Trim();
                    var Phone = val[12].ToString().Trim();
                    var Website = val[13].ToString().Trim();
                    var Fax = val[14].ToString().Trim();
                    var Email = val[15].ToString().Trim();
                    var EmailAddress = val[16].ToString().Trim();
                    var Balance = Convert.ToInt32(val[17]);
                    var loginuser = val[18].ToString().Trim();
                    var dated = DateTime.Now;
                    var active = Convert.ToInt32(val[19]);
                    if (id == 0)
                    {
                        var check = db.Customers.Where(s => s.Name == name && s.ContactName == Cuntact && s.ContactTitle == Title && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Address == Address && s.RegionId == Regin && s.PostalCode == Postal && s.Phone == Phone && s.Fax == Fax && s.Website == Website && s.Email == Email && s.EmailAddress == EmailAddress && s.PreviousCreditBalance == Balance && s.CustomerGroupId == Group && s.InsertUserId == loginuser && s.InsertDate == dated && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Customers cta = new SchoolErp.Customers();
                            cta.Name = name;
                            cta.ContactName = Cuntact;
                            cta.ContactTitle = Title;
                            cta.CountryId = Country;
                            cta.StateId = State;
                            cta.CityId = City;
                            cta.Address = Address;
                            cta.RegionId = Regin;
                            cta.PostalCode = Postal;
                            cta.Phone = Phone;
                            cta.Fax = Fax;
                            cta.Website = Website;
                            cta.Email = Email;
                            cta.EmailAddress = EmailAddress;
                            cta.PreviousCreditBalance = Balance;
                            cta.CustomerGroupId = Group;
                            cta.InsertUserId = loginuser;
                            cta.InsertDate = dated;
                            cta.IsActive = active;

                            db.Customers.Add(cta);
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



                else if (a == "Territories")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Territory = val[2].ToString().Trim();
                    var RegionID = Convert.ToInt32(val[3]);
                    var dated = DateTime.Now;
                    var loginuser = val[4].ToString().Trim();
                    var active = Convert.ToInt32(val[5]);
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
    }
}
