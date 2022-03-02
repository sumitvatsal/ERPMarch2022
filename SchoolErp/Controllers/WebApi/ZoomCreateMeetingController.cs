using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;


//using System;



namespace SchoolErp.Controllers.WebApi
{
    public class ZoomCreateMeetingController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();

        
        [System.Web.Http.Route("api/ZoomCreateMeeting/CreateMeeting")]
       // [System.Web.Http.AcceptVerbs("GET","POST")]
        [System.Web.Http.HttpPost]
        public string CreateMeeting(List<string> val)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            //var userid = val[0];
            //var CDate = val[1];
            //var SubjectId = val[2];
            //var STime = val[3];
            //var Duration = val[4];
            //var SchoolId = val[5];
            //var Class = val[6];
            //var Section = val[7];
            //var TeacherId = val[8];

            // int ID = Convert.ToInt32(val[6]);
            var msg = "";
            try
            {
                string Datef = Convert.ToDateTime(val[1]).ToString("yyyy-mm-dd") + "T" + TimeTo24Hrs(val[3]).TimeOfDay;

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var apiSecret = "4F4724Xvbvvt9ghNv8chnXhH8QJRV3Ke7Ifq";
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "6idOirBlSLe3LO4xDfShAw",
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var client = new RestClient("https://api.zoom.us/v2/users/priyarajput25mar@gmail.com/meetings");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                // request.AddJsonBody(new { topic = "Meeting with Pooja", duration = "10", start_time = "2022-02-10T20:35:00", type = "2" });
                request.AddJsonBody(new { topic = "Meeting", duration = val[4], start_time = Datef, type = "2" });

                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
                RestSharp.RestClient rest = new RestClient();
                RestResponse restResponse = (RestResponse)client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                var jObject = JObject.Parse(restResponse.Content);

                string Host = (string)jObject["start_url"];
                string meetid = (string)jObject["id"];
                string password = (string)jObject["password"];
                string uuid = (string)jObject["uuid"];
                string Join = (string)jObject["join_url"];
                string Code = Convert.ToString(numericStatusCode);


                ZommDetail zd = new ZommDetail();

                //var userid = val[0];
                //var CDate = val[1];
                //var SubjectId = val[2];
                //DateTime STime = TimeTo24Hrs(val[3]);
                //var Duration = val[4];
                //var SchoolId = val[5];
                //var Class = val[6];
                //var Section = val[7];
                //var TeacherId = val[8];
                //DateTime Sttdtt = timeTo24Hrs(Convert.ToDateTime(val[3]));




                zd.InsertUserId = val[0];
                zd.Date = Convert.ToDateTime(val[1]);
                zd.SubjectID = Convert.ToInt32(val[2]);
                zd.STime = TimeTo24Hrs(val[3]).TimeOfDay;
                zd.Duration = Convert.ToInt32(val[4]);
                zd.SchoolID = Convert.ToInt32(val[5]);
                zd.ClassID = Convert.ToInt32(val[6]);
                zd.SectionID = Convert.ToInt32(val[7]);
                zd.TeacherID = Convert.ToInt32(val[8]);
                zd.InsertDate = DateTime.Now;
                zd.MeetingID = meetid;
                zd.Password = password;
                db.ZommDetail.Add(zd);
                db.SaveChanges();
                msg = "Meeting Create Successfully";






            }
            catch (Exception ex)
            {
                // msg = "Some error!! process unsuccessful";
                // msg = "error";
                throw ex;
            }


            return msg;
        }

        public DateTime TimeTo24Hrs(string TimeFormat)
        {
            DateTime dt = DateTime.Parse(TimeFormat);
            return dt;
        }


        [System.Web.Http.Route("api/ZoomCreateMeeting/ViewStructure")]
        [System.Web.Http.HttpPost]
        public List<ZommDetail> ViewStructure(List<string> aa)
        {
           

                List<ZommDetail> list = new List<ZommDetail>();
                var SchoolId = Convert.ToInt32(aa[2]);
                int Type = Convert.ToInt32(aa[1]);
                var userid = aa[0];
                int Sid = Convert.ToInt32(aa[3]);

             try
            {
                var cond = "";

                if (Type == 3)
                {
                    if (aa[0] != null)
                    {
                        var check = db.tblEmployees.Where(x => x.UserID == userid).FirstOrDefault();

                        cond = cond + "and z.TeacherID='" + check.Id + "'";
                    }

                }

                if (Type == 4)
                {
                    if (aa[0] != null)
                    {
                        var check = db.TBLStudents.Where(x => x.ID == Sid).FirstOrDefault();

                        cond = cond + "and z.ClassID='" + check.ClassID + "'" + "and z.SectionID='" + check.SectionID + "'" + "and convert(varchar,z.Date,111) >= convert(varchar, getdate(), 111)";
                    }

                }

                sqlHelper obj = new sqlHelper();
                {

                    DataTable dt = obj.getDataTable(@"select z.ZID,z.MeetingID,z.Password,z.Date,e.FirstName + ' ' + e.MiddleName +' '+ e.LastName as Teacher, 
                                                  c.CourseName, s.SectionName,sb.Subject,z.STime,z.Duration from ZommDetail z 
                                                  left join tblEmployee e on z.TeacherID =e.Id
                                                  left join tblCourses c on z.ClassID=c.Id
                                                  left join tblSections s on s.Id=z.SectionID
                                                  left join tblSubject Sb on sb.ID = z.SubjectID
                                                  where z.SchoolId='" + SchoolId + "'  " + cond + "");

                    foreach (DataRow dr in dt.Rows)
                    {
                        ZommDetail usr = new ZommDetail();
                        usr.ZID = Convert.ToInt32(dr["ZID"].ToString());
                        usr.MeetingID = dr["MeetingID"].ToString();
                        usr.Password = dr["Password"].ToString();
                        usr.Dt = dr["Date"].ToString();
                        usr.Teacher = dr["Teacher"].ToString();
                        usr.Class = dr["CourseName"].ToString();
                        usr.Section = dr["SectionName"].ToString();
                        usr.Subject = dr["Subject"].ToString();
                        usr.Time = Convert.ToDateTime(dr["STime"].ToString()).ToString("hh:mm tt");
                        usr.Duration = Convert.ToInt32(dr["Duration"].ToString());
                        list.Add(usr);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

    }

}
