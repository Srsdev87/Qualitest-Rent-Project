



function onSubmitUser() {
    alert("user submited");
}

function watchMovie(val) {
    var user = getCookie("UserToken");

    if (user !== "") {
        var moviename = val.split('-');
        var splittermoviename = moviename[1];
    
        var username = document.getElementById("logIDlogin").innerHTML;

        var rentObj = {
            UserName: username,
            MovieName: splittermoviename
        };
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "http://localhost:60205/Home/SetRentOrder/",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false,
            data: JSON.stringify(rentObj)
        });
        ajaxRequest.done(function (xhr, textStatus) {
            console.log("ok " + textStatus);
            alert("Movie Rented!");
        }, function (response) {
            alert("Movie Rented!");
        }); 
    } else {
        window.location.href = 'http://localhost:60205/Home/Login';
    }
  
}








function show(input) {
    if (input.files && input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function(e) {
            $('#user_img').attr('src', e.target.result);
        };
        filerdr.readAsDataURL(input.files[0]);
    }
};

function showuser(input) {
    if (input.files && input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function (e) {
            $('#user_img1').attr('src', e.target.result);
        }
        filerdr.readAsDataURL(input.files[0]);
    }
};

function getPicture() {
    var data = new FormData();
    var ajaxRequest = $.ajax({
        type: "GET",
        url: "http://localhost:60205/Home/ShowMoviePic/?moviename=" + document.getElementsByName("Name")[1].value,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);
    },
        function (response) {
            document.getElementById("user_img").src = response;
        });
}

function getUserPicture() {
    var data = new FormData();
    var ajaxRequest = $.ajax({
        type: "GET",
        url: "http://localhost:60205/Home/ShowUserPic/?username=" + document.getElementsByName("UName")[1].value,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);
    },
        function (response) {
            document.getElementById("user_img1").src = response;
        });
}

function UpdateMovie() {
   
  
    var updateMovie = {
        Id: document.getElementById("movieId").value,
        MoviePicture: document.getElementById("file").value,
        MovieName: document.getElementById("txt_name").value,
        MovieBody: document.getElementById("mdescription").value,
        Category: document.getElementById("categoryid").value,
        Country: document.getElementById("countryid").value,
        Year: document.getElementById("selyear").value,
        Quality: document.getElementById("qualityid").value,
        Length: document.getElementById("mlength").value
    };
   var ajaxRequest = $.ajax({
        type: "POST",
        url: "http://localhost:60205/Home/UpdateMovie/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        data: JSON.stringify(updateMovie)
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);

    });
    document.getElementById("user_img").src = "";
    document.getElementById("file").value = "";
    document.getElementById("txt_name").value = "";
    document.getElementById("mdescription").value = "";
    document.getElementById("categoryid").value = "";
    document.getElementById("countryid").value = "";
    document.getElementById("selyear").value = "";
    document.getElementById("qualityid").value = "";
    document.getElementById("mlength").value = "";
    alert("Movie Updated!");
   
}

function updateUser() {
    var data = new FormData();

    var updateuser = {
        id: document.getElementById("userId").value,
        Picture: document.getElementById("file1").value,
        Name: document.getElementById("uname").value,
        LastName: document.getElementById("ulname").value,
        Username: document.getElementById("uuname").value,
        Email: document.getElementById("umail").value,
        Password: document.getElementById("upass").value,
        Isadmin: document.getElementById("uisadmin").value,
      
    };
    var ajaxRequest = $.ajax({
        type: "POST",
        url: "http://localhost:60205/Home/UpdateUser/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        data: JSON.stringify(updateuser)
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);

    });
    document.getElementById("user_img1").src = "";
    document.getElementById("file1").value = "";
    document.getElementById("uname").value = "";
    document.getElementById("ulname").value = "";
    document.getElementById("uuname").value = "";
    document.getElementById("umail").value = "";
    document.getElementById("upass").value = "";
    document.getElementById("uisadmin").value = "";
    alert("User Updated!");
};

