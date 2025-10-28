<%@ Page Title="Employee List" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="Report_Viewer_EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdList" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="Selected Employee" Selected="true"></asp:ListItem>
                    <asp:ListItem Value="2" Text="All Employees"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Employee Id Card"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <div class="panel-heading">
        <h4>
            <asp:Label ID="lblCriteria" runat="server" Text="Criteria"></asp:Label></h4>
    </div>
    <div class="panel-body" id="criteria">

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
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6 col-md-6">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label3" runat="server" Text="Grade"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Type" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label4" runat="server" Text="Level"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlLevel" runat="server" DataTextField="LevelName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                 <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label5" runat="server" Text="Employee Id"></asp:Label></label>
                    <div class="col-sm-6">
                       <asp:TextBox ID="tbxEmployeeId" runat="server" CssClass="form-control dropdown" MaxLength="11"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel-footer">
        <asp:Button runat="server" OnClick="btnReport_Click" Text="Generate Report" CssClass="btn btn-success pull-right" />
    </div>

<script type="text/javascript">

    $(document).ready(function () {
        $("#rdList").change(function () {
            var checked_radio = $("[id*=rdList] input:checked");
            if (checked_radio.val() == "1") {
                $("#criteria").slideDown();
            }
            else if (checked_radio.val() == "2") {
                $("#criteria").slideUp();
            }
            else {
                $("#criteria").slideDown();
            }
        })
    })

</script>
</asp:Content>