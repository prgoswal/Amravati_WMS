<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmZeroLevel.aspx.cs" Inherits="FrmSecondLevel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="js/jquery-1.3.2.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/dg-picture-zoom.css" />
    <script src="jsZoom/external/mootools-1.2.4-core-yc.js"></script>
    <script src="jsZoom/external/mootools-more.js"></script>
    <script src="jsZoom/dg-picture-zoom-autoload.js"></script>
    <script src="jsZoom/dg-picture-zoom.js"></script>
    <script type="text/javascript" src="libs/jquery/jquery.js"></script>
    <script type="text/javascript" src="libs/jquery/jquery-ui.js"></script>
    <script type="text/javascript" src="libs/jquery.fs.zoetrope.min.js"></script>
    <script type="text/javascript" src="libs/toe.min.js"></script>
    <script type="text/javascript" src="libs/jquery.mousewheel.min.js"></script>
    <script type="text/javascript" src="src/imgViewer.js"></script>
    <script src="bootstrap-3.3.6-dist/js/wheelzoom.js"></script>--%>

    <script type="text/javascript" id="MyKey" lang="javascript">  <%-- For open another window --%>
        var myWindow;
        function windowOpen() {
            myWindow = window.open($('#<%=hiddenfield.ClientID%>').val() + 'FrmMarksheetEntry.aspx', '_blank', 'width=1500,height=800, scrollbars=no,resizable=no')
            myWindow.focus()
            return false;
        }
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

        <%--function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {

            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
            //location.reload();
        }--%>
        //$("#btnCls").live('click', function () {
        //    HidePopup();

        //});

        function closeModal() {
            $('#<%=cancelApplicationModal.ClientID%>').hide();
        }
    </script>
    <style type="text/css">
        body {
            font-family: Trebuchet MS, Arial;
            width: 100%;
        }

        #content {
            width: 760px;
            margin: 0 auto;
        }

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

        #noti_Button {
            width: 22px;
            height: 22px;
            line-height: 22px;
            background: #FFF;
            margin: -3px 10px 0 10px;
            cursor: pointer;
            color: #003F8E;
        }
        /* THE POPULAR RED NOTIFICATIONS COUNTER. */
        #noti_Counter {
            display: block;
            position: absolute;
            background: #E1141E;
            color: #FFF;
            font-size: 12px;
            font-weight: normal;
            padding: 1px 3px;
            margin: -2px 0 0 9px;
            border-radius: 2px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            z-index: 1;
            top: -6px;
            left: 130px;
            border-radius: 14px;
            width: 20px;
        }

        #noti_Container {
            position: relative;
        }
        /* A CIRCLE LIKE BUTTON IN THE TOP MENU. */
        #noti_Button {
            width: 0px;
            height: 22px;
            line-height: 22px;
            margin: -2px 0 0 9px;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 class="header-title">
        <%--<asp:Label runat="server" ID="lblHeadeing" Style="font-weight: 600;" Text="FIRST LEVEL"></asp:Label>--%></h3>
    <div class="box">
        <div class="row">
            <div class="col-md-12 col-lg-12 text-center">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 70%">
                            <h4>
                                <asp:Label runat="server" ID="lblPending" Style="font-weight: 600; margin-left: 40%" Text="Pending Application"> </asp:Label>
                            </h4>
                        </td>
                        <td style="width: 15%">
                            <div style="text-align: left; margin-left: 20%;">
                                <span>Pending Since :</span>
                                &nbsp;<asp:Label runat="server" ID="lblPendingsince" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </div>
                        </td>
                        <td style="width: 15%">
                            <div id="noti_Container">
                                <!--SHOW NOTIFICATIONS COUNT.-->
                                <div id="noti_Counter" class="text-center">
                                    <asp:Label ID="lblCounter" runat="server" />
                                </div>
                                <asp:LinkButton ID="lnkCancelApplication" Text="Cancel Applications" Style="margin-right: 30px;" OnClick="lnkCancelApplication_Click" runat="server"></asp:LinkButton>
                                <div id="notifications"></div>
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="text-align: right; margin-right: 50px;">
                    <span>Total No. Of Application(s)&nbsp&nbsp</span>
                    <asp:Label ID="lblpendingcount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="col-md-12 col-lg-12 text-center">
                <table style="width: 99%; margin-left: 7px; background-color: #526a9c; color: white;">
                    <tr>
                        <td style="width: 5%; background-color: #526a9c; color: white;">Sr.No.</td>
                        <td style="width: 7%;">Application ID</td>
                        <td style="width: 12%;">Application Date</td>
                        <td style="width: 5%;">Roll No.</td>
                        <td style="width: 20%;">Student Name</td>
                        <td style="width: 35%;">Applied For</td>
                        <td style="width: 6%;">View Image</td>
                        <td style="width: 8%;">Cancel</td>
                    </tr>
                </table>
                <div class="text-center" style="overflow: auto; height: 140px; width: 100%">
                    <asp:GridView align="center" ID="grdPandingDetails" runat="server" DataKeyNames="RollNo" ShowHeader="false" over HeaderStyle-BackColor="#50618c"
                        HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="99%" CellPadding="4" RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef" BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false"
                        AutoGenerateColumns="false" HorizontalAlign="Center" OnRowCommand="grdPandingDetails_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApplicationID" ControlStyle-Height="100" ItemStyle-Width="7%" />
                            <asp:BoundField DataField="ApplicationDate" ControlStyle-Height="100" ItemStyle-Width="13%" />
                            <asp:BoundField DataField="RollNo" ControlStyle-Height="100" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="StudentName" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="AppliedFor" ItemStyle-Width="35%" />
                            <asp:TemplateField ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="ShowPopup">View</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="ShowCancelPopup">Cancel</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblCancelSuccessMSG" Style="float: right; margin-right: 10px;" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box">
        <div class="row">
            <div class="col-md-12 col-lg-12 text-center">
                <h4>
                    <asp:Label runat="server" ID="Label1" Style="font-weight: 600;"
                        Text="Completed Application">
                    </asp:Label>
                </h4>
                <div style="text-align: right; margin-right: 50px;">
                    <span>Total No. Of Application(s)&nbsp&nbsp</span>
                    <asp:Label ID="lblcompletecount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="col-md-12 col-lg-12 text-center">
                <table style="width: 99%; margin-left: 7px; background-color: #526a9c; color: white; text-align: center">
                    <tr>
                        <td style="width: 5%; background-color: #526a9c; color: white;">Sr.No.</td>
                        <td style="width: 8%;">Application ID</td>
                        <td style="width: 12%;">Application Date</td>
                        <td style="width: 10%;">Roll No.</td>
                        <td style="width: 20%;">Student Name</td>
                        <td style="width: 35%">Applied For</td>
                        <td style="width: 10%">Status</td>
                    </tr>
                </table>
                <div class="text-center" style="overflow: auto; width: 100%">
                    <asp:GridView align="center" ID="grdCompleted" runat="server" DataKeyNames="RollNo" ShowHeader="false" HeaderStyle-BackColor="#50618c"
                        HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="99%" CellPadding="4"
                        RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false" AutoGenerateColumns="false" HorizontalAlign="Center" OnRowCommand="grdCompleted_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApplicationID" ControlStyle-Height="100" ItemStyle-Width="8%" />
                            <asp:BoundField DataField="ApplicationDate" ControlStyle-Height="100" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="RollNo" ControlStyle-Height="100" ItemStyle-Width="8%" />
                            <asp:BoundField DataField="StudentName" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="AppliedFor" ItemStyle-Width="35%" />
                            <asp:TemplateField ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonTrail" runat="server" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="ShowStatus">Status</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div id="mask"></div>

    <style>
        .divOveFlow {
            max-height: 370px;
            overflow-y: scroll;
        }

        .p-r17 {
            padding-right: 17px;
        }
    </style>

    <%-------------------------------------------Model PopUP----------------------------------------------------%>

    <div class="modal fade in" runat="server" id="viewApplicationModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
        <div class="modal-dialog modal-sm vertical-align-center" style="width: 90%;" role="document">
            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 25px">
                <asp:Panel ID="Panel1" runat="server" Width="100%" Height="50%" Visible="true">
                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                        <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                            <tr style="background-color: #95969c">
                                <td id="htd1" colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="left">
                                    <asp:Label ID="LblPopupHeading" runat="server" Text="Foil"></asp:Label>&nbsp;
                                          <input id="b1" type="button" class="btn btn-primary" name="name" value="Show" />
                                </td>
                                <td id="htd2" colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Counter Foil"></asp:Label>&nbsp;
                                            <input id="b2" type="button" class="btn btn-primary" name="name" value="Show" />
                                </td>
                                <td colspan="3" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">
                                    <asp:Label ID="lblEntry" Style="margin-right: 15px; font-size: 15px;" runat="server" Text=""></asp:Label><br />
                                    Application No.
                                        <asp:Label ID="lblappno" ForeColor="floralwhite" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lnkViewAppClose" OnClick="lnkViewAppClose_Click" Style="color: white; float: right; text-decoration: none;" runat="server"><img src="images/cancel-512.png" alt="Alternate Text" style="margin-top: -54px;" height="20px" width="20px" /></asp:LinkButton>

                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-body">
                        <style>
                            .modal {
                                display: none;
                                overflow: hidden;
                                position: fixed;
                                top: 0;
                                right: 0;
                                bottom: 0;
                                left: 0;
                                z-index: 1050;
                                -webkit-overflow-scrolling: touch;
                                outline: 0;
                            }

                            .modal-header {
                                padding: 5px;
                                border-bottom: 1px solid #e5e5e5;
                            }

                            .fade.in {
                                opacity: 1;
                            }

                            .fade {
                                opacity: 0;
                                -webkit-transition: opacity .15s linear;
                                -o-transition: opacity .15s linear;
                                transition: opacity .15s linear;
                            }

                            .modal-dialog {
                                width: 600px;
                                margin: 30px auto;
                            }

                            .modal-content {
                                -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                box-shadow: 0 5px 15px rgba(0,0,0,.5);
                            }

                            .modal-sm {
                                width: 300px;
                            }

                            .modal-body {
                                position: relative;
                                padding: 4px 5px !important;
                            }

                                .modal-body table {
                                    font-size: 12px;
                                }

                            .table-bordered1 {
                                border: 1px solid #918f8f; /*918f8f*/
                            }

                                .table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {
                                    border: 1px solid #918f8f;
                                }

                                .table-bordered1 > thead > tr > th, .table-bordered1 > thead > tr > td {
                                    border-bottom-width: 2px;
                                }

                            .first_tr_hide tbody tr:first-child {
                                display: none;
                            }

                            .hide_my_pdosi + tr {
                                display: none;
                            }

                            .first_tr_hide tr td:first-child {
                                display: none;
                            }

                            .calign {
                                text-align: center;
                            }

                            .ralign {
                                text-align: right;
                            }

                            .marginTop {
                                margin-top: 5px;
                            }

                            .width10 {
                                width: 10%;
                            }

                            .width65 {
                                width: 65%;
                            }

                            .width85 {
                                width: 85%;
                            }

                            .width50 {
                                width: 50%;
                            }

                            .width20 {
                                width: 20%;
                            }

                            .width15 {
                                width: 15%;
                            }

                            .width-fix {
                                height: 520px;
                            }

                                .width-fix img {
                                    width: 450px !important;
                                    height: 520px !important;
                                }
                        </style>
                        <table class="table-bordered1" style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                            <tr>
                                <td id="td1" colspan="2" style="border: 2px solid Red; padding-top: 10px; text-align: center;">
                                    <div id="imgdiv" class="width-fix">
                                        <asp:Image ID="ImageFoil" alt="" class="zoom marksheet" Style="top: 0px; width: 450px; max-width: 800px !important; align-content: center; height: 462px; position: inherit;"
                                            runat="server" />
                                        <script src="js/wheelzoom.js"></script>
                                        <script>
                                            wheelzoom(document.querySelector('img.zoom'));
                                        </script>
                                    </div>
                                </td>
                                <td id="td2" colspan="2" style="border: 2px solid Red; padding-top: 10px; text-align: center;">
                                    <div id="Div1" class="width-fix">
                                        <asp:Image ID="ImageCF" class='zoom1 marksheet width-fix' alt="" Style="top: 0px; align-content: center; max-width: 800px !important; height: 462px; position: inherit;" runat="server" />
                                        <script src="js/wheelzoom.js"></script>
                                        <script>
                                            wheelzoom(document.querySelector('img.zoom1'));
                                        </script>
                                    </div>
                                </td>
                                <td style="border: 2px solid; padding-top: 10px; padding-right: 1%; text-align: center; width: 38%">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlform" runat="server" Visible="false">
                                                <div id="DivBlink" runat="server" class="bg-primary blink_me " role="alert" style="margin-left: 8px; border-radius: 10px">
                                                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                    <span class="sr-only">Error:</span>
                                                    Data Not Available / Incomplete, Please go for manual entry.
                                                </div>
                                                <%--Universtiy Data Not Available,You can go for Manual Entry.--%>
                                                <table class="table table-bordered" style="margin-left: 1%; height: 100px">
                                                    <tr>
                                                        <th>Exam Name
                                                        <asp:Label runat="server" ID="Label4" Style="color: red; font-weight: 600" Text=" *"></asp:Label>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtExamName" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);"
                                                                CssClass="form-control input-sm" TabIndex="1" Style="text-transform: uppercase" MaxLength="100"
                                                                required="" data-validation-required-message="Exam Name Required"
                                                                runat="server" placeholder="Enter Exam Name"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Branch Name
                                                        <asp:Label runat="server" ID="Label5" Style="color: red; font-weight: 600"></asp:Label>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtBranchName" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);"
                                                                CssClass="form-control input-sm" TabIndex="2" Style="text-transform: uppercase" MaxLength="50"
                                                                runat="server" placeholder="Enter Branch Name"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Exam Session
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtExamSession" runat="server" placeholder="Enter Exam Session" CssClass="form-control input-sm" TabIndex="3"
                                                                required="" data-validation-required-message="Exam Session Required"
                                                                Style="text-transform: uppercase" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Exam Year
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtExamYear" runat="server" placeholder="Enter Exam Year" CssClass="form-control input-sm" TabIndex="4"
                                                                required="" data-validation-required-message="Exam Year Required"
                                                                Style="text-transform: uppercase" MaxLength="4"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtExamYear"
                                                                ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Roll No.
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtRollNo" CssClass="form-control input-sm" TabIndex="1" Enabled="false"
                                                                runat="server" MaxLength="5" placeholder=""></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="filterCourseCode" TargetControlID="txtRollNo"
                                                                ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>Name<asp:Label runat="server" ID="Label3" Style="color: red; font-weight: 600" Text=" *"></asp:Label>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtStudentName" CssClass="form-control input-sm" Style="text-transform: uppercase"
                                                                required="" data-validation-required-message="Student Name Required"
                                                                runat="server" TabIndex="6" MaxLength="40" placeholder="Enter Student Name"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="filterExamShortName" TargetControlID="txtStudentName"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trDivision" runat="server" visible="false">
                                                        <th>Division</th>
                                                        <td>
                                                            <asp:TextBox ID="txtDivision" CssClass="form-control input-sm" Style="text-transform: uppercase" TabIndex="7" MaxLength="6"
                                                                required="" data-validation-required-message="Enter Division"
                                                                runat="server" placeholder="Enter Division"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender11" TargetControlID="txtDivision"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trCgpa" runat="server" visible="false">
                                                        <th>CGPA
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtCGPA" CssClass="form-control input-sm" TabIndex="8" MaxLength="5"
                                                                required="" data-validation-required-message="Enter CGPA"
                                                                runat="server" placeholder="Enter CGPA No."></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txtCGPA"
                                                                ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trEnroll" runat="server" visible="false">
                                                        <th>Enrollment No.</th>
                                                        <td>
                                                            <asp:TextBox ID="txtEnrollmentlNo" CssClass="form-control input-sm" TabIndex="9" Style="text-transform: uppercase" MaxLength="10"
                                                                runat="server" placeholder="Enter Enrollment No."></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4" TargetControlID="txtEnrollmentlNo"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/-"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trCollege" runat="server" visible="false">
                                                        <th>College</th>
                                                        <td>
                                                            <asp:TextBox ID="txtCollege" CssClass="form-control input-sm" TabIndex="10" Style="text-transform: uppercase" MaxLength="7"
                                                                required="" data-validation-required-message="Enter College Code"
                                                                runat="server" placeholder="Enter College Code"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtCollege"
                                                                ValidChars="1234567890"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trSubjectName" runat="server" visible="false">
                                                        <th>Subject Name</th>
                                                        <td>
                                                            <asp:TextBox ID="txtSubjectName" CssClass="form-control input-sm text-uppercase" Height="50px" TextMode="MultiLine" TabIndex="11"
                                                                required="" data-validation-required-message="Entre Subject Name"
                                                                runat="server" placeholder="Enter Subject Name"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender5" TargetControlID="txtSubjectName"
                                                                ValidChars="1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz*-&"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trDistinctionSub" runat="server" visible="false">
                                                        <th>Distinction Subject</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="12" ID="txtDistinctionSub" CssClass="form-control input-sm text-uppercase" Style="text-transform: uppercase" Height="65px" TextMode="MultiLine"
                                                                runat="server" placeholder="Enter Distinction Subject"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" TargetControlID="txtDistinctionSub"
                                                                ValidChars="1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz*-&"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trMeritNo" runat="server" visible="false">
                                                        <th>Merit No.</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="13" ID="txtMeritNo" CssClass="form-control input-sm" MaxLength="3" runat="server" placeholder="Enter Merit No."
                                                                required="" data-validation-required-message="Enter Merit No."></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7" TargetControlID="txtMeritNo" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trLaterReferenceNo" runat="server" visible="false">
                                                        <th>Letter Reference No.</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="14" ID="txtLaterReferenceNo" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="20"
                                                                required="" data-validation-required-message="Letter Reference No. Required"
                                                                runat="server" placeholder="Enter Letter Reference No."></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender8" TargetControlID="txtLaterReferenceNo"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz0123456789/-"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trLaterReferenceDate" runat="server" visible="false">
                                                        <th>Letter Reference Date</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="15" ID="txtLaterReferenceDate" CssClass="form-control datepicker"
                                                                required="" data-validation-required-message="Letter Reference Date Required"
                                                                runat="server" placeholder="Enter Letter Reference Date"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trPassingYear" runat="server" visible="false">
                                                        <th>Passing Year</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="16" ID="txtPassingYear" CssClass="form-control input-sm" MaxLength="4" runat="server" placeholder="Enter Passing Year"
                                                                required="" data-validation-required-message="Passing Year Required"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9" TargetControlID="txtPassingYear" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trExamMedium" runat="server" visible="false">
                                                        <th>Exam Medium</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="17" ID="txtExamMedium" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="10"
                                                                required="" data-validation-required-message="Exam Medium Required"
                                                                runat="server" placeholder="Enter Exam Medium"></asp:TextBox>

                                                            <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender10" TargetControlID="txtExamMedium"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trResulDeclarationDate" runat="server" visible="false">
                                                        <th>Result Declaration Date</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="18" ID="txtDateResultDeclaration" TextMode="Date" CssClass="form-control input-sm"
                                                                required="" data-validation-required-message="Result Declaration Date Required"
                                                                runat="server" placeholder="Enter Result Declaration Date"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trRank" runat="server">
                                                        <th>Rank</th>
                                                        <td>
                                                            <asp:DropDownList TabIndex="19" ID="ddlRank" runat="server" CssClass="form-control" required="">
                                                                <asp:ListItem Value="">--- Select ---</asp:ListItem>
                                                                <asp:ListItem>Ist</asp:ListItem>
                                                                <asp:ListItem>IInd</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr id="trAwardedBy" runat="server">
                                                        <th>Awarded By</th>
                                                        <td>
                                                            <asp:DropDownList TabIndex="20" ID="ddlAwardedby" required="" runat="server" CssClass="form-control" OnTextChanged="ddlAwardedby_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="---- Select ----" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Gold Medal" />
                                                                <asp:ListItem Text="Silver Medal" />
                                                                <asp:ListItem Text="Cash Prize" />
                                                            </asp:DropDownList>
                                                        </td>

                                                    </tr>
                                                    <tr id="TrawardPrice" runat="server">
                                                        <th>Award Prize</th>
                                                        <td>
                                                            <asp:TextBox TabIndex="21" CssClass="form-control" runat="server" ID="txtAwardPrize" Required="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button TabIndex="23" ID="lnkSubmit" class="btn btn-primary" runat="server" Text="SUBMIT"
                                                                OnClick="lnkSubmit_Click" />
                                                            <asp:Label Text="" ID="msgVal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Label runat="server" ID="lblmsg" Text="aaa" Style="color: red; font-weight: 600; text-align: center"></asp:Label>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblremarks" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 100px; padding-top: 0px;" cellpadding="0">
                                        <tr>
                                            <td>Remark
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="200" Width="100%"
                                                    TabIndex="22" runat="server" placeholder="Enter Remark" />
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnApprove" class="btn btn-primary" TabIndex="24" runat="server" Text="APPROVE" OnClick="btnApprove_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnReject" class="btn btn-primary" TabIndex="25" runat="server" Text="REJECT" OnClick="btnReject_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCorrection" class="btn btn-primary" TabIndex="26" runat="server" Text="CORRECTION" OnClick="btnCorrection_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:Panel ID="imgPanel" runat="server" BackColor="White"
                                Style="z-index: 111; background-color: red; width: 746px; position: absolute; top: 4px; left: 5px; height: 534px; display: none">

                                <div id="divimg" style="width: 100%;">
                                    <asp:Image class='zoom2' ID="HideFoil" runat="server" border="2" Style="width: 742px; position: absolute; top: 2px; left: 2px; height: 530px;" />
                                    <script src="js/wheelzoom.js"></script>
                                    <script>
                                        wheelzoom(document.querySelector('img.zoom2'));
                                    </script>
                                </div>

                            </asp:Panel>
                        </div>
                        <div>
                            <asp:Panel ID="imgpanel2" runat="server" BackColor="White"
                                Style="z-index: 111; background-color: red; width: 746px; position: absolute; top: 4px; left: 5px; height: 534px; display: none">

                                <div id="divimg1" style="width: 100%;">
                                    <asp:Image class='zoom3' ID="HideCF" runat="server" border="2" Style="width: 742px; position: absolute; top: 2px; left: 2px; height: 530px;" />
                                    <script src="js/wheelzoom.js"></script>
                                    <script>
                                        wheelzoom(document.querySelector('img.zoom3'));
                                    </script>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="form-inline">
            <div class="" style="padding: 10px;">
                <div class="col-lg-5 col-md-5 col-md-offset-1 col-lg-offset-1">
                    <div>
                        <%--<asp:Button Text="" CssClass="btn btn-primary" ID="" runat="server" />--%>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-lg-4 col-md-4">
                        <div class="">
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="modal fade in" runat="server" id="cancelApplicationModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 141px">
                <asp:Panel ID="pnlCancelApplication" runat="server" Width="100%" Height="100%" Visible="true">
                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                        <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                            <tr style="background-color: #95969c">
                                <td colspan="6" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px; text-align: center" align="left">
                                    <asp:Label ID="Label6" Text="Cancelled Application" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-body">
                        <table class="table-bordered1" style="width: 100%">
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    <b>Application ID : </b>
                                    <asp:Label ID="lblApplicationID" runat="server" />
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <b>Application Date : </b>
                                    <asp:Label ID="lblApplicationDate" runat="server" />
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <b>Roll No. : </b>
                                    <asp:Label ID="lblRollNo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    <b>Student Name : </b>
                                    <asp:Label ID="lblStudentName" runat="server" />
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <b>Applied For : </b>
                                    <asp:Label ID="lblAppliedFor" runat="server" />
                                </td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: left; vertical-align: top">

                                    <div style="height: 100%; width: 12%; float: left">
                                        <b>Cancel Reason
                                        <label style="color: red">*</label>
                                            : </b>
                                    </div>
                                    <asp:TextBox ID="txtCancelReason" TextMode="MultiLine" Style="height: 100%; width: 88%; float: left"
                                        MaxLength="199" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <div class="modal-footer">
                    <asp:Label ID="lblCancelMSG" Text="" ForeColor="Red" runat="server" />
                    <asp:LinkButton ID="lnkSave" Text="Save" CssClass="btn btn-primary" OnClick="lnkSave_Click" runat="server" />
                    <asp:LinkButton ID="lnkCancel" Text="Cancel" CssClass="btn btn-danger" OnClick="lnkCancel_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" runat="server" id="cancelApplicationListModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 141px">
                <asp:Panel ID="pnlCancelApplicationList" runat="server" Width="100%" Height="100%" Visible="true">
                    <div class="modal-header c" style="background-color: #4D6188; color: #545454;">
                        <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                            <tr style="background-color: #95969c">
                                <td colspan="4" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px; text-align: center" align="left">
                                    <asp:Label ID="lblCancelApplicationList" Text="Cancelled Application List" runat="server"></asp:Label>
                                </td>
                                <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">
                                    <asp:LinkButton ID="lnkCancelAppListClose" Style="color: white; float: right; text-decoration: none;" OnClick="lnkCancelAppListClose_Click"
                                        runat="server"><img src="images/cancel-512.png" alt="Alternate Text" style="margin-top: -12px; margin-right: -3px;" 
                                            height="20px" width="20px" /></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <style>
                        .cancelListHeadColor {
                            background-color: #4E6A98;
                            color: #fff;
                        }
                    </style>
                    <div class="modal-body">
                        <table class="table-bordered1" style="width: 100%">
                            <tr>
                                <td colspan="6">
                                    <div class="divOveFlow">
                                        <asp:GridView ID="grdCancelApplicationList" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr class="cancelListHeadColor">
                                                            <th style="width: 15%">
                                                                <asp:Label ID="lblHeaderSrNo" Text="Sr. No." runat="server"></asp:Label></th>
                                                            <th style="width: 15%">
                                                                <asp:Label ID="lblHeaderAN" Text="Application No." runat="server"></asp:Label></th>
                                                            <th style="width: 15%">
                                                                <asp:Label ID="lblHeaderAD" Text="Application Date" runat="server"></asp:Label></th>
                                                            <th style="width: 15%">
                                                                <asp:Label ID="lblHeaderRN" Text="Roll No." runat="server"></asp:Label></th>
                                                            <th style="width: 20%">
                                                                <asp:Label ID="lblHeaderSN" Text="Student Name" runat="server"></asp:Label></th>
                                                            <th style="width: 20%">
                                                                <asp:Label ID="lblHeaderAF" Text="Applied For" runat="server"></asp:Label></th>
                                                        </tr>
                                                        <tr class="hide_my_pdosi">
                                                        </tr>
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicationNo" Text='<%#Eval("ApplicationID") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicationDate" Text='<%#Eval("ApplicationDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRollNo" Text='<%#Eval("RollNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentName" Text='<%#Eval("StudentName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAppliedFor" Text='<%#Eval("AppliedFor") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:Label ID="lblNoRecord" Text="" Visible="false" ForeColor="Red" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <div class="modal-footer">
                    <%--<button type="button" id="btnPrintDetails" class="font-awesome-font btn btn-info" onclick="PrintDeatilPanel();" data-dismiss="modal">Print</button>--%>
                    <%--<input type="button" id="btncancelDetail" class="btn btn-danger" value="Cancel" data-dismiss="modal" />--%>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hiddenfield" runat="server" />
    <script type="text/javascript">
        function ShowPopupPrint() {
            //$('#mask').show();
            $('#<%=imgPanel.ClientID %>').show();
        }
        function HidePopupPrint() {
            //$('#mask').hide();
            $('#<%=imgPanel.ClientID %>').hide();
        }
        //$(".btnClose11").live('Click', function () {
        //    HidePopupPrint();
        //});
    </script>
    <script type="text/javascript">
        $("#b1").click(function () {

            if ($(this).val() == "Show") {
                $(this)
                .attr('value', 'Hide')
                .removeClass("btn-primary")
                .addClass("btn-danger");


                $('#<%=Label2.ClientID%>').hide();
                $("#td2").hide();
                $("#htd2").hide();
                $('#<%= ImageFoil.ClientID %>').css({
                    "width": "800px",
                    "background-size": "100% 100%"
                });
                $('#<%=imgPanel.ClientID %>').show();
                $("#b2").hide();
            }
            else {

                $(this)
               //.val("Show")
               .attr('value', 'Show')
               .removeClass("btn-danger")
               .addClass("btn-primary");
                $("#htd2").show();
                $("#td2").show();
                $('#<%=Label2.ClientID%>').show();
                $('#<%= ImageFoil.ClientID %>').css({
                    "width": "100%",
                    "background-size": "100% 100%"
                });
                $('#<%=imgPanel.ClientID %>').hide();
                $("#b2").show();
            }
        });
        $("#b2").click(function () {

            if ($(this).val() == "Show") {
                $(this)
                .attr('value', 'Hide')
                .removeClass("btn-primary")
                .addClass("btn-danger");
                $("#td1").hide();
                $("#htd1").hide();
                $('#<%=LblPopupHeading.ClientID%>').hide();
                $('#<%= ImageCF.ClientID %>').css({
                    "width": "100%",
                    "background-size": "100% 100%"
                });
                $('#<%=imgpanel2.ClientID %>').show();
                $("#b1").hide();
            }
            else {
                $(this)
               //.val("Show")
               .attr('value', 'Show')
               .removeClass("btn-danger")
               .addClass("btn-primary");
                $("#td1").show();
                $("#htd1").show();
                $('#<%=LblPopupHeading.ClientID%>').show();
                $('#<%= ImageCF.ClientID %>').css({
                    "width": "100%",
                    "background-size": "100% 100%"
                });
                $('#<%=imgpanel2.ClientID %>').hide();
                $("#b1").show();
            }
        });

        function blinker() {
            $('.blink_me').fadeOut(500);
            $('.blink_me').fadeIn(500);
        }

        setInterval(blinker, 1000);

    </script>
</asp:Content>
