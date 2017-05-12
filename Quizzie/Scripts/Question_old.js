//(function () {
//    var quizHub = $.connection.quizHub;

//    $.connection.hub.start()
//        .done(function () {
//            console.log("Hub started!");
                        
//            quizHub.server.getQuiz()
//                .done(function (result) {
//                    console.log("G");
//                    //var json = JSON.parse(result)
//                    console.log(result);
//                });



//                //.done(function (result) {
//                //    var jsonObject = JSON.parse(result);
//                //    console.log(jsonObject);
//                //    $("#quiz-title").append(jsonObject.title);                    
//                //})
//                //.fail(
//                //    alert("fail")
//                //);

//            //Hanterar klick på svarsknappar och hämtar svar (true/false) från databas 
//            $("input").click(function (event) {
//                quizHub.server.isCorrect(event.target.id)
//                    .done(function (result) {
//                        var buttonClass = "";
//                        if (result === true) {
//                            buttonClass = "btn btn-success";
//                        }
//                        else {
//                            buttonClass = "btn btn-danger";
//                        }
//                        //console.log(result + ' ' + event.target.id + ' ' + buttonClass + ' ' + event.target);
//                        //$(event.target).flip();
//                        $(event.target).removeClass("btn btn-info").addClass(buttonClass);
//                        $("input").off("click");
//                    })
//                    .fail();
//            });

//        })
//        .fail(function () { alert("Fail!"); });


//})();