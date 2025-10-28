<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage/Default.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControl/LatestBook.ascx" TagName="LatestBook" TagPrefix="UC" %>
<%@ Register Src="~/UserControl/Banner.ascx" TagName="Banner" TagPrefix="UC" %>
<%@ Register Src="~/UserControl/Login.ascx" TagName="Login" TagPrefix="UC" %>
<%@ Register Src="~/UserControl/AboutUs.ascx" TagName="AboutUs" TagPrefix="UC" %>
<%@ Register Src="~/UserControl/SpecialFacilities.ascx" TagName="SpecialFacilities" TagPrefix="UC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="mainContent" runat="server" style="width:100%">
          <UC:AboutUs ID="AboutUs" runat="server" />
        <%--Sunderland International School Bangladesh, Dhaka is a popular and traditional seat of learning in Dhaka city and entire of the country. From the beginning of this School & College claims a very outstanding command because of its teaching method, executive actions, assessment technique, and proportion of passing out with regular excellence schooling. In the SSC examination grading arrangement the outcome is incredibly surprising like other a ranking Schools & Colleges. 

The results of public exams of this institution have been drawing attention to this backward sited institution from opening to contemporary. It should be noted that most of the people come from very middle class inhabitants. But the result of the institution is brighter than any other well-known establishments of Dhaka city. This consign of learning is really enriched with extra curriculum activities including Debating Club, Science Club, Rich Library, Sports and Scouts ornamenting with campus decoration. This is very self-important utterance of all scholars. Quality Education School is a very traditional position of Dhaka city.--%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="Div1" runat="server" style="width:100%">
         <UC:SpecialFacilities ID="SpecialFacilities" runat="server" />

      <%--  It should be noted that most of the people come from very middle class inhabitants. But the result of the institution is brighter than any other well-known establishments of Dhaka city. This consign of learning is really enriched with extra curriculum activities including Debating Club, Science Club, Rich Library, Sports and Scouts ornamenting with campus decoration. This is very self-important utterance of all scholars. Quality Education School is a very traditional position of Dhaka city.--%>

    </div>
</asp:Content>
