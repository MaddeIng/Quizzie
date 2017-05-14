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

$(document).ready(function () {

    $("#upfile").change(function () {

        var File = this.files;

        if (File && File[0]) {
            ReadImage(File[0]);
        }

    });
});

var ReadImage = function (file) {

    var reader = new FileReader;
    var image = new Image;

    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {

            var heigth = this.heigth;
            var width = this.width;
            var type = file.type;
            var size = ~~(file.size / 1024) + "KB";

            $("#targetImg").attr('src', _file.target.result);
            $("#description").text("Radera och välj annan bild om du inte är nöjd");
            $("#imgPreview").show();

        }
    }
}

var ClearPreview = function () {
    $("#upfile").val('');
    $("#description").text('');
    $("#imgPreview").hide();
}
