<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" Async="true" AutoEventWireup="true" CodeFile="FrmDuplicateMarksheet.aspx.cs" Inherits="FrmDuplicateMarksheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/1.10.2.jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui.min.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/Calender/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap-3.3.6-dist/Calender/jquery-ui.min.js" type="text/javascript"></script>
    <script src="bootstrap-3.3.6-dist/Calender/jquery.min.js" type="text/javascript"></script>

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
            background-size:1159.57px 100%;

            /*Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;"*/


        }

        .container-full {
            margin-left: -30px;
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
        }
    </style>

    <script src="js/wheelzoom.js" type="text/javascript"></script>
    <link href="css1/style.css" rel="stylesheet">
    <link href="css1/font-awesome.css" rel="stylesheet">
    <link href="css1/font-awesome.min.css" rel="stylesheet">
    <style type="text/css">
        #mask {
            /*position: fixed;*/
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
    </style>
    <style type="text/css">
        .tablecell {
            border: solid;
            border-style: solid;
            border-width: 3px;
            /*line-height: 5;*/
            height: 40px;
            text-align: left;
            padding-left: 10px;
        }
    </style>

    <script src="js/jquery-1.3.2.js"></script>
    <script type="text/javascript" lang="javascript">

        $(".btnClose11").live('Click', function () { HidePopupPrint(); });
        function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
      document.getElementById("<%=pnlSubject.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=PnlResult.ClientID%>").style.visibility = 'hidden';
            
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }

        $(".btnClose").live('click', function () { HidePopup(); });  
        </script>

    <script type="text/javascript" src="Script/jquery-1.5.1min.js"></script>
    <link href="css/jquery.alerts.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid student-form" style="font-family: Calibri">
        <div class="main-form container" style="    padding: 10px 0px 10px 23px;">
          
            <div class="">
                    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="98%" Height="600px"
                        Style="background-color: #f1efef;left: 8%; top: 10%; border: outset 2px gray; padding: 5px;">
                        <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                            <tr style="background-color: #50618c">
                                <td style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">
                                    <a id="closebtn" style="color: white; float: right; text-decoration: none"
                                    class="btnClose" href="#">
                                    <img src="images/cross.gif" height="15px" width="15px" border="5" />

                                </a>
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
                                    <br />
                                    <asp:Label ID="lblstuname" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblroll" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblapplicationnumber" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblappliedfor" Style="font-size: 12px" runat="server" Text=""></asp:Label>

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

                        <div style="width: 100%;">

                            <table style="width: 100%; border: solid; border-style: solid; border-width: 2px; ">

                                <tr>

                                    <td style="width: 15%;" rowspan="2" >


                                        <table style="width: 100%; height: 280px; border: solid; border-style: solid; border-width: 2px; margin-left: 3px;">
                                            <tr>
                                                <td class="tablecell">


                                                    <asp:Button ID="btnstudetail" runat="server" class="btn btn-default" Style="width: 120px;" Text="StudentDetail" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecell">
                                                   
                                                    <asp:Button ID="btnsubject" runat="server" class="btn btn-default" Style="width: 120px;" Text="Subject Detail" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecell">
                                                    
                                                    <asp:Button ID="btnresult" runat="server" class="btn btn-default" Style="width: 120px;" Text="Result Detail" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecell">
                                                    <asp:Button ID="btnfinal" runat="server" class="btn btn-default" Style="width: 120px;" Text="Final Submission" />
                                                </td>
                                            </tr>

                                        </table>
                                    </td>
                                    <td style="width: 85%; padding: 20px;">
                                        <div>
                                            <asp:Panel ID="pnlstudentdetail" runat="server">
                                                
                                                <div class="form-group">
                                                    <asp:Label CssClass="col-md-3 control-label" Text="Student Roll No." runat="server" />
                                                    <div class="col-md-3">
                                                        <asp:TextBox CssClass="form-control" ID="txtStudentRoll" runat="server" />
                                                    </div>
                                                </div>     
                                                <div class="form-group">
                                                    <asp:Label CssClass="col-md-3 control-label" Text="Student Name" runat="server" />
                                                    <div class="col-md-3">
                                                        <asp:TextBox CssClass="form-control" ID="txtStudentName" runat="server" />
                                                    </div>
                                                </div> 
                                              

                                            </asp:Panel>
                                            <asp:Panel ID="pnlSubject" runat="server">
                                                Subject Name :<asp:TextBox ID="txtSubject" runat="server" />
                                        

                                            </asp:Panel>
                                            <asp:Panel ID="PnlResult" runat="server">

                                               Obtain Marks : <asp:TextBox ID="txtObtain" runat="server" /><br />
                                               Out Of Marks :  <asp:TextBox ID="txtOutOf" runat="server" />
                                             

                                            </asp:Panel>

                                            <asp:Panel ID="pnlFinalSub" runat="server">
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnFinalSub" Text="Submit" runat="server" /> 
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="">
                                    <asp:Button CssClass="col-md-offset-11 btn btn-primary" ID="btnOk" OnClick="btnOk_Click" Text="Ok" runat="server" />
                                        </td>
                                </tr>
                            </table>

                        </div>



                    </asp:Panel>
                </div>
        </div>
        <!-- jQuery -->
    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

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
    </script>
   

    <script src="js1/jquery.js"></script>
    <script src="js1/jquery-1.11.3.min.js"></script>
    <!-- Bootstrap Core JavaScript -->
    <%-- <script src="js1/bootstrap.min.js"></script>--%>
</asp:Content>


