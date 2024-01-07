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
window.importantDates = window.importantDates || {};

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

checkLeapYear = (year) => {
    return (year % 4 === 0 && year % 100 !== 0 && year % 400 !== 0)
        || (year % 100 === 0 && year % 400 === 0)
};

getFebruaryDays = (year) => {
    return checkLeapYear(year) ? 29 : 28
};

let calendar = document.querySelector('.calendar');
const monthNames = ['Януари', 'Февруари', 'Март', 'Април', 'Май', 'Юни', 'Юли', 'Август', 'Септември', 'Октомври', 'Ноември', 'Декември'];
let monthPicker = document.querySelector('#month-picker');

monthPicker.onclick = () => {
    monthList.classList.add('show')
};

generateCalendar = (date) => {

    let month = date.getMonth();
    let year = date.getFullYear();

    let calendarDay = document.querySelector('.calendar-day');
    calendarDay.innerHTML = '';

    let calendarHeaderYear = document.querySelector('#year');
    let daysOfMonth = [31, getFebruaryDays(year), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    let todaysDate = new Date();

    monthPicker.innerHTML = monthNames[month];
    calendarHeaderYear.innerHTML = year;

    let firstDay = new Date(year, month, 1);
    
    for (let i = 1; i <= daysOfMonth[month] + firstDay.getDay() - 1; i++) {

        let day = i - firstDay.getDay() + 1;
        let dayElement = document.createElement('div');
        let current = new Date(year, month, day);

        if (i >= firstDay.getDay()) {

            dayElement.innerHTML = day
            dayElement.innerHTML += `<span></span>
                             <span></span>
                             <span></span>
                             <span></span>`

            if (day === todaysDate.getDate() && year === todaysDate.getFullYear() && month === todaysDate.getMonth()) {
                dayElement.classList.add('todaysDate')
            }

            if (day === targetDate.getDate() && year === targetDate.getFullYear() && month === targetDate.getMonth()) {
                dayElement.classList.add('targetDate')
            } else {
                dayElement.classList.add('calendarDayHover')
            }            
            
            var shoppingListCount = window.importantDates[`${formatDate(current, "yyyy-MM-dd")}`]
            if (shoppingListCount) {
                dayElement.classList.add('importantDate')
                dayElement.innerHTML += `<span class="shopping-list-count">${shoppingListCount}</span>`
            }     
                    
        }

        calendarDay.appendChild(dayElement)
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