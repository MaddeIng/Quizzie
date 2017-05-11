(function () {
    var quizHub = $.connection.quizHub;
    
    $.connection.hub.start()
        .done(function () {
            console.log("Hub started!");

            //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
            $("input").click(function (event) {
                quizHub.server.isCorrect(event.target.id)
                    .done(function (result) {  
                        var buttonClass = "";
                        if (result === true) {
                            buttonClass = "btn btn-success";
                        }
                        else {
                            buttonClass = "btn btn-danger";
                        }
                        //console.log(result + ' ' + event.target.id + ' ' + buttonClass + ' ' + event.target);
                        //$(event.target).flip();
                        $(event.target).removeClass("btn btn-info").addClass(buttonClass);
                        $("input").off("click");
                    })
                    .fail();
            });

        })
        .fail(function () { alert("Fail!"); });


})();