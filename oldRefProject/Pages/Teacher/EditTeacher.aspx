<%@ Page Title="<%$ Resources:Application,EditTeacher %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="EditTeacher.aspx.cs" Inherits="Pages_Teacher_EditTeacher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:Panel ClientIDMode="Static" ID="Panel2" runat="server">
        <div class='<%= Common.SessionInfo.Panel %>'>
            <div class="panel-heading">
                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,TeacherList %>"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="box">
                    <div class="box-body">
                        <asp:Repeater ID="rptTeacher" runat="server">
                            <HeaderTemplate>
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,SL %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Application,TeacherPinNo %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,EmployeeId %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label></th>
                                            <th>
                                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label></th>
                                            <th class="action">
                                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="action"><%# Container.ItemIndex + 1 %></td>
                                    <td>
                                        <asp:Label ID="lblTeacherPin" Text='<%#Eval("TeacherPin") %>' runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmployeeId" Text='<%#Eval("EmployeeId") %>' runat="server" />
                                    </td>
                                    <td>
                                        <%#Eval("NameEng") %>
                                        <asp:HiddenField ID="hdnPersonID" Value='<%#Eval("PersonID") %>' runat="server" />
                                    </td>
                                    <td>
                                        <%#Eval("Mobile") %>
                                    </td>

                                    <td class="action">
                                        <asp:ImageButton ID="imgBtn" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit"/>
                                        <asp:ImageButton ID="btnView" runat="server" OnCommand="btnView_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/View.png" ToolTip="View" />
                                        <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("UserName")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure?')"  />
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
            <div class="panel-footer">
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script>
        $(document).ready(function () {
            $("#example2").DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": false,
                "autoWidth": true,
            });
        });
    </script>
</asp:Content>

