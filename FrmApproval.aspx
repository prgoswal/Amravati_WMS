<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmApproval.aspx.cs" Inherits="FrmSecondLevel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <script src="js/jquery-1.3.2.js"></script>
    <script type="text/javascript" lang="javascript">
        <%--function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }--%>
        $("#btnCls").live('click', function () { HidePopup(); });
    </script>

    <script type="text/javascript" lang="javascript">
        function ShowRemarkPopup() {
            $('#Remarkmask').show();
            $('#<%=pnlRemarkpopup.ClientID %>').show();
        }
        function HideRemarkPopup() {
            $('#Remarkmask').hide();
            $('#<%=pnlRemarkpopup.ClientID %>').hide();
        }
        $("#btnClsRemark").live('click', function () { HideRemarkPopup(); });
    </script>
    <link rel="stylesheet" href="css/dg-picture-zoom.css" />
    <style type="text/css">
        body {
            font-family: Trebuchet MS, Arial;
            width: 100%;
        }

        #content {
            width: 760px;
            margin: 0 auto;
        }
    </style>

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
    <script src="bootstrap-3.3.6-dist/js/wheelzoom.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ValidateRegister() {
            if ($('#<%=txtRollNo.ClientID%>').val() == '') {
                $('#<%=txtRollNo.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Roll No.', 'Amravati University(WMS)', function () { $('#<%=txtRollNo.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtRollNo.ClientID%>').css('border-color', ''); }

            if ($('#<%=txtStudentName.ClientID%>').val() == '') {
                $('#<%=txtStudentName.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Student Name', 'Amravati University(WMS)', function () { $('#<%=txtStudentName.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtStudentName.ClientID%>').css('border-color', ''); }

            if ($('#<%=txtExamName.ClientID%>').val() == '') {
                $('#<%=txtExamName.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Exam Name', 'Amravati University(WMS)', function () { $('#<%=txtExamName.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtExamName.ClientID%>').css('border-color', ''); }

            if ($('#<%=txtRemark.ClientID%>').val() == '') {
                $('#<%=txtRemark.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Reject Remarks', 'Amravati University(WMS)', function () { $('#<%=txtRemark.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtRemark.ClientID%>').css('border-color', ''); }
        };
    </script>
    <h3 class="header-title"></h3>
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
                        <td style="width: 30%">
                            <div style="text-align: left; margin-left: 20%;">
                                <span>Pending Since :</span>
                                &nbsp;<asp:Label runat="server" ID="lblPendingsince" ForeColor="Red" Font-Bold="true"></asp:Label>
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
                        <td style="width: 8%;">Application ID</td>
                        <td style="width: 12%;">Application Date</td>
                        <td style="width: 10%;">Roll No.</td>
                        <td style="width: 20%;">Student Name</td>
                        <td style="width: 35%;">Applied For</td>
                        <td style="width: 10%;">View Image</td>
                    </tr>
                </table>

                <div class="text-center" style="overflow: auto; height: 140px; width: 100%">
                    <asp:GridView align="center" ID="grdPandingDetails" runat="server" DataKeyNames="RollNo" ShowHeader="false" over HeaderStyle-BackColor="#50618c"
                        HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="99%" CellPadding="4"
                        RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false" AutoGenerateColumns="false" HorizontalAlign="Center" OnRowCommand="grdPandingDetails_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApplicationID" ControlStyle-Height="100" ItemStyle-Width="8%" />
                            <asp:BoundField DataField="ApplicationDate" ControlStyle-Height="100" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="RollNo" ControlStyle-Height="100" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="StudentName" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="AppliedFor" ItemStyle-Width="35%" />
                            <asp:TemplateField ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="ShowPopup">View</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="box">
        <div class="row">
            <div class="col-md-12 col-lg-12 text-center">
                <h4>
                    <asp:Label runat="server" ID="Label1" Style="font-weight: 600;" Text="Completed Application(s)"></asp:Label>
                </h4>
                <div style="text-align: right; margin-right: 50px;">
                    <span>Total No. Of Application(s)&nbsp&nbsp</span>
                    <asp:Label ID="lblcompletecount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="col-md-12 col-lg-12 text-center">
                <table style="width: 99%; margin-left: 7px; background-color: #526a9c; color: white;">
                    <tr>
                        <td style="width: 5%; background-color: #526a9c; color: white;">Sr.No.</td>
                        <td style="width: 8%;">Application ID</td>
                        <td style="width: 12%;">Application Date</td>
                        <td style="width: 10%;">Roll No.</td>
                        <td style="width: 20%;">Student Name</td>
                        <td style="width: 25%">Applied For</td>
                        <td style="width: 10%">Action Taken</td>
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
                            <asp:BoundField DataField="RollNo" ControlStyle-Height="100" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="StudentName" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="AppliedFor" ItemStyle-Width="25%" />
                            <asp:BoundField DataField="AppStatus" ItemStyle-Width="10%" />

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
    <div></div>
    <div id="mask"></div>
    <div id="Remarkmask"></div>
    <div>
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
                                                    <table class="table table-bordered" style="margin-left: 1%; height: 100px">
                                                        <tr>
                                                            <th>Exam Name
                                                            </th>
                                                            <td>
                                                                <asp:TextBox ID="txtExamName" CssClass="form-control input-sm" TabIndex="3" Style="text-transform: uppercase" MaxLength="100"
                                                                    runat="server" placeholder="Enter Exam Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>Branch Name
                                                            </th>
                                                            <td>
                                                                <asp:TextBox ID="txtBranchName" CssClass="form-control input-sm" TabIndex="4" Style="text-transform: uppercase" MaxLength="50"
                                                                    runat="server" placeholder="Enter Branch Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>Exam Session</th>
                                                            <td>
                                                                <asp:TextBox ID="txtExamSession" runat="server" placeholder="Enter Exam Session" CssClass="form-control input-sm" TabIndex="5" Style="text-transform: uppercase" MaxLength="10"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>Exam Year</th>
                                                            <td>
                                                                <asp:TextBox ID="txtExamYear" runat="server" placeholder="Enter Exam Year" CssClass="form-control input-sm" TabIndex="6" Style="text-transform: uppercase" MaxLength="4"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>Roll No.
                                                            </th>
                                                            <td>
                                                                <asp:TextBox ID="txtRollNo" CssClass="form-control input-sm" TabIndex="1" Enabled="false"
                                                                    runat="server" MaxLength="8" placeholder=""></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>Name
                                                            </th>
                                                            <td>
                                                                <asp:TextBox ID="txtStudentName" CssClass="form-control input-sm" Style="text-transform: uppercase"
                                                                    runat="server" TabIndex="2" MaxLength="40" placeholder="Enter Student Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDivision" runat="server" visible="false">
                                                            <th>Division</th>
                                                            <td>
                                                                <asp:TextBox ID="txtDivision" CssClass="form-control input-sm" TabIndex="11" MaxLength="6"
                                                                    runat="server" placeholder="Enter Division"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trCgpa" runat="server" visible="false">
                                                            <th>CGPA</th>
                                                            <td>
                                                                <asp:TextBox ID="txtCGPA" CssClass="form-control input-sm" TabIndex="10" MaxLength="6"
                                                                    runat="server" placeholder="Enter CGPA No."></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trEnroll" runat="server" visible="false">
                                                            <th>Enrollment No.</th>
                                                            <td>
                                                                <asp:TextBox ID="txtEnrollmentlNo" CssClass="form-control input-sm" TabIndex="8" Style="text-transform: uppercase" MaxLength="10"
                                                                    runat="server" placeholder="Enter Enrollment No."></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trCollege" runat="server" visible="false">
                                                            <th>College</th>
                                                            <td>
                                                                <asp:TextBox ID="txtCollege" CssClass="form-control input-sm" TabIndex="9" Style="text-transform: uppercase" MaxLength="7"
                                                                    runat="server" placeholder="Enter College No."></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trSubjectName" runat="server" visible="false">
                                                            <th>Subject Name</th>
                                                            <td>
                                                                <asp:TextBox ID="txtSubjectName" CssClass="form-control input-sm" Height="65px" TextMode="MultiLine"
                                                                    runat="server" placeholder="Enter Subject Name"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDistinctionSub" runat="server" visible="false">
                                                            <th>Distinction Subject</th>
                                                            <td>
                                                                <asp:TextBox ID="txtDistinctionSub" CssClass="form-control input-sm" Style="text-transform: uppercase" Height="65px" TextMode="MultiLine"
                                                                    runat="server" placeholder="Enter Distinction Subject"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trMeritNo" runat="server" visible="false">
                                                            <th>Merit No.</th>
                                                            <td>
                                                                <asp:TextBox ID="txtMeritNo" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="3"
                                                                    runat="server" placeholder="Enter Merit No."></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trLaterReferenceNo" runat="server" visible="false">
                                                            <th>Letter Reference No.</th>
                                                            <td>
                                                                <asp:TextBox ID="txtLaterReferenceNo" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="4"
                                                                    runat="server" placeholder="Enter Letter Reference No."></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trLaterReferenceDate" runat="server" visible="false">
                                                            <th>Letter Reference Date</th>
                                                            <td>
                                                                <asp:TextBox ID="txtLaterReferenceDate" TextMode="Date" CssClass="form-control input-sm" Style="text-transform: uppercase"
                                                                    runat="server" placeholder="Enter Later Reference Date"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trPassingYear" runat="server" visible="false">
                                                            <th>Passing Year</th>
                                                            <td>
                                                                <asp:TextBox ID="txtPassingYear" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="4"
                                                                    runat="server" placeholder="Enter Passing Year"></asp:TextBox>


                                                            </td>
                                                        </tr>
                                                        <tr id="trExamMedium" runat="server" visible="false">
                                                            <th>Exam Medium</th>
                                                            <td>
                                                                <asp:TextBox ID="txtExamMedium" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="10"
                                                                    runat="server" placeholder="Enter Exam Medium"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trResulDeclarationDate" runat="server" visible="false">
                                                            <th>Result Declaration Date</th>
                                                            <td>
                                                                <asp:TextBox TabIndex="15" ID="txtDateResultDeclaration" TextMode="Date" CssClass="form-control input-sm"
                                                                    required="" data-validation-required-message="Result Declaration Date Required"
                                                                    runat="server" placeholder="Enter Result Declaration Date"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="trRank" runat="server">
                                                            <th>Rank</th>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRank" runat="server" CssClass="form-control">
                                                                    <asp:ListItem>--- Select ---</asp:ListItem>
                                                                    <asp:ListItem>Highest/Ist</asp:ListItem>
                                                                    <asp:ListItem>IInd</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr id="trAwardedBy" runat="server">
                                                            <th>Awarded By</th>
                                                            <td>
                                                                <asp:DropDownList ID="ddlAwardedby" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                    <asp:ListItem Text="---- Select ----"></asp:ListItem>
                                                                    <asp:ListItem Text="Gold Medal" />
                                                                    <asp:ListItem Text="Silver Medal" />
                                                                    <asp:ListItem Text="Cash Prize" />
                                                                </asp:DropDownList>
                                                            </td>

                                                        </tr>
                                                        <tr id="TrawardPrice" runat="server">
                                                            <th>Award Prize</th>
                                                            <td>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAwardPrize" Required="" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Button ID="lnkSubmit" class="btn btn-primary" runat="server" TabIndex="13" Text="SUBMIT" OnClientClick="return ValidateRegister();" OnClick="lnkSubmit_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:Label runat="server" ID="lblmsg" Text="" Style="color: red; font-weight: 600; text-align: center"></asp:Label>
                                        
                                        <table style="width: 100%; height: 100px; padding-top: 0px;" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="LinkButtonRemark" runat="server" CssClass="text-info bg-info" Style="margin-left: 23%" Font-Bold="true" OnClientClick="ShowRemarkPopup(); return false;">Approval&nbsp;Remark's</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Remark</th>
                                                <td>
                                                    <asp:TextBox ID="txtRemark" CssClass="form-control input-sm" TabIndex="12" Style="text-transform: uppercase" MaxLength="200" Width="100%"
                                                        runat="server" placeholder="Enter Remark"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnApprove" class="btn btn-primary" TabIndex="14" runat="server" Text="APPROVE" OnClick="btnApprove_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnReject" class="btn btn-primary" runat="server" TabIndex="15" Text="REJECT" OnClientClick="return ValidateRegister();" OnClick="btnReject_Click" CommandName="ShowPopup" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCorrection" class="btn btn-primary" runat="server" TabIndex="15" Text="CORRECTION" OnClick="btnCorrection_Click" />
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
        <%--<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="98%" Height="620px"
            Style="z-index: 111; background-color: #f1efef; position: absolute; left: 1%; top: 2%; border: outset 2px gray; padding: 5px; display: none">
            <asp:Panel runat="server" ID="pnlEntry">
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
                            <asp:Label ID="lblEntry" Style="margin-right: 15px; font-size: 15px;" runat="server" Text=""></asp:Label>
                            <br />
                            Application No. 
                            <asp:Label ID="lblappno" ForeColor="floralwhite" runat="server"></asp:Label>
                            <a id="btnCls" style="color: white; float: right; text-decoration: none;" class="btnClose" href="javascript:HidePopup()">
                                <img src="images/cancel-512.png" style="margin-top: -54px;" height="20px" width="20px" />
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td id="td1" colspan="2" style="border: 2px solid Red; padding-top: 10px; text-align: center;">
                            <div id="imgdiv">
                                <asp:Image ID="ImageFoil" alt="" class="zoom marksheet" Style="top: 0px; width: 450px; max-width: 800px !important; align-content: center; height: 462px; position: inherit;" runat="server" />
                                <script src="js/wheelzoom.js"></script>
                                <script>
                                    wheelzoom(document.querySelector('img.zoom'));
                                </script>
                            </div>
                        </td>
                        <td id="td2" colspan="2" style="border: 2px solid Red; padding-top: 10px; text-align: center;">
                            <div id="Div1">
                                <asp:Image ID="ImageCF" class='zoom1 marksheet' alt="" Style="top: 0px; align-content: center; max-width: 800px !important; height: 462px; position: inherit" runat="server" />
                                <script src="wheelzoom.js"></script>
                                <script>
                                    wheelzoom(document.querySelector('img.zoom1'));
                                </script>
                            </div>
                        </td>
                        <td style="border: 2px solid; padding-top: 10px; padding-right: 1%; text-align: center; width: 38%">
                            <asp:Panel ID="pnlform" runat="server" Visible="false">
                                <table class="table table-bordered" style="margin-left: 1%; height: 100px">
                                    <tr>
                                        <th>Exam Name
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtExamName" CssClass="form-control input-sm" TabIndex="3" Style="text-transform: uppercase" MaxLength="100"
                                                runat="server" placeholder="Enter Exam Name"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Branch Name
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtBranchName" CssClass="form-control input-sm" TabIndex="4" Style="text-transform: uppercase" MaxLength="50"
                                                runat="server" placeholder="Enter Branch Name"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Exam Session</th>
                                        <td>
                                            <asp:TextBox ID="txtExamSession" runat="server" placeholder="Enter Exam Session" CssClass="form-control input-sm" TabIndex="5" Style="text-transform: uppercase" MaxLength="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Exam Year</th>
                                        <td>
                                            <asp:TextBox ID="txtExamYear" runat="server" placeholder="Enter Exam Year" CssClass="form-control input-sm" TabIndex="6" Style="text-transform: uppercase" MaxLength="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Roll No.
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtRollNo" CssClass="form-control input-sm" TabIndex="1" Enabled="false"
                                                runat="server" MaxLength="8" placeholder=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Name
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtStudentName" CssClass="form-control input-sm" Style="text-transform: uppercase"
                                                runat="server" TabIndex="2" MaxLength="40" placeholder="Enter Student Name"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trDivision" runat="server" visible="false">
                                        <th>Division</th>
                                        <td>
                                            <asp:TextBox ID="txtDivision" CssClass="form-control input-sm" TabIndex="11" MaxLength="6"
                                                runat="server" placeholder="Enter Division"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trCgpa" runat="server" visible="false">
                                        <th>CGPA</th>
                                        <td>
                                            <asp:TextBox ID="txtCGPA" CssClass="form-control input-sm" TabIndex="10" MaxLength="6"
                                                runat="server" placeholder="Enter CGPA No."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trEnroll" runat="server" visible="false">
                                        <th>Enrollment No.</th>
                                        <td>
                                            <asp:TextBox ID="txtEnrollmentlNo" CssClass="form-control input-sm" TabIndex="8" Style="text-transform: uppercase" MaxLength="10"
                                                runat="server" placeholder="Enter Enrollment No."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trCollege" runat="server" visible="false">
                                        <th>College</th>
                                        <td>
                                            <asp:TextBox ID="txtCollege" CssClass="form-control input-sm" TabIndex="9" Style="text-transform: uppercase" MaxLength="7"
                                                runat="server" placeholder="Enter College No."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trSubjectName" runat="server" visible="false">
                                        <th>Subject Name</th>
                                        <td>
                                            <asp:TextBox ID="txtSubjectName" CssClass="form-control input-sm" Height="65px" TextMode="MultiLine"
                                                runat="server" placeholder="Enter Subject Name"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trDistinctionSub" runat="server" visible="false">
                                        <th>Distinction Subject</th>
                                        <td>
                                            <asp:TextBox ID="txtDistinctionSub" CssClass="form-control input-sm" Style="text-transform: uppercase" Height="65px" TextMode="MultiLine"
                                                runat="server" placeholder="Enter Distinction Subject"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trMeritNo" runat="server" visible="false">
                                        <th>Merit No.</th>
                                        <td>
                                            <asp:TextBox ID="txtMeritNo" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="3"
                                                runat="server" placeholder="Enter Merit No."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trLaterReferenceNo" runat="server" visible="false">
                                        <th>Letter Reference No.</th>
                                        <td>
                                            <asp:TextBox ID="txtLaterReferenceNo" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="4"
                                                runat="server" placeholder="Enter Letter Reference No."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trLaterReferenceDate" runat="server" visible="false">
                                        <th>Letter Reference Date</th>
                                        <td>
                                            <asp:TextBox ID="txtLaterReferenceDate" TextMode="Date" CssClass="form-control input-sm" Style="text-transform: uppercase"
                                                runat="server" placeholder="Enter Later Reference Date"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPassingYear" runat="server" visible="false">
                                        <th>Passing Year</th>
                                        <td>
                                            <asp:TextBox ID="txtPassingYear" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="4"
                                                runat="server" placeholder="Enter Passing Year"></asp:TextBox>


                                        </td>
                                    </tr>
                                    <tr id="trExamMedium" runat="server" visible="false">
                                        <th>Exam Medium</th>
                                        <td>
                                            <asp:TextBox ID="txtExamMedium" CssClass="form-control input-sm" Style="text-transform: uppercase" MaxLength="10"
                                                runat="server" placeholder="Enter Exam Medium"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trResulDeclarationDate" runat="server" visible="false">
                                        <th>Result Declaration Date</th>
                                        <td>
                                            <asp:TextBox TabIndex="15" ID="txtDateResultDeclaration" TextMode="Date" CssClass="form-control input-sm"
                                                required="" data-validation-required-message="Result Declaration Date Required"
                                                runat="server" placeholder="Enter Result Declaration Date"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trRank" runat="server">
                                        <th>Rank</th>
                                        <td>
                                            <asp:DropDownList ID="ddlRank" runat="server" CssClass="form-control">
                                                <asp:ListItem>--- Select ---</asp:ListItem>
                                                <asp:ListItem>Highest/Ist</asp:ListItem>
                                                <asp:ListItem>IInd</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trAwardedBy" runat="server">
                                        <th>Awarded By</th>
                                        <td>
                                            <asp:DropDownList ID="ddlAwardedby" runat="server" CssClass="form-control" AutoPostBack="true">
                                                <asp:ListItem Text="---- Select ----"></asp:ListItem>
                                                <asp:ListItem Text="Gold Medal" />
                                                <asp:ListItem Text="Silver Medal" />
                                                <asp:ListItem Text="Cash Prize" />
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr id="TrawardPrice" runat="server">
                                        <th>Award Prize</th>
                                        <td>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAwardPrize" Required="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="linkSubmit" class="btn btn-primary" runat="server" TabIndex="13" Text="SUBMIT" OnClientClick="return ValidateRegister();" OnClick="linkSubmit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Label runat="server" ID="lblmsg" Text="" Style="color: red; font-weight: 600; text-align: center"></asp:Label>
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonRemark" runat="server" CssClass="text-info bg-info" Style="margin-left: 23%" Font-Bold="true" OnClientClick="ShowRemarkPopup(); return false;">Approval Remark's</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; height: 100px; padding-top: 0px;" cellpadding="0">
                                <tr>
                                    <th>Remark</th>
                                    <td>
                                        <asp:TextBox ID="txtRemark" CssClass="form-control input-sm" TabIndex="12" Style="text-transform: uppercase" MaxLength="200" Width="100%"
                                            runat="server" placeholder="Enter Remark"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnApprove" class="btn btn-primary" TabIndex="14" runat="server" Text="APPROVE" OnClick="btnApprove_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReject" class="btn btn-primary" runat="server" TabIndex="15" Text="REJECT" OnClientClick="return ValidateRegister();" OnClick="btnReject_Click" CommandName="ShowPopup" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCorrection" class="btn btn-primary" runat="server" TabIndex="15" Text="CORRECTION" OnClick="btnCorrection_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div class="container">
                <div class="form-inline">
                    <div class="" style="padding: 10px;">
                        <div class="col-lg-5 col-md-5 col-md-offset-1 col-lg-offset-1">
                            <div class=""></div>
                        </div>

                        <div class="form-group">
                            <div class="col-lg-4 col-md-4">
                                <div class=""></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:Panel ID="imgPanel" runat="server" BackColor="White" Width="61.5%" Height="517px"
                    Style="z-index: 111; background-color: #f1efef; position: absolute; left: 5.1px; top: 9.7%; padding: 5px; display: none">
                    <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                        <tr>
                            <td style="padding-top: 10px; text-align: center;">
                                <div id="divimg" style="width: 100%">
                                    <asp:Image class='zoom2' ID="HideFoil" Width="800px" Height="500px" runat="server" border="2" />
                                    <script src="js/wheelzoom.js"></script>
                                    <script>
                                        wheelzoom(document.querySelector('img.zoom2'));
                                    </script>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:Panel ID="imgpanel2" runat="server" BackColor="White" Width="61.5%" Height="517px"
                    Style="z-index: 111; background-color: #f1efef; position: absolute; left: 5.1px; top: 9.7%; padding: 5px; display: none">
                    <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                        <tr>
                            <td style="padding-top: 10px; text-align: center;">
                                <div id="divimg" style="width: 100%">
                                    <asp:Image class='zoom3' ID="HideCF" Width="800px" Height="500px" runat="server" border="2" />
                                    <script src="js/wheelzoom.js"></script>
                                    <script>
                                        wheelzoom(document.querySelector('img.zoom3'));
                                    </script>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div></div>
        </asp:Panel>--%>
    </div>
    <div>
        <asp:Panel ID="pnlRemarkpopup" runat="server"
             style="height: 100%; width: 100%; float: left; position: fixed; background: rgba(0,0,0,0.5); top: 0; z-index: 99999; left: 0;display:none">
          <div   Style="height: 400px; width: 60%; z-index: 99999; float: left; background-color: rgb(241, 239, 239); position: absolute; left: 50%; top: 50%; border: 2px outset gray; padding: 5px; transform: translate(-50%,-50%);">
             <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-12 text-center" style="font-weight: bold; font-family: Calibri; font-size: 15pt;padding:15px">
                        Approval Remarks
                        <a id="btnClsRemark" style="color: white; float: right; text-decoration: none;" class="btnClose" href="javascript:HideRemarkPopup()">
                                <img src="images/cancel-512.png" style="margin-top: -7px; margin-right: 0px;" height="20px" width="20px" />
                            </a>
                        
                    </div>
                   
                            
                </div>

            </div>
            <div id="tblRemark" runat="server">
            </div>
              </div>
        </asp:Panel>
    </div>

    <script type="text/javascript">
        function ShowPopupPrint() { $('#<%=imgPanel.ClientID %>').show(); }
        function HidePopupPrint() { $('#<%=imgPanel.ClientID %>').hide(); }
        $(".btnClose11").live('Click', function () { HidePopupPrint(); });
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

    </script>
</asp:Content>

