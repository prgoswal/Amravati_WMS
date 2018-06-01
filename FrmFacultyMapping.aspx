<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmFacultyMapping.aspx.cs" Inherits="FrmFacultyMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .myjumbotron{                      
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color:white; 
            padding: 15px;
            border-radius: 6px;
            overflow: auto;
        }
        .mycontainer {
            left: 0;
            right: 0;
            padding: 20px;
            min-height:78%;
            margin-top: 5px;
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="bg-info mycontainer">
        <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-1 control-label">Exam Year</label>
            <div class="col-md-2">
                <asp:DropDownList CssClass="form-control" ID="ddlYear" runat="server" />
            </div>
            <label class="col-md-1 control-label">Exam Session</label>
            <div class="col-md-2">
                <asp:DropDownList CssClass="form-control" ID="ddlSession" runat="server" />
            </div>
            <div class="col-md-2">
                <asp:Button CssClass="btn btn-primary" ID="btnShowFaculty" OnClick="btnShowFaculty_Click" Text="Show" runat="server" />
                <asp:LinkButton CssClass="btn btn-danger" ID="btnClear" OnClick="btnClear_Click" runat="server" ><i class="fa fa-trash"></i> Clear</asp:LinkButton>
            </div>
        </div>
        <div class="form-group" id="divhidden" visible="false" runat="server">
            <div class="col-md-3">
                <div class="myjumbotron">
                <asp:GridView Caption="Soft Data Faculty"  CssClass="table table-responsive" ID="gvSoftDataFaculty" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="ExamFacultyCD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  />
                        <asp:BoundField HeaderText="Faculty" DataField="ExamFaculty" />
                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:CheckBox AutoPostBack="true" ID="CBSoftDataFaculty" OnCheckedChanged="CBSoftDataFaculty_CheckedChanged" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </div>
            </div>
            <div class="col-md-2">                
                <div class="myjumbotron">
                <asp:GridView Caption="OCCPL Faculty"  CssClass="table table-responsive" ID="gvOccPlFaculty" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="ItemID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:CheckBox AutoPostBack="true" ID="CBOccPlFaculty" OnCheckedChanged="CBOccPlFaculty_CheckedChanged" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Faculty" DataField="ItemDesc" />
                    </Columns>
                </asp:GridView>
                    </div>
            </div>
            <div class="col-md-2 text-center">
                <asp:LinkButton CssClass="btn btn-default" ID="btnMapped" OnClick="btnMapped_Click" runat="server"><i class="fa fa-download"></i> Mapped</asp:LinkButton>
                <div class="form-group"></div>
                <asp:LinkButton CssClass="btn btn-default" ID="btnUnMapped" OnClick="btnUnMapped_Click" runat="server" ><i class="fa fa-upload"></i> UnMapped</asp:LinkButton><br />
                <div class="form-group"></div>
                <asp:LinkButton CssClass="btn btn-primary" ID="btnSave" OnClick="btnSave_Click" runat="server"><i class="fa fa-save"></i> Save Data</asp:LinkButton>
            </div>
            <div class="col-md-5" runat="server" visible="false" id="divMapp">
                <div class="myjumbotron">
                <asp:GridView Caption="Mapped Data" CssClass="table table-responsive" ID="gvMappedData" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ExamFacultyCD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField HeaderText="Soft Data" DataField="SoftData" />
                        <asp:BoundField HeaderText="OCC PL" DataField="OccPl" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CBUnMapped" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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

    </script>
</asp:Content>

