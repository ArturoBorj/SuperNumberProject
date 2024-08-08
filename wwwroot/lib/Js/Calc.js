function getUserId() {
    return localStorage.getItem('userId');
}
$(document).ready(function () {
    const btnSave = $('#btnSave');
    const btnDeleteHis = $('#btnDeleteHis');
    const tableHistorical = $('#tableHistorical');

    // Inicializa Tabulator
    const table = new Tabulator("#tableHistorical", {
        layout: "fitColumns",
        columns: [
            { title: "Numero", field: "number", width:'30%' },
            { title: "Resultado", field: "result", width: '30%' },
            { title: "Fecha", field: "fecha",  width: '40%' }
        ],
    });
    function init() {
        const idUser = getUserId();
        getHistorical(idUser);
    }
    init();
    function createSuperNumber(number, idUser) {
        $.ajax({
            url: '/api/supernumberapi/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Number: number, IdUser: idUser }),
            success: function (response) {
                console.log("Hola");
                //console.log('Response:', response);
                alert('Super número creado exitosamente.' + response);
                    $('#result').val(response);
                    getHistorical(idUser);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error('Failed to create super number:', textStatus, errorThrown);
                alert('Error al crear el super número. Por favor, intenta de nuevo helpeee.');
            }
        });
    }
    
    function deleteHistoricalSuperNumbers(idUser) {
        $.ajax({
            url: `/api/supernumberapi/delete/${idUser}`,
            type: 'DELETE',
            success: function (data) {
                console.log('Historical super numbers deleted:', data);
                alert('Historial de super números borrado exitosamente.');
                table.clearData(); 
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error('Failed to delete historical super numbers:', textStatus, errorThrown);
                alert('Error al borrar el historial. Por favor, intenta de nuevo.');
            }
        });
    }


    function getHistorical(idUser) {
        $.ajax({
            url: `/api/supernumberapi/history/${idUser}`, 
            type: 'GET',
            success: function (data) {
                console.log('Historical super numbers:', data);
                table.setData(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error('Failed to fetch historical super numbers:', textStatus, errorThrown);
                alert('Error al obtener el historial. Por favor, intenta de nuevo.');
            }
        });
    }
    
    btnSave.on('click', function (event) {
        event.preventDefault();

        const number = $('#number').val();
        const idUser = getUserId(); 
        console.info(idUser);
        const userId = localStorage.getItem('userId');
        console.info(userId);
        if (!number || !idUser) {
            alert('Número o ID de usuario no válidos.');
            return;
        }

        createSuperNumber(parseInt(number), idUser);
    });
    
    btnDeleteHis.on('click', function (event) {
        event.preventDefault();

        const idUser = getUserId(); 

        if (!idUser) {
            alert('ID de usuario no válido.');
            return;
        }

        deleteHistoricalSuperNumbers(idUser);
    });
});
