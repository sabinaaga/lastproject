

"use strict";
let deleteButtons = document.querySelectorAll(".delete-btn");

let mainButtons = document.querySelectorAll(".ismain-btn");

document.querySelectorAll(".delete-btn").forEach(btn => {

    btn.addEventListener("click", function () {

        let imageId = parseInt(this.parentElement.getAttribute("data-id"));
        fetch("/Admin/Product/DeleteProductInUpdate?id=" + imageId, {
            method: "POST"
        })
            .then(response => {

                if (response.ok) {
                    this.closest(".col-3").remove();
                }

            })
            .catch(error => console.log(error));

    });

});




document.querySelectorAll(".ismain-btn").forEach(btn => {
    btn.addEventListener("click", function () {
        let imageId = parseInt(this.parentElement.getAttribute("data-id"));

        fetch("/Admin/Product/IsMainProductInUpdate?id=" + imageId, { method: "POST" })
            .then(response => {
                if (response.ok) {
                    // Убираем зелёный бордер у всех картинок
                    document.querySelectorAll(".edit-main-product").forEach(div => div.classList.remove("edit-main-product"));

                    // Показываем все кнопки IsMain
                    mainButtons.forEach(b => b.classList.remove("d-none"));
                    deleteButtons.forEach(b => b.classList.remove("d-none"));

                    // Ставим зелёный бордер на выбранную картинку
                    let selectedDiv = this.closest(".col-3"); // div с картинкой
                    selectedDiv.classList.add("edit-main-product");

                    // Скрываем кнопку IsMain у выбранной картинки
                    this.classList.add("d-none");
                }
            })
            .catch(error => console.log(error));
    });
});


//document.querySelectorAll(".ismain-btn").forEach(btn => {

//    btn.addEventListener("click", function () {

//        let imageId = parseInt(this.parentElement.getAttribute("data-id"));
//        fetch("/Admin/Product/IsMainProductInUpdate?id=" + imageId, {
//            method: "POST"
//        })
//            .then(response => {

//                if (response.ok) {
//                    deleteButtons.forEach(btn => btn.classList.add("d-none"));
//                    mainButtons.forEach(btn => btn.classList.add("d-none"));

//                }

//            })
//            .catch(error => console.log(error));

//    });

//});







//let deleteButtons = document.querySelectorAll(".delete-btn");

//document.querySelectorAll(".delete-btn").forEach(btn => {

//    btn.addEventListener("click", function () {

//        let imageId = this.parentElement.getAttribute("data-id");

//        fetch("https://localhost:7187/Admin/DeleteProductInUpdate/" + imageId, {
//            method: "POST"
//        })
//            .then(response => response.json())
//            .then(data => {

//                if (data.success) {
//                    this.closest(".col-3").remove();
//                }

//            })
//            .catch(error => console.log(error));

//    });

//});