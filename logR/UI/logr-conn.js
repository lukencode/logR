
$(function () {

    var log = $.connection.logRHub;

    log.addLog = function (log) {
        $(".empty").remove();
        $('#logTemplate').tmpl(log).prependTo("#logList");
    };

    $.connection.hub.start()
});