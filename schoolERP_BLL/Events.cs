using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolERP_BLL
{
  public  class EventsType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Extra10 { get; set; }
        public int SchoolID { get; set; }
        public string  School { get; set; }
    }


    public class EventsDetailsAPP
    {
        public bool status { get; set; }
        public string message { get; set; }

        public List< EventsDetails1 > data { get; set; }
    }

    public class EventsDetails1
    {
       
        public string EventName { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public string StartEndDate { get; set; }
        public string StartEndTime { get; set; }
        public string ClassId { get; set; }
        public string Section { get; set; }
        public int SchoolID { get; set; }
      

    }



    public class EventsDetails
    {
        public string Id { get; set; }       
        public int eventId { get; set; }
        public string EventName { get; set; }
        public string Holiday { get; set; }
        public string  EventType { get; set; }

        public string Description { get; set; }
        //add
        public DateTime StartDatechange { get; set; }
        public DateTime EndDatechange { get; set; }
        //
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InchargName { get; set; }
        public string EventFor { get; set; }
        public string  ClassId { get; set; }
        public string[] Class { get; set; }
        public string Section { get; set; }
        public string Department { get; set; }
        public string Extra10 { get; set; }

        public string starttime { get; set; }
        public string endtime { get; set; }

        public string startdated { get; set; }
        public string enddated { get; set; }
        public long eventTypeId { get; set; }
        public string EventForId { get; set; }
        public string classSection { get; set; }
        public int SchoolID { get; set; }
        public string School { get; set; }
        public long depId { get; set; }
        public bool SendSms { get; set; }

    }

    public class TaskDetailsAPP
    {
        public bool status { get; set; }
       public string message { get; set; }
        public List < TaskDetails1 > data { get; set; }
    }
    public class TaskDetails1
    {
        public string sno { get; set; }
        public string Id { get; set; }

        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string TaskDate { get; set; }
       
        public string Class { get; set; }
        public string Section { get; set; }
        
        public string Status { get; set; }
        
        public string StudentName { get; set; }
       
        public string color { get; set; }
        public string priorityColor { get; set; }
        public int SchoolID { get; set; }

      


    }
    public class TaskDetails {
        public string sno { get; set; }
        public string Id { get; set; }

        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string TaskDate { get; set; }
        public string UserType { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string[] Student { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public string[] Employee { get; set; }
        public string StudentName { get; set; }
        public string EmployeeName { get; set; }
        public string color { get; set; }
        public string priorityColor { get; set; }
        public int SchoolID { get; set; }
       
        public string  School { get; set; }


    }
    public class NoticeboardDetailsforapp
    {
        public bool status { get; set; }
        public string message { get; set; }

        public List<NoticeboardDetails1> data { get; set; }
    }

    public class NoticeboardDetails1
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string NoticeDate { get; set; }
        public string Desc { get; set; }
        public string NoticeFile { get; set; }
        public string userType { get; set; }
        public string Status { get; set; }
        public string color { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string DateTimeCreated { get; set; }
        public string DateCreated { get; set; }

       

        //public List<NoticeboardDetails> data { get; set; }
       

    }
    public class NoticeboardDetails
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime NoticeDate { get; set; }
        public string datetime1 { get; set; }
        public string noticeda { get; set; }
        public string sNoticeDate { get; set; }
        public string Desc { get; set; }
        public string userType { get; set; }
        public string NoticeFile { get; set; }
        public string Status { get; set; }
        public bool SendSms { get; set; }
        
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Extra { get; set; }
        public string blanValue { get; set; }

        public string color { get; set; }

        public string datetime { get; set; }


        public DateTime StDt { get; set; }
        public DateTime eDt { get; set; }
        public String School { get; set; }
        //public int SchoolID { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public List<NoticeboardDetails> data { get; set; }

        public string LoginUser { get; set; }


    }

 

}
