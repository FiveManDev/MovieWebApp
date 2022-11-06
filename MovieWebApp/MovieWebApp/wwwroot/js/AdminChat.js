function SendMessageByAdmin() {
  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  var UserID = document.getElementById("UserID");
  var MyID = document.getElementById("MyID");
  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    groupID: UserID,
    adminID: MyID,
    message: "string",
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  fetch(
    "https://localhost:7237/api/v1/Chat/SendMessageFromAdmin",
    requestOptions
  )
    .then((response) => response.text())
    .then((result) => console.log(result))
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
}

//send file
function sendMessageSupport() {
  let owner = document.querySelector(".boxchat_footer");
  let boxchat_footer_sender = document.querySelector(".boxchat_footer_sender");
  if (boxchat_footer_sender) {
    boxchat_footer_sender.addEventListener("click", () => {
      let textbox = owner.querySelector(".textbox").value;
      if (textbox != "") {
        renderMyChat(`${textbox}`);
        owner.querySelector(".textbox").value = "";
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

      //   document.getElementById("UserID").id = ele.id;
      render_chatbox({
        link: `${ele.querySelector(".userID").id}`,
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
