function initFullCalendar(jobs, dotNetHelper) {
    document.addEventListener('DOMContentLoaded', function () {
        var calendarEl = document.getElementById('calendar');
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            timeZone: 'Europe/Vilnius',
            locale: 'lt',
            eventTimeFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            },
            displayEventTime: false,
            buttonText: {
                today: 'Šiandien',
                month: 'Mėnuo',
                week: 'Savaitė',
                day: 'Diena',
                list: 'Sąrašas'
            },
            events: function (info, successCallback, failureCallback) {
                dotNetHelper.invokeMethodAsync('GetEvents')
                    .then(events => {
                        successCallback(JSON.parse(events));
                    })
                    .catch(error => {
                        failureCallback(error);
                    });
            }
        });

        calendar.render();
    });
}
