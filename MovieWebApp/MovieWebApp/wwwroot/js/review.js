
var url_string = window.location;
var url = new URL(url_string);
var pathName = url.pathname.replace("/detail/","");
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7237/review").build();
//Response
connection.on("SendReview", function (review) {
    AddReview(review);
});
connection.start().then(function () {
    connection.invoke("JoinGroup", pathName).then(function () {
        console.log("Connect and Join Group Success")
    })
}).catch(function (err) {
    return console.error(err.toString());
});
function AddReview(review) {
    console.log(review)
    var html = `
        <div class="reviews__autor">
            <img class="reviews__avatar" src="${review.avatar}" alt="">
                <span class="reviews__name">${review.title}</span>
                <span class="reviews__time">${review.reviewTime} by ${review.firstName} ${review.lastName}</span>

                <span class="reviews__rating">
                    <svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24">
                        <path
                            d="M22,10.1c0.1-0.5-0.3-1.1-0.8-1.1l-5.7-0.8L12.9,3c-0.1-0.2-0.2-0.3-0.4-0.4C12,2.3,11.4,2.5,11.1,3L8.6,8.2L2.9,9C2.6,9,2.4,9.1,2.3,9.3c-0.4,0.4-0.4,1,0,1.4l4.1,4l-1,5.7c0,0.2,0,0.4,0.1,0.6c0.3,0.5,0.9,0.7,1.4,0.4l5.1-2.7l5.1,2.7c0.1,0.1,0.3,0.1,0.5,0.1v0c0.1,0,0.1,0,0.2,0c0.5-0.1,0.9-0.6,0.8-1.2l-1-5.7l4.1-4C21.9,10.5,22,10.3,22,10.1z">
                        </path>
                    </svg>${review.rating}
                </span>
        </div>
        <p class="reviews__text">
            ${review.reviewContent}
        </p>`
    var div = document.createElement("li");
    div.setAttribute("class", "reviews__item");
    div.innerHTML = html;
    document.querySelector(".reviews__list").prepend(div)

   
}