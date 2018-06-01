<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" Async="true" AutoEventWireup="true" CodeFile="UserLockingUnlocking.aspx.cs" Inherits="UserLockingUnlocking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .container-full {
            margin-left: -255px;
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            /*background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;*/
        }

        .panelSytle {
            position: relative;
            z-index: 2;
            display: block; /*z-index: 111;*/
            margin-top: -45px;
            /*position: absolute;*/
            left: 25%;
            top: 25%;
            border-radius: 10px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            border: outset 2px gray;
            padding: 5px;
        }
    </style>
    <script type="text/javascript">
        function CheckFirstChar(key, txt) {
            if (key == 32 && txt.value.length <= 0) {
                return false;
            }
            else if (txt.value.length > 0) {
                if (txt.value.charCodeAt(0) == 32) {
                    txt.value = txt.value.substring(1, txt.value.length);
                    return true;
                }
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-1 col-lg-2">
        </div>
        <div class="col-md-10 col-lg-8">
            <div class="user-creation" style="position: relative;">
                <h3 class="text-center">Active / Lock Users</h3>
                <hr class="changecolor" />
                <div id="collapseExample" style="display: none">
                    <div class="container-full">
                        <div class="">
                            <div style="padding-top: 13%; padding-left: 8%;">
                                <asp:Panel ID="pnlprint" runat="server" BackColor="#d6dbe9" Width="45%" Height="230px" CssClass="panelSytle">
                                    <a id="btnCls" style="color: white; float: right; text-decoration: none" class="btnClose" href="javascript:HidePopupPrint()">
                                        <img src="images/cancel-512.png" height="20px" width="20px" /></a>
                                    <%--<div id="divprint" class="form-horizontal" style="background-color: white;box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); margin:12%;height:50%; width:76%; padding:2%; border-radius: 15px;"><div>--%>

                                    <table style="">
                                        <tr class="form-group">
                                            <td class="col-md-8">You Want To Perform Action :-<asp:Label ID="lblaction" runat="server"></asp:Label>
                                                &nbsp;&nbsp; For User :-<asp:Label ID="lblusername" runat="server"></asp:Label>
                                                <br>
                                                Please Enter Reason & Save.
                                            </td>
                                        </tr>
                                        <tr class="form-group">
                                            <td class="col-md-8">
                                                <%--<textarea id="txtremarks"  CssClass="form-control" runat="server" PlaceHolder="Enter Reason" minlenght="20" onkeydown="return (event.keyCode!=32);" maxlength="200" style="width:100%; height:100%;margin-top:20px;"></textarea><br />--%>
                                                <asp:TextBox ID="txtremarks" Width="100%" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TextMode="MultiLine" MaxLength="200" runat="server" placeholder="Enter Reason"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblremark" CssClass="bg-danger" runat="server" Text="Minimum 10 Characters Compulsary."></asp:Label>
                                                <br />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtremarks"
                                                    ValidationExpression="[a-zA-Z ]*$" ForeColor="Red" ErrorMessage=" Please Enter Only Alphabets." />
                                            </td>
                                        </tr>
                                        <tr class="form-group">
                                            <td class="col-md-8">
                                                <asp:Button ID="btnsave" class="btn btn-primary btn-block" runat="server" Style="margin-top: 8px;" Text="SAVE" OnClick="btnsave_Click" />
                                                <asp:RequiredFieldValidator ID="reqremarks" runat="server" ControlToValidate="txtremarks" ErrorMessage="Please Enter Reason"></asp:RequiredFieldValidator>
                                                <br>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtremarks" Display="Dynamic" ErrorMessage="Minimum 10 & Maximum 200 Characters Allowed." ValidationExpression="^[\s\S]{10,200}$"></asp:RegularExpressionValidator>
                                                <br>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--  </div></div>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <%--<div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <div>
                                    <asp:RadioButtonList ID="rblist" runat="server" RepeatDirection="Horizontal" Width="50%" AutoPostBack="true">
                                        <asp:ListItem Selected="True">Active Users</asp:ListItem>
                                        <asp:ListItem>InActive Users</asp:ListItem>
                                        <asp:ListItem>Locked Users</asp:ListItem>
                                        <asp:ListItem>Unlocked Users</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="text-center" style="overflow: auto; height: 400px; width: 100%">
                                <table style="width: 70%; background-color: #526a9c; color: white;">
                                    <tr>
                                        <td style="background-color: #526a9c; color: white; width: 5%; align-content: center; text-align: center;">Sr.No.</td>
                                        <td style="width: 30%; text-align: center;">Profile Type</td>
                                        <td style="width: 23%; text-align: center;">User Name</td>
                                        <td style="width: 22%; text-align: center;">Login ID</td>
                                        <td style="width: 10%; text-align: center;">Action</td>
                                    </tr>
                                </table>
                                
                                    <asp:GridView ID="grdUserUnlocking" runat="server" class="table table-bordered" DataKeyNames="UserId"
                                        AutoGenerateColumns="False"  ShowHeader="false"
                                        OnRowCommand="grdUserUnlocking_RowCommand"
                                        OnRowUpdating="grdUserUnlocking_RowUpdating"
                                        OnRowDataBound="grdUserUnlocking_RowDataBound" OnSelectedIndexChanging="grdUserUnlocking_SelectedIndexChanging">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="User Profile" DataField="UserProfile" />
                                            <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                            <asp:BoundField HeaderText="LoginID" DataField="UserLoginID" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton runat="server" ID="lnkUpdte" CommandName="Update" Text="Lock"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnklock" runat="server" ValidationGroup="1"
                                                        CommandName="Lock" CommandArgument='<%# Container.DataItemIndex%>'>Lock</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <%--<div class="text-center" style="overflow: auto; height: 400px; width: 100%">
                                    <table align="center">
                                        <tr>
                                            <asp:GridView ID="grdUserUnlocking" runat="server" class="table table-bordered" DataKeyNames="UserId"
                                                AutoGenerateColumns="False" Width="100%" ShowHeader="false"
                                                OnRowCommand="grdUserUnlocking_RowCommand"
                                                OnRowUpdating="grdUserUnlocking_RowUpdating"
                                                OnRowDataBound="grdUserUnlocking_RowDataBound" OnSelectedIndexChanging="grdUserUnlocking_SelectedIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                                    <asp:BoundField HeaderText="LoginID" DataField="UserLoginID" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton runat="server" ID="lnkUpdte" CommandName="Update" Text="Lock"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnklock" runat="server" ValidationGroup="1"
                                                                CommandName="Lock" CommandArgument='<%# Container.DataItemIndex%>'>Lock</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <div class="col-md-offset-1 col-lg-offset-1 col-lg-10 col-md-10 text-center">
                        <div class="table-responsive">
                            <div>
                                <asp:RadioButtonList ID="rblist" runat="server" RepeatDirection="Horizontal" Width="100%" AutoPostBack="true">
                                    <asp:ListItem Selected="True">Active Users</asp:ListItem>
                                    <asp:ListItem>InActive Users</asp:ListItem>
                                    <asp:ListItem>Locked Users</asp:ListItem>
                                    <asp:ListItem>Unlocked Users</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-offset-1 col-lg-offset-1 col-lg-10 col-md-10">
                            <table class="form-control" style="background-color: #526a9c; color: white;">
                                <tr>
                                    <td class="col-lg-1 col-md-1" style="background-color: #526a9c; color: white;">Sr.No.</td>
                                    <td class="col-lg-3 col-md-3 text-center">User Profile</td>
                                    <td class="col-lg-3 col-md-3 text-center">User Name</td>
                                    <td class="col-lg-3 col-md-3 text-center">Login ID</td>
                                    <td class="col-lg-1 col-md-1 text-center" style="text-align: right">Action</td>
                                </tr>

                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-offset-1 col-lg-offset-1 col-lg-10 col-md-10 " style="overflow: auto; height: 300px;">
                            <table class="table-responsive text-center">
                                <tr>
                                    <asp:GridView ID="grdUserUnlocking" runat="server" class="table table-bordered" DataKeyNames="UserId"
                                        AutoGenerateColumns="False" Width="100%" ShowHeader="false"
                                        OnRowCommand="grdUserUnlocking_RowCommand"
                                        OnRowUpdating="grdUserUnlocking_RowUpdating"
                                        OnRowDataBound="grdUserUnlocking_RowDataBound" OnSelectedIndexChanging="grdUserUnlocking_SelectedIndexChanging">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="User Profile" ItemStyle-Width="30%" DataField="UserProfile" />
                                            <asp:BoundField HeaderText="User Name" ItemStyle-Width="25%" DataField="UserName" />
                                            <asp:BoundField HeaderText="LoginID" ItemStyle-Width="25%" DataField="UserLoginID" />
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--    <asp:LinkButton runat="server" ID="lnkUpdte" CommandName="Update"
                                                Text="Lock"></asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lnklock" runat="server" ValidationGroup="1"
                                                        CommandName="Lock" CommandArgument='<%# Container.DataItemIndex%>'>Lock</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></td>
                                </tr>
                            </table>
                        </div>

                    </div>




                </div>
            </div>
        </div>
    </div>
    <div class="col-md-1 col-lg-2">
    </div>
    <script type="text/javascript">
        function ShowPopupPrint() {
            $('#mask').show();
            $('#collapseExample').show();
            $("#txtremarks").focus()
        }

        function HidePopupPrint() {
            $('#mask').hide();
            //$('#<%=pnlprint.ClientID %>').hide();
            $('#collapseExample').hide();
        }
        $(".btnClose11").live('Click', function () {
            HidePopupPrint();
        });
    </script>

</asp:Content>

