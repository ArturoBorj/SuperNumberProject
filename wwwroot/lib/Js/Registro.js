$(document).ready(function () {
    async function createUser(name, password) {
            $.ajax({
                url: '/api/home/create',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ Name: name, Password: password }),
                success: function (data) {
                    console.log('User created:', data);
                    alert('Usuario creado exitosamente.');
                    window.location.href = '/';
                    
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Failed to create user:', textStatus, errorThrown);
                    alert('Error al crear el usuario. Por favor, intenta de nuevo.');
                    
                }
        });
    }
    

    $('#btnGuardar').on('click', async function (event) { 
        event.preventDefault(); 
        const username = $('#usernameR').val();
        const password = $('#passwordR').val();
        const confirmPassword = $('#confirmPassword').val();

        if (password !== confirmPassword) {
            alert('Las contraseñas no coinciden.');
            return;
        }

        try {
            await createUser(username, password);
        } catch (error) {
            console.error('Error:', error);
            alert('Error de red. Por favor, intenta de nuevo.');
        }
    });

    
});