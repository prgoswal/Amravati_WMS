<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Async="true" Inherits="UserLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Amravati University</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap core CSS -->
    <link href="bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap theme -->
    <link href="bootstrap-3.3.6-dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="bootstrap-3.3.6-dist/css/mycss.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/header.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap-3.3.6-dist/js/jquery-1.11.3.min.js" type="text/javascript"></script>

    <style type="text/css">
        span:hover {
            cursor: pointer;
        }
    </style>
    <%--    <script type="text/javascript" id="MyKey">
        var myWindow;
        function windowOpen()
        {
            myWindow = window.open('http://localhost:52033/Home.aspx', '_blank', 'width=1500,height=800, scrollbars=no,resizable=no')
            myWindow.focus()
            return false;
        }
    </script>--%>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        function ShowPopupPrint() {
            $('#mask').show();
            $('#<%=pnlprint.ClientID %>').show();
        }
        function HidePopupPrint() {
            $('#mask').hide();
            $('#<%=pnlprint.ClientID %>').hide();
        }
        //$(".btnClose11").live('Click', function () {
        //    HidePopupPrint();
        //});
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.name = Date();
            $('#<%=hfvalue.ClientID%>').val(window.name);
           });
    </script>
    <script type="text/javascript">
        //Initialize tooltip with jQuery
        $(document).ready(function () {
            $('.tooltips').tooltip();
        });
    </script>
    <script type="text/javascript" id="k">
        function alert() {
            sweetAlert("Oops...", "Something went wrong!", "error");
        }
    </script>
    <style>
        .left-inner-addon {
            position: relative;
        }

            .left-inner-addon input {
                padding-left: 30px;
            }

            .left-inner-addon i {
                position: absolute;
                padding: 10px 12px;
                pointer-events: none;
            }

        .right-inner-addon {
            position: relative;
        }

            .right-inner-addon input {
                padding-right: 30px;
            }

            .right-inner-addon i {
                position: absolute;
                right: 0px;
                padding: 10px 12px;
                pointer-events: none;
            }
    </style>

    <%-- End Area --%>
