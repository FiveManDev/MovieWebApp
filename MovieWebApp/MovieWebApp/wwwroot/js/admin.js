$(document).ready(function () {
  "use strict"; // start of use strict

  /*==============================
    Menu
    ==============================*/
  $(".header__btn").on("click", function () {
    $(this).toggleClass("header__btn--active");
    $(".header").toggleClass("header--active");
    $(".sidebar").toggleClass("sidebar--active");
  });

  /*==============================
    Filter
    ==============================*/
  $(".filter__item-menu li").each(function () {
    $(this).attr("data-value", $(this).text().toLowerCase());
  });

  $(".filter__item-menu li").on("click", function () {
    var text = $(this).text();
    var item = $(this);
    var id = item.closest(".filter").attr("id");
    $("#" + id)
      .find(".filter__item-btn input")
      .val(text);
  });

  /*==============================
    Tabs
    ==============================*/
  $(".profile__mobile-tabs-menu li").each(function () {
    $(this).attr("data-value", $(this).text().toLowerCase());
  });

  $(".profile__mobile-tabs-menu li").on("click", function () {
    var text = $(this).text();
    var item = $(this);
    var id = item.closest(".profile__mobile-tabs").attr("id");
    $("#" + id)
      .find(".profile__mobile-tabs-btn input")
      .val(text);
  });

  /*==============================
    Modal
    ==============================*/
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

  /*==============================
    Select2
    ==============================*/
  $("#quality").select2({
    placeholder: "Choose quality",
    allowClear: true,
  });

  $("#country").select2({
    placeholder: "Choose country / countries",
  });

  $("#genre").select2({
    placeholder: "Choose genre / genres",
  });

  $("#classMovie").select2({
    placeholder: "Choose class",
  });

  $("#subscription, #rights").select2();

  /*==============================
    Upload cover
    ==============================*/
  function readURL(input) {
    if (input.files && input.files[0]) {
      var reader = new FileReader();

      reader.onload = function (e) {
        $("#form__img").attr("src", e.target.result);
      };

      reader.readAsDataURL(input.files[0]);
    }
  }

  $("#form__img-upload").on("change", function () {
    readURL(this);
  });

  /*==============================
    Upload video
    ==============================*/
  $(".form__video-upload").on("change", function () {
    var videoLabel = $(this).attr("data-name");

    if ($(this).val() != "") {
      $(videoLabel).text($(this)[0].files[0].name);
    } else {
      $(videoLabel).text("Upload video");
    }
  });

  /*==============================
    Upload gallery
    ==============================*/
  $(".form__gallery-upload").on("change", function () {
    var length = $(this).get(0).files.length;
    var galleryLabel = $(this).attr("data-name");

    if (length > 1) {
      $(galleryLabel).text(length + " files selected");
    } else {
      $(galleryLabel).text($(this)[0].files[0].name);
    }
  });

  /*==============================
    Scroll bar
    ==============================*/
  $(".scrollbar-dropdown").mCustomScrollbar({
    axis: "y",
    scrollbarPosition: "outside",
    theme: "custom-bar",
  });

  $(".main__table-wrap").mCustomScrollbar({
    axis: "x",
    scrollbarPosition: "outside",
    theme: "custom-bar2",
    advanced: {
      autoExpandHorizontalScroll: true,
    },
  });

  $(".dashbox__table-wrap").mCustomScrollbar({
    axis: "x",
    scrollbarPosition: "outside",
    theme: "custom-bar3",
    advanced: {
      autoExpandHorizontalScroll: 2,
    },
  });

  /*==============================
    Bg
    ==============================*/
  $(".section--bg").each(function () {
    if ($(this).attr("data-bg")) {
      $(this).css({
        background: "url(" + $(this).data("bg") + ")",
        "background-position": "center center",
        "background-repeat": "no-repeat",
        "background-size": "cover",
      });
    }
  });
});

