<%@ Page Title="<%$ Resources:Application,MarksView %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="MarksView.aspx.cs" Inherits="Pages_Result_MarksView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script>
        Sys.Application.add_load(Load);
    </script>
    <div class="<%= Common.SessionInfo.Panel %>">
        <div class="panel-heading">
            <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
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
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"
                                OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,MarksOutof %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMarksOutOf" runat="server" DataTextField="OutOfMarks" DataValueField="Id" CssClass="form-control dropdown">
                                <%--<asp:ListItem Text="100" Value="1"></asp:ListItem>
                                <asp:ListItem Text="50" Value="2"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,ExamType %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlExamType" runat="server" DataTextField="ExamType" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
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
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updSubject" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-4">
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Application,Subject %>"></asp:Label></label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlSubject" runat="server" DataTextField="SubjectName" DataValueField="SubjectToClassId" CssClass="form-control dropdown"
                                        OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RollNo %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxRoll" runat="server" placeholder="Enter Roll No." CssClass="form-control" MaxLength="9"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredtbxRoll" runat="server"
                                Enabled="True" TargetControlID="tbxRoll" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="tbxReg" runat="server" placeholder="Enter Student ID" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" TargetControlID="tbxReg" FilterType="Custom" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-footer">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-success" Text="<%$ Resources:Application,Search %>" />
        </div>
    </div>
    <%--<asp:UpdatePanel ID="updatelist" runat="server">
        <ContentTemplate>--%>
            <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                <div class='<%= Common.SessionInfo.Panel %>'>
                    <div class="panel-heading">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,StudentList %>"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="box">
                            <div class="box-body">
                                <asp:Repeater ID="rptStudent" runat="server" OnItemDataBound="rptStudent_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="example1" style="border: 1px solid #0000FF; width: 100%" cellpadding="0">
                                            <thead>
                                                <tr style="background-color: #FF6600; color: #000000; font-size: large; font-weight: bold;" class="table table-bordered table-hover">
                                                    <th>
                                                        <asp:CheckBox ID="chkHeader" runat="server" /></th>
                                                    <th>
                                                        <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,Roll %>"></asp:Label>
                                                        <asp:Label ID="LabelReg" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Application,Name %>"></asp:Label>

                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label16" runat="server" Text="CT"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label17" runat="server" Text="MT"></asp:Label>

                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label11" runat="server" Text="Theory"></asp:Label>

                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label12" runat="server" Text="MCQ"></asp:Label>

                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label13" runat="server" Text="Practical"></asp:Label>
                                                    </th>
                                        
                                                   <%-- <th>
                                                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Application,SBSMarks %>"></asp:Label>

                                                    </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr id="trRow" runat="server" style="background-color: #deedf2; color: #000000; font-size: large; font-weight: bold; border: 5px solid red;">

                                            <td>
                                                <asp:CheckBox ID="chkrow" runat="server" /></td>
                                            <td>
                                                <asp:Label ID="lblRollNo" Text='<%#Eval("RollNo") %>' runat="server" />
                                                <asp:Label ID="lblRegNo" Text='<%#Eval("RegNo") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNameEng" runat="server" Text='<%#Eval("NameEng") %>'></asp:Label>

                                                <asp:HiddenField ID="hdnStudentId" Value='<%#Eval("StudentToClassId") %>' runat="server" />
                                                <asp:HiddenField ID="hdnObtainMarksEntry" Value='<%#Eval("Id") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAttendanceMarks" runat="server" Text='<%#Eval("AttendanceMarks") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMonthlyMarks" runat="server" Text='<%#Eval("MonthlyMarks") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSubjectiveMarks" runat="server" Text='<%#Eval("SubjectiveMarks") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblObjectiveMarks" runat="server" Text='<%#Eval("ObjectiveMarks") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPracticalMarks" runat="server" Text='<%#Eval("PracticalMarks") %>'></asp:Label>
                                            </td>
                               
                                        <%--    <td>
                                                <asp:Label ID="lblOtherMarks" runat="server" Text='<%#Eval("OtherMarks") %>'></asp:Label>
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
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="<%$ Resources:Application,Finished %>" />
                        <asp:Button ID="btnPublish" runat="server" CssClass="btn btn-success" OnClick="btnPublish_Click" Text="<%$ Resources:Application,Publish %>" />
                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-default" OnClientClick="return isAnyChecked();" OnClick="btnReport_Click" Text="<%$ Resources:Application,GenerateReport %>" />
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-default" OnClientClick="if ( ! SaveConfirmation()) return false;" OnClick="btnDelete_Click" Text="Delete" />
                    </div>
                    <div style="text-align: center">
                        <asp:Label ID="lblNoRecordFond" ForeColor="Red" Font-Size="16px" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
      <%--  </ContentTemplate>
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>


