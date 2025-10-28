<%@ Page Title="<%$ Resources:Application,StudentDetails %>" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/AdminMaster.master" CodeFile="StudentList.aspx.cs" Inherits="Pages_Admission_StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class='<%= Common.SessionInfo.Panel %>' id="criteria">
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
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
            <div class="col-lg-6 col-md-6">
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
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnViewStudent" runat="server" CssClass="btn btn-flat btn-success"
                Text="Student Information" OnClick="btnViewStudent_Click" />
        </div>
    </div>
    
    <div>
        <asp:UpdatePanel ID="updatelist" runat="server">
            <ContentTemplate>
                <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                    <div class='<%= Common.SessionInfo.Panel %>'>
                        <div class="panel-heading">
                            Student Profile List
                        </div>
                        <div class="panel-body">
                            <div class="box">
                                <div class="box-body">
                                    <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                        <HeaderTemplate>
                                            <table id="example1" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>SL.</th>
                                                        <th>Name</th>
                                                        <th>Mobile</th>
                                                        <th>Student ID</th>
                                                        <th>Father Name</th>
                                                        <th>Mother Name</th>
                                                        <th>Student Picture</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="action"><%# Container.ItemIndex + 1 %></td>
                                                <td>
                                                    <%#Eval("NameEng") %>
                                                  
                                                </td>
                                                <td>
                                                    <%#Eval("Mobile") %>
                                                </td>
                                                <td>
                                                    <%#Eval("RegistrationNo") %>
                                                </td>
                                                <td>
                                                    <%#Eval("FatherNameEng") %>  
                                                </td>
                                                <td>
                                                    <%#Eval("MotherNameEng") %>  
                                                </td>
                                                 <td>
                                                    <asp:HiddenField Value='<%# Eval("PersonImage") %>' ID="hdnStudent" runat="server" />
                                                    <asp:Image ID="Image1" runat="server" Height="80px" Width="75px" />
                                                </td>
                                                 <td class="action">
                                                    <asp:ImageButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/View.png" ToolTip="View" />
                                                      <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" Visible="False" OnClientClick="return confirm('Are you sure?')" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnViewStudent" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>



