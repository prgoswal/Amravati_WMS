<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" Async="true"
    CodeFile="UserCreation.aspx.cs" Inherits="UserCreation" Title="Amravati University" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />
    <style type="text/css">
        .cal .ajax__calendar_header {
            background-color: white;
        }

        .cal .ajax__calendar_container {
            background-color: #bfb8b8;
        }

        .custom-width {
            width: 34.333333% !important;
        }
    </style>
    <script>
        // A $( document ).ready() block.
        $(document).ready(function () {
            //Attach key up event handler
            $('#<%= txt1.ClientID %>').keyup(function () {
                //check if user typed three characters
                if (this.value.length == $(this).attr('maxlength')) {
                    //move the focus to another textbox
                    $('#<%= txt2.ClientID %>').focus();
                }
            });
            $('#<%= txt2.ClientID %>').keyup(function () {
                if (this.value.length == $(this).attr('maxlength')) {
                    $('#<%= txt3.ClientID %>').focus();
                }
            });
            $('#<%= txt3.ClientID %>').keyup(function () {
                if (this.value.length == $(this).attr('maxlength')) {
                    $('#<%= txt4.ClientID %>').focus();
                }
            });
            $('#<%= txt4.ClientID %>').keyup(function () {
                if (this.value.length == $(this).attr('maxlength')) {
                    $('#<%= txt5.ClientID %>').focus();
                }
            });
            $('#<%= txt5.ClientID %>').keyup(function () {
                if (this.value.length == $(this).attr('maxlength')) {
                    $('#<%= txt6.ClientID %>').focus();
                }
            });
        });
    </script>

    <script type="text/javascript" language="javascript">
        function ValidateUserDetails() {

            if ($('#<%=ddlUserType.ClientID%>').val() == 0) {
                $('#<%=ddlUserType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select User Type.', 'Error', function () { $('#<%=ddlUserType.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=ddlUserType.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtUserName.ClientID%>').val() == '') {
                $('#<%=txtUserName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User Name.', 'Error', function () { $('#<%=txtUserName.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txtUserName.ClientID%>').css('border-color', '');
            }
       <%--     if ($('#<%=txtUserState.ClientID%>').val() == '') {
                $('#<%=txtUserState.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User State', 'Error', function () { $("#txtUserState").focus() })
                return false;
            }
            else {
                $('#<%=txtUserState.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtUserCity.ClientID%>').val() == '') {
                $('#<%=txtUserCity.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User City', 'Error', function () { $("#txtUserCity").focus() })
                return false;
            }
            else {
                $('#<%=txtUserCity.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtEmailAddress.ClientID%>').val() == '') {
                $('#<%=txtEmailAddress.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Email Address', 'Error', function () { $("#txtEmailAddress").focus() })
                return false;
            }
            else {
                $('#<%=txtEmailAddress.ClientID%>').css('border-color', '');
            }--%>
            if ($('#<%=txtContactNo.ClientID%>').val() == '') {
                $('#<%=txtContactNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Mobile No.', 'Error', function () { $('#<%=txtContactNo.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txtContactNo.ClientID%>').css('border-color', '');
            }
          <%--  if ($('#<%=txtAddress.ClientID%>').val() == '') {
                $('#<%=txtAddress.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User Address', 'Error', function () { $("#txtAddress").focus() })
                return false;
            }
            else {
                $('#<%=txtAddress.ClientID%>').css('border-color', '');
            }--%>
            if ($('#<%=txtLoginId.ClientID%>').val() == '') {
                $('#<%=txtLoginId.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Login-ID', 'Error', function () { $('#<%=txtLoginId.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txtLoginId.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txt1.ClientID%>').val() == '') {
                $('#<%=txt1.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter MAC Address', 'Error', function () { $('#<%=txt1.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txt2.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txt2.ClientID%>').val() == '') {
                $('#<%=txt2.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter MAC Address', 'Error', function () { $('#<%=txt2.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txt2.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txt3.ClientID%>').val() == '') {
                $('#<%=txt3.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter MAC Address', 'Error', function () { $('#<%=txt3.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txt3.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txt4.ClientID%>').val() == '') {
                $('#<%=txt4.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter MAC Address', 'Error', function () { $('#<%=txt4.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txt4.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txt5.ClientID%>').val() == '') {
                $('#<%=txt5.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter MAC Address', 'Error', function () { $('#<%=txt5.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txt5.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txt6.ClientID%>').val() == '') {
                $('#<%=txt6.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter MAC Address', 'Error', function () { $('#<%=txt6.ClientID%>').focus() })
                return false;
            }
            else {
                $('#<%=txt6.ClientID%>').css('border-color', '');
            }
        }
    </script>
    <%-- <script type="text/javascript">
     var email = document.getElementById('txtEmailAddress');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please provide a valid email address');
                email.focus;
                return false;
            }
    </script>--%>

    <div class="row">
        <div class="col-md-1 col-lg-2">
        </div>

        <div class="col-md-10 col-lg-8">
            <div class="user-creation" style="height: 100%; position: relative;">
                <h3 class="text-center" style="font-family: Calibri;">Create Users</h3>
                <hr class="changecolor" />
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row " style="font-family: Arial">
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4 custom-width">
                                        User Profile &nbsp;<span style="color: Red">*</span></label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:DropDownList ID="ddlUserType" CssClass="form-control input-sm" runat="server"
                                            TabIndex="1" Font-Bold="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4 custom-width">
                                        Email Address
                                    </label>
                                    <div class="col-md-6 col-lg-6">

                                        <asp:TextBox ID="txtEmailAddress" CssClass="form-control input-sm" runat="server"
                                            MaxLength="33" TabIndex="2"></asp:TextBox>

                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorEmail" ControlToValidate="txtEmailAddress"
                                            ErrorMessage="Not Valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Style="position: absolute; right: -46px; top: 4px;">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4 custom-width">
                                        Full Name &nbsp;<span style="color: Red">*</span></label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control input-sm" runat="server" MaxLength="33"
                                            TabIndex="3" Style="text-transform: uppercase"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUserName"
                                            ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6" style="margin-bottom: -20px;">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4 custom-width">
                                        Mobile No. &nbsp;<span style="color: Red">*</span></label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:TextBox ID="txtContactNo" CssClass="form-control input-sm" runat="server" MaxLength="10"
                                            TabIndex="4" ValidationGroup="s"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactNo"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtContactNo"
                                            ID="RegularExpressionValidator1" ValidationExpression="^[789]\d{9}$" runat="server"
                                            ErrorMessage="Enter 10 Digit Start with 7 or 8 or 9" ForeColor="Red"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="reqcontact" ValidationGroup="s" runat="server" ErrorMessage="Please enter MobileNo." ControlToValidate="txtContactNo">

                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <%--        <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4 custom-width">
                                State &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtUserState" CssClass="form-control input-sm" runat="server" MaxLength="28"
                                    TabIndex="5"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtUserState"
                                    ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ. ">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4 custom-width">
                                City &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtUserCity" CssClass="form-control input-sm" runat="server" MaxLength="28"
                                    TabIndex="6"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtUserCity"
                                    ValidChars="abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ. ">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4 custom-width">
                                Address &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtAddress" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Height="70px" Width="210px"
                                    MaxLength="148" TabIndex="7"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>


                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4 custom-width">
                                        Login ID &nbsp;<span style="color: Red">*</span></label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:TextBox ID="txtLoginId" CssClass="form-control input-sm" runat="server" MaxLength="8" placeholder="(Alpha-Numeric 8 Digit)"
                                            TabIndex="5" Style="text-transform: uppercase"></asp:TextBox>

                                    </div>
                                    <div class="col-md-6 col-lg-6 custom-width">
                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtLoginId"
                                            ID="RegularExpressionValidator2" ValidationExpression="^[a-zA-Z0-9]{8}$" runat="server"
                                            ErrorMessage="8 Characters In Alpha/Numbers/Both." ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6 col-lg-6">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <label class="col-md-3 col-lg-3 custom-width">
                                                MAC Address &nbsp;<span style="color: Red">*</span></label>
                                            <div class="col-md-7 col-lg-7">
                                                <asp:TextBox ID="txt1" runat="server" MaxLength="2" OnTextChanged="txt1_TextChanged"
                                                    TabIndex="6" Style="width: 22px; text-transform: uppercase"></asp:TextBox>-
                                  <asp:TextBox ID="txt2" Style="width: 22px; text-transform: uppercase" runat="server" MaxLength="2"
                                      TabIndex="7"></asp:TextBox>-
                                  <asp:TextBox ID="txt3" Style="width: 22px; text-transform: uppercase" runat="server" MaxLength="2"
                                      TabIndex="8"></asp:TextBox>-
                                  <asp:TextBox ID="txt4" Style="width: 22px; text-transform: uppercase" runat="server" MaxLength="2"
                                      TabIndex="9"></asp:TextBox>-
                                  <asp:TextBox ID="txt5" Style="width: 22px; text-transform: uppercase" runat="server" MaxLength="2"
                                      TabIndex="10"></asp:TextBox>-
                                  <asp:TextBox ID="txt6" Style="width: 22px; text-transform: uppercase" runat="server" MaxLength="2"
                                      TabIndex="11"></asp:TextBox>

                                                <asp:LinkButton ID="lnkmac" runat="server" Text="Generate MAC-Address" OnClick="lnkmac_Click"></asp:LinkButton>

                                                <%--<asp:RequiredFieldValidator ID="reqmacaddress" runat="server" ControlToValidate="txtmacaddress" ErrorMessage="Please enter MacAddress"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4">
                                        Date Of Expiry
                                    </label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:DropDownList ID="ddluservalidity" runat="server" Width="100%" Font-Bold="true" CssClass="form-control input-sm" TabIndex="12">
                                            <asp:ListItem>No Expiry</asp:ListItem>
                                            <asp:ListItem>7 Days</asp:ListItem>
                                            <asp:ListItem>15 Days</asp:ListItem>
                                            <asp:ListItem>1 Month</asp:ListItem>
                                            <asp:ListItem>3 Month</asp:ListItem>
                                            <asp:ListItem>6 Month</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4">
                                        Employee ID
                                    </label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:TextBox ID="txtempID" CssClass="form-control input-sm" runat="server" MaxLength="8" TabIndex="13"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtempID"
                                            ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12 col-lg-12">
                                <div class="form-group text-center">
                                    <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server" OnClientClick="return ValidateUserDetails()"
                                        OnClick="linkSave_Click" TabIndex="16"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
                                    <asp:LinkButton ID="linkClear" class="btn btn-primary" runat="server" TabIndex="17"
                                        OnClick="linkClear_Click" ValidationGroup="xyz"><i class="fa fa-close"></i> &nbsp;Clear</asp:LinkButton>
                                    <br />
                                    <asp:Label runat="server" ID="lblmsg" ForeColor="Red" Text=""></asp:Label>
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-md-12 col-lg-12" style="margin-top: 20px;">
                        <div class="form-group text-center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 34%">
                                        <asp:LinkButton ID="linkall" CssClass="bg-info" OnClick="linkall_Click" runat="server" Style="font-weight: bold; font-family: Calibri" Text="Show All Users" ValidationGroup="k"></asp:LinkButton>
                                    </td>
                                    <td style="width: 33%">
                                        <asp:LinkButton ID="link2day" CssClass="bg-info" runat="server" Style="font-weight: bold; font-family: Calibri" Text="User-Login Expires in 2 Days" OnClick="link2day_Click" ValidationGroup="k"></asp:LinkButton>
                                    </td>
                                    <td style="width: 33%">
                                        <asp:LinkButton ID="link7day" CssClass="bg-info" runat="server" Style="font-weight: bold; font-family: Calibri" Text="User-Login Expires in 7 Days" OnClick="link7day_Click" ValidationGroup="k"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                    <%--<asp:Panel ID="pnlgrid" runat="server" ScrollBars="Vertical" Height="150px" style="margin-top:10px; overflow:auto">--%>

                    <div id="divtable" runat="server">
                        <table style="width: 100%; margin-top: 10px;">
                            <tr>
                                <%--<td style="width: 13%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Profile Name</td>
                            <td style="width: 8%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">User Name</td>
                            <td style="width: 9%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Login-ID</td>
                            <td style="width: 18%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Employee ID</td>
                            <td style="width: 9%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Expiry Date</td>
                            <td style="width: 5%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Action </td>--%>

                                <td style="width: 20%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Profile Name</td>
                                <td style="width: 20%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">User Name</td>
                                <td style="width: 12%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Login-ID</td>
                                <td style="width: 10%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Password</td>
                                <td style="width: 15%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Employee ID</td>
                                <td style="width: 15%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Expiry Date</td>
                                <td style="width: 8%; font-size: 12pt; background-color: #175080; color: white; font-family: Calibri">Action </td>
                            </tr>
                        </table>
                        <div style="overflow: auto; width: 100%; height: 300px;">
                            <asp:GridView ID="grddata" class="table table-bordered" runat="server" ShowHeader="false" AutoGenerateColumns="false" DataKeyNames="UserID" OnRowCommand="grddata_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="UserType" ItemStyle-Width="20%" HeaderText="Profile Name" />
                                    <asp:BoundField DataField="UserName" ItemStyle-Width="20%" HeaderText="User Name" />
                                    <asp:BoundField DataField="UserLoginID" ItemStyle-Width="12%" HeaderText="Login-ID" />
                                    <asp:BoundField DataField="UserPWD" ItemStyle-Width="10%" HeaderText="Password" />
                                    <asp:BoundField DataField="EmployeeID" ItemStyle-Width="15%" HeaderText="Employee ID" />
                                    <asp:BoundField DataField="UserValidity" DataFormatString="{0:dd-M-yyyy}" ItemStyle-Width="15%" HeaderText="Expiry Date" />
                                    <%-- 	<asp:TemplateField>
                                <ItemTemplate >
                                    <asp:LinkButton ID="LinkButtonPwd" runat="server" ValidationGroup="1" CommandArgument='<%#Eval("userID") %>'
                                        CommandName="ShowData" style="text-align:right">Show</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" ValidationGroup="1" CommandArgument='<%#Eval("userID") %>'
                                                CommandName="EditData" Style="text-align: right">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <%-- <div id="div1" runat="server" style="overflow: auto; width: 100%;">
                    <asp:GridView ID="grddata" class="table table-bordered" runat="server" ShowHeader="false" AutoGenerateColumns="false" DataKeyNames="UserID" OnRowCommand="grddata_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="UserType" ItemStyle-Width="30%" HeaderText="Profile Name"/>
                            <asp:BoundField DataField="UserName" ItemStyle-Width="20%" HeaderText="User Name"/>
                            <asp:BoundField DataField="UserLoginID" ItemStyle-Width="12%" HeaderText="Login-ID"/>
                            <asp:BoundField DataField="EmployeeID" ItemStyle-Width="15%" HeaderText="Employee ID"/>
                            <asp:BoundField DataField="UserValidity" DataFormatString="{0:dd-M-yyyy}" ItemStyle-Width="15%" HeaderText="Expiry Date"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" ValidationGroup="1" CommandArgument='<%#Eval("userID") %>'
                                        CommandName="EditData">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div></asp:Panel><hr />--%>
            <hr class="changecolor" style="margin-top: 55px;" />
            <p class="" style="font-weight: 600; font-family: Calibri;">
                <span style="color: Red; font-size: 15px; text-align: center;">*&nbsp;</span>Fields Are Mandetory.
            </p>
        </div>

    </div>

    <div class="col-md-1 col-lg-2">
    </div>
</asp:Content>
