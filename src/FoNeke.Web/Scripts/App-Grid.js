
var loadGrid = function (gridSelector, pagerSelector, controller, colNames, colModel, gridData,height) {

    //var colModel = [{ name: 'id', index: 'id', width: 60, sorttype: "int", editable: true },
    //{ name: 'sdate', index: 'sdate', width: 90, editable: true, sorttype: "date", unformat: pickDate },
    //{ name: 'name', index: 'name', width: 150, editable: true, editoptions: { size: "20", maxlength: "30" } },
    //{ name: 'stock', index: 'stock', width: 70, editable: true, edittype: "checkbox", editoptions: { value: "Yes:No" }, unformat: aceSwitch },
    //{ name: 'ship', index: 'ship', width: 90, editable: true, edittype: "select", editoptions: { value: "FE:FedEx;IN:InTime;TN:TNT;AR:ARAMEX" } },
    //{ name: 'note', index: 'note', width: 150, sortable: false, editable: true, edittype: "textarea", editoptions: { rows: "2", cols: "10" } }];
    //var colNames = ['ID', 'Last Sales', 'Name', 'Stock', 'Ship via', 'Notes'];

    //if (colModel!=undefined) {
    //    colModel.push({
    //        name: 'myac',
    //        index: '',
    //        width: 80,
    //        fixed: true,
    //        sortable: false,
    //        resize: false,
    //        formatter: 'actions',
    //        formatoptions: {
    //            keys: true,

    //            delOptions: { recreateForm: true, beforeShowForm: beforeDeleteCallback },
    //            //editformbutton:true, editOptions:{recreateForm: true, beforeShowForm:beforeEditCallback}
    //        }
    //    });
    //}

    jQuery(gridSelector).jqGrid({
        //direction: "rtl",

        url: '/' + controller + '/Get',
        datatype: "json",
        
        //data: gridData,
        //datatype: "local",

        colNames: colNames,
        colModel: colModel,

        viewrecords: true,
        rowNum: 10,
        rowList: [10, 20, 30],
        pager: pagerSelector,
        altRows: true,
        //toppager: true,

        multiselect: true,
        //multikey: "ctrlKey",
        multiboxonly: true,

        loadComplete: function() {
            var table = this;
            setTimeout(function() {
                styleCheckbox(table);

                updateActionIcons(table);
                updatePagerIcons(table);
                enableTooltips(table);
            }, 0);
        },

        editurl: "/" + controller + "/Edit",
        caption: "jqGrid with inline editing",
        height:height,
        autowidth: true
    });

    //enable search/filter toolbar
    //jQuery(gridSelector).jqGrid('filterToolbar',{defaultSearch:true,stringResult:true})




   



    //var selr = jQuery(grid_selector).jqGrid('getGridParam','selrow');


};

function MakeGridNavButtons(gridSelector, pagerSelector) {
    var popUpHeight = $("#cartouche-dialog").height();
    if (popUpHeight>0) {
        $(gridSelector).height(popUpHeight);
    }
    
    $(window).bind('resize', function () {
        var width = popUpHeight >0 ? $("#cartouche-dialog").width() : $(".page-content").width();
        $(gridSelector).setGridWidth(width);
    }).trigger('resize');
 
    

    //navButtons
    jQuery(gridSelector).jqGrid('navGrid', pagerSelector,
        {
            //navbar options
            edit: true,
            editicon: 'icon-pencil blue',
            add: true,
            addicon: 'icon-plus-sign purple',
            del: true,
            delicon: 'icon-trash red',
            search: true,
            searchicon: 'icon-search orange',
            refresh: true,
            refreshicon: 'icon-refresh green',
            view: true,
            viewicon: 'icon-zoom-in grey',
        },
        {
            //edit record form
            //closeAfterEdit: true,
            zIndex:2500,
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                //form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
                //style_edit_form(form);
            }
        },
        {
            //new record form
            closeAfterAdd: true,
            recreateForm: true,
            viewPagerButtons: false,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
                style_edit_form(form);
            }
        },
        {
            //delete record form
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                if (form.data('styled')) return false;

                form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />');
                style_delete_form(form);

                form.data('styled', true);
            },
            onClick: function (e) {
                alert(1);
            }
        },
        {
            //search form
            recreateForm: true,
            afterShowSearch: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />')
                style_search_form(form);
            },
            afterRedraw: function () {
                style_search_filters($(this));
            },
            multipleSearch: true,
            /**
         multipleGroup:true,
         showQuery: true
         */
        },
        {
            //view record form
            recreateForm: true,
            beforeShowForm: function (e) {
                var form = $(e[0]);
                form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />')
            }
        }
    );
}

//switch element when editing inline

function aceSwitch(cellvalue, options, cell) {
    setTimeout(function () {
        $(cell).find('input[type=checkbox]')
            .wrap('<label class="inline" />')
            .addClass('ace ace-switch ace-switch-5')
            .after('<span class="lbl"></span>');
    }, 0);
}

//enable datepicker

function pickDate(cellvalue, options, cell) {
    setTimeout(function () {
        $(cell).find('input[type=text]')
            .datepicker({ format: shortDatePattern, autoclose: true });
    }, 0);
}

function style_edit_form(form) {
    //enable datepicker on "sdate" field and switches for "stock" field
    form.find('input[name=sdate]').datepicker({ format: shortDatePattern, autoclose: true })
        .end().find('input[name=stock]')
        .addClass('ace ace-switch ace-switch-5').wrap('<label class="inline" />').after('<span class="lbl"></span>');

    //update buttons classes
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove(); //ui-icon, s-icon
    buttons.eq(0).addClass('btn-primary').prepend('<i class="icon-ok"></i>');
    buttons.eq(1).prepend('<i class="icon-remove"></i>')

    buttons = form.next().find('.navButton a');
    buttons.find('.ui-icon').remove();
    buttons.eq(0).append('<i class="icon-chevron-left"></i>');
    buttons.eq(1).append('<i class="icon-chevron-right"></i>');
}

