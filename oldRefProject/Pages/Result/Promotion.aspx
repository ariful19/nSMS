<%@ Page Title="<%$ Resources:Application,StudentPromotion %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Promotion.aspx.cs" Inherits="Pages_Result_Promotion" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script>
        Sys.Application.add_load(Load);
    </script>
     <div class="padding-bottom-15">
                <asp:RadioButtonList ID="rdlList" runat="server" ClientIDMode="Static" RepeatColumns="3" CssClass="form-control FormatRadioButtonList">
                    <asp:ListItem Value="1" Text="Promotion" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Transfer"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
    <div class="<%= Common.SessionInfo.Panel %>">
        <div class="panel-heading">
            Criteria:
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
                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label><span class="required">*</span></label>
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
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>         
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal"> 
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label21" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-footer">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-success" Text="<%$ Resources:Application,Search %>" />
        </div>
        <div class="panel-heading">
            Promotion To:
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPromotionYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPromotionMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPromotionCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPromotionClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>                   
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                     <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPromotionGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPromotionShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlPromotionSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
  <%--  <asp:UpdatePanel ID="updatelist" runat="server">
        <ContentTemplate>
            <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptStudent" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                                    <th>
                                                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,PreviousRollNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Application,AttendanceMarks %>"></asp:Label>

                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Application,MonthlyMarks %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,SubjectiveMarks %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,ObjectiveMarks %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,PracticalMarks %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,SBSMarks %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,TotalMarks %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,NewRollNo %>"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" onclick="onCheckedClick(this)" runat="server" Width="100%" /></td>
                                            <td>
                                                <asp:Label ID="lblRollNo" runat="server" Width="100%" Text='<%#Eval("RollNo") %>'></asp:Label>


                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Width="100%" Text='<%#Eval("NameEng") %>'></asp:Label>
                                           
                                                <asp:HiddenField ID="hdStudentid" runat="server" Value='<%#Eval("StudentToClassId") %>' />
                                              
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAttendanceMarks" runat="server" Text='<%#Eval("AttendanceMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMonthlyMarks" runat="server" Text='<%#Eval("MonthlyMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSubjectiveMarks" runat="server" Text='<%#Eval("SubjectiveMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblObjectiveMarks" runat="server" Text='<%#Eval("ObjectiveMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPracticalMarks" runat="server" Text='<%#Eval("PracticalMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOtherMarks" runat="server" Text='<%#Eval("OtherMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalMarks" runat="server" Text='<%#Eval("TotalMarks") %>' Width="100%"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxNewRoll" runat="server" Width="100%" CssClass="form-control" Weight="100%" MaxLength="6"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="tbxNewRoll" ValidationExpression="^([0-9]{1,2}){1}(\.[0-9]{1,2})?$|^100$"
                                                    ErrorMessage="Please enter valid number!" />
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidatortbxNewRoll" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter New Roll!!!" ControlToValidate="tbxNewRoll">*</asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredtbxNewRoll" runat="server"
                                                    Enabled="True" TargetControlID="tbxNewRoll" FilterType="Custom" ValidChars="0123456789.">
                                                </cc1:FilteredTextBoxExtender>
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
                    <div class="panel panel-footer">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="return isAnyChecked();" OnClick="btnSubmit_Click" Text="<%$ Resources:Application,Submit %>" />
                    </div>
                    <div style="text-align: center">
                        <asp:Label ID="lblNoRecordFond" ForeColor="Red" Font-Size="16px" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>        
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>--%>
    
    
     <%-- <asp:UpdatePanel ID="updatelist" runat="server">
        <ContentTemplate>--%>
            <asp:Panel ClientIDMode="Static" ID="pnlStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptStu" runat="server">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                                    <th>
                                                        <asp:Label ID="lblPrevipousRoll" runat="server" Text="<%$ Resources:Application,PreviousRollNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>
                                                    </th>
                                                     <th>
                                                        <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label>
                                                    </th>          
                                                    <th>
                                                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label>
                                                    </th>        
                                                    <th>
                                                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label>
                                                    </th>       
                                                   <%-- <th>
                                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Application,NewRollNo %>"></asp:Label>
                                                    </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkrow" onclick="onCheckedClick(this)" runat="server" Width="100%" /></td>
                                            <td>
                                                <asp:Label ID="lblRollNo" runat="server" Width="100%" Text='<%#Eval("RegNo") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Width="100%" Text='<%#Eval("NameEng") %>'></asp:Label>                                           
                                                <asp:HiddenField ID="hdStudentToClassId" runat="server" Value='<%#Eval("StudentToClassId") %>' />
                                              <asp:HiddenField ID="hdnStudentId" runat="server" Value='<%#Eval("StudentId") %>' />
                                            </td>
                                              <td>
                                                <asp:Label ID="Label9" runat="server" Width="100%" Text='<%#Eval("CampusName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Width="100%" Text='<%#Eval("ClassName") %>'></asp:Label>
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="Label17" runat="server" Width="100%" Text='<%#Eval("GroupName") %>'></asp:Label>
                                              
                                            </td>  
                                              <td>
                                                <asp:Label ID="Label20" runat="server" Width="100%" Text='<%#Eval("Shift") %>'></asp:Label>
                                              
                                            </td>  
                                              <td>
                                                <asp:Label ID="Label23" runat="server" Width="100%" Text='<%#Eval("Section") %>'></asp:Label>
                                              
                                            </td>         
                                           <%-- <td>
                                                <asp:TextBox ID="tbxNewRoll" runat="server" Width="100%" CssClass="form-control" Weight="100%" MaxLength="6"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="tbxNewRoll" ValidationExpression="^([0-9]{1,2}){1}(\.[0-9]{1,2})?$|^100$"
                                                    ErrorMessage="Please enter valid number!" />
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidatortbxNewRoll" runat="server" ValidationGroup="save"
                                                    ErrorMessage="Enter New Roll!!!" ControlToValidate="tbxNewRoll">*</asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredtbxNewRoll" runat="server"
                                                    Enabled="True" TargetControlID="tbxNewRoll" FilterType="Custom" ValidChars="0123456789.">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>--%>
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
                    <div class="panel panel-footer">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClientClick="return isAnyChecked();" OnClick="btnSubmit_Click" Text="<%$ Resources:Application,Submit %>" />
                    </div>
                     <div class="panel panel-footer">
                        <asp:Button ID="btnPromoted" runat="server" CssClass="btn btn-primary" OnClientClick="return isAnyChecked();" OnClick="btnNotPromoted_Click" Text="Generate Report" />
                    </div>
                    <div style="text-align: center">
                        <asp:Label ID="lblNoRecordFond" ForeColor="Red" Font-Size="16px" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>        
   <%--     </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script>
        function isAnyChecked() {
            if ($("#example1 [id*=chkrow]:checked").length == 0) {
                alert("Nothing checked. Please select atleast one row.");
                return false;
            }
        }
        function Load() {
            $("#example1 [id*=chkHeader]").click(function () {
                if ($(this).is(":checked")) {
                    $("#example1 [id*=chkrow]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkrow]").prop("checked", false);
                }
            });

            $("#example1 [id*=chkrow]").click(function () {
                if ($("#example1 [id*=chkrow]").length == $("#example1 [id*=chkrow]:checked").length) {
                    $("#example1 [id*=chkHeader]").prop("checked", true);
                } else {
                    $("#example1 [id*=chkHeader]").prop("checked", false);
                }
            });

        }
        $(function () {
            Load();
        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                Load();
            });
        };

        function onCheckedClick(e) {
            //var e = document.getElementById("");
            var id = e.id.substring(e.id.search(/chkrow/) + 7, e.id.length);
            var tb = document.querySelector('[id*="tbxNewRoll_' + id + '"]');

            if (e.checked) {
                tb.setAttribute("required", "required");
            } else {
                tb.removeAttribute("required");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>



