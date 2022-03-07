var retriesVsLevelGraph1 = [];
var pathSelectionVsLevelGraph2 = [];
var timeTakenVsLevelGraph3 = [];
var distanceTravelledVsLevelGraph4 = [];
var modeOfDeathVsLevelGraph5 = [];
var powerupsCollectedVsLevelGraph6 = [];

$(document).ready(function () {
  $("#analytics-form-data").submit((e) => {
    // avoid to execute the actual submit of the form
    e.preventDefault();

    // get raw data entered by the user
    var form = $("#analytics-form-data");
    data = form.serializeArray()[0].value;

    data.replaceAll("/\r?\n/g", "");

    // format string to get valid json array
    data =
      "[" +
      data.replaceAll('"type":"custom"}', '"type":"custom"},').slice(0, -1) +
      "]";

    data = JSON.parse(data);
    console.log("This is formatted data as a JSON array: ", data);

    // traverse json array to get 6 analytics specific data
    for (var i = 0; i < data.length; i++) {
      const analyticsName = data[i].name;

      if (analyticsName === "Level_Death_Analytics") {
        retriesVsLevelGraph1.push(data[i]);
      } else if (analyticsName === "Path_Selection_Analytics") {
        pathSelectionVsLevelGraph2.push(data[i]);
      } else if (analyticsName === "Total_Time_Analytics") {
        timeTakenVsLevelGraph3.push(data[i]);
      } else if (analyticsName === "Distance_Travelled_Analytics") {
        distanceTravelledVsLevelGraph4.push(data[i]);
      } else if (analyticsName === "Mode_Of_Death_Analytics") {
        modeOfDeathVsLevelGraph5.push(data[i]);
      } else if (analyticsName === "PowerUps_Count_Analytics") {
        powerupsCollectedVsLevelGraph6.push(data[i]);
      }
    }

    CreateHighChartForRetriesVSLevel(retriesVsLevelGraph1);
    CreateHighChartForPathChosenVSLevel(pathSelectionVsLevelGraph2);
    CreateHighChartForTimeTakenVsLevel(timeTakenVsLevelGraph3);
    CreateHighChartForDistanceTravelledVsLevel(distanceTravelledVsLevelGraph4);
    CreateHighChartForModeOfDeathVsLevel(modeOfDeathVsLevelGraph5);
    CreateHighChartForPowerupsCollectedVsLevel(powerupsCollectedVsLevelGraph6);
  });
});

function CreateHighChartForRetriesVSLevel(data) {
  var levels = [0, 0, 0];
  var count = [0, 0, 0];

  data.forEach((d) => {
    levels[parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Retries
    );
    count[parseInt(d.custom_params.Level) - 1] += 1;
  });

  // Comment this for-loop if you don't want the average calculation
  for (var i = 0; i < 3; i++) {
    console.log("Level ", levels[i]);
    console.log("Count ", count[i]);
    levels[i] = levels[i] / count[i];
  }

  Highcharts.chart("container1", {
    chart: {
      type: "column",
    },
    title: {
      text: "Number of Retries v/s Level",
    },
    subtitle: {
      text: "Tracks the number of tries it took for a player to complete a particular level",
    },
    xAxis: {
      categories: ["1", "2", "3"],
      title: {
        text: "Levels",
      },
    },
    yAxis: {
      min: 0,
      title: {
        text: "Number of Retries",
      },
    },
    plotOptions: {
      column: {
        pointPadding: 0,
        borderWidth: 0,
      },
    },
    series: [
      {
        name: "Retries",
        data: levels,
      },
    ],
    colors: ["#ec4040"],
  });
}

function CreateHighChartForPathChosenVSLevel(data) {
  var levels = [
    [0, 0, 0],
    [0, 0, 0],
    [0, 0, 0],
  ];

  var count = [
    [0, 0, 0],
    [0, 0, 0],
    [0, 0, 0],
  ];

  data.forEach((d) => {
    levels[0][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Red_Path
    );
    count[0][parseInt(d.custom_params.Level) - 1] += 1;

    levels[1][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Green_Path
    );
    count[1][parseInt(d.custom_params.Level) - 1] += 1;

    levels[2][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Blue_Path
    );
    count[2][parseInt(d.custom_params.Level) - 1] += 1;
  });

  // Comment this for-loop if you don't want the average calculation
  for (var i = 0; i < 3; i++) {
    console.log("Path:", levels.toString());
    console.log("Count:", count.toString());
    for (var j = 0; j < 3; j++) {
      levels[i][j] = levels[i][j] / count[i][j];
    }
  }

  Highcharts.chart("container2", {
    chart: {
      type: "column",
    },
    title: {
      text: "Number of times path is chosen v/s Level",
    },
    subtitle: {
      text: "Tracks the different path choices a player makes while playing the game",
    },
    xAxis: {
      categories: ["1", "2", "3"],
      title: {
        text: "Levels",
      },
    },
    yAxis: {
      min: 0,
      title: {
        text: "Number of times path is chosen",
      },
    },
    plotOptions: {
      column: {
        pointPadding: 0,
        borderWidth: 0,
      },
    },
    series: [
      {
        name: "Red",
        data: levels[0],
      },
      {
        name: "Green",
        data: levels[1],
      },
      {
        name: "Blue",
        data: levels[2],
      },
    ],
    colors: ["#ec4040", "#69ec40", "#40a6ec"],
  });
}

