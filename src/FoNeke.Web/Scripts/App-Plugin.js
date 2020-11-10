
(function($) {
    $.fn.GridAddLine = function() {
        var controllerName = $(this).attr("data-controller-name");
        $(this).on("click", function() {
            $(this).OpenModal("/" + controllerName + "/Create");
        });
    };
    $.fn.GridEditLine = function() {
        $(this).on("click", function() {
            var controllerName = $(this).attr("data-controller-name");
            var grid = $("#Grid" + controllerName).data("kendoGrid");
            var selectedItem = grid.dataItem(grid.select());

            $(this).OpenModal("/" + controllerName + "/Edit?Id=" + selectedItem.Id);
        });
    };


    $.fn.GridDeleteLine = function() {
        $(this).on("click", function () {
            var controllerName = $(this).attr("data-controller-name");
            var grid = $("#Grid" + controllerName).data("kendoGrid");
            var selectedItem = grid.dataItem(grid.select());

            $("<div>Delete selected row ?</div>").dialog({
                resizable: false,
                modal: true,
                title: "<div class='widget-header'><h4 class='smaller'><i class='icon-warning-sign red'></i> Delete selected row ?</h4></div>",
                title_html: true,
                buttons: [
                    {
                        html: "<i class='icon-trash bigger-110'></i>&nbsp; Delete",
                        "class": "btn btn-danger btn-xs",
                        click: function () {
                            grid.dataSource.remove(selectedItem);
                            grid.dataSource.read();
                            $(this).dialog("close");
                        }
                    },
                    {
                        html: "<i class='icon-remove bigger-110'></i>&nbsp; Cancel",
                        "class": "btn btn-xs",
                        click: function() {
                            $(this).dialog("close");
                        }
                    }
                ]
            });
        });
    };

    $.fn.OpenModal = function (url) {
        location.href = url;
        return;

        //$("<div></div>").
        //    load(url).
        //    dialog({
        //        modal: true,
        //        //title: "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='icon-ok'></i> jQuery UI Dialog</h4></div>",
        //        title_html: true,
        //        width: $(window).width() / 2 + ($(window).width() / 3),
        //        height: $(window).height() / 2 + ($(window).height() / 3),

        //        buttons: [
        //            {
        //                text: "Cancel",
        //                "class": "btn btn-xs",
        //                click: function() {
        //                    $(this).dialog("close");
        //                }
        //            },
        //            {
        //                text: "OK",
        //                "class": "btn btn-primary btn-xs",
        //                click: function () {
        //                    $("form", $(this)).submit();
        //                    $(this).dialog("close");
        //                }
        //            }
        //        ],
        //    });
    };
    
   
}(jQuery));