</head>
<body style="overflow-x: hidden; font-family: Calibri;">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="page-header">
            <h3 class="header-title text-center" style="font-family: Calibri; padding: 11px 15px 0 0px !important;">Sant Gadge Baba Amravati University
                <br />
                <span style="font-size: 29px;">WorkFlow Management System</span>
            </h3>
        </div>
        <div class="navbar navbar-default" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".menu2">
                        <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span>
                        <span class="icon-bar"></span><span class="icon-bar"></span>
                    </button>
                </div>
                <div class="navbar-collapse collapse menu2">
                    <ul class="nav navbar-nav navbar-right"></ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
        <div class="wrapper form-signin">
            <div class="container">
                <div class="card card-container">
                    <img id="profile-img" class="profile-img-card" src="bootstrap-3.3.6-dist/images/logo.png" />
                    <!-- <img class="profile-img-card" src="//lh3.googleusercontent.com/-6V8xOA6M7BA/AAAAAAAAAAI/AAAAAAAAAAA/rzlHcD0KYwo/photo.jpg?sz=120" alt="" /> -->
                    <asp:Panel ID="pnlusernmae" runat="server">

                        <%--         <script>
                                  function myfunction() {
                                      //var i = document.getElementById("pass").innerHTML
                                      var chk = document.getElementById("pass").type
                                      if (chk == "password") {
                                          document.getElementById("pass").type = "text"
                                      }
                                      else {
                                          document.getElementById("pass").type = "password"
                                      }
                                  }
                    </script>--%>
                        <script type="text/javascript">

                            $(document).ready(function () {
                                $("#btnHideShowUsrNm").click(function () {

                                    if ($("#txtLoginId").attr("type") == "password") {
                                        $('#s').removeClass("glyphicon-eye-close");
                                        $('#s').addClass("glyphicon-eye-open");
                                        $("#txtLoginId").attr("type", "text");
                                    }
                                    else {
                                        $('#s').removeClass("glyphicon-eye-open");
                                        $('#s').addClass("glyphicon-eye-close");
                                        $("#txtLoginId").attr("type", "password");
                                    }
                                });
                            });


                            $(function () {
                                $('[data-toggle="tooltip"]').tooltip()
                            });

                        </script>
                        <div>
                            <asp:TextBox ID="txtLoginId" class="form-control floatLabel" Style="width: 100%; text-transform: uppercase" placeholder="Enter Login ID" data-toggle="tooltip" data-placement="top"
                                runat="server" MaxLength="8" AutoCompleteType="Disabled">                         
                            </asp:TextBox>
                            <%--<input type="password" class="form-control" placeholder="Enter User Name" maxlength="20" autocomplete="off" />--%>
                            <%--This is login id show and hide
                            <span class="input-group-btn">
                                <button class="btn btn-default hide_pwd" type="button" id="btnHideShowUsrNm">
                                    <span class="glyphicon glyphicon-eye-open tooltips" id="s" data-placement="top" title="Hide & Show UserName"></span>
                                </button>
                            </span>--%>
                        </div>
                        <asp:Button ID="btnnext" class="btn btn-lg btn-primary btn-block btn-signin " runat="server" Text="Next" OnClick="btnnext_Click" />
                    </asp:Panel>
                    <%--<asp:LinkButton ID="lnk" runat="server" OnClientClick="return showMacAddress();" Text="Mac"></asp:LinkButton>--%>
                    <asp:Panel ID="pnlpassword" runat="server">
                        <h4 style="font-family: Arial; text-align: center;">
                            <asp:Label ID="lbluser" CssClass="label label-primary" runat="server"></asp:Label>
                        </h4>
                        <div class="input-group">
                            <asp:TextBox ID="txtLoginPwd" Style="height: 34px;" class="inputPassword form-control input-group tooltips" placeholder="Enter Password" data-placement="top" title="Please Enter Password" ValidationGroup="s"
                                TextMode="Password" runat="server" required="" MaxLength="8"></asp:TextBox>
                            <span class="input-group-btn">
                                <button class="btn btn-default" type="button" id="btnHideShowUsrNm1">
                                    <span class="glyphicon glyphicon-eye-close tooltips" id="s1" data-placement="top" title="Show Password"></span>
                                </button>
                            </span>
                        </div>
                        <asp:Button ID="btnLogin" class="btn btn-lg btn-primary btn-block btn-signin" runat="server" ValidationGroup="s"
                            Text="Login" OnClick="btnLogin_Click" />
                        <br />
                        <asp:LinkButton ID="linklogin" runat="server" Text="Sign in with a different account" OnClick="linklogin_Click"></asp:LinkButton>
                    </asp:Panel>
                    <asp:Panel ID="pnllogout" runat="server">
                        <div class=" form-horizontal">
                            <div class="form-group">
                                <h4 class="bg-info" style="font-family: Calibri">Welcome :
                                    <asp:Label ID="lbllogout" runat="server"></asp:Label>
                                </h4>
                                <asp:TextBox ID="txtlogoutpassword" TextMode="Password" Style="height: 34px;" runat="server" MaxLength="8" CssClass="form-control tooltips"
                                    data-placement="top" title="Please Enter Password" placeholder="Enter Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnlogout" CssClass="btn btn-lg btn-primary btn-block btn-signin" class="" runat="server" Text="Logout" OnClick="btnlogout_Click" />
                                <span class="bg-warning">Yor Are Already Login Please Logout First.</span>
                                <br />
                                <asp:LinkButton ID="linklogout" runat="server" Text="Sign in with a different account" OnClick="linklogout_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Label ID="lblmsg1" runat="server" Text="" CssClass="text-danger" Style="font-size: 12px;"></asp:Label>
                    <div>
                        <asp:Panel ID="pnlprint" runat="server" BackColor="#50618C" Width="50%" Height="390px"
                            Style="z-index: 111; margin-top: -45px; position: absolute; left: 25%; top: 25%; border-radius: 10px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); border: outset 2px gray; padding: 5px; display: none">
                            <div id="divprint" class="form-horizontal" style="background-color: white; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); margin: 12%; height: 50%; width: 76%; padding: 2%; border-radius: 15px;">
                                <div class="col-md-4" style="height: 100%">
                                    <img src="images/p3.png" style="min-height: 100%" class="img-responsive" />
                                </div>
                                <div>
                                    <table>
                                        <tr class="form-group">
                                            <td class="col-md-8">
                                                <asp:TextBox ID="txtNewPwd" CssClass="form-control" PlaceHolder="Enter New Password" runat="server" TabIndex="2"
                                                    TextMode="Password" MaxLength="8">
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNewPwd"
                                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                                    ErrorMessage="Must Be 8 Characters" ForeColor="Red" />
                                            </td>
                                        </tr>
                                        <tr class="form-group">
                                            <td class="col-md-8">
                                                <asp:TextBox ID="txtConfirmPwd" CssClass="form-control" PlaceHolder="Enter Confirm Password" runat="server" TabIndex="3"
                                                    TextMode="Password" MaxLength="8">
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConfirmPwd"
                                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                                    ErrorMessage="Must Be 8 Characters" ForeColor="Red" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPwd" CssClass="ValidationError"
                                                    ControlToCompare="txtNewPwd" ErrorMessage="No Match" ToolTip="Password must be the same" />
                                            </td>
                                        </tr>
                                        <tr class="form-group">
                                            <td class="col-md-8">
                                                <asp:Button ID="btnsavepassword" class="btn btn-lg btn-primary btn-block" runat="server" Text="SAVE" OnClick="btnsavepassword_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <!-- /form -->
                    <!-- /card-container -->
                    <!-- /container -->
                </div>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#btnHideShowUsrNm1").click(function () {
                            if ($("#txtLoginPwd").attr("type") == "password") {
                                $('#s1').removeClass("glyphicon-eye-close");
                                $('#s1').addClass("glyphicon-eye-open");
                                $("#txtLoginPwd").attr("type", "text");
                            }
                            else {
                                $('#s1').removeClass("glyphicon-eye-open");
                                $('#s1').addClass("glyphicon-eye-close");
                                $("#txtLoginPwd").attr("type", "password");
                            }
                        });
                    });
                </script>
                <script type="text/javascript">
                    //$(document).ready(function ()
                    //{
                    //    $('input[placeholder]').placeholderLabel();
                    //})
                </script>
                <asp:HiddenField ID="hfvalue" runat="server" />
            </div>
        </div>
    </form>
