 $(function () {

    /* initialize the external events
     -----------------------------------------------------------------*/
    function init_events(ele) {
      ele.each(function () {

        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
        // it doesn't need to have a start or end
        var eventObject = {
          title: $.trim($(this).text()) // use the element's text as the event title
        }

        // store the Event Object in the DOM element so we can get to it later
        $(this).data('eventObject', eventObject)

        // make the event draggable using jQuery UI
        $(this).draggable({
          zIndex        : 1070,
          revert        : true, // will cause the event to go back to its
          revertDuration: 0  //  original position after the drag
        })

      })
    }

    init_events($('#external-events div.external-event'))

    /* initialize the calendar
     -----------------------------------------------------------------*/
    //Date for the calendar events (dummy data)
    var date = new Date()
    var d    = date.getDate(),
        m    = date.getMonth(),
        y = date.getFullYear()






    $.ajax({

        type: 'POST',
        async: false,
        cache: false,
        contentType: 'application/json',
        data: "{}",
        dataType: 'json',
        url: "/api/EmployeeAPI/getTeacherDiaryByEmployeeId",
        cache: false,
         success: function (data) {
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                buttonText: {
                    today: 'today',
                    month: 'month',
                    week: 'week',
                    day: 'day'
                },



                //Random default events

                //events: [
                //  {
                //      title: 'All Day Event',
                //      start: new Date(y, m, 1),
                //      backgroundColor: '#f56954', //red
                //      borderColor: '#f56954' //red
                //  },
                //  {
                //      title: 'Long Event',
                //      start: new Date(y, m, d - 5),
                //      end: new Date(y, m, d - 2),
                //      backgroundColor: '#f39c12', //yellow
                //      borderColor: '#f39c12' //yellow
                //  },
                //  {
                //      title: 'Meeting',
                //      start: new Date(y, m, d, 10, 30),
                //      allDay: false,
                //      backgroundColor: '#0073b7', //Blue
                //      borderColor: '#0073b7' //Blue
                //  },
                //  {
                //      title: 'Lunch',
                //      start: new Date(y, m, d, 12, 0),
                //      end: new Date(y, m, d, 14, 0),
                //      allDay: false,
                //      backgroundColor: '#00c0ef', //Info (aqua)
                //      borderColor: '#00c0ef' //Info (aqua)
                //  },
                //  {
                //      title: 'Birthday Party',
                //      start: new Date(y, m, d + 1, 19, 0),
                //      end: new Date(y, m, d + 1, 22, 30),
                //      allDay: false,
                //      backgroundColor: '#00a65a', //Success (green)
                //      borderColor: '#00a65a' //Success (green)
                //  },
                //  {
                //      title: 'Click for Google',
                //      start: new Date(y, m, 28),
                //      end: new Date(y, m, 29),
                //      url: 'http://google.com/',
                //      backgroundColor: '#3c8dbc', //Primary (light-blue)
                //      borderColor: '#3c8dbc' //Primary (light-blue)
                //  }
                //],
              //  editable: true,
                editable: false,
                lang: 'en-IN',
                eventLimit: 1,
                eventLimitText: 'More',
                weekMode: 'liquid',
                // editable: true,
                events: $.map(data.d, function (item, i) {
                    var event = new Object();
                    event.id = item.EventID;
                    event.start = new Date(item.StartDate);
                    event.end = new Date(item.EndDate);
                    event.title = item.EventName;
                    event.url = item.Url;
                    event.ImageType = item.ImageType;
                    event.backgroundColor = item.Color;
                    
                    return event;
                }), eventRender: function (event, eventElement) {
                    if (event.ImageType) {
                        if (eventElement.find('span.fc-event-time').length) {
                            eventElement.find('span.fc-event-time').before($(GetImage(event.ImageType)));
                        } else {
                            eventElement.find('span.fc-event-title').before($(GetImage(event.ImageType)));
                        }
                    }
                },
                loading: function (bool) {
                    if (bool) $('#loading').show();
                    else $('#loading').hide();
                },
                eventClick: function (events) {
                    
                },
                droppable: true, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject')

                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject)

                    // assign it the date that was reported
                    copiedEventObject.start = date
                    copiedEventObject.allDay = allDay
                    copiedEventObject.backgroundColor = $(this).css('background-color')
                    copiedEventObject.borderColor = $(this).css('border-color')

                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#calendar').fullCalendar('renderEvent', copiedEventObject, true)
                    var end = new Date(date).addHours(2);
                    var tempColor = $.trim((copiedEventObject.backgroundColor).replace("rgb(", "").replace(")", "")).split(',');
                    var color = rgbToHex(tempColor[0], tempColor[1], tempColor[2]);
                    var startDate = getDateFromCompleteDate(date._d);
                    var startTime = getTimeFromDateAMPMFormat(date._d);
                    var endDate = getDateFromCompleteDate(end);
                    var endTime = getTimeFromDateAMPMFormat(end);

                    var obj = new Object();
                    obj.Id = 1;
                    obj.color = color;
                    obj.startDate = startDate;
                    obj.startTime = startTime;
                    obj.endDate = endDate;
                    obj.endTime = endTime;
                    obj.Title = copiedEventObject.title;
                  

                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        data: "{'sd':"+JSON.stringify(obj)+"}",
                        url: "WebForm1.aspx/saveTeacherDiary",
                        dataType: "json",
                        success: function (data) {

                        }
                    })
                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove()
                    }

                }
            })
        }
    });







    /* ADDING EVENTS */
    var currColor = '#3c8dbc' //Red by default
    //Color chooser button
    var colorChooser = $('#color-chooser-btn')
    $('#color-chooser > li > a').click(function (e) {
      e.preventDefault()
      //Save color
      currColor = $(this).css('color')
      //Add color effect to button
      $('#add-new-event').css({ 'background-color': currColor, 'border-color': currColor })
    })
    $('#add-new-event').click(function (e) {
        e.preventDefault()
        alert("rahul");
      //Get value and make sure it is not null
      var val = $('#new-event').val()
      if (val.length == 0) {
        return
      }

      //Create events
      var event = $('<div />')
      event.css({
        'background-color': currColor,
        'border-color'    : currColor,
        'color'           : '#fff'
      }).addClass('external-event')
      event.html(val)
      $('#external-events').prepend(event)

      //Add draggable funtionality
      init_events(event)

      //Remove event from text input
      $('#new-event').val('')
    })
 })

 function GetImage(type) {
     if (type == 0) {
         return "<br/><img src = 'Styles/Images/attendance.png' style='width:24px;height:24px'/><br/>"
     }
     else if (type == 1) {
         return "<br/><img src = 'Styles/Images/not_available.png' style='width:24px;height:24px'/><br/>"
     }
     else
         return "<br/><img src = 'Styles/Images/not_available.png' style='width:24px;height:24px'/><br/>"
 }