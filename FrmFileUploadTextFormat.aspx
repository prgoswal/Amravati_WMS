<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Async="true" CodeFile="FrmFileUploadTextFormat.aspx.cs" Inherits="FrmFileUploadTextFormat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="js/jquery.min.js"></script>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/jasny-bootstrap/3.1.3/css/jasny-bootstrap.min.css" />

    <!-- Latest compiled and minified JavaScript -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/jasny-bootstrap/3.1.3/js/jasny-bootstrap.min.js"></script>

    <style type="text/css">
        .jumbotron {
            margin-top: 22px;
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color: white;
        }

        .container-full {
            margin-left: -15px;
            position: absolute;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: auto;
            overflow:auto;
            
        }

        .glyphicon-refresh-animate {
            -animation: spin .7s infinite linear;
            -webkit-animation: spin2 .7s infinite linear;
        }

        @-webkit-keyframes spin2 {
            from {
                -webkit-transform: rotate(0deg);
                /*-ms-transform:rotateZ(10deg);*/
                -ms-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
                -ms-transform: rotate(0deg);
            }
        }

        @keyframes spin {
            from {
                transform: scale(1) rotate(0deg);
                -ms-transform: scale(1) rotate(0deg);
            }

            to {
                transform: scale(1) rotate(360deg);
                -ms-transform: scale(1) rotate(360deg);
            }
        }
    </style>

    <script type="text/javascript">
        var btnClick = document.getElementById("<%=btnSave.ClientID%>")
       <%-- var msg = document.getElementById("<%=lblMsg.ClientID%>");--%>
        var alertB = document.getElementById("AlertBox");
        var txtc = document.getElementById("txtCheck");
        var span1 = document.getElementById("<%=spanFName1.ClientID%>");
        function CheckFUpload() {
            span1 = "Span Show 1";
            var y = "";
            var x = document.getElementById("txtCheck").innerText = span1;
            //return false;
            if (x == "") {
                document.getElementById("txtCheck").innerText = "IF visibility";
                //txtc.innerText = 'alertB.style.visibility';
                //alertB.style.visibility = "visible";
                y = "false";
            }
            else {
                document.getElementById("txtCheck").innerText = "Else visibility";
                txtc.innerText = "alertB";
                return y = "false";
            }
            return y;
        }

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnHide.ClientID%>").click();
                document.getElementById("fileName").innerHTML = fileUpload.value;
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="modal fade" data-backdrop="static" id="collapseExample">
        <div class="text-center">
            <div style="padding-top: 280px;">
                <span class="glyphicon glyphicon-refresh glyphicon-refresh-animate" style="color: white; font-size: 50px;"></span>
            </div>
        </div>
    </div>
    <div class="bg-info container-full">
        <div class="container">
            <div class="col-md-offset-3 col-md-6 col-lg-6">
                <div class="jumbotron">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-6 col-lg-6">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlSession" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <asp:DropDownList CssClass=" form-control" runat="server" ID="ddlExamName" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <div class="col-md-12">
                                <div class="badge">
                                    <label  style="margin-top:0">Select For Exam CG &nbsp;&nbsp;&nbsp; - &nbsp;&nbsp;&nbsp;</label>
                                    <asp:CheckBox onclick="CGChecked()" ID="CbCG" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                        <div class="col-md-12">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                    1. <i class="glyphicon glyphicon-file"></i>
                                    <%--<asp:Label Text="" ID="spanFName1" runat="server" />--%>
                                    <span class="fileinput-filename" id="spanFName1" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                    <span class="fileinput-new">Select file</span>
                                    <asp:FileUpload ID="FU1" onchange="UploadFile(this)" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                   2. <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName2" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                    <span class="fileinput-new">Select file</span>
                                    <asp:FileUpload ID="FU2" onchange="UploadFile(this)" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                   3. <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName3" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                    <span class="fileinput-new">Select file</span>
                                    <asp:FileUpload ID="FU3" onchange="UploadFile(this)" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                   4. <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName4" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                    <span class="fileinput-new">Select file</span>
                                    <asp:FileUpload Style="width: 110px" CssClass="col-md-3" ID="FU4" onchange="UploadFile(this)" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                   5. <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName5" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                    <span class="fileinput-new">Select file</span>
                                    <asp:FileUpload Style="width: 110px" CssClass="col-md-3" ID="FU5" onchange="UploadFile(this)" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="col-md-12 hidden" id="divFU6">
                                <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                    <div class="form-control" data-trigger="fileinput">
                                       6. <i class="glyphicon glyphicon-file"></i>
                                        <span class="fileinput-filename" id="spanFName6" runat="server"></span>
                                    </div>
                                    <span class="input-group-addon btn btn-default btn-file">
                                        <span class="fileinput-new">Select file</span>
                                        <asp:FileUpload Style="width: 110px" CssClass="col-md-3" ID="FU6" onchange="UploadFile(this)" runat="server" />
                                    </span>
                                </div>
                            </div>
                        <div class="col-md-12">
                                <asp:Button OnClientClick="return CheckFUpload()" CssClass="btn btn-primary"
                                    data-toggle="modal" data-target="#collapseExample"
                                    Text="Import" ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="false" />
                                <asp:Button CssClass="btn btn-danger" Text="Clear" ID="btnClear" OnClick="btnClear_Click" runat="server" />
                            <asp:Label ID="lblCountFile" runat="server" />
                        </div>
                        <div class="form-group">
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnHide" CssClass="hidden"
                                data-toggle="modal" data-target="#collapseExample"
                                runat="server" OnClick="btnHide_Click" />
                            <div class="">
                                    <div class="alert-danger form-control" visible="false" id="AlertWarning" runat="server">
                                        <span class="glyphicon glyphicon-exclamation-sign"></span>
                                        <strong>Please! </strong>Upload All Files With Select Exam & Session.
                                    </div>

                                <div class="alert-success form-control" visible="false" id="AlertSuccess" role="alert" runat="server">
                                    <span class="glyphicon glyphicon-saved"></span>
                                    <strong>Done! </strong>All Files Have Uploaded Successfully.
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalException" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <i class="glyphicon glyphicon-info-sign"></i> There is Errors in File Import.
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="text-danger">
                        <i class="glyphicon glyphicon-warning-sign"></i>&nbsp;&nbsp;
                        <asp:Label CssClass="" ID="lblException" runat="server" ></asp:Label>
                    </div>
                </div>
                <div></div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConfirmation" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class=" modal-header">
                    <button class="close" data-dismiss="modal" ><i class="fa fa-close"></i></button>
                    <h5 class="modal-title">Confirmation !</h5>
                </div>
                <div class="modal-body" >
                    <h6>Data Successfully Import. Do You Want To Merge Data</h6>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton CssClass="btn btn-primary" ID="btnYes" OnClientClick="ShowSpin()" OnClick="btnYes_Click" Text="Yes"  runat="server">Yes <i class="fa fa-spinner fa-spin hidden"></i></asp:LinkButton>
                    <%--<asp:Button CssClass="btn btn-primary" ID="btnYes" OnClick="btnYes_Click" Text="Yes" runat="server" />--%>
                    <asp:Button CssClass="btn btn-danger" ID="btnNo" OnClick="btnNo_Click" Text="NO" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <script src="js1/bootstrap.min.js"></script>
    <script type="text/javascript">
        function ShowSpin() {
            $(".fa-spin").removeClass("hidden");
        }
        function ShowException() {
            $("#modalException").modal();
        }
        function ShowConfirmation() {
            $("#modalConfirmation").modal();
        }
        $(document).ready(function () {
            CGChecked();
        });
        function CGChecked() {
            var e = document.getElementById('<%=CbCG.ClientID %>');
            if(e.checked==true){
                $("#divFU6").removeClass("hidden");
            }
            else {
                $("#divFU6").addClass("hidden");
            }
        }
    
    </script>

</asp:Content>
