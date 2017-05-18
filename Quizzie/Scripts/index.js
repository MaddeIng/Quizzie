$(document).ready(function () {

    $.ajax({
        url: "/Quiz/GetPartialViewIndex", type: "GET",
        success: function (result) {
            $("#main-body").html(result);
            //$.loader.open();
            SetIndexPage();
        }
    });

    var quizHub = $.connection.quizHub;

    function SetIndexPage() {
        //$.loader.close();
        $("#start-div #start-btn").click(function () {
            var playerName = $("#player-name").val();
            var accessCode = $("#access-code").val();

            $('#access-code').loader(); //Loading spinner

            $.connection.hub.start()
                .done(function () {
                    quizHub.server.validateStartOfQuiz(playerName, accessCode)
                        .done(function (isValid) {
                            console.log("Quiz exists: " + isValid);

                            $.loader.close(true); //Loading spinner

                            if (isValid === false) {
                                $("#access-code").effect("shake").css("border-color", "red");
                            }
                            else {
                                $("body").toggle("drop", function () {
                                    $.ajax({
                                        url: "/Quiz/GetPartialViewQuestion", type: "GET",
                                        success: function (result) {
                                            $("#main-body").html(result);
                                            SetupQuiz(accessCode);
                                        }
                                    });
                                });
                            }
                        });

                    //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
                    $("#answers input").click();

                })
                .fail(function () { alert("Fail!"); });
        });
    }

    function SetupQuiz(accessCode) {
        console.log("SetupQuiz");
        quizHub.server.initialize(accessCode)
            .done(function () {
                $("body").toggle("drop");
            });
    }

    function handleInputClick(event) {

        //$.loader.open({ title: "Väntar på samtliga svar" }); //TS
        var x = event.data.id;

        console.log(x);

        quizHub.server.isCorrect(x)
            .done(function (result) {

                result = JSON.parse(result);

                console.log("Result: " + result);

                var buttonClass = "";

                if (result.answer === true) {
                    buttonClass = "btn btn-correct";
                }
                else {
                    buttonClass = "btn btn-fail";
                }

                var testbutton = $("input[data-answer='" + result.ID + "']");

                console.log(testbutton);

                $("input[data-answer='" + result.ID + "']").removeClass("btn btn-inQuiz").addClass(buttonClass);
                $("input").off("click");

            })
            .fail(function (result) {
                console.log("FAAAIL");
                console.log(arguments);
            });
    }

    quizHub.client.setQuestion = function (question, answers) {

        console.log("setQuestion");


        var $currentQuestion = $("#currentQuestion");
        var $question = $("#question");
        var $answers = $("#answers");
        var $imageLink = $("#image-link");
        var $questionBody = $("#question-body");

        var noDataAnswers = function () {
            $("#one").removeAttr("data-answer");
            $("#two").removeAttr("data-answer");
            $("#three").removeAttr("data-answer");
            $("#four").removeAttr("data-answer");
        };




        //$currentQuestion.toggle("drop", function () {

        console.log("currentQuestion");
        $imageLink.attr("src", question.ImageLink);
        $question.text(question.Question);

        $("#one").val(answers[0].Answer).attr("data-answer", answers[0].ID).removeClass("btn btn-correct").removeClass("btn btn-fail").addClass("btn btn-inQuiz");
        $("#two").val(answers[1].Answer).attr("data-answer", answers[1].ID).removeClass("btn btn-correct").removeClass("btn btn-fail").addClass("btn btn-inQuiz");
        $("#three").val(answers[2].Answer).attr("data-answer", answers[2].ID).removeClass("btn btn-correct").removeClass("btn btn-fail").addClass("btn btn-inQuiz");
        $("#four").val(answers[3].Answer).attr("data-answer", answers[3].ID).removeClass("btn btn-correct").removeClass("btn btn-fail").addClass("btn btn-inQuiz");

        $("#one").click({ id: answers[0].ID }, handleInputClick);
        $("#two").click({ id: answers[1].ID }, handleInputClick);
        $("#three").click({ id: answers[2].ID }, handleInputClick);
        $("#four").click({ id: answers[3].ID }, handleInputClick);

        $currentQuestion.show();
        //})
    };

    quizHub.client.calculateFinalScore = function () {
        //$.loader.close();
        $.ajax({
            url: "/Quiz/GetPartialViewResults", type: "GET",
            success: function (result) {
                $("#main-body").html(result);
                //$.loader.close();

                quizHub.server.calculateIndividualScore()
                    .done(function (score) {
                        console.log(score);
                        $.loader.close();

                    })
                    .fail(function () {
                        console.log("Failed to load score");
                    });
            }
        });

    };
    quizHub.client.addChatMessage = function (message) {
        console.log(message);
    };

    quizHub.client.justDoIt = function (finalResults) {
        $("#score").append(finalResults.Name + " " + finalResults.Score + "<br>");
    };

    quizHub.client.showUsers = function (name) {
        $("#usersParticipating").addClass("sofiabox");
        $("#usersParticipating").append("<div id =" + name + ">" + name + "</div>");
    };

    quizHub.client.changeAppearance = function (name) {
        var user = name;
        $("#" + user).addClass("appearence");
    };

    quizHub.client.removeAppearance = function () {
        $("#usersParticipating").children().removeClass("appearence");
    };

});