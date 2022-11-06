var baseUrl = "https://localhost:7237/";

function UploadMovie() {
  var loading = document.getElementById("loading");
  var errorMessage = document.getElementById("errorMessage");
  loading.hidden = false;
  errorMessage.classList.remove("activeMsg");

  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  let userId = document.getElementById("UserID").value;
  var coverImage = document.getElementById("form__img-upload").files[0];
  var title = document.getElementById("movieTitle").value;
  var des = document.getElementById("text").value;
  var releaseYear = document.getElementById("releaseYear").value;
  var date = new Date(releaseYear);
  var runningTime = document.getElementById("runningTime").value;
  var quality = document.getElementById("quality").value;
  var age = document.getElementById("age").value;
  var country = document.getElementById("country").value;
  var genre = document
    .getElementById("genre")
    .parentNode.querySelectorAll(".select2-selection__choice");
  var classMovie = document.getElementById("classMovie").value;
  var thumbnail = document.getElementById("form__gallery-upload").files[0];
  var movie = document.getElementById("form__video-upload").files[0];
  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);

  var formdata = new FormData();
  formdata.append("MovieName", title);
  formdata.append("Description", des);
  formdata.append("Thumbnail", thumbnail);
  formdata.append("Country", country);
  formdata.append("Actor", "Null");
  formdata.append("Director", "Null");
  formdata.append("Language", country);
  formdata.append("Subtitle", "VietNam");
  formdata.append("ReleaseTime", date.toJSON());
  formdata.append("CoverImage", coverImage);
  formdata.append("Age", age);
  formdata.append("Movie", movie);
  formdata.append("RunningTime", runningTime);
  formdata.append("Quality", quality);
  formdata.append("UserID", userId);
  formdata.append("ClassName", classMovie);
  formdata.append("MovieTypeName", "Movie Theater");
  genre.forEach((g) => {
    formdata.append("GenreName", g.getAttribute("title"));
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: formdata,
    redirect: "follow",
  };

  fetch(baseUrl + "api/v1/Movie/PostMovie", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      loading.hidden = true;
      if (result.isSuccess) {
        toastr.success("Upload video successfully!");
      } else {
        toastr.error("Upload video fail!");
        /*errorMessage.classList.add("activeMsg");*/
      }
    })
    .catch((error) => console.log("error", error));
}

function UpdateMovie() {
  loading.hidden = false;
  var token = document.getElementById("Token").value;
  if (token === "") {
    window.location = "/login";
  }
  let userId = document.getElementById("UserID").value;
  var movieID = document.getElementById("MovieID").value;
  var coverImage = document.getElementById("form__img-upload").files[0];
  var title = document.getElementById("movieTitle").value;
  var des = document.getElementById("text").value;
  var releaseYear = document.getElementById("releaseYear").value;
  var date = new Date(releaseYear);
  var runningTime = document.getElementById("runningTime").value;
  var quality = document.getElementById("quality").value;
  var age = document.getElementById("age").value;
  var country = document.getElementById("country").value;
  var genre = document
    .getElementById("genre")
    .parentNode.querySelectorAll(".select2-selection__choice");
  var classMovie = document.getElementById("classMovie").value;
  var thumbnail = document.getElementById("form__gallery-upload").files[0];
  var movie = document.getElementById("form__video-upload").files[0];
  var myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);

  var formdata = new FormData();
  formdata.append("MovieID", movieID);
  formdata.append("MovieName", title);
  formdata.append("Description", des);
  formdata.append("Thumbnail", thumbnail);
  formdata.append("Country", country);
  formdata.append("Actor", "Null");
  formdata.append("Director", "Null");
  formdata.append("Language", country);
  formdata.append("Subtitle", "Viet Nam");
  formdata.append("ReleaseTime", date.toJSON());
  formdata.append("CoverImage", coverImage);
  formdata.append("Age", age);
  formdata.append("Movie", movie);
  formdata.append("RunningTime", runningTime);
  formdata.append("Quality", quality);
  formdata.append("UserID", userId);
  formdata.append("ClassName", classMovie);
  formdata.append("MovieTypeName", "Movie Theater");
  genre.forEach((g) => {
    formdata.append("GenreName", g.getAttribute("title"));
  });

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    body: formdata,
    redirect: "follow",
  };

  fetch(baseUrl + "api/v1/Movie/UpdateMovie", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      loading.hidden = true;
      if (result.isSuccess) {
        toastr.success("Update video successfully!");
      } else {
        toastr.error("Update video fail!");
      }
    })
    .catch((error) => console.log("error", error));
}
