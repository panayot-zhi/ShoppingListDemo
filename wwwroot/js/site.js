// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Dark Mode Toggle
//document.querySelector('.dark-mode-switch').onclick = () => {
//    document.querySelector('section.calendar-container').classList.toggle('dark');
//    document.querySelector('section.calendar-container').classList.toggle('light');
//};

window.currentDate = window.currentDate || new Date();
window.targetDate = window.targetDate || new Date();

const padZero = (value) => (value < 10 ? `0${value}` : `${value}`);

function formatDate(inputDate, format) {
    if (!inputDate) return '';

    const padZero = (value) => (value < 10 ? `0${value}` : `${value}`);
    const parts = {
        yyyy: inputDate.getFullYear(),
        MM: padZero(inputDate.getMonth() + 1),
        dd: padZero(inputDate.getDate()),
        HH: padZero(inputDate.getHours()),
        hh: padZero(inputDate.getHours() > 12 ? inputDate.getHours() - 12 : inputDate.getHours()),
        mm: padZero(inputDate.getMinutes()),
        ss: padZero(inputDate.getSeconds()),
        tt: inputDate.getHours() < 12 ? 'AM' : 'PM'
    };

    return format.replace(/yyyy|MM|dd|HH|hh|mm|ss|tt/g, (match) => parts[match]);
}

// Check Year
isCheckYear = (year) => {
    return (year % 4 === 0 && year % 100 !== 0 && year % 400 !== 0)
        || (year % 100 === 0 && year % 400 === 0)
};

getFebDays = (year) => {
    return isCheckYear(year) ? 29 : 28
};

let calendar = document.querySelector('.calendar');
const monthNames = ['Януари', 'Февруари', 'Март', 'Април', 'Май', 'Юни', 'Юли', 'Август', 'Септември', 'Октомври', 'Ноември', 'Декември'];
let monthPicker = document.querySelector('#month-picker');

monthPicker.onclick = () => {
    monthList.classList.add('show')
};

// Generate Calendar
generateCalendar = (date) => {

    let month = date.getMonth();
    let year = date.getFullYear();

    let calendarDay = document.querySelector('.calendar-day');
    calendarDay.innerHTML = '';

    let calendarHeaderYear = document.querySelector('#year');
    let daysOfMonth = [31, getFebDays(year), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    let todaysDate = new Date();

    monthPicker.innerHTML = monthNames[month];
    calendarHeaderYear.innerHTML = year;

    let firstDay = new Date(year, month, 0);

    for (let i = 0; i <= daysOfMonth[month] + firstDay.getDay() - 1; i++) {
        let day = document.createElement('div')
        
        if (i >= firstDay.getDay()) {

            day.innerHTML =  i - firstDay.getDay() + 1
            day.innerHTML += `<span></span>
                             <span></span>
                             <span></span>
                             <span></span>`

            if (i - firstDay.getDay() + 1 === todaysDate.getDate() && year === todaysDate.getFullYear() && month === todaysDate.getMonth()) {
                day.classList.add('todaysDate')
            }

            if (i - firstDay.getDay() + 1 === targetDate.getDate() && year === targetDate.getFullYear() && month === targetDate.getMonth()) {
                day.classList.add('targetDate')
                //day.classList.add('importantDate')
            } else {
                day.classList.add('calendarDayHover')
            }
        }

        calendarDay.appendChild(day)
    };
};

let monthList = calendar.querySelector('.month-list');
monthNames.forEach((e, index) => {
    let month = document.createElement('div')
    month.innerHTML = `<div>${e}</div>`
    month.onclick = () => {
        monthList.classList.remove('show')
        currentDate.setMonth(index)        
        generateCalendar(currentDate)
    }
    monthList.appendChild(month)
});

document.querySelector('#prev-year').onclick = () => {    
    currentDate.setFullYear(currentDate.getFullYear() - 1)
    generateCalendar(currentDate)
};

document.querySelector('#next-year').onclick = () => {
    currentDate.setFullYear(currentDate.getFullYear() + 1)
    generateCalendar(currentDate)
};