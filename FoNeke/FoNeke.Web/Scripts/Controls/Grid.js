

$(function () {
    $(document).on('click', '.grid-select-row', function (ele) {
        var selectedIds = $("#" + $(ele.target).attr("data-grid-entity-name") + "SelectedIds");
        var $cb = $(this);

        //Get Current Selected Values
        var selectedVals = [];
        var selectedRowIds = selectedIds.val();
        if (selectedRowIds != "") {
            selectedVals = selectedRowIds.split(',');
        }

        var $row = $cb.parents("[data-uid]").first();
        var rowId = $cb.attr('data-ui-id');
        if ($cb.is(':checked')) {
            //$row.addClass('k-state-selected');
            selectedVals.push(rowId);
        } else {
            //$row.removeClass('k-state-selected');
            selectedVals = _.without(selectedVals, rowId);
        }

        //Set selected values to a custom data attribute on the grid
        selectedIds.val(selectedVals);
    });


    $('.scrollable').each(function () {
        var $this = $(this);
        $(this).ace_scroll({
            size: $this.data('height') || 250,
            //styleClass: 'scroll-left scroll-margin scroll-thin scroll-dark scroll-light no-track scroll-visible'
        });
    });
});

var gridDataBound = function (e) {
    var $gridTable = e.sender.table;
    var $checkAll = $("#grid-select-all", $($gridTable));
    var entityName = $checkAll.attr("data-grid-entity-name");
    var $selectedIds = $("#"+entityName+"SelectedIds");

    //Mark any selected rows as selected (persists selections from page to page)
    var selectedRowIds = $selectedIds.val();
    if (selectedRowIds != null) {
        var selectedRowIdArray = selectedRowIds.split(',');
        var $visibleRows = $gridTable.find('[role="row"]');
        $($visibleRows).each(function () {
            var $row = $(this);
            var rowID = $row.find('.grid-select-row').attr("data-ui-id");
            if (_.contains(selectedRowIdArray, rowID) || $checkAll.is(':checked')) {
                //$row.addClass('k-state-selected');
                $row.find('.grid-select-row').attr('checked', 'checked');
            }
        });
    }
};

var checkAll = function(ele) {
    var state = $(ele).is(':checked');
    var gridName = $(ele).attr("data-grid-entity-name");
    var $grid = $('#grid' + gridName);

    var kendoGrid = $grid.data().kendoGrid;
    var $selectedIds = $("#" + gridName + "SelectedIds").val("");

    if (state) {
        $('.grid-select-row', $grid).attr("checked", "checked");
        kendoGrid.select(kendoGrid.tbody.find(">tr"));
        $selectedIds.val('All');
    } else {
        $('.grid-select-row', $grid).removeAttr("checked");
        kendoGrid.clearSelection();
        $selectedIds.val("");
        kendoGrid.refresh();
    }
};