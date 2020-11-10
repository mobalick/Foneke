jQuery(function($) {
    
    $(".cartouche-search").on(ace.click_event, function (e) {
        e.preventDefault();
        var controller = $(e.target).attr("data-cartouche-entity");
        $('#cartouche-dialog').attr("data-cartouche-id", $(e.target).attr("data-cartouche-id"));
        $('#cartouche-dialog').attr("data-cartouche-entity", $(e.target).attr("data-cartouche-entity"));


        $('#cartouche-dialog')
            .removeClass('hide')
			.load('/' + controller + '/Cartouche/')
			.dialog({
			    modal: true,
			    title: "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='icon-ok'></i> jQuery UI Dialog</h4></div>",
			    title_html: true,
			    width: $(window).width() / 2, //+ ($(window).width() / 3),
			    height: $(window).height() / 2,//+ ($(window).height() / 3),

			    buttons: [
                    {
                        text: "Cancel",
                        "class": "btn btn-xs",
                        click: function () {
                            $(this).dialog("close");
                        }
                    },
                    {
                        text: "OK",
                        "class": "btn btn-primary btn-xs",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
			    ],
			});
    });

    var autoCompleteCache = {};
    $(".cartouche-field").autocomplete({
        minLength: 2,
        change: function (event, ui) {
            if (ui.item == null) {
                var value = autoCompleteCache[$(event.target).attr("id") + $(event.target).val()] != null ?
                    autoCompleteCache[$(event.target).attr("id") + $(event.target).val()][0] : null;

                if (value != null) {
                    $("#" + $(event.target).attr("data-cartouche-id")).val(value.Id);
                    $(event.target).val(value.Display);
                } else {
                    $("#" + $(event.target).attr("data-cartouche-id")).val("");
                    $(event.target).val("");
                }

            } else {
                $("#" + $(event.target).attr("data-cartouche-id")).val(ui.item.id);
            }

        },
        source: function (request, response) {
            var term = this.element.attr("id") + request.term;
            if (term in autoCompleteCache) {
                response($.map(autoCompleteCache[term], function (obj) {
                    return {
                        label: obj.Display,
                        id: obj.Id,
                        value: obj.Display
                    };
                }));
            }

            $.getJSON("/" + this.element.attr("data-cartouche-entity") + "/SearchByDisplay/", request, function (data, status, xhr) {
                autoCompleteCache[term] = data;

                response($.map(data, function (obj) {
                    return {
                        label: obj.Display,
                        id: obj.Id,
                        value: obj.Display
                    };
                }));

            });
        }
    });

})