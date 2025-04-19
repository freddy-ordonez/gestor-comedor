$(document).ready(function () {
    loadChartInventory();
    loadChartActivities();
    loadChartMoneydDonation();
});

function loadChartInventory() {
    $.ajax({
        url: urlBase + "/inventories", 
        method: 'GET',
        success: function (res, textStatus) {
            const now = new Date();
            const labels = [];
            const quantities = [];
            const colors = [];
            const inventories = textStatus === "nocontent" ? [] : res.data;

            inventories.forEach(item => {
                labels.push(item.productName);
                quantities.push(item.quantity);

                const expiryDate = new Date(item.expiryDate);
                const diffDays = (expiryDate - now) / (1000 * 60 * 60 * 24);

                if (expiryDate < now) {
                    colors.push('rgba(255, 99, 132, 0.7)');
                } else if (diffDays < 7) {
                    colors.push('rgba(255, 206, 86, 0.7)');
                } else {
                    colors.push('rgba(75, 192, 192, 0.7)');
                }
            });

            new Chart($('#myInventoryChart'), {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Cantidad',
                        data: quantities,
                        backgroundColor: colors,
                        borderColor: 'rgba(0, 0, 0, 0.2)',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: false }
                    },
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
        error: function (err) {
        }
    });
}

function loadChartActivities() {
    $.ajax({
        url: urlBase + '/activities',
        method: 'GET',
        success: function (res, textStatus) {
            const activities = textStatus === "nocontent" ? [] : res.data
            const events = activities.map(function (actividad) {
                return {
                    title: actividad.name,
                    start: actividad.startDate,
                    end: actividad.endDate,
                    description: actividad.description
                };
            });

            const calendar = new FullCalendar.Calendar(document.getElementById('calendarActivities'), {
                initialView: 'dayGridMonth',
                locale: 'es', // Calendario en español
                events: events,
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,listWeek'
                },
                eventClick: function (info) {
                    alert("Actividad: " + info.event.title + "\nDescripción: " + info.event.extendedProps.description);
                }
            });

            calendar.render();
        },
        error: function () {
        }
    });
}

function loadChartMoneydDonation() {
    $.ajax({
        url: urlBase + '/money-donations',
        type: 'GET',
        success: function (res, textStatus) {
            const donations = textStatus === "nocontent" ? [] : res.data
            const monthlyTotals = {};

            donations.forEach(d => {
                const date = new Date(d.donationDate);
                const key = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`;
                monthlyTotals[key] = (monthlyTotals[key] || 0) + d.amount;
            });

            const labels = Object.keys(monthlyTotals);
            const data = Object.values(monthlyTotals);

            new Chart(document.getElementById('myMoneyDonationChart'), {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Monto Donado (₡)',
                        data: data,
                        backgroundColor: 'rgba(40, 167, 69, 0.7)',
                        borderColor: 'rgba(40, 167, 69, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
        error: function (error) {
        }
    });
}
