/// <reference path="jquery-1.4.4-vsdoc.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

$(function ($) {
    //Jquery DatePicker Validator
    $.validator.addMethod(
        'date',
        function (value, element, params) {
            if (this.optional(element)) {
                return true;
            }
            var result;
            try {
                $.datepicker.parseDate(shortDatePattern, value);
                result = true;
            } catch (err) {
                result = false;
            }
            return result;
        },
        ''
    );
    //End Jquery DatePicker Validator

    $.validator.addMethod('requiredif',
    function (value, element, parameters) {
        var id = '#' + parameters['dependentproperty'];
        var name = parameters['dependentproperty'].replace('_', '.');
        // get the target value (as a string, 
        // as that's what actual value will be)
        var targetvalue = parameters['targetvalue'];
        targetvalue =
          (targetvalue == null ? '' : targetvalue).toString();

        // get the actual value of the target control
        // note - this probably needs to cater for more 
        // control types, e.g. radios
        var control = $(id);
        var controltype = control.attr('type');
        var actualvalue =
            controltype === 'checkbox' ?
            control.prop('checked').toString() :
            control.val();

        //var actualvalue;
        //if (controltype === 'checkbox') {
        //    control = $('[name="' +name + '"][type="hidden"]');
        //    actualvalue = control.val();
        //}


        // if the condition is true, reuse the existing 
        // required field validator functionality
        if (targetvalue === actualvalue)
            return $.validator.methods.required.call(
              this, value, element, parameters);

        return true;
    },''
);

    $.validator.unobtrusive.adapters.add(
        'requiredif',
        ['dependentproperty', 'targetvalue'],
        function (options) {
            options.rules['requiredif'] = {
                dependentproperty: options.params['dependentproperty'],
                targetvalue: options.params['targetvalue']
            };
            options.messages['requiredif'] = options.message;
        });


})