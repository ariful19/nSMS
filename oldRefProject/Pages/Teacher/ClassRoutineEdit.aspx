<%@ Page Title="<%$ Resources:Application,ClassRoutineEdit %>" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ClassRoutineEdit.aspx.cs" Inherits="Pages_Teacher_ClassRoutineView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class='<%= Common.SessionInfo.Panel %>' id="criteria">
        <div class="panel-heading">
            <asp:Label ID="MessageLabel" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlYear" runat="server" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlMedium" runat="server" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown" 
                                OnSelectedIndexChanged="ddlMedium_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6 col-md-6">
                <div class="form-horizontal">

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown"
                                 AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlSection" runat="server" DataTextField="Section" DataValueField="Id" CssClass="form-control dropdown"
                                 AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4">
                            <asp:Label ID="Label7" runat="server" Text="Day"></asp:Label></label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlDay" AutoPostBack="true" runat="server" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                                <asp:ListItem Text="Saturday"></asp:ListItem>
                                <asp:ListItem Text="Sunday"></asp:ListItem>
                                <asp:ListItem Text="Monday"></asp:ListItem>
                                <asp:ListItem Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Text="Wednesday"></asp:ListItem>
                                <asp:ListItem Text="Thursday"></asp:ListItem>
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
                    <asp:GridView ID="Gridview1" runat="server" CssClass="table" AutoGenerateColumns="false" >
                        <Columns>
                            <asp:BoundField DataField="PeriodNo" HeaderText="Class Period#" ReadOnly="false" />
                            <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdnClassRoutineId" Value='<%# Eval("Id") %>' />
                                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" DataTextField="SubjectName" DataValueField="SubjectId"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                    </asp:DropDownList>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Teacher Name">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                    </asp:DropDownList>                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="fa fa-2x fa-trash-o"
                                        OnClick="btnDelete_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>

        <div class="panel-footer">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-flat btn-success pull-left"
                Text="Create New Routine"
                OnClick="btnBack_Click"  />

            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-flat btn-primary pull-right"
                Text="Update" OnClick="btnUpdate_Click" OnClientClick="if ( ! UpdateConfirmation()) return false;" />
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        function UpdateConfirmation() {
            return confirm("Are you sure you want to Update Class Routine?");
        }
    </script> 
    
</asp:Content>

