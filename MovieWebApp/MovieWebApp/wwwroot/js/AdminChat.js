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
        "groupID": UserID,
        "adminID": MyID,
        "message": "string"
    });

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };

    fetch("https://localhost:7237/api/v1/Chat/SendMessageFromAdmin", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));
}


function renderMyChat(text) {

}
function renderOtherChat(text, image) {

}
