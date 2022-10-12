"use strict";

var userID = document.getElementById("UserID").value
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7237/chat").build();
document.querySelector(".form__btn").disabled = true;
//Response
connection.on("SendMessage", function (message) {
    AddMessage(message);
});
connection.start().then(function () {
    connection.invoke("JoinGroup", userID).then(function () {
        console.log("Connect and Join Group Success")
        document.querySelector(".form__btn").disabled = false;
    })
}).catch(function (err) {
    return console.error(err.toString());
});
function AddMessage(message) {
    var html = `<div class="d-flex flex-row justify-content-start mb-4">
    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava1-bg.webp"
         alt="avatar 1" style="width: 45px; height: 100%;">
    <div class="p-3 ml-3" style="border-radius: 15px; background-color: #ff55a5">
        <p class="small mb-0">
             ${message}
        </p>
        </div>
  </div>`
    var div = document.createElement("div");
    div.setAttribute("class", "d-flex flex-row justify-content-start mb-4");
    div.innerHTML = html;
    document.querySelector(".card-body").appendChild(div)
}