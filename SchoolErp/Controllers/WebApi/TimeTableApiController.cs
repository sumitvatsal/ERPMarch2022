using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SchoolErp.Models;
using System.Data.Entity;
using System.Globalization;
using System.Data;
using LinqKit;
using schoolERP_BLL;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net;
using System.Web;


namespace SchoolErp.Controllers.WebApi
{
    public class TimeTableApiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();




        [System.Web.Http.Route("api/TimeTableApi/SaveTiming")]
        [System.Web.Http.HttpPost]
        //public string SaveTiming(TimeTable[] val)
        //{
        //    TimeTable tt = new Models.TimeTable();
        //    string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        //    SqlConnection con = new SqlConnection(constr);
        //    SqlCommand cmd = null;
        //    con.Open();
        //    try
        //    {

        //        var a = val.SingleOrDefault();
        //        if (a.ViewType == "ClassTiming")
        //        {

        //            if (a.ID == 0)
        //            {

        //                cmd = new SqlCommand("insert into tblClassTiming(Name,Description,Status,SchoolID) values('" + a.ct.Name + "','" + a.ct.Description + "','" + a.ct.Status + "','" + a.ct.SchoolID + "')", con);
        //                cmd.ExecuteNonQuery();
        //                con.Close();

        //               tt.Msg = "Record added succesfully";

        //            }
        //            else
        //            {
        //                cmd = new SqlCommand("update tblClassTiming set Name='" + a.ct.Name + "',Description='" + a.ct.Description + "',Status=" + a.ct.Status + ",SchoolID='" + a.ct.SchoolID + "' where Id=" + a.ID, con);
        //                cmd.ExecuteNonQuery();
        //                con.Close();

        //                tt.Msg = "Record Updated Successfully";
        //            }
        //        }
        //        else if(a.ViewType == "ClassTimingDetails")
        //        {
        //            var availability = db.tblClassTimingDets.Where(s=>s.STime==a.period.STime && s.ETime==a.period.ETime && s.CT_ID==a.period.CT_ID).Any();

        //           var lastVal = db.tblClassTimingDets.OrderByDescending(s => s.ID).FirstOrDefault();
        //            if (lastVal == null && availability == false)
        //            {
        //                DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
        //                DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
        //                if (a.ID == 0)
        //                {


        //                    tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
        //                    d.Name = a.period.Name;
        //                    d.STime = Sttdtt.TimeOfDay;//a.period.STime;
        //                    d.ETime = Enddtt.TimeOfDay;//a.period.ETime;
        //                    d.Status = a.period.Status;
        //                    d.IsBreak = a.period.IsBreak;
        //                    d.CT_ID = a.period.CT_ID;
        //                    db.tblClassTimingDets.Add(d);
        //                    db.SaveChanges();
        //                    tt.ID = d.ID;
        //                    tt.Msg = "Record added succesfully";
        //                }
        //            }

        //            TimeSpan LastTime = lastVal.ETime;
        //            double seconds = TimeSpan.Parse(LastTime.ToString()).TotalSeconds;

        //            DateTime ThisTimeVal = timeTo24Hrs(a.period.StartTime);

        //            double ThisTimeSecds = ThisTimeVal.TimeOfDay.TotalSeconds;

        //            if (ThisTimeSecds > seconds)
        //            {
        //                if (availability == false)
        //                {
        //                    DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
        //                    DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
        //                    if (a.ID == 0)
        //                    {


        //                        tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
        //                        d.Name = a.period.Name;
        //                        d.STime = Sttdtt.TimeOfDay;//a.period.STime;
        //                        d.ETime = Enddtt.TimeOfDay;//a.period.ETime;
        //                        d.Status = a.period.Status;
        //                        d.IsBreak = a.period.IsBreak;
        //                        d.CT_ID = a.period.CT_ID;
        //                        db.tblClassTimingDets.Add(d);
        //                        db.SaveChanges();
        //                        tt.ID = d.ID;
        //                        tt.Msg = "Record added succesfully";
        //                    }
        //                    else
        //                    {
        //                        var result = db.tblClassTimingDets.SingleOrDefault(b => b.ID == a.ID);
        //                        result.Name = a.period.Name;
        //                        result.Status = a.period.Status;
        //                        result.STime = Sttdtt.TimeOfDay;
        //                        result.ETime = Enddtt.TimeOfDay;
        //                        result.IsBreak = a.period.IsBreak;
        //                        result.CT_ID = a.period.CT_ID;
        //                        db.SaveChanges();
        //                        tt.ID = result.ID;
        //                        tt.Msg = "Record edited succesfully";
        //                    }
        //                }
        //                else
        //                {
        //                    tt.Msg = "This Timming Already aloted Please Check It";
        //                }
        //            }
        //            else
        //            {
        //                tt.Msg = "Specified Time Already Exist In TimeTable";
        //            }
        //        }
        //        else if(a.ViewType == "TimeTableConfigCreate")
        //        {
        //           tt= SaveTimeTableConfig(a);
        //        }
        //        else if(a.ViewType == "CreateTimeTable")
        //        {
        //            tt = SaveTTPeriod(a);
        //        }
        //        else if(a.ViewType == "TimeTableConfig")
        //        {
        //            var d = db.tblTimeTableConfigs.SingleOrDefault(b => b.ID == a.ID);
        //            d.Status = a.Status;
        //            db.SaveChanges();
        //            tt.ID = d.ID;
        //            tt.Msg = "Record edited succesfully";
        //        }
        //        else if (a.ViewType == "TeacherSubjectAllocation")
        //        {
        //            tt = SaveSubTeacherDet(a);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tt.Msg = "Some error!! process unsuccessful";
        //        tt.ID = -1;
        //        return "";
        //        throw ex;
        //    }

        //    return "";
        //}


