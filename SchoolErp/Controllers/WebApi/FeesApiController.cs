using LinqKit;
using SchoolErp.Models;
using schoolERP_BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace SchoolErp.Controllers.WebApi
{
    public class FeesApiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/FeesApi/SaveRecord")]
        [System.Web.Http.HttpPost]
        public int SaveRecord(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];

                if (a == "FeeCategory")
                {


                    var id = Convert.ToInt32(val[1]);
                    var bank = val[2].ToString().Trim();
                    var Ftype = val[3].ToString().Trim();
                    var refu = val[5].ToString().Trim();
                    var alias = val[4].ToString().Trim();
                    var SchoolID = Convert.ToInt32(val[6]);
                    var status = val[5].ToString().Trim() == "0" ? false : true;
                    if (id == 0)
                    {
                        var check = db.tblFeeCategories.Where(s => s.FeeCategory == bank && s.Refund == refu && s.Ftype == Ftype && s.SchoolID == SchoolID && s.IsDeleted == null).FirstOrDefault();
                        if (check == null)
                        {

                            tblFeeCategory cta = new SchoolErp.tblFeeCategory();
                            cta.FeeCategory = bank;
                            cta.Ftype = Ftype;
                            cta.Refund = refu;
                            cta.Cat_Desc = alias;
                            cta.Refundable = false;
                            cta.SchoolID = SchoolID;
                            db.tblFeeCategories.Add(cta);
                            avi = 1;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    else
                    {
                        var check = db.tblFeeCategories.Where(s => s.FeeCategory == bank && s.Ftype == Ftype && s.Refund == refu && s.ID != id && s.SchoolID == SchoolID && s.IsDeleted == null).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.tblFeeCategories.SingleOrDefault(b => b.ID == id);
                            result.FeeCategory = bank;
                            result.Ftype = Ftype;
                            result.Refund = refu;
                            result.Cat_Desc = alias;
                            result.Refundable = status;
                            result.SchoolID = SchoolID;
                            avi = 2;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    db.SaveChanges();

                }



                else if (a == "FeeConcession")
                {


                    var id = Convert.ToInt32(val[1]);
                    var bank = val[2].ToString().Trim();
                    var refu1 = val[3].ToString().Trim();
                    var alias = Convert.ToInt32(val[4]);
                    var SchoolID = Convert.ToInt32(val[5]);
                    var Academic_Year = Convert.ToInt32(val[6]);
                    if (id == 0)
                    {
                        var check = db.tblFeeHead.Where(s => s.TariffName == bank && s.Per == refu1 && s.TariffPer == alias && s.SchoolID == SchoolID && s.Academic_Year == Academic_Year && s.IsDeleted == null).FirstOrDefault();
                        if (check == null)
                        {

                            tblFeeHead cta = new SchoolErp.tblFeeHead();
                            cta.TariffName = bank;
                            cta.Per = refu1;
                            cta.TariffPer = alias;
                            cta.Academic_Year = Academic_Year;
                            cta.SchoolID = SchoolID;
                            db.tblFeeHead.Add(cta);
                            avi = 1;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    else
                    {
                        var check = db.tblFeeHead.Where(s => s.TariffName == bank && s.TariffPer == alias && s.SchoolID == SchoolID && s.ID != id && s.Academic_Year == s.Academic_Year && s.IsDeleted == null).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.tblFeeHead.SingleOrDefault(b => b.ID == id);
                            result.TariffName = bank;
                            result.TariffPer = alias;
                            result.Academic_Year = Academic_Year;
                            result.SchoolID = SchoolID;
                            avi = 2;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    db.SaveChanges();

                }


                else if (a == "FeeStructure")
                {
                    saveFeeStructure(val);
                }
                else if (a == "FeeConcessionHead")
                {
                    saveFeeHeadConcession(val);
                }

                else if (a == "ConcessionAllow")
                {
                    saveConcessionAllow(val);
                }

                else if (a == "FeeSubmission")
                {
                    saveFeeSubmission(val);
                }
                else if (a == "FeeStructureConfig")
                {
                    return saveFeeStructureConfig(val);
                }
                else if (a == "FeeCalculate")
                {
                    return saveFeeCalculate(val);
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



        public void saveConcessionAllow(List<string> val)
        {


            //var ID = Convert.ToInt32(val[1]);
            var studentid = Convert.ToInt32(val[2]);
            var Con = Convert.ToInt32(val[3]);
            var Amount = Convert.ToInt32(val[4]);
            var des = val[5].ToString().Trim();
            var Academic_Year = Convert.ToInt32(val[6]);
            var Active = val[7].ToString().Trim() == "0" ? false : true;
            var ClassID = Convert.ToInt32(val[8]);
            var SectionID = Convert.ToInt32(val[9]);
            var FeeHead = Convert.ToInt32(val[10]);
            var SchoolId = Convert.ToInt32(val[11]);

            var check = db.TariffDetail.Where(s => s.TariffID == Con && s.amount == Amount && s.status == Active && s.SectionID == SectionID && s.ClassID == ClassID && s.Studentid == studentid && s.Remarks == des && s.AcademicYearid == Academic_Year && s.FeeHeadID == FeeHead).FirstOrDefault();
            if (check == null)
            {

                TariffDetail cta = new SchoolErp.TariffDetail();
                cta.TariffID = Con;
                cta.amount = Amount;
                cta.status = Active;
                cta.ClassID = ClassID;
                cta.SectionID = SectionID;
                cta.Studentid = studentid;
                cta.AcademicYearid = Academic_Year;
                cta.Remarks = des;
                cta.FeeHeadID = FeeHead;
                cta.SchoolID = SchoolId;
                db.TariffDetail.Add(cta);

            }
            else
            {

            }


            db.SaveChanges();

        }


        public void saveFeeSubmission(List<string> val)
        {

            var studentid = Convert.ToInt32(val[2]);
            var Amount = Convert.ToInt32(val[3]);
            var desc = val[4].ToString().Trim();
            var Manual = val[5].ToString().Trim();
            var ClassID = Convert.ToInt32(val[6]);
            var SectionID = Convert.ToInt32(val[7]);
            var RegNo = val[8].ToString().Trim();
            var FatherName = val[9].ToString().Trim();
            var FirstName = val[10].ToString().Trim();
            var SchoolId = Convert.ToInt32(val[11]);
            var AcademicYear = Convert.ToInt32(val[12]);
            var plusminus = Convert.ToInt32(val[13]);
            var FeeHead = Convert.ToInt32(val[14]);
            var dated = DateTime.Now;
            //  var DoCNO = "DAV"+1;
            int voucherId = db.stdfee1.OrderByDescending(x => x.VoucherID).Select(y => y.VoucherID).FirstOrDefault() + 1;
            var DoCNO = "DAV" + voucherId;
            var BankName = val[15].ToString().Trim();
            var Branch = val[16].ToString().Trim();
            var DdCheq = val[17].ToString().Trim();
            var PUserID = val[18].ToString().Trim();
            var paymentby = val[19].ToString().Trim();
            var check = db.stdfee1.Where(s => s.StudentID == studentid && s.manualno == Manual && s.Amount == Amount && s.CourseID == ClassID && s.sem == SectionID && s.AdmNo == RegNo && s.Father == FatherName && s.Student == FirstName && s.InstituteID == SchoolId && s.Remarks == desc && s.Sessionid == AcademicYear && s.yearid == AcademicYear && s.PlusMinus == plusminus && s.FeeheadID == FeeHead && s.Dated == dated && s.DocNo == DoCNO && s.BankName == BankName && s.Branch == Branch && s.DDCheq == DdCheq && s.UserID == PUserID && s.Paymentby == paymentby).FirstOrDefault();
            if (check == null)
            {

                stdfee1 cta = new SchoolErp.stdfee1();
                cta.StudentID = studentid;
                cta.Amount = Amount;
                cta.Remarks = desc;
                cta.CourseID = ClassID;
                cta.sem = SectionID;
                cta.manualno = Manual;
                cta.AdmNo = RegNo;
                cta.Father = FatherName;
                cta.Student = FirstName;
                cta.InstituteID = SchoolId;
                cta.Sessionid = AcademicYear;
                cta.yearid = AcademicYear;
                cta.PlusMinus = plusminus;
                cta.FeeheadID = FeeHead;
                cta.Dated = dated;
                cta.DocNo = DoCNO;
                cta.BankName = BankName;
                cta.Branch = Branch;
                cta.DDCheq = DdCheq;
                cta.UserID = PUserID;
                cta.Paymentby = paymentby;
                db.stdfee1.Add(cta);

            }
            else
            {

            }


            db.SaveChanges();

        }





        public void saveFeeHeadConcession(List<string> val)
        {


            var id = Convert.ToInt32(val[1]);
            var Concession = Convert.ToInt32(val[2]);
            var cat_Id = Convert.ToInt32(val[3]);
            var Serial = Convert.ToInt32(val[4]);
            var SchoolID = Convert.ToInt32(val[5]);
            if (id == 0)
            {
                var check = db.tblDestination.Where(s => s.TariffID == Concession && s.FeeCategory == cat_Id && s.SerialNo == Serial && s.SchoolId == SchoolID).FirstOrDefault();
                if (check == null)
                {

                    tblDestination cta = new SchoolErp.tblDestination();
                    cta.TariffID = Concession;
                    cta.FeeCategory = cat_Id;
                    cta.SerialNo = Serial;
                    cta.SchoolId = SchoolID;
                    db.tblDestination.Add(cta);

                }
                else
                {

                }
            }
            else
            {
                var check = db.tblDestination.Where(s => s.TariffID == Concession && s.FeeCategory == cat_Id && s.SerialNo == Serial && s.ID != id && s.SchoolId == SchoolID).FirstOrDefault();
                if (check == null)
                {

                    var result = db.tblDestination.SingleOrDefault(b => b.ID == id);
                    result.TariffID = Concession;
                    result.FeeCategory = cat_Id;
                    result.SerialNo = Serial; ;
                    result.SchoolId = SchoolID;

                }
                else
                {
                }
            }
            db.SaveChanges();

        }






        [System.Web.Http.Route("api/FeeApi/getAllfeeDetails")]
        [System.Web.Http.HttpPost]
        public Student[] getAllfeeDetails(List<string> aa)
        {
            //string loginuser = aa[0];
            //int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            int rno = 0;
            DataTable dt = obj.getDataTable("select * from udf_getsfee1 ('100' )");

            foreach (DataRow dr in dt.Rows)
            {
                rno++;
                Student usr = new Student();
                usr.RollNo = rno.ToString();
                usr.ID = int.Parse(dr["ID"].ToString());
                usr.AcademicYear = dr["AcademicYear"].ToString();
                //  usr.RegNo = dr["RegNo"].ToString();
                //  usr.FatherName = dr["fathername"].ToString();
                //usr.dated = DateTime.Parse(dr["dated"].ToString());

                // usr.PUserID = dr["PUserID"].ToString();
                // usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                // usr.Section = dr["SectionName"].ToString();
                //  usr.Class = dr["Class"].ToString();
                //usr.ClassID = int.Parse(dr["ClassID"].ToString());
                usr.SectionID = int.Parse(dr["SectionID"].ToString());
                //  usr.SchoolID = dr["SchoolID"].ToString();
                usr.FeeCategory = dr["FeeCategory"].ToString();
                //  usr.SStatus = dr["StatusName"].ToString();
                //  usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                //  usr.School = dr["School"].ToString();




                //  usr.Status = dr["Status"].ToString();
                //if (dr["Status"].ToString() == "True")
                //{

                //    usr.SStatus = "Activate";
                //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                //}
                //else
                //{
                //    usr.SStatus = "DeActivate";
                //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                //}
                //  usr.School = dr["School"].ToString();

                list.Add(usr);
            }





            return list.ToArray();
        }











        public void saveFeeStructure(List<string> val)



        {
            var id = Convert.ToInt32(val[1]);
            var name = val[2].ToString().Trim();
            var year = val[3].ToString().Trim();
            var classes = val[4].ToString().Trim();
            var arr = classes.Split(',');
            var status = val[5].ToString().Trim() == "1" ? true : false;
            var SchoolID = Convert.ToInt32(val[6]);
            var FeeFineamount = val[7].ToString();
            if (id == 0)
            {
                tblFeeStructure cta = new SchoolErp.tblFeeStructure();
                cta.AcademicYear = Convert.ToInt32(year);
                cta.StructureName = name;
                cta.SchoolID = SchoolID;
                cta.DueFeeFineAmount = FeeFineamount;
                db.tblFeeStructures.Add(cta);
                db.SaveChanges();

                foreach (var ar in arr)
                {
                    var a = ar.Split('-');
                    int course = Convert.ToInt32(a[0]);
                    int section = Convert.ToInt32(a[1]);

                    tblFeeStructureClass c = new SchoolErp.tblFeeStructureClass();
                    c.ClassID = course;
                    c.SectionID = section;
                    c.FeeStructureID = cta.ID;
                    c.SchoolID = cta.SchoolID;

                    db.tblFeeStructureClasses.Add(c);
                }
            }
            else
            {
                var result = db.tblFeeStructures.SingleOrDefault(b => b.ID == id);
                result.AcademicYear = Convert.ToInt32(year);
                result.StructureName = name;
                result.Status = status;
                result.SchoolID = SchoolID;
                result.DueFeeFineAmount = FeeFineamount;

                var users = db.tblFeeStructureClasses.Where(u => u.FeeStructureID == id);

                foreach (var u in users)
                {
                    db.tblFeeStructureClasses.Remove(u);
                }

                foreach (var ar in arr)
                {
                    var a = ar.Split('-');
                    int course = Convert.ToInt32(a[0]);
                    int section = Convert.ToInt32(a[1]);

                    tblFeeStructureClass c = new SchoolErp.tblFeeStructureClass();
                    c.ClassID = course;
                    c.SectionID = section;
                    c.FeeStructureID = id;
                    c.SchoolID = SchoolID;
                    db.tblFeeStructureClasses.Add(c);
                }
            }
            db.SaveChanges();
        }

        public int saveFeeCalculate(List<string> val)
        {
            var id = Convert.ToInt32(val[1]);
            var value = val[2].ToString().Trim();
            //var payable =Convert.ToInt32(val[4].ToString().Trim());
            //if (id == 0)
            //{
            //    tblFeeCalculate_temp cta = new SchoolErp.tblFeeCalculate_temp();
            //    cta.monthId = mnth;
            //    cta.Months = mnth_txt;
            //    cta.monthlyAmount = amnt;
            //    db.tblFeeCalculate_temp.Add(cta);
            //    db.SaveChanges();
            //    return 1;
            //}
            //else
            //{
            var result = db.tblFeeCalculate_temp.SingleOrDefault(b => b.fldId == id);
            double amnt = Convert.ToDouble(result.monthlyAmount);
            double paid = Convert.ToDouble(value);
            double diff = amnt - paid;
            if (diff > 0)
            {
                result.duesAmount = diff.ToString();
            }
            result.PaidAmount = value;
            db.SaveChanges();
            //    return 2;
            //}
            return 0;
        }





        public int saveFeeStructureConfig(List<string> val)
        {
            var id = Convert.ToInt32(val[1]);
            long ct_id = Convert.ToInt32(val[2].ToString().Trim());
            var amnt = val[3].ToString().Trim();
            var ClassID = Convert.ToInt32(val[4].ToString().Trim());
            var SectionId = Convert.ToInt32(val[5].ToString().Trim());
            var year = Convert.ToInt32(val[6].ToString().Trim());
            var Cat = Convert.ToInt32(val[7].ToString().Trim());
            //var classes = val[6].ToString().Trim();
            //var arr = classes.Split(',');
            var SchoolID = Convert.ToInt32(val[8]);
            if (id == 0)
            {
                var chk = db.tblFeeStructureConfigs.Any(x => x.FeeCategory == ct_id && x.ClassID == ClassID && x.SectionID == SectionId && x.Academic_Year == year && x.SeatID == Cat && x.IsDeleted == null);
                if (!chk)
                {
                    tblFeeStructureConfig cta = new SchoolErp.tblFeeStructureConfig();
                    cta.FeeCategory = ct_id;
                    cta.Amount = amnt;
                    cta.SchoolID = SchoolID;
                    cta.ClassID = ClassID;
                    cta.SectionID = SectionId;
                    cta.Academic_Year = year;
                    cta.SeatID = Cat;
                    // cta.FeeStructureID = fsid;
                    db.tblFeeStructureConfigs.Add(cta);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                var result = db.tblFeeStructureConfigs.SingleOrDefault(b => b.ID == id);
                result.FeeCategory = ct_id;
                result.ClassID = ClassID;
                result.SectionID = SectionId;
                result.Amount = amnt;
                result.Academic_Year = year;
                result.SeatID = Cat;
                //  result.FeeStructureID = fsid;
                db.SaveChanges();
                return 2;
            }

        }


        [System.Web.Http.Route("api/FeesApi/DeleteRecord")]
        [System.Web.Http.HttpPost]
        public int DeleteRecord(List<string> val)
        {
            int id = Convert.ToInt32(val[0]);
            string type = val[1].ToString().Trim();
            try
            {
                //if (type == "Bank")
                //{
                //    var employer = new tblBankMaster { ID = id };
                //    db.Entry(employer).State = EntityState.Deleted;
                //}
                if (type == "FeeCategory")
                {
                    //var employer = new tblFeeCategory { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    var aa = db.tblFeeCategories.SingleOrDefault(s => s.ID == idd);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;
                    db.SaveChanges();

                }

                else if (type == "FeeConcession")
                {
                    //var employer = new tblFeeCategory { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    var aa = db.tblFeeHead.SingleOrDefault(s => s.ID == idd);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;
                    db.SaveChanges();

                }


                else if (type == "FeeStructure")
                {
                    //var employer = new tblFeeStructure { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;

                    int idd = Convert.ToInt32(id);
                    var aa = db.tblFeeStructures.SingleOrDefault(s => s.ID == idd);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;
                    db.SaveChanges();
                    var users = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == idd).ToList();
                    users.ForEach(a =>
                    {
                        a.IsDeleted = 1;
                        a.Deleted_on = DateTime.Now;

                    });
                    db.SaveChanges();


                    //var users = db.tblFeeStructureClasses.Where(u => u.FeeStructureID == id);

                    //foreach (var u in users)
                    //{


                    //    db.tblFeeStructureClasses.Remove(u);

                    //}


                }
                else if (type == "FeeStructureConfig")
                {
                    //var employer = new tblFeeStructureConfig { ID = id };
                    //db.Entry(employer).State = EntityState.Deleted;
                    int idd = Convert.ToInt32(id);
                    var aa = db.tblFeeStructureConfigs.SingleOrDefault(a => a.ID == idd);
                    aa.IsDeleted = 1;
                    aa.Deleted_on = DateTime.Now;
                    db.SaveChanges();
                }
                else if (type == "FeeCalculate")
                {

                    var users = db.tblFeeCalculate_temp.Where(u => u.monthId == id);

                    foreach (var u in users)
                    {
                        db.tblFeeCalculate_temp.Remove(u);
                    }
                }
                else if (type == "TruncateFeeTemp")
                {
                    db.Database.ExecuteSqlCommand("truncate table tblFeeCalculate_temp");
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
        [System.Web.Http.Route("api/FeesApi/ViewStructure")]
        [System.Web.Http.HttpPost]
        public List<StructureDetails> ViewStructure(List<string> aa)
        {
            List<StructureDetails> list = new List<StructureDetails>();
            var SchoolId = Convert.ToInt32(aa[2]);

            var cond = "";
            //var cond1 = "";

            if (aa[3] != null && Convert.ToInt32(aa[3]) > 0)
            {
                cond = cond + "and fs.ClassID ='" + aa[3] + "' ";
            }
            if (aa[4] != null && Convert.ToInt32(aa[4]) > 0)
            {
                cond = cond + "and fs.SectionID='" + aa[4] + "' ";
            }
            if (aa[5] != null && Convert.ToInt32(aa[5]) > 0)
            {
                cond = cond + "and fs.SeatID='" + aa[5] + "' ";
            }
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select fs.ID,fs.SeatID,fs.FeeCategory as FCID,fs.ClassID,fs.SectionID,fs.Academic_Year,DATEPART(YEAR,ay.DateFrom) DateFrom,DATEPART(year,ay.dateto) dateto,fs.Academic_Year,s.coursename, s1.SectionName, c.CategoryName, f.FeeCategory, fs.Amount from tblFeeStructureConfig fs  left outer join tblCourses s on s.Id = fs.ClassID
                                 left outer join tblSections s1 on s1.Id = fs.SectionID
                                     left outer join tblCastCategory c on c.CatId = fs.SeatID
                                     left outer join tblFeeCategory f on f.ID = fs.FeeCategory
                                     left join tblAcademicYear ay on ay.id=fs.Academic_Year
                                       where fs.IsDeleted is NULL and f.FeeCategory is Not NULL and fs.SchoolId='" + SchoolId + "' and  ay.CurrActive='1' "+cond+"");
                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                //left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                //left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId order by ep.Id desc");

                foreach (DataRow dr in dt.Rows)
                {
                    StructureDetails usr = new StructureDetails();
                    // PayeeDetails usr = new PayeeDetails();
                    usr.Coursename = dr["Coursename"].ToString();
                    usr.ID = dr["ID"].ToString();
                    usr.FCID = dr["FCID"].ToString();
                    usr.CID = dr["ClassID"].ToString();
                    usr.SID = dr["SectionID"].ToString();
                    usr.StID = dr["SeatID"].ToString();
                    usr.AID = dr["Academic_Year"].ToString();
                    usr.Academic_Year = dr["DateFrom"].ToString() + '-' + dr["dateto"].ToString();  // a.DateFrom.Year + "-" + a.DateTo.ToString("yy");//dr["Academic_Year"].ToString();
                    usr.SectionName = dr["SectionName"].ToString();
                    usr.categoryName = dr["categoryName"].ToString();
                    usr.FeeCategory = dr["FeeCategory"].ToString();
                    usr.Amount = dr["Amount"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/FeesApi/StudentConcessionList")]
        [System.Web.Http.HttpPost]
        public List<StudentConcessionList> StudentConcessionList(List<string> aa)
        {
            var SchoolId = Convert.ToInt32(aa[2].ToString());
            List<StudentConcessionList> list = new List<StudentConcessionList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select DATEPART(year,ac.DateFrom) DateFrom,DATEPART(year,ac.DateTo) DateTo,td.AcademicYearid, s.CourseName,s1.SectionName,st.FirstName,st.MiddleName,st.LastName,f.FeeCategory,td.amount from TariffDetail td
                          left outer join tblCourses s on s.Id=td.Classid
						         left outer join tblSections s1 on s1.Id=td.SectionID
									   left outer join tblFeeCategory f on f.ID=td.FeeHeadID
									     left outer join tblAcademicYear ac on ac.ID=td.AcademicYearid
										  left outer join TBLStudent st on st.ID=td.Studentid
									   where  f.FeeCategory is Not NULL and td.SchoolID='"+SchoolId+"'");
                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                //left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                //left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId order by ep.Id desc");

                foreach (DataRow dr in dt.Rows)
                {
                    StudentConcessionList usr = new StudentConcessionList();
                    // PayeeDetails usr = new PayeeDetails();
                    usr.Coursename = dr["Coursename"].ToString();
                    //  usr.Id = dr["Id"].ToString();
                    usr.Academic_Year = dr["DateFrom"].ToString() +'-'+ dr["DateTo"].ToString();
                    usr.SectionName = dr["SectionName"].ToString();
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    usr.FeeCategory = dr["FeeCategory"].ToString();
                    usr.Amount = dr["Amount"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/FeesApi/ConcessionHead")]
        [System.Web.Http.HttpPost]
        public List<ConcessionHead> ConcessionHead(List<string> aa)
        {
            List<ConcessionHead> list = new List<ConcessionHead>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select sc.School,dt.ID, f.FeeCategory,fh.TariffName,dt.SerialNo  from tblDestination dt
                                                left outer join tblFeeCategory f on f.ID=dt.FeeCategory
                                                left outer join tblFeeHead fh on fh.ID=dt.TariffID
                                                left outer join tblSchoolDetails sc on sc.ID=dt.SchoolId
                                                 where  f.FeeCategory is Not NULL");

                foreach (DataRow dr in dt.Rows)
                {
                    ConcessionHead usr = new ConcessionHead();
                   // usr.ID = dr["ID"].ToString();
                    usr.FeeCategory = dr["FeeCategory"].ToString();
                    usr.TariffName = dr["TariffName"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SerialNo = dr["SerialNo"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/FeesApi/getList")]
        [System.Web.Http.HttpPost]
        public fees[] getList(List<string> val)
        {
            // int a = 0;
            string type = val[0];

            List<fees> list = new List<Models.fees>();

            try
            {
                if (type == "FeeCategory")
                {
                    list = fillCategoryList(val);
                }
                else if (type == "FeeConcession")
                {
                    list = fillConcessionList(val);
                }
                else if (type == "FeeConcessionHead")
                {
                    list = fillConcessionHeadList(val);
                }

                else if (type == "FeeStructure")
                {
                    list = fillFeeStructureList(val);
                }
                else if (type == "FeeStructure1")
                {
                    list = fillFeeStructureList1(val);
                }

                else if (type == "FeeStructureConfig")
                {
                    list = fillFeeStructConfigList(val);
                }
                else if (type == "FeeCalculate")
                {
                    list = fillFeeCalculateList(val);
                }
                else if (type == "PrevFeeDetails")
                {
                    list = fillPrevFeeDetails(val);
                }
                else if (type == "PrevFeeDetailstudent")
                {
                    list = StudentfillPrevFeeDetails(val);
                }


            }

            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }

        public List<fees> fillConcessionList(List<string> val)
        {
            int count = 0;
            List<fees> list = new List<Models.fees>();
            string bank = val[1];
            string alias = val[2];
            string loginuser = val[5];
            int typeuser = Convert.ToInt32(val[6]);

            if (typeuser == 2)
            {

                var result = (from c in db.tblFeeHead
                              join s in db.tblSchoolDetails on
           c.SchoolID equals s.ID
                              where c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (bank != "")
                {
                    result = result.Where(c => c.model.TariffName == bank).ToList();
                    //   result = result.Where(c => c.model.TeacherID == teacher).ToList();
                }
                if (alias != "")
                {
                    result = result.Where(c => c.model.Per == alias).ToList();
                    //   predicate = predicate.and(x => x.cat_desc.toupper().contains(alias.toupper()));
                }

                //if (val[3] != "" && val[3] != "-1")
                //{
                //    result = result.Where(c => c.model.Refundable).ToList();
                //    //predicate = predicate.and(x => x.status);
                //}
                if (val[4] != "" && Convert.ToInt32(val[4]) != -1 && val[4] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[4]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    count++;
                    fees cf = new Models.fees();
                    cf.cf = new tblFeeHead();
                    cf.cf = m.model;
                    cf.count = count;
                    cf.School = m.SchoolName;

                    //if (ct.ct.Refundable)
                    //{
                    //    ct.StatusNm = "Refundable";
                    //    ct.style = "btn btn-block btn-success btn-sm";
                    //}
                    //else
                    //{
                    //    ct.StatusNm = "Inactivuuuue";
                    //    ct.style = "btn btn-block btn-danger btn-sm";

                    //}
                    cf.feeCategory = m.model.TariffName;//for fee category dropdown
                    cf.Cat_ID = m.model.ID;//for fee category dropdown

                    list.Add(cf);
                }
            }
            else
            {
                var result = (from c in db.tblFeeHead
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (bank != "")
                {
                    result = result.Where(c => c.model.TariffName == bank).ToList();
                    //   result = result.Where(c => c.model.TeacherID == teacher).ToList();
                }
                if (alias != "")
                {
                    result = result.Where(c => c.model.Per == alias).ToList();
                    //   predicate = predicate.and(x => x.cat_desc.toupper().contains(alias.toupper()));
                }

                //if (val[3] != "" && val[3] != "-1")
                //{
                //    result = result.Where(c => c.model.Refundable).ToList();
                //    //predicate = predicate.and(x => x.status);
                //}
                if (val[4] != "" && Convert.ToInt32(val[4]) != -1 && val[4] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[4]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    count++;
                    fees cf = new Models.fees();
                    cf.cf = new tblFeeHead();
                    cf.cf = m.model;
                    cf.count = count;
                    cf.School = m.SchoolName;

                    //if (ct.ct.Refundable)
                    //{
                    //    ct.StatusNm = "NonRefundable";
                    //    ct.style = "btn btn-block btn-success btn-sm";
                    //}
                    //else
                    //{
                    //    ct.StatusNm = "Refundable";
                    //    ct.style = "btn btn-block btn-danger btn-sm";

                    //}
                    cf.feeCategory = m.model.TariffName;//for fee category dropdown
                    cf.Cat_ID = m.model.ID;//for fee category dropdown

                    list.Add(cf);
                }
            }

            return list;


        }





        public List<fees> fillConcessionHeadList(List<string> val)
        {

            int count = 0;
            List<fees> list = new List<Models.fees>();
            //int TariffID = Convert.ToInt32(val[1]);
            //int FeeCategory = Convert.ToInt32(val[2]);
            string loginuser = val[5];
            int typeuser = Convert.ToInt32(val[6]);

            if (typeuser == 2)
            {

                var result = (from c in db.tblDestination
                              join s in db.tblSchoolDetails on
           c.SchoolId equals s.ID

                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();


                //if (val[3] != "" && val[3] != "-1")
                //{
                //    result = result.Where(c => c.model.Refundable).ToList();
                //    //predicate = predicate.and(x => x.status);
                //}
                if (val[4] != "" && Convert.ToInt32(val[4]) != -1 && val[4] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[4]);
                    result = result.Where(c => c.model.SchoolId == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    count++;
                    fees cd = new Models.fees();
                    cd.cd = new tblDestination();
                    cd.cd = m.model;
                    cd.count = count;
                    cd.School = m.SchoolName;

                    int TariffID = m.model.TariffID;
                    cd.Cat_ID = m.model.ID;//for fee category dropdown

                    list.Add(cd);
                }
            }
            else
            {
                var result = (from c in db.tblDestination
                              join s in db.tblSchoolDetails on c.SchoolId equals s.ID
                              join em in db.tblEmployees on c.SchoolId equals em.SchoolID
                              where em.UserID == loginuser
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();




                if (val[4] != "" && Convert.ToInt32(val[4]) != -1 && val[4] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[4]);
                    result = result.Where(c => c.model.SchoolId == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    count++;
                    fees cd = new Models.fees();
                    cd.cd = new tblDestination();
                    cd.cd = m.model;
                    cd.count = count;
                    cd.School = m.SchoolName;

                    //if (ct.ct.Refundable)
                    //{
                    //    ct.StatusNm = "NonRefundable";
                    //    ct.style = "btn btn-block btn-success btn-sm";
                    //}
                    //else
                    //{
                    //    ct.StatusNm = "Refundable";
                    //    ct.style = "btn btn-block btn-danger btn-sm";

                    //}
                    int TariffID = m.model.TariffID;
                    //cd.feeCategory = TariffID;//for fee category dropdown
                    cd.Cat_ID = m.model.ID;//for fee category dropdown

                    list.Add(cd);
                }
            }

            return list;


        }

















        public List<fees> fillCategoryList(List<string> val)
        {
            int count = 0;
            List<fees> list = new List<Models.fees>();
            string bank = val[1];
            string alias = val[2];
            string loginuser = val[5];
            int typeuser = Convert.ToInt32(val[6]);

            if (typeuser == 2)
            {

                var result = (from c in db.tblFeeCategories
                              join s in db.tblSchoolDetails on
           c.SchoolID equals s.ID
                              where c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (bank != "")
                {
                    result = result.Where(c => c.model.FeeCategory == bank).ToList();
                    //   result = result.Where(c => c.model.TeacherID == teacher).ToList();
                }
                if (alias != "")
                {
                    result = result.Where(c => c.model.Cat_Desc == bank).ToList();
                    //   predicate = predicate.and(x => x.cat_desc.toupper().contains(alias.toupper()));
                }

                //if (val[3] != "" && val[3] != "-1")
                //{
                //    result = result.Where(c => c.model.Refundable).ToList();
                //    //predicate = predicate.and(x => x.status);
                //}
                if (val[4] != "" && Convert.ToInt32(val[4]) != -1 && val[4] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[4]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    count++;
                    fees ct = new Models.fees();
                    ct.ct = new tblFeeCategory();
                    ct.ct = m.model;
                    ct.count = count;
                    ct.School = m.SchoolName;

                    //if (ct.ct.Refundable)
                    //{
                    //    ct.StatusNm = "Refundable";
                    //    ct.style = "btn btn-block btn-success btn-sm";
                    //}
                    //else
                    //{
                    //    ct.StatusNm = "Inactivuuuue";
                    //    ct.style = "btn btn-block btn-danger btn-sm";

                    //}
                    ct.feeCategory = m.model.FeeCategory;//for fee category dropdown
                    ct.Cat_ID = m.model.ID;//for fee category dropdown

                    list.Add(ct);
                }
            }
            else
            {
                var result = (from c in db.tblFeeCategories
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (bank != "")
                {
                    result = result.Where(c => c.model.FeeCategory == bank).ToList();
                    //   result = result.Where(c => c.model.TeacherID == teacher).ToList();
                }
                if (alias != "")
                {
                    result = result.Where(c => c.model.Cat_Desc == bank).ToList();
                    //   predicate = predicate.and(x => x.cat_desc.toupper().contains(alias.toupper()));
                }

                //if (val[3] != "" && val[3] != "-1")
                //{
                //    result = result.Where(c => c.model.Refundable).ToList();
                //    //predicate = predicate.and(x => x.status);
                //}
                if (val[4] != "" && Convert.ToInt32(val[4]) != -1 && val[4] != "0")
                {
                    long SchoolID = Convert.ToInt32(val[4]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    count++;
                    fees ct = new Models.fees();
                    ct.ct = new tblFeeCategory();
                    ct.ct = m.model;
                    ct.count = count;
                    ct.School = m.SchoolName;

                    //if (ct.ct.Refundable)
                    //{
                    //    ct.StatusNm = "NonRefundable";
                    //    ct.style = "btn btn-block btn-success btn-sm";
                    //}
                    //else
                    //{
                    //    ct.StatusNm = "Refundable";
                    //    ct.style = "btn btn-block btn-danger btn-sm";

                    //}
                    ct.feeCategory = m.model.FeeCategory;//for fee category dropdown
                    ct.Cat_ID = m.model.ID;//for fee category dropdown

                    list.Add(ct);
                }
            }

            return list;

            //int count = 0;
            //List<fees> list = new List<Models.fees>();
            //string bank = val[1];
            //string alias = val[2];
            ////int SchoolID = Convert .ToInt32(val[4]);
            //var predicate = PredicateBuilder.True<tblFeeCategory>();
            //if (bank != "")
            //{
            //    predicate = predicate.And(x => x.FeeCategory.ToUpper().Contains(bank.ToUpper()));
            //}
            //if (alias != "")
            //{
            //    predicate = predicate.And(x => x.Cat_Desc.ToUpper().Contains(alias.ToUpper()));
            //}

            //if (val[3] != "" && val[3] != "-1")
            //{
            //    predicate = predicate.And(x => x.Status);
            //}

            //var result = db.tblFeeCategories.AsExpandable().Where(predicate).ToList();

            //foreach (var m in result)
            //{
            //    count++;
            //    fees ct = new Models.fees();
            //    ct.ct = new tblFeeCategory();
            //    ct.ct = m;
            //    ct.count = count;

            //    if (ct.ct.Status)
            //    {
            //        ct.StatusNm = "Active";
            //        ct.style = "btn btn-block btn-success btn-sm";
            //    }
            //    else
            //    {
            //        ct.StatusNm = "Inactive";
            //        ct.style = "btn btn-block btn-danger btn-sm";

            //    }
            //    ct.feeCategory = m.FeeCategory;//for fee category dropdown
            //    ct.Cat_ID = m.ID;//for fee category dropdown

            //    list.Add(ct);
            //}
            //return list;
        }


        [System.Web.Http.Route("api/FeesApi/checkFeedueDate")]
        [System.Web.Http.HttpPost]
        public DateTime checkFeedueDate(List<string> val)
        {
            DateTime duefeedate11 = DateTime.Now;
            if (val[1] != "")
            {
                int SchoolID = Convert.ToInt32(val[0]);
                int academicyear = Convert.ToInt32(val[2]);
                int month = Convert.ToInt32(val[1]) + 1;
                var check = db.tblAcademicYears.Where(x => x.ID == academicyear && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
                DateTime a = check.DateFrom;
                DateTime b = check.DateTo.AddMonths(-1);
                int startmonth = a.Month;
                int endmonth = b.Month;
                int startYear = a.Year;
                int endYear = b.Year;
                {
                    var FEEDate = db.tblSchoolDetails.Where(x => x.ID == SchoolID && x.IsDeleted == null).FirstOrDefault();
                    var feeday = Convert.ToInt32(FEEDate.FeeDueDay);
                    duefeedate11 = new DateTime(startYear, month, feeday);
                }

                if (startYear < endYear)
                {


                    for (DateTime dt = a; dt < b; dt = dt.AddMonths(1))
                    {
                        int checkmonth = dt.Month;
                        if (checkmonth == month)
                        {
                            int checkyear = dt.Year;
                            var FEEDate = db.tblSchoolDetails.Where(x => x.ID == SchoolID && x.IsDeleted == null).FirstOrDefault();
                            var feeday = Convert.ToInt32(FEEDate.FeeDueDay);
                            duefeedate11 = new DateTime(checkyear, month, feeday);

                        }
                    }


                }


            }


            return duefeedate11;

        }


        [System.Web.Http.Route("api/FeesApi/getDuefine")]
        [System.Web.Http.HttpPost]
        public int getDuefine(List<string> val)
        {
            var fineamount = 0;
            if (val[0] != null && val[0] != "" && val[0] != "0")
            {
                int feestructure = Convert.ToInt32(val[0]);
                var fine = db.tblFeeStructures.Where(x => x.ID == feestructure && x.IsDeleted == null).FirstOrDefault();
                fineamount = Convert.ToInt32(fine.DueFeeFineAmount);
            }

            return fineamount;

        }


        [System.Web.Http.Route("api/FeesApi/getFeeStructureIDByStdID")]
        [System.Web.Http.HttpPost]
        public int getFeeStructureIDByStdID(List<string> val)
        {
            int stdID = Convert.ToInt32(val[0]);
            int year = 0;
            if (val[1] != "" && val[1] != "-1")
            {
                year = Convert.ToInt32(val[1]);
            }

            int fsID = 0;
            try
            {
                //var fsIDs = from s in db.TBLStudents
                //            from sc in db.tblFeeStructureClasses
                //            from fs in db.tblFeeStructures
                //            where s.ClassID == sc.ClassID &&
                //            s.SectionID == sc.SectionID && fs.Status == true && fs.AcademicYear == year
                //            && s.ID == stdID && fs.ID == sc.FeeStructureID
                //            select sc.FeeStructureID;
                //foreach (var f in fsIDs)
                //{
                //    fsID = f;
                //}

                var fsIDs = from s in db.tblFeeStructureAssigns
                            join fs in db.tblFeeStructures on s.FeeStructureID equals fs.ID
                            where s.StudentID == stdID && fs.Status == true && fs.AcademicYear == year && s.isActive && !s.isDeleted
                            select s.FeeStructureID;

                fsID = Convert.ToInt32(fsIDs.FirstOrDefault());

                return fsID;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }

        }


        public List<fees> fillFeeStructConfigList(List<string> val)
        {
            int count = 0;
            List<fees> list = new List<Models.fees>();
            int SchoolID = Convert.ToInt32(val[7]);


            var result = (from c in db.tblFeeStructureConfigs
                          join fs in db.tblFeeCategories on c.FeeCategory equals fs.ID
                          where c.IsDeleted == null && c.SchoolID==SchoolID
                          select new
                          {
                              c,
                              fs
                          }).ToList();

            foreach (var m in result)
            {
                count++;
                fees ct = new Models.fees();
                ct.fsc = new SchoolErp.tblFeeStructureConfig();
                ct.ct = new tblFeeCategory();
                ct.fsc = m.c;
                ct.ct = m.fs;
                ct.count = count;

                list.Add(ct);
            }
            return list;
        }

        //[System.Web.Http.Route("api/FeesApi/fillFeeCalculateListapp")]
        //[System.Web.Http.HttpPost]

        //public feesAPPP fillFeeCalculateListapp(List<string> val)
        //{
        //    int count = 0;
        //    feesAPPP obj = new feesAPPP();
        //    List<feesss> list = new List<Models.feesss>();
        //    //int fsID = Convert.ToInt32(val[1]);
        //    int avi = 0;
        //    int SchoolID = Convert.ToInt32(val[1]);


        //    var result = db.tblFeeCalculate_temp.Where(x => x.SchoolID == SchoolID).OrderBy(x => x.monthId).ToList();
        //    try
        //    {
        //        foreach (var m in result)
        //        {
        //            avi++;

        //            count++;
        //            feesss ct = new Models.feesss();
        //            ct.cal = new SchoolErp.tblFeeCalculate_temp();
        //            if (string.IsNullOrEmpty(m.duesAmount))
        //            {
        //                m.duesAmount = "0";
        //            }
        //            if (string.IsNullOrEmpty(m.PaidAmount))
        //            {
        //                m.PaidAmount = "0";
        //            }
        //            ct.cal = m;
        //            //  ct.count = count;

        //            list.Add(ct);
        //        }
        //        if (avi != 0)
        //        {
        //            obj.status = "200";
        //            obj.message = "Sucess";
        //            obj.data = list;
        //        }
        //        else if (avi == 0)
        //        {
        //            obj.status = "200";
        //            obj.message = "No data found";
        //            obj.data = list;
        //        }
        //    }
        //    catch
        //    {
        //        obj.status = "404";
        //        obj.message = "Something went wrong";
        //    }

        //    return obj;
        //}


        public List<fees> fillFeeCalculateList(List<string> val)
        {
            int count = 0;
            List<fees> list = new List<Models.fees>();
            //int fsID = Convert.ToInt32(val[1]);
            int SchoolID = Convert.ToInt32(val[1]);
            var result = db.tblFeeCalculate_temp.Where(x => x.SchoolID == SchoolID).OrderBy(x => x.monthId).ToList();

            foreach (var m in result)
            {
                count++;
                fees ct = new Models.fees();
                ct.cal = new SchoolErp.tblFeeCalculate_temp();
                if (string.IsNullOrEmpty(m.duesAmount))
                {
                    m.duesAmount = "0";
                }
                if (string.IsNullOrEmpty(m.PaidAmount))
                {
                    m.PaidAmount = "0";
                }
                ct.cal = m;
                ct.count = count;

                list.Add(ct);
            }
            return list;
        }
        //[System.Web.Http.Route("api/FeesApi/StudentfillPrevFeeDetailsApp")]
        //[System.Web.Http.HttpPost]
        //public feesAPP StudentfillPrevFeeDetailsApp(feess val)
        //{
        //    int count = 0;
        //    feesAPP obj = new feesAPP();
        //    List<feess> list = new List<Models.feess>();

        //    try
        //    {
        //        if (val.StudentID.Equals(null) || "".Equals(val.StudentID) || val.StudentID == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Student ID";
        //            obj.data = list;
        //        }
        //        else if (val.SchoolID.Equals(null) || "".Equals(val.SchoolID) || val.SchoolID == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";
        //            obj.data = list;
        //        }
        //        else if (val.AcademicYear.Equals(null) || "".Equals(val.AcademicYear) || val.AcademicYear == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter AcademicYear";
        //            obj.data = list;
        //        }
        //        else
        //        {
        //            feess ct = new Models.feess();
        //            long id = Convert.ToInt32(val.StudentID);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int AcademicYear1 = Convert.ToInt32(val.AcademicYear);
        //            int monthsApart = 0;
        //            var month = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
        //           if (month!=null)
        //            {
        //                DateTime startDate = month.DateFrom;
        //                DateTime endDate = month.DateTo;
        //                monthsApart = ( endDate.Month + endDate.Year * 12 )- (startDate.Month + startDate.Year * 12)+1;

        //                var result1 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1).FirstOrDefault();
        //                if (result1 != null)
        //                {
        //                    int feestructureid = Convert.ToInt32(result1.feeStructureID);
        //                    var totalamountFee = db.tblFeeStructureConfigs.Where(a => a.FeeStructureID == feestructureid && a.IsDeleted == null).ToList();
        //                    int totalfee1 = 0;
        //                    foreach ( var t in totalamountFee)
        //                    {
        //                        int amount1 = Convert.ToInt32(t.Amount);
        //                        totalfee1 = totalfee1 + amount1;
        //                    }
        //                   ct.Amount  = totalfee1 * 12; //Total fee ac to avademicyear


        //                }

        //            }






        //            var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID).OrderBy(x => x.monthId).ToList();
        //            if (val.AcademicYear != null && val.AcademicYear != -1)
        //            {
        //                int AcademicYear = Convert.ToInt32(val.AcademicYear);
        //                result = result.Where(y => y.AcademicYear == AcademicYear).OrderBy(x => x.monthId).ToList();
        //            }
        //            int totalfee = 0;
        //            int paidamount = 0;
        //            int TotalDiscount = 0;


        //            foreach (var avi in result)
        //            {
        //                int fee = Convert.ToInt32( avi.monthlyAmount);
        //                totalfee = totalfee + fee;  //To
        //                int PaidFee = Convert.ToInt32(avi.PaidAmount); 
        //                paidamount = paidamount + PaidFee; //(Total)paid
        //                int Discountcal = Convert.ToInt32(avi.discountAmnt);
        //                TotalDiscount = TotalDiscount + Discountcal;  //(Total)Discount
        //                int feestructureid = Convert.ToInt32( avi.feeStructureID);
        //            }

        //            // var result = (from F in db.tblFeeCalculates join  AC in db.tblAcademicYears 
        //            //  var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID==SchoolID &&).OrderBy(x => x.monthId).ToList();

        //            foreach (var m in result)
        //            {
        //                count++;
        //               // feess ct = new Models.feess();
        //                ct.FeeDetails = new SchoolErp.tblFeeCalculate();
        //                if (string.IsNullOrEmpty(m.duesAmount))
        //                {
        //                    m.duesAmount = "0";

        //                }

        //                if (string.IsNullOrEmpty(m.PaidAmount))
        //                {
        //                    m.PaidAmount = "0";
        //                }
        //                ct.FeeDetails = m;

        //                ct.StudentID = val.StudentID;
        //                ct.AcademicYear = val.AcademicYear;
        //                ct.SchoolID = val.SchoolID;
        //                list.Add(ct);
        //            }
        //            if (count != 0)
        //            {
        //                obj.status = true;
        //                obj.message = "sucess";
        //                obj.data = list;
        //            }
        //            if (count == 0)
        //            {
        //                obj.status = false;
        //                obj.message = "No Details Found";
        //                obj.data = list;
        //            }
        //        }


        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //        obj.data = list;
        //    }
        //    return obj;
        //}



        //[System.Web.Http.Route("api/FeesApi/StudentfillPrevFeeDetailsApp1")]
        //[System.Web.Http.HttpPost]
        //public feesAPP StudentfillPrevFeeDetailsApp1(feess val)
        //{
        //    int count = 0;
        //    feesAPP obj = new feesAPP();
        //     //feess list = new List<Models.feess>();
        //    feess ct = new Models.feess();
        //    ct.FeeDetails = new List<newfee>(); 

        //    try
        //    {
        //        if (val.StudentID.Equals(null) || "".Equals(val.StudentID) || val.StudentID == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Student ID";

        //        }
        //        else if (val.SchoolID.Equals(null) || "".Equals(val.SchoolID) || val.SchoolID == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else if (val.AcademicYear.Equals(null) || "".Equals(val.AcademicYear) || val.AcademicYear == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter AcademicYear";

        //        }
        //        else
        //        {

        //            long id = Convert.ToInt32(val.StudentID);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int AcademicYear1 = Convert.ToInt32(val.AcademicYear);
        //            int monthsApart = 0;
        //            var month1 = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
        //            if (month1 != null)
        //            {
        //                DateTime startDate1 = month1.DateFrom;
        //                DateTime endDate1 = month1.DateTo;

        //                monthsApart = (endDate1.Month + endDate1.Year * 12) - (startDate1.Month + startDate1.Year * 12) ;

        //                sqlHelper obj1 = new sqlHelper();
        //                string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                var totalamountFee = db.tblFeeStructureConfigs.Where(a => a.FeeStructureID == FEESTRUCTUREIDused && a.IsDeleted == null).ToList();
        //                    int totalfee1 = 0;
        //                    foreach (var ta in totalamountFee)
        //                    {
        //                        int amount1 = Convert.ToInt32(ta.Amount);
        //                        totalfee1 = totalfee1 + amount1;
        //                    }
        //                ct.TotalAmount= totalfee1 *monthsApart; //Total fee ac to avademicyear

        //                var result3 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1).ToList();
        //                int totalpaid = 0;
        //                int TotalDiscount = 0;
        //                foreach (var tp in result3)
        //                {
        //                    int amount2 = Convert.ToInt32(tp.PaidAmount);
        //                    totalpaid = totalpaid + amount2;
        //                    int amount3 = Convert.ToInt32(tp.discountAmnt) ;
        //                    TotalDiscount = TotalDiscount + amount3;

        //                }

        //                ct.TotalPaid = totalpaid; //TotalPaid
        //                ct.TotalDiscount = TotalDiscount; //TotalDiscount
        //                ct.TotalDueBalence = ct.TotalAmount -( ct.TotalPaid+ ct.TotalDiscount); //TotalDueBalence
        //                ct.StudentID = val.StudentID;
        //                ct.SchoolID = val.SchoolID;
        //                ct.AcademicYear = val.AcademicYear;
        //            }







        //            var month = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
        //            if (month != null)
        //            {
        //                DateTime startDate = month.DateFrom;
        //                DateTime endDate = month.DateTo;
        //                int startmonth = startDate.Month;
        //                int Endmonth = endDate.Month;
        //                int startyear = startDate.Year;
        //                int EndYear = endDate.Year;
        //                if (startyear == EndYear)
        //                {
        //                    int j = 0;
        //                    for (j = startmonth - 1; j < Endmonth-1; j++)
        //                    {

        //                        startmonth++;

        //                        var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == j).FirstOrDefault();



        //                        if (result2 != null)
        //                        {
        //                            count++;
        //                            //ct.FeeDetails = new List<newfee>(); 
        //                            newfee A = new newfee();
        //                            A.Amount = result2.monthlyAmount;
        //                            int Paidamount = 0;
        //                            int Dueamount = 0;
        //                            var pa = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();

        //                            foreach (var f in pa)
        //                            {
        //                                int paida = Convert.ToInt32(f.PaidAmount);
        //                                Paidamount = Paidamount + paida;
        //                            }

        //                            A.PaidAmount = Paidamount.ToString();

        //                            var da1 = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();
        //                            int Discount = 0;
        //                            foreach (var d1 in da1)
        //                            {
        //                                int duea = Convert.ToInt32(d1.duesAmount);
        //                                Dueamount = Dueamount + (duea * -1);
        //                                if (duea == 0)
        //                                {
        //                                    Dueamount = 0;
        //                                }
        //                                int amount4 = Convert.ToInt32(d1.discountAmnt);
        //                                Discount = Discount + amount4;
        //                            }
        //                            A.discountAmount = Discount.ToString();
        //                            A.duesAmount = Dueamount.ToString();
        //                            A.Month = result2.Months;
        //                            ct.FeeDetails.Add(A);

        //                        }
        //                        else
        //                        {
        //                            count++;
        //                            newfee A = new newfee();
        //                            sqlHelper obj1 = new sqlHelper();
        //                            string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                            int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                            int am = 0;
        //                            var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
        //                            if (checkfeestructure != null)
        //                            {
        //                                var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
        //                                foreach (var m in totalamountFee)
        //                                {
        //                                    count++;
        //                                    int amu = Convert.ToInt32(m.Amount);
        //                                    am = am + amu;
        //                                }
        //                                A.Amount = am.ToString();
        //                                A.PaidAmount = "0";
        //                                A.duesAmount = am.ToString();
        //                                A.discountAmount = "0";
        //                                A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(j + 1);
        //                                ct.FeeDetails.Add(A);
        //                            }


        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    int i = 0;
        //                    for (i = startmonth - 1; i <= 11; i++)
        //                    {
        //                        startmonth++;

        //                        var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).FirstOrDefault();



        //                        if (result2 != null)
        //                        {
        //                            count++;
        //                            //ct.FeeDetails = new List<newfee>(); 
        //                            newfee A = new newfee();
        //                            A.Amount = result2.monthlyAmount;
        //                            int Paidamount = 0;
        //                            int Dueamount = 0;
        //                            var pa = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();

        //                            foreach (var f in pa)
        //                            {
        //                                int paida = Convert.ToInt32(f.PaidAmount);
        //                                Paidamount = Paidamount + paida;
        //                            }

        //                            A.PaidAmount = Paidamount.ToString();

        //                            var da1 = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();
        //                            int Discount = 0;
        //                            foreach (var d1 in da1)
        //                            {
        //                                int duea = Convert.ToInt32(d1.duesAmount);
        //                                Dueamount = Dueamount + (duea * -1);
        //                                if (duea == 0)
        //                                {
        //                                    Dueamount = 0;
        //                                }
        //                                int amount4 = Convert.ToInt32(d1.discountAmnt);
        //                                Discount = Discount + amount4;
        //                            }
        //                            A.discountAmount = Discount.ToString();
        //                            A.duesAmount = Dueamount.ToString();
        //                            A.Month = result2.Months;
        //                            ct.FeeDetails.Add(A);

        //                        }
        //                        else
        //                        {
        //                            count++;
        //                            newfee A = new newfee();
        //                            sqlHelper obj1 = new sqlHelper();
        //                            string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                            int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                            int am = 0;
        //                            var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
        //                            if (checkfeestructure != null)
        //                            {
        //                                var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
        //                                foreach (var m in totalamountFee)
        //                                {
        //                                    count++;
        //                                    int amu = Convert.ToInt32(m.Amount);
        //                                    am = am + amu;
        //                                }
        //                                A.Amount = am.ToString();
        //                                A.PaidAmount = "0";
        //                                A.duesAmount = am.ToString();
        //                                A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
        //                                A.discountAmount = "0";


        //                                ct.FeeDetails.Add(A);



        //                            }


        //                        }


        //                    }
        //                    for (i = 0; i <= Endmonth - 2; i++)
        //                    {

        //                        //Endmonth++;

        //                        var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).FirstOrDefault();



        //                        if (result2 != null)
        //                        {
        //                            count++;
        //                            //ct.FeeDetails = new List<newfee>(); 
        //                            newfee A = new newfee();
        //                            A.Amount = result2.monthlyAmount;
        //                            int Paidamount = 0;
        //                            int Dueamount = 0;
        //                            var pa = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();

        //                            foreach (var f in pa)
        //                            {
        //                                int paida = Convert.ToInt32(f.PaidAmount);
        //                                Paidamount = Paidamount + paida;
        //                            }

        //                            A.PaidAmount = Paidamount.ToString();

        //                            var da1 = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();
        //                            int Discount = 0;
        //                            foreach (var d1 in da1)
        //                            {
        //                                int duea = Convert.ToInt32(d1.duesAmount);
        //                                Dueamount = Dueamount + (duea * -1);
        //                                if (duea == 0)
        //                                {
        //                                    Dueamount = 0;
        //                                }
        //                                int amount4 = Convert.ToInt32(d1.discountAmnt);
        //                                Discount = Discount + amount4;
        //                            }
        //                            A.discountAmount = Discount.ToString();
        //                            A.duesAmount = Dueamount.ToString();
        //                            A.Month = result2.Months;
        //                            ct.FeeDetails.Add(A);

        //                        }
        //                        else
        //                        {
        //                            count++;
        //                            newfee A = new newfee();
        //                            sqlHelper obj1 = new sqlHelper();
        //                            string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                            int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                            int am = 0;
        //                            var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
        //                            if (checkfeestructure != null)
        //                            {
        //                                var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
        //                                foreach (var m in totalamountFee)
        //                                {
        //                                    count++;
        //                                    int amu = Convert.ToInt32(m.Amount);
        //                                    am = am + amu;
        //                                }
        //                                A.Amount = am.ToString();
        //                                A.PaidAmount = "0";
        //                                A.duesAmount = am.ToString();
        //                                A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i+1);
        //                                A.discountAmount = "0";
        //                                ct.FeeDetails.Add(A);
        //                            }


        //                        }


        //                        //monthsApart = (endDate.Month + endDate.Year * 12) - (startDate.Month + startDate.Year * 12) + 1;

        //                        //var result1 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1).FirstOrDefault();
        //                        //if (result1 != null)
        //                        //{
        //                        //    int feestructureid = Convert.ToInt32(result1.feeStructureID);
        //                        //    var totalamountFee = db.tblFeeStructureConfigs.Where(a => a.FeeStructureID == feestructureid && a.IsDeleted == null).ToList();
        //                        //    int totalfee1 = 0;
        //                        //    foreach (var t in totalamountFee)
        //                        //    {
        //                        //        int amount1 = Convert.ToInt32(t.Amount);
        //                        //        totalfee1 = totalfee1 + amount1;
        //                        //    }
        //                        //    ct.Amount = totalfee1 * 12; //Total fee ac to avademicyear


        //                        //}



        //                    }
        //                }



        //                if (count != 0)
        //                {
        //                    obj.status = true;
        //                    obj.message = "sucess";
        //                    obj.data = ct;
        //                }
        //                if (count == 0)
        //                {
        //                    obj.status = false;
        //                    obj.message = "No Details Found";
        //                    obj.data = ct;
        //                }
        //            }
        //        }


        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //        obj.data = ct;
        //    }
        //    return obj;

        //}

        //[System.Web.Http.Route("api/FeesApi/StudentfillPrevFeeDetailsApp1")] //before new
        //[System.Web.Http.HttpPost]
        //public feesAPP StudentfillPrevFeeDetailsApp1(feess val)
        //{
        //    int count = 0;
        //    feesAPP obj = new feesAPP();
        //    //feess list = new List<Models.feess>();
        //    feess ct = new Models.feess();
        //    ct.FeeDetails = new List<newfee>();

        //    try
        //    {
        //        if (val.StudentID.Equals(null) || "".Equals(val.StudentID) || val.StudentID == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Student ID";

        //        }
        //        else if (val.SchoolID.Equals(null) || "".Equals(val.SchoolID) || val.SchoolID == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else if (val.AcademicYear.Equals(null) || "".Equals(val.AcademicYear) || val.AcademicYear == 0)
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter AcademicYear";

        //        }
        //        else
        //        {

        //            long id = Convert.ToInt32(val.StudentID);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int AcademicYear1 = Convert.ToInt32(val.AcademicYear);
        //            int monthsApart = 0;
        //            var month1 = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
        //            if (month1 != null)
        //            {
        //                DateTime startDate1 = month1.DateFrom;
        //                DateTime endDate1 = month1.DateTo;

        //                monthsApart = (endDate1.Month + endDate1.Year * 12) - (startDate1.Month + startDate1.Year * 12);

        //                sqlHelper obj1 = new sqlHelper();
        //                string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                var totalamountFee = db.tblFeeStructureConfigs.Where(a => a.FeeStructureID == FEESTRUCTUREIDused && a.IsDeleted == null).ToList();
        //                int totalfee1 = 0;
        //                foreach (var ta in totalamountFee)
        //                {
        //                    int amount1 = Convert.ToInt32(ta.Amount);
        //                    totalfee1 = totalfee1 + amount1;
        //                }
        //                ct.TotalAmount = totalfee1 * monthsApart; //Total fee ac to avademicyear

        //                var result3 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1).ToList();
        //                int totalpaid = 0;
        //                int TotalDiscount = 0;
        //                foreach (var tp in result3)
        //                {
        //                    int amount2 = Convert.ToInt32(tp.PaidAmount);
        //                    totalpaid = totalpaid + amount2;
        //                    int amount3 = Convert.ToInt32(tp.discountAmnt);
        //                    TotalDiscount = TotalDiscount + amount3;

        //                }

        //                ct.TotalPaid = totalpaid; //TotalPaid
        //                ct.TotalDiscount = TotalDiscount; //TotalDiscount
        //                ct.TotalDueBalence = ct.TotalAmount - (ct.TotalPaid + ct.TotalDiscount); //TotalDueBalence
        //                ct.StudentID = val.StudentID;
        //                ct.SchoolID = val.SchoolID;
        //                ct.AcademicYear = val.AcademicYear;
        //            }







        //            var month = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
        //            if (month != null)
        //            {
        //                DateTime startDate = month.DateFrom;
        //                DateTime endDate = month.DateTo;
        //                int startmonth = startDate.Month;
        //                int Endmonth = endDate.Month;
        //                int startyear = startDate.Year;
        //                int EndYear = endDate.Year;
        //                if (startyear == EndYear)
        //                {
        //                    int j = 0;
        //                    for (j = startmonth - 1; j < Endmonth - 1; j++)
        //                    {

        //                        startmonth++;

        //                        //var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == j).FirstOrDefault();

        //                        var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == j).ToList();

        //                        if (result2.Count != 0)
        //                        {
        //                            int paidamountfinal = 0;
        //                            foreach (var a in result2)
        //                            {
        //                                newfee A = new newfee();
        //                                A.Amount = a.monthlyAmount;
        //                                //  A.PaidAmount = a.PaidAmount;
        //                                int thistimepayamount = Convert.ToInt32(a.PaidAmount);
        //                                paidamountfinal = paidamountfinal + thistimepayamount;
        //                                A.PaidAmount = paidamountfinal.ToString();
        //                                int due = Convert.ToInt32(a.duesAmount);
        //                                int duee = due * -1;
        //                                A.duesAmount = duee.ToString();
        //                                A.Month = a.Months;
        //                                //int Tamount = Convert.ToInt32(A.Amount);
        //                                //int paidamount = Convert.ToInt32(A.PaidAmount);
        //                                //int discount = Convert.ToInt32(a.discountAmnt);
        //                                //int realpaid = paidamount + discount;
        //                                A.discountAmount = a.discountAmnt.ToString();
        //                                if (duee == 0)
        //                                {
        //                                    A.Feestatus = "Paid";
        //                                }
        //                                else if (duee != 0)
        //                                {
        //                                    A.Feestatus = "Partial";
        //                                }
        //                                //if (Tamount == realpaid)
        //                                //{
        //                                //    A.Feestatus = "Paid";
        //                                //}
        //                                //else if (Tamount != realpaid)
        //                                //{
        //                                //    A.Feestatus = "Partial";
        //                                //}

        //                                ct.FeeDetails.Add(A);
        //                            }
        //                            count++;

        //                            //ct.FeeDetails = new List<newfee>(); 
        //                            //newfee A = new newfee();
        //                            ////A.Amount = result2.monthlyAmount;
        //                            //A.Amount = a.monthlyAmount;
        //                            //int Paidamount = 0;
        //                            //int Dueamount = 0;
        //                            //var pa = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();

        //                            //foreach (var f in pa)
        //                            //{
        //                            //    int paida = Convert.ToInt32(f.PaidAmount);
        //                            //    Paidamount = Paidamount + paida;
        //                            //}

        //                            //A.PaidAmount = Paidamount.ToString();

        //                            //var da1 = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();
        //                            //int Discount = 0;
        //                            //foreach (var d1 in da1)
        //                            //{
        //                            //    int duea = Convert.ToInt32(d1.duesAmount);
        //                            //    Dueamount = /*Dueamount + */(duea * -1);
        //                            //    if (duea == 0)
        //                            //    {
        //                            //        Dueamount = 0;
        //                            //    }
        //                            //    int amount4 = Convert.ToInt32(d1.discountAmnt);
        //                            //    Discount = Discount + amount4;
        //                            //}
        //                            //A.discountAmount = Discount.ToString();
        //                            //A.duesAmount = Dueamount.ToString();
        //                            //A.Month = result2.Months;
        //                            //ct.FeeDetails.Add(A);

        //                        }
        //                        else
        //                        {
        //                            count++;
        //                            newfee A = new newfee();
        //                            sqlHelper obj1 = new sqlHelper();
        //                            string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                            int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                            int am = 0;
        //                            var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
        //                            if (checkfeestructure != null)
        //                            {
        //                                var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
        //                                foreach (var m in totalamountFee)
        //                                {
        //                                    count++;
        //                                    int amu = Convert.ToInt32(m.Amount);
        //                                    am = am + amu;
        //                                }
        //                                A.Amount = am.ToString();
        //                                A.PaidAmount = "0";
        //                                A.duesAmount = am.ToString();
        //                                A.discountAmount = "0";
        //                                A.Feestatus = "UnPaid";
        //                                A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(j + 1);
        //                                ct.FeeDetails.Add(A);
        //                            }


        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    int i = 0;
        //                    for (i = startmonth - 1; i <= 11; i++)
        //                    {
        //                        startmonth++;

        //                        //var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).FirstOrDefault();
        //                        var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).ToList();


        //                        if (result2.Count != 0)
        //                        {
        //                            count++;
        //                            //ct.FeeDetails = new List<newfee>(); 
        //                            int paidamountfinal = 0;
        //                            foreach (var a in result2)
        //                            {
        //                                newfee A = new newfee();

        //                                A.Amount = a.monthlyAmount;
        //                                //A.PaidAmount = a.PaidAmount;
        //                                int thistimepayamount = Convert.ToInt32(a.PaidAmount);
        //                                paidamountfinal = paidamountfinal + thistimepayamount;
        //                                A.PaidAmount = paidamountfinal.ToString();
        //                                int due = Convert.ToInt32(a.duesAmount);
        //                                int duee = due * -1;
        //                                A.duesAmount = duee.ToString();
        //                                A.Month = a.Months;
        //                                //int Tamount = Convert.ToInt32(A.Amount);
        //                                //int paidamount = Convert.ToInt32(A.PaidAmount);
        //                                //int discount = Convert.ToInt32(a.discountAmnt);
        //                                //int realpaid = paidamount + discount;
        //                                A.discountAmount = a.discountAmnt.ToString();
        //                                if (duee == 0)
        //                                {
        //                                    A.Feestatus = "Paid";
        //                                }
        //                                else if (duee != 0)
        //                                {
        //                                    A.Feestatus = "Partial";
        //                                }
        //                                //if (Tamount == realpaid)
        //                                //{
        //                                //    A.Feestatus = "Paid";
        //                                //}
        //                                //else if (Tamount != realpaid)
        //                                //{
        //                                //    A.Feestatus = "Partial";
        //                                //}

        //                                ct.FeeDetails.Add(A);
        //                            }


        //                            //A.Amount = result2.monthlyAmount;
        //                            //int Paidamount = 0;
        //                            //int Dueamount = 0;
        //                            //var pa = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();

        //                            //foreach (var f in pa)
        //                            //{
        //                            //    int paida = Convert.ToInt32(f.PaidAmount);
        //                            //    Paidamount = Paidamount + paida;
        //                            //}

        //                            //A.PaidAmount = Paidamount.ToString();

        //                            //var da1 = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();
        //                            //int Discount = 0;
        //                            //foreach (var d1 in da1)
        //                            //{
        //                            //    int duea = Convert.ToInt32(d1.duesAmount);
        //                            //    Dueamount =/* Dueamount + */(duea * -1);
        //                            //    if (duea == 0)
        //                            //    {
        //                            //        Dueamount = 0;
        //                            //    }
        //                            //    int amount4 = Convert.ToInt32(d1.discountAmnt);
        //                            //    Discount = Discount + amount4;
        //                            //}
        //                            //A.discountAmount = Discount.ToString();
        //                            //A.duesAmount = Dueamount.ToString();
        //                            //A.Month = result2.Months;
        //                            //ct.FeeDetails.Add(A);

        //                        }
        //                        else
        //                        {
        //                            count++;
        //                            newfee A = new newfee();
        //                            sqlHelper obj1 = new sqlHelper();
        //                            string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                            int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                            int am = 0;
        //                            var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
        //                            if (checkfeestructure != null)
        //                            {
        //                                var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
        //                                foreach (var m in totalamountFee)
        //                                {
        //                                    count++;
        //                                    int amu = Convert.ToInt32(m.Amount);
        //                                    am = am + amu;
        //                                }
        //                                A.Amount = am.ToString();
        //                                A.PaidAmount = "0";
        //                                A.duesAmount = am.ToString();
        //                                A.Feestatus = "UnPaid";
        //                                A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
        //                                A.discountAmount = "0";


        //                                ct.FeeDetails.Add(A);



        //                            }


        //                        }


        //                    }
        //                    for (i = 0; i <= Endmonth - 2; i++)
        //                    {

        //                        //Endmonth++;

        //                        //var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).FirstOrDefault();

        //                        var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).ToList();

        //                        if (result2.Count != 0)
        //                        {
        //                            count++;
        //                            //ct.FeeDetails = new List<newfee>(); 
        //                            int paidamountfinal = 0;
        //                            foreach (var a in result2)
        //                            {
        //                                newfee A = new newfee();

        //                                A.Amount = a.monthlyAmount;
        //                                //A.PaidAmount = a.PaidAmount;
        //                                int thistimepayamount = Convert.ToInt32(a.PaidAmount);
        //                                paidamountfinal = paidamountfinal + thistimepayamount;
        //                                A.PaidAmount = paidamountfinal.ToString();
        //                                int due = Convert.ToInt32(a.duesAmount);
        //                                int duee = due * -1;
        //                                A.duesAmount = duee.ToString();
        //                                A.Month = a.Months;
        //                                //int Tamount = Convert.ToInt32(A.Amount);
        //                                //int paidamount = Convert.ToInt32(A.PaidAmount);
        //                                //int discount = Convert.ToInt32(a.discountAmnt);
        //                                //int realpaid = paidamount + discount;
        //                                A.discountAmount = a.discountAmnt.ToString();
        //                                if (duee == 0)
        //                                {
        //                                    A.Feestatus = "Paid";
        //                                }
        //                                else if (duee != 0)
        //                                {
        //                                    A.Feestatus = "Partial";
        //                                }

        //                                ct.FeeDetails.Add(A);
        //                            }
        //                            //A.Amount = result2.monthlyAmount;
        //                            //int Paidamount = 0;
        //                            //int Dueamount = 0;
        //                            //var pa = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();

        //                            //foreach (var f in pa)
        //                            //{
        //                            //    int paida = Convert.ToInt32(f.PaidAmount);
        //                            //    Paidamount = Paidamount + paida;
        //                            //}

        //                            //A.PaidAmount = Paidamount.ToString();

        //                            //var da1 = db.tblFeeCalculates.Where(x => x.monthId == result2.monthId && x.SchoolID == result2.SchoolID && x.AcademicYear == result2.AcademicYear && x.fldstudentID == result2.fldstudentID).ToList();
        //                            //int Discount = 0;
        //                            //foreach (var d1 in da1)
        //                            //{
        //                            //    int duea = Convert.ToInt32(d1.duesAmount);
        //                            //    Dueamount = /*Dueamount + */(duea * -1);
        //                            //    if (duea == 0)
        //                            //    {
        //                            //        Dueamount = 0;
        //                            //    }
        //                            //    int amount4 = Convert.ToInt32(d1.discountAmnt);
        //                            //    Discount = Discount + amount4;
        //                            //}
        //                            //A.discountAmount = Discount.ToString();
        //                            //A.duesAmount = Dueamount.ToString();
        //                            //A.Month = result2.Months;
        //                            //ct.FeeDetails.Add(A);

        //                        }
        //                        else
        //                        {
        //                            count++;
        //                            newfee A = new newfee();
        //                            sqlHelper obj1 = new sqlHelper();
        //                            string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
        //                            int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
        //                            int am = 0;
        //                            var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
        //                            if (checkfeestructure != null)
        //                            {
        //                                var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
        //                                foreach (var m in totalamountFee)
        //                                {
        //                                    count++;
        //                                    int amu = Convert.ToInt32(m.Amount);
        //                                    am = am + amu;
        //                                }
        //                                A.Amount = am.ToString();
        //                                A.PaidAmount = "0";
        //                                A.duesAmount = am.ToString();
        //                                A.Feestatus = "UnPaid";
        //                                A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
        //                                A.discountAmount = "0";
        //                                ct.FeeDetails.Add(A);
        //                            }


        //                        }


        //                        //monthsApart = (endDate.Month + endDate.Year * 12) - (startDate.Month + startDate.Year * 12) + 1;

        //                        //var result1 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1).FirstOrDefault();
        //                        //if (result1 != null)
        //                        //{
        //                        //    int feestructureid = Convert.ToInt32(result1.feeStructureID);
        //                        //    var totalamountFee = db.tblFeeStructureConfigs.Where(a => a.FeeStructureID == feestructureid && a.IsDeleted == null).ToList();
        //                        //    int totalfee1 = 0;
        //                        //    foreach (var t in totalamountFee)
        //                        //    {
        //                        //        int amount1 = Convert.ToInt32(t.Amount);
        //                        //        totalfee1 = totalfee1 + amount1;
        //                        //    }
        //                        //    ct.Amount = totalfee1 * 12; //Total fee ac to avademicyear


        //                        //}



        //                    }
        //                }



        //                if (count != 0)
        //                {
        //                    obj.status = true;
        //                    obj.message = "sucess";
        //                    obj.data = ct;
        //                }
        //                if (count == 0)
        //                {
        //                    obj.status = false;
        //                    obj.message = "No Details Found";
        //                    obj.data = ct;
        //                }
        //            }
        //        }


        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //        obj.data = ct;
        //    }
        //    return obj;

        //}

        [System.Web.Http.Route("api/FeesApi/StudentfillPrevFeeDetailsApp1")]
        [System.Web.Http.HttpPost]
        public feesAPP StudentfillPrevFeeDetailsApp1(feess val)
        {
            int count = 0;
            feesAPP obj = new feesAPP();
            //feess list = new List<Models.feess>();
            feess ct = new Models.feess();
            ct.FeeDetails = new List<newfee>();

            try
            {
                if (val.StudentID.Equals(null) || "".Equals(val.StudentID) || val.StudentID == 0)
                {
                    obj.status = false;
                    obj.message = "Please Enter Student ID";

                }
                else if (val.SchoolID.Equals(null) || "".Equals(val.SchoolID) || val.SchoolID == 0)
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";

                }
                else if (val.AcademicYear.Equals(null) || "".Equals(val.AcademicYear) || val.AcademicYear == 0)
                {
                    obj.status = false;
                    obj.message = "Please Enter AcademicYear";

                }
                else
                {

                    long id = Convert.ToInt32(val.StudentID);
                    int SchoolID = Convert.ToInt32(val.SchoolID);
                    int AcademicYear1 = Convert.ToInt32(val.AcademicYear);
                    int monthsApart = 0;
                    var month1 = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
                    if (month1 != null)
                    {
                        DateTime startDate1 = month1.DateFrom;
                        DateTime endDate1 = month1.DateTo;

                        monthsApart = (endDate1.Month + endDate1.Year * 12) - (startDate1.Month + startDate1.Year * 12);

                        sqlHelper obj1 = new sqlHelper();
                        string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
                        int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
                        var totalamountFee = db.tblFeeStructureConfigs.Where(a => a.FeeStructureID == FEESTRUCTUREIDused && a.IsDeleted == null).ToList();
                        int totalfee1 = 0;
                        foreach (var ta in totalamountFee)
                        {
                            int amount1 = Convert.ToInt32(ta.Amount);
                            totalfee1 = totalfee1 + amount1;
                        }
                        ct.TotalAmount = totalfee1 * monthsApart; //Total fee ac to avademicyear

                        var result3 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1).ToList();
                        int totalpaid = 0;
                        int TotalDiscount = 0;
                        foreach (var tp in result3)
                        {
                            int amount2 = Convert.ToInt32(tp.PaidAmount);
                            totalpaid = totalpaid + amount2;
                            int amount3 = Convert.ToInt32(tp.discountAmnt);
                            TotalDiscount = TotalDiscount + amount3;

                        }

                        ct.TotalPaid = totalpaid; //TotalPaid
                        ct.TotalDiscount = TotalDiscount; //TotalDiscount
                        ct.TotalDueBalence = ct.TotalAmount - (ct.TotalPaid + ct.TotalDiscount); //TotalDueBalence
                        ct.StudentID = val.StudentID;
                        ct.SchoolID = val.SchoolID;
                        ct.AcademicYear = val.AcademicYear;
                    }

                    //gET FEE Structure ID
                    var GetFEEStructureID = from s in db.tblFeeStructureAssigns
                                            join fs in db.tblFeeStructures on s.FeeStructureID equals fs.ID
                                            where s.isActive && !s.isDeleted && fs.Status == true && fs.AcademicYear == AcademicYear1 && s.StudentID == id
                                            select s.FeeStructureID;


                    int FSID = Convert.ToInt32(GetFEEStructureID.FirstOrDefault());
                    var latedateday = db.tblSchoolDetails.Where(x => x.ID == SchoolID && x.IsDeleted == null).FirstOrDefault();
                    int feeday = Convert.ToInt32(latedateday.FeeDueDay);

                    //
                    var month = db.tblAcademicYears.Where(x => x.ID == AcademicYear1 && x.IsDeleted == null).FirstOrDefault();
                    if (month != null)
                    {
                        DateTime startDate = month.DateFrom;
                        DateTime endDate = month.DateTo;
                        int startmonth = startDate.Month;
                        int Endmonth = endDate.Month;
                        int startyear = startDate.Year;
                        int EndYear = endDate.Year;
                        if (startyear == EndYear)
                        {
                            int j = 0;
                            for (j = startmonth - 1; j < Endmonth - 1; j++)
                            {

                                startmonth++;

                                //var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == j).FirstOrDefault();

                                var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == j).ToList();
                                int paidamountfinal = 0;
                                if (result2.Count != 0)
                                {
                                    foreach (var a in result2)
                                    {
                                        newfee A = new newfee();
                                        A.Amount = a.monthlyAmount;
                                        int thistimepayamount = Convert.ToInt32(a.PaidAmount);
                                        paidamountfinal = paidamountfinal + thistimepayamount;
                                        A.PaidAmount = paidamountfinal.ToString();
                                        int due = Convert.ToInt32(a.duesAmount);
                                        int duee = due * -1;
                                        A.duesAmount = duee.ToString();
                                        A.Month = a.Months;
                                        //int Tamount = Convert.ToInt32(A.Amount);
                                        //int paidamount = Convert.ToInt32(A.PaidAmount);
                                        //int discount = Convert.ToInt32(a.discountAmnt);
                                        //int realpaid = paidamount + discount;
                                        A.discountAmount = a.discountAmnt.ToString();
                                        if (duee == 0)
                                        {
                                            A.Colour = "#004e00";
                                            A.Feestatus = "Paid";
                                        }
                                        else if (duee != 0)
                                        {
                                            A.Colour = "#C3C300";
                                            A.Feestatus = "Partial";
                                        }

                                        A.LateFeeFine = Convert.ToInt32(a.LateFeeFine);
                                        DateTime duefeedate = new DateTime(startyear, j + 1, feeday);
                                        A.DueDate = duefeedate.ToString("dd MMMM yyyy");
                                        DateTime feepaid = Convert.ToDateTime(a.dateCreated);
                                        A.FeePaidDate = feepaid.ToString("dd MMMM yyyy");
                                        ct.FeeDetails.Add(A);
                                    }
                                    count++;



                                }
                                else
                                {
                                    count++;
                                    newfee A = new newfee();
                                    sqlHelper obj1 = new sqlHelper();
                                    string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
                                    int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
                                    int am = 0;
                                    var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
                                    if (checkfeestructure != null)
                                    {
                                        var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
                                        foreach (var m in totalamountFee)
                                        {
                                            count++;
                                            int amu = Convert.ToInt32(m.Amount);
                                            am = am + amu;
                                        }
                                        A.Amount = am.ToString();
                                        A.PaidAmount = "0";
                                        A.duesAmount = am.ToString();
                                        A.discountAmount = "0";
                                        A.Colour = "#dd4b39";
                                        A.Feestatus = "UnPaid";
                                        A.LateFeeFine = 0;
                                        A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(j + 1);
                                        DateTime duefeedate = new DateTime(startyear, j + 1, feeday);
                                        A.DueDate = duefeedate.ToString("dd MMMM yyyy");
                                        A.FeePaidDate = "";
                                        ct.FeeDetails.Add(A);
                                    }


                                }

                            }
                        }
                        else
                        {
                            int i = 0;
                            for (i = startmonth - 1; i <= 11; i++)
                            {
                                startmonth++;

                                //var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).FirstOrDefault();
                                var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).ToList();

                                int paidamountfinal = 0;
                                if (result2.Count != 0)
                                {
                                    count++;
                                    //ct.FeeDetails = new List<newfee>(); 
                                    foreach (var a in result2)
                                    {
                                        newfee A = new newfee();

                                        A.Amount = a.monthlyAmount;
                                        int thistimepayamount = Convert.ToInt32(a.PaidAmount);
                                        paidamountfinal = paidamountfinal + thistimepayamount;
                                        A.PaidAmount = paidamountfinal.ToString();
                                        int due = Convert.ToInt32(a.duesAmount);
                                        int duee = due * -1;
                                        A.duesAmount = duee.ToString();
                                        A.Month = a.Months;

                                        A.discountAmount = a.discountAmnt.ToString();
                                        if (duee == 0)
                                        {
                                            A.Colour = "#004e00";
                                            A.Feestatus = "Paid";
                                        }
                                        else if (duee != 0)
                                        {
                                            A.Colour = "#C3C300";
                                            A.Feestatus = "Partial";
                                        }

                                        A.LateFeeFine = Convert.ToInt32(a.LateFeeFine);
                                        DateTime duefeedate = new DateTime(startyear, i + 1, feeday);
                                        A.DueDate = duefeedate.ToString("dd MMMM yyyy");
                                        DateTime feepaid = Convert.ToDateTime(a.dateCreated);
                                        A.FeePaidDate = feepaid.ToString("dd MMMM yyyy");
                                        ct.FeeDetails.Add(A);
                                    }

                                }
                                else
                                {
                                    count++;
                                    newfee A = new newfee();
                                    sqlHelper obj1 = new sqlHelper();
                                    string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
                                    int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
                                    int am = 0;
                                    var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
                                    if (checkfeestructure != null)
                                    {
                                        var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
                                        foreach (var m in totalamountFee)
                                        {
                                            count++;
                                            int amu = Convert.ToInt32(m.Amount);
                                            am = am + amu;
                                        }
                                        A.Amount = am.ToString();
                                        A.PaidAmount = "0";
                                        A.duesAmount = am.ToString();
                                        A.Colour = "#dd4b39";
                                        A.Feestatus = "UnPaid";
                                        A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
                                        A.discountAmount = "0";
                                        A.LateFeeFine = 0;
                                        DateTime duefeedate = new DateTime(startyear, i + 1, feeday);
                                        A.DueDate = duefeedate.ToString("dd MMMM yyyy");
                                        A.FeePaidDate = "";

                                        ct.FeeDetails.Add(A);



                                    }


                                }


                            }
                            for (i = 0; i <= Endmonth - 2; i++)
                            {

                                //Endmonth++;

                                //var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).FirstOrDefault();

                                var result2 = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID && y.AcademicYear == AcademicYear1 && y.monthId == i).ToList();
                                int paidamountfinal = 0;
                                if (result2.Count != 0)
                                {
                                    count++;
                                    //ct.FeeDetails = new List<newfee>(); 
                                    foreach (var a in result2)
                                    {
                                        newfee A = new newfee();

                                        A.Amount = a.monthlyAmount;
                                        int thistimepayamount = Convert.ToInt32(a.PaidAmount);
                                        paidamountfinal = paidamountfinal + thistimepayamount;
                                        A.PaidAmount = paidamountfinal.ToString();
                                        int due = Convert.ToInt32(a.duesAmount);
                                        int duee = due * -1;
                                        A.duesAmount = duee.ToString();
                                        A.Month = a.Months;
                                        //int Tamount = Convert.ToInt32(A.Amount);
                                        //int paidamount = Convert.ToInt32(A.PaidAmount);
                                        //int discount = Convert.ToInt32(a.discountAmnt);
                                        //int realpaid = paidamount + discount;
                                        A.discountAmount = a.discountAmnt.ToString();
                                        if (duee == 0)
                                        {
                                            A.Colour = "#004e00";
                                            A.Feestatus = "Paid";
                                        }
                                        else if (duee != 0)
                                        {
                                            A.Colour = "#C3C300";
                                            A.Feestatus = "Partial";
                                        }
                                        DateTime duefeedate = new DateTime(EndYear, i + 1, feeday);
                                        A.LateFeeFine = Convert.ToInt32(a.LateFeeFine);
                                        A.DueDate = duefeedate.ToString("dd MMMM yyyy");
                                        DateTime feepaid = Convert.ToDateTime(a.dateCreated);
                                        A.FeePaidDate = feepaid.ToString("dd MMMM yyyy");
                                        ct.FeeDetails.Add(A);
                                    }
                                }
                                else
                                {
                                    count++;
                                    newfee A = new newfee();
                                    sqlHelper obj1 = new sqlHelper();
                                    string FEESTRUCTUREID = obj1.ExecuteScaler("select FeeStructureID  from tblFeeStructureAssign where StudentID='" + id + "' and SchoolID='" + SchoolID + "' and isDeleted=0");
                                    int FEESTRUCTUREIDused = Convert.ToInt32(FEESTRUCTUREID);
                                    int am = 0;
                                    var checkfeestructure = db.tblFeeStructures.Where(x => x.ID == FEESTRUCTUREIDused && x.AcademicYear == AcademicYear1 && x.IsDeleted == null && x.Status == true).FirstOrDefault();
                                    if (checkfeestructure != null)
                                    {
                                        var totalamountFee = db.tblFeeStructureConfigs.Where(x => x.FeeStructureID == checkfeestructure.ID && x.IsDeleted == null).ToList();
                                        foreach (var m in totalamountFee)
                                        {
                                            count++;
                                            int amu = Convert.ToInt32(m.Amount);
                                            am = am + amu;
                                        }
                                        A.Amount = am.ToString();
                                        A.PaidAmount = "0";
                                        A.duesAmount = am.ToString();
                                        A.Feestatus = "UnPaid";
                                        A.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1);
                                        A.discountAmount = "0";
                                        A.LateFeeFine = 0;
                                        DateTime duefeedate = new DateTime(EndYear, i + 1, feeday);
                                        A.DueDate = duefeedate.ToString("dd MMMM yyyy");

                                        A.FeePaidDate = "";
                                        ct.FeeDetails.Add(A);
                                    }


                                }
                            }
                        }



                        if (count != 0)
                        {
                            obj.status = true;
                            obj.message = "sucess";
                            obj.data = ct;
                        }
                        if (count == 0)
                        {
                            obj.status = false;
                            obj.message = "No Details Found";
                            obj.data = ct;
                        }
                    }
                }


            }
            catch
            {
                obj.status = false;
                obj.message = "Something Went Wrong";
                obj.data = ct;
            }
            return obj;

        }
        public List<fees> StudentfillPrevFeeDetails(List<string> val)
        {
            int count = 0;
            List<fees> list = new List<Models.fees>();
            long id = Convert.ToInt32(val[1]);
            int SchoolID = Convert.ToInt32(val[2]);
            //var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID ).OrderBy(x => x.monthId).ToList();
            var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID == SchoolID).OrderBy(x => x.fldId).ToList();
            if (val[3] != null && val[3] != "-1")
            {
                int AcademicYear = Convert.ToInt32(val[3]);
                result = result.Where(y => y.AcademicYear == AcademicYear).OrderBy(x => x.fldId).ToList();
                //result = result.Where(y => y.AcademicYear == AcademicYear).OrderBy(x => x.monthId).ToList();
            }
            // var result = (from F in db.tblFeeCalculates join  AC in db.tblAcademicYears 
            //  var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id && y.SchoolID==SchoolID &&).OrderBy(x => x.monthId).ToList();

            foreach (var m in result)
            {
                count++;
                fees ct = new Models.fees();
                ct.fcal = new SchoolErp.tblFeeCalculate();
                if (string.IsNullOrEmpty(m.duesAmount))
                {
                    m.duesAmount = "0";

                }

                if (string.IsNullOrEmpty(m.PaidAmount))
                {
                    m.PaidAmount = "0";
                }
                ct.fcal = m;
                ct.count = count;

                list.Add(ct);
            }
            return list;
        }



        public List<fees> fillPrevFeeDetails(List<string> val)
        {
            int count = 0;
            List<fees> list = new List<Models.fees>();
            long id = Convert.ToInt32(val[1]);

            //var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id).OrderBy(x => x.monthId).ToList();
            var result = db.tblFeeCalculates.Where(y => y.fldstudentID == id).OrderBy(x => x.fldId).ToList();
            foreach (var m in result)
            {
                count++;
                fees ct = new Models.fees();
                ct.fcal = new SchoolErp.tblFeeCalculate();
                if (string.IsNullOrEmpty(m.duesAmount))
                {
                    m.duesAmount = "0";
                }
                if (string.IsNullOrEmpty(m.PaidAmount))
                {
                    m.PaidAmount = "0";
                }
                ct.fcal = m;
                ct.count = count;

                list.Add(ct);
            }
            return list;
        }



        [System.Web.Http.Route("api/FeesApi/getAllStudentsDetails1")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsDetails1(List<string> val)
        {
            List<Student> list = new List<Student>();
            string loginuser = val[7];
            int typeuser = Convert.ToInt32(val[8]);
            int count = 0;
            string name = val[0];


            if (typeuser == 2)
            {
                var result = (from c in db.TBLStudents
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              where s.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (name != "")
                {

                    result = result.Where(c => c.model.FirstName.ToUpper().Contains(name.ToUpper())).ToList();
                }
                if (val[1] != "" && val[1] != "-1")
                {
                    string year = val[1];

                    result = result.Where(c => c.model.AcademicYear.Contains(year)).ToList();
                }

                if (val[2] != "" && val[2] != "-1")
                {
                    int course = Convert.ToInt32(val[2]);

                    result = result.Where(c => c.model.ClassID == course).ToList();
                }

                if (val[3] != "" && val[3] != "-1")
                {
                    int section = Convert.ToInt32(val[3]);

                    result = result.Where(c => c.model.SectionID == section).ToList();
                }
                if (val[4] != "" && val[4] != "-1")
                {

                    string rollno = (val[4]).ToString().Trim();

                    result = result.Where(c => c.model.RollNo == rollno).ToList();
                }
                if (val[5] != "" && val[5] != "-1" && val[5] != "0")
                {

                    int SchoolID = int.Parse(val[5]);

                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
                }

                foreach (var m in result)
                {
                    var s = (from c in db.tblCourses
                             from sec in db.tblSections
                             where c.Id == m.model.ClassID && sec.Id == m.model.SectionID && sec.Id != -1
                             select new { c, sec }).SingleOrDefault();
                    count++;
                    if (s != null)
                    {
                        Student st = new Student();
                        st.ID = m.model.ID;

                        st.Class = s.c.CourseName;
                        st.Section = s.sec.SectionName;
                        st.RollNo = m.model.RollNo;
                        st.RegNo = m.model.RegNo;
                        st.FirstName = m.model.FirstName + " " + m.model.MiddleName + " " + m.model.LastName;

                        if (!string.IsNullOrEmpty(m.model.PicPath))
                        {
                            st.PicPath = m.model.PicPath;
                        }
                        else { st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg"; }
                        st.School = m.SchoolName;
                        list.Add(st);
                    }
                }
            }
            else
            {
                var result = (from c in db.TBLStudents
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (name != "")
                {

                    result = result.Where(c => c.model.FirstName.ToUpper().Contains(name.ToUpper())).ToList();
                }
                if (val[1] != "" && val[1] != "-1")
                {
                    string year = val[1];

                    result = result.Where(c => c.model.AcademicYear.Contains(year)).ToList();
                }

                if (val[2] != "" && val[2] != "-1")
                {
                    int course = Convert.ToInt32(val[2]);

                    result = result.Where(c => c.model.ClassID == course).ToList();
                }

                if (val[3] != "" && val[3] != "-1")
                {
                    int section = Convert.ToInt32(val[3]);

                    result = result.Where(c => c.model.SectionID == section).ToList();
                }
                if (val[4] != "" && val[4] != "-1")
                {

                    string rollno = (val[4]).ToString().Trim();

                    result = result.Where(c => c.model.RollNo == rollno).ToList();
                }
                if (val[5] != "" && val[5] != "-1" && val[5] != "0")
                {

                    int SchoolID = int.Parse(val[5]);

                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
                }

                foreach (var m in result)
                {
                    var s = (from c in db.tblCourses
                             from sec in db.tblSections
                             where c.Id == m.model.ClassID && sec.Id == m.model.SectionID && sec.Id != -1 && c.IsDeleted == null && sec.IsDeleted == null
                             select new { c, sec }).SingleOrDefault();
                    count++;
                    if (s != null)
                    {
                        Student st = new Student();
                        st.ID = m.model.ID;

                        st.Class = s.c.CourseName;
                        st.Section = s.sec.SectionName;
                        st.RollNo = m.model.RollNo;
                        st.RegNo = m.model.RegNo;
                        st.FirstName = m.model.FirstName + " " + m.model.MiddleName + " " + m.model.LastName;

                        if (!string.IsNullOrEmpty(m.model.PicPath))
                        {
                            st.PicPath = m.model.PicPath;
                        }
                        else { st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg"; }
                        st.School = m.SchoolName;
                        list.Add(st);
                    }
                }
            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/FeesApi/getAllStudentsDetails")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsDetails(List<string> val)
        {
            List<Student> list = new List<Student>();
            string loginuser = val[6];
            int typeuser = Convert.ToInt32(val[7]);
            int count = 0;
            string name = val[0];


            if (typeuser == 2)
            {
                var result = (from c in db.TBLStudents
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              where s.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School

                              }).ToList();

                if (name != "")
                {

                    result = result.Where(c => c.model.FirstName.ToUpper().Contains(name.ToUpper())).ToList();
                }
                if (val[1] != "" && val[1] != "-1")
                {
                    string year = val[1];

                    result = result.Where(c => c.model.AcademicYear.Contains(year)).ToList();
                }

                if (val[2] != "" && val[2] != "-1")
                {
                    int course = Convert.ToInt32(val[2]);

                    result = result.Where(c => c.model.ClassID == course).ToList();
                }

                if (val[3] != "" && val[3] != "-1")
                {
                    int section = Convert.ToInt32(val[3]);

                    result = result.Where(c => c.model.SectionID == section).ToList();
                }
                if (val[4] != "" && val[4] != "-1")
                {

                    string rollno = (val[4]).ToString().Trim();

                    result = result.Where(c => c.model.RollNo == rollno).ToList();
                }
                if (val[5] != "" && val[5] != "-1" && val[5] != "0")
                {

                    int SchoolID = int.Parse(val[5]);

                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
                }

                foreach (var m in result)
                {
                    var s = (from c in db.tblCourses
                             from sec in db.tblSections
                             where c.Id == m.model.ClassID && sec.Id == m.model.SectionID && sec.Id != -1
                             select new { c, sec }).SingleOrDefault();
                    count++;
                    if (s != null)
                    {
                        Student st = new Student();
                        st.ID = m.model.ID;

                        st.Class = s.c.CourseName;
                        st.Section = s.sec.SectionName;
                        st.RollNo = m.model.RollNo;
                        st.RegNo = m.model.RegNo;
                        st.FirstName = m.model.FirstName + " " + m.model.MiddleName + " " + m.model.LastName;

                        if (!string.IsNullOrEmpty(m.model.PicPath))
                        {
                            st.PicPath = m.model.PicPath;
                        }
                        else { st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg"; }
                        st.School = m.SchoolName;
                        list.Add(st);
                    }
                }
            }
            else
            {
                var result = (from c in db.TBLStudents
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  SchoolName = s.School,


                              }).ToList();

                if (name != "")
                {

                    result = result.Where(c => c.model.FirstName.ToUpper().Contains(name.ToUpper())).ToList();
                }
                if (val[1] != "" && val[1] != "-1")
                {
                    string year = val[1];

                    result = result.Where(c => c.model.AcademicYear.Contains(year)).ToList();
                }

                if (val[2] != "" && val[2] != "-1")
                {
                    int course = Convert.ToInt32(val[2]);

                    result = result.Where(c => c.model.ClassID == course).ToList();
                }

                if (val[3] != "" && val[3] != "-1")
                {
                    int section = Convert.ToInt32(val[3]);

                    result = result.Where(c => c.model.SectionID == section).ToList();
                }
                if (val[4] != "" && val[4] != "-1")
                {

                    string rollno = (val[4]).ToString().Trim();

                    result = result.Where(c => c.model.RollNo == rollno).ToList();
                }
                if (val[5] != "" && val[5] != "-1" && val[5] != "0")
                {

                    int SchoolID = int.Parse(val[5]);

                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();
                }

                foreach (var m in result)
                {
                    var s = (from c in db.tblCourses
                             from sec in db.tblSections
                             where c.Id == m.model.ClassID && sec.Id == m.model.SectionID && sec.Id != -1 && c.IsDeleted == null && sec.IsDeleted == null
                             select new { c, sec }).SingleOrDefault();
                    count++;
                    if (s != null)
                    {
                        Student st = new Student();
                        st.ID = m.model.ID;

                        st.Class = s.c.CourseName;
                        st.Section = s.sec.SectionName;
                        st.RollNo = m.model.RollNo;
                        st.RegNo = m.model.RegNo;
                        st.FirstName = m.model.FirstName + " " + m.model.MiddleName + " " + m.model.LastName;

                        if (!string.IsNullOrEmpty(m.model.PicPath))
                        {
                            st.PicPath = m.model.PicPath;
                        }
                        else { st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg"; }
                        st.School = m.SchoolName;
                        list.Add(st);
                    }
                }
            }
            return list.ToArray();


            //    var predicate = PredicateBuilder.True<TBLStudent>();
            //    if (name != "")
            //    {
            //        predicate = predicate.And(x => x.FirstName.ToUpper().Contains(name.ToUpper()));
            //    }
            //    if (val[1] != "" && val[1] != "-1")
            //    {
            //        string year = val[1];
            //        predicate = predicate.And(x => x.AcademicYear.Contains(year));
            //    }

            //    if (val[2] != "" && val[2] != "-1")
            //    {
            //        int course = Convert.ToInt32(val[2]);
            //        predicate = predicate.And(x => x.ClassID == course);
            //    }

            //    if (val[3] != "" && val[3] != "-1")
            //    {
            //        int section = Convert.ToInt32(val[3]);
            //        predicate = predicate.And(x => x.SectionID == section);
            //    }

            //    if (val[4] != "" && val[4] != "-1")
            //    {
            //        //int section = Convert.ToInt32(val[5]);
            //        string rollno = (val[4]).ToString().Trim();
            //        predicate = predicate.And(x => x.RollNo == rollno);
            //    }

            //    var result = db.TBLStudents.AsExpandable().Where(predicate).ToList();


            //    foreach (var m in result)
            //    {
            //        var s = (from c in db.tblCourses
            //                 from sec in db.tblSections
            //                 where c.Id == m.ClassID && sec.Id == m.SectionID && sec.Id != -1
            //                 select new { c, sec }).SingleOrDefault();
            //        count++;
            //        if (s != null)
            //        {
            //            Student st = new Student();
            //            st.ID = m.ID;

            //            st.Class = s.c.CourseName;
            //            st.Section = s.sec.SectionName;
            //            st.RollNo = m.RollNo;
            //            st.RegNo = m.RegNo;
            //            st.FirstName = m.FirstName + " " + m.MiddleName + " " + m.LastName;

            //            if (!string.IsNullOrEmpty(m.PicPath))
            //            {
            //                st.PicPath = m.PicPath;
            //            }
            //            else { st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg"; }

            //            list.Add(st);
            //        }
            //    }

            //    return list.ToArray();
        }


        //avi

        public List<fees> fillFeeStructureList1(List<string> val)
        {


            string loginuser = val[7];
            int typeuser = Convert.ToInt32(val[8]);
            int count = 0;
            List<fees> list = new List<Models.fees>();

            if (typeuser == 2)
            {
                string name = val[1]; //structurename
                var result = (from c in db.tblFeeStructures
                              join scl in db.tblFeeStructureClasses on c.ID equals scl.FeeStructureID
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              where c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  model1 = scl,
                                  SchoolName = s.School

                              }).ToList();

                if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
                {
                    int year = Convert.ToInt32(val[2]);
                    result = result.Where(c => c.model.AcademicYear == year).ToList();

                }
                if (name != "") //structurename
                {
                    result = result.Where(c => c.model.StructureName == name).ToList();

                }
                if (val[5] != "" && val[5] != "-1") //Status
                {
                    bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
                    result = result.Where(c => c.model.Status == status).ToList();

                }
                if (val[6] != "0" && val[6] != "")
                {
                    int SchoolID = int.Parse(val[6]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                {
                    int course = Convert.ToInt32(val[3]);
                    result = result.Where(c => c.model1.ClassID == course).ToList();
                }
                if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                {
                    int sec = Convert.ToInt32(val[4]);
                    result = result.Where(x => x.model1.SectionID == sec).ToList();
                }
                foreach (var m in result)
                {
                    fees ct = new Models.fees();
                    ct.classes = new List<string>();
                    //if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                    //{
                    //    int course = Convert.ToInt32(val[3]);



                    //    var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


                    //    int cls_count = cls.Count;
                    //    if (cls_count > 0)
                    //    {
                    //        if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                    //        {
                    //            int sec = Convert.ToInt32(val[4]);
                    //            cls = cls.Where(x => x.SectionID == sec).ToList();
                    //            if (cls.Count < 0)
                    //            {
                    //                continue;
                    //            }
                    //        }
                    //        foreach (var c_s in cls)
                    //        {
                    //            ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        continue;
                    //    }
                    //}
                    count++;

                    ct.fs = new tblFeeStructure();
                    ct.fs = m.model;
                    ct.count = count;
                    ct.School = m.SchoolName;
                    if (ct.classes.Count == 0)
                    {
                        var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
                        foreach (var c in fsCls)
                        {
                            ct.classes.Add(c.ClassID + "-" + c.SectionID);
                        }
                    }

                    if (ct.fs.Status)
                    {
                        ct.StatusNm = "Active";
                        ct.style = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        ct.StatusNm = "InActive";
                        ct.style = "btn btn-block btn-danger btn-sm";

                    }

                    list.Add(ct);
                }

            }
            else
            {
                string name = val[1]; //structurename
                var result = (from c in db.tblFeeStructures
                              join scl in db.tblFeeStructureClasses on c.ID equals scl.FeeStructureID
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && c.IsDeleted == null && scl.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  model1 = scl,
                                  SchoolName = s.School

                              }).ToList();

                if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
                {
                    int year = Convert.ToInt32(val[2]);
                    result = result.Where(c => c.model.AcademicYear == year).ToList();

                }
                //if (name != "") //structurename
                //{
                //    result = result.Where(c => c.model.StructureName == name).ToList();

                //}
                if (val[5] != "" && val[5] != "-1") //Status
                {
                    bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
                    result = result.Where(c => c.model.Status == status).ToList();

                }
                if (val[6] != "0" && val[6] != "")
                {
                    int SchoolID = int.Parse(val[6]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                {
                    int course = Convert.ToInt32(val[3]);
                    result = result.Where(c => c.model1.ClassID == course).ToList();
                }
                if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                {
                    int sec = Convert.ToInt32(val[4]);
                    result = result.Where(x => x.model1.SectionID == sec).ToList();
                }
                foreach (var m in result)
                {
                    fees ct = new Models.fees();
                    ct.classes = new List<string>();
                    //if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                    //{
                    //    int course = Convert.ToInt32(val[3]);



                    //    var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


                    //    int cls_count = cls.Count;
                    //    if (cls_count > 0)
                    //    {
                    //        if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                    //        {
                    //            int sec = Convert.ToInt32(val[4]);
                    //            cls = cls.Where(x => x.SectionID == sec).ToList();
                    //            if (cls.Count < 0)
                    //            {
                    //                continue;
                    //            }
                    //        }
                    //        foreach (var c_s in cls)
                    //        {
                    //            ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        continue;
                    //    }
                    //}
                    count++;

                    ct.fs = new tblFeeStructure();
                    ct.fs = m.model;
                    ct.count = count;
                    ct.School = m.SchoolName;
                    if (ct.classes.Count == 0)
                    {
                        var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
                        foreach (var c in fsCls)
                        {
                            ct.classes.Add(c.ClassID + "-" + c.SectionID);
                        }
                    }

                    if (ct.fs.Status)
                    {
                        ct.StatusNm = "Active";
                        ct.style = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        ct.StatusNm = "InActive";
                        ct.style = "btn btn-block btn-danger btn-sm";

                    }

                    list.Add(ct);

                }
            }
            return list;
        }





        //privious
        //public List<fees> fillFeeStructureList1(List<string> val)
        //{


        //    string loginuser = val[7];
        //    int typeuser = Convert.ToInt32(val[8]);
        //    int count = 0;
        //    List<fees> list = new List<Models.fees>();

        //     if (typeuser == 2)
        //    {
        //        string name = val[1]; //structurename
        //        var result = (from c in db.tblFeeStructures
        //                      join s in db.tblSchoolDetails on c.SchoolID equals s.ID
        //                      where c.IsDeleted == null
        //                      select new
        //                      {
        //                          model = c,
        //                          SchoolName = s.School

        //                      }).ToList();

        //       if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
        //        {
        //            int year = Convert.ToInt32(val[2]);
        //            result = result.Where(c => c.model.AcademicYear == year).ToList();

        //        }
        //        if (name != "") //structurename
        //        {
        //            result = result.Where(c => c.model.StructureName == name).ToList();

        //        }
        //        if (val[5] != "" && val[5] != "-1") //Status
        //        {
        //            bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
        //            result = result.Where(c => c.model.Status == status).ToList();

        //        }
        //        if (val[6] != "0" && val[6] != "")
        //        {
        //            int SchoolID = int.Parse(val[6]);
        //            result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

        //        }

        //        foreach (var m in result)
        //        {
        //            fees ct = new Models.fees();
        //            ct.classes = new List<string>();
        //            if (val[3] != "-1" && val[3] != "0" && val[3] != "")
        //            {
        //                int course = Convert.ToInt32(val[3]);



        //                var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


        //                int cls_count = cls.Count;
        //                if (cls_count > 0)
        //                {
        //                    if (val[4] != "-1" && val[4] != "0" && val[4] != "")
        //                    {
        //                        int sec = Convert.ToInt32(val[4]);
        //                        cls = cls.Where(x => x.SectionID == sec).ToList();
        //                        if (cls.Count < 0)
        //                        {
        //                            continue;
        //                        }
        //                    }
        //                    foreach (var c_s in cls)
        //                    {
        //                        ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //            count++;

        //            ct.fs = new tblFeeStructure();
        //            ct.fs = m.model;
        //            ct.count = count;
        //            ct.School = m.SchoolName;
        //            if (ct.classes.Count == 0)
        //            {
        //                var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
        //                foreach (var c in fsCls)
        //                {
        //                    ct.classes.Add(c.ClassID + "-" + c.SectionID);
        //                }
        //            }

        //            if (ct.fs.Status)
        //            {
        //                ct.StatusNm = "Active";
        //                ct.style = "btn btn-block btn-success btn-sm";
        //            }
        //            else
        //            {
        //                ct.StatusNm = "Inactive";
        //                ct.style = "btn btn-block btn-danger btn-sm";

        //            }

        //            list.Add(ct);
        //        }

        //    }
        //     else
        //    {
        //        string name = val[1]; //structurename
        //        var result = (from c in db.tblFeeStructures
        //                      join s in db.tblSchoolDetails on c.SchoolID equals s.ID
        //                      join em in db.tblEmployees on c.SchoolID equals em.SchoolID
        //                      where em.UserID == loginuser && c.IsDeleted == null
        //                      select new
        //                      {
        //                          model = c,
        //                          SchoolName = s.School

        //                      }).ToList();

        //        if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
        //        {
        //            int year = Convert.ToInt32(val[2]);
        //                result = result.Where(c => c.model.AcademicYear == year).ToList();

        //        }
        //        //if (name != "") //structurename
        //        //{
        //        //    result = result.Where(c => c.model.StructureName == name).ToList();

        //        //}
        //        if (val[5] != "" && val[5] != "-1") //Status
        //        {
        //            bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
        //            result = result.Where(c => c.model.Status == status).ToList();

        //        }
        //        if (val[6] != "0" && val[6] != "")
        //        {
        //            int SchoolID = int.Parse(val[6]);
        //            result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

        //        }

        //        foreach (var m in result)
        //        {
        //            fees ct = new Models.fees();
        //            ct.classes = new List<string>();
        //            if (val[3] != "-1" && val[3] != "0" && val[3] != "")
        //            {
        //                int course = Convert.ToInt32(val[3]);



        //                var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


        //                int cls_count = cls.Count;
        //                if (cls_count > 0)
        //                {
        //                    if (val[4] != "-1" && val[4] != "0" && val[4] != "")
        //                    {
        //                        int sec = Convert.ToInt32(val[4]);
        //                        cls = cls.Where(x => x.SectionID == sec).ToList();
        //                        if (cls.Count < 0)
        //                        {
        //                            continue;
        //                        }
        //                    }
        //                    foreach (var c_s in cls)
        //                    {
        //                        ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //            count++;

        //            ct.fs = new tblFeeStructure();
        //            ct.fs = m.model;
        //            ct.count = count;
        //            ct.School = m.SchoolName;
        //            if (ct.classes.Count == 0)
        //            {
        //                var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
        //                foreach (var c in fsCls)
        //                {
        //                    ct.classes.Add(c.ClassID + "-" + c.SectionID);
        //                }
        //            }

        //            if (ct.fs.Status)
        //            {
        //                ct.StatusNm = "Active";
        //                ct.style = "btn btn-block btn-success btn-sm";
        //            }
        //            else
        //            {
        //                ct.StatusNm = "Inactive";
        //                ct.style = "btn btn-block btn-danger btn-sm";

        //            }

        //            list.Add(ct);

        //        }
        //    }
        //    return list;
        //}
        //avi

        public List<fees> fillFeeStructureList(List<string> val)
        {


            string loginuser = val[7];
            int typeuser = Convert.ToInt32(val[8]);
            int count = 0;
            List<fees> list = new List<Models.fees>();

            if (typeuser == 2)
            {
                string name = val[1]; //structurename
                var result = (from c in db.tblFeeStructures
                              join scl in db.tblFeeStructureClasses on c.ID equals scl.FeeStructureID
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              where c.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  model1 = scl,
                                  SchoolName = s.School

                              }).ToList();

                if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
                {
                    int year = Convert.ToInt32(val[2]);
                    result = result.Where(c => c.model.AcademicYear == year).ToList();

                }
                if (name != "") //structurename
                {
                    int structurename = Convert.ToInt32(name);
                    result = result.Where(c => c.model.ID == structurename).ToList();

                }
                if (val[5] != "" && val[5] != "-1") //Status
                {
                    bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
                    result = result.Where(c => c.model.Status == status).ToList();

                }
                if (val[6] != "0" && val[6] != "")
                {
                    int SchoolID = int.Parse(val[6]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                {
                    int course = Convert.ToInt32(val[3]);
                    result = result.Where(c => c.model1.ClassID == course).ToList();
                }
                if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                {
                    int sec = Convert.ToInt32(val[4]);
                    result = result.Where(x => x.model1.SectionID == sec).ToList();
                }
                foreach (var m in result)
                {
                    fees ct = new Models.fees();
                    ct.classes = new List<string>();
                    //if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                    //{
                    //    int course = Convert.ToInt32(val[3]);



                    //    var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


                    //    int cls_count = cls.Count;
                    //    if (cls_count > 0)
                    //    {
                    //        if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                    //        {
                    //            int sec = Convert.ToInt32(val[4]);
                    //            cls = cls.Where(x => x.SectionID == sec).ToList();
                    //            if (cls.Count < 0)
                    //            {
                    //                continue;
                    //            }
                    //        }
                    //        foreach (var c_s in cls)
                    //        {
                    //            ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        continue;
                    //    }
                    //}
                    count++;

                    ct.fs = new tblFeeStructure();
                    ct.fs = m.model;
                    ct.count = count;
                    ct.School = m.SchoolName;
                    if (ct.classes.Count == 0)
                    {
                        var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
                        foreach (var c in fsCls)
                        {
                            ct.classes.Add(c.ClassID + "-" + c.SectionID);
                        }
                    }

                    if (ct.fs.Status)
                    {
                        ct.StatusNm = "Active";
                        ct.style = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        ct.StatusNm = "InActive";
                        ct.style = "btn btn-block btn-danger btn-sm";

                    }

                    list.Add(ct);
                }

            }
            else
            {
                string name = val[1]; //structurename
                var result = (from c in db.tblFeeStructures
                              join scl in db.tblFeeStructureClasses on c.ID equals scl.FeeStructureID
                              join s in db.tblSchoolDetails on c.SchoolID equals s.ID
                              join em in db.tblEmployees on c.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && c.IsDeleted == null && scl.IsDeleted == null
                              select new
                              {
                                  model = c,
                                  model1 = scl,
                                  SchoolName = s.School

                              }).ToList();

                if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
                {
                    int year = Convert.ToInt32(val[2]);
                    result = result.Where(c => c.model.AcademicYear == year).ToList();

                }
                if (name != "" && name != "-1") //structurename
                {
                    int structurename = Convert.ToInt32(name);
                    result = result.Where(c => c.model.ID == structurename).ToList();

                }
                if (val[5] != "" && val[5] != "-1") //Status
                {
                    bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
                    result = result.Where(c => c.model.Status == status).ToList();

                }
                if (val[6] != "0" && val[6] != "")
                {
                    int SchoolID = int.Parse(val[6]);
                    result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

                }
                if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                {
                    int course = Convert.ToInt32(val[3]);
                    result = result.Where(c => c.model1.ClassID == course).ToList();
                }
                if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                {
                    int sec = Convert.ToInt32(val[4]);
                    result = result.Where(x => x.model1.SectionID == sec).ToList();
                }
                foreach (var m in result)
                {
                    fees ct = new Models.fees();
                    ct.classes = new List<string>();
                    //if (val[3] != "-1" && val[3] != "0" && val[3] != "")
                    //{
                    //    int course = Convert.ToInt32(val[3]);



                    //    var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


                    //    int cls_count = cls.Count;
                    //    if (cls_count > 0)
                    //    {
                    //        if (val[4] != "-1" && val[4] != "0" && val[4] != "")
                    //        {
                    //            int sec = Convert.ToInt32(val[4]);
                    //            cls = cls.Where(x => x.SectionID == sec).ToList();
                    //            if (cls.Count < 0)
                    //            {
                    //                continue;
                    //            }
                    //        }
                    //        foreach (var c_s in cls)
                    //        {
                    //            ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        continue;
                    //    }
                    //}
                    count++;

                    ct.fs = new tblFeeStructure();
                    ct.fs = m.model;
                    ct.count = count;
                    ct.School = m.SchoolName;
                    ct.FeeFine = m.model.DueFeeFineAmount;
                    if (ct.classes.Count == 0)
                    {
                        var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
                        foreach (var c in fsCls)
                        {
                            ct.classes.Add(c.ClassID + "-" + c.SectionID);
                        }
                    }

                    if (ct.fs.Status)
                    {
                        ct.StatusNm = "Active";
                        ct.style = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        ct.StatusNm = "InActive";
                        ct.style = "btn btn-block btn-danger btn-sm";

                    }

                    list.Add(ct);

                }
            }
            return list;
        }


        //previous
        //public List<fees> fillFeeStructureList(List<string> val)
        //{


        //    string loginuser = val[7];
        //    int typeuser = Convert.ToInt32(val[8]);
        //    int count = 0;
        //    List<fees> list = new List<Models.fees>();

        //    if (typeuser == 2)
        //    {
        //        string name = val[1]; //structurename
        //        var result = (from c in db.tblFeeStructures
        //                      join s in db.tblSchoolDetails on c.SchoolID equals s.ID
        //                      where c.IsDeleted == null
        //                      select new
        //                      {
        //                          model = c,
        //                          SchoolName = s.School

        //                      }).ToList();

        //        if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
        //        {
        //            int year = Convert.ToInt32(val[2]);
        //            result = result.Where(c => c.model.AcademicYear == year).ToList();

        //        }
        //        if (name != "") //structurename
        //        {
        //            result = result.Where(c => c.model.StructureName == name).ToList();

        //        }
        //        if (val[5] != "" && val[5] != "-1") //Status
        //        {
        //            bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
        //            result = result.Where(c => c.model.Status == status).ToList();

        //        }
        //        if (val[6] != "0" && val[6] != "")
        //        {
        //            int SchoolID = int.Parse(val[6]);
        //            result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

        //        }

        //        foreach (var m in result)
        //        {
        //            fees ct = new Models.fees();
        //            ct.classes = new List<string>();
        //            if (val[3] != "-1" && val[3] != "0" && val[3] != "")
        //            {
        //                int course = Convert.ToInt32(val[3]);



        //                var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


        //                int cls_count = cls.Count;
        //                if (cls_count > 0)
        //                {
        //                    if (val[4] != "-1" && val[4] != "0" && val[4] != "")
        //                    {
        //                        int sec = Convert.ToInt32(val[4]);
        //                        cls = cls.Where(x => x.SectionID == sec).ToList();
        //                        if (cls.Count < 0)
        //                        {
        //                            continue;
        //                        }
        //                    }
        //                    foreach (var c_s in cls)
        //                    {
        //                        ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //            count++;

        //            ct.fs = new tblFeeStructure();
        //            ct.fs = m.model;
        //            ct.count = count;
        //            ct.School = m.SchoolName;
        //            if (ct.classes.Count == 0)
        //            {
        //                var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
        //                foreach (var c in fsCls)
        //                {
        //                    ct.classes.Add(c.ClassID + "-" + c.SectionID);
        //                }
        //            }

        //            if (ct.fs.Status)
        //            {
        //                ct.StatusNm = "Active";
        //                ct.style = "btn btn-block btn-success btn-sm";
        //            }
        //            else
        //            {
        //                ct.StatusNm = "Inactive";
        //                ct.style = "btn btn-block btn-danger btn-sm";

        //            }

        //            list.Add(ct);
        //        }

        //    }
        //    else
        //    {
        //        string name = val[1]; //structurename
        //        var result = (from c in db.tblFeeStructures
        //                      join s in db.tblSchoolDetails on c.SchoolID equals s.ID
        //                      join em in db.tblEmployees on c.SchoolID equals em.SchoolID
        //                      where em.UserID == loginuser && c.IsDeleted == null
        //                      select new
        //                      {
        //                          model = c,
        //                          SchoolName = s.School

        //                      }).ToList();

        //        if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
        //        {
        //            int year = Convert.ToInt32(val[2]);
        //            result = result.Where(c => c.model.AcademicYear == year).ToList();

        //        }
        //        if (name != "") //structurename
        //        {
        //            result = result.Where(c => c.model.StructureName == name).ToList();

        //        }
        //        if (val[5] != "" && val[5] != "-1") //Status
        //        {
        //            bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
        //            result = result.Where(c => c.model.Status == status).ToList();

        //        }
        //        if (val[6] != "0" && val[6] != "")
        //        {
        //            int SchoolID = int.Parse(val[6]);
        //            result = result.Where(c => c.model.SchoolID == SchoolID).ToList();

        //        }

        //        foreach (var m in result)
        //        {
        //            fees ct = new Models.fees();
        //            ct.classes = new List<string>();
        //            if (val[3] != "-1" && val[3] != "0" && val[3] != "")
        //            {
        //                int course = Convert.ToInt32(val[3]);



        //                var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course && x.IsDeleted == null).ToList();


        //                int cls_count = cls.Count;
        //                if (cls_count > 0)
        //                {
        //                    if (val[4] != "-1" && val[4] != "0" && val[4] != "")
        //                    {
        //                        int sec = Convert.ToInt32(val[4]);
        //                        cls = cls.Where(x => x.SectionID == sec).ToList();
        //                        if (cls.Count < 0)
        //                        {
        //                            continue;
        //                        }
        //                    }
        //                    foreach (var c_s in cls)
        //                    {
        //                        ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //            count++;

        //            ct.fs = new tblFeeStructure();
        //            ct.fs = m.model;
        //            ct.count = count;
        //            ct.School = m.SchoolName;
        //            if (ct.classes.Count == 0)
        //            {
        //                var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.model.ID && x.IsDeleted == null).ToList();
        //                foreach (var c in fsCls)
        //                {
        //                    ct.classes.Add(c.ClassID + "-" + c.SectionID);
        //                }
        //            }

        //            if (ct.fs.Status)
        //            {
        //                ct.StatusNm = "Active";
        //                ct.style = "btn btn-block btn-success btn-sm";
        //            }
        //            else
        //            {
        //                ct.StatusNm = "Inactive";
        //                ct.style = "btn btn-block btn-danger btn-sm";

        //            }

        //            list.Add(ct);

        //        }
        //    }
        //    return list;
        //}
        // int count = 0;
        // List<fees> list = new List<Models.fees>();
        // string name = val[1]; //structurename

        // var predicate = PredicateBuilder.True<tblFeeStructure>();
        // if (val[2] != "" && val[2] != "-1" && val[2] != "0") //Academic Year
        // {
        //     int year = Convert.ToInt32(val[2]);
        //     predicate = predicate.And(x => x.AcademicYear == year);
        // }
        // if (name != "") //structurename
        // {
        //     predicate = predicate.And(x => x.StructureName.ToUpper().Contains(name.ToUpper()));
        // }
        // if (val[5] != "" && val[5] != "-1") //Status
        // {
        //     bool status = Convert.ToInt32(val[5].Trim()) == 1 ? true : false;
        //     predicate = predicate.And(x => x.Status == status);
        // }
        // if ( val[6]!="0" && val[6] != "")
        // {
        //     int SchoolID = int.Parse(val[6]);
        //     predicate = predicate.And(x => x.SchoolID == SchoolID);
        // }
        //var result = db.tblFeeStructures.AsExpandable().Where(predicate).ToList();

        // foreach (var m in result)
        // {
        //     fees ct = new Models.fees();
        //     ct.classes = new List<string>();
        //     if (val[3] != "-1" && val[3] != "0" && val[3] != "")
        //     {
        //         int course = Convert.ToInt32(val[3]);



        //         var cls = db.tblFeeStructureClasses.Where(x => x.ClassID == course).ToList();


        //         int cls_count = cls.Count;
        //         if (cls_count > 0)
        //         {
        //             if (val[4] != "-1" && val[4] != "0" && val[4] != "")
        //             {
        //                 int sec = Convert.ToInt32(val[4]);
        //                 cls = cls.Where(x => x.SectionID == sec).ToList();
        //                 if (cls.Count < 0)
        //                 {
        //                     continue;
        //                 }
        //             }
        //             foreach (var c_s in cls)
        //             {
        //                 ct.classes.Add(c_s.ClassID + "-" + c_s.SectionID);
        //             }
        //         }
        //         else
        //         {
        //             continue;
        //         }
        //     }
        //     count++;

        //     ct.fs = new tblFeeStructure();
        //     ct.fs = m;
        //     ct.count = count;

        //     if (ct.classes.Count == 0)
        //     {
        //         var fsCls = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == m.ID).ToList();
        //         foreach (var c in fsCls)
        //         {
        //             ct.classes.Add(c.ClassID + "-" + c.SectionID);
        //         }
        //     }

        //     if (ct.fs.Status)
        //     {
        //         ct.StatusNm = "Active";
        //         ct.style = "btn btn-block btn-success btn-sm";
        //     }
        //     else
        //     {
        //         ct.StatusNm = "Inactive";
        //         ct.style = "btn btn-block btn-danger btn-sm";

        //     }

        //     list.Add(ct);
        // }
        // return list;



        //[System.Web.Http.Route("api/FeesApi/GetClasseswithSectionSchoolID")]
        //[System.Web.Http.HttpPost]
        //public Student[] GetClasseswithSectionSchoolID(List<string> val)
        //{
        //    List<Student> list = new List<Student>();
        //    int SchoolID = Convert.ToInt32(val[0]);
        //    try
        //    {
        //        var result = (from c in db.tblCourses
        //                      join s in db.tblSections on c.Id equals s.ClassId
        //                      where c.SchoolID==SchoolID && s.SchoolID==SchoolID
        //                      && c.IsDeleted==null && s.IsDeleted==null
        //                      select new
        //                      {
        //                          c,
        //                          s
        //                      }).ToList();
        //        foreach (var a in result)
        //        {
        //            Student items = new Student();
        //            items.Class = a.c.CourseName + "-" + a.s.SectionName;
        //            items.Section = a.c.Id + "-" + a.s.Id;
        //            list.Add(items);
        //        }
        //    }
        //    catch (Exception e)
        //    { throw e; }
        //    return list.ToArray();
        //}


        [System.Web.Http.Route("api/FeesApi/GetClasseswithSection")]
        [System.Web.Http.HttpPost]
        public Student[] GetClasseswithSection()
        {
            List<Student> list = new List<Student>();

            try
            {
                var result = (from c in db.tblCourses
                              join s in db.tblSections on c.Id equals s.ClassId
                              select new
                              {
                                  c,
                                  s
                              }).ToList();
                foreach (var a in result)
                {
                    Student items = new Student();
                    items.Class = a.c.CourseName + "-" + a.s.SectionName;
                    items.Section = a.c.Id + "-" + a.s.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/FeesApi/GetFeeStructureDet")]
        [System.Web.Http.HttpPost]
        public fees GetFeeStructureDet(List<string> val)
        {
            fees f = new Models.fees();
            f.fs = new tblFeeStructure();
            f.classes = new List<string>();
            int id = Convert.ToInt32(val[0]);
            try
            {
                //var result = db.tblFeeStructures.SingleOrDefault(x=>x.ID==id);
                var result = (from c in db.tblFeeStructures
                              join a in db.tblAcademicYears on c.AcademicYear equals a.ID
                              where c.ID == id
                              select new
                              { c, a }
                              ).SingleOrDefault();
                f.year = result.a.DateFrom.Year + "-" + result.a.DateTo.ToString("yy");
                f.fs = result.c;


                //var clas = db.tblFeeStructureClasses.Where(x => x.FeeStructureID == id).ToList();
                var clas = (from c in db.tblFeeStructureClasses
                            join cr in db.tblCourses on c.ClassID equals cr.Id
                            join s in db.tblSections on c.SectionID equals s.Id
                            where c.FeeStructureID == id
                            select new
                            { c, cr, s }).ToList();
                foreach (var c in clas)
                {
                    f.classes.Add(c.cr.CourseName + "-" + c.s.SectionName);
                }
                f.courses = string.Join(",", f.classes.ToArray());

            }
            catch (Exception ex)
            { f.fs.ID = -1; throw ex; }
            return f;
        }



        [System.Web.Http.Route("api/FeesApi/SaveTempAPP")]
        [System.Web.Http.HttpPost]
        public feesAPP SaveTempAPP(fees[] f)
        {
            //List<string> l = new List<string>();
            //l.Add("0");
            //l.Add("FeeCalculate");
            //DeleteRecord(l);
            feesAPP obj = new feesAPP();
            List<long> mnths = new List<long>();

            int avi = 0;
            var a = f.SingleOrDefault();
            int SchoolID = Convert.ToInt32(a.SchoolID);
            try
            {
                foreach (var m in a.monthList)
                {
                    long month = Convert.ToInt32(m.monthID);

                    var chk = db.tblFeeCalculate_temp.Any(x => x.monthId == month && x.SchoolID == SchoolID);
                    avi++;
                    if (!chk)
                    {

                        tblFeeCalculate_temp cta = new SchoolErp.tblFeeCalculate_temp();
                        cta.monthId = Convert.ToInt32(m.monthID);
                        cta.Months = m.month;
                        cta.monthlyAmount = a.courses;
                        cta.ActualPayableAmnt = a.style.Trim();
                        cta.SchoolID = SchoolID;
                        db.tblFeeCalculate_temp.Add(cta);

                    }
                    mnths.Add(month);
                }
                db.SaveChanges();
                if (avi != 0)
                {
                    obj.status = true;
                    obj.message = "Sucess";
                    //obj.data = ;
                }
                else if (avi != 0)
                {
                    obj.status = false;
                    obj.message = "data not found";
                    //obj.data = ;
                }
                var monthsindb = db.tblFeeCalculate_temp.Where(x => x.SchoolID == SchoolID).Select(x => x.monthId).ToList();
                foreach (long mon in monthsindb)
                {
                    if (!mnths.Contains(mon))
                    {
                        List<string> l = new List<string>();
                        l.Add(mon.ToString());
                        l.Add("FeeCalculate");
                        DeleteRecord(l);
                    }
                }

            }
            catch
            {
                obj.status = false;
                obj.message = "Something Went Wrong";
            }


            return obj;
        }



        [System.Web.Http.Route("api/FeesApi/SaveTemp")]
        [System.Web.Http.HttpPost]
        public int SaveTemp(fees[] f)
        {
            //List<string> l = new List<string>();
            //l.Add("0");
            //l.Add("FeeCalculate");
            //DeleteRecord(l);

            List<long> mnths = new List<long>();

            var a = f.SingleOrDefault();
            int SchoolID = Convert.ToInt32(a.SchoolID);
            foreach (var m in a.monthList)
            {
                long month = Convert.ToInt32(m.monthID);

                var chk = db.tblFeeCalculate_temp.Any(x => x.monthId == month && x.SchoolID == SchoolID);

                if (!chk)
                {
                    tblFeeCalculate_temp cta = new SchoolErp.tblFeeCalculate_temp();
                    cta.monthId = Convert.ToInt32(m.monthID);
                    cta.Months = m.month;
                    cta.monthlyAmount = a.courses;
                    cta.ActualPayableAmnt = a.style.Trim();
                    cta.SchoolID = SchoolID;
                    db.tblFeeCalculate_temp.Add(cta);

                }
                mnths.Add(month);
            }
            db.SaveChanges();
            var monthsindb = db.tblFeeCalculate_temp.Where(x => x.SchoolID == SchoolID).Select(x => x.monthId).ToList();
            foreach (long mon in monthsindb)
            {
                if (!mnths.Contains(mon))
                {
                    List<string> l = new List<string>();
                    l.Add(mon.ToString());
                    l.Add("FeeCalculate");
                    DeleteRecord(l);
                }
            }

            return 0;
        }






        [System.Web.Http.Route("api/FeesApi/saveFeeDetails")]
        [System.Web.Http.HttpPost]
        public fees saveFeeDetails(feecalculationdetails[] fee)
        {
            fees f = new Models.fees();
            try
            {
                foreach (var a in fee)
                {
                    tblFeeCalculate tfee = new tblFeeCalculate();
                    tfee.AcademicYear = a.AcademicYear;
                    tfee.monthId = a.MonthId;
                    tfee.Months = a.Month.Trim();
                    tfee.monthlyAmount = a.monthlyamt.Trim();
                    tfee.PaidAmount = a.amount;
                    tfee.duesAmount = a.duesAmount;
                    if (a.duesAmount.Trim() != "0")
                    {
                        tfee.feePaid = 0;
                    }
                    tfee.fldstudentID = a.studentId;
                    tfee.ActualPayableAmnt = a.ActualPayableAmnt;
                    tfee.dateCreated = DateTime.Now;
                    tfee.discountAmnt = a.discountAmnt;
                    tfee.discountPer = a.discountPer;
                    tfee.feeStructureID = a.feeStructureID;
                    tfee.SchoolID = a.SchoolID;
                    tfee.LateFeeFine = a.LateFeeFine;
                    db.tblFeeCalculates.Add(tfee);


                }
                db.SaveChanges();
                foreach (var a in fee)
                {
                    if (a.duesAmount.Trim() == "0")
                    {
                        var tfc = db.tblFeeCalculates.Where(x => x.fldstudentID == a.studentId && x.AcademicYear == a.AcademicYear
                        && x.monthId == a.MonthId).ToList();
                        tfc.ForEach(m => m.feePaid = 1);
                        db.SaveChanges();
                    }
                }

                f.StatusNm = "Record Successfully saved";
                f.Cat_ID = 1;
            }
            catch (Exception ex)
            {
                f.StatusNm = "Some error while saving";
                f.Cat_ID = -1;
                throw ex;
            }

            return f;
        }

        [System.Web.Http.Route("api/FeesApi/getFeeDetails")]
        [System.Web.Http.HttpPost]
        public tblFeeCalculate[] getFeeDetails(List<string> val)
        {
            long id = Convert.ToInt32(val[0]);
            int year = Convert.ToInt32(val[1]);
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<tblFeeCalculate> list = new List<tblFeeCalculate>();
            var result = db.tblFeeCalculates.SqlQuery("select * from tblFeeCalculate where AcademicYear=" + year + " and duesAmount=0 and fldstudentID=" + id).ToList();
            foreach (var a in result)
            {
                tblFeeCalculate tfee = new tblFeeCalculate();
                tfee.monthId = a.monthId;
                tfee.Months = a.Months;
                tfee.monthlyAmount = a.monthlyAmount;
                tfee.PaidAmount = a.PaidAmount;
                tfee.duesAmount = a.duesAmount;
                tfee.AcademicYear = a.AcademicYear;
                list.Add(tfee);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/FeesApi/SearchStudentsforFeeReceipts")]
        [System.Web.Http.HttpPost]
        public fees[] SearchStudentsforFeeReceipts(List<string> val)
        {
            List<fees> list = new List<fees>();
            Student[] arrayStudent = getAllStudentsDetails1(val);
            long dt = -1;
            string month = "-";
            int year = 0;
            string feeSt = "";
            var predicate = PredicateBuilder.True<tblFeeCalculate>();
            if (val[1] != "" && val[1] != "-1")
            {
                year = Convert.ToInt32(val[1]);
                predicate = predicate.And(x => x.AcademicYear == year);
            }
            if (val[4] != "" && val[4] != "-1")
            {
                long stdID = Convert.ToInt32(val[4]);
                predicate = predicate.And(x => x.fldstudentID == stdID);
            }
            if (val[5] != "" && val[5] != "-1")
            {
                dt = Convert.ToInt32(val[5]);
                predicate = predicate.And(x => x.monthId == dt);
                DateTime dtDate = new DateTime(2000, Convert.ToInt32(dt + 1), 1);
                month = dtDate.ToString("MMMM");
            }
            if (val[6] != "" && val[6] != "-1" && dt != -1)
            {
                feeSt = val[6].Trim();
            }

            var result = db.tblFeeCalculates.AsExpandable().Where(predicate).ToList();
            int count = 0;
            foreach (var s in arrayStudent)
            {
                var stFees = result.Where(x => x.fldstudentID == s.ID).ToList();

                if (stFees.Count > 0)
                {
                    foreach (var c in stFees)
                    {
                        fees f = new fees();
                        count++;
                        f.count = count;
                        f.std = s;
                        f.fcal = c;
                        list.Add(f);
                    }
                }
                else
                {
                    fees f = new fees();
                    tblFeeCalculate fc = new tblFeeCalculate();
                    count++;
                    f.count = count;
                    fc.Months = month;
                    fc.PaidAmount = "-";
                    fc.ActualPayableAmnt = "-";
                    f.std = s;
                    f.fcal = fc;
                    list.Add(f);
                }
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/FeesApi/GetMonthsBySessionAPP")]
        [System.Web.Http.HttpPost]
        public monthsAPP GetMonthsBySessionAPP(List<int> val)
        {
            monthsAPP obj = new monthsAPP();
            List<months> list = new List<months>();
            int yearID = 0;
            int avi = 0;
            foreach (var v in val)
            {
                yearID = v;
            }
            try
            {

                var result = db.tblAcademicYears.Where(x => x.ID == yearID && x.IsDeleted == null).SingleOrDefault();
                var dts = GetMonthsBetween(result.DateFrom, result.DateTo);
                months m0 = new months();
                m0.month = "--Select month--";
                m0.month_id = -1;
                list.Add(m0);
                foreach (var dt in dts)
                {
                    avi++;
                    months m = new months();
                    m.month = dt.ToString("MMMM");
                    m.month_id = dt.Month - 1;
                    list.Add(m);
                }
                if (avi != 0)
                {
                    obj.status = "200";
                    obj.message = "sucess";

                    obj.data = list;

                }
                else if (avi == 0)
                {
                    obj.status = "200";
                    obj.message = "No Data Found";
                    obj.data = list;

                }

            }
            catch
            {
                obj.status = "404";
                obj.message = "Something Went Wrong";

            }
            return obj;
        }



        [System.Web.Http.Route("api/FeesApi/GetMonthsBySession")]
        [System.Web.Http.HttpPost]
        public months[] GetMonthsBySession(List<int> val)
        {
            List<months> list = new List<months>();
            int yearID = 0;
            foreach (var v in val)
            {
                yearID = v;
            }
            try
            {
                var result = db.tblAcademicYears.Where(x => x.ID == yearID && x.IsDeleted == null).SingleOrDefault();
                var dts = GetMonthsBetween(result.DateFrom, result.DateTo);

                months m0 = new months();
                m0.month = "--Select month--";
                m0.month_id = -1;
                list.Add(m0);
                foreach (var dt in dts)
                {
                    months m = new months();
                    m.month = dt.ToString("MMMM");
                    m.month_id = dt.Month - 1;
                    list.Add(m);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        public static List<DateTime> GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                monthDiff -= 1;
            }

            List<DateTime> results = new List<DateTime>();
            for (int i = monthDiff; i >= 1; i--)
            {
                results.Add(to.AddMonths(-i));
            }

            return results;
        }
        //[System.Web.Http.Route("api/FeesApi/getStudentFeesDetailForReceipt")]
        //[System.Web.Http.HttpPost]
        //public fees getStudentFeesDetailForReceipt(List<int> val)
        //{
        //    int id = val[0];
        //    fees f = new Models.fees();
        //    var result = (from s in db.TBLStudents
        //                  join cl in db.tblCourses on s.ClassID equals cl.Id
        //                  join sec in db.tblSections on s.SectionID equals sec.Id
        //                  join c in db.tblFeeCalculates on s.ID equals c.fldId
        //                  where s.ID == id
        //                  select new
        //                  { s, c, cl, sec }).SingleOrDefault();
        //    if (result != null)
        //    {
        //        f.fcal = result.c;
        //        f._std = result.s;
        //        f.std = new Models.Student();
        //        f.std.Class = result.cl.CourseName;
        //        f.std.Section = result.sec.SectionName;
        //        f.std.PSAddress = Convert.ToDateTime(result.c.Dated).ToString("MMMM dd,yyyy");
        //    }
        //    return f;
        //}

        [System.Web.Http.Route("api/FeesApi/AssignFeeStructure")]
        [System.Web.Http.HttpPost]
        public Student AssignFeeStructure(Student st)
        {
            int i = 0;
            try
            {
                foreach (var s in st.rgDoclist)
                {
                    int SchoolID = Convert.ToInt32(st.SchoolID);
                    var exists = db.tblFeeStructureAssigns.SingleOrDefault(x => x.StudentID == s.ID && !x.isDeleted && x.isActive && x.SchoolID == SchoolID);
                    if (exists != null)
                    {
                        exists.FeeStructureID = st.ID;
                        tblFeeStructureAssign a = new tblFeeStructureAssign();
                        a.StudentID = s.ID;
                        a.FeeStructureID = st.ID;
                        a.DateCreated = DateTime.Now;
                        a.DateModified = DateTime.Now;
                        a.isActive = true;
                        a.isDeleted = false;
                        a.SchoolID = Convert.ToInt32(st.SchoolID);
                        db.SaveChanges();
                        st.StatusName = "Assigned Fee Structure  Updated for Student ";
                    }
                    else
                    {

                        tblFeeStructureAssign a = new tblFeeStructureAssign();
                        a.StudentID = s.ID;
                        a.FeeStructureID = st.ID;
                        a.DateCreated = DateTime.Now;
                        a.DateModified = DateTime.Now;
                        a.isActive = true;
                        a.isDeleted = false;
                        a.SchoolID = Convert.ToInt32(st.SchoolID);
                        db.tblFeeStructureAssigns.Add(a);
                        st.StatusName = "Fee Structure Assigned to the selected student";
                    }
                }
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                st.StatusName = "Some Errors";
            }
            return st;
        }

        //[System.Web.Http.Route("api/FeesApi/QuotationRPT")]
        //[System.Web.Http.HttpPost]
        //public Student QuotationRPT(Parameters param)
        //{

        //    Student st = new Models.Student();
        //    int a = Convert.ToInt32(param.val);
        //    Quotations s = db.Quotations.Where(x => x.Id == a).FirstOrDefault();

        //    //st.Id = s.Id;
        //    st.bill = "#Q-000";
        //    st.Id = s.Id;
        //    st.bill = st.bill + s.Id;
        //    st.Date = s.Date;
        //    st.Today = DateTime.Now;
        //    st.Tax = s.Vat;
        //    st.TotalTax = s.TotalTax;
        //    st.Shiping = s.ShippingCost;
        //    st.netTotal = s.NetTotal;
        //    st.GrandTotal = s.GrandTotal;
        //    st.TotalDiscount = s.TotalDiscount;
        //    st.Detail = s.Details;
        //    st.ExpDate = s.ExpiryDate;
        //    st.customer = Convert.ToInt32(s.CustomerId);

        //    if (s.CustomerId != -1)
        //    {
        //        long customer = s.CustomerId;
        //        Customers sec = db.Customers.Where(x => x.Id == customer).FirstOrDefault();
        //        st.customer1 = sec.Name;
        //        st.cphone = sec.Phone;
        //        st.cemail = sec.Email;
        //        st.cAddress = sec.Address;

        //    }
        //    if (s.CustomerId != -1)
        //    {
        //        long country = s.CustomerId;
        //        tblCountry cou = db.tblCountries.Where(x => x.CountryID == country).FirstOrDefault();
        //        st.ccountry = cou.CountryName;
        //    }
        //    if (s.CustomerId != -1)
        //    {
        //        long city = s.CustomerId;
        //        tblCity cou = db.tblCities.Where(x => x.Id == city).FirstOrDefault();
        //        st.ccity = cou.CityName;
        //    }
        //    if (s.Id != -1)
        //    {
        //        long quotation = s.Id;
        //        QuotationDetails quo = db.QuotationDetails.Where(x => x.Id == quotation).FirstOrDefault();
        //        st.Product = quo.ProductName;
        //        st.UnitPrice = quo.UnitPrice;
        //        st.Quantity = quo.Quantity;
        //        st.Discount = quo.Discount;

        //    }
        //    return st;
        //}



        [System.Web.Http.Route("api/FeesApi/sss")]
        [System.Web.Http.HttpPost]
        public Student sss(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            ServiceInvoices s = db.ServiceInvoices.Where(x => x.Id == a).FirstOrDefault();

            //st.Id = s.Id;
            st.bill = " #INV-000";
            st.Id = s.Id;
            st.bill = st.bill + s.Id;
            st.Date = s.Date;
            st.Tax = s.Vat;
            st.TotalTax = s.TotalTax;
            st.Shiping = s.ShippingCost;
            st.netTotal = s.NetTotal;
            st.GrandTotal = s.GrandTotal;
            st.TotalDiscount = s.TotalDiscount;
            st.Detail = s.Details;
            st.customer = Convert.ToInt32(s.CustomerId);

            if (s.CustomerId != -1)
            {
                long customer = s.CustomerId;
                Customers sec = db.Customers.Where(x => x.Id == customer).FirstOrDefault();
                st.customer1 = sec.Name;
                st.cphone = sec.Phone;
                st.cemail = sec.Email;
                st.cAddress = sec.Address;

            }
            if (s.CustomerId != -1)
            {
                long country = s.CustomerId;
                tblCountry cou = db.tblCountries.Where(x => x.CountryID == country).FirstOrDefault();
                st.ccountry = cou.CountryName;
            }
            if (s.CustomerId != -1)
            {
                long city = s.CustomerId;
                tblCity cou = db.tblCities.Where(x => x.Id == city).FirstOrDefault();
                st.ccity = cou.CityName;
            }
            if (s.CustomerId != -1)
            {
                long quotation = s.CustomerId;
                ServiceInvoiceDetails quo = db.ServiceInvoiceDetails.Where(x => x.Id == quotation).FirstOrDefault();
                st.Product = quo.ServiceName;
                st.UnitPrice = quo.UnitPrice;
                st.Quantity = quo.Quantity;
                st.Discount = quo.Discount;

            }
            return st;
        }



        [System.Web.Http.Route("api/FeesApi/FindStudentByVoucherID")]
        [System.Web.Http.HttpPost]
        public Student FindStudentByVoucherID(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            int Schoolid = Convert.ToInt32(param.val1);

            stdfee1 s = db.stdfee1.Where(x => x.VoucherID == a).FirstOrDefault();
            st.ID = Convert.ToInt32(s.StudentID);
            st.voucherid = s.VoucherID;
            st.FirstName = s.Student;
            st.RegNo = s.AdmNo;
            st.FatherName = s.Father;
            st.dated = s.Dated.Value;
            st.Amount = s.Amount.Value;
            st.AcademicYear = s.Sessionid.ToString();
            //st.Amount = AmtInWord(s.Amount).Value;
            st.DocNo = s.DocNo;
            st.RollNo = st.DocNo;
            st.Remarks = s.Remarks;
            st.ManualNo = s.manualno;
            st.Paymentby = s.Paymentby;
            st.SectionID = Convert.ToInt32(s.sem);
            st.FeeHeadID = Convert.ToInt32(s.FeeheadID);
            st.ClassID = Convert.ToInt32(s.CourseID);

            tblSchoolDetail school = db.tblSchoolDetails.Where(x => x.ID == Schoolid).FirstOrDefault();
            st.School = school.School;
            st.Address = school.Address + "(Phone:" + school.Phone + ")";
            st.School2 = school.LogoPic;

            if (s.sem != -1)
            {
                int sem = s.sem.Value;
                tblSection sec = db.tblSections.Where(x => x.Id == sem).FirstOrDefault();
                st.Section = sec.SectionName;
            }
            if (s.CourseID != -1)
            {
                int CourseID = s.CourseID.Value;
                tblCours cls = db.tblCourses.Where(x => x.Id == CourseID).FirstOrDefault();
                st.Class = cls.CourseName;
            }

            //if (s.FeeheadID != -1)
            //{
            //    int FeeheadID = s.FeeheadID.Value;
            //    tblFeeCategory cat = db.tblFeeCategories.Where(x => x.ID == FeeheadID).FirstOrDefault();
            //    st.FeeCategory = cat.FeeCategory;
            //}
            return st;
        }
    }
}
