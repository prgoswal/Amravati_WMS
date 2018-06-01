<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmImageInsertion.aspx.cs" Inherits="FrmImageInsertion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">




    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
    <link href="CSS/jquery-ui.css" rel="stylesheet" type="text/css" />


    <style type="text/css">
        #mask
        {
            /*position: fixed;*/
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
    </style>
    <script src="https://code.jquery.com/jquery-1.3.2.js" type="text/javascript"></script>
    <script type="text/javascript" lang="javascript">
        function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }
        $(".btnClose").live('click', function () {
            HidePopup();
        });
    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />




    <script type="text/javascript">

        function ValidateRegister() {

            if ($('#<%=txtLotNo.ClientID%>').val() == 0) {
                $('#<%=txtLotNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Lot No.', 'Error', function () { $("#txtLotNo").focus() })
                return false;
            }
            else {
                $('#<%=txtLotNo.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtImageNo.ClientID%>').val() == 0) {
                $('#<%=txtImageNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Image No.', 'Error', function () { $("#txtImageNo").focus() })
                return false;
            }
            else {
                $('#<%=txtImageNo.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtPageNo.ClientID%>').val() == 0) {
                $('#<%=txtPageNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Page No.', 'Error', function () { $("#txtPageNo").focus() })
                return false;
            }
            else {
                $('#<%=txtPageNo.ClientID%>').css('border-color', '');
            }
        }
    </script>
    <h3 class="header-title">Image Insertion</h3>
    <div class="box">
        <div class="row">
            <div class="col-md-10 col-lg-10">
                <div class="form-group">
                    <%--<label class="col-sm-2 col-md-2 col-lg-4">Register No.</label>--%>
                    <asp:Label runat="server" ID="lblLotNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                        Text="Lot No.">
                    </asp:Label>
                    <div class="col-sm-2 col-md-2 col-lg-2">
                        <asp:TextBox ID="txtLotNo" CssClass="form-control input-sm"
                            runat="server" MaxLength="7" AutoPostBack="True" 
                            ontextchanged="txtLotNo_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <asp:Label runat="server" ID="lblreg" Style="font-size: 16px; font-weight: 600" ForeColor="Red" Text="As Per Master Entry Total Images :-"></asp:Label>
                        <asp:Label runat="server" ID="lblTotalLotImages" Style="font-size: 16px; font-weight: 600" ForeColor="Red" Text=""></asp:Label>
                    </div>
                    <%-- <div class="col-sm-2 col-md-2 col-lg-2">
                        <asp:LinkButton ID="linkSearch" CssClass="btn btn-sm btn-primary" runat="server"
                            Text="" OnClick="linkSearch_Click"><i class="fa fa-search"> </i>&nbsp;Search</asp:LinkButton>
                    </div>
                    <div class="col-sm-2 col-md-5 col-lg-5">
                        <asp:Label runat="server" ID="lblSrchRegNo" ForeColor="Red"></asp:Label>
                    </div>--%>
                </div>
                
            </div>
            <div class="col-md-10 col-lg-10">
                <div class="form-group">
                    <%--<label class="col-sm-2 col-md-2 col-lg-4">Register No.</label>--%>
                    <asp:Label runat="server" ID="lblImageNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                        Text="Image No.">
                    </asp:Label>
                    <div class="col-sm-2 col-md-2 col-lg-2">
                        <asp:TextBox ID="txtImageNo" CssClass="form-control input-sm"
                            runat="server" MaxLength="3"></asp:TextBox>
                           
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <asp:Label runat="server" ID="lblIm" Style="font-size: 16px; font-weight: 600" ForeColor="Red" Text="As Per Scan Folder Total Images :- "></asp:Label>
                        <asp:Label runat="server" ID="lblTotalFolderImage" Style="font-size: 16px; font-weight: 600" ForeColor="Red" Text=""></asp:Label>
                    </div>

                </div>
            </div>
            <br />
            <div style="overflow: auto;">
                <asp:GridView ID="grdAvailablePages" ShowHeader="true" runat="server" class="" AutoGenerateColumns="true"
                    Width="100%" Style="text-align: center; border: 1px; borderstyle: Double;">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="40px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-10 col-lg-10">
                <div class="form-group">
                    <asp:Label runat="server" ID="lblFile" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                        Text="Select Image"></asp:Label>
                        &nbsp &nbsp
                         <asp:Label runat="server" ID="Label1" Style="font-size: 16px; font-weight: 600" ForeColor="Red" Text="Difference of Images :- "></asp:Label>
                        <asp:Label runat="server" ID="lblDiffrImg" Style="font-size: 16px; font-weight: 600" ForeColor="Red" Text="0"></asp:Label>
                    <asp:FileUpload ID="FileUpload1" runat="server" class="col-sm-4 col-md-4 col-lg-4" />
                    <asp:Button runat="server" ID="UploadButton" Visible="false" Text="Upload" class="col-sm-1 col-md-1 col-lg-1" OnClick="UploadButton_Click" />
                    <div class="col-md-5 col-lg-3">
                        <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>

                    </div>
                </div>
                <%--<asp:Label runat="server" ID="StatusLabel" Text="Upload status: " />--%>
            </div>
            <div class="col-md-10 col-lg-10">
                <div class="form-group">
                    <%--<label class="col-sm-2 col-md-2 col-lg-4">Register No.</label>--%>
                    <asp:Label runat="server" ID="lblPageNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                        Text="Page No.">
                    </asp:Label>
                    <div class="col-sm-2 col-md-2 col-lg-2">
                        <asp:TextBox ID="txtPageNo" CssClass="form-control input-sm"
                            runat="server" MaxLength="3"></asp:TextBox>
                    </div>
                    <div class="col-sm-5 col-md-5 col-lg-5">
                        <asp:LinkButton ID="linkAdd" CssClass="btn btn-primary" runat="server" OnClientClick="return ValidateRegister();"
                            Text="" OnClick="linkAdd_Click">&nbsp;ADD</asp:LinkButton>

                        <asp:Button ID="linkSave" class="btn btn-primary" runat="server" Text="Save" OnClick="linkSave_Click" />

                        <asp:LinkButton ID="LinkClear" class="btn btn-primary" runat="server" OnClick="LinkClear_Click">
                                   &nbsp;Clear</asp:LinkButton>
                        <asp:LinkButton ID="linkCancel" class="btn btn-primary" runat="server" OnClick="linkCancel_Click">
                                   &nbsp;Cancel</asp:LinkButton>
                    </div>

                    <div class="col-sm-1 col-md-1 col-lg-1">
                    </div>
                </div>
            </div>
            <br />

        </div>

        <div style="overflow: auto; text-align: center; height: 240px; width: 100%">
            <asp:GridView align="center" ID="grdImagedetails" runat="server" DataKeyNames="ImageNo" ShowHeader="true" HeaderStyle-BackColor="#50618c"
                HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="43%" CellPadding="4"
                RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="false" AutoGenerateColumns="false" OnRowCommand="grdImagedetails_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ImageField DataImageUrlField="Image" HeaderText="Image" ControlStyle-Height="100" ControlStyle-Width="260px" />
                    <asp:BoundField DataField="ImageNo" HeaderText="PageNo" ItemStyle-Width="70" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandArgument='<%#Eval("ImageNo") %>' CommandName="ShowPopup">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
        <div id="mask">
        </div>
        <div>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="80%" Height="550px"
                Style="z-index: 111; background-color: #f1efef; position: absolute; left: 11%; top: 10%; border: outset 2px gray; padding: 5px; display: none">
                <table style="width: 100%; padding-top: 0px;" cellpadding="0" cellspacing="5">
                    <tr style="background-color: #0924BC">
                        <td style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                            align="center">Image Details <a id="closebtn" style="color: white; float: right; text-decoration: none" class="btnClose" href="#">X</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px; text-align: center;">
                            <div id="imgdiv">
                                <asp:Image ID="ImageSelected" Width="100%" Height="480px" runat="server" />

                            </div>


                        </td>
                    </tr>




                </table>
            </asp:Panel>


        </div>

    </div>
</asp:Content>

