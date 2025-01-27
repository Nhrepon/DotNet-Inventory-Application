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

function handleFileChange(e) {
    previewImage(e);
    document.getElementById("imagePreviewBox").classList.remove("d-none");
}
        


function uploadFile() {
    //let formData = new FormData();

    errorToast("Please, Select images");

}










const removeFile = (e) => {
    setFile(file.filter((item, i) => i !== e))
}

const deleteFile = async (id) => {
    if (await DeleteAlert()) {
        const res = await axios.delete(`api/fileDelete/${id}`);
        if (res.data.status === "success") {
            await getFileList();
            toast.success(`${id} deleted successfully!`);
        } else {
            toast.error(res.data.message);
        }
    }


}
