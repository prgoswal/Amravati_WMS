<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMarksheetEntry_OLD.aspx.cs" Inherits="FrmMarksheetEntry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <script src="js/1.10.2.jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui.min.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/header.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />
    <script src="bootstrap-3.3.6-dist/js/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="bootstrap-3.3.6-dist/js/numberonly.js" type="text/javascript"></script>
    <script src="js1/jquery.js"></script>
    <script src="js/wheelzoom.js" type="text/javascript"></script>
    <link href="css1/style.css" rel="stylesheet" />
    <link href="css1/font-awesome.css" rel="stylesheet" />
    <link href="css1/font-awesome.min.css" rel="stylesheet" />
    <script src="js/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="Script/jquery-1.5.1min.js"></script>

    <script type="text/javascript">
        jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();
    </script>
    <style>
        img {
            padding: 1px;
            border: 2px solid #E1141E;
            background-color: transparent;
            background-size:1159.57px 340.623px;    
            /*Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;"*/            
        }

        .container-full 
        {
            margin-left: -30px;
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
        }
    </style>

        <script>
            function foilshow() {
                $("#DivCF").hide();
                $("#imgdiv").show();
                $(this).toggleClass('red');
            };
            function Cfoilshow() {
                $("#DivCF").show();
                $("#imgdiv").hide();
                $(this).toggleClass('red');
            };

            function closewindow() {
                window.close($('#<%=hiddenfield.ClientID%>').val() + 'FrmMarksheetEntry.aspx');
            }


    </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    <div class="container-fluid student-form" style="font-family: Calibri">
        <div class="main-form container" style="    padding: 10px 0px 10px 23px;">
           
            <div class="">
                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="98%" Height="600px"
                        Style="background-color: #f1efef;left: 8%; top: 10%; border: outset 2px gray; padding: 5px;">
                        <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                            <tr style="background-color: #50618c">
                                <td style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">
                               
                                    <div style="font-size: 12pt" class="btn-group btn-group-sm" role="group">
                                        <button type="button" id="btnfoil" style="font-weight: bold; font-family: Arial; font-size: 12px; width: 105px; height: 25px;"
                                            onclick="foilshow(); this.style.background='#50618c'; this.style.color='white'; document.getElementById('btncfoil').style.background='gray'; 
                                            document.getElementById('btncfoil').style.color='white'"
                                            class="btn btn-default ">
                                            <span class="text-primary1">FOIL </span>
                                        </button>
                                        <button type="button" id="btncfoil" style="font-weight: bold; font-family: Arial; font-size: 12px; width: 105px; height: 25px;"
                                            onclick="Cfoilshow();this.style.background='#50618c';this.style.color='white'; document.getElementById('btnfoil').style.background='gray'; 
                                            document.getElementById('btnfoil').style.color='white'"
                                            class="btn btn-default  ">
                                            <span class="text-primary1">COUNTER FOIL </span>
                                        </button>
                              
                                             
                               
                                        
                                    </div>
                       <div>
                                    <asp:Label ID="lblstuname" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblroll" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblapplicationnumber" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblappliedfor" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                           <asp:LinkButton ID="lnkhome" runat="server" CssClass="btn btn-default" OnClientClick="closewindow();" style="font-weight: bold; font-family: Arial; font-size: 12px; width: 105px; height: 25px;" >Home</asp:LinkButton>
                                       
                       </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px; text-align: center;">

                                    <div id="imgdiv" style="width: 100%">

                                        <div>
                                            <asp:Image class='zoom' CssClass="" ID="ImageSelected" Width="1100px" Height="235px" Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;" runat="server" border="5" />
                                            <script src="wheelzoom.js"></script>
                                            <script>
                                                wheelzoom(document.querySelector('img.zoom'));
                                            </script>
                                        </div>
                                    </div>
                                    <div id="DivCF" hidden>
                                        <asp:Image class='zoom22' ID="ImgCF" Width="1100" Height="235px" Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;" runat="server" border="5" />
                                        <script src="js/wheelzoom.js"></script>
                                        <script>
                                            wheelzoom(document.querySelector('img.zoom22'));
                                        </script>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                        <div style="width: 100%;">
                            <table style="width: 100%; border: solid; border-style: solid; border-width: 2px; ">
                                <tr>
                                    <td style="width: 15%;" rowspan="2" >
                                        <table style="width: 100%; height: 280px; border: solid; border-style: solid; border-width: 2px; margin-left: 3px;">
                                            <tr>
                                                <td class="tablecell" style="text-align:center">
                                                    <asp:Button ID="btnstudetail" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="StudentDetail" OnClick="btnstudetail_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecell" style="text-align:center">
                                                   
                                                    <asp:Button ID="btnsubject" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="Subject Detail"
                                                        OnClick="btnsubject_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecell" style="text-align:center">
                                                    
                                                    <asp:Button ID="btnresult" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="Result Detail" OnClick="btnresult_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecell" style="text-align:center">
                                                    <asp:Button ID="btnfinal" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="Final Submission" OnClick="btnfinal_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 85%; padding: 20px;">
                                        <div>
                                            <asp:Panel ID="pnlstudentdetail" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>

