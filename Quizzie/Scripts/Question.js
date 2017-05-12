(function () {
    var quizHub = $.connection.quizHub;

    function handleInputClick(event) {
        quizHub.server.isCorrect($(this).data().answer)
            .done(function (result) {

                var buttonClass = "";
                if (result === true) {
                    buttonClass = "btn btn-success";
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

        console.log(question);

        $imageLink.html('<img src="' + question.ImageLink +'" />')
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

    $.connection.hub.start()
        .done(function () {
            console.log("Hub started!");
            // Tell the server to initialize us.
            quizHub.server.initialize();

            //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
            $("input").click();

        })
        .fail(function () { alert("Fail!"); });


})();