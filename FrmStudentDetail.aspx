<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmStudentDetail.aspx.cs" Inherits="FrmStudentDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="js/1.10.2.jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui.min.js"></script>--%>
    <%--<link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="bootstrap-3.3.6-dist/Calender/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap-3.3.6-dist/Calender/jquery-ui.min.js" type="text/javascript"></script>
    <script src="bootstrap-3.3.6-dist/Calender/jquery.min.js" type="text/javascript"></script>--%>
    <%--<script src="js/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="Script/jquery-1.5.1min.js"></script>
    <link href="css/jquery.alerts.css" rel="stylesheet" type="text/css" />--%>
    <%-- <script src="js/Index.js"></script>--%>
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
            background-size: cover;
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

    <script type="text/javascript" lang="javascript">

        function PrintDiv() {
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('<style> body{padding: 15px 20px;} .table-bordered {border: solid ;}  border: 1px solid #ddd !important;</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }

        function ShowPopupPrint() {
            $('#mask').show();
            $('#modelPrint').show();
        }

        function HidePopupPrint() {
            $('#mask').hide();
            $('#modelPrint').hide();
        }

        //$(".btnClose11").live('Click', function () { HidePopupPrint(); });

        function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }

        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }

        //$(".btnClose").live('click', function () { HidePopup(); });

    </script>

    <script type="text/javascript">
        function toggleSelectionGrid(source) {
            var isChecked = source.checked;
            $("#GirdViewTable input[id*='CheckBox1']").each(function (index) { $(this).attr('checked', false); });
            source.checked = isChecked;
        }
    </script>

    <script type="text/javascript">

        function ValidateRegister() {
            debugger;
            if ($('#<%=DDLExamYear.ClientID%>').val() == 0) {
                $('#<%=DDLExamYear.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Year.', 'Error', function () { $("#DDLExamYear").focus() })
                return false;
            }
            else {
                $('#<%=DDLExamYear.ClientID%>').css('border-color', '');
            }

            if ($('#<%=DDLExamSession.ClientID%>').val() == 0) {
                $('#<%=DDLExamSession.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Session.', 'Error', function () { $("#DDLExamSession").focus() })
                return false;
            }
            else {
                $('#<%=DDLExamSession.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtRollNo.ClientID%>').val() == '') {
                $('#<%=txtRollNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Exam Full Name', 'Error', function () { $("#txtRollNo").focus() })
                return false;
            }
            else {
                $('#<%=txtRollNo.ClientID%>').css('border-color', '');
            }


            <%--if ($('#<%=txtExamShortName.ClientID%>').val() == '') {
                $('#<%=txtExamShortName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Exam Short Name', 'Error', function () { $("#txtExamShortName").focus() })
                return false;
            }
            else {
                $('#<%=txtExamShortName.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlExamType.ClientID%>').val() == 0) {

                $('#<%=ddlExamType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Type', 'Error', function () { $("#ddlExamType").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamType.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlTotalYr.ClientID%>').val() == "Select Year") {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Total No. Of Year/Sem', 'Error', function () { $("#ddlTotalYr").focus() })
                return false;
            }
            else {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlTotalYr.ClientID%>').val() == "Select Sem") {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Total No. Of Year/Sem', 'Error', function () { $("#ddlTotalYr").focus() })
                return false;
            }
            else {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', '');
            }--%>
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #noti_Button {
            width: 22px;
            height: 22px;
            line-height: 22px;
            background: #FFF;
            margin: -3px 10px 0 10px;
            cursor: pointer;
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

    <%--<script type="text/javascript">
        function ValidateRegister() {
            debugger;
            if ($('#<%=DDLExamYear.ClientID%>').val() == "0") {
                $('#<%=DDLExamYear.ClientID%>').css('border-color', 'Red');
                jAlert('Select Exam Year', 'Error', function () { $('#<%=DDLExamYear.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=DDLExamYear.ClientID%>').css('border-color', ''); }

            if ($('#<%=DDLExamSession.ClientID%>').val() == "0") {
                $('#<%=DDLExamSession.ClientID%>').css('border-color', 'Red');
                jAlert('Select Exam Session', 'Error', function () { $('#<%=DDLExamSession.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=DDLExamSession.ClientID%>').css('border-color', ''); }

            if ($('#<%=TxtStuName.ClientID%>').val() == '' && $('#<%=txtRollNo.ClientID%>').val() == '') {
                $('#<%=txtRollNo.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Roll No. Or Student Name.', 'Error', function () { $('#<%=txtRollNo.ClientID%>').focus() })
                return false;
            }

            if ($('#<%=DDLApply.ClientID%>').val() == "0") {
                $('#<%=DDLApply.ClientID%>').css('border-color', 'Red');
                jAlert('Select Applied For', 'Error', function () { $('#<%=DDLApply.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=DDLApply.ClientID%>').css('border-color', ''); }
        }
    </script>--%>

    <script>
        $(document).ready(function () {
            // ANIMATEDLY DISPLAY THE NOTIFICATION COUNTER.
            $('#noti_Counter')
                .css({ opacity: 0 })
                //.text('7')              // ADD DYNAMIC VALUE (YOU CAN EXTRACT DATA FROM DATABASE OR XML).
                .css({
                    margin: "-2px 0 0 9px",
                    width: "22px", "border-radius": "12px"
                })
                .animate({ top: '-4px', opacity: 1 }, 500);
        });

        $('#notifications').click(function () {
            return false;       // DO NOTHING WHEN CONTAINER IS CLICKED.
        });
    </script>

    <script type="text/javascript">
        function getConfirmation() {
            var retVal = confirm("Do you want to continue ?");
            if (retVal == true) {
                location.href = "FrmZeroLevel.aspx";
                alert("Done ");
                return true;
            }
            else {
                location.href = "FrmStudentDetail.aspx";
                alert("Not Done ");
                return false;
            }
        }
    </script>

    <div class="container-full" id="modelPrint" style="display: none;">
        <asp:Panel ID="pnlprint" runat="server" BackColor="White" Width="50%" Height="600px" ScrollBars="Horizontal"
            Style="z-index: 111; margin-top: -7%; position: absolute; left: 24%; top: 25%; border: outset 2px gray; padding: 5px;">
            <div class="col-sm-12">
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <%-- <asp:Button ID="btnprint" runat="server" Text="Print" class="btn btn-success" OnClick="btnprint_Click"  />--%>
                            </td>
                            <td>
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" onclick="return PrintDiv();">Print</button>
                            </td>
                            <td style="text-align: right">
                                <a id="closebtn1" class="btnClose11" data-toggle="modal" data-target="#myModal" href="javascript:HidePopupPrint();">
                                    <img src="images/croxx.png" style="height: 17px; width: 17px; background-color: white;" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="row" id="dvContents" style="margin: 10px; margin-top: 45px;">
                <style type="text/css">
                    /*This for report border*/
                    .double {
                        border-width: 10px;
                        -webkit-border-image: url(images/Bor-11.png) 7.11% round;
                        -moz-border-image: url(images/Bor-11.png) 7.11% round;
                        border-image: url(images/Bor-11.png) 7.11% round;
                        outline: 1px solid black;
                    }
                    /*This for Print Html Report*/
                    @media print {
                        @page {
                            margin: 0;
                            width: 21cm;
                            height: 29.7cm;
                        }
                    }
                </style>
                <div style="height: 45%;" class="double table-bordered">
                    <div style="width: 100%; height: 99.4%; border: 1px solid black; font-family: Calibri;">
                        <div style="padding: 20px; font-family: Calibri;">
                            <div class="col-md-12" style="font-family: Calibri;">
                                <div style="">
                                    <table style="font-weight: bold; text-align: center">
                                        <tr>
                                            <td style="font-family: Calibri; font-size: 16pt;">
                                                <%=DataAcces.universityName %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%;">
                                                <img src="<%=DataAcces.LogoUrl%>" style="margin-right: 10px; height: 66px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 12px; font-family: Calibri;">
                                <div style="text-align: center; font-weight: bold;">STUDENT COPY</div>
                            </div>
                            <div class="clearfix" style="margin-bottom: 20px;"></div>

                            <div class="col-sm-12" style="text-justify: auto">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 23%; text-align: right;">
                                            <span style="font-weight: bold">Application No.:</span>
                                        </td>
                                        <td colspan="2" style="text-align: left;">
                                            <asp:Label ID="lblappno" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2" style="text-align: right; width: 43%;">
                                            <span style="font-weight: bold">Application Date:  &nbsp;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblappdate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="clearfix" style="margin-bottom: 10px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <div>
                                    <span style="font-weight: bold; margin-left: 100px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;I</span>
                                    &nbsp;<asp:Label ID="lblStudentName" runat="server" Font-Underline="true"></asp:Label>
                                    &nbsp;<span style="font-weight: bold">&nbsp;&nbsp;&nbsp; &nbsp;Studied in Exam</span>
                                </div>
                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                <div>
                                    <asp:Label ID="lblExamNm" runat="server" Font-Underline="true"></asp:Label>
                                </div>
                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                <div>
                                    <span style="font-weight: bold">Session   &nbsp;</span>
                                    <asp:Label ID="lblSessionYear" Font-Underline="true" runat="server"></asp:Label>
                                    &nbsp;<span style="font-weight: bold">With RollNo.</span>
                                    &nbsp;<asp:Label ID="lblRollNo" Font-Underline="true" runat="server"></asp:Label>
                                    &nbsp;<span style="font-weight: bold">have applied for  </span>&nbsp;<asp:Label ID="lblAppliedfor" Font-Underline="true" runat="server"></asp:Label>.
                                </div>
                                <%--<div class="clearfix" style="margin-bottom: 20px;"></div>
                                <div>
                                    <asp:Label ID="lblAppliedfor" Font-Underline="true" runat="server"></asp:Label>.
                                </div>--%>
                            </div>
                            <div class="clearfix" style="margin-bottom: 70px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <table style="width: 90%;">
                                    <tr>
                                        <td style="width: 25%; text-align: right;">
                                            <span style="font-weight: bold">Please Oblige me.</span>
                                        </td>
                                        <td colspan="2" style="text-align: left;"></td>
                                        <td colspan="2" style="text-align: right; width: 59%;"></td>
                                        <td>
                                            <span style="font-weight: bold">Thanks</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clearfix" style="margin-bottom: 30px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <table style="width: 100%; font-family: Calibri">
                                    <tr>
                                        <td>
                                            <div style="float: left; text-align: center;">
                                                Date:&nbsp&nbsp  <%=DataAcces.Date.ToShortDateString() %>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="float: right; text-align: center;">
                                                <%=DataAcces.universityName %><br />
                                                <%=DataAcces.city %>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12" style="border: 1px dashed; margin-top: 17px;"></div>
                <div style="height: 48%; margin-top: 15px; font-family: Calibri" class="double table-bordered">
                    <div style="width: 100%; height: 99.4%; border: 1px solid black; font-family: Calibri">
                        <div style="padding: 10px; font-family: Calibri">
                            <div class="col-md-12" style="margin-top: 3%;">
                                <div style="font-family: Calibri">
                                    <table style="font-weight: bold; text-align: center">
                                        <tr>
                                            <td style="font-family: Calibri; font-size: 16pt;">
                                                <%=DataAcces.universityName %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1%;">
                                                <img src="<%=DataAcces.LogoUrl%>" style="margin-right: 10px; height: 66px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div style="text-align: center; font-family: Calibri; font-weight: bold; margin-top: 20px;">UNIVERSITY COPY</div>
                            </div>
                            <div class="clearfix" style="margin-bottom: 20px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <table style="width: 90%; font-family: Calibri;">
                                    <tr>
                                        <td style="width: 25%; text-align: right; font-family: Calibri;">
                                            <span style="font-weight: bold; font-family: Calibri;">Application No.:</span>
                                        </td>
                                        <td colspan="2" style="text-align: left; font-family: Calibri;">
                                            <asp:Label ID="_lblAppNo" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2" style="text-align: right; width: 42%; font-family: Calibri;">
                                            <span style="font-weight: bold">Application Date: &nbsp;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="_lblAppDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clearfix" style="margin-bottom: 10px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <div>
                                    <span style="font-weight: bold; margin-left: 100px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;I</span>
                                    &nbsp;<asp:Label ID="_lblStudentName" Font-Underline="true" runat="server"></asp:Label>
                                    &nbsp;<span style="font-weight: bold">&nbsp;&nbsp;&nbsp; &nbsp;Studied in Exam</span>
                                </div>
                                <div class="clearfix" style="margin-bottom: 20px;"></div>
                                <div>
                                    <asp:Label ID="_lblExamNm" Font-Underline="true" runat="server"></asp:Label>
                                </div>
                                <div class="clearfix" style="margin-bottom: 20px; font-family: Calibri;"></div>
                                <div>
                                    <span style="font-weight: bold; font-family: Calibri;">Session  &nbsp;</span>
                                    <asp:Label ID="_lblSessionYear" Font-Underline="true" runat="server"></asp:Label>
                                    &nbsp;<span style="font-weight: bold">With RollNo.</span>
                                    &nbsp;<asp:Label ID="_lblRollNo" Font-Underline="true" runat="server"></asp:Label>
                                    &nbsp;<span style="font-weight: bold">have applied for</span>&nbsp;<asp:Label ID="_lblAppliedfor" Font-Underline="true" runat="server"></asp:Label>.
                                </div>
                                <%--<div class="clearfix" style="margin-bottom: 20px;"></div>
                                    <div>
                                    </div>--%>
                            </div>
                            <div class="clearfix"></div>
                            <div class="clearfix" style="margin-bottom: 70px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <table style="width: 90%;">
                                    <tr>
                                        <td style="width: 25%; text-align: right;">
                                            <span style="font-weight: bold">Please Oblige me.</span>
                                        </td>
                                        <td colspan="2" style="text-align: left;"></td>
                                        <td colspan="2" style="text-align: right; width: 59%;"></td>
                                        <td>
                                            <span style="font-weight: bold">Thanks</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clearfix" style="margin-bottom: 30px;"></div>
                            <div class="col-sm-12" style="text-justify: auto">
                                <table style="width: 100%; font-family: Calibri">
                                    <tr>
                                        <td>
                                            <div style="float: left; text-align: center;">
                                                Date:&nbsp&nbsp  <%=DataAcces.Date.ToShortDateString() %>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="float: right; text-align: center;">
                                                <%=DataAcces.universityName %><br />
                                                <%=DataAcces.city %>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <%-- Dialog --%>
                    </div>
                </div>
            </div>
            <div class="col-sm-12"></div>
            <div></div>
        </asp:Panel>
    </div>
    <div class="container-fluid student-form" style="font-family: Calibri">
        <div class="main-form container">
            <div class="row">
                <div class="col-md-9">
                    <h3 class="text-center col-md-offset-3">Search Student Details</h3>
                </div>
                <div class="col-md-2">
                    <div style="margin-top: 22px;">
                        <div id="noti_Container">
                            <!--SHOW NOTIFICATIONS COUNT.-->
                            <div id="noti_Counter" class="text-center">
                                <asp:Label Text="7" ID="lblCounter" runat="server" />
                            </div>
                            <!--A CIRCLE LIKE BUTTON TO DISPLAY NOTIFICATION DROPDOWN.-->
                            <div id="noti_Button" style="width: 134px; background-color: transparent;" onmouseover="this.style.background='white';" onmouseout="this.style.background='none';">
                                <p style="font-size: 15px;">
                                    <asp:LinkButton ID="lnkpending" runat="server" OnClick="lnkpending_Click">   Pending Applications</asp:LinkButton>
                                    <b />
                                </p>
                            </div>
                            <!--THE NOTIFICAIONS DROPDOWN BOX.-->
                            <div id="notifications"></div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- section1 -->
            <div class="section1">
                <div class="col-md-2 exam-year">
                    <label for="Exam-year">
                        Exam Year <span style="color: red">*</span></label>
                    <div class="dropdown">
                        <asp:DropDownList ID="DDLExamYear" Style="padding: 8px !important; border-radius: 3px !important; border-color: #D3D3D3 !important;" class="form-control exam-year-ddl" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 exam-session">
                    <label for="Exam-session">
                        Exam Session <span style="color: red">*</span></label>
                    <div class="dropdown2">
                        <asp:DropDownList ID="DDLExamSession" Style="padding: 8px !important; border-radius: 3px !important; border-color: #D3D3D3 !important;" class="form-control exam-session-ddl" runat="server">
                            <asp:ListItem>Select Session</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <!-- section1-end -->
            <!-- section2 -->
            <div class="section2">
                <div class="col-md-2 student-rollno">
                    <label for="rollno" class="">Student Roll No.</label>
                    <asp:TextBox ID="txtRollNo" runat="server" MaxLength="8" CssClass="form-control" placeholder="Enter Roll No."></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRollNo" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                </div>
                <div class="col-md-2 student-name">
                    <label for="rollno">Student Name</label>
                    <asp:TextBox ID="TxtStuName" Style="text-transform: uppercase; display: block; width: 100%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 4px;" runat="server" class="form-control exam-name-ddl autosuggest stu-name"
                        placeholder="Enter Student Name" aria-invalid="false" MaxLength="25">                                            
                    </asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TxtStuName"
                        ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtStuName"
                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetCity">
                    </cc1:AutoCompleteExtender>
                </div>
                <div class="col-md-4">
                    <label for="apply-for">Applied For <span style="color: red">*</span></label>
                    <div class="dropdown4">
                        <asp:DropDownList ID="DDLApply" Style="padding: 6px !important; border-radius: 3px !important; border-color: #D3D3D3 !important;"
                            class="apply-for-ddl exam-name-ddl"
                            runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <!-- section2-end -->
            <!-- section3 -->
            <div class="row section3">
                <div class="col-md-12 text-right" style="padding-right: 30px; margin-top: 10px;">
                    <asp:LinkButton ID="BtnSearch" class="search-btn btn btn-default" runat="server" OnClick="BtnSearch_Click" OnClientClick="return ValidateRegister();" Text="Search" />
                    <asp:LinkButton ID="BtnReset" runat="server" Text="Reset" class="search-btn btn btn-default" OnClick="BtnReset_Click" />
                    <asp:Label ID="LblSearchCount" ForeColor="Blue" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-12 reset-btn-section"></div>
            </div>
            <!-- section3-end -->
            <table style="width: 100%;" id="GirdViewTable">
                <tr>
                    <td>
                        <table style="width: 97.5%; margin-left: 7px; background-color: #526a9c; color: white;">
                            <tr>
                                <td style="width: 5%">Select</td>
                                <td style="width: 8%">Roll No.</td>
                                <td style="width: 25%">Student Name</td>
                                <td style="width: 55%">Faculty - Exam Name</td>
                                <td style="width: 6%">Action</td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server" BackColor="White" Width="100%" Style="z-index: 111; min-height: 55px; max-height: 150px; background-color: #f1efef" ScrollBars="Vertical">
                            <asp:GridView align="center" ID="GridStudentDetail" DataKeyNames="RollNo,PartARegNo,PartAImagePath,PartBRegNo,PartBImagePath"
                                runat="server" ShowHeader="false" HeaderStyle-BackColor="#295582" HeaderStyle-Font-Bold="true"
                                HeaderStyle-ForeColor="White" Width="99%" CellPadding="4" RowStyle-BorderStyle="Solid"
                                ForeColor="Black" GridLines="Vertical" BackColor="#f1efef" BorderColor="Gray"
                                BorderStyle="Solid" BorderWidth="2px" OnRowCommand="GridStudentDetail_RowCommand1"
                                ShowFooter="false" AutoGenerateColumns="false">
                                <RowStyle BorderStyle="Solid" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" onclick="toggleSelectionGrid(this);" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RollNo" ItemStyle-Width="8%" />
                                    <asp:BoundField DataField="SName" ItemStyle-Width="25%" />
                                    <asp:TemplateField ItemStyle-Width="55%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExamName" Text='<%#Eval("Faculty_Exam_Name") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandArgument='<%#Eval("FilePath") %>'
                                                CommandName="ShowPopup">View</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartARegNo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartARegNo" Text='<%#Eval("PartARegNo") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartAImagePath" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartAImagePath" Text='<%#Eval("PartAImagePath") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartBRegNo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartBRegNo" Text='<%#Eval("PartBRegNo") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartBImagePath" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartBImagePath" Text='<%#Eval("PartBImagePath") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#50618C" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div>
                <!-- Modal -->
                <div class="modal fade" data-backdrop="static" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalLabel">Application No. :
                                                <asp:Label ID="lblappno1" runat="server" CssClass="text-danger"></asp:Label>
                                    <br />
                                    Proceed For Entry?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnYes" class="btn btn-primary">Yes</button>
                                <button type="button" id="btnNo" class="btn btn-danger" data-dismiss="modal">No</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal -->
                <!-- Button trigger modal -->
                <h3 style="padding-left: 16px;">Payment Details</h3>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row" style="padding-left: 16px; padding-right: 16px;">
                            <div class="col-md-2 student-rollno text-left">
                                <label for="rollno" class="">Payment Mode</label>
                                <asp:DropDownList ID="ddllist" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True">By Cash</asp:ListItem>
                                    <asp:ListItem>By Cheque</asp:ListItem>
                                    <asp:ListItem>By DD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 student-rollno text-left">
                                <label for="rollno" class="">
                                    Receipt No.
                                </label>
                                <asp:TextBox ID="txtRecieptNo" placeholder="Enter Reciept No." MaxLength="8" runat="server" Style="width: 100%;" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRecieptNo" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-2 student-rollno text-left">
                                <label for="rollno" class="">
                                    Receipt Amount (Rs.)
                                </label>
                                <asp:TextBox ID="txtRecieptAmt" placeholder="Enter Receipt Amount" runat="server" Style="width: 150px;" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtRecieptAmt" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-2 student-rollno text-left">
                                Receipt Date
                                <label style="color: red">*</label>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtRecieptDate" placeholder="DD/MM/YYYY" CssClass="form-control datepicker" MaxLength="10" runat="server"></asp:TextBox></td>
                                        <%--<td>
                                            <asp:ImageButton ID="imgPopup" ImageUrl="images/calendar_icon.jpg" Height="30px" ImageAlign="Bottom" runat="server" />
                                        </td>--%>
                                    </tr>
                                    <%--<tr>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-GB" TargetControlID="txtRecieptDate"
                                            Mask="99/99/9999" MaskType="Date" AcceptNegative="None" />
                                        <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            EmptyValueMessage="Please enter Date" InvalidValueMessage="Invalid Date" ControlToValidate="txtRecieptDate" ValidationGroup="DtVal" />
                                        <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtRecieptDate" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </tr>--%>
                                </table>
                            </div>
                            <div class="col-md-4 remark-section">
                                <label for="remark">Remark</label>
                                <asp:TextBox ID="TxtRmark" runat="server" Style="text-transform: uppercase; display: block; width: 100%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 4px;" class="form-control remark" placeholder="Want To Give Some Detail About Application" Width="60%"
                                    aria-invalid="false" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-5 col-md-offset-7 remark-section text-right">
                <asp:Label ID="lblSms" ForeColor="Red" runat="server" Text="" Style="margin-top: 20px;"></asp:Label>
                <asp:Button ID="BtnSave" runat="server" class="search-btn btn btn-default btn-sm" Text="Submit" OnClick="BtnSave_Click1" Style="margin: 10px 0px;" />
            </div>
            <div id="mask"></div>
            <div>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="86%" Height="600px"
                    Style="z-index: 111; background-color: #f1efef; position: absolute; left: 8%; top: 10%; border: outset 2px gray; padding: 5px; display: none">
                    <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                        <tr style="background-color: #50618c">
                            <td style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">
                                <asp:ImageButton ID="btnImgClose" ImageUrl="~/images/cross.gif" Style="float: right; height: 15px;" CssClass="btnClose" OnClick="btnImgClose_Click" runat="server" />
                                <div style="font-size: 12pt" class="btn-group btn-group-sm" role="group">
                                    <button type="button" id="btnfoil" style="font-weight: bold; font-family: Arial; font-size: 12px; width: 120px;"
                                        onclick="foilshow(); this.style.background='#50618c'; this.style.color='white'; document.getElementById('btncfoil').style.background='gray'; 
                                                    document.getElementById('btncfoil').style.color='white'"
                                        class="btn btn-default ">
                                        <span class="text-primary1">FOIL </span>
                                    </button>
                                    <button type="button" id="btncfoil" style="font-weight: bold; font-family: Arial; font-size: 12px; width: 120px;"
                                        onclick="Cfoilshow();this.style.background='#50618c';this.style.color='white'; document.getElementById('btnfoil').style.background='gray'; 
                                                    document.getElementById('btnfoil').style.color='white'"
                                        class="btn btn-default  ">
                                        <span class="text-primary1">COUNTER FOIL </span>
                                    </button>
                                </div>
                                <br />
                                <asp:Label ID="lblYeaeSession" Style="font-weight: 600" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblExamName" Style="font-weight: 600" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 10px; text-align: center;">
                                <div id="imgdiv" style="width: 100%">
                                    <div>
                                        <asp:Image class='zoom' CssClass="" ID="ImageSelected" Width="900" Height="480px" Style="background-size: cover" runat="server" border="5" />
                                        <script src="js/wheelzoom.js"></script>
                                        <script>
                                            wheelzoom(document.querySelector('img.zoom'));
                                        </script>
                                    </div>
                                </div>
                                <div id="DivCF" hidden>
                                    <asp:Image class='zoom22' CssClass="" ID="ImgCF" Width="900" Height="480px" Style="background-size: cover" runat="server" border="5" />
                                    <script src="js/wheelzoom.js"></script>
                                    <script>
                                        wheelzoom(document.querySelector('img.zoom22'));
                                    </script>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <!-- section5-end-->
        </div>
        <!-- jQuery -->
    </div>

    <asp:HiddenField ID="hfRowIndex" runat="server" />

    <style>
        .red {
            background-color: red;
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
    </script>

    <script type="text/javascript">
        $('#myModal').on('shown.bs.modal', function () { $('#myInput').focus() })
    </script>

    <script>
        $('#btnYes').click(function () { window.location.href = "FrmZeroLevel.aspx"; });
    </script>

    <script>
        $('#btnNo').click(function () { window.location.href = "FrmStudentDetail.aspx"; });
    </script>

    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
        var jq = $.noConflict();
        jq(function () {
            jq('#ContentPlaceHolder1_txtRecieptDate').datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>

     <style type="text/css">
        
        #txtRecieptDate
        {
            background-repeat: no-repeat;
            padding-left: 25px;
        }
    </style>--%>

    <%--<script type="text/javascript">
        //// For Valid Date Allow \\\\\\
        function PopOverError(id, plac, msg) {
            try {
                $(id).popover({
                    title: 'Error!',
                    trigger: 'manual',
                    placement: plac,
                    content: function () {
                        var message = msg; //"Allow Numbers Only!";
                        return message;
                    }
                });
                $(id).popover("show");
            } catch (e) { }
        }

        //// For Valid Date Allow \\\\\\
        $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });

        $(".setDate.datepicker").datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true, }).datepicker("setDate", "0");

        $('.datepicker').blur(function (e) {
            try {

                var id = ('#' + this.id);
                var date = $(id).val();
                $(id).popover('destroy');
                var valid = true;
                if (date.length <= 0 || date == '' || date == undefined) {
                    return false;
                }
                if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                    valid = true;
                } else {
                    valid = false;
                }

                if (valid) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Invalid Date.');
                    $(id).focus();
                }
            } catch (e) { }
        });
        $('.datepicker').keypress(function (e) {
            try {
                $(id).popover('destroy');
                var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
                var id = ('#' + this.id);
                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Enter Valid Key For Date.');
                    return false; //Non Numeric Value Return Directly;
                }
                else {
                    if ($(id).val() === undefined) {
                        event.preventDefault();
                        return;
                    }
                    if (e.key == "/") {
                        PopOverError(id, 'top', 'This Key Is Invalid!');
                        event.preventDefault();
                        return false;
                    }
                    if (e.keyCode != 8) {

                        var DateVal = $(id).val();
                        if (e.keyCode == 191) {
                            var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                            PopOverError(id, 'top', 'Enter Valid Date!');
                            $(id).val(corr);
                            event.preventDefault();
                            return false;
                        }

                        if ($(id).val().length == 2) {
                            if ($(id).val() < 1 || $(id).val() > 31) {
                                $(id).val("")
                                PopOverError(id, 'top', 'Enter Valid Day!');
                                event.preventDefault();
                                return false;
                            }
                            $(id).val($(id).val() + "/");
                        } else if ($(id).val().length == 5) {
                            var month = $(id).val().substring(3, 6);
                            if (month < 1 || month > 12) {
                                var corr = $(id).val().replace("/" + month, "");
                                $(id).val(corr);
                                PopOverError(id, 'top', 'Enter Valid Month!');
                                event.preventDefault();
                                return false;
                            }
                            $(id).val($(id).val() + "/");
                        } else if ($(id).val().length == 10) {
                            var Inputyear = $(id).val().substring(6, 11);
                            var NowYear = new Date().getUTCFullYear();
                            if (Inputyear < 1900 || Inputyear > NowYear) {
                                var corr = $(id).val().replace(Inputyear, "");
                                $(id).val(corr);
                                PopOverError(id, 'top', 'Enter Valid Year!');
                                event.preventDefault();
                                return false;
                            }
                        }
                        else { $(id).popover('destroy'); }
                    }
                }
            } catch (e) { }
        });

        //// Disable Pasting IN Text Box \\\\
        $('input.datepicker').bind('copy paste', function (e) {
            e.preventDefault();
        });

        $("input").attr("autocomplete", "off");
        $(".datepicker").attr("autocomplete", "off");
    </script>--%>

    <%--    <script src="js1/jquery.js"></script>
    <script src="js1/jquery-1.11.3.min.js"></script>--%>
</asp:Content>