Roll No.
                                                        </td>
                                                            <td>
 <asp:TextBox CssClass="form-control" ID="txtStudentRoll" runat="server"/>

                                                        </td>
                                                        <td>
Student Name
                                                        </td>
                                                        <td>
 <asp:TextBox CssClass="form-control" ID="txtStudentName" runat="server" />
                                                        </td>
                                                        <td>
<asp:Button ID="btnstudentdetailok" Text="OK" runat="server" CssClass="col-md-offset-11 btn btn-primary" OnClick="btnstudentdetailok_Click" />

                                                        </td>
                                                    </tr>
                                                </table>                                                                                    
                                            </asp:Panel>
                                            <asp:Panel ID="pnlSubject" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
 Subject Name
                                                        </td>
                                                        <td>
 <asp:TextBox ID="txtSubject" runat="server" TextMode="MultiLine" />
                                                        </td>
                                                        <td>
<asp:Button ID="btnsubjectOK" Text="OK" runat="server" CssClass="col-md-offset-11 btn btn-primary" OnClick="btnsubjectOK_Click" />

                                                        </td>
                                                        <td>

                                                        </td>
                                                    </tr>

                                                </table>                                        
                                               
                                            </asp:Panel>
                                            <asp:Panel ID="PnlResult" runat="server">
                                                <div>
                                                    <table>
                                                        <tr>
                                                           <td>
  Obtain Marks
                                                           </td>
                                                            <td>
                                                                   <asp:TextBox ID="txtObtain" runat="server"  />
                                                           </td>
                                                            <td>
Out Of Marks
                                                           </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOutOf" runat="server" />
                                                           </td>
                                                            <td>
                                                                <asp:Button ID="btnresultok" Text="OK" runat="server" CssClass="col-md-offset-11 btn btn-primary" OnClick="btnresultok_Click" />

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>                                   
                                            </asp:Panel>

                                            <asp:Panel ID="pnlFinalSub" runat="server">
                                  
                                                <table>
                                                    <tr>
                                                        <td>
<asp:Button ID="btnFinalSub" Text="Submit" CssClass="btn-primary" OnClick="btnFinalSub_Click" runat="server" /> 
                                     
                                                        </td>
                                                    </tr>

                                                </table>
                                                    
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="">
                                   <%-- <asp:Button CssClass="col-md-offset-11 btn btn-primary" ID="btnOk" OnClick="btnOk_Click" Text="Ok" runat="server" />
                                   --%>     
                                    <asp:Label ID="lblmsg" runat="server" CssClass="text-danger"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
        </div>
        <!-- jQuery -->
    </div>
    </div>
      <asp:HiddenField  id="hiddenfield" runat="server" />

    </form>
</body>
</html>
