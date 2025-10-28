<%@ Page Title="Class Routine" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ClassRoutine.aspx.cs" Inherits="Pages_Administration_ClassRoutine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class='<%= Common.SessionInfo.Panel %>'>
        <div class="panel-heading">
            Assign Student to Class
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">Year<span class="required">*</span></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">Class<span class="required">*</span></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">Group<span class="required">*</span></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">Shift<span class="required">*</span></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">Section<span class="required">*</span></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView ID="gvCustomers" CssClass="table" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Degree" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <%# Eval("DegreeName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Board" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <%# Eval("Board") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <%# Eval("Subject") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <%# Eval("PassingYear") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <%# Eval("Grade") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GPA Scale" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                        <ItemTemplate>
                            <%# Eval("GPAScale")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Division" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                        <ItemTemplate>
                            <%# Eval("ResultDivision")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Division" ItemStyle-Width="150px" ItemStyle-CssClass="Country">
                        <ItemTemplate>
                            <button id="lnkDelete" class="btn btn-default"><i class="fa fa-plus"></i>Delete</button>
                            <%-- <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server"/>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

