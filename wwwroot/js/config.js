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













