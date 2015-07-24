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

    $("body")
        .on("click", ".relation-multiple button", function () {
            var parent = $(this).closest(".relation"),
                relationMultiple = $(this).closest(".relation-multiple"),
                relationList = relationMultiple.find(".relation-list"),
                container = parent.find(".relation-list ul"),
                name = parent.data("relation-name"),
                idName = name + "_add" + counter;
            counter++;

            var content = "";
            var label = [];
            relationList.each(function (i, r) {
                var $r = $(r);
                var id = $r.data('relation-id');
                var select = $r.find("select");
                content += "<input type='hidden' name='" + name + "[" + idName + "]." + id + "' value='" + select.val() + "' />" ;
                label.push(select.children("option").filter(":selected").text());
            });

            var item = $("<li>" +
                    content +
                    "<a href='javascript:;' data-relation-delete='true'>X</a>" +
                    "<input type='hidden' id='" + idName + "' name='" + name + ".index' value='" + idName + "' />" +
                    " <label for='" + idName + "'>" + label.join(" - ") + "</label>" +
                "</li>");
            container.append(item);
        })
        .on("click", ".relation [data-relation-delete]", function () {
            var parent = $(this).closest("li");

            parent.remove();
        });
})(jQuery);