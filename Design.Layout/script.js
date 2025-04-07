document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('registrationModal');
    const outputDiv = document.getElementById('userData');
    const modalElement = document.getElementById('registrationModal');
    const modal = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);
    
    form.addEventListener('submit', function(e) {
        e.preventDefault();

        const fullName = document.getElementById('fullName').value.trim();
        const email = document.getElementById('email').value.trim();
        
        const userName = document.getElementById('user-name');
        const userEmail = document.getElementById('user-email');
        
        userName.innerText = `ФИО: ${fullName}`
        userEmail.innerText = `E-mail: ${email}`
        
        modal.hide();
    });
    
});