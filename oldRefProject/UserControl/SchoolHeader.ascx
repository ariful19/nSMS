<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SchoolHeader.ascx.cs" Inherits="UserControl_SchoolHeader" %>
<div class="row">
    <div class="col-sm-12">
        <div class="row">
            <div class="col-sm-4 text-left">
                <strong>Code: </strong>
                <asp:Label ID="lblSchoolCode" runat="server"></asp:Label>
            </div>
            <asp:Repeater runat="server" ID="rptNews" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <div class="col-sm-4 text-center">
                        <a href='<%# String.Concat("../Pages/user/NewsDetails.aspx?ID=", Eval("Id")) %>' title="What you have to know News Details">
                            <asp:HiddenField Value='<%# Eval("Logo") %>' ID="HiddenField1" runat="server" />
                            <asp:Image ID="Image1" runat="server" Height="65" Width="65" />
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="col-sm-4 text-right">
                <strong>Estd. Year:</strong>
                <asp:Label ID="lblYear" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 text-center">
                <asp:Label ID="lblSchoolName" CssClass="school-heading" runat="server"> </asp:Label><br />
                <asp:Label ID="lblAddress" runat="server"> </asp:Label>
            </div>
        </div>
    </div>
</div>
<div>
    <hr />
</div>
