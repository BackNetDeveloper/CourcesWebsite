$(function () {
    function FireUpDatePicker()
    {
        const addMonths = 2;

        var currentDate = new Date();

        $('.datepicker').datepicker
            (
                {
                    dateFormat: "yy-mm-dd",

                    minDate: currentDate,

                    maxDate: AddSubtractMonth(currentDate, addMonths)
                }
            );
    }

    FireUpDatePicker();
});