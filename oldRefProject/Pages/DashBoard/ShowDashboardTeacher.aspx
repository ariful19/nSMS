<%@ Page Title="Bidyapith(Education Management System)" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ShowDashboardTeacher.aspx.cs" Inherits="Pages_DashBoard_ShowDashboardTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="row">
        <div class="col-lg-12 col-xs-12">
            <asp:Label ID="lblError" runat="server" ForeColor="#ff0000" Font-Size="14" Font-Bold="true" Text="No Information Found."></asp:Label>
        </div>
        <div class="col-lg-12 col-xs-12">
            <asp:Repeater ID="rptTeacher" runat="server">
                <HeaderTemplate>
                    <asp:Label ID="lblHdr" runat="server" Text="Teacher List" ForeColor="#006666" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <table id="example2" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Sl %>"></asp:Label>
                                </th>
                                <th class="text-center">
                                    <asp:Label ID="Label" runat="server" Text="<%$ Resources:Application,PinNo %>"></asp:Label>
                                </th>
                                <th class="text-left">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                </th>
                                <th class="text-center">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Mobile %>"></asp:Label>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="text-center">
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td class="text-center">
                            <%#Eval("TeacherPin") %>
                        </td>
                        <td class="text-left">
                            <%#Eval("NameEng") %>
                        </td>
                        <td class="text-center">
                            <%# Eval("Mobile") %>
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
     <script type="text/javascript">
        $(document).ready(function () {

            $('#example2').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
            $('#example11').DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false
            });
        });

    </script>
</asp:Content>
