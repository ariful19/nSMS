<%@ Page Title="<%$ Resources:Application,StudentListReports %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StudentList.aspx.cs" Inherits="Report_Viewer_StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-sm-12">
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="4" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="<%$ Resources:Application,AssignStudent %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="All Active StudentList"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$ Resources:Application,AllUnassignStudent %>"></asp:ListItem> 
                    <asp:ListItem Value="4" Text="All InActive StudentList"></asp:ListItem>                        
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <div class='<%= Common.SessionInfo.Panel %>'>
        <div class="panel-heading">
            <h4>
                <asp:Label ID="lblCriteria" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label></h4>
        </div> 
          <div class="col-lg-4 col-md-4">
         <div class="">
                    <div class="form-group" id="year">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="campus">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                </div>
          </div>
        <div class="panel-body" id="criteria">
            <div class="col-lg-4 col-md-4">
                <div class="form-horizontal">                  
                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>                    
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>                 
                </div>
            </div>
            <div class="col-lg-4 col-md-4">
                <div class="form-horizontal">
                      <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-footer" id="report">
             <asp:Button ID="btnReport" CssClass="btn btn-success" OnClick="btnReport_Click" runat="server" Text="<%$ Resources:Application,GenerateReport %>" ValidationGroup="save"/>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#criteria").hide();
            $("#report").hide();

            $("#rdList").change(function () {

                var checked_radio = $("[id*=rdList] input:checked");
                if (checked_radio.val() == "1") {
                    $("#criteria").slideDown();
                    $("#year").slideDown();
                    $("#campus").slideDown();
                    $("#report").slideDown();
                    
                }
                else if (checked_radio.val() == "2") {
                    $("#criteria").hide();
                    $("#year").slideDown();
                    $("#campus").slideDown();
                    $("#report").slideDown();
                }
                else if (checked_radio.val() == "3")
                    {
                    $("#criteria").hide();
                    $("#year").hide();
                    $("#campus").hide();
                    $("#report").slideDown();
                }
                else
                {
                    $("#criteria").hide();
                    $("#year").slideDown();
                    $("#campus").slideDown();
                    $("#report").slideDown();
                }
         

            });
        });
        function GetEncriptedSearchCriteria() {
            var year = $("#cphMain_ddlYear option:selected").text();
            var mediumID = $('#cphMain_ddlMedium').val();
            var campusID = $('#cphMain_ddlCampus').val();
            var classID = $('#cphMain_ddlClass').val();
            var groupID = $('#cphMain_ddlGroup').val();
            var shiftID = $('#cphMain_ddlShift').val();
            var sectionID = $('#cphMain_ddlSection').val();

            var searchData = '{year: ' + year + ', mediumID: '+ mediumID +', campusID: ' + campusID + ', classID: ' + classID + ' , groupID: ' + groupID + ' , shiftID: ' + shiftID + ', sectionID: ' + sectionID + '}';
            alert(searchData);

            $.ajax({
                type: "POST",
                data: searchData,
                url: "StudentList.aspx/GetSearchCriteria",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    window.open("../../Report/Viewer/ReportView.aspx?query=" + data.d + "&report=studentList", 'Student List');
                },
                error: function (data) {
                    alert("Failed");
                }
            });
        }
    </script>

</asp:Content>

