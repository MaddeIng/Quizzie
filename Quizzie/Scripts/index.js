$(document).ready(function () {
    $("#start-div #start-btn").click(function () {
        console.log("start");
        var playerName = $("#player-name").val();
        var accessCode = $("#access-code").val();

        console.log("Name: " + playerName + "\ Access code: " + accessCode);

        $.connection.hub.start()
            .done(function () {
                console.log("Hub started!");
                // Tell the server to initialize us.

                //quizHub.server.initialize("Hej");

                //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
                //$("input").click();
                $("#answers input").click();

            })
            .fail(function () { alert("Fail!"); });


        //quizHub.server.initialize(playerName, accessCode);
    });
});