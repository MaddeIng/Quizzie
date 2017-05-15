$(document).ready(function () {


    $.ajax({
        url: "/Quiz/GetPartialViewIndex", type: "GET",
        success: function (result) {
            $("#main-body").html(result);
            SetIndexPage();
        }
    });

    var quizHub = $.connection.quizHub;

    console.log("start");

    function SetIndexPage() {
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
                                $("body").toggle("drop");
                                $.ajax({
                                    url: "/Quiz/GetPartialViewQuestion", type: "GET",
                                    success: function (result) {
                                        $("#main-body").html(result);
                                        SetupQuiz(accessCode);
                                    }
                                });
                            }

                        })

                    //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
                    //$("input").click();
                    $("#answers input").click();

                })
                .fail(function () { alert("Fail!"); });
        });
    }

    function SetupQuiz(accessCode) {
        quizHub.server.initialize(accessCode);

        console.log("initialized: " + accessCode);
        $("body").toggle("drop");
        var isCorrect;
        //var point = 0;
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
                $("#score").empty().append("Din poäng:" + point);
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

        console.log(question);

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
    }

    quizHub.client.quizLengthFinished = function (word) {
        console.log(word);
        console.log("help");
        window.location.href = "../quiz/results";
    };

    quizHub.client.addChatMessage = function (message) {
        console.log(message);
    };
});

