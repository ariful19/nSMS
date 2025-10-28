<%@ Page Title="<%$ Resources:Application,Bidyapith %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblStudent" runat="server"></asp:Label></h3>
                    <p>Total Student</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person"></i>
                </div>
                <a href="../DashBoard/ShowDashboardStudent.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-green">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblTeacher" runat="server"></asp:Label></h3>

                    <p>Total Teacher</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-stalker"></i>
                </div>
                <a href="../DashBoard/ShowDashboardTeacher.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-yellow">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblOnlineUser" runat="server"></asp:Label></h3>

                    <p>User Online</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
                <a href="#" class="small-box-footer"><i class="fa fa-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-red">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblUniqueVisit" runat="server"></asp:Label></h3>

                    <p>Unique Visitors</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="#" class="small-box-footer"><i class="fa fa-circle-right"></i></a>
            </div>
        </div>

        <!-- ./col -->
    </div>
    <div class="row">
        <div class="col-sm-6">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Class Wise Student</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                    <div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                </div>
            </div>
        </div>

        <div class="col-sm-6">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Class Wise Student Bar</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <canvas id="barChart" style="height: 250px"></canvas>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-blue">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblBoys" runat="server"></asp:Label></h3>
                    <p>Total Boys</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person"></i>
                </div>
                <a href="../DashBoard/ShowDashboardStudent.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-purple">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lblGirls" runat="server"></asp:Label></h3>

                    <p>Total Girls</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-stalker"></i>
                </div>
                <a href="../DashBoard/ShowDashboardTeacher.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <%--      <div class="small-box bg-yellow">
                <div class="inner">
                    <h3>
                        <asp:Label ID="Label3" runat="server"></asp:Label></h3>

                    <p>User Online</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
                <a href="#" class="small-box-footer"><i class="fa fa-circle-right"></i></a>
            </div>--%>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <%--   <div class="small-box bg-red">
                <div class="inner">
                    <h3>
                        <asp:Label ID="Label4" runat="server"></asp:Label></h3>

                    <p>Unique Visitors</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="#" class="small-box-footer"><i class="fa fa-circle-right"></i></a>
            </div>--%>
        </div>

         <div class="modal fade" id="notifyModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body">
                        <h3 id="warn" style ="color:red"> Your account will expire after <%=Session["expireDays"]%> days due to payment dues.</h3>
                        <p>
                            Please pay all outstanding invoices as soon as possible to avoid service interruption. If payment has not been received within <%=Session["expireDays"]%> days your account with us will be suspended.
Once paid, no further action will be needed and your account will remain active.
We thank you for your continued business and assistance in helping us to get this resolved. Feel free to contact us if you have any questions, comments, or concerns.<br/>
    </p><p>
Best regards,<br/> 
Nanosoft
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ./col -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="../../Scripts/highcharts.js"></script>
    <script type="text/javascript">
        function drawPieChart(seriesData) {

            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie',
                    renderTo: 'container'
                },
                title: {
                    text: 'Class Wise Student Percentage'
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
                            format: '<b>{point.name}</b>: {point.percentage:.1f} % <br>{series.name}:{point.y}',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: "Students",
                    colorByPoint: true,
                    data: seriesData
                }]
            });
        }

        function load(year) {
            if (!year) {
                year = new Date().getFullYear();
            }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/ClassWiseStudent",
                data: "{pData:'" + year + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_,
                error: function () {
                    alert("Whoops something went wrong!");
                }
            });
            function OnSuccess_(response) {
                var aData = response.d;
                var arr = []

                for (var i in aData) {
                    var obj = { name: "", y: 0 };
                    obj.name = aData[i].Text;
                    obj.y = parseInt(aData[i].Value);
                    arr.push(obj);
                }

                var myJsonString = JSON.stringify(arr);
                var jsonArray = JSON.parse(JSON.stringify(arr));

                drawPieChart(arr);

            }
        }
        $(document).ready(function () {
            $(".select2").select2();

            load(new Date().getFullYear());
            $("#ddlYear").on('change', function () {
                load($("#ddlYear :selected").text());
            });

            var expiresInDays = '<%=Session["expireDays"]%>';
            var user = '<%=Session["IsAccountsOrAdmin"]%>';
            if (parseInt(expiresInDays) <= 15 && (user == "True")) {
                $('#notifyModal').modal({backdrop:'static',keyboard:false});
            }
        });
    </script>

</asp:Content>


