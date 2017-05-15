$(document).ready(function () {


    $.ajax({
        url: "/Quiz/GetPartialViewIndex", type: "GET",
        success: function (result) {
            $("#main-body").html(result);
            debug();
        }
    });

    var quizHub = $.connection.quizHub;

    console.log("start");



    function debug() {
        $("#start-div #start-btn").click(function () {

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
                                //$("body").effect("drop");
                                //window.location.href = "../quiz/question";
                                $.ajax({
                                    url: "/Quiz/GetPartialViewQuestion", type: "GET",
                                    success: function (result) {
                                        $("#main-body").html(result);
                                        SetupQuizLogic();
                                    }
                                });
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
    }
});

$("#login").click(function () {
    window.location.href = "../quiz/login";

});