/*Chat*/
function load_header_icon() {
  let boxchat = document.querySelectorAll(".boxchat");

  boxchat.forEach((ele) => {
    let exit = ele.querySelector(".exit");
    let zoomout = ele.querySelector(".zoomout");

    exit.addEventListener("click", () => {
      ele.style.display = "none";
    });

    zoomout.addEventListener("click", () => {
      ele.style.display = "none";
      let linkUrl = ele
        .querySelector(".boxchat_header_title a")
        .getAttribute("href");
      let n = linkUrl.lastIndexOf("/");
      let userhref = "userhref" + linkUrl.substring(n + 1);
      let small_zoomout = document.querySelectorAll(".small_zoomout");
      small_zoomout.forEach((e) => {
        let small_zoomout_id = e.classList.contains(userhref);
        if (small_zoomout_id == true) {
          e.style.display = "block";
        }
      });

      if (document.querySelector(`.${userhref}`) == null) {
        let img = ele.querySelector(".custom_image").getAttribute("src");
        let title = ele.querySelector(
          ".boxchat_header_title_username"
        ).innerHTML;

        render_small_zoomout_icon({
          link: `${userhref}`,
          src: `${img}`,
          title: `${title}`,
        });
        document.querySelector(`.${userhref}`).style.display = "block";
        load_small_zoomout();
      }
    });
  });
}
load_header_icon();
function load_small_zoomout() {
  let small_zoomout = document.querySelectorAll(".small_zoomout");
  small_zoomout.forEach((ele) => {
    ele.querySelector(".small_zoomout_img").addEventListener("click", () => {
      let small_zoomout_img = ele
        .querySelector(".small_zoomout_img")
        .getAttribute("src");
      let boxchat_img = document.querySelectorAll(
        ".boxchat_header_title_img .custom_image"
      );
      boxchat_img.forEach((e) => {
        if (e.getAttribute("src") == small_zoomout_img) {
          e.parentNode.parentNode.parentNode.parentNode.parentNode.style.display =
            "block";
          ele.style.display = "none";
        }
      });
    });
    ele.querySelector(".small_zoomout_icon").onclick = () => {
      ele.style.display = "none";
    };
  });
}
load_small_zoomout();
function load_edit_border() {
  let otheruser_chat = document.querySelectorAll(".otheruser_chat");
  let owneruser_chat = document.querySelectorAll(".owneruser_chat");

  otheruser_chat.forEach((ele) => {
    let otheruser_chat_message = ele.querySelectorAll(
      ".otheruser_chat_message"
    );

    if (otheruser_chat_message.length === 2) {
      otheruser_chat_message[0].style.borderBottomLeftRadius = "4px";
      otheruser_chat_message[1].style.borderTopLeftRadius = "4px";
    } else if (otheruser_chat_message.length > 2) {
      otheruser_chat_message.forEach((ele, index) => {
        if (index == 0) {
          otheruser_chat_message[0].style.borderBottomLeftRadius = "4px";
        } else if (index == otheruser_chat_message.length - 1) {
          otheruser_chat_message[index].style.borderTopLeftRadius = "4px";
        } else {
          otheruser_chat_message[index].style.borderBottomLeftRadius = "4px";
          otheruser_chat_message[index].style.borderTopLeftRadius = "4px";
        }
      });
    }
  });
  owneruser_chat.forEach((ele) => {
    let owneruser_chat_message = ele.querySelectorAll(
      ".owneruser_chat_message"
    );

    if (owneruser_chat_message.length === 2) {
      owneruser_chat_message[0].style.borderBottomRightRadius = "4px";
      owneruser_chat_message[1].style.borderTopRightRadius = "4px";
    } else if (owneruser_chat_message.length > 2) {
      owneruser_chat_message.forEach((ele, index) => {
        if (index == 0) {
          owneruser_chat_message[0].style.borderBottomRightRadius = "4px";
        } else if (index == owneruser_chat_message.length - 1) {
          owneruser_chat_message[index].style.borderTopRightRadius = "4px";
        } else {
          owneruser_chat_message[index].style.borderBottomRightRadius = "4px";
          owneruser_chat_message[index].style.borderTopRightRadius = "4px";
        }
      });
    }
  });
}
load_edit_border();

function footer_img_message() {
  let footer_img_message = document.querySelectorAll(".footer_img_message");
  footer_img_message.forEach((ele) => {
    let clearimg = ele.querySelector(".footer_img_message_icon");
    clearimg.onclick = () => {
      ele.remove();
    };
  });
}
footer_img_message();

