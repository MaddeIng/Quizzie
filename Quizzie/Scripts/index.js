$(document).ready(function () {

    var quizHub = $.connection.quizHub;

    $("#start-div #start-btn").click(function () {
        console.log("start");
        var playerName = $("#player-name").val();
        var accessCode = $("#access-code").val();


        $.connection.hub.start()
            .done(function () {
                console.log("Hub started!");
                
                console.log("Index Ready:\n Name: " + playerName + "\n Access code: " + accessCode);

                quizHub.server.validateStartOfQuiz(playerName, accessCode)
                    .done(function (isValid) {
                        console.log("Quiz exists: " + isValid);

                        if (isValid === false) {
                            $("#access-code").effect("shake").css("border-color", "red");
                        }
                        else {
                            $("body").effect("drop");
                            window.location.href = "../quiz/question";
                        }

                    })

                //quizHub.server.initialize(playerName, accessCode);

                //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
                //$("input").click();
                $("#answers input").click();

            })
            .fail(function () { alert("Fail!"); });


        //quizHub.server.initialize(playerName, accessCode);
    });
});