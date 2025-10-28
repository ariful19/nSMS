﻿function drawPieChart(seriesData) {

    $('#container').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            renderTo: 'container'
        },
        title: {
            text: 'Population percentage city wise'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }
                }
            }
        },
        series: [{
            name: "Brands",
            colorByPoint: true,
            data: seriesData
        }]
    });
}

$(document).ready(function () {
    $("#ddlYear").on('change', function () {
        //var pData = [];
        //pData[0] = $("#ddlYear :selected").val();
        //var jsonData = JSON.stringify({ pData: pData });

        $.ajax({
            type: "POST",
            url: "Dashboard.aspx/ClassWiseStudent",
            //data: jsonData,
            data: "{pData:'" + $("#ddlYear :selected").text() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess_,
            error: OnErrorCall_
        });
        function OnSuccess_(response) {
            var aData = response.d;
            var arr = []

            $.map(aData, function (item, index) {
                var i = [item.ClassName, item.Student];
                var obj = {};
                obj.name = item.ClassName;
                obj.y = item.Student;
                arr.push(obj);
            });
            var myJsonString = JSON.stringify(arr);
            var jsonArray = JSON.parse(JSON.stringify(arr));

            drawPieChart(jsonArray);

        }
        function OnErrorCall_(response) {
            alert("Whoops something went wrong!");
        }

    });
});