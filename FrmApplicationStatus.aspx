<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmApplicationStatus.aspx.cs" Inherits="FrmApplicationStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/style1.css" />
    <%--<script src="js/jquery-1.9.1.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid student-form" style="font-family: Calibri">
        <div class="main-form container">
            <div class="row user-creation" style="height: 100%; position: relative;">
                <h3 class="text-center" style="font-family: Calibri;">Application Status</h3>
                <hr class="changecolor" />

                <div class="container">
                    <div class="row">
                        <div class="col-md-4">
                            <label class="col-md-4">Application Date</label>
                            <label class="col-md-2">From</label>
                            <div class="col-md-5">
                                <asp:TextBox ID="txtfromdate" Style="height: 28px;" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </div>
                            <%--<div class="col-md-1">
                                    <asp:ImageButton ID="imgPopup" ValidationGroup="1" Style="margin: -5px -30px; height: 33px;" ImageUrl="images/calendar_icon.jpg" ImageAlign="Bottom" TabIndex="11" runat="server" />
                                </div>--%>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-2">
                                To
                            </label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txttodate" Style="height: 28px;" runat="server" CssClass="form-control datepicker"></asp:TextBox>

                            </div>
                            <%--<div class="col-md-2">
                                    <asp:ImageButton ID="ImageButton1" Style="margin: -5px -30px; height: 33px;" ImageUrl="images/calendar_icon.jpg" Height="40px" ImageAlign="Bottom" TabIndex="11" runat="server" />
                                </div>--%>
                        </div>
                        <div class="col-md-3">
                            <label class="col-md-2">
                                For
                            </label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlfor" AutoPostBack="true" Style="padding: 3px !important; height: 28px;" class="form-control exam-session-ddl" runat="server" TabIndex="1">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem>Success</asp:ListItem>
                                    <asp:ListItem>Reject</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label class="col-md-3">Applications</label>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton runat="server" Text="Show" ID="linkshow" CssClass="btn btn-primary" OnClick="btnshow_Click"></asp:LinkButton>
                            <asp:LinkButton runat="server" Text="Clear" ID="linkClear" CssClass="btn btn-primary" OnClick="linkClear_Click"></asp:LinkButton>
                        </div>
                        <div>
                            &nbsp;
                        </div>

                        <div align="right" style="margin-right: 7%;">
                            <asp:LinkButton ID="gridtoexcel" runat="server" Text="Export to Excel" OnClick="gridtoexcel_Click" CssClass="label label-primary"></asp:LinkButton>

                        </div>
                        <div>
                            &nbsp;
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
                        <div>
                            &nbsp;
                        </div>
                        <asp:Panel align="left" Style="overflow: auto; height: 300px; width: 97%; border: solid 1px;" ID="pnlgrid" runat="server">
                            <div class="col-sm-12">
                                <asp:GridView align="center" ID="grdListApplication" runat="server" HeaderStyle-BackColor="#295582" HeaderStyle-Font-Bold="true" OnRowCommand="grdListApplication_RowCommand"
                                    HeaderStyle-ForeColor="White" Width="100%" CellPadding="4" RowStyle-BorderStyle="Solid"
                                    ForeColor="Black" GridLines="Vertical" BackColor="#f1efef" BorderColor="Gray"
                                    BorderStyle="Solid" BorderWidth="2px"
                                    ShowFooter="false" AutoGenerateColumns="false">
                                    <RowStyle BorderStyle="Solid" />
                                    <Columns>
                                        <asp:BoundField DataField="AppNo" ItemStyle-Width="10%" HeaderText="Application No." />
                                        <asp:BoundField DataField="AppDate" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" ItemStyle-Width="17%" HeaderText="Application Date" />
                                        <asp:BoundField DataField="RollNo" ItemStyle-Width="8%" HeaderText="Roll No." />
                                        <asp:BoundField DataField="StudentName" ItemStyle-Width="25%" HeaderText="Student Name" />
                                        <asp:BoundField DataField="AppliedFor" ItemStyle-Width="25%" HeaderText="Applied For" />
                                        <asp:BoundField DataField="AppStatus" ItemStyle-Width="10%" HeaderText="Applied Status" />
                                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButtonTrail" runat="server" CommandArgument='<%#Eval("AppNo") %>' CommandName="ShowStatus">Trail</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <!-- section1 -->
            </div>
        </div>
        <!-- jQuery -->
    </div>
</asp:Content>
