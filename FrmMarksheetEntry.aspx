<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="FrmMarksheetEntry.aspx.cs" Inherits="FrmMarksheetEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            background-size: 100% 340.623px;
            /*Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;"*/
        }

        .form-control {
            height: 26px !important;
            padding: 2px 2px;
        }
       
        .myRequire {font-size:11px;
        }
    </style>

    <script>
        function foilshow() {
            $("#DivCF").hide();
            $("#imgdiv").show();
            $('#<%= ImageSelected.ClientID %>').css({
                "width": "1282px",
                "background-size": "100% 100%",
                "background-position": " 0 0"
            });
            $(this).toggleClass('red');
        };
        function Cfoilshow() {
            $("#DivCF").show();
            $("#imgdiv").hide();
            $('#<%= ImgCF.ClientID %>').css({
                "width": "1282px",
                "background-size": "100% 100%",
                "background-position": " 0 0"
            });
            $(this).toggleClass('red');
            $(this).toggleClass('red');
        };

    </script>
      <script type="text/javascript">
          function CheckFirstChar(key, txt) {
              if (key == 32 && txt.value.length <= 0) {
                  return false;
              }
              else if (txt.value.length > 0) {
                  if (txt.value.charCodeAt(0) == 32) {
                      txt.value = txt.value.substring(1, txt.value.length);
                      return true;
                  }
              }
              return true;
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid student-form" style="font-family: Calibri; margin-top: 0%;">
        <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
            <tr style="background-color: #50618c">
                <td style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">


                    <table style="width: 100%">
                        <tr style="background-color: gray;">
                            <td style="width: 10%">
                                <button type="button" id="btnfoil" style="font-weight: bold; font-family: Arial; padding:0px; margin-left:10px; font-size: 12px; width: 105px; height: 25px; text-align: center"
                                    onclick="foilshow(); this.style.background='#50618c'; this.style.color='white'; document.getElementById('btncfoil').style.background='gray'; 
                                            document.getElementById('btncfoil').style.color='white'"
                                    class="btn btn-default ">
                                    <span class="text-primary1">FOIL </span>
                                </button>
                            </td>
                            <td style="width: 10%">
                                <button type="button" id="btncfoil" style="font-weight: bold; font-family: Arial;padding:0px;  font-size: 12px; width: 105px; height: 25px; text-align: center"
                                    onclick="Cfoilshow();this.style.background='#50618c';this.style.color='white'; document.getElementById('btnfoil').style.background='gray'; 
                                            document.getElementById('btnfoil').style.color='white'"
                                    class="btn btn-default  ">
                                    <span class="text-primary1">COUNTER FOIL </span>
                                </button>
                            </td>
                            <td style="width: 25%">
                                <asp:Label ID="lblstuname" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lblroll" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblapplicationnumber" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblappliedfor" Style="font-size: 12px" runat="server" Text=""></asp:Label>
                            </td>


                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px; text-align: center;">

                    <div id="imgdiv" style="height: 235px; overflow: auto">

                        <div>
                            <%--<asp:Image class='zoom' CssClass="" ID="ImageSelected" Width="100%" Height="235px" Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;" runat="server" border="5" />--%>
                            <asp:Image class='zoom' ID="ImageSelected" Width="1282px" Height="600px" runat="server" />
                            <script src="wheelzoom.js"></script>
                            <script>
                                wheelzoom(document.querySelector('img.zoom'));
                            </script>
                        </div>
                    </div>
                    <div id="DivCF" style="height: 235px; overflow: auto" hidden>
                        <%--<asp:Image class='zoom22' ID="ImgCF" Width="100%" Height="235px" Style="background-size: 1159.57px 340.623px; background-position: 0px -42px;" runat="server" border="5" />--%>
                        <asp:Image class='zoom22' ID="ImgCF" Width="1282px" Height="600px" runat="server" />
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
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td style="width: 15%;" rowspan="2">
                                <table style="width: 100%; height: 280px; border: solid; border-style: solid; border-width: 2px;">
                                    <tr>
                                        <td class="tablecell" style="text-align: center">
                                            <img id="img1" runat="server" src="images/succ.jpg" style="width: 26px; height: 29px; border-color: white;" />
                                            <asp:Button ID="btnstudetail" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="STUDENT DETAIL" OnClick="btnstudetail_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tablecell" style="text-align: center">
                                            <img id="img2" runat="server" src="images/succ.jpg" style="width: 26px; height: 29px; border-color: white;" />
                                            <asp:Button ID="btnsubject" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="SUBJECT DETAIL"
                                                OnClick="btnsubject_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tablecell" style="text-align: center">
                                            <img id="img3" runat="server" src="images/succ.jpg" style="width: 26px; height: 29px; border-color: white;" />
                                            <asp:Button ID="btnresult" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="RESULT DETAIL" OnClick="btnresult_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tablecell" style="text-align: center">
                                            <img id="img4" runat="server" src="images/succ.jpg" style="width: 26px; height: 29px; border-color: white;" />
                                            <asp:Button ID="btnfinal" ValidationGroup="s" runat="server" class="btn btn-success" Style="width: 120px;" Text="FINAL SUBMISSION" OnClick="btnfinal_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 85%; border: solid; border-style: solid; border-width: 2px;">

                                <asp:Panel ID="pnlstudentdetail" runat="server">


                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label for="usr">ROLL NO.</label></div>
                                        <div class="col-md-2">
                                            <asp:TextBox CssClass="form-control" ID="txtStudentRoll" runat="server" MaxLength="6" placeholder="Enter Roll No."/>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="usr">STUDENT NAME</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox CssClass="form-control" Style="text-transform: uppercase" ID="txtStudentName" runat="server" placeholder="Enter Student Name"/>
                                        </div>

                                    </div>
                                    <div class="col-md-12" style="text-align: center; color: red">
                                        &nbsp  
                                      <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStudentRoll" Display="Dynamic" ErrorMessage="PLEASE ENTER STUDENT ROLL NO."></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStudentName" Display="Dynamic" ErrorMessage="PLEASE ENTER STUDENT NAME"></asp:RequiredFieldValidator>
                                         
                                          <cc1:FilteredTextBoxExtender runat="server" ID="filterCourseCode" TargetControlID="txtStudentRoll"
                                                ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                         <cc1:FilteredTextBoxExtender runat="server" ID="filterExamShortName" TargetControlID="txtStudentName"
                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                            </cc1:FilteredTextBoxExtender>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="">
                                            <label for="txtexamname" class="col-md-2">EXAM NAME</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox CssClass="form-control" ID="txtexamname" runat="server" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" 
                                                Style="text-transform: uppercase" MaxLength="100" placeholder="Enter Exam Name"/>
                                        </div>

                                    </div>
                                    <div class="col-md-12" style="text-align: center; color: red">
                                        &nbsp  
                                        <asp:RequiredFieldValidator CssClass="myRequire" ID="reqexam" runat="server" ControlToValidate="txtexamname" Display="Dynamic" ErrorMessage=" ENTER EXAM NAME"></asp:RequiredFieldValidator>
                                       
                                    </div>
                                    <div class="col-md-10 col-md-offset-2">
                                        <div class="col-md-2 text-justify">
                                            <label for="usr">ENROLLMENT NO.</label>
                                            <asp:TextBox CssClass="form-control" ID="txtEnrollment" placeholder="Alpha Numeric " runat="server" Style="text-transform: uppercase" MaxLength="12" />
                                            
                                          <p>  <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator8" runat="server" Style="color: red"  ControlToValidate="txtEnrollment" Display="Dynamic" ErrorMessage="ENTER ENROLLMENT NO.">
                                            </asp:RequiredFieldValidator></p>

                                              <cc1:FilteredTextBoxExtender runat="server"  ID="FilteredTextBoxExtender4" TargetControlID="txtEnrollment"
                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/-">
                                            </cc1:FilteredTextBoxExtender> 
                                        </div>
                                        <div class="col-md-2 text-justify">
                                            <label for="usr">CENTER NO.</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcenterno" placeholder="Numeric Only" runat="server" Style="text-transform: uppercase" MaxLength="10" />
                                            <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator4" runat="server" Style="color: red" ControlToValidate="txtcenterno" Display="Dynamic" ErrorMessage="ENTER CENTER NO.">

                                            </asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txtcenterno"
                                                ValidChars="1234567890">
                                            </cc1:FilteredTextBoxExtender>

                                        </div>
                                        <div class="col-md-2 text-justify">
                                            <label for="usr">COLLEGE NO.</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcollageno" placeholder="Numeric Only" runat="server" Style="text-transform: uppercase" MaxLength="10" />
                                            <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator5" Style="color: red" runat="server" ControlToValidate="txtcollageno" Display="Dynamic" ErrorMessage="ENTER COLLEGE NO.">

                                            </asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtcollageno"
                                                ValidChars="1234567890">
                                            </cc1:FilteredTextBoxExtender>


                                        </div>
                                        <div class="col-md-2 text-justify">
                                            <label for="usr">MEDIUM</label>
                                            <asp:TextBox CssClass="form-control" placeholder="Enter Medium" ID="txtmedium" runat="server" Style="text-transform: uppercase" MaxLength="10" />
                                            <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator6" runat="server" Style="color: red" ControlToValidate="txtmedium" Display="Dynamic" ErrorMessage=" ENTER MEDIUM"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtmedium"
                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz">
                                            </cc1:FilteredTextBoxExtender>
                                        
                                        </div>
                                        <div class="col-md-2 text-justify">
                                            <label for="usr">CATEGORY</label>
                                            <asp:TextBox CssClass="form-control" placeholder="Enter Category" ID="txtcategory" runat="server" Style="text-transform: uppercase" MaxLength="15" />
                                            <asp:RequiredFieldValidator CssClass="myRequire" ID="RequiredFieldValidator7" runat="server" Style="color: red" ControlToValidate="txtcategory" Display="Dynamic" ErrorMessage="ENTER CATEGORY">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        
                                    </div>




                                    <div class="col-md-12 text-right">
                                        <asp:Button ID="btnstudentdetailok" Style="width: 100px;" Text="OK" runat="server" CssClass="col-md-offset-5 btn btn-primary" OnClick="btnstudentdetailok_Click" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlSubject" runat="server">
                                    <table>
                                        <tr>
                                            <td>Subject Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubject" runat="server" TextMode="MultiLine" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnsubjectOK" Text="OK" runat="server" CssClass="col-md-offset-11 btn btn-primary" OnClick="btnsubjectOK_Click" />

                                            </td>
                                            <td></td>
                                        </tr>

                                    </table>

                                </asp:Panel>
                                <asp:Panel ID="PnlResult" runat="server">
                                    <div>
                                        <table>
                                            <tr>
                                                <td>Obtain Marks
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtObtain" runat="server" />
                                                </td>
                                                <td>Out Of Marks
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

                            </td>
                        </tr>

                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <!-- jQuery -->
    </div>


</asp:Content>

