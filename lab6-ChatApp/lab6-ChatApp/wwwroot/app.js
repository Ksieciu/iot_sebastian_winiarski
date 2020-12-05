(function () {
    var btnSend = $('#btnSend');
    var textboxMsg = $('#textboxMsg');
    var listChat = $('#listChat');
    var username = $('#user-name').val();

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    $(btnSend).click(function () {
        var msg = $(textboxMsg).val();
        connection.invoke('SendMessage', {
            username: username,
            message: msg
        });
    });

    connection.on('ReceivedMessage', function (obj) {
        var htmlElement = '<li>'
            + '<span class="font-weight-bold">['
            + obj.formattedTimeStamp + '] ' + obj.username + '</span>: '
            + obj.message
            + '</li>';

        $(textboxMsg).val('');

        $(listChat).prepend(htmlElement);
    });

    connection.on('UserSignedIn', function (obj) {
        var htmlElement = '<li>'
            + '<span class="font-weight-bold text-success">'
            + obj + ' - connected to the room.</span>'
            + '</li>';

        $(textboxMsg).val('');

        $(listChat).prepend(htmlElement);
    });


    connection.start().then(function () {
        console.log("connected as " + username);

        connection.invoke('SignInUser', username);
    });


})();