<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginthroughService.aspx.cs" Inherits="LoginthroughService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>
                Enter User Name

            </td>
            <td>
                <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>

            </td>
        </tr>
            <tr>
            <td>
                Password

            </td>
            <td>
                <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>

            </td>
        </tr>
            <tr>
            <td>


            </td>
            <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" />

                      

            </td>
        </tr>
        <tr>
            <td>
                  <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>



      
    
    </div>
    </form>
</body>
</html>
