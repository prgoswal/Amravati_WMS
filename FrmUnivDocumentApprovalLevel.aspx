<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmUnivDocumentApprovalLevel.aspx.cs" Inherits="FrmUnivDocumentApprovalLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.txtVertical 
{
	filter:  progid:DXImageTransform.Microsoft.BasicImage(rotation=3);  /* IE6,IE7 */
	ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=3)"; /* IE8 */
	-moz-transform: rotate(-90deg);  /* FF3.5+ */
	-o-transform: rotate(-90deg);  /* Opera 10.5 */
	-webkit-transform: rotate(-90deg);  /* Safari 3.1+, Chrome */
	position: absolute; 
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div align="center" style="font-family:Calibri;font-size:12pt">
       
        <h3 class="text-center" style="margin-left:35%">
          DOCUMENT APPROVAL LEVEL 
        </h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <table width="100%" >
            <tr>
            <td style="width:11%;text-align:left;left:0">
            <h3 style="left:0">
            
            <asp:Label ID="lblside" class="txtVertical" runat="server" Text="Document Type"></asp:Label>
            </h3>
            </td>
            <td  style="width:89%">
             <asp:Panel ID="pl" runat="server" Width="99%"  ScrollBars="Auto">
                    <asp:GridView ID="GridView1" Width="100%" runat="server" OnRowDataBound="GridView1_RowDataBound"
                        OnRowCommand="GridView1_RowCommand" Height="274px" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" 
                        HorizontalAlign="Center">
                        <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
            </tr>
            </table>
            
               
                
                <br />
                <asp:Button ID="Btnselect" class="btn btn-primary" runat="server" Text="Save" 
                    OnClick="Btnselect_Click" />
                <br />
                <br />
                <asp:Label ID="lblvalue" runat="server" ForeColor="Red" Text=""></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
</asp:Content>

