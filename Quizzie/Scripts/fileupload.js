function getFile() {
    document.getElementById("upfile").click();
}

function sub(obj) {
    var file = obj.value;
    var fileName = file.split("\\");
    document.getElementById("bild").innerHTML = fileName[fileName.length - 1];
    document.myForm.submit();
    event.preventDefault();
}

//$(document).ready(function () {

//    $("#upfile").change(function () {
//        var File = this.files;

//        if (File && File[0]) {
//            ReadImage(File[0]);
//        }
//    });
//});

//var ReadImage = function (file) {

//    var reader = new FileReader;
//    var image = new Image;

//    reader.readAsDataURL(file);
//    reader.onload(function (_file) {
//        image.src = _file.target.result;
//        image.onload = function () {


//            $("#targetImg").attr("src", _file.target.result);
//        };
//    });

//}