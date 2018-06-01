<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ExamMaster.aspx.cs" Inherits="_Default" %>

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

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 91 || charCode == 93 || charCode == 46 || charCode > 64 || charCode < 91)
                return true;
            return false;
        }</script>

    <script type="text/javascript">
        $(function () {
            $('#upper').keyup(function () {
                this.value = this.value.toUpperCase();
            });
        });
    </script>


    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <h3 class="header-title">Exam Master</h3>
    <div class="box">
        <asp:UpdatePanel runat="server" ID="updatepanelExamMaster">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-4 col-md-6 col-lg-5">
                                Faculty
                                <asp:Label runat="server" ID="lblFaculty" ForeColor="Red" Text="*"></asp:Label>
                            </label>
                            <div class="col-sm-8 col-md-6 col-lg-7">
                                <asp:DropDownList ID="ddlFaculty" CssClass="form-control input-sm" runat="server" required="true"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="col-sm-8 col-md-8 col-lg-8">
                        <div class="form-group">
                            <label class="col-sm-2 col-md-3 col-lg-2">
                                Exam Full Name
                                <asp:Label runat="server" ID="lblExamName" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-10 col-md-9 col-lg-10">
                                <%--<asp:DropDownList ID="ddlExamname" CssClass="form-control input-sm" runat="server" AutoPostBack="True" ></asp:DropDownList>--%>
                                <asp:TextBox ID="txtExamFullName" runat="server" CssClass="form-control input-sm upper"
                                    TabIndex="2"></asp:TextBox>
                                <%-- <cc1:FilteredTextBoxExtender runat="server" ID="filterExamFullName" TargetControlID="txtExamFullName"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ.[] ">
                                </cc1:FilteredTextBoxExtender>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-6 col-md-6 col-lg-5">
                                Exam Short Name <span style="color: red;">*</span>
                            </label>
                            <div class="col-sm-6 col-md-6 col-lg-7">
                                <asp:TextBox ID="txtExamShortName" CssClass="form-control input-sm upper" runat="server"
                                    TabIndex="3"></asp:TextBox>
                                <%--  <cc1:FilteredTextBoxExtender runat="server" ID="filterExamShortName" TargetControlID="txtExamShortName"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ.[] ">
                                </cc1:FilteredTextBoxExtender>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-6 col-md-6 col-lg-4">
                                Exam Type
                                <asp:Label runat="server" ID="lblSession" ForeColor="Red" Text="*"></asp:Label></label>
                            <div class="col-sm-6 col-md-6 col-lg-8">
                                <asp:DropDownList ID="ddlExamType" CssClass="form-control input-sm" runat="server"
                                    OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged" AutoPostBack="true"
                                    TabIndex="4">
                                    <asp:ListItem Value="0" Text="Select Exam Type"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yearly"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Semester"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-2 col-md-6 col-lg-4">
                                Total Year / Sem
                                <asp:Label runat="server" ID="Label1" ForeColor="Red" Text="*"></asp:Label>
                            </label>
                            <div class="col-sm-10 col-md-6 col-lg-8">
                                <asp:DropDownList ID="ddlTotalYr" CssClass="form-control input-sm" runat="server"
                                    AutoPostBack="true" TabIndex="5">
                                </asp:DropDownList>
                                <%--<asp:DropDownList ID="ddlTotalSem" CssClass="form-control input-sm" runat="server"
                                    Visible="false" AutoPostBack="true" TabIndex="6">
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-6 col-md-6 col-lg-5">
                                Course Code
                            </label>
                            <div class="col-sm-6 col-md-6 col-lg-7">
                                <asp:TextBox ID="txtCourseCode" CssClass="form-control input-sm" runat="server" MaxLength="6"
                                    TabIndex="7"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender runat="server" ID="filterCourseCode" TargetControlID="txtCourseCode"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-6 col-md-6 col-lg-4">
                                Course Name
                            </label>
                            <div class="col-sm-6 col-md-6 col-lg-8">
                                <asp:TextBox ID="txtCourseName" CssClass="form-control input-sm upper" runat="server"
                                    TabIndex="8"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender runat="server" ID="filterCourseName" TargetControlID="txtCourseName"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ.[] ">
                                </cc1:FilteredTextBoxExtender>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-2 col-md-6 col-lg-4">
                                Ref. Ctrl
                            </label>
                            <div class="col-sm-10 col-md-6 col-lg-8">
                                <asp:TextBox ID="txtRefCtrl" CssClass="form-control input-sm" runat="server" TabIndex="9"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender runat="server" ID="filterRefCtrl" TargetControlID="txtRefCtrl"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <div class="form-group">
                            <label class="col-sm-2 col-md-6 col-lg-5">
                                Course Type
                            </label>
                            <div class="col-sm-10 col-md-6 col-lg-7">
                                <asp:DropDownList ID="ddlCourseType" CssClass="form-control input-sm" runat="server"
                                    TabIndex="10">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <asp:Label runat="server" Text="" ID="lblMsg" ForeColor="Red"></asp:Label>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="pull-right">
                            <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server" OnClientClick="return ValidateRegister();"
                                TabIndex="12" OnClick="linkSave_Click"><i class="fa fa-save" ></i>&nbsp;Save</asp:LinkButton>
                            <asp:LinkButton ID="linkCancel" class="btn btn-primary" runat="server" TabIndex="13"
                                OnClick="linkCancel_Click"><i class="fa fa-close"></i>
                        &nbsp;Clear</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive" style="height: 200px">
                                <tr>
                                    <asp:GridView ID="gridExamDetails" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderText="Exam Name" DataField="ExamName" />
                                            <asp:BoundField HeaderText="Exam Short Name" DataField="ExamShortName" />
                                            <asp:BoundField HeaderText="Exam Type" DataField="ExamType" />
                                            <asp:BoundField HeaderText="Total Year/Sem" DataField="TotalYearSem" />
                                            <asp:BoundField HeaderText="Ctrl" DataField="Ctrl" Visible="false" />
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                                </tbody>
                                    </table>
                            </div>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">

        function ValidateRegister() {

            if ($('#<%=ddlFaculty.ClientID%>').val() == 0) {
                $('#<%=ddlFaculty.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Faculty', 'Error', function () { $("#ddlFaculty").focus() })
                return false;
            }
            else {
                $('#<%=ddlFaculty.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtExamFullName.ClientID%>').val() == '') {
                $('#<%=txtExamFullName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Exam Full Name', 'Error', function () { $("#txtExamFullName").focus() })
                return false;
            }
            else {
                $('#<%=txtExamFullName.ClientID%>').css('border-color', '');
            }


            if ($('#<%=txtExamShortName.ClientID%>').val() == '') {
                $('#<%=txtExamShortName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Exam Short Name', 'Error', function () { $("#txtExamShortName").focus() })
                return false;
            }
            else {
                $('#<%=txtExamShortName.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlExamType.ClientID%>').val() == 0) {

                $('#<%=ddlExamType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Type', 'Error', function () { $("#ddlExamType").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamType.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlTotalYr.ClientID%>').val() == "Select Year") {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Total No. Of Year/Sem', 'Error', function () { $("#ddlTotalYr").focus() })
                return false;
            }
            else {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlTotalYr.ClientID%>').val() == "Select Sem") {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Total No. Of Year/Sem', 'Error', function () { $("#ddlTotalYr").focus() })
                return false;
            }
            else {
                $('#<%=ddlTotalYr.ClientID%>').css('border-color', '');
            }
        }

    </script>
</asp:Content>
