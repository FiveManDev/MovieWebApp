var BaseUrl = "https://localhost:7237/";

function SendMessageByAdmin(content) {
  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  var UserID = document.getElementById("UserID").value;
  var MyID = document.getElementById("MyID").value;

  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");
  console.log(content);
  var raw = JSON.stringify({
    groupID: UserID,
    adminID: MyID,
    message: content,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  fetch(BaseUrl + "api/v1/Chat/SendMessageFromAdmin", requestOptions)
    .then((response) => response.json())
    .then((result) => renderMyChat(result.data))
    .catch((error) => console.log("error", error));
}

function renderMyChat(text) {
  let boxchat_inner = document.querySelector(".boxchat_inner");
  let owneruser = document.createElement("div");
  owneruser.classList.add("owneruser");
  owneruser.innerHTML = `
                        <div class="owneruser_chat">
                            <span class="owneruser_chat_message">${text}</span>
                        </div>
                    `;
  boxchat_inner.appendChild(owneruser);
  scrollBotom(".boxchat_inner");
}
function renderOtherChat(text, image) {
  let main = document.querySelector(".boxchat_inner");
  let otheruser = document.createElement("div");
  otheruser.classList.add("otheruser");
  otheruser.innerHTML = `
                        <div class="otheruser_image wh_wrap_inner">
                            <img src="${image}" alt="" class="custom_image" />
                        </div>
                        <div class="otheruser_chat">
                            <span class="otheruser_chat_message">${text}</span>
                        </div>
    `;
  main.appendChild(otheruser);
  scrollBotom(".boxchat_inner");
}

//send file
function sendMessageSupport() {
  let owner = document.querySelector(".boxchat_footer");
  let boxchat_footer_sender = document.querySelector(".boxchat_footer_sender");
  if (boxchat_footer_sender) {
    boxchat_footer_sender.addEventListener("click", () => {
      let textbox = owner.querySelector(".textbox").value;
      if (textbox != "") {
        owner.querySelector(".textbox").value = "";
        SendMessageByAdmin(textbox);
      }
    });
  }
}

/*supportsItem*/
function createPopupChatbox() {
  let supportsItems = document.querySelectorAll(".supportsItem");

  supportsItems.forEach((ele) => {
    ele.addEventListener("click", () => {
      let boxchat_container = document.querySelectorAll(".boxchat");
      let small_zoomout = document.querySelectorAll(".small_zoomout ");

      small_zoomout.forEach((ele) => {
        ele.parentNode.removeChild(ele);
      });
      boxchat_container.forEach((ele) => {
        ele.parentNode.removeChild(ele);
      });
      var userID = document.getElementById("UserID");
      if (userID.value !== "") {
        Disconnect();
      }
      userID.value = ele.id;
      HubConnect(userID.value);
      RenderChat();
      var statusElm = ele.querySelector("#chatStatus");
      var status = statusElm.textContent;
      var ticketID = statusElm.getAttribute("ticketid");
      if (status === "New") {
        UpdateStatus(ticketID, statusElm);
      }

      render_chatbox({
        link: `${ele.querySelector(".userID").id}`,
        src: `${ele.querySelector(".modelAvatar").src}`,
        title: `${ele.querySelector(".fullname").innerHTML}`,
      });
      sendMessageSupport();
      load_header_icon();
      load_small_zoomout();
      load_edit_border();
    });
  });
}
createPopupChatbox();
function RenderChat() {
  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);

  var requestOptions = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  var UserID = document.getElementById("UserID").value;
  fetch(
    BaseUrl + "api/v1/Chat/GetChatMessage?GroupID=" + UserID,
    requestOptions
  )
    .then((response) => response.json())
    .then((result) => {
      result.data.forEach((e) => {
        if (!e.isFromAdmin) {
          var parent = document.querySelector(".boxchat_container");
          var image = parent.querySelector(".custom_image").src;
          renderOtherChat(e.messageContent, image);
        } else {
          renderMyChat(e.messageContent);
        }
      });
    })
    .catch((error) => console.log("error", error));
}
var connection = new signalR.HubConnectionBuilder()
  .withUrl(BaseUrl + "chat")
  .build();

//Response
function HubOn() {
  connection.on("SendMessage", function (from, id, message,avatar) {
    //scroll bottom of chat
    var MyID = document.getElementById("MyID").value;
    if (!CompareGuid(id, MyID)) {
      var parent = document.querySelector(".boxchat_container");
      var image = parent.querySelector(".custom_image").src;
      if(from==="Admin"){
        image = avatar;
      }
      renderOtherChat(message, image);
    }
  });
}
function HubConnect(UserID) {
  connection = new signalR.HubConnectionBuilder()
    .withUrl(BaseUrl + "chat")
    .build();
  connection
    .start()
    .then(function () {
      connection.invoke("JoinGroup", UserID).then(function () {
        console.log("Connect and Join Group Success");
      });
    })
    .catch(function (err) {
      return console.error(err.toString());
    });
  HubOn();
}
function Disconnect() {
  connection.stop();
}

function CompareGuid(guid1, guid2) {
  var regExp = new RegExp(guid1, "i");

  return regExp.test(`{${guid2}}`);
}
function UpdateStatus(id, element) {
  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(BaseUrl + "api/v1/Chat/UpdateReadStatus?TicketID=" + id, requestOptions)
    .then((response) => response.json())
    .then((result) => {
      if (result.isSuccess) {
        element.classList.remove("main__table-text--green")
        element.classList.add("main__table-text--red")
        element.textContent = "Old";
      }
    })
    .catch((error) => console.log("error", error));
}
function scrollBotom(selector) {
  const containerChat = document.querySelector(`${selector}`);
  containerChat.scrollTop = containerChat.scrollHeight;
}
