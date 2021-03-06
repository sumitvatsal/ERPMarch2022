

var MyApp = (function () {

    var _hub = null;
    var _hubUrl = 'https://localhost:44317/signalr/hubs';
    var meeting_id = '';
    var user_id = '';


    function init(uid, mid) {
        user_id = uid;
        meeting_id = mid;

        $('#meetingname').text(meeting_id);
        $('#me h4').text(user_id + '(Me)');
        document.title = user_id;

        SignalServerEventBinding();
        EventBinding();
    }

    function SignalServerEventBinding() {
        // Set up the SignalR connection
        $.connection.hub.logging = true;

        _hub = $.connection.chatHub;
        $.connection.hub.url = _hubUrl;

        var serverFn = function (data, to_connid) {
            _hub.server.exchangeSDP(data, to_connid);
        };

        _hub.client.reset = function () {
            location.reload();
        }

        _hub.client.exchangeSDP = async function (data, from_connid) {
            //alert(from_connid);
            await WrtcHelper.ExecuteClientFn(data, from_connid);
        };

        _hub.client.informAboutNewConnection = function (other_user_id, connId) {
            AddNewUser(other_user_id, connId);
            WrtcHelper.createNewConnection(connId);
        };

        _hub.client.informAboutConnectionEnd = function (connId) {
            $('#' + connId).remove();
            WrtcHelper.closeExistingConnection(connId);
        };

        _hub.client.showChatMessage = function (from, message, time) {
            var div = $("<div>").text(from + '(' + time + '):' + message);
            $('#messages').append(div);
        }

        $.connection.hub.start(function () {
            console.log('connected to signal server.');
        }).done(function () {

            WrtcHelper.init(serverFn, $.connection.hub.id);

            if (user_id != "" && meeting_id != "") {
                _hub.server.connect(user_id, meeting_id).done(function (other_users) {
                    $('#divUsers .other').remove();
                    if (other_users) {
                        for (var i = 0; i < other_users.length; i++) {
                            AddNewUser(other_users[i].user_id, other_users[i].connectionId);
                            WrtcHelper.createNewConnection(other_users[i].connectionId);
                        }
                    }
                    $(".toolbox").show();
                    $('#messages').show();
                    $('#divUsers').show();
                });
            }
        });
    }

    function EventBinding() {
        $('#btnResetMeeting').on('click', function () {
            _hub.server.reset();
        });

        $('#btnsend').on('click', function () {
            _hub.server.sendMessage($('#msgbox').val());
            $('#msgbox').val('');
        });

        $('#divUsers').on('dblclick', 'video', function () {
            this.requestFullscreen();
        });
    }

    function AddNewUser(other_user_id, connId) {
        var $newDiv = $('#otherTemplate').clone().addClass('content');
        $newDiv = $newDiv.attr('id', connId).addClass('other');
        $newDiv.find('h4').text(other_user_id);
        $newDiv.find('video').attr('id', 'v_' + connId);
        $newDiv.find('audio').attr('id', 'a_' + connId);
       
        $newDiv.show();
        $('#divUsers').append($newDiv);
    }

    return {

        _init: function (uid, mid) {
            init(uid, mid);
        }

    };

}());