</body>
<%--<script language="javascript" type="text/javascript">
        function showMacAddress() {
            var obj = new ActiveXObject("WbemScripting.SWbemLocator");
            var s = obj.ConnectServer(".");
            var properties = s.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
            var e = new Enumerator(properties);
            var output;
            output = '<table border="0" cellPadding="5px" cellSpacing="1px" bgColor="#CCCCCC">';
            output = output + '<tr bgColor="#EAEAEA"><td>Caption</td><td>MACAddress</td></tr>';
            while (!e.atEnd()) {
                e.moveNext();
                var p = e.item();
                if (!p) continue;
                output = output + '<tr bgColor="#FFFFFF">';
                output = output + '<td>' + p.Caption; +'</td>';
                output = output + '<td>' + p.MACAddress + '</td>';
                output = output + '</tr>';
            }
            output = output + '</table>';
           
        }
</script>--%>
<script src="bootstrap-3.3.6-dist/js/jquery.min.js" type="text/javascript"></script>
<script src="bootstrap-3.3.6-dist/js/jquery-1.10.2.js" type="text/javascript"></script>
<script src="bootstrap-3.3.6-dist/js/bootstrap.min.js" type="text/javascript"></script>
<script src="bootstrap-3.3.6-dist/js/script.js" type="text/javascript"></script>
</html>
