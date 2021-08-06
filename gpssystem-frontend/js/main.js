var root_url = "https://localhost:44358/"
var progressListCache = "";
if(false){
  root_url = "https://gpsproj.azurewebsites.net/"
}

let comin_back_status = '<center><span class="badge bg-success">Coming back</span></center>';
let arrived_status = '<center><span class="badge bg-warning text-dark">Arrived</span></center>';
let in_progress_status = '<center><span class="badge bg-danger ">In progress</span></center>';
let unknown_status = '<center><span class="badge bg-secondary">Unknown</span>';

$(document).ready(function() {
  $.ajax({
    type: "POST",
    url: root_url+"api/Dispatcher/checktoken",
    dataType: "json",
    contentType: "application/json",
    data: JSON.stringify({
        token: $("#token").val()
    }),
    success: function (data) {
        console.log("user authentification: " + data);
        if(data==false){
          window.location.href = "login.php";
        }
    },
    error: function () {
          //redirect to error page
    }
  });

  $( "#emrReset" ).click(function() {
    reset_request();
  });

  $( "#emrAddress" ).click(function() {
    update_driver_list();
  });

  update_driver_list();
  update_progress_list();
});


function update_driver_list(){
  //console.log("update_driver_list()");
  $.ajax({
    type: "POST",
    url: root_url+"api/Dispatcher/getavaliabledrivers",
    dataType: "json",
    contentType: "application/json",
    data: JSON.stringify({
        token: $("#token").val()
    }),
    success: function (data) {
      if(data["isSuccess"]){
        //console.log(data);
        $("#emrDriver").empty();
        if(data["result"].length == 0){
          $("#emrDriver").append(new Option("No avaliable drivers", 0));
        } else {
          for(let i in data["result"]){
            $("#emrDriver").append(new Option(data["result"][i]["driverId"], data["result"][i]["driverId"]));
          }
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

function update_progress_list(){
  $.ajax({
    type: "POST",
    url: root_url+"api/Dispatcher/getrequests",
    dataType: "json",
    contentType: "application/json",
    data: JSON.stringify({
        token: $("#token").val()
    }),
    success: function (data) {
      if(data["isSuccess"]){
        if(JSON.stringify(data["result"]) !== progressListCache){
          update_driver_list();
          progressListCache = JSON.stringify(data["result"]);
        }
        let f = "";
        if(data["result"].length > 0){
          let pgrCard1 = '<div class="container prgCard"><div class="row"><div class="col-sm-8"><span class="badge bg-secondary">'; // driver_id
          let pgrCard2 = '</span> <span>'; //Imangali Turar
          let pgrCard3 = '</span><br><span>'; //Auezov 23, 1a-54
          let pgrCard4 = '</span></div><div class="col-sm-4"><center>'; //21:23
          let pgrCard5 = '</div></div></div>'
          for(let i in data["result"]){
              f += pgrCard1 + data["result"][i]["driverId"];
              f += pgrCard2 + data["result"][i]["clientName"];
              f += pgrCard3 + data["result"][i]["addressName"];
              let minDiff = getTimeDiffInMins(new Date(data["result"][i]["startTime"]));
              f += pgrCard4 + minDiff;
              if(data["result"][i]["status"] == "In progress"){
                f+=in_progress_status;
              }else if(data["result"][i]["status"] == "Arrived"){
                f+=arrived_status;
              } else if(data["result"][i]["status"]== "Coming back"){
                f+=comin_back_status;
              } else{
                f+=unknown_status;
              }
              f += pgrCard5;
          }
        } else {
          f = '<div class="container prgCard"><div class="row"><div class="col-sm-12"><center>No requests in progress</center></div></div></div>';
        }

        $("#prgContent").html(f);
      }else{
        console.log(data["errorMessage"]);
      }
    },
    error: function () {
          alert("Server error occured. Please contact your administrator!");
    }
  });

  setTimeout(update_progress_list, 5000);
}

function parseDate(stringDate){
  let day = parseInt(stringDate.slice(0,2));
  let month = parseInt(stringDate.slice(3,5))-1;
  let year = parseInt(stringDate.slice(6,10));
  let ap = stringDate.slice(-2);

  let time = stringDate.slice(11,19).split(":");
  let h = parseInt(time[0]);
  let m = parseInt(time[1]);
  let s = parseInt(time[2]);

  if(ap=='pm' && h < 12){
    h = h+12;
  } else if(ap=='am' && h==12){
    h = 0;
  }

  return new Date(Date.UTC(year, month, day, h, m, s));
}

function getTimeDiffInMins(date){
  let offset = 28800000;
  let mm = new Date() - date - offset;
  let mins = Math.floor((mm/1000/60) << 0);

  if(mins == 0){
    return "New order";
  } else {
    return mins + " min.";
  }
}
