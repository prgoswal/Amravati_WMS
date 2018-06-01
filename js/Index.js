$(document).ready(function () {

    /// For Create Popover \\\
    function PopOverError(id, plac, msg) {
        try {
            $(id).popover({
                title: 'Error!',
                trigger: 'manual',
                placement: plac,
                content: function () {
                    var message = msg; //"Allow Numbers Only!";
                    return message;
                }
            });
            $(id).popover("show");
        } catch (e) { }
    }

    //// For Valid Date Allow \\\\\\
    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });

    $(".setDate.datepicker").datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true, }).datepicker("setDate", "0");

    $('.datepicker').blur(function (e) {
        try {

            var id = ('#' + this.id);
            var date = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (date.length <= 0 || date == '' || date == undefined) {
                return false;
            }
            if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');
            } else {
                PopOverError(id, 'top', 'Invalid Date.');
                $(id).focus();
            }
        } catch (e) { }
    });
    $('.datepicker').keypress(function (e) {
        try {
            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == "/") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 31) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Day!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 5) {
                        var month = $(id).val().substring(3, 6);
                        if (month < 1 || month > 12) {
                            var corr = $(id).val().replace("/" + month, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Month!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });

    //// Disable Pasting IN Text Box \\\\
    $('input.datepicker').bind('copy paste', function (e) {
        e.preventDefault();
    });

    $("input").attr("autocomplete", "off");
    $(".datepicker").attr("autocomplete", "off");
});