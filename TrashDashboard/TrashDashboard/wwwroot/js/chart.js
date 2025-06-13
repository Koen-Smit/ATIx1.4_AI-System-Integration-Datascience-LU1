window.renderAfvalChart = (data) => {
    try {
        const ctx = document.getElementById('afvalChart')?.getContext('2d');
        if (!ctx) {
            console.error('Canvas context not found!');
            return;
        }

        if (window.afvalChartInstance instanceof Chart) {
            window.afvalChartInstance.destroy();
        }

        window.afvalChartInstance = new Chart(ctx, {
            type: 'line',
            data: {
                labels: [
                    "06:00", "07:00", "08:00", "09:00", "10:00", "11:00",
                    "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"
                ],
                datasets: [{
                    label: 'Afval gedetecteerd per uur',
                    data: data,
                    borderColor: 'rgb(233, 233, 233)',
                    fill: false,
                    tension: 0
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Aantal afvalmeldingen',
                            color: 'white'
                        },
                        ticks: {
                            color: 'white'
                        }
                    },
                    x: {
                        ticks: {
                            color: 'white'
                        }
                    }
                },
                plugins: {
                    legend: {
                        labels: {
                            color: 'white'
                        }
                    }
                }
            }
        });

        console.log('Afval chart rendered successfully.');
    } catch (error) {
        console.error('Error rendering afval chart:', error);
    }
};

window.renderWeerChart = (temperatuurData, neerslagData) => {
    try {
        const ctx = document.getElementById('weerChart')?.getContext('2d');
        if (!ctx) {
            console.error('Canvas context not found!');
            return;
        }

        if (window.weerChartInstance instanceof Chart) {
            window.weerChartInstance.destroy();
        }

        const labels = [
            "02/06/2025", "03/06/2025", "04/06/2025", "05/06/2025",
            "06/06/2025", "07/06/2025", "08/06/2025", "09/06/2025"
        ];

        window.weerChartInstance = new Chart(ctx, {
            data: {
                labels: labels,
                datasets: [
                    {
                        type: 'line',
                        label: 'Temperatuur (°C)',
                        data: temperatuurData,
                        yAxisID: 'y',
                        borderColor: 'rgb(188, 88, 88)',
                        backgroundColor: 'rgba(255, 99, 132, 0.2)',
                        tension: 0.3,
                        fill: false
                    },
                    {
                        type: 'bar',
                        label: 'Neerslag (mm)',
                        data: neerslagData,
                        yAxisID: 'y1',
                        backgroundColor: 'rgba(54, 162, 235, 0.5)',
                        borderColor: 'rgb(143, 194, 228)',
                        borderWidth: 1
                    }
                ]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        type: 'linear',
                        position: 'left',
                        title: {
                            display: true,
                            text: 'Temperatuur (°C)',
                            color: 'black'
                        },
                        ticks: {
                            color: 'black'
                        }
                    },
                    y1: {
                        type: 'linear',
                        position: 'right',
                        title: {
                            display: true,
                            text: 'Neerslag (mm)',
                            color: 'black'
                        },
                        ticks: {
                            color: 'black'
                        },
                        grid: {
                            drawOnChartArea: false
                        }
                    },
                    x: {
                        ticks: {
                            color: 'black'
                        }
                    }
                },
                plugins: {
                    legend: {
                        labels: {
                            color: 'black'
                        }
                    }
                }
            }
        });

        console.log('Weer chart rendered successfully.');
    } catch (error) {
        console.error('Error rendering weer chart:', error);
    }
};