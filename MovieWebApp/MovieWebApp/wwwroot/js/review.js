var baseUrl = "https://localhost:7237/"
var url_string = window.location;
var url = new URL(url_string);
var pathName = url.pathname.replace("/detail/", "");
document.getElementById("sendReview").addEventListener("click", CreateReview)
var editButtons = document.querySelectorAll("#editReview");
var deleteButtons = document.querySelectorAll("#deleteReview");
function Even() {
    var editButtons = document.querySelectorAll("#editReview");
    var deleteButtons = document.querySelectorAll("#deleteReview");
    editButtons.forEach(button => {
        button.addEventListener("click", () => {
            var parent = button.parentElement.parentElement.parentElement;
            var id = parent.id;
            var editbuton = document.getElementById("confirmEdit");
            editbuton.addEventListener("click", () => {
                UpdateReview(id)
                editbuton.parentNode.getElementsByTagName("button")[1].click()
            })

        })
    })
    deleteButtons.forEach(button => {
        button.addEventListener("click", () => {
            var parent = button.parentElement.parentElement.parentElement;
            var id = parent.id;
            var editbuton = document.getElementById("confirmDelete");
            editbuton.addEventListener("click", () => {
                DeleteReview(id)
            })

        })
    })
}
Even()
//Create Review
function CreateReview() {
    var token = document.getElementById("Token").value;
    if (token === "") {
        window.location = "/login"
    }
    let title = document.getElementById("reviewTitle").value;
    let content = document.getElementById("reviewContent").value;
    let ratting = document.getElementById("rating").value;
    let userId = document.getElementById("UserID").value;
    var myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${token}`);
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "title": title,
        "reviewContent": content,
        "rating": ratting,
        "userID": userId,
        "movieID": pathName
    });

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };

    fetch(baseUrl + "api/v1/Review/CreateReview", requestOptions)
        .then(response => response.text())
        .then(result => {
            let json = JSON.parse(result)
            AddMyReview(json.data)
            document.getElementById("reviewTitle").value = "";
            document.getElementById("reviewContent").value = ""
            document.getElementById("rating").value = 8
        }
        )
        .catch(error => console.log('error', error));
}
//End Create Review
//Update Review
function UpdateReview(id) {
    var token = document.getElementById("Token").value;
    if (token === "") {
        window.location = "/login"
    }
    let title = document.getElementById("editReviewTitle").value;
    let content = document.getElementById("editReviewContent").value;
    let ratting = document.getElementById("rating2").value;
    var myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${token}`);
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "reviewID": id,
        "title": title,
        "reviewContent": content,
        "rating": ratting
    });

    var requestOptions = {
        method: 'PUT',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };

    fetch(baseUrl + "api/v1/Review/UpdateReview", requestOptions)
        .then(response => response.text())
        .then(result => {
            let json = JSON.parse(result)
            EditReview(json.data)
        })
        .catch(error => console.log('error', error));
}
//End Update Review
//Delete Review
function DeleteReview(id) {
    var token = document.getElementById("Token").value;
    if (token === "") {
        window.location = "/login"
    }
    var myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${token}`);
    myHeaders.append("Content-Type", "application/json");

    var requestOptions = {
        method: 'DELETE',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch(baseUrl + "api/v1/Review/DeleteReview?ReviewID=" + id, requestOptions)
        .then(response => response.text())
        .then(result => {
            let json = JSON.parse(result)
            DeleteElement(json.data)
        })
        .catch(error => console.log('error', error));

}
//End Delete Review

function AddReview(review) {
    var html = `

    <div class="reviews__autor">
        <img class="reviews__avatar" src="${review.avatar}" alt="">
        <span class="reviews__name">${review.title}</span>
        <span class="reviews__time">${ConvertDateTimeToJs(review.reviewTime)} by ${review.firstName} ${review.lastName}</span>


        <div>
                <span class="reviews__rating2 t-10">
                    <svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24"
                    style="margin-right: 5px;">
                    <path
                        d="M22,10.1c0.1-0.5-0.3-1.1-0.8-1.1l-5.7-0.8L12.9,3c-0.1-0.2-0.2-0.3-0.4-0.4C12,2.3,11.4,2.5,11.1,3L8.6,8.2L2.9,9C2.6,9,2.4,9.1,2.3,9.3c-0.4,0.4-0.4,1,0,1.4l4.1,4l-1,5.7c0,0.2,0,0.4,0.1,0.6c0.3,0.5,0.9,0.7,1.4,0.4l5.1-2.7l5.1,2.7c0.1,0.1,0.3,0.1,0.5,0.1v0c0.1,0,0.1,0,0.2,0c0.5-0.1,0.9-0.6,0.8-1.2l-1-5.7l4.1-4C21.9,10.5,22,10.3,22,10.1z">
                    </path>
                </svg><span>${review.rating}</span>
            </span>


        </div>

    </div>
    <p class="reviews__text">
        ${review.reviewContent}
    </p>

`
    
    var div = document.createElement("li");
    div.setAttribute("class", "reviews__item");
    div.id = review.reviewID
    div.innerHTML = html;
    document.querySelector(".reviews__list").prepend(div)
    editButtons = document.querySelectorAll("#editReview");
    deleteButtons = document.querySelectorAll("#deleteReview");
    Even()
}
function EditReview(review) {
    var parent = document.getElementById(review.reviewID);
    parent.querySelector(".reviews__name").textContent = review.title;
    parent.querySelector(".reviews__text").textContent = review.reviewContent;
    parent.querySelector(".reviews__time").innerHTML = ConvertDateTimeToJs(review.reviewTime) + " by " + review.firstName + " " + review.lastName ;
    parent.querySelector(".t-10 span").textContent = review.rating;
}
function DeleteElement(id) {
    document.getElementById(id).remove();
    DismissAll()
}
function AddMyReview(review) {
    var html = `
     <div class="reviews__autor">
        <img class="reviews__avatar" src="${review.avatar}" alt="">
        <span class="reviews__name">${review.title}</span>
        <span class="reviews__time">${ConvertDateTimeToJs(review.reviewTime)} by ${review.firstName} ${review.lastName}</span>


        <div>
            
                <span class="reviews__rating2" style="right: 80px;" id="editReview">
                    <a href=${"#editReview" + review.reviewID} class="main__table-btn main__table-btn--edit open-modal">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                            <path
                            d="M5,18H9.24a1,1,0,0,0,.71-.29l6.92-6.93h0L19.71,8a1,1,0,0,0,0-1.42L15.47,2.29a1,1,0,0,0-1.42,0L11.23,5.12h0L4.29,12.05a1,1,0,0,0-.29.71V17A1,1,0,0,0,5,18ZM14.76,4.41l2.83,2.83L16.17,8.66,13.34,5.83ZM6,13.17l5.93-5.93,2.83,2.83L8.83,16H6ZM21,20H3a1,1,0,0,0,0,2H21a1,1,0,0,0,0-2Z" />
                        </svg>
                    </a>
                </span>

                <span class="reviews__rating2" style="right: 50px;" id="deleteReview">
                    <a href=${"#delCata" + review.reviewID} class="main__table-btn main__table-btn--delete open-modal">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                            <path
                            d="M20,6H16V5a3,3,0,0,0-3-3H11A3,3,0,0,0,8,5V6H4A1,1,0,0,0,4,8H5V19a3,3,0,0,0,3,3h8a3,3,0,0,0,3-3V8h1a1,1,0,0,0,0-2ZM10,5a1,1,0,0,1,1-1h2a1,1,0,0,1,1,1V6H10Zm7,14a1,1,0,0,1-1,1H8a1,1,0,0,1-1-1V8H17Z" />
                        </svg>
                    </a>
                </span>

            <span class="reviews__rating2 t-10">
                <svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24"
                    style="margin-right: 5px;">
                    <path
                        d="M22,10.1c0.1-0.5-0.3-1.1-0.8-1.1l-5.7-0.8L12.9,3c-0.1-0.2-0.2-0.3-0.4-0.4C12,2.3,11.4,2.5,11.1,3L8.6,8.2L2.9,9C2.6,9,2.4,9.1,2.3,9.3c-0.4,0.4-0.4,1,0,1.4l4.1,4l-1,5.7c0,0.2,0,0.4,0.1,0.6c0.3,0.5,0.9,0.7,1.4,0.4l5.1-2.7l5.1,2.7c0.1,0.1,0.3,0.1,0.5,0.1v0c0.1,0,0.1,0,0.2,0c0.5-0.1,0.9-0.6,0.8-1.2l-1-5.7l4.1-4C21.9,10.5,22,10.3,22,10.1z">
                    </path>
                </svg><span>${review.rating}</span>
            </span>


        </div>

    </div>
    <p class="reviews__text">
         ${review.reviewContent}
    </p>
</li>
<!-- modal status -->
<div id=${"editReview" + review.reviewID} class="zoom-anim-dialog mfp-hide modal">
    <h6 class="modal__title">Edit review "${review.title}"</h6>
    <div class="reviews">
        <div class="form">
            <input type="text" class="form__input" placeholder="Title" id="editReviewTitle" required>
            <textarea class="form__textarea" placeholder="Review" id="editReviewContent" required></textarea>
            <div class="form__slider">
                <input type="number" class=" form__input" id="rating2" name="rating2" min="0" max="10" value="8" />
            </div>
        </div>
    </div>

    <div class="modal__btns">
        <button class="modal__btn modal__btn--apply" id="confirmEdit" type="button">Edit</button>
        <button class="modal__btn modal__btn--dismiss" type="button">Dismiss</button>
    </div>
</div>
<!-- end modal status -->
<!-- modal delete -->
<div id=${"#delCata" + review.reviewID} class="zoom-anim-dialog mfp-hide modal">
    <h6 class="modal__title">Delete review</h6>

    <p class="modal__text">Are you sure to permanently delete "${review.title} "?</p>

    <div class="modal__btns">

        <button class="modal__btn modal__btn--apply" id="confirmDelete" type="button">Delete</button>

        <button class="modal__btn modal__btn--dismiss" type="button">Dismiss</button>
    </div>
</div>
<!-- end modal delete -->
    
`
    $(document).ready(function () {
        $(".open-modal").magnificPopup({
            fixedContentPos: true,
            fixedBgPos: true,
            overflowY: "auto",
            type: "inline",
            preloader: false,
            focus: "#username",
            modal: false,
            removalDelay: 300,
            mainClass: "my-mfp-zoom-in",
        });

        $(".modal__btn--dismiss").on("click", function (e) {
            e.preventDefault();
            $.magnificPopup.close();
        });
    })
    var div = document.createElement("li");
    div.setAttribute("class", "reviews__item");
    div.id = review.reviewID
    div.innerHTML = html;
    document.querySelector(".reviews__list").prepend(div)
}
// SignalR
var connection = new signalR.HubConnectionBuilder().withUrl(baseUrl + "review").build();
//Response
connection.on("Review", function (type, userId, review) {
    let myId = document.getElementById("UserID").value;
    if (!CompareGuid(myId, userId)) {
        console.log(review)
        if (type == "Create") {
            AddReview(review);
        }
        if (type == "Update") {
            EditReview(review)
        }
        if (type == "Delete") {
            DeleteElement(review)
        }
    }


});
connection.start().then(function () {
    connection.invoke("JoinGroup", pathName).then(function () {
        console.log("Connect and Join Group Success")
    })
}).catch(function (err) {
    return console.error(err.toString());
});

function DismissAll() {
    document.querySelectorAll(".modal__btn").forEach(item => {
        item.click()
    })
}
function CompareGuid(guid1, guid2) {
    var regExp = new RegExp(guid1, "i"); 

    return regExp.test(`{${guid2}}`);
}

function ConvertDateTimeToJs(time) {
    var date = new Date(time);
    var Time = date.toLocaleTimeString()
    var str = date.getMonth()+1 + "/" + date.getDate()  + "/" + "" + date.getFullYear() + " " + Time;
    return str;
}