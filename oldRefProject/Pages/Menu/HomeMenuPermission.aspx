<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/AdminMaster.master" EnableEventValidation="false" CodeFile="HomeMenuPermission.aspx.cs" Inherits="Pages_Menu_HomeMenuPermission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/fontawesome-iconpicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
   
            <div class="col-sm-9">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-10 col-sm-10 col-xs-10">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                    <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Menu %>"></asp:Label></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlMenu" runat="server" DataTextField="TextBan" DataValueField="MenuID" CssClass="form-control dropdown">
                                    </asp:DropDownList>
                                      <asp:HiddenField ID="hdfMenuId" runat="server" />
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,ispublish %>"></asp:Label></label>
                                <div class="col-sm-3">
                                    <asp:CheckBox ID="chkIsPublish" runat="server" />
                                </div>

                            </div>
                            <div class="form-group"> 
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,PublishStartDate %>"></asp:Label></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="tbxPublishStartDate" runat="server" placeholder="Enter Start Date" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxPublishStartDate" ValidationGroup="save"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                        ErrorMessage="Invalid Date format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Date" ControlToValidate="tbxPublishStartDate">*</asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxPublishStartDate"
                                        TargetControlID="tbxPublishStartDate">
                                    </cc1:CalendarExtender>
                                
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,PublishEndDate %>"></asp:Label></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="tbxPublishEndDate" runat="server" placeholder="Enter End Date" CssClass="form-control"></asp:TextBox>

                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxPublishEndDate" ValidationGroup="save"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                        ErrorMessage="Invalid Date format, should be- dd/MM/yyyy"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Date" ControlToValidate="tbxPublishEndDate">*</asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="tbxPublishEndDate"
                                        TargetControlID="tbxPublishEndDate">
                                    </cc1:CalendarExtender>
                                  
                                </div>
                            </div>
                            <%--<div class="form-group" style="display:none">
                                <label for="inputEmail3" class="col-sm-2">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,URL %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbxURL" runat="server" placeholder="Enter Role Name" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter URL" ControlToValidate="tbxURL">*</asp:RequiredFieldValidator>
                                </div>
                            </div>--%>

                            <div class="form-group">
                                <div class="col-sm-offset-4 col-sm-10">
                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Save %>" CssClass="btn btn-primary" ValidationGroup="save"
                                        OnClick="btnSave_Click" />
                                    
                                    <asp:Button ID="btnReset" runat="server" ClientIDMode="Static" Text="<%$ Resources:Application,Refresh %>" CssClass="btn btn-primary" CausesValidation="false"
                                        OnClick="btnReset_Click" />
                                </div>
                            </div>


                        </div>
                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-12">
                    <div class="box">
                        <div class="box-body">
                            <asp:Repeater ID="rptTask" runat="server">
                                <HeaderTemplate>
                                    <table id="example1" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,TitleEnglidh %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,TitleBangla %>"></asp:Label></th>
                                                <%--<th>
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,URL %>"></asp:Label>

                                                </th>--%>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Root %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,PublishStartDate %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,PublishEndDate %>"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Action %>"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("TextEng") %></td>
                                        <td><%#Eval("TextBan") %></td>
                                        <%--<td><%#Eval("URL") %></td>--%>
                                        <td><%#Eval("Parent") %></td>
                                        <td><%# Eval("PublishStartDate")%></td>
                                        <td><%# Eval("PublishEndDate")%></td>
                                        <%--<td><%# Convert.ToDateTime(Eval("PublishStartDate")).ToString("dd-MMM-yyyy") %></td>
                                        <td><%# Convert.ToDateTime(Eval("PublishEndDate")).ToString("dd-MMM-yyyy")%></td>--%>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnEdit1_Command" CommandArgument='<%# Eval("MenuID")%>' ImageUrl="~/Images/Common/edit.png" ToolTip="Edit" />
                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("MenuID")%>' ImageUrl="~/Images/Common/delete.png" ToolTip="Delete" OnClientClick="return confirm('If you want to delete a parent ensure that all child will be deleted\n\n\r\t\t\t\t\t\tAre you sure? ')" />

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
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        function loadIcon() {
            $('.icp-auto').iconpicker();
            $('.action-create').on('click', function () {
                $('.icp-auto').iconpicker();

                $('.icp-opts').iconpicker({
                    title: 'With custom options',
                    icons: ['fa-github', 'fa-heart', 'fa-html5', 'fa-css3'],
                    selectedCustomClass: 'label label-success',
                    mustAccept: true,
                    placement: 'bottomRight',
                    showFooter: true,
                    hideOnSelect: true,
                    templates: {
                        footer: '<div class="popover-footer">' +
                                    '<div style="text-align:left; font-size:12px;">Placements: \n\
                    <a href="#" class=" action-placement">inline</a>\n\
                    <a href="#" class=" action-placement">topLeftCorner</a>\n\
                    <a href="#" class=" action-placement">topLeft</a>\n\
                    <a href="#" class=" action-placement">top</a>\n\
                    <a href="#" class=" action-placement">topRight</a>\n\
                    <a href="#" class=" action-placement">topRightCorner</a>\n\
                    <a href="#" class=" action-placement">rightTop</a>\n\
                    <a href="#" class=" action-placement">right</a>\n\
                    <a href="#" class=" action-placement">rightBottom</a>\n\
                    <a href="#" class=" action-placement">bottomRightCorner</a>\n\
                    <a href="#" class=" active action-placement">bottomRight</a>\n\
                    <a href="#" class=" action-placement">bottom</a>\n\
                    <a href="#" class=" action-placement">bottomLeft</a>\n\
                    <a href="#" class=" action-placement">bottomLeftCorner</a>\n\
                    <a href="#" class=" action-placement">leftBottom</a>\n\
                    <a href="#" class=" action-placement">left</a>\n\
                    <a href="#" class=" action-placement">leftTop</a>\n\
                    </div><hr></div>'
                    }
                }).data('iconpicker').show();
            }).trigger('click');

            $('.icp').on('iconpickerSelected', function (e) {
                $('.lead .picker-target').get(0).className = 'picker-target fa-3x ' +
                            e.iconpickerInstance.options.iconBaseClass + ' ' +
                            e.iconpickerInstance.options.fullClassFormatter(e.iconpickerValue);
            });
        }
        $(function () {
            loadIcon();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                loadIcon();
            });
        };
    </script>
</asp:Content>

