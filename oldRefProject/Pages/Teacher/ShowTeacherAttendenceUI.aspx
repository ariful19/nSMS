<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ShowTeacherAttendenceUI.aspx.cs" Inherits="ShowTeacherAttendenceUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row" id="month">
        <div class="col-sm-12">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-6">
                        <asp:Label ID="Label1" runat="server" Text="Show Employee Attendence"></asp:Label></label>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Year" CssClass="form-control dropdown"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label2" runat="server" Text="Month"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control dropdown">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,TeacherName %>"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlTeacher" runat="server" DataTextField="NameEng" DataValueField="TeacherId" CssClass="form-control dropdown"  AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
                <div class="col-sm-4">
                    <asp:Button ID="searchButton" OnClick="searchButton_Click" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:Application,Search %>" ValidationGroup="save" />

                    <asp:Button ID="btnTeacherAttendenceReport" OnClick="btnTeacherAttendenceReport_Click" CssClass="btn btn-default" runat="server" Text="<%$ Resources:Application,GenerateReport %>" ValidationGroup="save" />
                </div>
            </div>
        </div>
    <div class="col-lg-12 col-md-12">
        <div class="form-horizontal">
            <asp:GridView ID="loadTeacherGridView" CssClass="table" OnRowDataBound="loadTeacherGridView_RowDataBound" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="SI">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="testIdLabel" runat="server" Text='<%#Eval("Date","{0:dd/MM/yyyy}") %>'>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" In Time">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("InTime") %>'>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Out Time">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("OutTime") %>'>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>







