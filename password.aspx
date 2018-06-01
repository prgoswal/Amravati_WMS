<%@ Page Language="C#" AutoEventWireup="true" CodeFile="password.aspx.cs" Inherits="password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/floating.css" rel="stylesheet" />
<link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    
<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,800' rel='stylesheet' type='text/css' />
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script> 
<script type="text/javascript" src="js/jquery.placeholder.label.js"></script> 
<script type="text/javascript">
		$(document).ready(function (){
			$('input[placeholder]').placeholderLabel();
		})
	</script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-36251023-1']);
        _gaq.push(['_setDomainName', 'jqueryscript.net']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lable1" runat="server" ></asp:Label><br />
        <asp:Label ID="Label1" runat="server" ></asp:Label><br />
        <asp:Label ID="Label2" runat="server" ></asp:Label><br />
        <asp:Label ID="Label4" runat="server" ></asp:Label><br />
        <asp:Label ID="Label8" runat="server" ></asp:Label><br />
        <asp:Label ID="Label9" runat="server" ></asp:Label><br />
        <asp:Label ID="Label7" runat="server" ></asp:Label><br />
        <asp:Label ID="Label6" runat="server" ></asp:Label><br />
        <asp:Label ID="Label5" runat="server" ></asp:Label><br />

        <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:Button ID="btn" runat="server" Text="show" OnClientClick="return networkInfo()" />
    </div>
         
        <script type="text/javascript" >
            function networkInfo() {
                var wmi = new ActiveXObject("WbemScripting.SWbemLocator");
                var service = wmi.ConnectServer(".");
                e = new Enumerator(service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"));                
                for (; !e.atEnd() ; e.moveNext())
                {
                    var s = e.item();
                    var macAddress = unescape(s.MACAddress);                    
                }
                document.getElementById("Label9").innerHTML = macAddress;
                alert(macAddress);
                return macAddress;
            }
        </script>
        


    </form>
</body>
</html>
