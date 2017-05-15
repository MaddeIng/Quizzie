var quizHub = $.connection.quizHub;
function SetupQuizLogic() {
    console.log("question_1");

    quizHub.server.initialize("9999");

    console.log("question_2");

    $("#question").text("Testar");

    var isCorrect;
    var point = 0;
}
function handleInputClick(event) {
    quizHub.server.isCorrect($(this).data().answer)
        .done(function (result) {

            var buttonClass = "";
            if (result === true) {
                buttonClass = "btn btn-success";

                point++;
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

quizHub.client.quizLengthFinished = function (word) {
    console.log(word);
    console.log("help");
    window.location.href = "../quiz/results";
};

quizHub.client.addChatMessage = function (message) {
    console.log(message);
};

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

