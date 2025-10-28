<%@ Page Title="<%$ Resources:Application,CreateExamRoutine %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateExamRoutine.aspx.cs" Inherits="Pages_Teacher_CreateExamRoutine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class='<%= Common.SessionInfo.Panel %>' id="criteria">
        <div class="panel-heading">
            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-10 col-sm-10 col-xs-10">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save" />
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" SkinID="message"></asp:Label>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown" 
                                OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlCampus" runat="server" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlCampus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlClass" runat="server" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Application,ExamType %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlExamType" runat="server" DataTextField="ExamType" DataValueField="Id" CssClass="form-control dropdown"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-body">
            <asp:UpdatePanel ID="update" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="Gridview1" runat="server" CssClass="table"
                        AutoGenerateColumns="false"
                        OnRowCreated="Gridview1_RowCreated" OnRowDataBound="Gridview1_RowDataBound" OnRowEditing="Gridview1_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="PeriodNo" HeaderText="SL#" />
                            <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdnExamRoutineId" Value='<%# Eval("Id") %>' />
                                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" DataTextField="SubjectName" DataValueField="Id"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Ex: 10:00"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Join Date" ControlToValidate="TextBox1">Enter Start Time</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexStartTime" ControlToValidate="TextBox1" ValidationGroup="save"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="Input valid time: ex: 10:00" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ex: 01:00"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Join Date" ControlToValidate="TextBox2">Enter Start Time</asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBox2" ValidationGroup="save"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ErrorMessage="Input valid time. ex: 01:00" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exam Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxDate" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="save"
                                        ErrorMessage="Enter Join Date" ControlToValidate="tbxDate">Enter Date</asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" StartDate="<%# DateTime.Now %>" runat="server" PopupButtonID="tbxDate" Format="dd/MM/yyyy"
                                        TargetControlID="tbxDate">
                                    </cc1:CalendarExtender>
                                    <asp:CustomValidator runat="server" ClientValidationFunction="ValidateDate" ControlToValidate="tbxDate"
                                        ErrorMessage="Invalid Date." ValidationGroup="Group2" />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="fa fa-2x fa-trash-o"
                                        OnClick="LinkButton1_Click" Visible="false"></asp:LinkButton>
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CssClass="fa fa-2x fa-edit" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonAdd" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="panel-footer">
            <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-flat btn-success" ValidationGroup="save"
                Text="Add New Row"
                OnClick="ButtonAdd_Click" />
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-flat btn-primary pull-right"
                Text="Submit"
                OnClick="btnSave_Click" OnClientClick="if ( ! SaveConfirmation()) return false;" Visible="true" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">

        function ValidateDate(sender, args) {
            var dateString = document.getElementById(sender.controltovalidate).value;
            var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
            if (regex.test(dateString)) {
                var parts = dateString.split("/");
                var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
            } else {
                //args.IsValid = false;
                alert('Invalid date');
                document.getElementById(sender.controltovalidate).value = Today();
            }
        }
        function Today() {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            return today;
        }
        function SaveConfirmation() {
            return confirm("Are you sure you want to Save Exam Routine?");
        }
        function DeleteConfirmation() {
            return confirm("Are you sure you want to Delete Exam Routine?");
        }
    </script>
</asp:Content>