// render small zoomout icon
function render_small_zoomout_icon({ link = "", src = "", title = "" }) {
  let small_zooomout_container = document.querySelector(
    ".small_zooomout_container"
  );
  let icon = document.createElement("div");
  icon.classList.add("small_zoomout", `${link}`);
  icon.innerHTML = `
        <img
          src="${src}"
          alt="Image load fail"
          title="${title}"
          class="small_zoomout_img"
        />
        <div class="small_zoomout_icon"><i class="fas fa-times"></i></div>
      `;
  small_zooomout_container.appendChild(icon);
}
// render chatbox
function render_chatbox({ link = "", src = "", title = "" }) {
  let main = document.querySelector(".boxchat_container");
  let boxchat = document.createElement("div");
  boxchat.classList.add("boxchat");
  boxchat.innerHTML = `
        <div class="boxchat_header">
          <div class="boxchat_header_title">
            <a href="${link}">
              <span class="boxchat_header_title_img wh_wrap_header">
                <img src="${src}" alt="" class="custom_image" />
              </span>
              <span class="boxchat_header_title_username"> ${title} </span>
            </a>
          </div>

          <div class="boxchat_header_icon">
            <span class="zoomout icon"><i class="fas fa-minus"></i></span>
            <span class="exit icon"><i class="fas fa-times"></i></span>
          </div>
        </div>

        <div class="boxchat_inner">
                   
        </div>

        <div class="boxchat_footer">
          <div class="boxchat_footer_image">
            <label for="messagefile"><i class="fas fa-images"></i>Image</label>
            <input
              type="file"
              name="messagefile"
              id="messagefile"
              accept="image/*"
            />
          </div>

          <div class="boxchat_footer_groupinput">
            <div class="boxchat_footer_input">
            <div class="boxchat_footer_input_img"> </div>
              <textarea
                name=""
                id="textbox"
                class="textbox"
                placeholder="Aa"
                rows="1"
              ></textarea>
            </div>
            <div class="boxchat_footer_sender">
              <i class="fas fa-paper-plane"></i>
            </div>
          </div>
        </div>
  `;
  main.appendChild(boxchat);
}
// render message
function render_otheruser({ src = "" }) {
  let main = document.querySelector(".boxchat_inner");
  let otheruser = document.createElement("div");
  otheruser.classList.add("otheruser");
  otheruser.innerHTML = `
            <div class="otheruser_image wh_wrap_inner">
              <img src="${src}" alt="" class="custom_image" />
            </div>
            <div class="otheruser_chat">
             
            </div>
  `;
  main.appendChild(otheruser);
}
function render_owneruser() {
  let main = document.querySelector(".boxchat_inner");
  let owneruser = document.createElement("div");
  owneruser.classList.add("owneruser");
  owneruser.innerHTML = `
            <div class="owneruser_chat">
            
            </div>
             `;
  main.appendChild(owneruser);
}
function render_owneruser_text({ title = "" }) {
  let owneruser_chat = document.querySelectorAll(".owneruser_chat");
  let owneruser_chat_message = document.createElement("div");
  owneruser_chat_message.classList.add("owneruser_chat_message");
  owneruser_chat_message.innerHTML = `${title}`;
  owneruser_chat[owneruser_chat.length - 1].appendChild(owneruser_chat_message);
}
function render_owneruser_img({ src = "" }) {
  let owneruser_chat = document.querySelectorAll(".owneruser_chat");
  let owneruser_chat_message = document.createElement("div");
  owneruser_chat_message.classList.add("owneruser_chat_message");
  owneruser_chat_message.innerHTML = `<img
  src="${src}"
  alt=""
  class="owner_chat_message_image"
/>`;
  owneruser_chat[owneruser_chat.length - 1].appendChild(owneruser_chat_message);
}

//send file

let owner = document.querySelector(".boxchat_footer");
let boxchat_footer_sender = document.querySelector(".boxchat_footer_sender");
if (boxchat_footer_sender) {
  boxchat_footer_sender.addEventListener("click", () => {
    let listimg = owner.querySelectorAll(".footer_img_message");
    let textbox = owner.querySelector(".textbox").value;
    if (listimg != null) {
      listimg.forEach((img) => {
        let src = img.querySelector("img").src;
        render_owneruser_img({ src: `${src}` });
        img.remove();
      });
    }
    if (textbox != "") {
      render_owneruser_text({ title: `${textbox}` });
      owner.querySelector(".textbox").value = "";
    }
  });
}
