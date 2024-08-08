$(document).ready(function () {
    $('#authForm').on('submit', function (event) {
        event.preventDefault();
        const name = $('#username').val();
        const password = $('#password').val();
        authenticateUser(name, password);
    });
});


function authenticateUser(name, password) {
    $.ajax({
        url: '/api/home/authenticate',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ name, password }),
        success: function (data) {
            
            $('#message').text('Todo bien').css('color', 'green');
            console.log('User authenticated:', data);
            var userid = data.id;
            localStorage.setItem('userId', userid);
            console.log(localStorage.getItem('userId'));
            window.location.href = '/calcular';
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 401) {
                $('#message').text('Credenciales inválidas.').css('color', 'red');
            } else {
                $('#message').text('Error al autenticar usuario. Intenta nuevamente.').css('color', 'red');
            }
            console.error('Error during authentication:', textStatus, errorThrown);
        }
    });
}