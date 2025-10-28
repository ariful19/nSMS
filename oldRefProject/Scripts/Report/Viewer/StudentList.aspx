<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StudentList.aspx.cs" Inherits="Report_Viewer_StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class='<%= Common.SessionInfo.Panel %>'>
        <div class="panel-heading">
            <h4>
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label></h4>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">

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
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="panel-footer">
            <%--    <asp:Button ID="btnReport" CssClass="btn btn-success" OnClick="btnReport_Click" OnClientClick="window.open('ReportView.aspx?report=studentList')" runat="server" Text="<%$ Resources:Application,GenerateReport %>" ValidationGroup="save" PostBackUrl="~/Report/Viewer/ReportView.aspx" />--%>
            <%-- <input type="button" onclick="GetEncriptedSearchCriteria()" class="btn btn-success"  value= "GenerateReport" />--%>
<%--            <asp:Button ID="btnReport" CssClass="btn btn-success" OnClientClick="GetEncriptedSearchCriteria()" runat="server" Text="<%$ Resources:Application,GenerateReport %>" ValidationGroup="save" />--%>
             <asp:Button ID="btnReport" CssClass="btn btn-success" OnClick="btnReport_Click" runat="server" Text="<%$ Resources:Application,GenerateReport %>" ValidationGroup="save"/>
        </div>
    </div>

    <script type="text/javascript">

        function GetEncriptedSearchCriteria() {
            var year = $("#cphMain_ddlYear option:selected").text();
            var classID = $('#cphMain_ddlClass').val();
            var groupID = $('#cphMain_ddlGroup').val();
            var shiftID = $('#cphMain_ddlShift').val();
            var sectionID = $('#cphMain_ddlSection').val();

            var searchData = '{year: ' + year + ', classID: ' + classID + ' , groupID: ' + groupID + ' , shiftID: ' + shiftID + ', sectionID: ' + sectionID + '}';
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

