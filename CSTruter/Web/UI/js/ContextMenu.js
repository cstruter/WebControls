CSTruter.ContextMenu = {
    Init: function () {
        $(".cstruter_contextmenu").on("contextmenu", function (e) {
            return false;
        });
        $(document).bind("mouseup", function (e) {
            if (e.which == 1) {
                $(".cstruter_contextmenu").hide();
            }
        });
    },
    Handler: function (e, selector) {
        $(".cstruter_contextmenu").hide();
        var menu = $(selector);
        var winWidth = $(window).width();
        var winHeight = $(window).height();
        if ((e.pageX + menu.outerWidth()) > winWidth)
            menu.css("left", winWidth - menu.outerWidth());
        else
            menu.css("left", e.pageX);

        if ((e.pageY + menu.outerHeight()) > winHeight)
            menu.css("top", winHeight - menu.outerHeight());
        else
            menu.css("top", e.pageY);
        menu.show();
        return false;
    },
    Hover: function (sender, foreColor, backColor) {
        $(sender).css('color', foreColor);
        $(sender).css('background-color', backColor);
    }
};