        public TimeTable SaveTiming(TimeTable[] val)
        {
            TimeTable tt = new Models.TimeTable();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = null;
            con.Open();
            try
            {
                var a = val.SingleOrDefault();
                if (a.ViewType == "ClassTiming")
                {

                    if (a.ID == 0)
                    {

                        var Check = db.tblClassTimings.Where(s => s.Name == a.ct.Name && s.SchoolID == a.ct.SchoolID && s.IsDeleted == null).FirstOrDefault();
                        if (Check == null)
                        {
                            cmd = new SqlCommand("insert into tblClassTiming(Name,Description,Status,SchoolID,ClassId,SectionId) values('" + a.ct.Name + "','" + a.ct.Description + "','" + a.ct.Status + "','" + a.ct.SchoolID + "','" + a.ct.ClassId + "','" + a.ct.SectionId + "')", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            tt.Msg = "Class Timing added succesfully";
                        }
                        else
                        {
                            tt.Msg = "Class Timing with same name already Exist";
                        }



                    }
                    else
                    {
                        //int idd = Convert.ToInt32(a.ID);

                        var Check = db.tblClassTimings.Where(x => x.Name == a.ct.Name && x.ID != a.ID && x.SchoolID == a.ct.SchoolID && x.IsDeleted == null).FirstOrDefault();
                        if (Check == null)
                        {
                            cmd = new SqlCommand("update tblClassTiming set Name='" + a.ct.Name + "',Description='" + a.ct.Description + "',Status=" + a.ct.Status + ",SchoolID='" + a.ct.SchoolID + "' where Id=" + a.ID, con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            tt.Msg = "Class Timing Updated Successfully";
                        }
                        else
                        {
                            tt.Msg = "Class Timing with same name already Exist";

                        }

                    }
                }
                else if (a.ViewType == "ClassTimingDetails")
                {
                    if (a.ID != 0)
                    {
                        var Editval = db.tblClassTimingDets.Where(p => p.ID == a.ID && p.SchoolID == a.period.SchoolID).FirstOrDefault();
                        if (Editval != null)
                        {

                            Editval.IsDeleted = 1;
                            db.SaveChanges();

                            DateTime Sttdttt = timeTo24Hrs(a.period.StartTime);
                            DateTime Enddttt = timeTo24Hrs(a.period.EndTime);
                            TimeSpan STARTtime = Sttdttt.TimeOfDay;
                            TimeSpan ENDtime = Enddttt.TimeOfDay;


                            var starttimeavailability = db.tblClassTimingDets.Where(s => s.STime <= STARTtime && s.ETime >= STARTtime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted == null).Any();
                            var Endtimeavailability = db.tblClassTimingDets.Where(s => s.STime <= ENDtime && s.ETime >= ENDtime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted == null).Any();
                            var fulltimetimeavailability = db.tblClassTimingDets.Where(s => s.STime >= STARTtime && s.ETime <= ENDtime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted == null).Any();
                            if (starttimeavailability != false)
                            {
                                var Editval1 = db.tblClassTimingDets.Where(p => p.ID == a.ID && p.SchoolID == a.period.SchoolID).FirstOrDefault();
                                if (Editval != null)
                                {

                                    Editval.IsDeleted = null;
                                    db.SaveChanges();
                                    tt.Msg = "This Timming Already alloted Please Check It";
                                }



                            }

                            else if (Endtimeavailability != false)
                            {
                                var Editval1 = db.tblClassTimingDets.Where(p => p.ID == a.ID && p.SchoolID == a.period.SchoolID).FirstOrDefault();
                                if (Editval != null)
                                {

                                    Editval.IsDeleted = null;
                                    db.SaveChanges();
                                    tt.Msg = "This Timming Already alloted Please Check It";
                                }


                            }

                            else if (fulltimetimeavailability != false)
                            {
                                var Editval1 = db.tblClassTimingDets.Where(p => p.ID == a.ID && p.SchoolID == a.period.SchoolID).FirstOrDefault();
                                if (Editval != null)
                                {

                                    Editval.IsDeleted = null;
                                    db.SaveChanges();
                                    tt.Msg = "This Timming Already alloted Please Check It";
                                }

                            }
                            else
                            {
                                DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
                                DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
                                tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
                                DateTime Sttdtt1 = Sttdtt.AddMinutes(1);
                                DateTime Enddtt2 = Enddtt.AddMinutes(-1);
                                d.SubjectId = Convert.ToInt32(a.period.Name);
                                d.STime = Sttdtt1.TimeOfDay;//a.period.STime;
                                d.ETime = Enddtt2.TimeOfDay;//a.period.ETime;
                                d.Status = a.period.Status;
                                d.IsBreak = a.period.IsBreak;
                                d.CT_ID = a.period.CT_ID;
                                d.SchoolID = a.period.SchoolID;
                                d.TeacherId = a.period.TeacherId;
                                d.WeekDays = a.period.WeekDays;
                                d.ClassId = a.period.ClassId;
                                d.SectionId = a.period.SectionId;

                                db.tblClassTimingDets.Add(d);
                                db.SaveChanges();
                                tt.ID = d.ID;
                                tt.Msg = "Record added succesfully";


                            }



                        }


                        //var availability = db.tblClassTimingDets.Where(s => s.STime < a.period.STime && s.ETime > a.period.STime).Any();

                    }
                    else if (a.ID == 0)
                    {
                        var lastVal = db.tblClassTimingDets.Where(s => s.SchoolID == a.period.SchoolID && s.IsDeleted == null).OrderByDescending(s => s.ID).FirstOrDefault();
                        if (lastVal != null)
                        {
                            //var availability = db.tblClassTimingDets.Where(s => s.STime < a.period.STime && s.ETime > a.period.STime).Any();
                            DateTime Sttdttt = timeTo24Hrs(a.period.StartTime);
                            DateTime Enddttt = timeTo24Hrs(a.period.EndTime);
                            TimeSpan STARTtime = Sttdttt.TimeOfDay;
                            TimeSpan ENDtime = Enddttt.TimeOfDay;


                            //var starttimeavailability = db.tblClassTimingDets.Where(s => s.STime <= STARTtime && s.ETime >= STARTtime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted == null && s.WeekDays==a.period.WeekDays).Any();
                            //var Endtimeavailability = db.tblClassTimingDets.Where(s => s.STime <= ENDtime && s.ETime >= ENDtime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted == null && s.WeekDays == a.period.WeekDays).Any();
                            //var fulltimetimeavailability = db.tblClassTimingDets.Where(s => s.STime >= STARTtime && s.ETime <= ENDtime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted == null && s.WeekDays == a.period.WeekDays).Any();
                            var starttimeavailability = db.tblClassTimingDets.Where(s => s.STime <= STARTtime && s.ETime >= STARTtime && s.TeacherId == a.period.TeacherId && s.SchoolID == a.period.SchoolID && s.IsDeleted == null && s.WeekDays == a.period.WeekDays).Any();
                            var Endtimeavailability = db.tblClassTimingDets.Where(s => s.STime <= ENDtime && s.ETime >= ENDtime && s.TeacherId == a.period.TeacherId && s.SchoolID == a.period.SchoolID && s.IsDeleted == null && s.WeekDays == a.period.WeekDays).Any();
                            var fulltimetimeavailability = db.tblClassTimingDets.Where(s => s.STime >= STARTtime && s.ETime <= ENDtime && s.TeacherId == a.period.TeacherId && s.SchoolID == a.period.SchoolID && s.IsDeleted == null && s.WeekDays == a.period.WeekDays).Any();

                            if (starttimeavailability != false)
                            {
                                tt.Msg = "This Timming Already alloted Please Check It";
                            }

                            else if (Endtimeavailability != false)
                            {
                                tt.Msg = "This Timming Already alloted Please Check It";

                            }

                            else if (fulltimetimeavailability != false)
                            {
                                tt.Msg = "This Timming Already alloted Please Check It";
                            }
                            else
                            {
                                DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
                                DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
                                tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
                                DateTime Sttdtt1 = Sttdtt.AddMinutes(1);
                                DateTime Enddtt2 = Enddtt.AddMinutes(-1);
                                d.SubjectId = Convert.ToInt32(a.period.Name);
                                d.Name = a.period.Name;
                                d.STime = Sttdtt1.TimeOfDay;//a.period.STime;
                                d.ETime = Enddtt2.TimeOfDay;//a.period.ETime;
                                d.Status = a.period.Status;
                                d.IsBreak = a.period.IsBreak;
                                d.CT_ID = a.period.CT_ID;
                                d.SchoolID = a.period.SchoolID;
                                d.TeacherId = a.period.TeacherId;
                                d.WeekDays = a.period.WeekDays;
                                db.tblClassTimingDets.Add(d);
                                db.SaveChanges();
                                tt.ID = d.ID;
                                tt.Msg = "Record added succesfully";
                            }

                        }
                        else if (lastVal == null)
                        {
                            DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
                            DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
                            tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
                            DateTime Sttdtt1 = Sttdtt.AddMinutes(1);
                            DateTime Enddtt2 = Enddtt.AddMinutes(-1);
                            d.Name = a.period.Name;
                            d.SubjectId = Convert.ToInt32(a.period.Name);

                            d.STime = Sttdtt1.TimeOfDay;//a.period.STime;
                            d.ETime = Enddtt2.TimeOfDay;//a.period.ETime;
                            d.Status = a.period.Status;
                            d.IsBreak = a.period.IsBreak;
                            d.CT_ID = a.period.CT_ID;
                            d.SchoolID = a.period.SchoolID;
                            d.TeacherId = a.period.TeacherId;
                            d.WeekDays = a.period.WeekDays;
                            db.tblClassTimingDets.Add(d);
                            db.SaveChanges();
                            tt.ID = d.ID;
                            tt.Msg = "Record added succesfully";
                        }


                    }


                    //var availability = db.tblClassTimingDets.Where(s => s.STime == a.period.STime && s.ETime == a.period.ETime && s.CT_ID == a.period.CT_ID && s.SchoolID == a.period.SchoolID && s.IsDeleted ==null).Any();

                    //var lastVal = db.tblClassTimingDets.Where (s=>s.SchoolID == a.period.SchoolID && s.IsDeleted==null).OrderByDescending(s => s.ID ).FirstOrDefault();

                    //if (lastVal == null && availability == false)
                    //{
                    //    DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
                    //    DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
                    //    if (a.ID == 0)
                    //    {


                    //        tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
                    //        d.Name = a.period.Name;
                    //        d.STime = Sttdtt.TimeOfDay;//a.period.STime;
                    //        d.ETime = Enddtt.TimeOfDay;//a.period.ETime;
                    //        d.Status = a.period.Status;
                    //        d.IsBreak = a.period.IsBreak;
                    //        d.CT_ID = a.period.CT_ID;
                    //        d.SchoolID = a.period.SchoolID;

                    //        db.tblClassTimingDets.Add(d);
                    //        db.SaveChanges();
                    //        tt.ID = d.ID;
                    //        tt.Msg = "Record added succesfully";
                    //    }
                    //}
                    //if (lastVal == null) { }
                    //else
                    //{
                    //    TimeSpan? LastTime = lastVal.ETime;
                    //    if (lastVal.ETime == null)
                    //    {

                    //    }

                    //    else
                    //    {
                    //        double seconds = TimeSpan.Parse(LastTime.ToString()).TotalSeconds;

                    //        DateTime ThisTimeVal = timeTo24Hrs(a.period.StartTime);

                    //        double ThisTimeSecds = ThisTimeVal.TimeOfDay.TotalSeconds;
                    //        if (a.ID !=0)
                    //        {


                    //        }
                    //        if (ThisTimeSecds >= seconds )
                    //        {
                    //            if (availability == false)
                    //            {
                    //                DateTime Sttdtt = timeTo24Hrs(a.period.StartTime);
                    //                DateTime Enddtt = timeTo24Hrs(a.period.EndTime);
                    //                if (a.ID == 0)
                    //                {


                    //                    tblClassTimingDet d = new SchoolErp.tblClassTimingDet();
                    //                    d.Name = a.period.Name;
                    //                    d.STime = Sttdtt.TimeOfDay;//a.period.STime;
                    //                    d.ETime = Enddtt.TimeOfDay;//a.period.ETime;
                    //                    d.Status = a.period.Status;
                    //                    d.IsBreak = a.period.IsBreak;
                    //                    d.CT_ID = a.period.CT_ID;
                    //                    d.SchoolID = a.period.SchoolID;
                    //                    db.tblClassTimingDets.Add(d);
                    //                    db.SaveChanges();
                    //                    tt.ID = d.ID;
                    //                    tt.Msg = "Record added succesfully";
                    //                }
                    //                else
                    //                {
                    //                    var result = db.tblClassTimingDets.SingleOrDefault(b => b.ID == a.ID);
                    //                    result.Name = a.period.Name;
                    //                    result.Status = a.period.Status;
                    //                    result.STime = Sttdtt.TimeOfDay;
                    //                    result.ETime = Enddtt.TimeOfDay;
                    //                    result.IsBreak = a.period.IsBreak;
                    //                    result.CT_ID = a.period.CT_ID;
                    //                    result.SchoolID = a.period.SchoolID;
                    //                    db.SaveChanges();
                    //                    tt.ID = result.ID;
                    //                    tt.Msg = "Record edited succesfully";
                    //                }
                    //            }
                    //            else
                    //            {
                    //                tt.Msg = "This Timming Already aloted Please Check It";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            tt.Msg = "Specified Time Already Exist In TimeTable";
                    //        }
                    //    }
                    //}
                }
                else if (a.ViewType == "TimeTableConfigCreate")
                {
                    tt = SaveTimeTableConfig(a);
                }
                else if (a.ViewType == "TimeTableConfigCreateNEW")
                {
                    tt = TimeTableConfigCreateNEW(a);
                }
                else if (a.ViewType == "CreateTimeTable")
                {
                    tt = SaveTTPeriod(a);
                }
                else if (a.ViewType == "TimeTableConfig")
                {
                    var d = db.tblTimeTableConfigs.SingleOrDefault(b => b.ID == a.ID);
                    d.Status = a.Status;
                    db.SaveChanges();
                    tt.ID = d.ID;
                    tt.Msg = "Time Table Status edited succesfully";
                }
                else if (a.ViewType == "TeacherSubjectAllocation")
                {
                    tt = SaveSubTeacherDet(a);
                }
                else if (a.ViewType == "checkSubteachersub")
                {
                    tt = checksubteachersub(a);
                }
            }
            catch (Exception ex)
            {
                tt.Msg = "Some error!! process unsuccessful";
                tt.ID = -1;

                throw ex;
            }
            finally
            {
                con.Close();
            }
            return tt;
        }

        public DateTime timeTo24Hrs(string TimeFormat)
        {
            DateTime dt = DateTime.Parse(TimeFormat);
            return dt;
        }

        public TimeTable TimeTableConfigCreateNEW(TimeTable a)
        {
            TimeTable TTNEW = new Models.TimeTable();

            return TTNEW;
        }

        public TimeTable SaveTimeTableConfig(TimeTable a)
        {
            TimeTable tt = new Models.TimeTable();
            if (a.ID == 0)
            {


                tblTimeTableConfig d = new SchoolErp.tblTimeTableConfig();
                d.Name = a.con.Name;

                var Check = db.tblTimeTableConfigs.Where(s => s.Name == a.con.Name && s.SchoolID == a.con.SchoolID && s.IsDeleted == null).FirstOrDefault();
                if (Check == null)
                {
                    var check2 = db.tblTimeTableConfigs.Where(x => x.ClassID == a.con.ClassID && x.SectionID == a.con.SectionID && x.SchoolID == a.con.SchoolID && x.IsDeleted == null).FirstOrDefault();
                    if (check2 != null)
                    {
                        check2.IsDeleted = 1;
                        check2.Deleted_on = DateTime.Now;
                        db.SaveChanges();
                        d.AcademicYear = a.con.AcademicYear;
                        d.ClassID = a.con.ClassID;
                        d.CT_ID = a.con.CT_ID;
                        d.EndDt = a.con.EndDt;
                        d.StartDT = a.con.StartDT;
                        d.Status = a.con.Status;
                        d.WeekDays = a.con.WeekDays;
                        d.SectionID = a.con.SectionID;
                        d.SchoolID = a.con.SchoolID;
                        db.tblTimeTableConfigs.Add(d);

                        db.SaveChanges();
                        tt.ID = d.ID;
                        tt.Msg = "Record added succesfully";
                        if (d.WeekDays != "")
                        {
                            var CTDET = db.tblClassTimingDets.Where(x => x.CT_ID == d.CT_ID && x.IsDeleted == null).ToList();
                            var ar = d.WeekDays.Split(',');
                            foreach (var c in CTDET)
                            {
                                foreach (string s in ar)
                                {
                                    tblTimeTable t = new tblTimeTable();
                                    t.CTDet_ID = c.ID;
                                    t.CT_ID = d.CT_ID;
                                    t.TimeTableID = d.ID;
                                    t.WeekDay = s;
                                    t.SchoolID = d.SchoolID;
                                    db.tblTimeTables.Add(t);
                                    db.SaveChanges();
                                }
                            }

                        }
                    }
                    else
                    {
                        d.AcademicYear = a.con.AcademicYear;
                        d.ClassID = a.con.ClassID;
                        d.CT_ID = a.con.CT_ID;
                        d.EndDt = a.con.EndDt;
                        d.StartDT = a.con.StartDT;
                        d.Status = a.con.Status;
                        d.WeekDays = a.con.WeekDays;
                        d.SectionID = a.con.SectionID;
                        d.SchoolID = a.con.SchoolID;
                        db.tblTimeTableConfigs.Add(d);

                        db.SaveChanges();
                        tt.ID = d.ID;
                        tt.Msg = "TimeTable added Successfully";
                        if (d.WeekDays != "")
                        {
                            var CTDET = db.tblClassTimingDets.Where(x => x.CT_ID == d.CT_ID && x.IsDeleted == null).ToList();
                            var ar = d.WeekDays.Split(',');
                            foreach (var c in CTDET)
                            {
                                foreach (string s in ar)
                                {
                                    tblTimeTable t = new tblTimeTable();
                                    t.CTDet_ID = c.ID;
                                    t.CT_ID = d.CT_ID;
                                    t.TimeTableID = d.ID;
                                    t.WeekDay = s;
                                    t.SchoolID = d.SchoolID;
                                    db.tblTimeTables.Add(t);
                                    db.SaveChanges();
                                }
                            }

                        }

                    }


                }
                else
                {
                    tt.Msg = "Time table name already exist";
                    tt.ID = -1;
                }

            }
            else
            {
                var check = db.tblTimeTableConfigs.Where(s => s.Name == a.con.Name && s.ID != a.ID && s.IsDeleted == null).FirstOrDefault();
                if (check == null)
                {
                    var d = db.tblTimeTableConfigs.SingleOrDefault(b => b.ID == a.ID);
                    d.Name = a.con.Name;
                    d.AcademicYear = a.con.AcademicYear;
                    d.ClassID = a.con.ClassID;
                    d.SectionID = a.con.SectionID;
                    d.CT_ID = a.con.CT_ID;
                    d.EndDt = a.con.EndDt;
                    d.StartDT = a.con.StartDT;
                    d.Status = a.con.Status;
                    d.WeekDays = a.con.WeekDays;
                    d.SchoolID = a.con.SchoolID;
                    db.SaveChanges();
                    tt.ID = d.ID;
                    tt.Msg = "TimeTable edited Successfully ";
                }
                else
                {
                    tt.Msg = "Time table name already exist";
                    tt.ID = -1;
                }

            }
            return tt;
        }

        public TimeTable SaveTTPeriod(TimeTable a)
        {
            TimeTable tt = new Models.TimeTable();

            var d = db.tblTimeTables.SingleOrDefault(b => b.ID == a.ID && b.IsDeleted == null);
            d.SubjectID = a.tt.SubjectID;
            d.TeacherID = a.tt.TeacherID;
            d.Status = a.tt.Status;
            db.SaveChanges();
            tt.ID = d.ID;
            tt.Msg = "Time Table Teacher Allocated succesfully";

            return tt;
        }

        public TimeTable SaveSubTeacherDet(TimeTable a)
        {
            TimeTable tt = new Models.TimeTable();
            if (a.ID == 1)
            {
                int empid = Convert.ToInt32(a.TeacherName);
                var users = db.tblSubjectTeacherAllocates.Where(u => u.TeacherID == empid && u.IsDeleted == null);

                foreach (var u in users)
                {
                    db.tblSubjectTeacherAllocates.Remove(u);
                }
                tt.Msg = "Record edited succesfully";
            }
            else
            {
                tt.Msg = "Record added succesfully";
            }
            if (a.subject != "")
            {
                if (a.ID == 1)
                {
                    var ar = a.subject.Split(',');
                    string message = "";
                    foreach (string s in ar)
                    {
                        long subID = Convert.ToInt32(s);
                        tblSubjectTeacherAllocate d = new SchoolErp.tblSubjectTeacherAllocate();
                        d.Status = a.Status == 1 ? true : false;
                        d.SubjectID = subID;
                        d.TeacherID = Convert.ToInt32(a.TeacherName);
                        d.SchoolID = Convert.ToInt32(a.SchoolID);
                        d.classid = Convert.ToInt32(a.Classid);
                        d.SectionId = Convert.ToInt32(a.SectionId);
                        db.tblSubjectTeacherAllocates.Add(d);
                    }
                }
                else
                {
                    var ar = a.subject.Split(',');

                    string message = "";
                    foreach (string s in ar)
                    {
                        long subID = Convert.ToInt32(s);
                        int teacheridd = Convert.ToInt32(a.TeacherName);
                        int classidd = Convert.ToInt32(a.Classid);
                        int SchoolIDD = Convert.ToInt32(a.SchoolID);
                        int SectionId = Convert.ToInt32(a.SectionId);
                        var avi = db.tblSubjectTeacherAllocates.Where(x => x.SubjectID == subID && x.TeacherID == teacheridd && x.classid == classidd && x.IsDeleted == null && x.SchoolID == SchoolIDD && x.SectionId == SectionId).FirstOrDefault();
                        if (avi == null)
                        {
                            tblSubjectTeacherAllocate d = new SchoolErp.tblSubjectTeacherAllocate();
                            d.Status = a.Status == 1 ? true : false;
                            d.SubjectID = subID;
                            d.TeacherID = Convert.ToInt32(a.TeacherName);
                            d.SchoolID = Convert.ToInt32(a.SchoolID);
                            d.classid = Convert.ToInt32(a.Classid);
                            d.SectionId = Convert.ToInt32(a.SectionId);
                            db.tblSubjectTeacherAllocates.Add(d);
                        }
                        else
                        {
                            var sub = db.tblSubjects.Where(x => x.SchoolID == SchoolIDD && x.ID == subID && x.classid == classidd && x.IsDeleted == null).FirstOrDefault();
                            //message = sub.Subject;
                            message = message + sub.Subject + ",";
                            tt.Msg = message + "Already Exist for Same Teacher";

                        }

                    }
                }

            }
            db.SaveChanges();
            return tt;
        }

        public TimeTable checksubteachersub(TimeTable a)
        {
            TimeTable tt = new Models.TimeTable();
            if (a.subject != "" && a.subject != null)
            {
                var ar = a.subject.Split(',');
                int tid = Convert.ToInt32(a.TeacherName);
                int SchoolID = Convert.ToInt32(a.SchoolID);
                int classid = Convert.ToInt32(a.Classid);
                string avi = "";
                foreach (string s in ar)
                {
                    long subID = Convert.ToInt32(s);
                    var check = db.tblSubjectTeacherAllocates.Where(x => x.TeacherID == tid && x.SchoolID == SchoolID && x.classid == classid && x.IsDeleted == null && x.SubjectID == subID).FirstOrDefault();
                    if (check != null)
                    {
                        var sub = db.tblSubjects.Where(x => x.ID == check.SubjectID && x.IsDeleted == null && x.SchoolID == SchoolID).FirstOrDefault();
                        avi = avi + sub.Subject + ",";
                    }

                }
                if (avi != "")
                {
                    tt.Msg1 = avi;

                }
                else
                {
                    tt.Msg1 = "0";
                }
            }

            return tt;
        }

        [System.Web.Http.Route("api/TimeTableApi/getClassTimings")]
        [System.Web.Http.HttpPost]
        public TimeTable[] getClassTimings(List<string> val)
        {
            List<TimeTable> list = new List<Models.TimeTable>();
            string loginuser = val[5];
            int typeuser = Convert.ToInt32(val[6]);

            try
            {

                if (typeuser == 2)
                {
                    int count = 0;
                    var result = (from c in db.tblClassTimings
                                  join ac in db.tblSchoolDetails on c.SchoolID equals ac.ID

                                  where c.IsDeleted == null
                                  select new
                                  {
                                      model = c,
                                      SchoolName = ac.School




                                  }).ToList();

                    if (val[0] != "default")
                    {
                        int status = Convert.ToInt32(val[3]);
                        string name = val[1];
                        string desc = val[2];
                        int School = Convert.ToInt32(val[4]);
                        if (val[0] == "status")
                        {


                            result = result.Where(s => s.model.Status == status).ToList();
                            //if(!String.IsNullOrEmpty(name) && String.IsNullOrEmpty(desc))
                            //{
                            //    name = name.ToUpper();
                            //    result = result.Where(s => s.model.Name.ToUpper().Contains(name)).ToList();
                            //}
                            //else if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(desc))
                            //{
                            //    desc = desc.ToUpper();
                            //    result = result.Where(s => s.model.Description.ToUpper().Contains(desc)).ToList();
                            //}
                            //else
                            //{
                            //    name = name.ToUpper(); desc = desc.ToUpper();
                            //    result = result.Where(s => s.model.Description.ToUpper().Contains(desc) && s.model.Name.ToUpper().Contains(name)).ToList();
                            //}
                        }
                        else if (val[0] == "name")
                        {
                            name = name.ToUpper();
                            result = result.Where(s => s.model.Name.ToUpper().Contains(name)).ToList();
                        }
                        else if (val[0] == "desc")
                        {
                            desc = desc.ToUpper();
                            result = result.Where(s => s.model.Description.ToUpper().Contains(desc)).ToList();
                        }
                        else if (val[0] == "School")
                        {

                            result = result.Where(s => s.model.SchoolID == School).ToList();

                        }
                    }
                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblClassTiming cls = new tblClassTiming();
                        ct.CountID = count;
                        ct.ID = m.model.ID;
                        cls.ID = m.model.ID;
                        cls.Name = m.model.Name;
                        ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                        cls.Description = m.model.Description;
                        cls.Status = m.model.Status;
                        ct.ct = cls;
                        if (ct.ct.Status == 0)
                        {
                            ct.StatusNm = "DeActive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "DeActive";
                        }
                        ct.School = m.SchoolName;
                        cls.SchoolID = m.model.SchoolID;
                        list.Add(ct);

                    }
                }
                else
                {
                    int count = 0;
                    var result = (from c in db.tblClassTimings
                                  join ac in db.tblSchoolDetails on c.SchoolID equals ac.ID
                                  join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                                  join cl in db.tblCourses on c.ClassId equals cl.Id
                                  join se in db.tblSections on c.SectionId equals se.Id

                                  where em.UserID == loginuser
                                  //&& em.IsDeleted==null && c.IsDeleted == null 
                                  select new
                                  {
                                      model = c,
                                      SchoolName = ac.School,
                                      ClassName = cl.CourseName,
                                      SectionName = se.SectionName,
                                      CLassId = cl.Id,
                                      SectionId = se.Id
                                  }).ToList();

                    if (val[0] != "default")
                    {
                        int status = Convert.ToInt32(val[3]);
                        string name = val[1];
                        string desc = val[2];
                        int School = Convert.ToInt32(val[4]);
                        int ClassId = Convert.ToInt32(val[8]);
                        int Section = Convert.ToInt32(val[9]);
                        if (val[0] == "status")
                        {
                            result = result.Where(s => s.model.Status == status).ToList();

                        }
                        else if (val[0] == "name")
                        {
                            name = name.ToUpper();
                            result = result.Where(s => s.model.Name.ToUpper().Contains(name)).ToList();
                        }
                        else if (val[0] == "desc")
                        {
                            desc = desc.ToUpper();
                            result = result.Where(s => s.model.Description.ToUpper().Contains(desc)).ToList();
                        }
                        else if (val[0] == "School")
                        {

                            result = result.Where(s => s.model.SchoolID == School).ToList();

                        }
                        else if (val[0] == "ClassName")
                        {

                            result = result.Where(s => s.model.ClassId == ClassId).ToList();

                        }
                        else if (val[0] == "SectionName")
                        {

                            result = result.Where(s => s.model.SectionId == Section).ToList();

                        }
                    }
                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblClassTiming cls = new tblClassTiming();
                        ct.CountID = count;
                        ct.ID = m.model.ID;
                        cls.ID = m.model.ID;
                        cls.Name = m.model.Name;
                        ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                        cls.Description = m.model.Description;
                        cls.Status = m.model.Status;
                        cls.SchoolID = m.model.SchoolID;
                        //cls.ClassName = m.model.ClassName;
                        //cls.SectionName = m.model.SectionName;
                        ct.ct = cls;
                        if (ct.ct.Status == 0)
                        {
                            ct.StatusNm = "DeActive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "DeActive";
                        }
                        ct.School = m.SchoolName;
                        ct.classname = Convert.ToString(m.ClassName);
                        ct.SectionName = Convert.ToString(m.SectionName);
                        cls.SchoolID = m.model.SchoolID;
                        cls.ClassId = m.model.ClassId;
                        cls.SectionId = m.model.SectionId;
                        list.Add(ct);

                    }
                }

            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/DeleteRecord")]
        [System.Web.Http.HttpPost]
        public TimeTable DeleteRecord(List<string> val)
        {
            int id = Convert.ToInt32(val[0]);
            TimeTable std = new Models.TimeTable();
            string type = val[1].ToString().Trim();
            try
            {
                if (type == "ClassTiming")
                {
                    //var employer = new tblClassTiming { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;
                    var aa = db.tblClassTimings.SingleOrDefault(x => x.ID == id);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;
                    db.SaveChanges();
                    var bb = db.tblClassTimingDets.Where(c => c.CT_ID == id).ToList();
                    bb.ForEach(cc =>
                    {
                        cc.IsDeleted = 1;
                        cc.Deleted_on = DateTime.Now;
                    });
                    db.SaveChanges();
                }
                else if (type == "ClassTimingDetails")
                {
                    //var employer = new tblClassTimingDet { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;
                    var aa = db.tblClassTimingDets.SingleOrDefault(x => x.ID == id);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;
                    db.SaveChanges();

                }
                else if (type == "CreateTimeTable")
                {
                    long ttID = Convert.ToInt32(val[0]);
                    var d = db.tblTimeTables.SingleOrDefault(b => b.ID == ttID);
                    d.SubjectID = null;
                    d.TeacherID = null;
                    d.Status = 0;
                    //db.SaveChanges();
                }
                else if (type == "TimeTableConfig")
                {
                    //var employer = new tblTimeTableConfig { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;
                    var aa = db.tblTimeTableConfigs.SingleOrDefault(x => x.ID == id);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;

                    db.Database.ExecuteSqlCommand("Update tblTimeTable set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' WHERE TimeTableID = {0}", id);
                    //var users = db.tblTimeTables.Where(u => u.TimeTableID == id);

                    //foreach (var u in users)
                    //{
                    //    db.tblTimeTables.Remove(u);
                    //}
                }
                else if (type == "TeacherSubjectAllocation")
                {
                    //var employer = new tblSubjectTeacherAllocate { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;
                    var aa = db.tblSubjectTeacherAllocates.SingleOrDefault(x => x.ID == id);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;


                }

                db.SaveChanges();
                std.Msg = "Record successfully deleted";
                return std;
            }
            catch (Exception e)
            {
                std.Msg = "Error!!";
                return std;
                throw e;
            }
        }

        [System.Web.Http.Route("api/TimeTableApi/GetClassTiming")]
        [System.Web.Http.HttpPost]
        public TimeTable GetClassTiming(List<string> val)
        {
            TimeTable ct = new Models.TimeTable();
            try
            {
                int id = Convert.ToInt32(val[0]);
                var result = (from c in db.tblClassTimings
                              where c.ID == id
                              select new
                              {
                                  c
                              }).SingleOrDefault();


                tblClassTiming cls = new tblClassTiming();
                ct.ID = result.c.ID;
                cls.ID = result.c.ID;
                cls.Name = result.c.Name;
                cls.Description = result.c.Description;
                cls.Status = result.c.Status;
                ct.ct = cls;
                if (ct.ct.Status == 0)
                {
                    ct.StatusNm = "Inactive";
                    ct.style = "btn btn-block btn-danger btn-sm";
                    ct.action = "Activate";
                }
                else
                {
                    ct.StatusNm = "Active";
                    ct.style = "btn btn-block btn-success btn-sm";
                    ct.action = "Inactivate";
                }
            }

            catch (Exception ex) { ct.ID = -1; throw ex; }
            return ct;

        }

        [System.Web.Http.Route("api/TimeTableApi/GetClassTimingDet")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetClassTimingDet(List<string> val)
        {
            string loginuser = val[1];
            int typeuser = Convert.ToInt32(val[2]);
            int SchoolId = Convert.ToInt32(val[3]);
            List<TimeTable> list = new List<Models.TimeTable>();
            long id = Convert.ToInt32(val[0]);
            try
            {
                if (typeuser == 2)
                {
                    int count = 0;
                    var result = (from c in db.tblClassTimingDets
                                  join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                                  join ct in db.tblClassTimings on c.CT_ID equals ct.ID
                                  join cl in db.tblCourses on ct.ClassId equals cl.Id
                                  join se in db.tblSections on ct.SectionId equals se.Id
                                  join em in db.tblEmployees on c.TeacherId equals em.Id

                                  where c.CT_ID == id && c.IsDeleted == null

                                  select new
                                  {
                                      model = c,
                                      SchoolName = s.School,
                                      ClassName = cl.CourseName,
                                      SectionName = se.SectionName,
                                      TeacherName = em.FirstName,
                                      TeacherId = em.Id

                                  }).ToList();

                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblClassTimingDet cls = new tblClassTimingDet();
                        DateTime SsdtTime = DateTime.Now.Date + m.model.STime;
                        DateTime EddtTime = DateTime.Now.Date + m.model.ETime;
                        // hh for 12 hour clock



                        ct.CountID = count;
                        cls.ID = m.model.ID;
                        //cls.Name = m.model.Name;
                        //cls.StartTime = SsdtTime.ToString("hh:mm:ss tt ");
                        //cls.EndTime = EddtTime.ToString("hh:mm:ss tt ");
                        //cls.Status = m.model.Status;
                        //cls.IsBreak = m.model.IsBreak;
                        //ct.period = cls;

                        cls.Name = m.model.Name;
                        //cls.ClassName = m.model.ClassName;
                        //cls.SectionName = m.model.SectionName;
                        //cls.TeacherName = m.model.TeacherName;

                        //cls.StartTime = SsdtTime.ToString("hh:mm:ss tt ");
                        //cls.EndTime = EddtTime.ToString("hh:mm:ss tt ");
                        DateTime Sttdtt1 = SsdtTime.AddMinutes(-1);
                        DateTime Enddtt2 = EddtTime.AddMinutes(1);
                        //cls.STime = Sttdtt1.TimeOfDay;
                        //cls.ETime = Enddtt2.TimeOfDay;
                        cls.StartTime = Sttdtt1.ToString("hh:mm:ss tt ");
                        cls.EndTime = Enddtt2.ToString("hh:mm:ss tt ");
                        cls.Status = m.model.Status;
                        cls.IsBreak = m.model.IsBreak;
                        ct.period = cls;

                        if (ct.period.Status == 0)
                        {
                            ct.StatusNm = "Inactive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "Inactivate";
                        }
                        if (cls.IsBreak == true)
                        {
                            ct.IsBreak = "Yes";
                            ct.style = "badge bg-green";
                        }
                        else
                        {
                            ct.IsBreak = "No";
                            ct.style = "badge bg-red";
                        }
                        ct.School = m.SchoolName;
                        ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                        ct.classname = Convert.ToString(m.ClassName);
                        ct.SectionName = Convert.ToString(m.SectionName);
                        ct.TeacherName = Convert.ToString(m.TeacherName);
                        list.Add(ct);
                    }
                }
                else
                {
                    // int count = 0;

                    var Week = db.tblWeekDays.ToList();
                    foreach (var quo in Week)
                    {
                        TimeTable ct = new Models.TimeTable();
                        ct.WeekDaysNm = quo.WeekDay;
                        ct.ID = quo.ID;
                        ct.CTD = new List<tblClassTimingDet>();
                        var quo1 = (from c in db.tblClassTimingDets
                                    join s in db.tblSubjects on c.SubjectId equals s.ID
                                    where c.CT_ID == id && c.IsDeleted == null && c.SchoolID == SchoolId && c.WeekDays == ct.ID

                                    select new
                                    {
                                        Name = s.Subject,
                                        STime = c.STime,
                                        ETime = c.ETime,

                                    }).ToList();

                        foreach (var TT in quo1)
                        {
                            tblClassTimingDet usr = new tblClassTimingDet();

                            usr.Name = TT.Name;
                            DateTime SsdtTime = DateTime.Now.Date + TT.STime;
                            DateTime EddtTime = DateTime.Now.Date + TT.ETime;
                            DateTime Sttdtt1 = SsdtTime.AddMinutes(-1);
                            DateTime Enddtt2 = EddtTime.AddMinutes(1);
                            usr.StartTime = Sttdtt1.ToString("hh:mm tt ");
                            usr.EndTime = Enddtt2.ToString("hh:mm tt ");
                            usr.STime = TT.STime;
                            usr.ETime = TT.ETime;
                            ct.CTD.Add(usr);
                        }

                        list.Add(ct);
                    }

                    //var result = (from c in db.tblClassTimingDets
                    //              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                    //              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                    //              join ct in db.tblClassTimings on c.CT_ID equals ct.ID
                    //              join cl in db.tblCourses on ct.ClassId equals cl.Id
                    //              join se in db.tblSections on ct.SectionId equals se.Id
                    //              join em1 in db.tblEmployees on c.TeacherId equals em1.Id

                    //              where em.UserID == loginuser && em.IsDeleted == null && c.CT_ID == id && c.IsDeleted == null

                    //              select new
                    //              {
                    //                  model = c,
                    //                  SchoolName = s.School,
                    //                  ClassName = cl.CourseName,
                    //                  SectionName = se.SectionName,
                    //                  TeacherName = em1.FirstName,
                    //                  TeacherId = em1.Id
                    //              }).ToList();

                    //foreach (var m in result)
                    //{
                    //    count++;
                    //    TimeTable ct = new Models.TimeTable();
                    //    tblClassTimingDet cls = new tblClassTimingDet();
                    //    DateTime SsdtTime = DateTime.Now.Date + m.model.STime;
                    //    DateTime EddtTime = DateTime.Now.Date + m.model.ETime;
                    //    // hh for 12 hour clock



                    //    ct.CountID = count;
                    //    cls.ID = m.model.ID;
                    //    cls.Name = m.model.Name;
                    //    //cls.StartTime = SsdtTime.ToString("hh:mm:ss tt ");
                    //    //cls.EndTime = EddtTime.ToString("hh:mm:ss tt ");
                    //    DateTime Sttdtt1 = SsdtTime.AddMinutes(-1);
                    //    DateTime Enddtt2 = EddtTime.AddMinutes(1);
                    //    //cls.STime = Sttdtt1.TimeOfDay;
                    //    //cls.ETime = Enddtt2.TimeOfDay;
                    //    cls.StartTime = Sttdtt1.ToString("hh:mm:ss tt ");
                    //    cls.EndTime = Enddtt2.ToString("hh:mm:ss tt ");
                    //    cls.Status = m.model.Status;
                    //    cls.IsBreak = m.model.IsBreak;
                    //    ct.period = cls;
                    //    if (ct.period.Status == 0)
                    //    {
                    //        ct.StatusNm = "Inactive";
                    //        ct.style = "btn btn-block btn-danger btn-sm";
                    //        ct.action = "Activate";
                    //    }
                    //    else
                    //    {
                    //        ct.StatusNm = "Active";
                    //        ct.style = "btn btn-block btn-success btn-sm";
                    //        ct.action = "Inactivate";
                    //    }
                    //    if (cls.IsBreak == true)
                    //    {
                    //        ct.IsBreak = "Yes";
                    //        ct.style = "badge bg-green";
                    //    }
                    //    else
                    //    {
                    //        ct.IsBreak = "No";
                    //        ct.style = "badge bg-red";
                    //    }
                    //    ct.School = m.SchoolName;
                    //    ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                    //    ct.classname = Convert.ToString(m.ClassName);
                    //    ct.SectionName = Convert.ToString(m.SectionName);
                    //    ct.TeacherName = Convert.ToString(m.TeacherName);
                    //    ct.TeacherId = Convert.ToInt32(m.model.TeacherId);

                    //    list.Add(ct);
                    //}
                }


            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetWeekdays")]
        [System.Web.Http.HttpPost]
        public tblWeekDay[] GetWeekdays()
        {
            List<tblWeekDay> list = new List<tblWeekDay>();

            try
            {
                var result = db.tblWeekDays.ToList();

                foreach (var a in result)
                {
                    tblWeekDay items = new tblWeekDay();
                    items.WeekDay = a.WeekDay;
                    items.ID = a.ID;
                    items.DayID = a.DayID;

                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetAcademicYearBySchoolActive")]
        [System.Web.Http.HttpPost]
        public AcademicYrModel[] GetAcademicYearBySchoolActive(List<string> aa)
        {
            List<AcademicYrModel> list = new List<AcademicYrModel>();

            try
            {
                int SchoolID = Convert.ToInt32(aa[0]);
                var result = db.tblAcademicYears.Where(s => s.Status == true && s.SchoolID == SchoolID && s.IsDeleted == null && s.CurrActive == true).OrderByDescending(s => s.DateFrom).ToList();

                foreach (var a in result)
                {
                    AcademicYrModel items = new AcademicYrModel();
                    items.StartYear = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetAcademicYearBySchool")]
        [System.Web.Http.HttpPost]
        public AcademicYrModel[] GetAcademicYearBySchool(List<string> aa)
        {
            List<AcademicYrModel> list = new List<AcademicYrModel>();

            try
            {
                int SchoolID = Convert.ToInt32(aa[0]);
                var result = db.tblAcademicYears.Where(s => s.Status == true && s.SchoolID == SchoolID && s.IsDeleted == null /*&& s.CurrActive==true*/).OrderByDescending(s => s.DateFrom).ToList();

                foreach (var a in result)
                {
                    AcademicYrModel items = new AcademicYrModel();
                    items.StartYear = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/TimeTableApi/GetAcademicYear")]
        [System.Web.Http.HttpPost]
        public AcademicYrModel[] GetAcademicYear()
        {
            List<AcademicYrModel> list = new List<AcademicYrModel>();

            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                string Schoolid = (string)HttpContext.Current.Session["schoolid"];
                int? schid = Convert.ToInt32(Schoolid);

                var result = db.tblAcademicYears.Where(s => s.Status == true && s.SchoolID == schid && s.IsDeleted == null).OrderByDescending(s => s.DateFrom).ToList();

                foreach (var a in result)
                {
                    AcademicYrModel items = new AcademicYrModel();
                    items.StartYear = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/getTimeTableConfig")]
        [System.Web.Http.HttpPost]
        public TimeTable[] getTimeTableConfig(List<string> val)
        {
            List<TimeTable> list = new List<Models.TimeTable>();
            try
            {
                string loginuser = val[1];
                int typeuser = Convert.ToInt32(val[2]);
                if (typeuser == 1 || typeuser == 3)
                {
                    int count = 0;
                    var result = (from c in db.tblTimeTableConfigs
                                  join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                                  join cl in db.tblCourses on c.ClassID equals cl.Id
                                  join s in db.tblSections on c.SectionID equals s.Id
                                  join t in db.tblClassTimings on c.CT_ID equals t.ID
                                  join sc in db.tblSchoolDetails on c.SchoolID equals sc.ID
                                  join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                                  where em.UserID == loginuser && em.IsDeleted == null && c.IsDeleted == null
                                  select new
                                  {
                                      model = c,
                                      DtFrom = ac.DateFrom,
                                      DtTo = ac.DateTo,
                                      courseNm = cl.CourseName,
                                      sectionNm = s.SectionName,
                                      timingNm = t.Name,
                                      SchoolName = sc.School
                                  }).ToList();

                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblTimeTableConfig cls = new tblTimeTableConfig();
                        ct.CountID = count;
                        ct.ID = m.model.ID;
                        cls = m.model;
                        ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                        ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
                        ct.Course = m.courseNm + "-" + m.sectionNm;
                        ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.timingNm = m.timingNm;
                        ct.con = cls;

                        var ar = m.model.WeekDays.Split(',');
                        ct.WeekDaysNm = ""; int n = 0;
                        foreach (var day in ar)
                        {
                            int dayID = Convert.ToInt32(day);
                            var w = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();

                            if (n < 1)
                            {
                                ct.WeekDaysNm = w.WeekDay;
                            }
                            else
                            {
                                ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
                            }
                            n++;
                        }

                        if (ct.con.Status == 0)
                        {
                            ct.StatusNm = "Deactive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "Deactive";
                        }
                        ct.School = m.SchoolName;
                        ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                        list.Add(ct);
                    }
                }
                else if (typeuser == 2)
                {
                    int count = 0;

                    var result = (from c in db.tblTimeTableConfigs
                                  join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                                  join cl in db.tblCourses on c.ClassID equals cl.Id
                                  join s in db.tblSections on c.SectionID equals s.Id
                                  join t in db.tblClassTimings on c.CT_ID equals t.ID
                                  join sc in db.tblSchoolDetails on c.SchoolID equals sc.ID
                                  where c.IsDeleted == null
                                  select new
                                  {
                                      model = c,
                                      DtFrom = ac.DateFrom,
                                      DtTo = ac.DateTo,
                                      courseNm = cl.CourseName,
                                      sectionNm = s.SectionName,
                                      timingNm = t.Name,
                                      SchoolName = sc.School
                                  }).ToList();
                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblTimeTableConfig cls = new tblTimeTableConfig();
                        ct.CountID = count;
                        ct.ID = m.model.ID;
                        cls = m.model;
                        ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                        ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
                        ct.Course = m.courseNm + "-" + m.sectionNm;
                        ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.timingNm = m.timingNm;
                        ct.con = cls;

                        var ar = m.model.WeekDays.Split(',');
                        ct.WeekDaysNm = ""; int n = 0;
                        foreach (var day in ar)
                        {
                            int dayID = Convert.ToInt32(day);
                            var w = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();

                            if (n < 1)
                            {
                                ct.WeekDaysNm = w.WeekDay;
                            }
                            else
                            {
                                ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
                            }
                            n++;
                        }

                        if (ct.con.Status == 0)
                        {
                            ct.StatusNm = "DeActive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "DeActive";
                        }
                        ct.School = m.SchoolName;
                        ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                        list.Add(ct);
                    }

                }

            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();
        }

        //[System.Web.Http.Route("api/TimeTableApi/SearchTisyllabus")]
        //[System.Web.Http.HttpPost]
        //public syllabus[] SearchTisyllabus(List<string> val)
        //{
        //    int year = Convert.ToInt32(val[0]);
        //    int classID = Convert.ToInt32(val[1]);
        //    int SchoolID = Convert.ToInt32(val[8]);
        //    string loginuser = val[9];
        //    int typeuser = Convert.ToInt32(val[10]);




        //    List<syllabus> list = new List<Models.syllabus>();
        //    try
        //    {
        //        if (typeuser == 1)
        //        {
        //            var result = (from c in db.tblSyllabus
        //                          join s in db.tblSchoolDetails on c.SchoolID equals s.ID
        //                          join em in db.tblEmployees on c.SchoolID equals em.SchoolID
        //                          where em.UserID == loginuser
        //                          select new
        //                          {
        //                              model = c,
        //                              SchoolName = s.School
        //                          }).ToList();
        //            if (year != -1)
        //            {
        //                //predicate = predicate.And(x => x.AcademicYear == year);
        //                result = result.Where(c => c.model.AcademicYear == year).ToList();
        //            }
        //            if (classID != -1)
        //            {
        //                //predicate = predicate.And(x => x.ClassID == classID);
        //                result = result.Where(c => c.model.Class == classID).ToList();
        //            }

        //            if (SchoolID != 0)
        //            {
        //                //predicate = predicate.And(x => x.Status == status);
        //                result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
        //            }
        //            int count = 0;



        //            foreach (var m in result)
        //            {
        //                count++;
        //                syllabus ct = new Models.syllabus();
        //                tblSyllabu cls = new tblSyllabu();
        //                ct.ID = count;

        //                ct.hw = m;


        //                var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.model.AcademicYear);
        //                ct.academicyearname = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");





        //                var rslt = (
        //                        from s in db.tblCourses
        //                        where s.Id == m.Class
        //                        select new
        //                        {
        //                            crs = s

        //                        }).SingleOrDefault();
        //                ct.Classname = rslt.crs.CourseName;





        //                list.Add(ct);
        //            }


        //        }
        //        if (typeuser == 2)
        //        {
        //            var result = (from c in db.tblTimeTableConfigs
        //                          join s in db.tblSchoolDetails on c.SchoolID equals s.ID
        //                          select new
        //                          {
        //                              model = c,
        //                              SchoolName = s.School
        //                          }).ToList();
        //            if (year != -1)
        //            {
        //                //predicate = predicate.And(x => x.AcademicYear == year);
        //                result = result.Where(c => c.model.AcademicYear == year).ToList();
        //            }
        //            if (classID != -1)
        //            {
        //                //predicate = predicate.And(x => x.ClassID == classID);
        //                result = result.Where(c => c.model.ClassID == classID).ToList();
        //            }
        //            if (val[2] != "-1" && val[2] != "")
        //            {
        //                int SecID = Convert.ToInt32(val[2]);
        //                //predicate = predicate.And(x => x.SectionID == SecID);
        //                result = result.Where(c => c.model.SectionID == SecID).ToList();
        //            }
        //            if (!string.IsNullOrWhiteSpace(ttname))
        //            {
        //                //predicate = predicate.And(x => x.Name.ToUpper().Contains(ttname.ToUpper()));
        //                result = result.Where(c => c.model.Name.ToUpper().Contains(ttname.ToUpper())).ToList();
        //            }
        //            if (timID != -1)
        //            {
        //                //predicate = predicate.And(x => x.CT_ID == timID);
        //                result = result.Where(c => c.model.CT_ID == timID).ToList();
        //            }
        //            if (!string.IsNullOrWhiteSpace(val[5]))
        //            {
        //                DateTime fromDt = Convert.ToDateTime(val[5]);
        //                //predicate = predicate.And(x => x.StartDT == fromDt);
        //                result = result.Where(c => c.model.StartDT == fromDt).ToList();
        //            }
        //            if (!string.IsNullOrWhiteSpace(val[6]))
        //            {
        //                DateTime toDt = Convert.ToDateTime(val[6]);
        //                //predicate = predicate.And(x => x.EndDt == toDt);
        //                result = result.Where(c => c.model.EndDt == toDt).ToList();
        //            }
        //            if (status != -1)
        //            {
        //                //predicate = predicate.And(x => x.Status == status);
        //                result = result.Where(c => c.model.Status == status).ToList();
        //            }
        //            if (SchoolID != 0)
        //            {
        //                //predicate = predicate.And(x => x.Status == status);
        //                result = result.Where(c => c.model.Status == SchoolID).ToList();
        //            }
        //            int count = 0;



        //            foreach (var m in result)
        //            {
        //                count++;
        //                TimeTable ct = new Models.TimeTable();
        //                tblTimeTableConfig cls = new tblTimeTableConfig();
        //                ct.CountID = count;
        //                ct.ID = m.model.ID;
        //                cls = m.model;
        //                ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate

        //                var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.model.AcademicYear);
        //                ct.AcYear = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

        //                long ctID = Convert.ToInt32(m.model.CT_ID);
        //                var t = db.tblClassTimings.Where(x => x.ID == ctID).SingleOrDefault();
        //                ct.timingNm = t.Name;

        //                var rslt = (from c in db.tblSections
        //                            join s in db.tblCourses on c.ClassId equals s.Id
        //                            where s.Id == m.model.ClassID && c.Id == m.model.SectionID
        //                            select new
        //                            {
        //                                crs = s,
        //                                sec = c
        //                            }).SingleOrDefault();
        //                ct.Course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
        //                ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                ct.con = cls;

        //                var ar = m.model.WeekDays.Split(',');
        //                ct.WeekDaysNm = ""; int n = 0;
        //                foreach (var day in ar)
        //                {
        //                    int dayID = Convert.ToInt32(day);
        //                    var w = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();

        //                    if (n < 1)
        //                    {
        //                        ct.WeekDaysNm = w.WeekDay;
        //                    }
        //                    else
        //                    {
        //                        ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
        //                    }
        //                    n++;
        //                }

        //                if (ct.con.Status == 0)
        //                {
        //                    ct.StatusNm = "Inactive";
        //                    ct.style = "btn btn-block btn-danger btn-sm";
        //                    ct.action = "Activate";
        //                }
        //                else
        //                {
        //                    ct.StatusNm = "Active";
        //                    ct.style = "btn btn-block btn-success btn-sm";
        //                    ct.action = "Inactivate";
        //                }
        //                ct.School = m.SchoolName;
        //                ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
        //                list.Add(ct);
        //            }
        //        }

        //    }
        //    catch (Exception ex) { throw ex; }
        //    return list.ToArray();
        //}





        [System.Web.Http.Route("api/TimeTableApi/SearchTimeTableConfig")]
        [System.Web.Http.HttpPost]
        public TimeTable[] SearchTimeTableConfig(List<string> val)
        {
            int year = Convert.ToInt32(val[0]);
            int classID = Convert.ToInt32(val[1]);
            int SchoolID = Convert.ToInt32(val[8]);
            string loginuser = val[9];
            int typeuser = Convert.ToInt32(val[10]);
            string ttname = val[3];
            int timID = Convert.ToInt32(val[4]);

            int status = Convert.ToInt32(val[7]);
            List<TimeTable> list = new List<Models.TimeTable>();
            try
            {
                if (typeuser == 1 || typeuser == 3)
                {
                    var result = (from c in db.tblTimeTableConfigs
                                  join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                                  join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                                  where em.UserID == loginuser && em.IsDeleted == null && c.IsDeleted == null
                                  select new
                                  {
                                      model = c,
                                      SchoolName = s.School
                                  }).ToList();
                    if (year != -1)
                    {
                        //predicate = predicate.And(x => x.AcademicYear == year);
                        result = result.Where(c => c.model.AcademicYear == year).ToList();
                    }
                    if (classID != -1)
                    {
                        //predicate = predicate.And(x => x.ClassID == classID);
                        result = result.Where(c => c.model.ClassID == classID).ToList();
                    }
                    if (val[2] != "-1" && val[2] != "")
                    {
                        int SecID = Convert.ToInt32(val[2]);
                        //predicate = predicate.And(x => x.SectionID == SecID);
                        result = result.Where(c => c.model.SectionID == SecID).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(ttname))
                    {
                        //predicate = predicate.And(x => x.Name.ToUpper().Contains(ttname.ToUpper()));
                        result = result.Where(c => c.model.Name.ToUpper().Contains(ttname.ToUpper())).ToList();
                    }
                    if (timID != -1)
                    {
                        //predicate = predicate.And(x => x.CT_ID == timID);
                        result = result.Where(c => c.model.CT_ID == timID).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(val[5]))
                    {
                        DateTime fromDt = Convert.ToDateTime(val[5]);
                        //predicate = predicate.And(x => x.StartDT == fromDt);
                        result = result.Where(c => c.model.StartDT == fromDt).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(val[6]))
                    {
                        DateTime toDt = Convert.ToDateTime(val[6]);
                        //predicate = predicate.And(x => x.EndDt == toDt);
                        result = result.Where(c => c.model.EndDt == toDt).ToList();
                    }
                    if (status != -1)
                    {
                        //predicate = predicate.And(x => x.Status == status);
                        result = result.Where(c => c.model.Status == status).ToList();
                    }
                    if (SchoolID != 0)
                    {
                        //predicate = predicate.And(x => x.Status == status);
                        result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
                    }
                    int count = 0;



                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblTimeTableConfig cls = new tblTimeTableConfig();
                        ct.CountID = count;
                        ct.ID = m.model.ID;
                        cls = m.model;
                        ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate

                        var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.model.AcademicYear);
                        ct.AcYear = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                        long ctID = Convert.ToInt32(m.model.CT_ID);
                        var t = db.tblClassTimings.Where(x => x.ID == ctID && x.IsDeleted == null).SingleOrDefault();
                        ct.timingNm = t.Name;

                        var rslt = (from c in db.tblSections
                                    join s in db.tblCourses on c.ClassId equals s.Id
                                    where s.Id == m.model.ClassID && c.Id == m.model.SectionID
                                    select new
                                    {
                                        crs = s,
                                        sec = c
                                    }).SingleOrDefault();
                        ct.Course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
                        ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.con = cls;

                        var ar = m.model.WeekDays.Split(',');
                        ct.WeekDaysNm = ""; int n = 0;
                        foreach (var day in ar)
                        {
                            int dayID = Convert.ToInt32(day);
                            var w = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();

                            if (n < 1)
                            {
                                ct.WeekDaysNm = w.WeekDay;
                            }
                            else
                            {
                                ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
                            }
                            n++;
                        }

                        if (ct.con.Status == 0)
                        {
                            ct.StatusNm = "DeActive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "DeActive";
                        }
                        ct.School = m.SchoolName;
                        ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                        list.Add(ct);
                    }


                }
                if (typeuser == 2)
                {
                    var result = (from c in db.tblTimeTableConfigs
                                  join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                                  select new
                                  {
                                      model = c,
                                      SchoolName = s.School
                                  }).ToList();
                    if (year != -1)
                    {
                        //predicate = predicate.And(x => x.AcademicYear == year);
                        result = result.Where(c => c.model.AcademicYear == year).ToList();
                    }
                    if (classID != -1)
                    {
                        //predicate = predicate.And(x => x.ClassID == classID);
                        result = result.Where(c => c.model.ClassID == classID).ToList();
                    }
                    if (val[2] != "-1" && val[2] != "")
                    {
                        int SecID = Convert.ToInt32(val[2]);
                        //predicate = predicate.And(x => x.SectionID == SecID);
                        result = result.Where(c => c.model.SectionID == SecID).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(ttname))
                    {
                        //predicate = predicate.And(x => x.Name.ToUpper().Contains(ttname.ToUpper()));
                        result = result.Where(c => c.model.Name.ToUpper().Contains(ttname.ToUpper())).ToList();
                    }
                    if (timID != -1)
                    {
                        //predicate = predicate.And(x => x.CT_ID == timID);
                        result = result.Where(c => c.model.CT_ID == timID).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(val[5]))
                    {
                        DateTime fromDt = Convert.ToDateTime(val[5]);
                        //predicate = predicate.And(x => x.StartDT == fromDt);
                        result = result.Where(c => c.model.StartDT == fromDt).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(val[6]))
                    {
                        DateTime toDt = Convert.ToDateTime(val[6]);
                        //predicate = predicate.And(x => x.EndDt == toDt);
                        result = result.Where(c => c.model.EndDt == toDt).ToList();
                    }
                    if (status != -1)
                    {
                        //predicate = predicate.And(x => x.Status == status);
                        result = result.Where(c => c.model.Status == status).ToList();
                    }
                    if (SchoolID != 0)
                    {
                        //predicate = predicate.And(x => x.Status == status);
                        result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
                    }
                    int count = 0;



                    foreach (var m in result)
                    {
                        count++;
                        TimeTable ct = new Models.TimeTable();
                        tblTimeTableConfig cls = new tblTimeTableConfig();
                        ct.CountID = count;
                        ct.ID = m.model.ID;
                        cls = m.model;
                        ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate

                        var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.model.AcademicYear);
                        ct.AcYear = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                        long ctID = Convert.ToInt32(m.model.CT_ID);
                        var t = db.tblClassTimings.Where(x => x.ID == ctID).SingleOrDefault();
                        ct.timingNm = t.Name;

                        var rslt = (from c in db.tblSections
                                    join s in db.tblCourses on c.ClassId equals s.Id
                                    where s.Id == m.model.ClassID && c.Id == m.model.SectionID
                                    select new
                                    {
                                        crs = s,
                                        sec = c
                                    }).SingleOrDefault();
                        ct.Course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
                        ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.con = cls;

                        var ar = m.model.WeekDays.Split(',');
                        ct.WeekDaysNm = ""; int n = 0;
                        foreach (var day in ar)
                        {
                            int dayID = Convert.ToInt32(day);
                            var w = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();

                            if (n < 1)
                            {
                                ct.WeekDaysNm = w.WeekDay;
                            }
                            else
                            {
                                ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
                            }
                            n++;
                        }

                        if (ct.con.Status == 0)
                        {
                            ct.StatusNm = "DeActive";
                            ct.style = "btn btn-block btn-danger btn-sm";
                            ct.action = "Activate";
                        }
                        else
                        {
                            ct.StatusNm = "Active";
                            ct.style = "btn btn-block btn-success btn-sm";
                            ct.action = "DeActive";
                        }
                        ct.School = m.SchoolName;
                        ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                        list.Add(ct);
                    }
                }

            }
            catch (Exception ex) { throw ex; }


            //    var predicate=PredicateBuilder.True<tblTimeTableConfig>();

            //    if(year!=-1)
            //    {
            //        predicate = predicate.And(x => x.AcademicYear == year);
            //    }
            //    if (classID != -1)
            //    {
            //        predicate = predicate.And(x => x.ClassID == classID);
            //    }
            //    if (val[2] != "-1" && val[2]!="")
            //    {
            //        int SecID = Convert.ToInt32(val[2]);
            //        predicate = predicate.And(x => x.SectionID == SecID);
            //    }
            //    if (!string.IsNullOrWhiteSpace(ttname))
            //    {
            //        predicate = predicate.And(x => x.Name.ToUpper().Contains(ttname.ToUpper()));
            //    }
            //    if (timID != -1)
            //    {
            //        predicate = predicate.And(x => x.CT_ID == timID);
            //    }
            //    if (!string.IsNullOrWhiteSpace(val[5]))
            //    {
            //        DateTime fromDt = Convert.ToDateTime(val[5]);
            //        predicate = predicate.And(x => x.StartDT == fromDt);
            //    }
            //    if (!string.IsNullOrWhiteSpace(val[6]))
            //    {
            //        DateTime toDt = Convert.ToDateTime(val[6]);
            //        predicate = predicate.And(x => x.EndDt == toDt);
            //    }
            //    if (status != -1)
            //    {
            //        predicate = predicate.And(x => x.Status == status);
            //    }
            //    int count = 0;

            //    var result = db.tblTimeTableConfigs.AsExpandable().Where(predicate).ToList();

            //    foreach (var m in result)
            //    {
            //        count++;
            //        TimeTable ct = new Models.TimeTable();
            //        tblTimeTableConfig cls = new tblTimeTableConfig();
            //        ct.CountID = count;
            //        ct.ID = m.ID;
            //        cls = m;
            //        ct.timingNm = m.Name;//for timing dropdown in TimeTableConfigCreate

            //        var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear);
            //        ct.AcYear = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

            //        long ctID = Convert.ToInt32(m.CT_ID);
            //        var t =db.tblClassTimings.Where(x=>x.ID == ctID).SingleOrDefault();
            //        ct.timingNm = t.Name;

            //        var rslt = (from c  in db.tblSections
            //                    join s in db.tblCourses on c.ClassId equals s.Id
            //                    where s.Id==m.ClassID && c.Id==m.SectionID
            //                    select new
            //                    {
            //                        crs = s,
            //                        sec=c
            //                    }).SingleOrDefault();
            //        ct.Course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
            //        ct.fromDT = ((DateTime)m.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        ct.ToDt = ((DateTime)m.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        ct.con = cls;

            //        var ar = m.WeekDays.Split(',');
            //        ct.WeekDaysNm = ""; int n = 0;
            //        foreach (var day in ar)
            //        {
            //            int dayID = Convert.ToInt32(day);
            //            var w = db.tblWeekDays.Where(x=>x.DayID == dayID).SingleOrDefault();

            //            if (n < 1)
            //            {
            //                ct.WeekDaysNm = w.WeekDay;
            //            }
            //            else
            //            {
            //                ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
            //            }
            //            n++;
            //        }

            //        if (ct.con.Status == 0)
            //        {
            //            ct.StatusNm = "Inactive";
            //            ct.style = "btn btn-block btn-danger btn-sm";
            //            ct.action = "Activate";
            //        }
            //        else
            //        {
            //            ct.StatusNm = "Active";
            //            ct.style = "btn btn-block btn-success btn-sm";
            //            ct.action = "Inactivate";
            //        }
            //        list.Add(ct);
            //    }
            //}
            //catch (Exception ex) { throw ex; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/getTimeTableConfigDetapp")]
        [System.Web.Http.HttpGet]
        public TimeTableapp getTimeTableConfigDetapp(int SchoolID, int ClassID, int SectionID)
        {
            //List<TimeTable> list = new List<Models.TimeTable>();
            TimeTableapp obj = new TimeTableapp();

            TimeTable1 ct = new Models.TimeTable1();
            ct.MonthlyTimeTable = new TimeTable3(); //this is for key inside key

            try
            {
                if (ClassID.Equals(0) || ClassID.Equals(null) || "".Equals(ClassID))

                {
                    obj.status = false;
                    obj.message = "Please Enter ClassID";
                }
                else if ("".Equals(SectionID) || SectionID.Equals(null) || "".Equals(SectionID))
                {
                    obj.status = false;
                    obj.message = "Please Enter Section";
                }
                else if (SchoolID.Equals(0) || SchoolID.Equals(null) || "".Equals(SchoolID))
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";

                }
                else
                {
                    int timetableid = 0;

                    var timetablecon = db.tblTimeTableConfigs.Where(x => x.ClassID == ClassID && x.SectionID == SectionID && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
                    if (timetablecon != null)
                    {

                        timetableid = Convert.ToInt32(timetablecon.ID);


                        int count = 0;
                        int id = Convert.ToInt32(timetableid);
                        var m = (from c in db.tblTimeTableConfigs
                                 join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                                 join cl in db.tblCourses on c.ClassID equals cl.Id
                                 join s in db.tblSections on c.SectionID equals s.Id
                                 where c.ID == id && c.IsDeleted == null
                                 select new
                                 {
                                     model = c,
                                     DtFrom = ac.DateFrom,
                                     DtTo = ac.DateTo,
                                     courseNm = cl.CourseName,
                                     sectionNm = s.SectionName
                                 }).SingleOrDefault();

                        count++;


                        int ID = Convert.ToInt32(m.model.ID);

                        ct.ClassID = Convert.ToString(m.model.ClassID);
                        ct.SectionID = Convert.ToString(m.model.SectionID);
                        ct.SchoolID = Convert.ToString(m.model.SchoolID);


                        if (m.model.WeekDays != "")
                        {
                            string days = "";
                            var w = m.model.WeekDays.Split(',');
                            int i = 0;
                            foreach (string s in w)
                            {
                                i++;
                                int dayID = Convert.ToInt32(s);
                                var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
                                if (i == 1)
                                {
                                    days = week.WeekDay;
                                }
                                else
                                {
                                    days = days + "," + week.WeekDay;
                                }
                            }
                            //cls.WeekDaysNm = days;

                            var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == m.model.CT_ID && x.IsDeleted == null).ToList();



                            ct.ttList = new List<TimeTable2>();
                            foreach (var ctd in classTimingdet)
                            {

                                var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
                                DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
                                DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);



                                foreach (var t in timetable)
                                {

                                    TimeTable2 a = new TimeTable2();


                                    if (t.WeekDay != null && t.WeekDay != "-1" && t.WeekDay != "0")
                                    {
                                        int dayid = Convert.ToInt32(t.WeekDay);
                                        var week = db.tblWeekDays.Where(x => x.DayID == dayid).SingleOrDefault();
                                        a.WeekDay = week.WeekDay;
                                    }

                                    a.timingNm = SsdtTime.ToString("hh:mm tt ") + "-" + EddtTime.ToString("hh:mm tt ");
                                    a.IsBreak = ctd.IsBreak.ToString();
                                    if (t.SubjectID != null && t.TeacherID != -1)
                                    {
                                        var sub = db.tblSubjects.Where(s => s.ID == t.SubjectID).SingleOrDefault();
                                        a.subjectNm = sub.Subject;
                                    }
                                    else { a.subjectNm = ""; }
                                    if (t.TeacherID != null && t.TeacherID != -1)
                                    {
                                        var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherID).SingleOrDefault();
                                        a.teacherNm = teacher.FirstName + " " + teacher.LastName;
                                    }
                                    else { a.teacherNm = ""; }

                                    ct.ttList.Add(a);

                                }



                            }

                            //   List<TimeTable1> timet = new List<TimeTable1>();
                            //  ct.ttList1 = new List<TimeTable2>();
                            ct.MonthlyTimeTable.Monday = new List<TimeTable2>();
                            ct.MonthlyTimeTable.TuesDay = new List<TimeTable2>();
                            ct.MonthlyTimeTable.WebnessDay = new List<TimeTable2>();
                            ct.MonthlyTimeTable.thrusday = new List<TimeTable2>();
                            ct.MonthlyTimeTable.Friday = new List<TimeTable2>();
                            ct.MonthlyTimeTable.Saterday = new List<TimeTable2>();
                            ct.MonthlyTimeTable.Sunday = new List<TimeTable2>();
                            string[] arr = new string[] { };
                            arr = arr.Concat(new string[] { "Monday" }).ToArray();
                            arr = arr.Concat(new string[] { "TuesDay" }).ToArray();
                            arr = arr.Concat(new string[] { "Wednesday" }).ToArray();
                            arr = arr.Concat(new string[] { "Thursday" }).ToArray();
                            arr = arr.Concat(new string[] { "Friday" }).ToArray();
                            arr = arr.Concat(new string[] { "Saturday" }).ToArray();
                            arr = arr.Concat(new string[] { "Sunday" }).ToArray();

                            for (int k = 0; k < arr.Length; k++)
                            {
                                var txt = arr[k];
                                //TimeTable3 munday = new TimeTable3();

                                //TimeTable4 TuesDay = new TimeTable4();
                                //TimeTable5 WebnessDay = new TimeTable5();
                                //TimeTable6 thrusday = new TimeTable6();
                                //TimeTable7 Friday = new TimeTable7();
                                //TimeTable8 Saterday = new TimeTable8();
                                //TimeTable9 Sunday = new TimeTable9();
                                foreach (var drr in ct.ttList)
                                {
                                    if (drr.WeekDay == txt)
                                    {
                                        if (txt == "Monday")
                                        {
                                            ct.MonthlyTimeTable.Monday.Add(drr);
                                        }
                                        if (txt == "TuesDay")
                                        {
                                            ct.MonthlyTimeTable.TuesDay.Add(drr);
                                        }
                                        if (txt == "Wednesday")
                                        {
                                            ct.MonthlyTimeTable.WebnessDay.Add(drr);
                                        }
                                        if (txt == "Thursday")
                                        {
                                            ct.MonthlyTimeTable.thrusday.Add(drr);
                                        }
                                        if (txt == "Friday")
                                        {
                                            ct.MonthlyTimeTable.Friday.Add(drr);
                                        }
                                        if (txt == "Saturday")
                                        {
                                            ct.MonthlyTimeTable.Saterday.Add(drr);
                                        }
                                        if (txt == "Sunday")
                                        {
                                            ct.MonthlyTimeTable.Sunday.Add(drr);
                                        }

                                    }
                                }


                            }
                            ct.ttList.Clear();
                            obj.data = ct;
                            obj.status = true;
                            obj.message = "Sucess";
                        }
                    }
                    else
                    {
                        obj.status = false;
                        obj.message = "No Timetable Assigend";
                    }
                }

            }
            catch (Exception ex)
            {
                obj.status = false;
                obj.message = "Something Went Wrong";
            }

            return obj;
        }



        //[System.Web.Http.Route("api/TimeTableApi/getTimeTableConfigDetapp")]
        //[System.Web.Http.HttpPost]
        //public TimeTableapp getTimeTableConfigDetapp(TimeTable1 val)
        //{



        //    //List<TimeTable> list = new List<Models.TimeTable>();
        //    TimeTableapp obj = new TimeTableapp();

        //    TimeTable1 ct = new Models.TimeTable1();

        //    try
        //    {
        //        if (val.ClassID.Equals(0) || val.ClassID.Equals(null) || "".Equals(val.ClassID))

        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter ClassID";
        //        }
        //        else if ("".Equals(val.SectionID) || val.SectionID.Equals(null) || "".Equals(val.SectionID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Section";
        //        }
        //        else if (val.SchoolID.Equals(0) || val.SchoolID.Equals(null) || "".Equals(val.SchoolID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else
        //        {
        //            int classID = Convert.ToInt32(val.ClassID);
        //            int SectionID = Convert.ToInt32(val.SectionID);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int timetableid = 0;

        //            var timetablecon = db.tblTimeTableConfigs.Where(x => x.ClassID == classID && x.SectionID == SectionID && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
        //            if (timetablecon != null)
        //            {

        //                timetableid = Convert.ToInt32(timetablecon.ID);


        //                int count = 0;
        //                int id = Convert.ToInt32(timetableid);
        //                var m = (from c in db.tblTimeTableConfigs
        //                         join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
        //                         join cl in db.tblCourses on c.ClassID equals cl.Id
        //                         join s in db.tblSections on c.SectionID equals s.Id
        //                         where c.ID == id && c.IsDeleted == null
        //                         select new
        //                         {
        //                             model = c,
        //                             DtFrom = ac.DateFrom,
        //                             DtTo = ac.DateTo,
        //                             courseNm = cl.CourseName,
        //                             sectionNm = s.SectionName
        //                         }).SingleOrDefault();

        //                count++;


        //                int ID = Convert.ToInt32(m.model.ID);

        //                ct.ClassID = Convert.ToInt32(m.model.ClassID);
        //                ct.SectionID = Convert.ToInt32(m.model.SectionID);
        //                ct.SchoolID = Convert.ToInt32(m.model.SchoolID);


        //                if (m.model.WeekDays != "")
        //                {
        //                    string days = "";
        //                    var w = m.model.WeekDays.Split(',');
        //                    int i = 0;
        //                    foreach (string s in w)
        //                    {
        //                        i++;
        //                        int dayID = Convert.ToInt32(s);
        //                        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
        //                        if (i == 1)
        //                        {
        //                            days = week.WeekDay;
        //                        }
        //                        else
        //                        {
        //                            days = days + "," + week.WeekDay;
        //                        }
        //                    }
        //                    //cls.WeekDaysNm = days;

        //                    var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == m.model.CT_ID && x.IsDeleted == null).ToList();



        //                    ct.ttList = new List<TimeTable2>();
        //                    foreach (var ctd in classTimingdet)
        //                    {




        //                        //                    

        //                        var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
        //                        DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
        //                        DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);



        //                        foreach (var t in timetable)
        //                        {

        //                            TimeTable2 a = new TimeTable2();


        //                            if (t.WeekDay != null && t.WeekDay != "-1" && t.WeekDay != "0")
        //                            {
        //                                int dayid = Convert.ToInt32(t.WeekDay);
        //                                var week = db.tblWeekDays.Where(x => x.DayID == dayid).SingleOrDefault();
        //                                a.WeekDay = week.WeekDay;
        //                            }

        //                            a.timingNm = SsdtTime.ToString("hh:mm:ss tt ") + "-" + EddtTime.ToString("hh:mm:ss tt ");
        //                            a.IsBreak = ctd.IsBreak;
        //                            if (t.SubjectID != null && t.TeacherID != -1)
        //                            {
        //                                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectID).SingleOrDefault();
        //                                a.subjectNm = sub.Subject;
        //                            }
        //                            else { a.subjectNm = ""; }
        //                            if (t.TeacherID != null && t.TeacherID != -1)
        //                            {
        //                                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherID).SingleOrDefault();
        //                                a.teacherNm = teacher.FirstName + " " + teacher.LastName;
        //                            }
        //                            else { a.teacherNm = ""; }

        //                            ct.ttList.Add(a);

        //                        }

        //                        obj.data = ct;
        //                        obj.status = true;
        //                        obj.message = "Sucess";

        //                    }
        //                    //List<TimeTable1> timet = new List<TimeTable1>();
        //                    //string[] arr = new string[] { };
        //                    //arr = arr.Concat(new string[] { "Monday" }).ToArray();
        //                    //arr = arr.Concat(new string[] { "TuesDay" }).ToArray();
        //                    //arr = arr.Concat(new string[] { "WebnessDay" }).ToArray();
        //                    //arr = arr.Concat(new string[] { "thrusday" }).ToArray();
        //                    //arr = arr.Concat(new string[] { "Friday" }).ToArray();
        //                    //arr = arr.Concat(new string[] { "Saterday" }).ToArray();
        //                    //arr = arr.Concat(new string[] { "Sunday" }).ToArray();

        //                    //for (int k = 0; k < arr.Length; k++)
        //                    //{

        //                    //    var txt = arr[k];

        //                    //    foreach (var drr in ct.ttList)
        //                    //    {
        //                    //        if (drr.WeekDay == txt)
        //                    //        {

        //                    //        }
        //                    //    }
        //                    //}
        //                }


        //            }
        //            else
        //            {
        //                obj.status = false;
        //                obj.message = "No Timetable Assigend";
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //    }

        //    return obj;
        //}



        //[System.Web.Http.Route("api/TimeTableApi/getTimeTableConfigDetapp")]
        //[System.Web.Http.HttpPost]
        //public TimeTableapp getTimeTableConfigDetapp(TimeTable1 val)
        //{



        //    //List<TimeTable> list = new List<Models.TimeTable>();
        //    TimeTableapp obj = new TimeTableapp();

        //  TimeTable1 ct = new Models.TimeTable1();

        //    try
        //    {
        //        if (val.Classid.Equals(0) || val.Classid.Equals(null) || "".Equals(val.Classid))

        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter ClassID";
        //        }
        //        else if ("".Equals(val.Section) || val.Section.Equals(null) || "".Equals(val.Section))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Section";
        //        }
        //        else if (val.SchoolID.Equals(0) || val.SchoolID.Equals(null) || "".Equals(val.SchoolID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else
        //        {
        //            int classID = Convert.ToInt32(val.Classid);
        //            int SectionID = Convert.ToInt32(val.Section);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int timetableid = 0;

        //            var timetablecon = db.tblTimeTableConfigs.Where(x => x.ClassID == classID && x.SectionID == SectionID && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
        //            if (timetablecon != null)
        //            {

        //                timetableid = Convert.ToInt32(timetablecon.ID);


        //                int count = 0;
        //                int id = Convert.ToInt32(timetableid);
        //                var m = (from c in db.tblTimeTableConfigs
        //                         join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
        //                         join cl in db.tblCourses on c.ClassID equals cl.Id
        //                         join s in db.tblSections on c.SectionID equals s.Id
        //                         where c.ID == id && c.IsDeleted == null
        //                         select new
        //                         {
        //                             model = c,
        //                             DtFrom = ac.DateFrom,
        //                             DtTo = ac.DateTo,
        //                             courseNm = cl.CourseName,
        //                             sectionNm = s.SectionName
        //                         }).SingleOrDefault();

        //                count++;


        //                int ID = Convert.ToInt32( m.model.ID);




        //                if (m.model.WeekDays != "")
        //                {
        //                    string days = "";
        //                    var w = m.model.WeekDays.Split(',');
        //                    int i = 0;
        //                    foreach (string s in w)
        //                    {
        //                        i++;
        //                        int dayID = Convert.ToInt32(s);
        //                        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
        //                        if (i == 1)
        //                        {
        //                            days = week.WeekDay;
        //                        }
        //                        else
        //                        {
        //                            days = days + "," + week.WeekDay;
        //                        }
        //                    }
        //                    //cls.WeekDaysNm = days;

        //                    var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == m.model.CT_ID && x.IsDeleted == null).ToList();


        //                    TimeTable1 att = new TimeTable1();
        //                    att.ttList = new List<TimeTable2>();
        //                    foreach (var ctd in classTimingdet)
        //                    {




        //                        //                    

        //                        var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
        //                        DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
        //                        DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);


        //                        ct.timetable = new List<TimeTable1>();
        //                        foreach (var t in timetable)
        //                        {

        //                            TimeTable2 a = new TimeTable2();


        //                            if (t.WeekDay !=null && t.WeekDay != "-1" && t.WeekDay != "0")
        //                            {
        //                                int dayid = Convert.ToInt32(t.WeekDay);
        //                                var week = db.tblWeekDays.Where(x => x.DayID == dayid).SingleOrDefault();
        //                                a.WeekDay = week.WeekDay;
        //                            }

        //                            a.timingNm = SsdtTime.ToString("hh:mm:ss tt ") + "-" + EddtTime.ToString("hh:mm:ss tt ");
        //                            a.IsBreak = ctd.IsBreak;
        //                            if (t.SubjectID != null && t.TeacherID != -1)
        //                            {
        //                                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectID).SingleOrDefault();
        //                                a.subjectNm = sub.Subject;
        //                            }
        //                            else { a.subjectNm = ""; }
        //                            if (t.TeacherID != null && t.TeacherID != -1)
        //                            {
        //                                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherID).SingleOrDefault();
        //                                a.teacherNm = teacher.FirstName + " " + teacher.LastName;
        //                            }
        //                            else { a.teacherNm = ""; }

        //                            att.ttList.Add(a);

        //                        }
        //                        ct.timetable.Add(att);
        //                        obj.status = true;
        //                        obj.message = "Sucess";
        //                        obj.data = ct;

        //                    }

        //                }


        //            }
        //            else
        //            {
        //                obj.status = false;
        //                obj.message = "No Timetable Assigend";
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //    }

        //    return obj;
        //}


        //[System.Web.Http.Route("api/TimeTableApi/getTimeTableConfigDetapp")]
        //[System.Web.Http.HttpPost]
        //public TimeTableapp getTimeTableConfigDetapp(TimeTable val)
        //{



        //    //List<TimeTable> list = new List<Models.TimeTable>();
        //    TimeTableapp obj = new TimeTableapp();
        //    TimeTable ct = new Models.TimeTable();
        //    try
        //    {
        //        if (val.Classid.Equals(0) || val.Classid.Equals(null) || "".Equals(val.Classid))

        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter ClassID";
        //        }
        //        else if ("".Equals(val.Section) || val.Section.Equals(null) || "".Equals(val.Section))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Section";
        //        }
        //        else if (val.SchoolID.Equals(0) || val.SchoolID.Equals(null) || "".Equals(val.SchoolID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else
        //        {
        //            int classID = Convert.ToInt32(val.Classid);
        //            int SectionID = Convert.ToInt32(val.Section);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int timetableid = 0;

        //            var timetablecon = db.tblTimeTableConfigs.Where(x => x.ClassID == classID && x.SectionID == SectionID && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
        //            if (timetablecon != null)
        //            {

        //                timetableid = Convert.ToInt32(timetablecon.ID);


        //                int count = 0;
        //                int id = Convert.ToInt32(timetableid);
        //                var m = (from c in db.tblTimeTableConfigs
        //                         join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
        //                         join cl in db.tblCourses on c.ClassID equals cl.Id
        //                         join s in db.tblSections on c.SectionID equals s.Id
        //                         where c.ID == id && c.IsDeleted == null
        //                         select new
        //                         {
        //                             model = c,
        //                             DtFrom = ac.DateFrom,
        //                             DtTo = ac.DateTo,
        //                             courseNm = cl.CourseName,
        //                             sectionNm = s.SectionName
        //                         }).SingleOrDefault();

        //                count++;
        //                tblTimeTableConfig cls = new tblTimeTableConfig();
        //                ct.CountID = count;
        //                ct.ID = m.model.ID;
        //                cls = m.model;
        //                ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
        //                ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
        //                ct.Course = m.courseNm + "-" + m.sectionNm;
        //                ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                ct.con = cls;
        //                if (ct.con.Status == 0)
        //                {
        //                    ct.StatusNm = "Inactive";
        //                    ct.style = "btn btn-block btn-danger btn-sm";
        //                    ct.action = "Activate";
        //                }
        //                else
        //                {
        //                    ct.StatusNm = "Active";
        //                    ct.style = "btn btn-block btn-success btn-sm";
        //                    ct.action = "Inactivate";
        //                }
        //                if (ct.con.WeekDays != "")
        //                {
        //                    string days = "";
        //                    var w = ct.con.WeekDays.Split(',');
        //                    int i = 0;
        //                    foreach (string s in w)
        //                    {
        //                        i++;
        //                        int dayID = Convert.ToInt32(s);
        //                        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
        //                        if (i == 1)
        //                        {
        //                            days = week.WeekDay;
        //                        }
        //                        else
        //                        {
        //                            days = days + "," + week.WeekDay;
        //                        }
        //                    }
        //                    ct.WeekDaysNm = days;

        //                    var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == m.model.CT_ID && x.IsDeleted == null).ToList();


        //                    ct.TimeTableList = new List<TimeTable>();
        //                    foreach (var ctd in classTimingdet)
        //                    {
        //                        TimeTable a = new TimeTable();
        //                        a.ttList = new List<tblTimeTable>();
        //                        a.period = new tblClassTimingDet();
        //                        a.TimeTableCLS_list = new List<TimeTableCLS>();

        //                        var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
        //                        DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
        //                        DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);
        //                        a.timingNm = SsdtTime.ToString("hh:mm:ss tt ") + "-" + EddtTime.ToString("hh:mm:ss tt ");
        //                        a.period.IsBreak = ctd.IsBreak;
        //                        foreach (var t in timetable)
        //                        {
        //                            TimeTableCLS ttCLS = new TimeTableCLS();
        //                            ttCLS.ID = t.ID;
        //                            ttCLS.CTDet_ID = t.CTDet_ID;
        //                            ttCLS.CT_ID = t.CT_ID;
        //                            ttCLS.Status = t.Status;
        //                            ttCLS.SubjectID = t.SubjectID;
        //                            ttCLS.TeacherID = t.TeacherID;
        //                            ttCLS.TimeTableID = t.TimeTableID;
        //                            ttCLS.WeekDay = t.WeekDay;
        //                            if (t.SubjectID != null && t.TeacherID != -1)
        //                            {
        //                                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectID).SingleOrDefault();
        //                                ttCLS.subjectNm = sub.Subject;
        //                            }
        //                            else { ttCLS.subjectNm = ""; }
        //                            if (t.TeacherID != null && t.TeacherID != -1)
        //                            {
        //                                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherID).SingleOrDefault();
        //                                ttCLS.teacherNm = teacher.FirstName + " " + teacher.LastName;
        //                            }
        //                            else { ttCLS.teacherNm = ""; }
        //                            a.TimeTableCLS_list.Add(ttCLS);
        //                            //a.ttList.Add(t);
        //                        }
        //                        ct.TimeTableList.Add(a);
        //                        obj.status = true;
        //                        obj.message = "Sucess";
        //                        obj.data = ct;
        //                    }

        //                }

        //            }
        //            else
        //            {
        //                obj.status = false;
        //                obj.message = "No Timetable Assigend";
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //    }

        //    return obj;
        //}

        [System.Web.Http.Route("api/TimeTableApi/getTimeTableConfigDet")]
        [System.Web.Http.HttpPost]
        public TimeTable getTimeTableConfigDet(Parameters param)
        {
            //List<TimeTable> list = new List<Models.TimeTable>();

            TimeTable ct = new Models.TimeTable();
            try
            {
                int count = 0;
                int id = Convert.ToInt32(param.val);
                var m = (from c in db.tblTimeTableConfigs
                         join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                         //join cl in db.tblCourses on c.ClassID equals cl.Id
                         //join s in db.tblSections on c.SectionID equals s.Id
                         where /*c.ID == id &&*/ c.IsDeleted == null
                         select new
                         {
                             model = c,
                             DtFrom = ac.DateFrom,
                             DtTo = ac.DateTo,
                             //courseNm = cl.CourseName,
                             //sectionNm = s.SectionName
                         }).SingleOrDefault();

                var mm = (from c in db.TBLStudents
                              //join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                          join cl in db.tblCourses on c.ClassID equals cl.Id
                          join s in db.tblSections on c.SectionID equals s.Id
                          where c.ID == id && c.IsDeleted == null
                          select new
                          {
                              courseid = c.ClassID,
                              sectionid = c.SectionID,
                              courseNm = cl.CourseName,
                              sectionNm = s.SectionName
                          }).SingleOrDefault();

                var TID = (from c in db.tblClassTimings
                           where c.SectionId == mm.sectionid && c.ClassId == mm.courseid && c.IsDeleted == null
                           orderby c.ID descending
                           select new { model = c }).Take(1).SingleOrDefault();





                count++;
                tblTimeTableConfig cls = new tblTimeTableConfig();
                ct.CountID = count;
                ct.ID = m.model.ID;
                cls = m.model;
                ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
                ct.Course = mm.courseNm + "-" + mm.sectionNm;
                ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ct.con = cls;
                if (ct.con.Status == 0)
                {
                    ct.StatusNm = "Inactive";
                    ct.style = "btn btn-block btn-danger btn-sm";
                    ct.action = "Activate";
                }
                else
                {
                    ct.StatusNm = "Active";
                    ct.style = "btn btn-block btn-success btn-sm";
                    ct.action = "Inactivate";
                }
                if (ct.con.WeekDays != "")
                {
                    string days = "";
                    var w = ct.con.WeekDays.Split(',');
                    int i = 0;
                    foreach (string s in w)
                    {
                        i++;
                        int dayID = Convert.ToInt32(s);
                        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
                        if (i == 1)
                        {
                            days = week.WeekDay;
                        }
                        else
                        {
                            days = days + "," + week.WeekDay;
                        }
                    }
                    ct.WeekDaysNm = days;
                    var classTimingdet = (from tt in db.tblClassTimingDets where tt.CT_ID == TID.model.ID && tt.IsDeleted == null orderby tt.STime select new { tt.STime, tt.ETime, tt.IsBreak }).Distinct().ToList();
                    // var ts= (from tt in db.tblClassTimingDets select tt.STime,tt.Etime).Distinct().ToList();
                    //var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == TID.model.ID && x.IsDeleted == null).OrderBy(x=>x.STime).Distinct().ToList();


                    ct.TimeTableList = new List<TimeTable>();
                    foreach (var ctd in classTimingdet)
                    {
                        TimeTable a = new TimeTable();
                        a.ttList = new List<tblTimeTable>();
                        a.period = new tblClassTimingDet();
                        a.TimeTableCLS_list = new List<TimeTableCLS>();

                        //var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
                        var timetable = db.tblClassTimingDets.Where(x => x.STime == ctd.STime && x.ETime == ctd.ETime && x.IsDeleted == null && x.CT_ID == TID.model.ID).ToList();
                        DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
                        DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);



                        a.timingNm = SsdtTime.ToString("hh:mm tt ") + "-" + EddtTime.ToString("hh:mm tt ");
                        a.period.IsBreak = ctd.IsBreak;
                        foreach (var t in timetable)
                        {
                            TimeTableCLS ttCLS = new TimeTableCLS();
                            ttCLS.ID = t.ID;
                            ttCLS.CTDet_ID = t.CT_ID;
                            ttCLS.CT_ID = t.CT_ID;
                            ttCLS.Status = t.Status;
                            ttCLS.SubjectID = t.SubjectId;
                            ttCLS.TeacherID = t.TeacherId;
                            // ttCLS.TimeTableID = t.TimeTableID;
                            var day = db.tblWeekDays.Where(s => s.ID == t.WeekDays).SingleOrDefault();

                            ttCLS.WeekDaysNm = day.WeekDay;


                            if (t.SubjectId != null && t.TeacherId != -1)
                            {
                                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectId).SingleOrDefault();
                                ttCLS.subjectNm = sub.Subject;
                            }
                            else { ttCLS.subjectNm = ""; }
                            if (t.TeacherId != null && t.TeacherId != -1)
                            {
                                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherId).SingleOrDefault();
                                ttCLS.teacherNm = teacher.FirstName + " " + teacher.LastName;
                            }
                            else { ttCLS.teacherNm = ""; }
                            a.TimeTableCLS_list.Add(ttCLS);
                            //a.ttList.Add(t);
                        }
                        ct.TimeTableList.Add(a);
                    }

                }




            }
            catch (Exception ex) { ct.ID = -1; throw ex; }


            return ct;
        }


        [System.Web.Http.Route("api/TimeTableApi/getStudentTimeTable")]
        [System.Web.Http.HttpGet]
        public TimeTable getStudentTimeTable(int studentid)
        {
            //List<TimeTable> list = new List<Models.TimeTable>();

            TimeTable ct = new Models.TimeTable();
            try
            {
                int count = 0;
                int id = Convert.ToInt32(studentid);
                var m = (from c in db.tblTimeTableConfigs
                         join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                         //join cl in db.tblCourses on c.ClassID equals cl.Id
                         //join s in db.tblSections on c.SectionID equals s.Id
                         where /*c.ID == id &&*/ c.IsDeleted == null
                         select new
                         {
                             model = c,
                             DtFrom = ac.DateFrom,
                             DtTo = ac.DateTo,
                             //courseNm = cl.CourseName,
                             //sectionNm = s.SectionName
                         }).SingleOrDefault();

                var mm = (from c in db.TBLStudents
                              //join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
                          join cl in db.tblCourses on c.ClassID equals cl.Id
                          join s in db.tblSections on c.SectionID equals s.Id
                          where c.ID == id && c.IsDeleted == null
                          select new
                          {
                              courseid = c.ClassID,
                              sectionid = c.SectionID,
                              courseNm = cl.CourseName,
                              sectionNm = s.SectionName
                          }).SingleOrDefault();

                var TID = (from c in db.tblClassTimings
                           where c.SectionId == mm.sectionid && c.ClassId == mm.courseid && c.IsDeleted == null
                           orderby c.ID descending
                           select new { model = c }).Take(1).SingleOrDefault();





                count++;
                tblTimeTableConfig cls = new tblTimeTableConfig();
                ct.CountID = count;
                ct.ID = m.model.ID;
                cls = m.model;
                ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
                ct.Course = mm.courseNm + "-" + mm.sectionNm;
                ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ct.con = cls;
                if (ct.con.Status == 0)
                {
                    ct.StatusNm = "Inactive";
                    ct.style = "btn btn-block btn-danger btn-sm";
                    ct.action = "Activate";
                }
                else
                {
                    ct.StatusNm = "Active";
                    ct.style = "btn btn-block btn-success btn-sm";
                    ct.action = "Inactivate";
                }
                if (ct.con.WeekDays != "")
                {
                    string days = "";
                    var w = ct.con.WeekDays.Split(',');
                    int i = 0;
                    foreach (string s in w)
                    {
                        i++;
                        int dayID = Convert.ToInt32(s);
                        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
                        if (i == 1)
                        {
                            days = week.WeekDay;
                        }
                        else
                        {
                            days = days + "," + week.WeekDay;
                        }
                    }
                    ct.WeekDaysNm = days;
                    var classTimingdet = (from tt in db.tblClassTimingDets where tt.CT_ID == TID.model.ID && tt.IsDeleted == null orderby tt.STime select new { tt.STime, tt.ETime, tt.IsBreak }).Distinct().ToList();
                    // var ts= (from tt in db.tblClassTimingDets select tt.STime,tt.Etime).Distinct().ToList();
                    //var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == TID.model.ID && x.IsDeleted == null).OrderBy(x=>x.STime).Distinct().ToList();


                    ct.TimeTableList = new List<TimeTable>();
                    foreach (var ctd in classTimingdet)
                    {
                        TimeTable a = new TimeTable();
                        a.ttList = new List<tblTimeTable>();
                        a.period = new tblClassTimingDet();
                        a.TimeTableCLS_list = new List<TimeTableCLS>();

                        //var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
                        var timetable = db.tblClassTimingDets.Where(x => x.STime == ctd.STime && x.ETime == ctd.ETime && x.IsDeleted == null && x.CT_ID == TID.model.ID).ToList();
                        DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
                        DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);



                        a.timingNm = SsdtTime.ToString("hh:mm tt ") + "-" + EddtTime.ToString("hh:mm tt ");
                        a.period.IsBreak = ctd.IsBreak;
                        foreach (var t in timetable)
                        {
                            TimeTableCLS ttCLS = new TimeTableCLS();
                            ttCLS.ID = t.ID;
                            ttCLS.CTDet_ID = t.CT_ID;
                            ttCLS.CT_ID = t.CT_ID;
                            ttCLS.Status = t.Status;
                            ttCLS.SubjectID = t.SubjectId;
                            ttCLS.TeacherID = t.TeacherId;
                            // ttCLS.TimeTableID = t.TimeTableID;
                            var day = db.tblWeekDays.Where(s => s.ID == t.WeekDays).SingleOrDefault();

                            ttCLS.WeekDaysNm = day.WeekDay;


                            if (t.SubjectId != null && t.TeacherId != -1)
                            {
                                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectId).SingleOrDefault();
                                ttCLS.subjectNm = sub.Subject;
                            }
                            else { ttCLS.subjectNm = ""; }
                            if (t.TeacherId != null && t.TeacherId != -1)
                            {
                                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherId).SingleOrDefault();
                                ttCLS.teacherNm = teacher.FirstName + " " + teacher.LastName;
                            }
                            else { ttCLS.teacherNm = ""; }
                            a.TimeTableCLS_list.Add(ttCLS);
                            //a.ttList.Add(t);
                        }
                        ct.TimeTableList.Add(a);
                    }

                }




            }
            catch (Exception ex) { ct.ID = -1; throw ex; }


            return ct;
        }
        //AV
        [System.Web.Http.Route("api/TimeTableApi/GetSubjectsbyLOGIID")]
        [System.Web.Http.HttpPost]
        public tblSubject[] GetSubjectsbyLOGIID(List<string> aa)
        {
            string username = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<tblSubject> list = new List<tblSubject>();
            try
            {

                if (typeuser == 2)
                {
                    var result = db.tblSubjects.Where(x => x.Status == true && x.IsDeleted == null).ToList();
                    //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                    foreach (var a in result)
                    {
                        tblSubject items = new tblSubject();
                        items.Subject = a.Subject;
                        items.ID = a.ID;
                        list.Add(items);
                    }
                }
                else
                {
                    var School = db.tblEmployees.SingleOrDefault(x => x.UserID == username && x.IsDeleted == null);

                    int SchoolID = Convert.ToInt32(School.SchoolID);

                    var result = db.tblSubjects.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
                    //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                    foreach (var a in result)
                    {
                        tblSubject items = new tblSubject();
                        items.Subject = a.Subject;
                        items.ID = a.ID;
                        list.Add(items);
                    }
                }

            }




            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/TimeTableApi/GetSubjectsbySCHOOLidbyclass")]
        [System.Web.Http.HttpPost]
        public tblSubject[] GetSubjectsbySCHOOLidbyclass(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            int classid = Convert.ToInt32(aa[1]);
            List<tblSubject> list = new List<tblSubject>();

            try
            {
                var result = db.tblSubjects.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null && x.classid == classid).OrderBy(x => x.Subject).ToList();

                // var result = db.tblSubjects.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSubject items = new tblSubject();
                    items.Subject = a.Subject;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/TimeTableApi/GetSectionbySCHOOLidbyclass")]
        [System.Web.Http.HttpPost]
        public tblSection[] GetSectionbySCHOOLidbyclass(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            int classid = Convert.ToInt32(aa[1]);
            List<tblSection> list = new List<tblSection>();

            try
            {
                var result = db.tblSections.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null && x.ClassId == classid).OrderBy(x => x.SectionName).ToList();

                // var result = db.tblSubjects.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSection items = new tblSection();
                    items.SectionName = a.SectionName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/TimeTableApi/GetSubjectsbySCHOOLid")]
        [System.Web.Http.HttpPost]
        public tblSubject[] GetSubjectsbySCHOOLid(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<tblSubject> list = new List<tblSubject>();

            try
            {
                var result = db.tblSubjects.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSubject items = new tblSubject();
                    items.Subject = a.Subject;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetSubjectsbySCHOOLid1")]
        [System.Web.Http.HttpPost]
        public tblSubject[] GetSubjectsbySCHOOLid1(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            int Teacherid = Convert.ToInt32(aa[1]);
            int Classid = Convert.ToInt32(aa[2]);
            int Sectionid = Convert.ToInt32(aa[3]);
            List<tblSubject> list = new List<tblSubject>();

            try
            {
                var result = (from t in db.tblSubjectTeacherAllocates
                              join s in db.tblSubjects on t.SubjectID equals s.ID

                              where t.SchoolID == SchoolID && t.classid == Classid && t.SectionId == Sectionid && t.TeacherID == Teacherid
                              select new
                              {
                                  ID = s.ID,
                                  Subject = s.Subject,

                              }).ToList();

                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSubject items = new tblSubject();
                    items.Subject = a.Subject;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/TimeTableApi/GetSubjects")]
        [System.Web.Http.HttpPost]
        public tblSubject[] GetSubjects()
        {
            List<tblSubject> list = new List<tblSubject>();

            try
            {
                var result = db.tblSubjects.Where(x => x.Status == true && x.IsDeleted == null).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSubject items = new tblSubject();
                    items.Subject = a.Subject;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetSubjectTeachers")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetSubjectTeachers(List<string> val)
        {
            List<TimeTable> list = new List<TimeTable>();
            long subID = Convert.ToInt32(val[0]);
            try
            {
                var result = (from st in db.tblSubjectTeacherAllocates
                              join e in db.tblEmployees on st.TeacherID equals e.Id
                              where st.Status == true && st.SubjectID == subID
                              select new
                              {
                                  ID = st.ID,
                                  teacher = e.FirstName + " " + e.LastName + " (" + e.Empcode + ")"
                              }).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    TimeTable items = new TimeTable();
                    items.Msg = a.teacher;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetEmpsbyloginidbyClass")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetEmpsbyloginidbyClass(List<string> aa)
        {
            List<TimeTable> list = new List<TimeTable>();

            try
            {
                int SchoolID = Convert.ToInt32(aa[0]);
                int SubjectID = Convert.ToInt32(aa[1]);
                int ClassID = Convert.ToInt32(aa[2]);
                var result = (from subt in db.tblSubjectTeacherAllocates
                              join em in db.tblEmployees on subt.TeacherID equals em.Id
                              where subt.SchoolID == SchoolID && subt.SubjectID == SubjectID && subt.classid == ClassID && subt.Status == true && subt.IsDeleted == null
                              select new
                              {
                                  ID = em.Id,
                                  teacher = em.FirstName + " " + em.LastName + " (" + em.Empcode + ")"

                              }).ToList();





                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    TimeTable items = new TimeTable();
                    items.Msg = a.teacher;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();






        }





        [System.Web.Http.Route("api/TimeTableApi/GetEmpsbyloginid")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetEmpsbyloginid(List<string> aa)
        {
            List<TimeTable> list = new List<TimeTable>();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            try
            {




                if (typeuser == 2)
                {
                    var result = db.tblEmployees.Where(e => e.Status == true && e.IsDeleted == null).ToList();
                    //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                    foreach (var a in result)
                    {
                        TimeTable items = new TimeTable();
                        items.Msg = a.FirstName + " " + a.LastName + " (" + a.Empcode + ")";
                        items.ID = a.Id;
                        list.Add(items);
                    }
                }
                else
                {
                    var school = db.tblEmployees.SingleOrDefault(a => a.UserID == loginuser && a.IsDeleted == null);

                    int SchoolID = Convert.ToInt32(school.SchoolID);
                    var result = db.tblEmployees.Where(e => e.Status == true && e.SchoolID == SchoolID && e.IsDeleted == null).ToList();
                    //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                    foreach (var a in result)
                    {
                        TimeTable items = new TimeTable();
                        items.Msg = a.FirstName + " " + a.LastName + " (" + a.Empcode + ")";
                        items.ID = a.Id;
                        list.Add(items);
                    }
                }





            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/TimeTableApi/GetEmpsbySchoolID")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetEmpsbySchoolID(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<TimeTable> list = new List<TimeTable>();
            try
            {
                //designation
                int desgid = 0;
                var designation = db.tblDesignations.Where(u => u.SchoolID == SchoolID && u.Designation.Contains("Driver") && u.IsDeleted == null).FirstOrDefault();
                if (designation != null)
                {
                    //desgid
                    desgid = designation.DesigID;

                }
                else
                {
                    desgid = 0;

                }

                var result = db.tblEmployees.Where(e => e.Status == true && e.SchoolID == SchoolID && e.IsDeleted == null && e.DesigID != desgid).OrderBy(e => e.FirstName).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    TimeTable items = new TimeTable();
                    items.Msg = a.FirstName + " " + a.LastName + " (" + a.Empcode + ")";
                    items.ID = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/GetTeacherbySchoolID")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetTeacherbySchoolID(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<TimeTable> list = new List<TimeTable>();
            try
            {
                int ClassID = Convert.ToInt32(aa[1]);
                int SEctionId = Convert.ToInt32(aa[2]);

                var result = (from subt in db.tblSubjectTeacherAllocates
                              join em in db.tblEmployees on subt.TeacherID equals em.Id
                              where subt.SchoolID == SchoolID && subt.Status == true && subt.IsDeleted == null
                              && subt.classid == ClassID && subt.SectionId == SEctionId
                              select new
                              {
                                  ID = em.Id,
                                  teacher = em.FirstName + " " + em.LastName + " (" + em.Empcode + ")"

                              }).ToList().Distinct();
                foreach (var a in result)
                {
                    TimeTable items = new TimeTable();
                    items.Msg = a.teacher;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/TimeTableApi/GetEmps")]
        [System.Web.Http.HttpPost]
        public TimeTable[] GetEmps()
        {
            List<TimeTable> list = new List<TimeTable>();
            try
            {
                var result = db.tblEmployees.Where(e => e.Status == true).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    TimeTable items = new TimeTable();
                    items.Msg = a.FirstName + " " + a.LastName + " (" + a.Empcode + ")";
                    items.ID = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        //   able to select teacher for class ,which is already assigned to another class .

        [System.Web.Http.Route("api/TimeTableApi/CheckTeacher")]
        [System.Web.Http.HttpPost]
        public int CheckTeacher(List<string> val)
        {
            //int SchoolID = Convert.ToInt32(val[3]);
            int empid = Convert.ToInt32(val[0]);
            int ctdetID = Convert.ToInt32(val[1]);
            string day = val[2];
            TimeTable t = new Models.TimeTable();
            int result = db.tblTimeTables.Where(e => e.CTDet_ID == ctdetID && e.WeekDay.Trim() == day && e.TeacherID == empid && e.IsDeleted == null /*&& e.SchoolID== SchoolID*/).ToList().Count;

            return result;
        }

        //[System.Web.Http.Route("api/TimeTableApi/GetTimeTableIDapp")]
        //[System.Web.Http.HttpPost]
        //public long GetTimeTableIDapp(List<string> val)
        //{

        //    int stdId = Convert.ToInt32(val[0]);
        //    DateTime dt = DateTime.Now;
        //    long ttID = -1;
        //    try
        //    {
        //        var std = db.TBLStudents.Where(x => x.ID == stdId).SingleOrDefault();

        //        var result = db.tblTimeTableConfigs.Where(x => x.Status == 1 && x.ClassID == std.ClassID && x.SectionID == std.SectionID && x.StartDT <= dt.Date && x.EndDt >= dt.Date && x.IsDeleted == null).SingleOrDefault();
        //        if (result != null)
        //        {
        //            ttID = result.ID;
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    return ttID;
        //}
        //[System.Web.Http.Route("api/TimeTableApi/GetTimeTableOFStudent")]
        //[System.Web.Http.HttpPost]
        //public long GetTimeTableOFStudent(List<string> val)
        //{
        //    int classID = Convert.ToInt32( val[1]);
        //    int SectionID = Convert.ToInt32(val[1]);
        //    int SchoolID = Convert.ToInt32(val[1]);
        //    int timetableid = 0;
        //    try
        //    {
        //        var timetablecon = db.tblTimeTableConfigs.Where(x => x.ClassID == classID && x.SectionID == SectionID && x.SchoolID == SchoolID).FirstOrDefault();
        //        if (timetablecon != null)
        //        {

        //            timetableid = Convert.ToInt32(timetablecon.ID);

        //        }


        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    return "";
        //}

        [System.Web.Http.Route("api/TimeTableApi/GetTimeTableID")]
        [System.Web.Http.HttpPost]
        public long GetTimeTableID(List<string> val)
        {
            int stdId = Convert.ToInt32(val[0]);
            DateTime dt = DateTime.Now;
            long ttID = -1;
            try
            {
                var std = db.TBLStudents.Where(x => x.ID == stdId).SingleOrDefault();
                int CurrenyAy = 0;
                var CaY = db.tblAcademicYears.Where(x => x.CurrActive == true && x.SchoolID == std.SchoolID && x.IsDeleted == null).SingleOrDefault();
                if (CaY != null)
                {
                    CurrenyAy = CaY.ID;
                }

                var result = db.tblTimeTableConfigs.Where(x => x.Status == 1 && x.ClassID == std.ClassID && x.SectionID == std.SectionID && x.StartDT <= dt.Date && x.EndDt >= dt.Date && x.IsDeleted == null && x.AcademicYear == CurrenyAy).SingleOrDefault();
                if (result != null)
                {
                    ttID = result.ID;
                }
            }
            catch (Exception ex)
            { throw ex; }
            return ttID;
        }

        [System.Web.Http.Route("api/TimeTableApi/CheckTeachingStaff")]
        [System.Web.Http.HttpPost]
        public bool CheckTeachingStaff(List<string> val)
        {
            int empid = Convert.ToInt32(val[0]);
            try
            {
                var result = (from e in db.tblEmployees
                              join c in db.tblStaffCategories on e.StaffCategory equals c.Id
                              where e.Id == empid && e.StaffCategory == 1 && c.Status == true
                              select new { e }
                             ).SingleOrDefault();

                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            { throw ex; }
            return false;
        }

        [System.Web.Http.Route("api/TimeTableApi/getTimeTableDetforTeacher")]
        [System.Web.Http.HttpPost]
        public TimeTable getTimeTableDetforTeacher(Parameters param)
        {
            //List<TimeTable> list = new List<Models.TimeTable>();
            TimeTable ct = new Models.TimeTable();
            ct.TimeTableList = new List<TimeTable>();
            try
            {
                //  int count = 0;
                int id = Convert.ToInt32(param.val);
                ct.ID = 1;
                var m = (from ac in db.tblClassTimingDets
                             // join ac in db.tblClassTimingDets on tt.CTDet_ID equals ac.CT_ID
                         join tc in db.tblClassTimings on ac.CT_ID equals tc.ID
                         join crs in db.tblCourses on tc.ClassId equals crs.Id
                         join s in db.tblSections on tc.SectionId equals s.Id
                         join sub in db.tblSubjects on ac.SubjectId equals sub.ID
                         join e in db.tblEmployees on ac.TeacherId equals e.Id
                         join d in db.tblDepartmnets on e.DeptID equals d.DepartmentId
                         join des in db.tblDesignations on e.DesigID equals des.DesigID
                         where ac.TeacherId == id
                         orderby ac.STime
                         select new
                         {

                             ac,
                             tc,
                             crs,
                             s,
                             sub,
                             e,
                             d,
                             des

                         }).ToList();
                var cted = (from tt in db.tblTimeTables select tt.CTDet_ID).Distinct().ToList();
                //ct.CountID = cted.Count;
                foreach (var x in cted)
                {
                    TimeTable t = new TimeTable();
                    t.TimeTableList = new List<TimeTable>();
                    foreach (var mm in m)
                    {
                        //if (mm.tt.CTDet_ID == x)
                        //{

                        ct.TeacherName = mm.e.FirstName + " " + mm.e.LastName;
                        TimeTable tm = new TimeTable();

                        DateTime SsdtTime = DateTime.Now.Date + mm.ac.STime;
                        DateTime EddtTime = DateTime.Now.Date + mm.ac.ETime;
                        DateTime Sttdtt1 = SsdtTime.AddMinutes(-1);
                        DateTime Enddtt2 = EddtTime.AddMinutes(1);
                        //usr.StartTime = Sttdtt1.ToString("hh:mm tt ");
                        //usr.EndTime = Enddtt2.ToString("hh:mm tt ");

                        tm.timingNm = Sttdtt1.ToString("hh:mm tt ") + "-" + Enddtt2.ToString("hh:mm tt ");

                        tm.Course = mm.crs.CourseName + "-" + mm.s.SectionName;
                        int day = Convert.ToInt32(mm.ac.WeekDays);
                        var week = db.tblWeekDays.Where(y => y.DayID == day).SingleOrDefault();
                        tm.WeekDaysNm = week.WeekDay;
                        tm.subject = mm.sub.Subject;
                        t.TimeTableList.Add(tm);
                    }
                    // }
                    t.CountID = t.TimeTableList.Count;
                    ct.TimeTableList.Add(t);
                }
                ct.CountID = ct.TimeTableList.Count;
            }
            catch (Exception ex) { ct.ID = -1; throw ex; }
            return ct;
        }

        [System.Web.Http.Route("api/TimeTableApi/SearchSubTeacherDet")]
        [System.Web.Http.HttpPost]
        public TimeTable[] SearchSubTeacherDet(List<string> val)
        {
            List<TimeTable> list = new List<TimeTable>();
            try
            {

                int count = 0;
                var result = (from c in db.tblSubjectTeacherAllocates
                              join s in db.tblSchoolDetails
                              on c.SchoolID equals s.ID
                              where c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                //order by name.

                if (val[0] != "" && Convert.ToInt32(val[0]) != -1)
                {
                    int teacher = Convert.ToInt32(val[0]);
                    result = result.Where(c => c.model.TeacherID == teacher).ToList();
                }

                if (val[1] != "" && Convert.ToInt32(val[1]) != -1)
                {
                    long sub = Convert.ToInt32(val[1]);
                    result = result.Where(c => c.model.SubjectID == sub).ToList();

                }
                if (val[2] != "" && Convert.ToInt32(val[2]) != -1 && val[2] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[2]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                if (val[3] != "" && Convert.ToInt32(val[3]) != -1 && val[3] != "0")
                {
                    int ClassID = Convert.ToInt32(val[3]);
                    result = result.Where(c => c.model.classid == ClassID).ToList();
                }

                foreach (var m in result)
                {
                    count++;
                    TimeTable ct = new Models.TimeTable();
                    ct.sta = new tblSubjectTeacherAllocate();
                    ct.sta = m.model;
                    ct.CountID = count;
                    ct.ID = m.model.ID;
                    ct.School = m.SchoolName;
                    ct.Classid = Convert.ToInt32(m.model.classid);
                    var ad = db.tblCourses.Where(s => s.Id == ct.Classid).SingleOrDefault();
                    ct.classname = ad.CourseName;
                    var d = db.tblSubjects.SingleOrDefault(b => b.ID == m.model.SubjectID);
                    ct.subject = d.Subject;

                    var e = db.tblEmployees.SingleOrDefault(x => x.Id == m.model.TeacherID);
                    ct.TeacherName = e.FirstName + " " + e.LastName + " (" + e.Empcode + ")";
                    long SchoolIDD = Convert.ToInt32(val[2]);
                    //int ClassIDD = Convert.ToInt32(val[3]);
                    var subs = (from tt in db.tblSubjectTeacherAllocates where tt.TeacherID == m.model.TeacherID && tt.IsDeleted == null && tt.SchoolID == SchoolIDD /*&& tt.classid== ClassIDD */select tt.SubjectID).Distinct().ToList();
                    int i = 0;
                    foreach (var s in subs)
                    {
                        if (i == 0)
                        {
                            ct.Course = s.ToString();
                        }
                        else
                        {
                            ct.Course = ct.Course + "," + s.ToString();
                        }
                        i++;
                    }
                    ct.SchoolID = Convert.ToInt32(m.model.SchoolID);
                    list.Add(ct);
                }
            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();

            //        var predicate = PredicateBuilder.True<tblSubjectTeacherAllocate>();

            //    if (val[0]!="" && Convert.ToInt32(val[0]) != -1)
            //    {
            //        int teacher = Convert.ToInt32(val[0]);
            //        predicate = predicate.And(x => x.TeacherID == teacher);
            //    }
            //    if (val[1] != "" && Convert.ToInt32(val[1]) != -1)
            //    {
            //        long sub = Convert.ToInt32(val[1]);
            //        predicate = predicate.And(x => x.SubjectID == sub);
            //    }
            //    if (val[2] != "" && Convert.ToInt32(val[2]) != -1)
            //    {
            //        long SchoolID = Convert.ToInt32(val[2]);
            //        predicate = predicate.And(x => x.SubjectID == SchoolID);
            //    }
            //   int count = 0;


            //  var result =  db.tblSubjectTeacherAllocates.AsExpandable().Where(predicate).ToList();

            //    foreach (var m in result)
            //    {
            //        count++;
            //        TimeTable ct = new Models.TimeTable();
            //        ct.sta = new tblSubjectTeacherAllocate();
            //        ct.sta = m;
            //        ct.CountID = count;
            //        ct.ID = m.ID;

            //        var d = db.tblSubjects.SingleOrDefault(b => b.ID == m.SubjectID);
            //        ct.subject = d.Subject;

            //        var e = db.tblEmployees.SingleOrDefault(x=>x.Id==m.TeacherID);
            //        ct.TeacherName = e.FirstName + " " + e.LastName+" ("+e.Empcode+")";

            //        var subs= (from tt in db.tblSubjectTeacherAllocates where tt.TeacherID== m.TeacherID select tt.SubjectID).Distinct().ToList();
            //        int i = 0;
            //        foreach (var s in subs)
            //        {
            //            if(i==0)
            //            {
            //                ct.Course = s.ToString();
            //            }
            //            else
            //            {
            //                ct.Course = ct.Course + "," + s.ToString();
            //            }
            //            i++;
            //        }

            //        list.Add(ct);
            //    }
            //}
            //catch (Exception ex) { throw ex; }
            //return list.ToArray();
        }

        [System.Web.Http.Route("api/TimeTableApi/CheckSubTeacherAvailabiltity")]
        [System.Web.Http.HttpPost]
        public tblSubjectTeacherAllocate[] CheckSubTeacherAvailabiltity(List<int> val)
        {
            int empid = val[0];
            int SchoolID = val[1];
            var list = db.tblSubjectTeacherAllocates.Where(x => x.TeacherID == empid && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/TimeTableApi/getEmployeeBySubjectIdlogin")]
        [System.Web.Http.HttpPost]
        public TimeTable[] getEmployeeBySubjectIdlogin(List<string> val)
        {
            List<TimeTable> list = new List<TimeTable>();
            int typeuser = Convert.ToInt32(val[2]);
            string logid = val[1];
            try
            {


                if (typeuser == 2)
                {
                    sqlHelper obj = new sqlHelper();
                    DataTable dt = obj.getDataTable(@"select  e.FirstName+' '+e.MiddleName+' '+e.LastName+ '('+e.Empcode+')' Name,st.TeacherID Id from tblSubjectTeacherAllocate st 
                                                         left outer join tblemployee e on e.Id = st.TeacherID
                                                         where  st.IsDeleted is null and st.SubjectID =" + val[0]);

                    foreach (DataRow dr in dt.Rows)
                    {
                        TimeTable items = new TimeTable();
                        items.Msg = dr["Name"].ToString();
                        items.ID = Convert.ToInt64(dr["Id"].ToString());
                        list.Add(items);
                    }
                }
                else
                {
                    var School = db.tblEmployees.SingleOrDefault(x => x.UserID == logid);
                    int SchoolID = Convert.ToInt32(School.SchoolID);
                    int subjectid = Convert.ToInt32(val[0]);
                    sqlHelper obj = new sqlHelper();
                    DataTable dt = obj.getDataTable(@"select  e.FirstName+' '+e.MiddleName+' '+e.LastName+ '('+e.Empcode+')' Name,st.TeacherID Id from tblSubjectTeacherAllocate st 
                                                         left outer join tblemployee e on e.Id = st.TeacherID
                                                         where st.SchoolID='" + SchoolID + "' and st.SubjectID ='" + subjectid + "' and st.IsDeleted is null and e.IsDeleted is null");

                    foreach (DataRow dr in dt.Rows)
                    {
                        TimeTable items = new TimeTable();
                        items.Msg = dr["Name"].ToString();
                        items.ID = Convert.ToInt64(dr["Id"].ToString());
                        list.Add(items);
                    }

                }


            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/TimeTableApi/getEmployeeBySubjectId")]
        [System.Web.Http.HttpPost]
        public TimeTable[] getEmployeeBySubjectId(List<string> val)
        {
            List<TimeTable> list = new List<TimeTable>();
            try
            {

                sqlHelper obj = new sqlHelper();
                DataTable dt = obj.getDataTable(@"select  e.FirstName+' '+e.MiddleName+' '+e.LastName+ '('+e.Empcode+')' Name,st.TeacherID Id from tblSubjectTeacherAllocate st 
                                                         left outer join tblemployee e on e.Id = st.TeacherID
                                                         where st.SubjectID =" + val[0]);

                foreach (DataRow dr in dt.Rows)
                {
                    TimeTable items = new TimeTable();
                    items.Msg = dr["Name"].ToString();
                    items.ID = Convert.ToInt64(dr["Id"].ToString());
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


    }
}
