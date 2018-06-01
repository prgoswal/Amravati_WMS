<%@ Page Title="" MaintainScrollPositionOnPostback="true" Async="true" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmPrint.aspx.cs" Inherits="FrmPrint" %>

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
    </style>
    <style type="text/css">
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
    </script>
    <script src="js/1.12.4.jquery.min.js"></script>
    <script type="text/javascript">
        function printReport() {
            var rv1 = $('#<%=ReportViewer1.ClientID%>');
            var iDoc = rv1.parents('html');

            // Reading the report styles
            var styles = iDoc.find("head style[id$='ReportControl_styles']").html();
            if ((styles == undefined) || (styles == '')) {
                iDoc.find('head script').each(function () {
                    var cnt = $(this).html();
                    var p1 = cnt.indexOf('ReportStyles":"');
                    if (p1 > 0) {
                        p1 += 15;
                        var p2 = cnt.indexOf('"', p1);
                        styles = cnt.substr(p1, p2 - p1);
                    }
                });
            }
            if (styles == '') { alert("Cannot generate styles, Displaying without styles.."); }
            styles = '<style type="text/css">' + styles + "</style>";

            // Reading the report html
            var table = rv1.find("div[id$='_oReportDiv']");
            if (table == undefined) {
                alert("Report source not found.");
                return;
            }

            // Generating a copy of the report in a new window
            var docType = '<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/loose.dtd">';
            var docCnt = styles + table.parent().html();
            var docHead = '<head><title>Printing ...</title><style>body{margin:5;padding:0;}</style></head>';
            var winAttr = "location=yes, statusbar=no, directories=no, menubar=no, titlebar=no, toolbar=no, dependent=no, width=720, height=600, resizable=yes, screenX=200, screenY=200, personalbar=no, scrollbars=yes";;
            var newWin = window.open("", "_blank", winAttr);
            writeDoc = newWin.document;
            writeDoc.open();
            writeDoc.write(docType + '<html>' + docHead + '<body onload="window.print();">' + docCnt + '</body></html>');
            writeDoc.close();

            // The print event will fire as soon as the window loads
            newWin.focus();
            // uncomment to autoclose the preview window when printing is confirmed or canceled.
            // newWin.close();
        };
    </script>

    <style type="text/css">
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
    </style>


    <script src="js/jquery-ui.min.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <style>
        /*#noti_Button {
            width: 22px;
            height: 22px;
            line-height: 22px;
            background: #FFF;
           margin: -3px 10px 0 10px;
            cursor: pointer;
        }*/
        /* THE POPULAR RED NOTIFICATIONS COUNTER. */
        #noti_Counter {
            display: block;
            position: absolute;
            background: #E1141E;
            color: #FFF;
            font-size: 10pt;
            font-weight: normal;
            padding: 1px 8px;
            /*margin: 10px 0 0 9px;*/
            border-radius: 2px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            text-align: right;
            z-index: 1;
            margin-top: -21px !important;
            left: 25%;
            /*top: 0px;*/
            /*left: 130px;*/
        }



        /*#noti_Container {
            position: relative;
        }*/
        /* A CIRCLE LIKE BUTTON IN THE TOP MENU. */
        /*#noti_Button {
            width: 0px;
            height: 22px;
            line-height: 22px;
            margin: -2px 0 0 9px;
            cursor: pointer;
        }*/
    </style>

    <script type="text/javascript" lang="javascript">


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

        $(".btnClose").live('click', function () { HidePopup(); });
    </script>

    <div class="row">
        <div class="col-lg-offset-0 col-md-offset-0 "></div>
        <div class="col-lg-12 col-sm-offset-0">
            <div class="user-creation">
                <h3 class="text-center">Pending For Printing Document </h3>
                <hr />
                <!--SHOW NOTIFICATIONS COUNT.-->
                <%--  <asp:UpdatePanel ID="upd" runat="server">

                    <ContentTemplate>--%>



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
                                                            <asp:LinkButton ID="lnkpending" runat="server" Text='<%#Eval("DocType") %>' PostBackUrl='<%#"~/FrmPrint.aspx?docid="+Eval("DocID") +"&Cnt="+ Eval("Cnt") +"&DocType="+ Eval("DocType")%>' OnClick="lnkpending_Click"></asp:LinkButton>
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
                                        <td class="tableDetail" style="width: 5%">Sr. No.</td>
                                        <td class="tableDetail" style="width: 11%">Application No.</td>
                                        <td class="tableDetail" style="width: 8%">Roll No.</td>
                                        <td class="tableDetail" style="width: 15%">Student Name.</td>
                                        <td class="tableDetail" style="width: 13%">Application Date</td>
                                        <td class="tableDetail" style="width: 14%">Last Approval Date</td>
                                        <td class="tableDetail" style="width: 7%">Action</td>
                                        <%--<td class="tableDetail" style="width: 7%">Action</td>
                                        <td class="tableDetail" style="width: 5%">Status</td>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="width: 100%">
                                            <div class="text-center" style="overflow: auto; height: 150px; width: 100%">
                                                <asp:UpdatePanel ID="upd" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView align="center" ID="grdPandingDetails" runat="server" DataKeyNames="ApplicationID" ShowHeader="false" HeaderStyle-BackColor="#50618c"
                                                            HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="100%" CellPadding="4"
                                                            RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                                                            BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false" OnRowCommand="grdPandingDetails_RowCommand" AutoGenerateColumns="false" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrownumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ApplicationID" ControlStyle-Height="100" ItemStyle-Width="11%" />
                                                                <asp:BoundField DataField="RollNo" ControlStyle-Height="100" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="StudentName" ControlStyle-Height="100" ItemStyle-Width="15%" />
                                                                <asp:BoundField DataField="EntryDate" ControlStyle-Height="100" ItemStyle-Width="13%" />

                                                                <asp:BoundField DataField="LastApprovalDate" ItemStyle-Width="14%" />

                                                                <%--  <asp:ButtonField CommandName="ShowPopup" HeaderText="Print" Text="Print" ButtonType="Link" ItemStyle-Width="7%" />--%>
                                                                <asp:TemplateField ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" Text="Print" CommandArgument='<%#Eval("ApplicationID") %>' CommandName="ShowPopup">Print</asp:LinkButton>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField ItemStyle-Width="7%">
                                                                    <ItemTemplate>                                                                       
                                                                        <asp:LinkButton ID="LinkButtonPrint" Visible="false" CommandArgument='<%#Eval("ApplicationID") %>' runat="server" CommandName="print">Print</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                        </td>
                                    </tr>

                                </table>
                            </asp:Panel>
                            <asp:Panel BorderStyle="Solid" BorderWidth="1px" ID="pnlReport" runat="server">
                                <%--<rsweb:ReportViewer ID="ReportViewer1" ShowPrintButton="true" ShowParameterPrompts="false" runat="server" Width="700px"></rsweb:ReportViewer>--%>
                                <div>
                                    <asp:Button ID="btnPrintReport" Text="Print" runat="server" OnClientClick="printReport(); return false;" />
                                </div>
                                <rsweb:ReportViewer ID="ReportViewer1" ShowPrintButton="true" ShowParameterPrompts="false" runat="server" Width="100%"></rsweb:ReportViewer>
                            </asp:Panel>
                            <h4>
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></h4>
                        </td>
                    </tr>
                </table>

                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>

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


                <script type="text/javascript">
                    function ShowPopupPrint() {
                        //$('#mask').show();


                        $('#<%=pnlpopup.ClientID %>').show();
                    }
                    function HidePopupPrint() {
                        //$('#mask').hide();
                        $('#<%=pnlpopup.ClientID %>').hide();
                    }
                    $(".btnClose11").live('Click', function () {
                        HidePopupPrint();


                    });
                </script>

            </div>
        </div>
    </div>
</asp:Content>

