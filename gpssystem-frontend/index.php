<?php
if(!isset($_COOKIE["token"])){
  $_COOKIE["token"] = "";
}
?>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Main page</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js" integrity="sha384-ygbV9kiqUc6oa4msXn9868pTtWMgiQaeYH7/t7LECLbyPA2x65Kgf80OJFdroafW" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://api-maps.yandex.ru/2.1/?apikey=25afb98b-302a-4fb2-99f2-219d6bf4c1c8&lang=en_US" type="text/javascript"></script>
    <script src="js/yandexmap.js" type="text/javascript"></script>
    <script src="js/main.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet">
</head>
<body>
  <input type="hidden" id="token" value="<?php echo $_COOKIE["token"]?>">
  <div class="container">
  <div class="row" style="height: 100vh">
    <div style="border: 1px solid black; width: 20%;margin: 0px; padding: 0px;">
        <div>
          <div class="menuHead">Request Emergency <small id="emrReset">(reset)</small></div>
          <div>
              <input id="emrName" class="emrForm" type="text" name="name" placeholder="Name" autocomplete="off">
              <input id="emrAddress" class="emrForm" type="text" name="address" placeholder="Address" autocomplete="off">
              <input id="emrAddress2" class="emrForm" type="text" name="address2" placeholder="Address II" autocomplete="off">
              <select class="emrForm" name="driver" id="emrDriver">
                <option value="0" selected data-default>Select a driver</option>
              </select>
              <center><img src="https://www.appcoda.com/learnswiftui/images/animation/swiftui-animation-8.gif" id="icon" alt="User Icon" /></center>
              <button id="emrSubmit" onclick="update_new_dest()">Find address</button>
          </div>
        </div>

        <div>
          <div class="menuHead" style="margin-top: 10px">Requests in Progress</div>
          <div id="prgContent"></div>
        </div>
    </div>
    <div style="width: 80%;">
      <div id="map" style="width: 100%; height: 100%"></div>
    </div>
  </div>
  </div>
</body>
</html>
