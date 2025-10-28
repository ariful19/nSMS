<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="DataEntry.aspx.cs" Inherits="Pages_Admission_DataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div>
        <asp:UpdatePanel ID="updatelist" runat="server">
            <ContentTemplate>
                <asp:Panel ClientIDMode="Static" ID="pnlAssignStudent" runat="server">
                    <div>
                        <div class="panel-heading">
                            Student List
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
                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                        </th>
                                                        <th>Branch</th>
                                                        <th>Student Sl</th>
                                                        <th>Code</th>
                                                        <th>Source</th>
                                                        <th>Level</th>
                                                        <th>Enter Student ID</th>
                                                        <th>1st Admission Date</th>
                                                        <th>Student Name</th>
                                                        <th>Father Name</th>
                                                        <th>Mother Name</th>
                                                        <th>District</th>
                                                        <th>Gender</th>
                                                        <th>Birth Date</th>
                                                        <th>Section</th>
                                                        <th>Optional Sub</th>
                                                        <th>Additional Sub</th>
                                                        <th>Special Notes</th>
                                                        <th>Counselor</th>
                                                        <th>Studentship</th>
                                                        <th>Address</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%-- <td class="action"><%# Container.ItemIndex + 1 %></td>--%>
                                                <td>
                                                    <asp:CheckBox ID="chkrow" runat="server" />
                                                    <asp:HiddenField ID="hdnId" Value='<%#Eval("Id")%>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRoll" Text='<%#Eval("Branch")%>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSl" runat="server" Text='<%#Eval("Sl")%>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSource" runat="server" Text='<%#Eval("Source")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLevel" runat="server" Text='<%#Eval("Level")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdmissionDate" runat="server" Text='<%#Eval("1stAdmissionDate")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStuName" runat="server" Text='<%#Eval("StudentName")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFather" runat="server" Text='<%#Eval("FatherName")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMother" runat="server" Text='<%#Eval("MotherName")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDist" runat="server" Text='<%#Eval("District")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender")%>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBirthDate" runat="server" Text='<%#Eval("BirthDate")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSection" runat="server" Text='<%#Eval("Section")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOptionalSub" runat="server" Text='<%#Eval("OptionalSub")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdditional" runat="server" Text='<%#Eval("AdditionalSub")%>'/>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSpecial" runat="server" Text='<%#Eval("SpecialNotes")%>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCounselor" runat="server" Text='<%#Eval("Counselor")%>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudentship" runat="server" Text='<%#Eval("Studentship")%>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address")%>'/>
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
        </asp:UpdatePanel>
    </div>
    <div class="panel-footer">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-flat btn-success"
            Text="Save" OnClick="btnSave_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script src="../../Scripts/jquery-2.1.3.min.js"></script>
    <script src="../../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../../Scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

<script>
    $(document).ready(function () {

        var $allCheckbox = $("#example1 [id*=chkHeader]");
        var $checkboxes = $("#example1 [id*=chkrow]");

        $allCheckbox.change(function () {
            if ($allCheckbox.is(':checked')) {
                $checkboxes.attr('checked', 'checked');
            }
            else {
                $checkboxes.removeAttr('checked');
            }
        });
        $checkboxes.change(function () {
            if ($checkboxes.not(':checked').length) {
                $allCheckbox.removeAttr('checked');
            }
            else {
                $allCheckbox.attr('checked', 'checked');
            }
        });
        $("#example1").DataTable({
            "paging": false,
            "lengthChange": false,
            "searching": true,
            "ordering": true,
            "info": false,
            "autoWidth": true
        });
    });
    </script>

</asp:Content>

