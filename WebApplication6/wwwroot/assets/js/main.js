let deleteBtns = document.querySelectorAll(".delete-btn");

deleteBtns.forEach(delBtn => delBtn.addEventListener("click", (e) => {
    e.preventDefault();
    let url = delBtn.getAttribute("href")

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            fetch(url).then(res => {
                if (res.ok == true) {
                    window.location.reload(true);

                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }

            })
        }
    });
}))


// Add to basket with Fetch
let addToBasketBns = document.querySelectorAll(".add-to-basket");


addToBasketBns.forEach(btn => btn.addEventListener("click", function (e) {
    let url = btn.getAttribute("href");

    e.preventDefault();

    fetch(url)
        .then(response => {
            if (response.status == 200) {
                alert("Added to basket")
            } else {
                alert("error!")
            }
        })


}))




// bookdetail model
const btns = document.querySelectorAll(".quick-modal-btn");

btns.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        const url = btn.getAttribute("href");
        console.log(url)
        fetch(url)
            .then(response => response.text())
            .then(data => {
                const modal = document.querySelector(".modal-dialog");
                modal.innerHTML = data
            })
    })
})