(function ($) {
    "use strict";

    var counter = 0;
    $("body").on("click", ".relation-add button", function () {
        var parent = $(this).closest(".relation"),
            textbox = parent.find(".relation-add input"),
            container = parent.find(".relation-list ul"),
            name = parent.data("relation-name"),
            idName = name + "_add" + counter;
        counter++;

        if (textbox.val()) {
            container.append("<li><input type='checkbox' checked='checked' id='" + idName + "' name='" + name + "Add' value='" + textbox.val() + "' /><label for='" + idName + "'>" + textbox.val() + "</label></li>");
            textbox.val("");
        }
    });
})(jQuery);