function style_delete_form(form) {
    var buttons = form.next().find('.EditButton .fm-button');
    buttons.addClass('btn btn-sm').find('[class*="-icon"]').remove(); //ui-icon, s-icon
    buttons.eq(0).addClass('btn-danger').prepend('<i class="icon-trash"></i>');
    buttons.eq(1).prepend('<i class="icon-remove"></i>')
}

function style_search_filters(form) {
    form.find('.delete-rule').val('X');
    form.find('.add-rule').addClass('btn btn-xs btn-primary');
    form.find('.add-group').addClass('btn btn-xs btn-success');
    form.find('.delete-group').addClass('btn btn-xs btn-danger');
}

function style_search_form(form) {
    var dialog = form.closest('.ui-jqdialog');
    var buttons = dialog.find('.EditTable')
    buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'icon-retweet');
    buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'icon-comment-alt');
    buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'icon-search');
}

function beforeDeleteCallback(e) {
    var form = $(e[0]);
    if (form.data('styled')) return false;

    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
    style_delete_form(form);

    form.data('styled', true);
}

function beforeEditCallback(e) {
    var form = $(e[0]);
    form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
    style_edit_form(form);
}


//it causes some flicker when reloading or navigating grid
//it may be possible to have some custom formatter to do this as the grid is being created to prevent this
//or go back to default browser checkbox styles for the grid

function styleCheckbox(table) {
    /**
        $(table).find('input:checkbox').addClass('ace')
        .wrap('<label />')
        .after('<span class="lbl align-top" />')


        $('.ui-jqgrid-labels th[id*="_cb"]:first-child')
        .find('input.cbox[type=checkbox]').addClass('ace')
        .wrap('<label />').after('<span class="lbl align-top" />');
    */
}


//unlike navButtons icons, action icons in rows seem to be hard-coded
//you can change them like this in here if you want

function updateActionIcons(table) {
    /**
        var replacement = 
        {
            'ui-icon-pencil' : 'icon-pencil blue',
            'ui-icon-trash' : 'icon-trash red',
            'ui-icon-disk' : 'icon-ok green',
            'ui-icon-cancel' : 'icon-remove red'
        };
        $(table).find('.ui-pg-div span.ui-icon').each(function(){
            var icon = $(this);
            var $class = $.trim(icon.attr('class').replace('ui-icon', ''));
            if($class in replacement) icon.attr('class', 'ui-icon '+replacement[$class]);
        })
        */
}

//replace icons with FontAwesome icons like above

function updatePagerIcons(table) {
    var replacement =
    {
        'ui-icon-seek-first': 'icon-double-angle-left bigger-140',
        'ui-icon-seek-prev': 'icon-angle-left bigger-140',
        'ui-icon-seek-next': 'icon-angle-right bigger-140',
        'ui-icon-seek-end': 'icon-double-angle-right bigger-140'
    };
    $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
        var icon = $(this);
        var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

        if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
    });
}

function enableTooltips(table) {
    $('.navtable .ui-pg-button').tooltip({ container: 'body' });
    $(table).find('.ui-pg-div').tooltip({ container: 'body' });
}

function onGridDatabound(parameters) {
    SetGridDoubleClick(parameters);
}

function onGridSelectionChanged(e) {
    var selected = $.map(this.select(), function (item) {
        return $(item).text();
    });
    
    if (selected.length>0) {
        $("#" + this.table.context.id + "EditBtn").removeClass("disabled");
        $("#" + this.table.context.id + "DeleteBtn").removeClass("disabled");

    } else {
        $("#" + this.table.context.id + "EditBtn").addClass("disabled");
        $("#" + this.table.context.id + "DeleteBtn").addClass("disabled");
    }
}

function SetGridOptions(gridElement) {
    resizeGrid(gridElement);
    $(window).resize(function () {
        resizeGrid(gridElement);
    });

};

function resizeGrid(gridElement) {
    var dataArea = gridElement.find(".k-grid-content"),
        gridHeight = gridElement.innerHeight(),
        otherElements = gridElement.children().not(".k-grid-content"),
        otherElementsHeight = 0;
    otherElements.each(function () {
        otherElementsHeight += $(this).outerHeight();
    });
    dataArea.height($(window).height() - (otherElementsHeight * 3));
}


function SetGridDoubleClick(parameters) {
    var grid = parameters.sender.element;
    var parent = grid.parent();
    grid.delegate("tbody>tr:not(.k-grouping-row)", "dblclick", function (e) {
        var row = e.currentTarget;
        var selectedItem = grid.data("kendoGrid").dataItem(row);
        if (selectedItem == undefined) {
            return;
        }
        
        switch (parent.attr("id")) {
        case "main-content":
            //console.log("id : " + selectedItem.Id + " - display : " + selectedItem.Display);
            window.location.href = "/" + grid.attr("id").substring(4) + "/Edit/" + selectedItem.Id;
            break;
        case "cartouche-dialog":
        default:
            //console.log("id : " + selectedItem.Id + " - display : " + selectedItem.Display);
            $("#" + parent.attr("data-cartouche-id") + "_Id").val(selectedItem.Id);
            $("#" + parent.attr("data-cartouche-id") + "_Display").val(selectedItem.Display);
            grid.parent().dialog('close');
            break;
        }
    });
}


function GridDisplayBaseEntity(cellvalue, options, rowobject) {
    return cellvalue==null?"":cellvalue.Display;
}