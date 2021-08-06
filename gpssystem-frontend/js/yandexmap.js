var root_url = "https://localhost:44358/"
if(false){
  root_url = "https://gpsproj.azurewebsites.net/"
}

var map;
var new_destination = []
var emrName = "";
var emrAddress = "";
var emrAddress2 = "";
var emrDriver = "";

ymaps.ready(init);

cars = {}
destinations = {}
medPoints = {}

function init(){
    map = new ymaps.Map("map", {
        center: [54.87356891669103, 69.15823584672634],
        zoom: 13,
        controls: ["fullscreenControl"]
    });

    updateMap();
    updateMedPoints();
    update_driver_pos();
}

function updateMedPoints(){
  $.ajax({
    type: "POST",
    url: root_url+"api/Dispatcher/gethospitallocation",
    dataType: "json",
    contentType: "application/json",
    data: JSON.stringify({
        token: $("#token").val()
    }),
    success: function (data) {
      if(data["isSuccess"]){
        medPoints = {};
        for(let i in data["result"]){
            let m = data["result"][i]["hospitalPos"].split(", ");
            medPoints[data["result"][i]["hospitalId"]] = [m[0], m[1]];
        }
      }else{
        console.log(data["errorMessage"]);
      }
    },
    error: function () {
          alert("Server error occured. Please contact your administrator!");
    }
  });
}

function updateMap(){
    update_driver_pos();
    map.geoObjects.removeAll()

    for(let key in medPoints){
        map.geoObjects.add(new ymaps.Placemark(medPoints[key], {
          iconContent: key
        }, {
            preset: 'islands#blueMedicalCircleIcon'
        }));
    }

    for(let key in cars){
        map.geoObjects.add(new ymaps.Placemark(cars[key], {
          iconContent: key
        }, {
            preset: 'islands#redCircleIcon'
        }));
    }

    for(let key in destinations){
        map.geoObjects.add(new ymaps.Placemark(destinations[key], {
          iconContent: key
        }, {
            preset: 'islands#blueCircleIcon'
        }));
    }

    if(new_destination.length != 0){
      map.geoObjects.add(new ymaps.Placemark(new_destination, {}, {
          preset: 'islands#blueHomeIcon'
      }));
    }

    setTimeout(updateMap, 5000);
}

function update_new_dest(){
    emrName = $("#emrName").val();
    emrAddress = $("#emrAddress").val();
    emrAddress2 = $("#emrAddress2").val()
    emrDriver = $("#emrDriver").val();

    if(emrName.length==0 || emrAddress.length==0 || emrAddress2.length==0 || emrDriver == 0){
        alert("You have to fill all form to make a request!");
        return;
    }

    if(new_destination.length == 0){
      $("#icon").show();
      var xmlHttp = new XMLHttpRequest();
      url = "https://geocode-maps.yandex.ru/1.x/?apikey=25afb98b-302a-4fb2-99f2-219d6bf4c1c8";
      address = "&geocode=" + emrAddress + ", Петропавловск, Казахстан";
      params = "&format=json"
      xmlHttp.open( "GET", url+address+params, false );
      xmlHttp.send( null );
      var json_res = JSON.parse(xmlHttp.responseText);
      $("#icon").hide();
      if (json_res["statusCode"] === undefined){
        found = json_res["response"]["GeoObjectCollection"]["metaDataProperty"]["GeocoderResponseMetaData"]["found"];
        if(found == 1){
          pos = json_res["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["Point"]["pos"].split(" ");
          new_destination = [pos[1], pos[0]]

          map.geoObjects.add(new ymaps.Placemark(new_destination, {}, {
              preset: 'islands#blueHomeIcon'
          }));

          $("#emrSubmit").html("Submit");
          $("#emrSubmit").css("background-color", "#a31010")

          $('#emrName').prop('readonly', true);
          $('#emrAddress').prop('readonly', true);
          $('#emrAddress2').prop('readonly', true);
          $('#emrDriver').prop('readonly', true);
        } else if (found == 0) {
            alert("No address was found!");
        } else {
            alert("More than one address was found. Please be more specific!");
        }
      } else {
        alert("Not able to make request to a server. Please, contact your administrator!");
        reset_request();
      }
    } else {
        send_request();
        reset_request();
    }
}

function send_request(){
    let address = emrAddress + ", " + emrAddress2;
    let destination = new_destination[0] + ", " + new_destination[1];
    $.ajax({
      type: "POST",
      url: root_url+"api/Dispatcher/addrequest",
      dataType: "json",
      contentType: "application/json",
      data: JSON.stringify({
          driverId: parseInt(emrDriver, 10),
          name: emrName,
          address: address,
          addressPos: destination,
          token: $("#token").val()
      }),
      success: function (data) {
          if(!data["isSuccess"]){
            console.log(data["errorMessage"]);
          }
      },
      error: function () {
          alert("Server error occured. Please contact your administrator!");
      }
    });
}

function update_driver_pos(){
  $.ajax({
    type: "POST",
    url: root_url+"api/Dispatcher/getcurrentmap",
    dataType: "json",
    contentType: "application/json",
    data: JSON.stringify({
        token: $("#token").val()
    }),
    success: function (data) {
      if(data["isSuccess"]){
        cars = {};
        destinations = {};
        for(let i in data["result"]){
            let c = data["result"][i]["driverPos"].split(", ");
            let d = data["result"][i]["addressPos"].split(", ");
            cars[data["result"][i]["driverId"]] = [c[0], c[1]];
            destinations[data["result"][i]["driverId"]] = [d[0], d[1]];
        }
      }else{
        console.log(data["errorMessage"]);
      }
    },
    error: function () {
          alert("Server error occured. Please contact your administrator!");
    }
  });
}

function reset_request(){
    new_destination = []
    emrName = "";
    emrAddress = "";
    emrAddress2 = "";
    emrDriver = "";

    $("#emrSubmit").html("Find address");
    $("#emrSubmit").css("background-color", "#519c6a")
    $("#emrName").val("");
    $("#emrAddress").val("");
    $("#emrAddress2").val("");
    $("#emrDriver").val("0");
    updateMap();
    $('#emrName').prop('readonly', false);
    $('#emrAddress').prop('readonly', false);
    $('#emrAddress2').prop('readonly', false);
    $('#emrDriver').prop('readonly', false);

    update_driver_list();
}
