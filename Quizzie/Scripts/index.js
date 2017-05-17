$(document).ready(function () {
    
    $.ajax({
        url: "/Quiz/GetPartialViewIndex", type: "GET",
        success: function (result) {
            $("#main-body").html(result);
            $.loader.open();
            SetIndexPage();
        }
    });

    var quizHub = $.connection.quizHub;

    function SetIndexPage() {
        $.loader.close();
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
        quizHub.server.initialize(accessCode);

        $("body").toggle("drop");
        var isCorrect;
    }

    function handleInputClick(event) {
        quizHub.server.isCorrect($(this).data().answer)
            .done(function (result) {

                var buttonClass = "";
                if (result === true) {
                    buttonClass = "btn btn-success";

                    //point++;
                    var point = 0;
                }
                else {
                    buttonClass = "btn btn-danger";
                }
                $(event.target).removeClass("btn btn-info").addClass(buttonClass);
                $("input").off("click");
            })
            .fail(function (event) {
                console.log("FAAAIL");
                console.log(arguments);
            });
    }

    quizHub.client.setQuestion = function (question, answers) {

        var $loading = $("#loading");
        var $currentQuestion = $("#currentQuestion");
        var $question = $("#question");
        var $answers = $("#answers");
        var $imageLink = $("#image-link");
        var $questionBody = $("#question-body");

        //$currentQuestion.toggle("drop", function () {

        $imageLink.attr("src", question.ImageLink);
        $question.text(question.Question);
        $answers.empty();
        for (var i = 0; i < answers.length; i++) {
            (function (index) {
                var btn = $("<input/>", {
                    type: 'button',
                    class: 'btn btn-info',
                    value: answers[index].Answer,
                    click: handleInputClick,
                    "data-answer": answers[index].ID
                });
                $answers.append(btn);
            })(i);
        }
        $loading.hide();
        $currentQuestion.show();
        //}) 
    };

    quizHub.client.calculateFinalScore = function () {
        $.ajax({
            url: "/Quiz/GetPartialViewResults", type: "GET",
            success: function (result) {
                $("#main-body").html(result);

                quizHub.server.calculateIndividualScore()
                    .done(function (score) {
                        console.log(score);
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

});