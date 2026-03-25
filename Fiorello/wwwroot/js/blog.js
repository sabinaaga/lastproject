const { count } = require("node:console");

let btn = document.querySelector(".buttonBlog");
var blog = document.querySelector(".blog-Append");
var coutAllBlogs = parseInt(this.getAttribute("data-count"));



btn.addEventListener("click", function () {
    var counter = btn.previousElementSibling.children.length;
    if (coutAllBlogs - count == 3) {
        this.style.display = "none";
    }
   
});





btn.addEventListener("click", function () {
    var counter = btn.previousElementSibling.children.length;
    if (coutAllBlogs - count==3) {
        this.style.display = "none";
    }
    else {
        fetch(`https://localhost:7187/blog/showmore?skip=${counter}`)

            .then(response => response.text())
            .then(json => {

                blog.innerHTML += json;
            })
    } 
});


//let btn = document.querySelector(".buttonBlog");
//let blog = document.querySelector(".blog-Append");

//btn.addEventListener("click", function () {
//    let beforeCount = blog.children.length;

//    fetch(`https://localhost:7207/blog/showmore?skip=${beforeCount}`)
//        .then(response => response.text())
//        .then(html => {

//            // добавляем блоги
//            blog.insertAdjacentHTML("beforeend", html);

//            let afterCount = blog.children.length;

//            // если ничего не добавилось — скрываем кнопку
//            if (afterCount === beforeCount) {
//                btn.style.display = "none";
//            }
//        });
//});