function CreateHighChartForTimeTakenVsLevel(data) {
  var levels = [0, 0, 0];
  var count = [0, 0, 0];

  data.forEach((d) => {
    levels[parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Time
    );
    count[parseInt(d.custom_params.Level) - 1] += 1;
  });

  // Comment this for-loop if you don't want the average calculation
  for (var i = 0; i < 3; i++) {
    console.log("Level ", levels[i]);
    console.log("Count ", count[i]);
    levels[i] = levels[i] / count[i];
  }

  Highcharts.chart("container3", {
    chart: {
      type: "column",
    },
    title: {
      text: "Time taken by the player v/s Level",
    },
    subtitle: {
      text: "Tracks the time taken by the player to complete each level",
    },
    xAxis: {
      categories: ["1", "2", "3"],
      title: {
        text: "Levels",
      },
    },
    yAxis: {
      min: 0,
      title: {
        text: "Time taken by the player to complete a level",
      },
    },
    plotOptions: {
      column: {
        pointPadding: 0,
        borderWidth: 0,
      },
    },
    series: [
      {
        name: "",
        data: levels,
      },
    ],
    colors: ["#FF0000"],
  });
}

function CreateHighChartForDistanceTravelledVsLevel(data) {
  var levels = [0, 0, 0];
  var count = [0, 0, 0];

  data.forEach((d) => {
    levels[parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Distance
    );
    count[parseInt(d.custom_params.Level) - 1] += 1;
  });

  // Comment this for-loop if you don't want the average calculation
  for (var i = 0; i < 3; i++) {
    console.log("Level ", levels[i]);
    console.log("Count ", count[i]);
    levels[i] = levels[i] / count[i];
  }

  Highcharts.chart("container4", {
    chart: {
      type: "column",
    },
    title: {
      text: "Distance travelled by the player v/s Level",
    },
    subtitle: {
      text: "Tracks the distance travelled by the player in each level",
    },
    xAxis: {
      categories: ["1", "2", "3"],
      title: {
        text: "Levels",
      },
    },
    yAxis: {
      min: 0,
      title: {
        text: "Distance travelled by the player",
      },
    },
    plotOptions: {
      column: {
        pointPadding: 0,
        borderWidth: 0,
      },
    },
    series: [
      {
        name: "Distance",
        data: levels,
      },
    ],
    colors: ["#008000"],
  });
}

function CreateHighChartForModeOfDeathVsLevel(data) {
  console.log("CreateHighChartForModeOfDeathVsLevel called");
  var levels = [
    [0, 0, 0],
    [0, 0, 0],
    [0, 0, 0],
    [0, 0, 0],
  ];

  data.forEach((d) => {
    levels[0][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Obstacle
    );
    levels[1][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Yellow_Path
    );
    levels[2][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Free_Fall
    );
    levels[3][parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.Out_Of_Time
    );
  });
  console.log(levels);

  Highcharts.chart("container5", {
    chart: {
      type: "column",
    },
    title: {
      text: "Mode of death v/s Level",
    },
    subtitle: {
      text: "Tracks the mode of death while playing the game",
    },
    xAxis: {
      categories: ["1", "2", "3"],
      title: {
        text: "Levels",
      },
    },
    yAxis: {
      min: 0,
      title: {
        text: "Number of Deaths",
      },
    },
    plotOptions: {
      column: {
        pointPadding: 0,
        borderWidth: 0,
      },
    },
    series: [
      {
        name: "Obstacle",
        data: levels[0],
      },
      {
        name: "Yellow Path",
        data: levels[1],
      },
      {
        name: "Free Fall",
        data: levels[2],
      },
      {
        name: "Out Of Time",
        data: levels[3],
      },
    ],
    colors: ["#f07c1d", "#f7c50f", "#087844", "#803808"],
  });
}

function CreateHighChartForPowerupsCollectedVsLevel(data) {
  console.log("CreateHighChartForPowerupsCollectedVsLevel called!");
  var levels = [0, 0, 0];
  var count = [0, 0, 0];

  data.forEach((d) => {
    levels[parseInt(d.custom_params.Level) - 1] += parseInt(
      d.custom_params.PowerUps_Count
    );
    count[parseInt(d.custom_params.Level) - 1] += 1;
  });

  // Comment this for-loop if you don't want the average calculation
  for (var i = 0; i < 3; i++) {
    console.log("Level ", levels[i]);
    console.log("Count ", count[i]);
    levels[i] = Math.ceil(levels[i] / count[i]);
  }

  console.log(levels);
  Highcharts.chart("container6", {
    chart: {
      type: "column",
    },
    title: {
      text: "Number of Power-Ups collected v/s Level",
    },
    subtitle: {
      text: "Tracks the number of power-ups collected while playing the game on each level",
    },
    xAxis: {
      categories: ["1", "2", "3"],
      title: {
        text: "Levels",
      },
    },
    yAxis: {
      min: 0,
      title: {
        text: "Number of power-ups collected",
      },
    },
    plotOptions: {
      column: {
        pointPadding: 0,
        borderWidth: 0,
      },
    },
    series: [
      {
        name: "Power-Ups",
        data: levels,
      },
    ],
    colors: ["#40a6ec"],
  });
}
