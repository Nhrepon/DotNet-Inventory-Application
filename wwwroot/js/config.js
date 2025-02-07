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
        document.getElementById("imagePreviewBox").classList.remove("d-none"); 
         } 
        reader.readAsDataURL(event.target.files[0]); 
        }

function removeItem(){
    document.getElementById("file").value = "";
    document.getElementById("imagePreviewBox").remove();
}

// function handleFileChange(e) {
//     previewImage(e);
//     document.getElementById("imagePreviewBox").classList.remove("d-none");
// }
      


// function handleFileChange(event) {
//     const files = event.target.files;
//     const imagePreviewBox = document.getElementById('imagePreviewBox');
//     imagePreviewBox.innerHTML = ''; // Clear previous previews

//     for (const file of files) {
//         if (file.type.startsWith('image/')) {
//             const reader = new FileReader();

//             reader.onload = function(e) {
//                 const previewCard = document.createElement('div');
//                 previewCard.classList.add('card', 'rounded', 'shadow', 'mb-3');

//                 const closeButton = document.createElement('span');
//                 closeButton.classList.add('position-absolute', 'bg-light', 'px-1', 'rounded', 'end-0', 'me-1', 'mt-1');
//                 const closeIcon = document.createElement('i');
//                 closeIcon.classList.add('bi', 'bi-x-square-fill');
//                 closeButton.appendChild(closeIcon);
//                 previewCard.appendChild(closeButton);

//                 const img = document.createElement('img');
//                 img.src = e.target.result;
//                 img.alt = 'Image Preview';
//                 img.classList.add('card-img', 'img-fluid');
//                 previewCard.appendChild(img);

//                 imagePreviewBox.appendChild(previewCard);
//                 imagePreviewBox.classList.remove('d-none'); // Show preview box

//                 // Add event listener to close button
//                 closeButton.addEventListener('click', () => {
//                     previewCard.remove();
//                     document.getElementById("file").value = "";
//                     // If no more previews, hide the box
//                     if (imagePreviewBox.children.length === 0) {
//                         imagePreviewBox.classList.add('d-none');
//                     }
//                 });
//             };

//             reader.readAsDataURL(file);
//         }
//     }
// }





async function uploadFile(){
    try{
        let files = document.getElementById("file").files;
        if(files.length == 0){
            errorToast("Please, Select images");
        }else{
            let formData = new FormData();
            for(const file of files){
                formData.append("file", file);
            }
            let response =await axios.post("/Media/upload", formData, );
            if(response.data.status === "success"){
                window.location.href = "/Media";
                successToast(response.data["message"]);
            }else{
                errorToast(response.data["message"]);
            }
        }
    }catch(e){
        errorToast(e.message);
    }
}






async function deleteFile(id) {
        const res = await axios.delete(`Media/Delete/${id}`);
        if (res.data.status === "success") {
            successToast(`${id} deleted successfully!`);
            window.location.href = "/Media";
        } else {
            errorToast(res.data.message);
        }
}
