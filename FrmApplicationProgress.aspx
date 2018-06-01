<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" Async="true" AutoEventWireup="true" CodeFile="FrmApplicationProgress.aspx.cs" Inherits="FrmApplicationProgress" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/style1.css" />
    <%--<script src="js/jquery-1.9.1.min.js"></script>--%>
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/progress-form.js"></script>
    <style type="text/css" media="print">
        @page {
            size: landscape;
        }
    </style>
    <style>
        .bs-wizard {
            margin-top: 40px;
        }
        /*Form Wizard*/
        .bs-wizard {
            border-bottom: solid 1px #e0e0e0;
            padding: 0 0 10px 0;
        }

            .bs-wizard > .bs-wizard-step {
                padding: 0;
                position: relative;
            }

                .bs-wizard > .bs-wizard-step + .bs-wizard-step {
                }

                .bs-wizard > .bs-wizard-step .bs-wizard-stepnum {
                    color: #595959;
                    font-size: 16px;
                    margin-bottom: 5px;
                }

                .bs-wizard > .bs-wizard-step .bs-wizard-info {
                    color: #999;
                    font-size: 14px;
                }

                .bs-wizard > .bs-wizard-step > .bs-wizard-dot {
                    position: absolute;
                    width: 30px;
                    height: 30px;
                    display: block;
                    background: #129745;
                    top: 45px;
                    left: 50%;
                    margin-top: -15px;
                    margin-left: -15px;
                    border-radius: 50%;
                }

                    .bs-wizard > .bs-wizard-step > .bs-wizard-dot:after {
                        content: ' ';
                        width: 14px;
                        height: 14px;
                        background: #fbbd19;
                        border-radius: 50px;
                        position: absolute;
                        top: 8px;
                        left: 8px;
                    }

                .bs-wizard > .bs-wizard-step > .progress {
                    position: relative;
                    border-radius: 0px;
                    height: 8px;
                    box-shadow: none;
                    margin: 20px 0;
                }

                    .bs-wizard > .bs-wizard-step > .progress > .progress-bar {
                        width: 0px;
                        box-shadow: none;
                        background: #129745;
                    }

                .bs-wizard > .bs-wizard-step.complete > .progress > .progress-bar {
                    width: 100%;
                }

                .bs-wizard > .bs-wizard-step.active > .progress > .progress-bar {
                    width: 50%;
                }

                .bs-wizard > .bs-wizard-step:first-child.active > .progress > .progress-bar {
                    width: 0%;
                }

                .bs-wizard > .bs-wizard-step:last-child.active > .progress > .progress-bar {
                    width: 100%;
                }

                .bs-wizard > .bs-wizard-step.disabled > .bs-wizard-dot {
                    background-color: #f5f5f5;
                }

                    .bs-wizard > .bs-wizard-step.disabled > .bs-wizard-dot:after {
                        opacity: 0;
                    }

                .bs-wizard > .bs-wizard-step:first-child > .progress {
                    left: 50%;
                    width: 50%;
                }

                .bs-wizard > .bs-wizard-step:last-child > .progress {
                    width: 50%;
                }

                .bs-wizard > .bs-wizard-step.disabled a.bs-wizard-dot {
                    pointer-events: none;
                }

        .rcorners2 {
            border-radius: 15px;
            border: 2px solid white;
            padding: 20px;
            width: 100%;
            height: 100%;
        }

        .mytextshadow {
            text-shadow: 2px 2px 4px #000000;
            font-size: 17px;
        }

        h4 {
            text-shadow: 1px 1px 2px black, 0 0 25px white, 0 0 5px;
            font-size: 15px;
        }

        .mylevel {
            text-shadow: 1px 1px 2px black, 0 0 25px white, 0 0 5px;
            font-size: 15px;
        }

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

        .main-Content {
            background-color: white;
            padding: 1%;
            border-radius: 6px;
            box-shadow: rgba(0, 0, 0, 0.3) -2px -1px 10px 6px;
            margin: 5%;
        }
    </style>

    <script>
        $(function () {
            $('#txtapplicationno').popover('show')
            $("[data-toggle = 'popover']").popover();
        });
        //$(function () {
        //    $(".Validators").Float();
        //});
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="font-family: Calibri;">
        <div class="user-creation" style="height: 100%; position: relative;">
            <h3 class="text-center" style="font-family: Calibri;">Application Trail</h3>
            <hr class="changecolor" />
            <div class="row rcorners2">
                <div class="row">
                    <label class="col-md-2 ">Enter Application No.</label>
                    <div class="col-md-2" id="txtAppCtrl">
                        <asp:TextBox ID="txtapplicationno" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="txtsearch" runat="server" data-toggle="popover" Text="Search" OnClick="txtsearch_Click" ValidationGroup="s" CssClass="btn btn-primary" />
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtapplicationno" FilterType="Numbers" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
                    </div>
                    <asp:LinkButton ID="LinkShowLog" runat="server" Text="Application Log" CssClass=" label label-default" Style="font-family: Calibri; font-weight: bold; font-size: medium; border-radius: 30px; text-decoration-color: lightyellow;" OnClientClick="ShowRemarkPopup(); return false;"></asp:LinkButton>

                    <label class="col-md-5">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" CssClass="Validators text-danger" runat="server" ValidationGroup="s" ErrorMessage="Please Enter Application No." ControlToValidate="txtapplicationno"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblmsg" runat="server" CssClass="Validators text-danger" Display="Dynamic"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="Validators text-danger " ValidationGroup="s" Display="Dynamic" runat="server" ErrorMessage="Length must be between 4 to 10 Integer Value" ControlToValidate="txtapplicationno" ValidationExpression="^[0-9]{4,10}$"></asp:RegularExpressionValidator>
                    </label>
                </div>
                <br />
                <div class="row">
                    <label class="col-md-3">
                        <asp:Label ID="lbltextstudentname" runat="server"></asp:Label></label>
                    <label class="col-md-9">
                        <asp:Label ID="lblapplicant" runat="server"></asp:Label>
                    </label>
                    <label class="col-md-3">
                        <asp:Label ID="lbltextrollno" runat="server"></asp:Label></label>
                    <label class="col-md-9">
                        <asp:Label ID="lblrollno" runat="server"></asp:Label></label>
                    <label class="col-md-3">
                        <asp:Label ID="lbltextexamname" runat="server"></asp:Label></label>
                    <label class="col-md-9">
                        <asp:Label ID="lblexamname" runat="server"></asp:Label></label>
                    <label class="col-md-3">
                        <asp:Label ID="lbltextappliedfor" runat="server"></asp:Label></label>
                    <label class="col-md-9">
                        <asp:Label ID="lblappliedfor" runat="server"></asp:Label></label>
                    <label class="col-md-3">
                        <asp:Label ID="lbltextFinalstatus" CssClass="text-info" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label></label>
                    <label class="col-md-9">
                        <asp:Label ID="lblfinalstatus" CssClass="text-info" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
                    </label>
                </div>

            </div>
            <asp:Panel ID="pnlprogress" runat="server" Width="170%" Style="margin-top: 50px;">
                <div class="row bs-wizard" style="border-bottom: 0; width: 100%;">
                    <div id="level0div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">
                                <asp:Label ID="Label1" Text="COUNTER" runat="server" />
                            </span>
                        </div>
                        <div id="thediv0" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>
                        <div class="bs-wizard-dot"></div>
                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lblpreparedatetext" runat="server" CssClass="text-danger"></asp:Label><br />
                            <asp:Label ID="lblpreparedate" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lblprepareStatus" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level1div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">
                                <asp:Label ID="Label2" Text="SUPRITENDENT" runat="server" /></span>
                        </div>
                        <div id="thediv1" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>
                        <div class="bs-wizard-dot"></div>
                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel1datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel1Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel1Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level2div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <!-- complete -->
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">
                                <asp:Label ID="Label3" Text="ASSISTANT REGISTRAR" runat="server" /></span>
                        </div>
                        <div id="thediv2" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>
                        <div class="bs-wizard-dot"></div>
                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel2Datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel2Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel2Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level3div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger"><asp:Label ID="Label4" Text="DEPUTY REGISTRAR" runat="server" /></span>
                        </div>
                        <div id="thediv3" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>
                        <div class="bs-wizard-dot"></div>
                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel3datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel3Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel3Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level4div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger"><asp:Label ID="Label5" Text="REGISTRAR" runat="server" /></span>
                        </div>
                        <div id="thediv4" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>
                        <div class="bs-wizard-dot"></div>
                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel4datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel4Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel4Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level5div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">
                                <asp:Label ID="Label6" runat="server" /></span>
                        </div>
                        <div id="thediv5" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>


                        <div class="bs-wizard-dot"></div>

                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel5datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel5Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel5Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level6div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">
                                <asp:Label ID="Label7" runat="server" /></span>
                        </div>
                        <div id="thediv6" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>


                        <div class="bs-wizard-dot"></div>

                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel6datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel6Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel6Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="level7div" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">
                                <asp:Label ID="Label8" runat="server" /></span>
                        </div>
                        <div id="thediv7" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>


                        <div class="bs-wizard-dot"></div>

                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lbllevel7datetext" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel7Date" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label><br />
                            <asp:Label ID="lbllevel7Status" CssClass="text-success" Font-Size="Smaller" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="printdiv" runat="server" class="col-sm-1 bs-wizard-step complete">
                        <div class="text-center bs-wizard-stepnum" style="width: 100%; color: #003F8E;">
                            <span class="text-danger">PRINTED</span>
                        </div>
                        <div id="thedivprint" runat="server" class="progress">
                            <div class="progress-bar"></div>
                        </div>
                        <div class="bs-wizard-dot"></div>
                        <div class="bs-wizard-info text-center" style="width: 100%; color: #003F8E;">
                            <asp:Label ID="lblPrintDateText" CssClass="text-danger" runat="server"></asp:Label><br />
                            <asp:Label ID="lblPrintDate" CssClass="text-success" runat="server"></asp:Label><br />
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnlRemarkpopup" runat="server" BackColor="White" Width="97.333%"
                Style="z-index: 111; float: right; background-color: #f1efef; position: absolute; top: 13%; border: outset 2px gray; padding: 0px; display: none">
                <div class="modal-fade">
                    <div style="margin-top: 1%">
                        <div class="main-Content">
                            <div class="row">
                                <div class="col-sm-12" style="margin-bottom: 3px;">
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" onclick="return PrintDiv();">Print</button>
                                    <div style="float: right;">
                                        <a id="btnClsRemark" style="color: white; text-decoration: none;" class="btnClose" href="javascript:HideRemarkPopup()">
                                            <img src="images/cancel-512.png" style="margin-top: -7px;" height="20px" width="20px" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-inline">
                                <div id="dvContents" style="font-family: Calibri;">
                                    <%--In this div contents are print in html report --%>
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
                                    <div class="double table-bordered">
                                        <div style="border: 1px solid black; height: 95%;">
                                            <div class="col-md-12" style="margin-top: 3%;">
                                                <div style="">
                                                    <table style="font-weight: bold; text-align: center">
                                                        <tr>
                                                            <td style="font-family: Calibri; font-size: 21pt;">
                                                                <%=DataAcces.universityName.ToUpper() %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-family: Calibri; font-size: 17pt;">APPLICATION LOG FOR APPLICATION NO.&nbsp&nbsp: &nbsp&nbsp  
                                        <asp:Label ID="lblPopupapplicationNo" CssClass="text-info " Font-Bold="true" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 212px; float: right;">Sr. No. -
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%;">
                                                                <img src="<%=DataAcces.LogoUrl%>" style="margin-right: 10px;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div>
                                                &nbsp;
                                            </div>
                                            <div class="col-md-12">
                                                <table style="width: 100%; font-size: 11pt;">
                                                    <tr>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 10%; text-align: left; font-size: 11pt; font-family: Calibri;">
                                                            <span style="font-weight: bold;">ROLL NO. </span><span style="float: right !important;">:</span>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="lblrollnopopup" Style="margin-left: 15px" CssClass="text-info" Font-Size="15px" font-family="Arial" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%; font-size: 11pt; font-family: Calibri;">
                                                            <span style="font-weight: bold">STUDENT NAME </span><span style="float: right !important;">:</span>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <asp:Label ID="Lblstudentnamepopup" Style="margin-left: 15px" CssClass="text-info " Font-Size="15px" font-family="Arial" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td style="width: 12%; font-family: Calibri;">
                                                            <span style="font-weight: bold">EXAM SESSION </span><span style="float: right !important;">:</span>
                                                        </td>
                                                        <td style="width: 18%;">
                                                            <asp:Label ID="lblpopupExamSession" Style="margin-left: 15px" CssClass="text-info" Font-Size="15px" font-family="Arial" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                            <div class="col-md-12">
                                                <table style="width: 100%; font-size: 11pt;">
                                                    <tr>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 10%; text-align: left; font-family: Calibri;">
                                                            <span style="font-weight: bold; font-family: Calibri;">EXAM NAME</span><span style="float: right !important;">:</span>
                                                        </td>
                                                        <td style="width: 80%;">
                                                            <asp:Label ID="lblexampopup" Style="margin-left: 15px" Font-Size="15px" font-family="Arial" CssClass="text-info" runat="server" Text="Label"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>

                                            <div class="col-md-12">
                                                <table style="width: 100%; font-size: 11pt;">
                                                    <tr>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 10%; font-family: Calibri;">
                                                            <span style="font-weight: bold; font-family: Calibri;">APPLIED FOR </span><span style="float: right !important;">:</span>
                                                        </td>
                                                        <td style="width: 50%;">
                                                            <asp:Label ID="lblappliedforpopup" Style="margin-left: 15px" Font-Size="15px" font-family="Arial" CssClass="text-info " runat="server" Text="Label"></asp:Label></td>
                                                        <td style="width: 12%; font-size: 11pt; font-family: Calibri;">
                                                            <span style="font-weight: bold">FINAL STATUS </span><span style="float: right !important;">:</span>
                                                        </td>
                                                        <td style="width: 18%;">
                                                            <asp:Label ID="lblfinalstatuspopup" Style="margin-left: 15px" CssClass="text-info" Font-Size="15px" font-family="Arial" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                &nbsp;
                                            </div>
                                            <div id="tblRemark" runat="server" style="font-family: Calibri;">
                                            </div>
                                            <br />
                                            <div style="width: 100%; margin-left: 9%;">
                                                <table style="width: 20%;">
                                                    <tr>
                                                        <td rowspan="2"><span style="font-size: 10px; font-family: Calibri; margin-left: 10%;">PRINT DATE</span></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="text-shadow: 0px 0px 0.9px black; color: black; font-family: Franklin Gothic Heavy; font-weight: bold; width: 17px;">
                                                            <div style="font-size: 2.8px; padding-top: 0px;">
                                                                <span>©</span>
                                                                <br />
                                                                <span>©</span>
                                                            </div>
                                                        </td>
                                                        <td style="padding-top: 0px;">
                                                            <asp:Label ID="lbldate" runat="server" CssClass="text-info" Font-Size="10px" font-family="Arial"></asp:Label></td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <table style="width: 86%; font-family: Calibri">
                                                    <tr>
                                                        <td>
                                                            <div style="float: left; text-align: center;">
                                                                <%=DataAcces.city %><br />
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
                                                <%--<div>
                                                        <div style="float:left; text-align:center;" >
                                                            <div>
                                                                  <%=DataAcces.city %><br />
                                                                 <%=DataAcces.Date %>
                                                                </div>
                                                        </div>
                                                        <div style="float:right; text-align:center;">
                                                            <%=DataAcces.universityName %><br />
                                                            <%=DataAcces.city %>
                                                        </div>
                                                </div>--%>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <label class="col-md-12" style="align-content: center; align-items: center;">
                    <asp:Label ID="LblRemark" runat="server" Text=""></asp:Label>
                </label>
            </div>
        </div>
    </div>
    <%--<style>
            @media screen {
                body { 
       
                    margin: 20px;
                }
                p {
                    font-family: verdana,sans-serif;
                    font-size: 14px;
                }
            }

            @media print {
     
                @page {
                    size: A4 landscape;      
                    margin: 0mm;
       
                }
                body { 
                    background-color: white; 
                    margin: 1in;
                }    
            }
    </style>--%>
    <%-- <script type="text/javascript">
        function PrintDiv() {
            var printContents = document.getElementById("dvContents").innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>--%>

    <script type="text/javascript"> ////////For Print Preview///////
        function PrintDiv() {
            var divcontents = document.getElementById("dvContents").innerHTML;
            var printwindow = window.open('', '');
            printwindow.document.write('<html><head><title></title>');
            printwindow.document.write('<style> .double {margin-top:2%; margin-left:6%;}' +
             'body{padding : 10px 20px 0px 5px}' +
             '.table-bordered {border: solid ;}  border: 1px solid #ddd !important;' +
            '</style>'
            );
            printwindow.document.write('</head><body>');
            printwindow.document.write(divcontents);
            printwindow.document.write('</body></html>');
            printwindow.document.close();
            printwindow.print();
        }

    </script>

    <script type="text/javascript" lang="javascript">

        var txtapp = document.getElementById("<%=txtapplicationno.ClientID%>");
        var lblRoll = document.getElementById("<%=lblrollno.ClientID%>");
        var msg = document.getElementById("<%=lblmsg.ClientID%>");

        function ShowRemarkPopup() {
            if (msg.value != '' && lblRoll.innerHTML == '') {
                document.getElementById("txtAppCtrl").classList.add('has-error');
                msg.innerText = "Please Enter Application No.";
                txtapp.focus();
                return false;
            }
            if (txtapp.value == '' && lblRoll.innerHTML == '') {
                msg.innerText = "Please Enter Application No.";
                txtapp.focus();
                document.getElementById("txtAppCtrl").classList.add('has-error');
            } else if (lblRoll.innerHTML == '') {
                msg.innerText = "Entry Not Done For Given Application No.";
                document.getElementById("txtAppCtrl").classList.add('has-error');
            }
            else {
                    <%--$('#<%=pnlprogress.ClientID%>').hide();--%>
                $('#Remarkmask').show();
                $('#<%=pnlRemarkpopup.ClientID %>').show();
            }
    }
        <%--function ShowRemarkPopup() {                  

            if ($('#<%=txtapplicationno.ClientID%>').val() != '' && lblRoll.innerHTML != '') {
                $('#Remarkmask').show();
                $('#<%=pnlRemarkpopup.ClientID %>').show();
            }
            else {

                jAlert('Please Enter Application No.', 'Error', function () { $('#<%=txtapplicationno.ClientID%>').focus() });
                $('#<%=txtapplicationno.ClientID%>').css('border-color', 'Red');


            }
        }--%>
        function HideRemarkPopup() {
            $('#Remarkmask').hide();
            $('#<%=pnlRemarkpopup.ClientID %>').hide();
        }
        $("#btnClsRemark").live('click', function () { HideRemarkPopup(); });
    </script>
</asp:Content>

