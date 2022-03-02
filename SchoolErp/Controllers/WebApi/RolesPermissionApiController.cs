using SchoolErp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace SchoolErp.Controllers.WebApi
{
    public class RolesPermissionApiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        //to get rolemanagemet checkboxes
        [System.Web.Http.Route("api/RolesPermissionApi/GetCheckBoxSubModulesSuperadmin")]
        [System.Web.Http.HttpPost]
        public RoleAssignment1 GetCheckBoxSubModulesSuperadmin(List<string> val1)
        {
            RoleAssignment1 a = new Models.RoleAssignment1();
            a.RList = new List<RoleAssignment1>();
            try
            {
                int count = 0;
                var Smodules = db.tblSuperAdminModules.ToList();
                foreach (var val in Smodules)
                {
                    RoleAssignment1 r0 = new RoleAssignment1();
                    count++;
                    r0.m = val;
                    r0.ID = count;
                    r0.subModList = new List<tblSuperAdminSubModule>();
                    var SSuubmodules = db.tblSuperAdminSubModules.Where(x => x.ModuleID == val.ModuleID).ToList();
                    foreach (var t in SSuubmodules)
                    {
                        r0.subModList.Add(t);
                    }
                    a.RList.Add(r0);
                }
                a.ID = 1;
            }
            catch (Exception ex) { a.ID = -1; throw ex; }
            return a;

        }

        [System.Web.Http.Route("api/RolesPermissionApi/checkRoleManagementRigtsSchool")]
        [System.Web.Http.HttpPost]
        public RoleAssignment checkRoleManagementRigtsSchool(List<int> val)
        {

            int SchoolID = val[0];
            RoleAssignment a = new Models.RoleAssignment();
            a.list = new List<int>();
            try
            {
                var checktabs = db.tblModules.Where(u => u.SchoolID == SchoolID).ToList();
                foreach (var m in checktabs)
                {
                    a.list.Add(m.ModuleID);


                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return a;
        }
        [System.Web.Http.Route("api/RolesPermissionApi/GetCheckBoxModSubMods")]
        [System.Web.Http.HttpPost]
        public RoleAssignment GetCheckBoxModSubMods(List<string> val)
        {
            int SchoolID = Convert.ToInt32(val[0]);
            //List<TimeTable> list = new List<Models.TimeTable>();
            RoleAssignment r = new Models.RoleAssignment();
            r.RList = new List<RoleAssignment>();
            try
            {
                int count = 0;
                //int id = Convert.ToInt32(val[0]);
                var mods = db.tblModules.Where(u => u.SchoolID == SchoolID).ToList();
                foreach (var m in mods)
                {
                    RoleAssignment r0 = new RoleAssignment();
                    count++;
                    r0.m = m;
                    r0.ID = count;
                    r0.subModList = new List<tblSubModule>();
                    var subMods = db.tblSubModules.Where(x => x.ModuleID == m.ModuleID && x.SchoolID == SchoolID).ToList();
                    foreach (var s in subMods)
                    {
                        r0.subModList.Add(s);
                    }
                    r.RList.Add(r0);
                }
                r.ID = 1;
                //tblTimeTableConfig cls = new tblTimeTableConfig();
                //ct.CountID = count;
                //ct.ID = m.model.ID;
                //cls = m.model;
                //ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
                //ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
                //ct.Course = m.courseNm + "-" + m.sectionNm;
                //ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //ct.con = cls;
                //if (ct.con.Status == 0)
                //{
                //    ct.StatusNm = "Inactive";
                //    ct.style = "btn btn-block btn-danger btn-sm";
                //    ct.action = "Activate";
                //}
                //else
                //{
                //    ct.StatusNm = "Active";
                //    ct.style = "btn btn-block btn-success btn-sm";
                //    ct.action = "Inactivate";
                //}
                //if (ct.con.WeekDays != "")
                //{
                //    string days = "";
                //    var w = ct.con.WeekDays.Split(',');
                //    int i = 0;
                //    foreach (string s in w)
                //    {
                //        i++;
                //        int dayID = Convert.ToInt32(s);
                //        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
                //        if (i == 1)
                //        {
                //            days = week.WeekDay;
                //        }
                //        else
                //        {
                //            days = days + "," + week.WeekDay;
                //        }
                //    }
                //    ct.WeekDaysNm = days;

                //    var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == m.model.CT_ID).ToList();


                //    ct.TimeTableList = new List<TimeTable>();
                //    foreach (var ctd in classTimingdet)
                //    {
                //        TimeTable a = new TimeTable();
                //        a.ttList = new List<tblTimeTable>();
                //        a.period = new tblClassTimingDet();
                //        a.TimeTableCLS_list = new List<TimeTableCLS>();

                //        var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID).ToList();
                //        a.timingNm = ctd.STime + "-" + ctd.ETime;
                //        a.period.IsBreak = ctd.IsBreak;
                //        foreach (var t in timetable)
                //        {
                //            TimeTableCLS ttCLS = new TimeTableCLS();
                //            ttCLS.ID = t.ID;
                //            ttCLS.CTDet_ID = t.CTDet_ID;
                //            ttCLS.CT_ID = t.CT_ID;
                //            ttCLS.Status = t.Status;
                //            ttCLS.SubjectID = t.SubjectID;
                //            ttCLS.TeacherID = t.TeacherID;
                //            ttCLS.TimeTableID = t.TimeTableID;
                //            ttCLS.WeekDay = t.WeekDay;
                //            if (t.SubjectID != null && t.TeacherID != -1)
                //            {
                //                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectID).SingleOrDefault();
                //                ttCLS.subjectNm = sub.Subject;
                //            }
                //            else { ttCLS.subjectNm = ""; }
                //            if (t.TeacherID != null && t.TeacherID != -1)
                //            {
                //                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherID).SingleOrDefault();
                //                ttCLS.teacherNm = teacher.FirstName + " " + teacher.LastName;
                //            }
                //            else { ttCLS.teacherNm = ""; }
                //            a.TimeTableCLS_list.Add(ttCLS);
                //            //a.ttList.Add(t);
                //        }
                //        ct.TimeTableList.Add(a);
                //    }

                //}

            }
            catch (Exception ex) { r.ID = -1; throw ex; }
            return r;
        }
        [System.Web.Http.Route("api/SchoolRolesManagementApi/SaveRecord")]
        [System.Web.Http.HttpPost]
        public RoleAssignment1 SaveRecord(RoleAssignment1 a)
        {
            RoleAssignment1 av = new RoleAssignment1();
            if (a.RList.Count > 0)
            {
                int SchoolID = a.SchoolID;

                var Module = db.tblModules.Where(m => m.SchoolID == SchoolID).ToList();
                foreach (var r0 in Module)
                {
                    db.tblModules.Remove(r0);
                }
                var CHECK = db.tblSubModules.Where(c => c.SchoolID == SchoolID).ToList();
                foreach (var data in CHECK)
                {
                    db.tblSubModules.Remove(data);
                }
                db.SaveChanges();

                foreach (var r1 in a.RList)
                {
                    tblModule samod = new tblModule();
                    string module = db.tblSuperAdminModules.SingleOrDefault(x => x.ModuleID == r1.SubModule).Module;
                    samod.ModuleID = r1.SubModule;
                    samod.Module = module;
                    samod.SchoolID = a.SchoolID;
                    samod.Status = true;
                    db.tblModules.Add(samod);
                    db.SaveChanges();
                    foreach (var r2 in r1.list)
                    {

                        tblSubModule sasubodule = new tblSubModule();
                        sasubodule.ModuleID = r1.SubModule;
                        sasubodule.SchoolID = a.SchoolID;
                        sasubodule.Status = true;
                        sasubodule.SubModuleID = r2;
                        string submodname = db.tblSuperAdminSubModules.SingleOrDefault(x => x.SubModuleID == sasubodule.SubModuleID).SubModule;
                        sasubodule.SubModule = submodname;
                        db.tblSubModules.Add(sasubodule);
                        db.SaveChanges();

                    }

                }


            }
            else
            {
                int SchoolID = a.SchoolID;

                var Module = db.tblModules.Where(m => m.SchoolID == SchoolID).ToList();
                foreach (var r0 in Module)
                {
                    db.tblModules.Remove(r0);
                }
                var CHECK = db.tblSubModules.Where(c => c.SchoolID == SchoolID).ToList();
                foreach (var data in CHECK)
                {
                    db.tblSubModules.Remove(data);
                }
                db.SaveChanges();
            }

            //var getmodule= db.tblSuperAdminModules.Where(x=>x.ModuleID==a.SubModule)




            return av;

        }

        [System.Web.Http.Route("api/RolesPermissionApi/SaveRecord")]
        [System.Web.Http.HttpPost]
        public RoleAssignment SaveRecord(RoleAssignment r)
        {
            try
            {
                if (r.roleID != 0)
                {
                    var model = new tblRoleAssign { RoleID = r.roleID };
                    db.Entry(model).State = EntityState.Deleted;

                    var roles = db.tblRoleSubModAssigns.Where(u => u.RoleID == r.roleID && u.SchoolID == r.SchoolID);

                    foreach (var u in roles)
                    {
                        db.tblRoleSubModAssigns.Remove(u);
                    }
                    db.SaveChanges();
                    if (r.RList.Count < 1)
                    {
                        r.ID = 1;
                        return r;
                    }
                }
                if (r.ID != -1 && r.ID != 0)
                {
                    tblRoleAssign ra = new SchoolErp.tblRoleAssign();
                    ra.EmpID = r.ID;
                    ra.RoleName = r.name;
                    ra.SchoolID = Convert.ToInt32(r.SchoolID);
                    db.tblRoleAssigns.Add(ra);
                    db.SaveChanges();
                    foreach (var r0 in r.RList)
                    {
                        foreach (var submod in r0.list)
                        {
                            tblRoleSubModAssign rsub = new tblRoleSubModAssign();
                            rsub.ModuleID = r0.ID;
                            rsub.SubmoduleID = submod;
                            rsub.RoleID = ra.RoleID;
                            rsub.SchoolID = r.SchoolID;
                            db.tblRoleSubModAssigns.Add(rsub);
                        }
                    }
                    db.SaveChanges();
                    r.ID = 1;
                }
            }
            catch (Exception ex)
            {
                r.ID = -1;
                throw ex;
            }
            return r;
        }
        [System.Web.Http.Route("api/RolesPermissionApi/GetAssignedRoleBySchool")]
        [System.Web.Http.HttpPost]
        public RoleAssignment1 GetassigenedRoleOfSchool(List<int> val)
        {
            int SchoolID = val[0];
            RoleAssignment1 r = new RoleAssignment1();
            r.smod = new List<tblSubModule>();
            r.list = new List<int>();
            try
            {
                var RR = (from m in db.tblModules
                          join sm in db.tblSubModules on m.ModuleID
                          equals sm.ModuleID
                          where m.SchoolID == SchoolID && sm.SchoolID == SchoolID
                          select new
                          {

                              m,
                              sm


                          }
                          ).ToList();

                foreach (var value in RR)
                {

                    r.list.Add(value.sm.ModuleID);
                    r.smod.Add(value.sm);


                }

                r.list = r.list.Distinct().ToList();
                r.ID = r.list.Count;
            }
            catch (Exception ex)
            {
                r.ID = -1;
                throw ex;
            }
            return r;

        }
        [System.Web.Http.Route("api/RolesPermissionApi/GetAssignedRoleManagementforSchool")]
        [System.Web.Http.HttpPost]
        public RoleAssignment1 GetAssignedRoleManagementforSchool(List<int> val)
        {
            int SchoolID = Convert.ToInt32(val[0]);
            RoleAssignment1 a = new Models.RoleAssignment1();
            a.smod = new List<tblSubModule>();
            a.list = new List<int>();
            try
            {
                var check = (from m in db.tblModules
                             join sm in db.tblSubModules on m.ModuleID equals sm.ModuleID
                             where
                             m.SchoolID == SchoolID && sm.SchoolID == SchoolID
                             select new
                             {
                                 m,
                                 sm
                             }
                             //1603
                             ).ToList();
                foreach (var r0 in check)
                {

                    a.list.Add(r0.sm.ModuleID);
                    a.smod.Add(r0.sm);
                }
                a.list = a.list.Distinct().ToList();
                a.smod.Distinct().ToList();
                a.ID = a.list.Count;
            }
            catch (Exception ex)
            {
                a.ID = -1;
                throw ex;
            }



            return a;
        }
        [System.Web.Http.Route("api/RolesPermissionApi/GetAssignedRoleByEmpID")]
        [System.Web.Http.HttpPost]
        public RoleAssignment GetAssignedRoleByEmpID(List<int> val)
        {
            //To STORE OBJ ARRAY MODEL
            //$.makeArray(obj)

            int empid = val[0];
            RoleAssignment r = new Models.RoleAssignment();
            r.RmodList = new List<tblRoleSubModAssign>();
            r.list = new List<int>();
            try
            {
                var result = (from a in db.tblRoleAssigns
                              join m in db.tblRoleSubModAssigns on a.RoleID equals m.RoleID
                              where a.EmpID == empid
                              select new { a, m }).ToList();
                foreach (var v in result)
                {
                    r.name = v.a.RoleName;
                    r.list.Add(v.m.ModuleID);
                    r.RmodList.Add(v.m);
                }
                r.list = r.list.Distinct().ToList();
                r.ID = r.list.Count;
            }
            catch (Exception ex)
            {
                r.ID = -1;
                throw ex;
            }

            return r;
        }
        [System.Web.Http.Route("api/RolesPermissionApi/CheckAttendenceRoleManagementforSchool")]
        [System.Web.Http.HttpPost]
        public int CheckAttendenceRoleManagementforSchool(List<int> val)
        {
            int checkvalue = 0;
            var SchoolID = val[0];
            var value = val[1];
            var check = db.tblModules.Where(u => u.SchoolID == SchoolID && u.ModuleID == value).FirstOrDefault();
            if (check != null)
            {
                checkvalue = check.ModuleID;
            }
            else
            {
                checkvalue = -1;
            }

            RoleAssignment aa = new RoleAssignment();
            return checkvalue;

        }


    }
}