function getMovieDetails() {
    var data = new FormData();
    var ajaxRequest = $.ajax({
        type: "GET",
        url: "http://localhost:60205/Home/ShowMovieDetails/?moviename=" + document.getElementsByName("Name")[1].value,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);
    },
        function (response) {
            var obj = response.split(':');
            document.getElementById("user_img").src = obj[0];
            document.getElementById("txt_name").value = obj[1];
            document.getElementById("mdescription").value = obj[2];
            document.getElementById("selyear").value = obj[3];
            document.getElementById("countryid").value = obj[4];
            document.getElementById("categoryid").value = obj[5];
            document.getElementById("qualityid").value = obj[6];
            document.getElementById("mlength").value = obj[7] + ":" + obj[8];
            document.getElementById("movieId").value = obj[9];
           
        });
}

function getUserDetails() {
    var data = new FormData();
    var ajaxRequest = $.ajax({
        type: "GET",
        url: "http://localhost:60205/Home/ShowUserDetails/?username=" + document.getElementsByName("UName")[1].value,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);
    },
        function (response) {
            var obj = response.split(':');
            document.getElementById("user_img1").src = obj[0];
            document.getElementById("uname").value = obj[1];
            document.getElementById("ulname").value = obj[2];
            document.getElementById("uuname").value = obj[3];
            document.getElementById("umail").value = obj[4];
            document.getElementById("uisadmin").value = obj[5];
            document.getElementById("userId").value = obj[6];
        });
}

function submitLogin() {
    var data = new FormData();
    var ajaxRequest = $.ajax({
        type: "GET",
        url: "http://localhost:60205/Home/submitlogIn/?username=" + document.getElementById("uuname").value + "&password=" + document.getElementById("upass").value,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);
    },
        function (response) {
            if (response !== "error") {
                  console.log("ok " + response);
            setCookie("UserToken", response, Date.now());
            window.location.href = 'http://localhost:60205/Home';
            } else {
                alert("password incorrect");
            }
          
        });
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie() {
    document.getElementById("logoutID").style.display = "none";
    document.getElementById("managementID").style.display = "none";
    var user = getCookie("UserToken");
    if (user !== "") {
        var data = new FormData();
        var ajaxRequest = $.ajax({
            type: "GET",
            url: "http://localhost:60205/Home/DecryptToken/?token=" + user,
            contentType: false,
            processData: false,
            data: data
        });
        ajaxRequest.done(function (xhr, textStatus) {
            console.log("ok " + textStatus);
        },
            function (response) {

                console.log("ok " + response);
        
                var decryptedToken = response.split(':');
                console.log("Username: " + decryptedToken[0]);
                console.log("Date: " + decryptedToken[1]);
                if (decryptedToken[0] !== "" || decryptedToken[0] !== null) {
             
                    document.getElementById("logIDlogin").innerHTML = decryptedToken[0];
                    document.getElementById("logIDlogin").href = "#";
                    document.getElementById("logoutID").style.display = "block";
                }

                var data = new FormData();
                var ajaxRequest = $.ajax({
                    type: "GET",
                    url: "http://localhost:60205/Home/IsAdminCheck/?username=" + decryptedToken[0],
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function(xhr, textStatus) {
                        console.log("ok " + textStatus);
                    },
                    function (response) {
                        var isadmin = response.split('_');
                        if (isadmin[1] === "true") {
                            document.getElementById("managementID").style.display = "block";
                        } else {
                            document.getElementById("managementID").style.display = "none";
                        }
                    });
            });
    }
}


function logOut() {
    document.cookie = "UserToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    window.location.href = 'http://localhost:60205/Home/';
}


function getMovieList(name) {
  
   
    var data = new FormData();
    var ajaxRequest = $.ajax({
        type: "GET",
        url: "http://localhost:60205/Home/GetUserOrders/?username=" + name,
        contentType: false,
        processData: false,
        data: data
    });
    ajaxRequest.done(function (xhr, textStatus) {
        console.log("ok " + textStatus);
    },
        function (response) {
            console.log(JSON.stringify(response));
            var splitresponse = response.split(",");
            $("ol").empty();
            splitresponse.forEach(function (element) {
               
                $("ol").append("<li class=\"form-control\">"+element+"</li>");
            });
           
        });
}








