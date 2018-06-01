<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmExamMapping.aspx.cs" Inherits="FrmExamMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .myjumbotron{                      
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color:white; 
            padding: 0px 10px;
            border-radius: 6px;
            padding-bottom: 10px;
            /*overflow: auto;*/
            /*max-height: 350px;*/
        }
        .mycontainer {
            left: 0;
            right: 0;
            padding: 5px 20px;
            min-height:78%;
            margin-top: 5px;
            position: absolute;
        }
        .form-control{
              height: 27px;
              padding: 0px 5px;
        }
        .btn-primary{
             height: 27px;
             padding: 0px 5px;
        }
        caption{
            padding-top: 4px;
            padding-bottom: 4px;
            color: #777;
            text-align: center;
            font-weight: bold;
        }
        .well{
            margin: 0;
        }
        
        .gvhigh{
            overflow:auto;
            max-height:350px;
        }
        .zoom{
            border: 1px solid #ddd;
            width:540px; 
            height:448px;
        }
        .modal-content{
            height:500px;
            margin-top:auto;
            margin-bottom:auto;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="bg-info mycontainer">
        <div class="form-horizontal">

            <div class="form-group">
                <label class="col-md-1">Exam Year</label>
                <div class="col-md-1">
                    <asp:DropDownList CssClass="form-control" ID="ddlYear" runat="server" />
                </div>
                <label class="col-md-1">Exam Session</label>
                <div class="col-md-1">
                    <asp:DropDownList CssClass="form-control" ID="ddlSession" runat="server" />
                </div>
                <div class="col-md-1 text-center">
                    <asp:Button CssClass="btn btn-primary" ID="btnShowExams" OnClick="btnShowExams_Click" Text="Display" runat="server" />
                </div>

                <div class="col-md-2 text-center">
                    <asp:CheckBox AutoPostBack="true" Checked="false" ID="cbShowSearchbox" Text="Search By Name" OnCheckedChanged="cbShowSearchbox_CheckedChanged" runat="server" />
                </div>
                <div class="col-md-5 well well-sm" id="divDataFind" visible="false" runat="server">
                    <div class="col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Search..." ID="txtSearchByName" runat="server" />
                    </div>
                    <div class="col-md-5">
                        <asp:RadioButtonList RepeatDirection="Horizontal" ID="rbSearch" runat="server">
                            <asp:ListItem Text="Both" Selected="True" Value="rbBoth"  />
                            <asp:ListItem Text="OCCPL" Value="rbOccPl" />
                            <asp:ListItem Text="University" Value="rbUniversity" />
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-3">
                        <asp:LinkButton CssClass="btn btn-default" ID="btnFindData" OnClick="btnFindData_Click" runat="server"><i class="fa fa-search"></i> Find</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divhidden" visible="false" runat="server">
                <div class="form-group">
                    <div class="col-md-5">
                        <div class="myjumbotron">
                            <h5 class="text-center">University Data</h5>
                            <div class="gvhigh">
                            <asp:GridView CssClass="table table-responsive overFlow" ID="gvUniversity" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:CheckBox AutoPostBack="true" ID="CBUniversity" OnCheckedChanged="CBUniversity_CheckedChanged" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ExamFacultyCD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField HeaderText="Faculty" DataField="ExamFaculty" />
                                    <asp:BoundField HeaderText="Exam Name" DataField="ExamName" />
                                </Columns>
                            </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2 text-center">
                        <asp:LinkButton CssClass="btn btn-default" ID="btnMapped" OnClick="btnMapped_Click" runat="server"><i class="fa fa-download"></i> Mapped</asp:LinkButton>
                        <div class="form-group"></div>
                        <%--<asp:LinkButton CssClass="btn btn-default" ID="btnUnMapped" OnClick="btnUnMapped_Click" runat="server"><i class="fa fa-upload"></i> UnMapped</asp:LinkButton><br />--%>
                    </div>

                    <div class="col-md-5">
                        <div class="myjumbotron">
                            <h5 class="text-center">OCCPL Exam Data</h5>
                            <div class="gvhigh">
                            <asp:GridView  CssClass="table table-responsive overFlow" ID="gvOccPlExam" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="FacultyCD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:CheckBox AutoPostBack="true" ID="CBOccPlFaculty" OnCheckedChanged="CBOccPlFaculty_CheckedChanged" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Faculty" DataField="Faculty" />
                                    <asp:BoundField HeaderText="Exam Name" DataField="ExamName" />
                                </Columns>
                            </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="form-group">
                    <div class="col-md-12">
                        <div class="myjumbotron" runat="server" visible="false" id="divMapp">
                            <h5 class="text-center">Mapped Data</h5>
                            <div class="gvhigh">
                            <asp:GridView CssClass="table table-responsive" ID="gvMappedData" OnRowCommand="gvMappedData_RowCommand" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="ExamFacultyCD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField HeaderText="Soft Data" DataField="UniversityData" />
                                    <asp:BoundField HeaderText="OCC PL" DataField="OccPl" />
                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btngvUnmap" CommandName="UnMap" runat="server"><i class="fa fa-upload"></i> UnMapped</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div class="col-md-12">
                                <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                <asp:LinkButton ID="btnSave" CssClass="btn btn-primary" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                                <asp:LinkButton ID="btnClear" CssClass="btn btn-primary" runat="server"><i class="fa fa-eraser"></i> Clear</asp:LinkButton>
                            </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

   <div class="modal fade" id="modalRollNo" data-backdrop="static">
        <div class="container" style="margin-top: 40px;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6" style="border-right: 1px solid #ddd;">
                            <asp:Label ID="lblExamName" runat="server" />
                            <div>
                                <asp:Image class="zoom" ImageUrl="~/images/00000009.JPG" runat="server" />
                                 <script src="js/wheelzoom.js"></script>      
                                <script>
                                    wheelzoom(document.querySelector('img.zoom'));
                                </script>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12 form-group">
                                <label class="col-md-2">Roll No.</label>
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" placeholder="Search By Roll No." style="height: 34px;" runat="server" />
                                    <span class="input-group-btn">
                                        <asp:LinkButton CssClass="btn btn-default"  ID="LinkButton1"  runat="server" ><i class="fa fa-search"></i> Go</asp:LinkButton>
                                    </span>
                                </div>
                                <%--<div class="col-md-6">
                                    <asp:TextBox CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <asp:LinkButton CssClass="btn btn-default" ID="btnGo"  runat="server" ><i class="fa fa-search"></i> Go</asp:LinkButton>
                                </div>--%>
                            </div>
                            <div class="col-md-12" style="min-height:385px;">                                
                                <asp:GridView runat="server"></asp:GridView>
                            </div>
                            <div class="col-md-12">
                                <asp:LinkButton CssClass="btn btn-default" ID="btnMappedbyRoll" OnClick="btnMappedbyRoll_Click" runat="server" ><i class="fa fa-download"></i> Mapped</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-default" runat="server"><i class="fa fa-backward"></i> Exit</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="errorModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="modal-body">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <i class="fa fa-warning"></i>
                        <asp:Label CssClass="text-danger" ID="lblMsg" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton CssClass="btn btn-primary" runat="server"><i class="fa fa-backward"></i> Back</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js1/bootstrap.min.js"></script>
    <script type="text/javascript">
        function errorModal() {
            $("#errorModal").modal();
        }
        function modalRollNo(){
            $("#modalRollNo").modal();
        }

    </script>
</asp:Content>