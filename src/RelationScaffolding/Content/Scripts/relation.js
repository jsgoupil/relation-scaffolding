(function ($) {
    "use strict";

    var counter = 0;
    $("body")
        .on("click", ".relation-add button", function () {
            var parent = $(this).closest(".relation"),
                textbox = parent.find(".relation-add input"),
                container = parent.find(".relation-list ul"),
                name = parent.data("relation-name"),
                id = parent.data("relation-id"),
                edit = parent.data("relation-edit"),
                idName = name + "_add" + counter;
            counter++;

            if (textbox.val()) {
                var item = $("<li>" +
                        "<input type='hidden' name='" + name + "[" + idName + "]." + id + "' value='' />" +
                        "<input type='hidden' name='" + name + "[" + idName + "]." + edit + "' />" +
                        "<input type='checkbox' checked='checked' id='" + idName + "' name='" + name + ".index' value='" + idName + "' />" +
                        " <label for='" + idName + "'>" + textbox.val() + "</label>" +
                    "</li>");
                item.find("input[type=hidden]:eq(1)").val(textbox.val());
                container.append(item);
                textbox.val("");
            }
        })
        .on("keypress", ".relation-add input", function (evt) {
            if (evt.keyCode === 13) {
                $(this).closest(".relation-add").find("button").click();
                evt.preventDefault();
            }
        });
})(jQuery);