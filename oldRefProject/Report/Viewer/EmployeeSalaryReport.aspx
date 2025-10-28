<%@ Page Title="EmployeeSalaryReport" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeSalaryReport.aspx.cs" Inherits="Report_Viewer_EmployeeSalaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="panel-heading">
        <h4>
            <asp:Label ID="lblCriteria" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label></h4>
    </div>
    <div class="panel-body" id="criteria">
        <div class="col-lg-6 col-md-6">
            <div class="form-horizontal">
                <div class="form-group" id="year">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="lblMonth" runat="server" Text="<%$ Resources:Application,Month %>"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlMonth" runat="server" DataTextField="Month" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                        <asp:Label ID="Label6" runat="server" Text="Grade"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Type" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                 <div class="form-group">
                    <label for="inputEmail3" class="col-sm-4">
                        <asp:Label ID="Label3" runat="server" Text="Level"></asp:Label></label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlLevel" runat="server" DataTextField="LevelName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class="panel-footer" id="report">
        <asp:Button ID="btnSalaryCash" CssClass="btn btn-info" OnClick="btnSalaryCash_Click" runat="server" Text="Salary Cash" />
        <asp:Button ID="btnSalaryBank" CssClass="btn btn-info" OnClick="btnSalaryBank_Click" runat="server" Text="Salary Bank" />
        <asp:Button ID="btnSalarySheet" CssClass="btn btn-info" OnClick="btnSalarySheet_Click" runat="server" Text="Salary Sheet" />
        <asp:Button ID="btnSignatureBank" CssClass="btn btn-info" OnClick="btnSignatureBank_Click" runat="server" Text="Signature Bank" />
         <asp:Button ID="btnSignatureCash" CssClass="btn btn-info" OnClick="btnSignatureCash_Click" runat="server" Text="Signature Cash" />
        <asp:Button ID="btnSummary" CssClass="btn btn-info" OnClick="btnSummary_Click" runat="server" Text="Summary" />
    </div>

</asp:Content>
