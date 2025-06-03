window.drawCharts = (plasticData, dateData) => {
    new Chart(document.getElementById("plasticChart"), {
        type: 'bar',
        data: {
            labels: plasticData.labels,
            datasets: [{
                label: 'Kg plastic',
                data: plasticData.data,
                backgroundColor: 'rgba(75, 192, 192, 0.6)'
            }]
        },
        options: {
            responsive: true
        }
    });

    new Chart(document.getElementById("dateChart"), {
        type: 'bar',
        data: {
            labels: dateData.labels,
            datasets: [{
                label: 'Kg afval',
                data: dateData.data,
                backgroundColor: 'rgba(153, 102, 255, 0.6)'
            }]
        },
        options: {
            responsive: true,
            scales: {
                x: { ticks: { maxRotation: 90, minRotation: 45 } }
            }
        }
    });
};
