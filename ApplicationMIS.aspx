<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="ApplicationMIS.aspx.cs" Inherits="FrmApplicationStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/style1.css" />
    <%--<script src="js/jquery-1.9.1.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid student-form" style="font-family: Calibri">
        <div class="main-form container">
            <div class="row user-creation" style="height: 100%; position: relative;">
                <h3 class="text-center" style="font-family: Calibri;">Application MIS</h3>
                <hr class="changecolor" />
                <div class="container">
                    <div class="row">
                        <div class="col-md-4">
                            <label class="col-md-4">Application Date</label>
                            <label class="col-md-2">From</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtfromdate" Style="height: 28px;" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </div>
                            <%--<div class="col-md-1">
                                <asp:ImageButton ID="imgPopup" ValidationGroup="1" Style="margin: -5px -30px; height: 33px;" ImageUrl="images/calendar_icon.jpg" ImageAlign="Bottom" TabIndex="11" runat="server" />
                            </div>--%>
                        </div>
                        <div class="col-md-4">
                            <label class="col-md-2">
                                To
                            </label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txttodate" Style="height: 28px;" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </div>
                            <%--<div class="col-md-2">
                                <asp:ImageButton ID="ImageButton1" Style="margin: -5px -30px; height: 33px;" ImageUrl="images/calendar_icon.jpg" Height="40px" ImageAlign="Bottom" TabIndex="11" runat="server" />
                            </div>--%>
                        </div>
                        <div class="col-md-4">
                            <asp:LinkButton runat="server" Text="Show" ID="linkshow" CssClass="btn btn-primary" OnClick="btnshow_Click"></asp:LinkButton>
                            <asp:LinkButton runat="server" Text="Clear" ID="linkClear" CssClass="btn btn-primary" OnClick="linkClear_Click"></asp:LinkButton>
                            <asp:LinkButton ID="gridtoexcel" runat="server" OnClick="gridtoexcel_Click" CssClass="label label-primary"></asp:LinkButton>
                        </div>
                        <div class="col-sm-12 text-center" style="margin-left: 60%">
                            <asp:Label ID="lblmsg" runat="server" CssClass="text-danger" Style="float: left;"></asp:Label>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-GB" TargetControlID="txtfromdate" Mask="99/99/9999" MaskType="Date" AcceptNegative="None" />
                            <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" EmptyValueMessage="Please enter Date" InvalidValueMessage="Invalid Date" ControlToValidate="txtfromdate" />
                            <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtfromdate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-GB" TargetControlID="txttodate" Mask="99/99/9999" MaskType="Date" AcceptNegative="None" />
                            <cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" EmptyValueMessage="Please enter Date" InvalidValueMessage="Invalid Date" ControlToValidate="txttodate" />
                            <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" TargetControlID="txttodate" Format="dd/MM/yyyy"></cc1:CalendarExtender>--%>
                        </div>
                        <br />
                        <div>
                            &nbsp;
                        </div>
                        <div class="col-sm-10" align="center">
                            <asp:GridView align="center" ID="grdListApplication" runat="server" HeaderStyle-BackColor="#295582" HeaderStyle-Font-Bold="true" OnRowCommand="grdListApplication_RowCommand"
                                HeaderStyle-ForeColor="White" Width="70%" CellPadding="4" RowStyle-BorderStyle="Solid"
                                ForeColor="Black" GridLines="Vertical" BackColor="#f1efef" BorderColor="Gray"
                                BorderStyle="Solid" BorderWidth="2px"
                                ShowFooter="false" AutoGenerateColumns="false">
                                <RowStyle BorderStyle="Solid" />
                                <Columns>
                                    <asp:BoundField DataField="DocID" ItemStyle-Width="10%" HeaderText="Serial No." ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="DocType" ItemStyle-Width="30%" HeaderText="Document Type" />
                                    <asp:BoundField DataField="ReceivedApps" ItemStyle-Width="10%" HeaderText="Received" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="SuccessApps" ItemStyle-Width="10%" HeaderText="Completed" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="RejectedApps" ItemStyle-Width="10%" HeaderText="Rejected" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <!-- section1 -->
            </div>
        </div>
        <!-- jQuery -->
    </div>
</asp:Content>

