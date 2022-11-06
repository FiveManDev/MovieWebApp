"use strict";
var baseUrl = "https://localhost:7237/";
var userID = document.getElementById("UserID").value;
var connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7237/chat")
  .build();
document.querySelector(".form__btn").disabled = true;
//Response
connection.on("SendMessage", function (from, id, message) {
  //scroll bottom of chat

  console.log(message);
  if (from === "Admin") {
    AddMessage(message);
  }
});
connection
  .start()
  .then(function () {
    connection.invoke("JoinGroup", userID).then(function () {
      console.log("Connect and Join Group Success");
      document.querySelector(".form__btn").disabled = false;
    });
  })
  .catch(function (err) {
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
  </div>`;
  var div = document.createElement("div");
  div.setAttribute("class", "d-flex flex-row justify-content-start mb-4");
  div.innerHTML = html;
  document.querySelector(".card-body").appendChild(div);
  scrollBotom();
}
function AddMyMessage(message) {
  var html = `
    <div class="p-3 mr-3 border" style="border-radius: 15px; background-color: #fbfbfb;">
        <p class="small mb-0">${message}</p>
    </div>
    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava2-bg.webp"
         alt="avatar 1" style="width: 45px; height: 100%;">
`;
  var div = document.createElement("div");
  div.setAttribute("class", "d-flex flex-row justify-content-end mb-4");
  div.innerHTML = html;
  document.querySelector(".card-body").appendChild(div);
  scrollBotom();
}
function CreateChat() {
  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  var content = document.querySelector(".form__textarea");
  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    groupID: userID,
    message: content.value,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  fetch(
    "https://localhost:7237/api/v1/Chat/SendMessageFromUser",
    requestOptions
  )
    .then((response) => response.text())
    .then((result) => {
      //scroll bottom of chat
      let json = JSON.parse(result);
      AddMyMessage(json.data);
      content.value = "";
    })
    .catch((error) => console.log("error", error));
}
document.querySelector(".form__btn").addEventListener("click", CreateChat);
scrollBotom();
function scrollBotom() {
  const containerChat = document.querySelector(".card-body");
  containerChat.scrollTop = containerChat.scrollHeight;
}
