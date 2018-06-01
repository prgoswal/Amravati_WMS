<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmFileUpload.aspx.cs" Inherits="FrmFileUpload" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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

         
    <script type="text/javascript">
        <%--var msg = document.getElementById("<%=lblMsg.ClientID%>").innerHTML--%>
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnHide.ClientID%>").click();
                <%--document.getElementById("<%=lblMsg.ClientID%>").innerText = "Success";--%>  
                document.getElementById("fileName").innerHTML = fileUpload.value;
                <%-- document.activeElement("<%= SaveTemp("  karthick  ") %>");--%>
                <%--document.getElementById("<%=lblMsg.ClientID%>").innerHTML = "<%=SaveTemp()%>"--%>
                }
        }
    </script>
    <script type="text/javascript">
        function btnAccept_onclick() {
            var name;
            name = "Manoj";// document.getElementById('txtName').value;
            PageMethods.SetName(name, onSuccess, onFailure);
            function onSuccess(result) {
                alert(name);
            }

            function onFailure(error) {
                alert(error);
            }

        }
    </script>

    <style>
        .jumbotron{
            margin-top:40px;                        
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color:white;  
        }
    </style>
     <style>
        .container-full {
            margin-left: -15px;
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
           
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
            var y="";
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
    </script>


    </asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
   
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>

        <div class="modal fade" data-backdrop="static" id="collapseExample">
            <div class="text-center">
                <div style="padding-top: 280px;">
                    <span class="glyphicon glyphicon-refresh glyphicon-refresh-animate" style="color: white; font-size: 50px;"></span>
                </div>
            </div>
        </div>
      <div class="bg-info">
   <%-- <div class="modal-content">--%>
       

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
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                    <i class="glyphicon glyphicon-file"></i>
                                    <%--<asp:Label Text="" ID="spanFName1" runat="server" />--%>
                                    <span class="fileinput-filename" id="spanFName1" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                <span class="fileinput-new">Select file</span>
                                <asp:FileUpload ID="FU1" onchange="UploadFile(this)" runat="server" />
                                </span>                                
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                    <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName2" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                <span class="fileinput-new">Select file</span>
                                <asp:FileUpload ID="FU2" onchange="UploadFile(this)" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                    <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName3"  runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                <span class="fileinput-new">Select file</span>
                            <asp:FileUpload ID="FU3" onchange="UploadFile(this)" runat="server" />
                            </span>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                <div class="form-control" data-trigger="fileinput">
                                    <i class="glyphicon glyphicon-file"></i>
                                    <span class="fileinput-filename" id="spanFName4" runat="server"></span>
                                </div>
                                <span class="input-group-addon btn btn-default btn-file">
                                <span class="fileinput-new">Select file</span>
                            <asp:FileUpload Style="width: 110px" CssClass="col-md-3" ID="FU4" onchange="UploadFile(this)" runat="server" />
                            </span>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="">
                                <asp:Button  OnClientClick="return CheckFUpload()" CssClass="btn btn-primary" 
                                    data-toggle="modal" data-target="#collapseExample"
                                    Text="Save" ID="btnSave" OnClick="btnSave_Click" runat="server" />
                            </div>
                        </div>
                        <div class="">
                            <asp:Button ID="btnHide" CssClass="hidden"
                                data-toggle="modal" data-target="#collapseExample"
                                runat="server" OnClick="btnHide_Click" />
                            <div class="">
                                <div class="form-group">
                                    <div class="alert-danger form-control" visible="false" id="AlertWarning" runat="server">
                                        <span class="glyphicon glyphicon-exclamation-sign"></span>
                                        <strong>Please! </strong>Upload All Files With Select Exam & Session.
                                    </div>
                                </div>
                               
                                <div class="alert-success form-control" visible="false" id="AlertSuccess" role="alert" runat="server">
                                        <span class="glyphicon glyphicon-saved"></span>
                                        <strong>Done! </strong>All Files Have Uploaded Successfully.
                                    </div>                                
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<p id="txtCheck"></p>--%>
                </div>
                </div>
             </div>  
    <%--</div>--%>
        <%--</div>--%>
</asp:Content>
