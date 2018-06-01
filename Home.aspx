<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p class="title" style="width:100%; font-family:Calibri;"> WorkFlow Management System</p>

  <%--  <script type="text/javascript" id="key">
        var url = "http://localhost:53425/UserLogin.aspx";
        function windowClose() {
            url.close()
            return false;
        }
    </script>--%>
   <%-- <script type="text/javascript" id="key">
        function closeWindow() {
            window.open('', '_parent', '');
            window.close();
        }
</script> --%>

<script type="text/javascript">
    function preventBack() { window.history.forward(); }
    setTimeout("preventBack()", 0);

    window.onunload = function () { null };

    </script>
</asp:Content>

