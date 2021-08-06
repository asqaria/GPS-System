<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login page</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" integrity="sha384-ygbV9kiqUc6oa4msXn9868pTtWMgiQaeYH7/t7LECLbyPA2x65Kgf80OJFdroafW" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="css/login.css" rel="stylesheet" id="bootstrap-css">
</head>
<body>
    <script type="text/javascript">
      var root_url = "https://localhost:44358/"
      if(false){
        root_url = "https://gpsproj.azurewebsites.net/"
      }

      $(document).ready(function() {
        $("#icon").hide();
        $("#loginfo").hide();
        $("#servererror").hide()
        $("#loginBtn").click(function(){
            $("#icon").show();

            $.ajax({
              type: "POST",
              url: root_url+"api/Dispatcher/gettoken",
              dataType: "json",
              contentType: "application/json",
              data: JSON.stringify({
                  email: $("#login").val(),
                  password: $("#password").val()
              }),
              success: function (data) {
                  if(data.result==""){
                    $("#loginfo").show();
                  }else{
                    console.log("Logged in!");
                    document.cookie = "token="+data.result;
                    window.location.href = "http://localhost/gps/";

                    $("#loginfo").hide();
                    $("#servererror").hide()
                  }
                  $("#icon").hide();
              },
              error: function () {
                  $("#servererror").show();
                  $("#icon").hide();
              }
            });
          });
      });
    </script>


    <div class="wrapper fadeInDown">
      <div id="formContent">
        <!-- Tabs Titles -->

        <!-- Icon -->
        <div class="fadeIn first">
          <div id="loginfo" class="alert alert-primary" role="alert">Wrong username or password!</div>
          <div id="servererror" class="alert alert-primary" role="alert">Failed to connect to server!</div>
          <img src="https://aktk.in/wp-content/uploads/2020/07/articleloader.gif" id="icon" alt="User Icon" />
        </div>

        <!-- Login Form -->
        <form>
          <input type="text" id="login" class="fadeIn second" name="username" placeholder="login">
          <input type="text" id="password" class="fadeIn third" name="password" placeholder="password">
          <input type="button" id='loginBtn' name="login" class="fadeIn fourth" value="Log In">
        </form>

        <!-- Remind Passowrd
        <div id="formFooter">
          <a class="underlineHover" href="#">Forgot Password?</a>
        </div>
        -->
      </div>
    </div>

</body>
</html>
