<%@ Page Title="" MaintainScrollPositionOnPostback="true" Async="true" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmPrint_OLD.aspx.cs" Inherits="FrmPrint_OLD" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .tablecell {
            border: solid;
            border-style: solid;
            border-width: 3px;
            /*line-height: 5;*/
            height: 35px;
            text-align: left;
        }

        .tableDetail {
            border: solid;
            border-style: solid;
            border-width: 2px;
            border-color: #224380;
            /*line-height: 5;*/
            height: 30px;
            text-align: center;
            padding-left: 0px;
        }

        body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modal {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 1000;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }
        /* THE POPULAR RED NOTIFICATIONS COUNTER. */
        #noti_Counter {
            display: block;
            position: absolute;
            background: #E1141E;
            color: #FFF;
            font-size: 10pt;
            font-weight: normal;
            /*margin: 10px 0 0 9px;*/
            border-radius: 2px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            text-align: center;
            z-index: 1;
            margin-top: -21px !important;
            left: 25%;
        }
    </style>

    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            img.onload = function () {
            };

            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
        }

        function PrintDiv() {

            if (confirm("Are you Sure You Want To Print.") == false) {
                return false;
            } else {
                var divContents = document.getElementById("divprint").innerHTML;
                var printWindow = window.open('', '', 'height=400,width=800');
                printWindow.document.write('<html><head><title></title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(divContents);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.print();

            }
        }
        $(document).ready(function () {
            window.onafterprint = function () {
                alert("Printing completed...");
            }
        });

        $(".btnClose").live('click', function () { HidePopup(); });

        function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-offset-0 col-md-offset-0 "></div>
        <div class="col-lg-12 col-sm-offset-0">
            <div class="user-creation">
                <h3 class="text-center">Pending For Printing Document </h3>
                <hr />
                <asp:UpdatePanel ID="upd" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td align="center" style="width: 27%; vertical-align: top">
                                    <h4 style="margin-top: -2.5%">DOCUMENT TYPE</h4>
                                    <table style="width: 100%; margin-top: 1%; border: solid; border-style: solid; border-width: 2px;">
                                        <tr>
                                            <td id="A1" runat="server" class="tablecell" style="">
                                                <asp:GridView align="center" ID="GridDocumentType" runat="server" DataKeyNames="DocID" ShowHeader="false" HeaderStyle-BackColor="#50618c"
                                                    HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="100%" CellPadding="4"
                                                    RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                                                    BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false" OnRowCommand="GridDocumentType_RowCommand" AutoGenerateColumns="false" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Height="30px">

                                                            <ItemTemplate>
                                                                <div style="display: inline; padding-left: 5px">
                                                                    <asp:LinkButton ID="lnkpending" runat="server" Text='<%#Eval("DocType") %>' PostBackUrl='<%#"~/FrmPrint_OLD.aspx?docid="+Eval("DocID") +"&Cnt="+ Eval("Cnt") +"&DocType="+ Eval("DocType")%>' OnClick="lnkpending_Click"></asp:LinkButton>
                                                                    <span id="noti_Counter" style="margin: -2px 0px 0px 9px; width: 22px; border-radius: 12px; opacity: 1;">
                                                                        <asp:Label ID="lblCnt" runat="server" Text='<%#Eval("Cnt") %>'></asp:Label>
                                                                    </span>
                                                                </div>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center" style="width: 73%; vertical-align: top; padding-left: 10px">
                                    <h4 style="margin-top: -2.5%; padding-top: 1.5%">
                                        <asp:Label runat="server" ID="lblForData" Text=""></asp:Label></h4>
                                    <asp:Panel ID="PanelclickDetail" runat="server">
                                        <table style="width: 100%; border: solid; border-style: solid; border-width: 2px;">
                                            <tr style="background-color: #224380; color: white; font-size: 10pt">
                                                <td class="tableDetail" style="width: 3%">Sr.&nbsp;No.</td>
                                                <td class="tableDetail" style="width: 7%">Application&nbsp;No.</td>
                                                <td class="tableDetail" style="width: 5%">Roll&nbsp;No.</td>
                                                <td class="tableDetail" style="width: 15%">Student&nbsp;Name.</td>
                                                <td class="tableDetail" style="width: 13%">Application&nbsp;Date</td>
                                                <td class="tableDetail" style="width: 14%">Last&nbsp;Approval&nbsp;Date</td>
                                                <td class="tableDetail" style="width: 5%">Print</td>
                                                <td class="tableDetail" style="width: 11%">Complete&nbsp;Print</td>
                                            </tr>
                                            <tr>
                                                <td colspan="9" style="width: 100%">
                                                    <div class="text-center" style="overflow: auto; max-height: 305px; width: 100%">
                                                        <asp:GridView align="center" ID="grdPandingDetails" runat="server" DataKeyNames="ApplicationID" ShowHeader="false" HeaderStyle-BackColor="#50618c"
                                                            HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="100%" CellPadding="4"
                                                            RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                                                            BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false" OnRowCommand="grdPandingDetails_RowCommand" AutoGenerateColumns="false" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrownumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ApplicationID" ControlStyle-Height="100" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="RollNo" ControlStyle-Height="100" ItemStyle-Width="5%" />
                                                                <asp:BoundField DataField="StudentName" ControlStyle-Height="100" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="EntryDate" ControlStyle-Height="100" ItemStyle-Width="13%" />
                                                                <asp:BoundField DataField="LastApprovalDate" ItemStyle-Width="14%" />
                                                                <asp:TemplateField ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPrint" runat="server" Text="Print" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="ShowPopup">Print</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="11%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPrintFinally" runat="server" Text="Complete Print" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="FinalPrint">Complete Print</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="modal fade in" runat="server" id="printConfirmationModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                                                            <div class="modal-dialog modal-sm vertical-align-center" style="width: 40%;" role="document">
                                                                <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                                                                    <asp:Panel ID="pnlPrintConfirmation" runat="server" Width="100%" Height="100%" Visible="true">
                                                                        <div class="modal-header c" style="background-color: #4D6188; color: #545454;">
                                                                            <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                                                                                <tr style="background-color: #95969c">
                                                                                    <td colspan="4" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px; text-align: center" align="left">
                                                                                        <asp:Label ID="lblPrintConfirmation" Text="Print Confirmation" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px" align="center">
                                                                                        <%--<asp:LinkButton ID="lnkCancelAppListClose" Style="color: white; float: right; text-decoration: none;" OnClick="lnkCancelAppListClose_Click"
                                                                                                runat="server"><img src="images/cancel-512.png" alt="Alternate Text" style="margin-top: -12px; margin-right: -3px;" 
                                                                                                    height="20px" width="20px" /></asp:LinkButton>--%>
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
                                                                            <table class="table-bordered" style="border: none; width: 100%">
                                                                                <tr>
                                                                                    <td colspan="6" style="border: none; text-align: left; font-size: 18px;">
                                                                                        <asp:Label ID="lblPrintConfirmationMSG" Style="margin-left: 10px" Text="Are you Sure? You Want To Print." runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <div class="modal-footer">
                                                                        <asp:Button ID="btnYes" Text="Yes" CssClass="btn btn-primary" OnClick="btnYes_Click" runat="server" />
                                                                        <asp:Button ID="btnNo" Text="No" CssClass="btn btn-danger" OnClick="btnNo_Click" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSuccessMSG" Style="float: right; margin-right: 5px;" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <h4>
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </h4>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="divBackground" style="top: -138px;" class="modal">
                </div>
                <div id="divImage" align="center">
                    <div id="divprint" style="height: 450px; vertical-align: top; padding-top: 20px">
                        <table style="width: 90%">
                            <tr>
                                <td valign="middle" align="center">
                                    <table class="table table-bordered" style="width: 100%">
                                        <tr>

                                            <td>Application No. </td>
                                            <td>
                                                <asp:Label ID="LblApplicationNo" runat="server" Style="font-weight: 600"></asp:Label>
                                            </td>
                                            <tr>
                                                <td>Roll No. 
                                          
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="LblRoll" Style="font-weight: 600"></asp:Label>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td>Student Name. 
                                          
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="LblStuName" Style="font-weight: 600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Date Application. 
                                          
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="LblDateApplication" Style="font-weight: 600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Last Date Application. </td>
                                            <td>
                                                <asp:Label ID="LblLastDate" runat="server" Style="font-weight: 600"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Button ID="Btnprint" OnClientClick=" return PrintDiv()" CssClass="btn btn-success" Visible="false" runat="server" Text="Print" />
                    <input id="btnClose" type="button" class="btn btn-danger" value="close" onclick="HideDiv()" />
                </div>
                <div id="mask"></div>
                <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="50%" Height="400px"
                    Style="z-index: 111; background-color: #f1efef; position: absolute; left: 25%; top: 10%; border: outset 2px gray; padding: 5px; display: none">
                    <asp:Panel ID="pnlform" runat="server">
                        <div id="dvContents">
                            <a id="closebtn" style="color: white; float: right; text-decoration: none"
                                class="btnClose" href="#">
                                <img src="images/cross.gif" height="15px" width="15px" border="5" />
                            </a>
                        </div>
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" onclick="return PrintDiv();">Print</button>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="divCertificate" style="display: none;">
        <div class="modal-fade">
            <div class="outer-Content">
                <div class="col-md-12" style="padding-right: initial;">
                    <asp:Button ID="btnPrintCertificate" Text="Print" CssClass="btn btn-primary" OnClientClick="PrintDiv()" runat="server" />
                    <div style="float: right;">
                        <a id="btnClsRemark" class="btnClose" href="javascript:HideRemarkPopup()">
                            <img src="images/cancel-512.png" style="margin-top: -7px;" height="20px" width="20px" />
                        </a>
                    </div>
                </div>
                <div id="PrintDiv" style="padding: 7% 2% 2%">
                    <div class="padd">
                        <div class="main-Content table-bordered">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="w100" style="border: 1px solid black; display: table; height: 100%;">
                                        <div style="padding: 10px;">

                                            <div id="Header" onload="javascript:CreateSrNo()">
                                                <div class="floatright w100">
                                                    <div class="floatright capitalize">
                                                        Sr No. :
                                                        <abbr>
                                                            <span id="SrNo"></span>
                                                            <asp:Label ID="lblNo" Style="display: none;" runat="server" />
                                                        </abbr>
                                                    </div>
                                                </div>
                                                <br />
                                                <div>
                                                    <span class="UnivName"><%=DataAcces.universityName %></span>
                                                    <br />
                                                    <asp:Label CssClass="Certifiacate" ID="lblCertifiacate" runat="server"></asp:Label><br />
                                                    <br />
                                                    <img src="<%=DataAcces.LogoUrl%>" alt="AMRAWATI" />
                                                </div>
                                            </div>

                                            <div id="BodyContent" runat="server" class="BodyContent">
                                                <div class="bodyStyle">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div id="Provisional" runat="server" class="mt-medium">
                                                                <div>
                                                                    <p>This is to certify that</p>
                                                                    <br />
                                                                    <p>
                                                                        <p>
                                                                            <span>Shri/Smt./Ku. : </span><span class="SpecialStudent text-lowercase"><%=pl.SName %></span><br />
                                                                        </p>
                                                                        <span>has passed the  Examination  and  has  become  entitled  to  the  said  degree  as  follows</span>
                                                                        <br />
                                                                        <br />
                                                                        <%--<div class="text-center"><span class="SpecialChar"><%=pl.ExamName %></span></div>--%>
                                                                    </p>
                                                                </div>
                                                                <div class="text-left provisionTable">
                                                                    <table class="w100 fontsmall">
                                                                        <tr>
                                                                            <td>&nbsp</td>
                                                                            <td></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="width: 145px;">Name of Examination & Year :</td>
                                                                            <td colspan="3">
                                                                                <div style="margin-left: 10px"><%=pl.ExamName %></div>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>&nbsp</td>
                                                                            <td></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Roll No.:</td>
                                                                            <td>
                                                                                <div style="margin-left: 10px"><%=pl.Rollno %> </div>
                                                                            </td>
                                                                            <td>Enrollment No. :</td>
                                                                            <td>
                                                                                <div style="margin-left: 10px"><%=pl.EnrollmentNo %> </div>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>&nbsp</td>
                                                                            <td></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>CGPA :</td>
                                                                            <td>
                                                                                <div style="margin-left: 10px"><%=pl.CGPA %></div>
                                                                            </td>
                                                                            <td>Division :</td>
                                                                            <td>
                                                                                <div style="margin-left: 10px"><%=pl.Division %></div>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>&nbsp</td>
                                                                            <td></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>College Code :</td>
                                                                            <td>
                                                                                <div style="margin-left: 10px"><%=pl.College %></div>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>&nbsp</td>
                                                                            <td></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>&nbsp</td>
                                                                            <td></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <br />
                                                            </div>
                                                            <div id="passingcertificate" runat="server" class="mt-medium">
                                                                <div>
                                                                    <p><span class="floatleft"><span>Roll No : </span><span><%=pl.Rollno %></span></span> <span class="floatright"><span>Enrol/Regd. No. : </span><span><%=pl.EnrollmentNo %></span></span></p>
                                                                    <br />
                                                                    <p>This is to certify that</p>
                                                                    <br />
                                                                    <p class="lineHeight">
                                                                        Shri/Smt./Ku.&nbsp;<span class="SpecialStudent"><%=pl.SName %></span><br />
                                                                        <span>passed the </span>
                                                                        <br />
                                                                        <span class="SpecialChar"><%=pl.ExamName %></span>
                                                                        <br />
                                                                        <span class="SpecialChar">Examination held in <%=pl.ExamSession %></span>
                                                                        <br />
                                                                        The subject(s) in which he/she was examined were:
                                                                        <br />
                                                                        <span class="SpecialChar text-left floatleft fontsmall"><%=pl.SubjectName %></span>
                                                                        <br />
                                                                        <span class="fontsmall floatleft">
                                                                            <br />
                                                                            <span class="text-left" style="margin-left: 50px;">Distinction, if any: </span>
                                                                            <span class="SpecialChar"><%=pl.DistinctionSub %></span>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <div id="MeritCertificate" runat="server" class="mt-medium">
                                                                <div>
                                                                    <p class="lineHeightMax">
                                                                        This is to certify that<br />
                                                                        Shri/Smt./Ku. <span class="SpecialStudent"><%=pl.SName %></span><br />
                                                                        Roll No. <span class="SpecialChar"><%=pl.Rollno %></span> has passed 
                                                                        <br />
                                                                        <span class="SpecialChar"><%=pl.ExamName %></span><br />
                                                                        Examination held in <span class="SpecialChar"><%=pl.ExamSession %> in </span><span class="SpecialChar"><%=pl.Division %></span>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;division as a College/External student of this University
                                                                        <br />
                                                                        and has been placed <span class="SpecialChar"><%=pl.MeritNo %></span> in order of merit.
                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <div id="AttempCerificate" runat="server" class="mt-medium">
                                                                <div>
                                                                    <p>This is to certify that</p>
                                                                    <p class="lineHeightMax">
                                                                        Shri/Smt./Ku. <span class="SpecialStudent"><%=pl.SName %></span><br />
                                                                        has passed the
                                                                        <br />
                                                                        <span class="SpecialChar"><%=pl.ExamName %></span>
                                                                        <br />
                                                                        <span class="SpecialChar">Examination of <%=pl.ExamSession %></span>
                                                                        <br />
                                                                        as per details of attempts given below.
                                                                    </p>
                                                                    <div style="">
                                                                        <h3>DETAILS OF ATTEMPTS</h3>
                                                                        <table class="fontsmall text-left">
                                                                            <tr>
                                                                                <th>1</th>
                                                                                <th></th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="MedalCertificate" runat="server" class="mt-medium">
                                                                <div>
                                                                    <p><span class="floatleft"><span>Roll No : </span><span><%=pl.Rollno %></span></span> <span class="floatright"><span>Enrolment No. : </span><span><%=pl.EnrollmentNo %></span></span></p>
                                                                    <br />
                                                                    <p>This is to certify that</p>
                                                                    <br />
                                                                    <p class="lineHeightMax">
                                                                        Shri/Smt./Ku. &nbsp; <span class="SpecialStudent"><%=pl.SName %></span><br />
                                                                        having passed                                                                
                                                                    <span class="SpecialChar"><%=pl.ExamName %></span>&nbsp;
                                                                        <span class="SpecialChar"><%=pl.ExamSession %></span>
                                                                        degree Examination in <span class="SpecialChar"><%=pl.Division %></span> Division and secured <span class="SpecialChar"><%=pl.Rank %></span> rank in
                                                                    <span class="SpecialChar capitalize"><%=pl.SubjectName %></span>
                                                                        subject marks and thereat has been awarded <b>Gold/Silver/Cash</b> <%--<span class="SpecialChar"><%=pl.AwardedBy %></span>--%>
                                                                        <span id="spanforPrize" runat="server"></span><span class="SpecialChar" id="spanAwardPrize" runat="server"></span>
                                                                        in the Convocation of the University.
                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <div id="MarkSheetVerification" runat="server" style="margin-top: 15px">
                                                                <div style="text-align: justify" class="fontsmall lineHeight">
                                                                    <p style="width: 45%;">
                                                                        To,<br />
                                                                        <asp:Label Text="Oswal Computers pvt. ltd." runat="server" />
                                                                    </p>
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <p><b style="width: 100px; display: table-cell;">Subject:</b><span style="display: table-cell; padding-left: 30px">Verification of Statement of Marks of the examinee of Sant Gadge Baba Amravati University.</span></p>
                                                                    <br />
                                                                    <p><b style="width: 100px; display: table-cell;">Reference:</b><span style="display: table-cell; padding-left: 30px">Your Letter No. <span class="SpecialChar"><%=pl.LaterReferenceNo %></span> <span style="float: right">Date - <span class="SpecialChar"><%=pl.LaterReferenceDate.ToShortDateString() %></span></span></span></p>
                                                                    <br />
                                                                    <p style="font-size:15px">Sir, </p>
                                                                    <p style="font-size: 15px; text-indent: 50px;">
                                                                        As desired by you vide letter under reference on the subject cited above, the genuineness
                                                                        of Statement of Marks of the following examinee/s of Sant Gadge Baba Amravati University is
                                                                        hereby confirmed in respect of particulars mentioned below against the name :-
                                                                    </p>
                                                                    <br />

                                                                    <table class="table-bor" style="width: 106%">
                                                                        <tr>
                                                                            <th style="width: 4%">Sr&nbsp;No.</th>
                                                                            <th style="width: 5%">Name&nbsp;of&nbsp;Candidate</th>
                                                                            <th style="width: 47%">Name&nbsp;of&nbsp;Examination</th>
                                                                            <th style="width: 18%">Year&nbsp;of&nbsp;Passing</th>
                                                                            <th style="width: 4%">Roll&nbsp;No.</th>
                                                                            <th style="width: 4%">Division</th>
                                                                            <th style="width: 20%">Remarks</t>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">1</td>
                                                                            <td class="capitalize"><%=pl.SName %></td>
                                                                            <td class="capitalize"><%=pl.ExamName %></td>
                                                                            <td><%=pl.ExamSession %></td>
                                                                            <td><%=pl.Rollno %></td>
                                                                            <td style="text-align: center"><%=pl.Division %></td>
                                                                            <td></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div id="DegreeVerification" runat="server" style="margin-top: 15px">
                                                                <div class="text-justify lineHeight fontsmall">
                                                                    <p style="width: 45%;" class="">
                                                                        To,<br />
                                                                        <asp:Label Text="Oswal Computers pvt. ltd." runat="server" />
                                                                    </p>
                                                                    <br />
                                                                    <br />
                                                                    <p><b style="width: 100px; display: table-cell;">Subject:</b><span style="display: table-cell; padding-left: 30px">Verification of Degree Certificate of the examinee of Sant Gadge Baba Amravati University.</span></p>
                                                                    <br />
                                                                    <p><b style="width: 100px; display: table-cell;">Reference:</b><span style="display: table-cell; padding-left: 30px"> <span class="SpecialChar"><%=pl.LaterReferenceNo %></span> <span style="float: right">Date - <span class="SpecialChar"><%=pl.LaterReferenceDate.ToShortDateString() %></span></span></span></p>
                                                                    <br />

                                                                    <p>Sir/Madam,</p>
                                                                    <p class="Indent-Text">
                                                                        As desired by you/student vide letter under reference on the subject cited above, the genuineness
                                                                        of Degree Certificate of the following Examinee/s of Sant Gadge Baba Amravati University is
                                                                        hereby confirmed in respect of particular/s mentioned below against the name :-
                                                                    </p>
                                                                    <br />
                                                                    <table class="table-bor">
                                                                        <tr>
                                                                            <th>Sr No.</th>
                                                                            <th>Name of Candidate</th>
                                                                            <th>Name of Examination</th>
                                                                            <th>Branch/ Subject/ Faculty</th>
                                                                            <th>Year of Passing <small>(Regular/Distance)</small></th>
                                                                            <th>Roll No.</th>
                                                                            <th>Division</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center; font-size: 15px;">1</td>
                                                                            <td style="font-size: 15px;" class="capitalize"><%=pl.SName %></td>
                                                                            <td style="font-size: 15px;" class="capitalize"><%=pl.ExamName %></td>
                                                                            <td style="text-align: center; font-size: 15px;"><%=pl.BranchName %></td>
                                                                            <td><%=pl.ExamSession %></td>
                                                                            <td><%=pl.Rollno %></td>
                                                                            <td style="text-align: center"><%=pl.Division %></td>
                                                                        </tr>
                                                                    </table>
                                                                    <br />
                                                                    <p style="font-size: small;" class="Indent-Text">
                                                                        <b>It is also informed you that, in future, whenever verification is required 
                                                                             the information (i.e. Name of Exam, Year of Passing, Roll No. & Division) be clearly mentioned,
                                                                             and the application/letter should be submitted for Degree or Mark sheet separately.
                                                                        </b>
                                                                    </p>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div id="DateOfDeclaration" runat="server" class="mt-small">
                                                                <div class="lineHeight">
                                                                    <p>This is to certify that</p>
                                                                    <p>
                                                                        Shri/Smt./Ku. <span class="SpecialStudent"><%=pl.SName %></span><br />
                                                                        Roll No. <span class="SpecialChar"><%=pl.Rollno %></span> Enrollment No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <span class="SpecialChar"><%=pl.EnrollmentNo %></span> presented
                                                                        himself/herself at the <span class="SpecialChar"><%=pl.ExamName %></span> Examination held in <span class="SpecialChar"><%=pl.ExamSession %></span> and offered :
                                                                        <br />
                                                                        <span class="SpecialChar text-left floatleft"><%=pl.SubjectName %></span>
                                                                        <br />
                                                                        <br />
                                                                        <br />
                                                                        as his/her subjects at the said Examination, the result of which is declared on <span class="SpecialChar"><%=pl.DateResultDeclaration.ToShortDateString() %></span> in the University office.
                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <div id="MediumOfInstruction" runat="server" class="mt-medium">
                                                                <div class="lineHeightMax text-justify">
                                                                    <p class="text-center">TO WHOMSOEVER IT MAY CONCERN</p>
                                                                    <p class="text-center">This is to certify that</p>
                                                                    <p style="font-size: 17px;">
                                                                        degree of <span class="SpecialChar"><%=pl.ExamName %></span> is conferred upon <span class="SpecialStudent"><%=pl.SName %></span>
                                                                        on having passed examination to the said degree in <span class="SpecialChar"><span><%=pl.ExamSession %></span>
                                                                            vide Roll No. - <span class="SpecialChar"><%=pl.Rollno %></span></span> from this University, formerly known as Amravati University.
                                                                    </p>
                                                                    <p style="font-size: 17px;" class="SpecialChar Indent-Text">Medium of instruction and assessment throughout the entire course was in English.</p>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div id="Footer" style="margin-bottom: 0px">
                                                <div class="floatleft" style="margin-top: 50px">
                                                    Amravati<%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                    <br />
                                                    Print Date : <%=DataAcces.Date.ToString("dd/MM/yyyy") %>
                                                    <br />
                                                    <asp:Label ID="lblVerification" Visible="false" runat="server" />
                                                </div>
                                                <div id="divFloatRight" class="floatright" style="margin-top: 30px" runat="server">
                                                    <asp:Label ID="lblYours" Text="Yours : " Visible="false" runat="server" />
                                                    <br />
                                                    <%=Signature %><br />
                                                    <%=DataAcces.universityName %><br />
                                                    <%=DataAcces.city %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hfApplicationID" runat="server" />
                                    <style type="text/css">
                                        /* This Style Should Be Under The PrintDiv */
                                        #PrintDiv {
                                            /*max-height:842px;
                                            height:842px;*/
                                        }

                                        #Header {
                                            text-align: center;
                                            padding-top: 20px;
                                            font-family: Arial;
                                            text-transform: uppercase;
                                            /*height:20%;*/
                                            /*height:200px;*/ /*Change*/
                                        }

                                        .BodyContent {
                                            display: inline-block;
                                            text-align: center;
                                        }

                                            .BodyContent > .bodyStyle {
                                                padding: 0px 20px 15px 20px;
                                                font-family: Arial;
                                                font-size: 22px;
                                                /*height:60%;*/
                                                height: 640px;
                                                max-height: 710px;
                                                overflow: hidden;
                                                /*letter-spacing: .5px;*/
                                            }

                                        #Footer {
                                            /*height:10%;*/
                                            text-align: center;
                                            font-size: 16px;
                                            /*height: 70px;*/
                                        }

                                        .provisionTable {
                                            margin-top: 5%;
                                            font-family: Arial;
                                        }

                                        .mt-medium {
                                            margin-top: 70px;
                                        }

                                        .mt-small {
                                            margin-top: 50px;
                                        }

                                        .capitalize {
                                            text-transform: capitalize;
                                        }

                                        .lineHeightMax {
                                            line-height: 2;
                                        }

                                        .lineHeight {
                                            line-height: 1.5;
                                        }

                                        .fontsmall {
                                            font-size: 17px;
                                        }

                                        .text-justify {
                                            text-align: justify;
                                        }

                                        .text-left {
                                            text-align: left;
                                        }

                                        .text-center {
                                            text-align: center;
                                        }

                                        .text-lowercase {
                                            text-transform: lowercase;
                                        }

                                        /*.certified {
                                            text-align: center;
                                            display: table;
                                            font-size: 22px;
                                            /*line-height: 2.5;
                                        }*/

                                        .w100 {
                                            width: 100%;
                                        }

                                        .floatleft {
                                            float: left;
                                        }

                                        .floatright {
                                            float: right;
                                        }

                                        .UnivName {
                                            font-size: 29px;
                                            font-weight: bold;
                                        }

                                        .Certifiacate {
                                            font-size: 22px;
                                            font-weight: bold;
                                        }

                                        p {
                                            margin: 0px;
                                            width: 100%;
                                            display: table;
                                            height: auto;
                                            /* font-size: 16px;
                                            line-height: 2.5;
                                            text-align: justify;
                                            font-family: Arial; */
                                        }

                                        .SpecialChar {
                                            font-weight: bold;
                                            /*font-size: 21px;
                                            font-family: 'Monotype Corsiva';*/
                                        }

                                        .Indent-Text {
                                            text-indent: 100px;
                                        }

                                        .SpecialStudent {
                                            font-family: 'Times New Roman';/*'Monotype Corsiva';*/
                                            font-weight: bold;
                                            font-size: 24px;
                                            text-transform: capitalize;
                                            /*line-height: normal;*/
                                        }

                                        /* This Is For Generating Report and Pop Up */

                                        .modal-fade {
                                            position: fixed;
                                            top: 0;
                                            right: 0;
                                            bottom: 0;
                                            left: 0;
                                            z-index: 1050;
                                            overflow-x: hidden;
                                            -webkit-overflow-scrolling: touch;
                                            nav-up: auto;
                                            outline: 0;
                                            background-color: rgba(128, 128, 128, 0.8);
                                        }

                                        .table-bor > thead > tr > th, .table-bor > tbody > tr > th, .table-bor > thead > tr > td, .table-bor > tbody > tr > td {
                                            border: 1px solid #50618C;
                                            padding-left: 1px;
                                        }

                                        .table-bor {
                                            border-collapse: collapse;
                                            font-size: small;
                                            width: 106%;
                                            margin: 0 -3% 0 -3%;
                                            text-align: left;
                                            letter-spacing: initial;
                                        }

                                        .outer-Content {
                                            background-color: white;
                                            padding: 1%;
                                            border-radius: 6px;
                                            box-shadow: rgba(0, 0, 0, 0.3) -2px -1px 10px 6px;
                                            margin: 5% 20%;
                                            display: table;
                                            width: 60%;
                                        }

                                        .main-Content {
                                            font-family: Arial;
                                            border-width: 10px;
                                            -webkit-border-image: url(images/Bor-11.png) 7.11% round;
                                            -moz-border-image: url(images/Bor-11.png) 7.11% round;
                                            border-image: url(images/Bor-11.png) 7.11% round;
                                            outline: 1px solid black;
                                            height: 91%;
                                        }

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
                                                height: 28cm;
                                                size:portrait
                                            }
                                        }
                                    </style>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        function PrintDiv() {
            //alert(1);
            //var url = "FrmPrint_OLD.aspx/PrintStatus";
            //request.open("POST", url, false);

            //request.setRequestHeader("Content-Type", "application/json");
            if (true) {

            }
            var divcontents = document.getElementById("PrintDiv").innerHTML;
            var printwindow = window.open('', '');
            printwindow.document.write('<html><head><title></title>');
            printwindow.document.write('<style> ' +
             '.table-bordered {border: solid ;} ' +
             '.padd{padding:5% 5%}' +
            '</style>'

            );
            printwindow.document.write('</head><body>');
            printwindow.document.write(divcontents);
            printwindow.document.write('</body></html>');
            printwindow.document.close();
            printwindow.print();
        }

        function CreateSrNo() {
            var x = document.getElementById("<%=lblNo.ClientID%>");
            var y = document.getElementById("SrNo");
            var str = "", size = 0;
            for (var i = 0; i < x.innerText.length ; i++) {
                size = 15 + i;
                str += '<span style="font-size:' + size + 'px">' + x.innerText.substring(i, i + 1) + '</span>'
            }
            y.innerHTML = str;
        }

        function ShowCertificate() {
            CreateSrNo();
            document.getElementById("divCertificate").style.display = 'block'; //$('#divCertificate').show();
        }

        function HideRemarkPopup() {
            $('#divCertificate').hide(); //document.getElementById("divCertificate").style.display = 'None';            
        }

        $("#btnClsRemark").live('click', function () { HideRemarkPopup(); });

    </script>
</asp:Content>
