jQuery(function($) {
    //Jquery plugin override
    //jQuery.validator.addMethod(
    //    'date',
    //    function(value, element, params) {
    //        if (this.optional(element)) {
    //            return true;
    //        }
    //        var result;
    //        try {
    //            $.datepicker.parseDate(shortDatePattern, value);
    //            result = true;
    //        } catch(err) {
    //            result = false;
    //        }
    //        return result;
    //    },
    //    ''
    //);
    ////End Jquery plugin override




    var mydate = $('.date-picker');
    if (mydate.length > 0 && mydate[0].type !== 'date') { //if browser doesn't support "date" input
        $(mydate).datepicker({
            //options
            autoclose: true,
            todayHighlight: true,
            format: shortDatePattern,
            language: twoLetterISOLanguageName,
        }).next().on(ace.click_event, function() {
            $(this).prev().focus();
        });
    }
})