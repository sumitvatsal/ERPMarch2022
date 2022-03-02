function getDateFromCompleteDate(date) {   // return  2/23/2014
    return parseInt(date.getMonth())+1 + "/" + date.getDate() + "/" + date.getFullYear();
}
 

function getTimeFromDateAMPMFormat(date) {   // return format  : 12:23 AM
    var trueTime = "";
    var AMPM = "AM";
    var hrs = date.getHours();
    var mins = date.getMinutes();

    if (parseInt(hrs) > 11) {
        AMPM = "PM";
    }
    if (parseInt(hrs) > 12) {
        hrs = parseInt(hrs) % 12;
    }
    if (parseInt(hrs) < 10) {
        hrs = "0" + hrs;
    }
    if (parseInt(mins) < 10) {
        mins = "0" + parseInt(mins);
    }
    trueTime = hrs + ":" + mins + " " + AMPM;
    return trueTime;
 }
 


