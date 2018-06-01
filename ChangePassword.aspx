<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Change Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />
    <script src="bootstrap-3.3.6-dist/js/jquery.min.js"></script>
    <script src="bootstrap-3.3.6-dist/js/bootstrap.min.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery-1.7.1.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery.ui.draggable.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery.alerts.js"></script>

    <script type="text/ecmascript">
        function ValidateChangePwd() {
            if ($('#<%=txtOldPwd.ClientID%>').val() == '') {
                $('#<%=txtOldPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Old Password.', 'Amravati University(WMS)', function () { $('#<%=txtOldPwd.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtOldPwd.ClientID%>').css('border-color', ''); } if ($('#<%=txtNewPwd.ClientID%>').val() == '') {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Enter New Password.', 'Amravati University(WMS)', function () { $('#<%=txtNewPwd.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtNewPwd.ClientID%>').css('border-color', ''); }

            if ($('#<%=txtConfirmPwd.ClientID%>').val() == '') {
                $('#<%=txtConfirmPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Enter Confirm Password.', 'Amravati University(WMS)', function () { $('#<%=txtConfirmPwd.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtConfirmPwd.ClientID%>').css('border-color', ''); }

            if ($('#<%=txtConfirmPwd.ClientID%>').val() != $('#<%=txtNewPwd.ClientID%>').val()) {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', 'Red');
                jAlert('New Password And Confirm Password Are Not Matched. Please Try Again.', 'Amravati University(WMS)', function () { $('#<%=txtNewPwd.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtNewPwd.ClientID%>').css('border-color', ''); }

            if ($('#<%=txtNewPwd.ClientID%>').val == $('#<%=txtOldPwd.ClientID%>').val()) {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Old Password And New Password Should Not Be Same.', 'Amravati University(WMS)', function () { $('#<%=txtNewPwd.ClientID%>').focus() })
                return false;
            }
            else { $('#<%=txtNewPwd.ClientID %>').css('border-color', ''); }
        }
    </script>
    <style type="text/css">
   .MyFadeModal
    {    
	position: fixed;
	top: 0;
	right: 0;
	bottom: 0;
	left: 0;
	z-index: 1050;	
	overflow: hidden;     
	-webkit-overflow-scrolling: touch;
        nav-up:auto;
	outline: 0; 
        background-color: rgba(128, 128, 128, 0.7);
       
     }        
</style>
    <script src="js/1.12.4.jquery.min.js"></script>

    <asp:UpdatePanel runat="server" ID="updatepanelExamMaster">
        <ContentTemplate>
            <div class="MyFadeModal" id="myModal" runat="server" visible="true" >



            <div class="row">
                <div class="col-md-2 col-lg-3"></div>
                <div class="col-md-8 col-lg-6">
                    <div class="change-password">
        
                <%--  <table>
                      <tr>
                          <td style="width:90%">
                               <h3 class="text-center">Change Password</h3>

                          </td>
                           <td style="width:10%">
 <span class="glyphicon glyphicon-remove"></span>
                          </td>
                      </tr>
                  </table>--%>
                  <div class="form-group">
                      <div class="col-md-11"><div><h3 class="text-center">Change Password</h3></div></div>
                      <div class="col-md-1"><div class="pull-right">
                          <asp:LinkButton runat="server" ID="btnClose" OnClick="btnClose_Click" ValidationGroup="Close" ><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                          </div></div>
                  </div>
           
                    
                      

                        <div id="divremark" runat="server">
                            <h5 class="text-center text-warning">You are Logged-In First Time so for Security Reason Please Change Your Password</h5>
                        </div>
                        <hr class="changecolor" />
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">Old Password &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-4 col-lg-4">
                                <asp:TextBox ID="txtOldPwd" ValidationGroup="s" CssClass="form-control  " runat="server" TabIndex="1" PlaceHolder="Enter Old Password" TextMode="Password" MaxLength="8">
                                </asp:TextBox>

                                <asp:RequiredFieldValidator ID="sampleRFV" ControlToValidate="txtOldPwd" runat="server" CssClass="text-danger"
                                    ErrorMessage="Please Enter Old Password" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-4 col-lg-4">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOldPwd" Display="Dynamic"
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ErrorMessage=" Password must have at least One Alphabet ,One Number,One Special Character & 8 digits only." ForeColor="Red" />

                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">New Password &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-4 col-lg-4">
                                <asp:TextBox ID="txtNewPwd" ValidationGroup="s" CssClass="form-control" PlaceHolder="Enter New Password"
                                    runat="server" TabIndex="2" TextMode="Password" MaxLength="8">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger"
                                    ErrorMessage="Please Enter New Password !" Display="Dynamic" SetFocusOnError="True"
                                    ControlToValidate="txtNewPwd"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-4 col-lg-4">
                                <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtNewPwd" Display="Dynamic"
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ErrorMessage=" Password must have at least One Alphabet ,One Number,One Special Character & 8 digits only." ForeColor="Red" />


                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">Confirm Password &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-4 col-lg-4">
                                <asp:TextBox ID="txtConfirmPwd" ValidationGroup="s" CssClass="form-control" PlaceHolder="Enter Confirm Password"
                                    runat="server" TabIndex="3" TextMode="Password" MaxLength="8">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Please Confirm Password !" ControlToValidate="txtConfirmPwd"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-4 col-lg-4">
                                <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtNewPwd" ControlToValidate="txtConfirmPwd" CssClass="text-danger" runat="server" ErrorMessage="Password Not Match."></asp:CompareValidator>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-4 col-lg-4">
                                <label class="text-warning" style="font-size: 14px;"><span style="color: Red; font-size: 15px;">*&nbsp;</span>Fields Are Mandatory.</label>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="center-block">
                                    <asp:LinkButton ID="linkSave" ValidationGroup="s" class="btn btn-primary" runat="server" TabIndex="4" OnClick="linkSave_Click"
                                        OnClientClick="return ValidateChangePwd();"><i class="fa fa-save"></i>&nbsp;Change
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="linkCancel" class="btn btn-primary" ValidationGroup="k" runat="server" TabIndex="5" OnClick="linkCancel_Click">
                                        <i class="fa fa-close"></i> &nbsp;Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label>
                        <hr class="changecolor" />
                        <div class="form-group text-center">
                            <p class="text-warning col-md-10 col-lg-10" style="font-weight: 600; font-size: 11px;">
                                <span style="color: red; font-size: 16px;">*&nbsp;</span>
                                <%--Password having atleast 1 Alphabet,1 Number, 1 Special Character And must be 8 digit Only.--%>
                                Password must have at least One Alphabet ,One Number,One Special Character & 8 digits only.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-md-2 col-lg-3"></div>
</asp:Content>
