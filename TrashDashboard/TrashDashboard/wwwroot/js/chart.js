window.renderAfvalChart = (data, labels) => {
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
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Voorspeld aantal stuks afval',
                    data: data,
                    backgroundColor: 'rgba(75, 192, 192, 0.7)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                aspectRatio: 2,
                plugins: {
                    legend: {
                        display: true,
                        position: 'top',
                        labels: {
                            color: 'black',
                            boxWidth: 12
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return `Voorspeld afval: ${context.raw} stuks`;
                            }
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Aantal stuks afval',
                            color: 'black'
                        },
                        ticks: {
                            color: 'black',
                            stepSize: 1,
                            precision: 0
                        },
                        grid: {
                            color: 'rgba(0, 0, 0, 0.1)'
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Datum',
                            color: 'black'
                        },
                        ticks: {
                            color: 'black'
                        },
                        grid: {
                            display: false
                        }
                    }
                },
                layout: {
                    padding: {
                        top: 10,
                        right: 10,
                        bottom: 10,
                        left: 10
                    }
                }
            }
        });

        console.log('Afval chart rendered successfully.');
    } catch (error) {
        console.error('Error rendering afval chart:', error);
    }
};

window.renderWeerChart = (temperatuurData, weerOmschrijvingen, labels) => {
    try {
        const ctx = document.getElementById('weerChart')?.getContext('2d');
        if (!ctx) {
            console.error('Canvas context not found!');
            return;
        }

        if (window.weerChartInstance instanceof Chart) {
            window.weerChartInstance.destroy();
        }

        const weatherTypes = {
            'Zonnig': 'rgba(255, 215, 0, 0.7)',
            'Licht bewolkt': 'rgba(169, 169, 169, 0.7)',
            'Bewolkt': 'rgba(105, 105, 105, 0.7)',
            'Regenachtig': 'rgba(30, 144, 255, 0.7)'
        };

        const backgroundColors = weerOmschrijvingen.map(omschrijving => {
            const normalized = omschrijving.toLowerCase();
            if (normalized.includes('zonnig')) return weatherTypes['Zonnig'];
            if (normalized.includes('licht bewolkt')) return weatherTypes['Licht bewolkt'];
            if (normalized.includes('bewolkt')) return weatherTypes['Bewolkt'];
            if (normalized.includes('regen')) return weatherTypes['Regenachtig'];
            return weatherTypes['Other'];
        });

        window.weerChartInstance = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Temperatuur (°C)',
                        data: temperatuurData,
                        yAxisID: 'y',
                        borderColor: 'rgb(188, 88, 88)',
                        backgroundColor: backgroundColors,
                        borderWidth: 1
                    }
                ]
            },
            options: {
                responsive: true,
                plugins: {
                    tooltip: {
                        callbacks: {
                            afterLabel: function (context) {
                                const index = context.dataIndex;
                                return `Weer: ${weerOmschrijvingen[index]}`;
                            }
                        }
                    },
                    legend: {
                        display: false
                    },
                    customLegend: {
                        position: 'top',
                        labels: Object.keys(weatherTypes).map(type => ({
                            text: type,
                            fillStyle: weatherTypes[type],
                            strokeStyle: '#fff',
                            lineWidth: 1
                        }))
                    }
                },
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
                        },
                        grid: {
                            color: 'rgba(255, 255, 255, 0.1)'
                        }
                    },
                    x: {
                        ticks: {
                            color: 'black'
                        },
                        grid: {
                            color: 'rgba(255, 255, 255, 0.1)'
                        }
                    }
                }
            },
            plugins: [{
                id: 'customLegend',
                afterDraw(chart) {
                    const ctx = chart.ctx;
                    const legendItems = chart.options.plugins.customLegend.labels;
                    const legendX = chart.width / 2 - (legendItems.length * 50) / 2;
                    let x = legendX;

                    ctx.font = '12px Arial';
                    ctx.textAlign = 'left';
                    ctx.textBaseline = 'middle';

                    legendItems.forEach(item => {
                        ctx.fillStyle = item.fillStyle;
                        ctx.strokeStyle = item.strokeStyle;
                        ctx.lineWidth = item.lineWidth;
                        ctx.fillRect(x, 10, 15, 15);
                        ctx.strokeRect(x, 10, 15, 15);

                        ctx.fillStyle = 'black';
                        ctx.fillText(item.text, x + 20, 18);

                        x += 100;
                    });
                }
            }]
        });

        console.log('Weer chart rendered successfully.');
    } catch (error) {
        console.error('Error rendering weer chart:', error);
    }
};