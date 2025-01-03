function showLoader() {
    document.getElementById('loader').classList.remove('d-none')
}
function hideLoader() {
    document.getElementById('loader').classList.add('d-none')
}

function successToast(msg) {
    Toastify({
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        text: msg,
        className: "mt-5 p-2",
        style: {
            background: "green",
        }
    }).showToast();
}

function errorToast(msg) {
    Toastify({
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        text: msg,
        className: "mt-5 p-3 rounded",
        style: {
            background: "red",
        },
        duration: 3000
    }).showToast();
}





function previewImage(event) { 
    var reader = new FileReader(); 
    reader.onload = function() { 
        var output = document.getElementById('imagePreview'); 
        output.src = reader.result; 
        output.style.display = 'block'; } 
        reader.readAsDataURL(event.target.files[0]); 
        }
