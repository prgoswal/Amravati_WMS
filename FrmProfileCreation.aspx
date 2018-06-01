<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmProfileCreation.aspx.cs" Inherits="FrmProfileCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-offset-2 col-md-offset-2 "></div>
        <div class="col-lg-8 col-sm-offset-2">
            <div class="user-creation">
                <h3 class="text-center">Profile Creation</h3>
                <hr class="changecolor" />
                <div class="row">
                     <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                    <div class="form-group">
                        <div class="col-md-3 col-md-offset-2">
                         <%--  <asp:LinkButton ID="lnkProfileName" style="font-weight:bold;" CssClass="text-primary bg-info" runat="server" Text="CLICK FOR ADD NEW PROFILE" OnClick="lnkProfileName_Click"></asp:LinkButton>--%>
                        Add New Profile

                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtuser" runat="server" MaxLength="20" CssClass="form-control" placeholder="User Profile"></asp:TextBox>
                        </div>
                        <div class="col-md-3 ">
                            <%--<asp:LinkButton ID="linkadd" class="btn btn-primary" runat="server" OnClick="linkadd_Click">
                           Save
                            </asp:LinkButton>--%>
                            <asp:Button ID="btnadd" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnadd_Click" />
                            <br />
                             <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <%--<div class="col-md-4" hidden>
                        <h5 class="text-center text-info">Profile List</h5>        
                        <asp:GridView ID="gridusers" CssClass="table table-bordered" runat="server" ShowHeader="false"
                            AutoGenerateColumns="false" DataKeyNames="ItemID"
                            OnRowCommand="gridusers_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="LevelDescription" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" ValidationGroup="1"
                                            CommandName="EditData" CommandArgument='<%# Container.DataItemIndex%>'>Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>                    
                   </div>
                   
                    <div class="col-md-4" hidden>
                         <h5 class="text-center text-info">Allow Menu List</h5>
                        <asp:GridView ID="gridmenu" CssClass="table table-bordered"  runat="server"  ShowHeader="false" 
                          AutoGenerateColumns="false" DataKeyNames="ItemID"  
                          OnRowCommand="gridmenu_RowCommand">
                        <Columns>
                          <asp:BoundField DataField="LevelDescription"  />                              
                             <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkmenu" Text="Allow" runat="server"  />                           
                                </ItemTemplate>
                            </asp:TemplateField>                           
                        </Columns>
                    </asp:GridView>
                    </div>--%>
                    <br />

                   
                    <div class="col-md-10 col-md-offset-1">
                        <asp:Label ID="label1" runat="server" CssClass="text-success"></asp:Label>
                        <asp:GridView ID="GridMatrix" align="center"   runat="server" CssClass="table table-striped table-bordered" OnRowCommand="GridMatrix_RowCommand" OnRowDataBound="GridMatrix_RowDataBound">
                            <%--CssClass="table table-striped table-bordered"--%>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="ACTION">
                                    <%--<EditItemTemplate>
                                        <asp:LinkButton ID="linkbtnSave" runat="server" Font-Bold="true" CssClass="bg-success"  CommandName="btnSave" CommandArgument='<%#Eval("ItemId" ) %>'></asp:LinkButton>
                                    </EditItemTemplate>
                                        --%>
                                    <ItemTemplate >
                                        <asp:LinkButton ID="linkbtnEdit" runat="server" Font-Bold="true" CssClass="bg-warning text-info"  CommandName="btnEdit" CommandArgument='<%#Eval("ItemId" ) %>'>EDIT</asp:LinkButton>
                                    <%--<asp:LinkButton ID="btnupdate"  CommandName="UpdateRow" CommandArgument='<%#Eval("ShopCodeOdp" ) %>' runat="server">Update</asp:LinkButton>--%>
                                    </ItemTemplate>
                                           
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                        <br />
                       
                        <asp:Button ID="btnfinalsave" runat="server" class="btn btn-primary" Visible="false" Text="Save" OnClick="btnfinalsave_Click" />

                    </div>
</ContentTemplate>
                    </asp:UpdatePanel>

                    
                    <div class="col-md-10 col-md-offset-1">
                        <asp:Label ID="lblMyMsg" runat="server"></asp:Label>
                    </div>




                    <div class="col-md-4 hidden">
                        <center>
                    <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server"
                        TabIndex="2" >Allow 
                    </asp:LinkButton>
                               <br />
                          
                      </center>
                    </div>
                    


                </div>
            </div>
        </div>




    </div>

</asp:Content>
<%--<div class="col-md-10 col-lg-10">
            <div class="user-creation" style="position: relative; height:400px;">
                   <h3 class="text-center">Profile Creation</h3>
                <hr class="changecolor" />

    <div class="col-md-4 " style="background-color:lavender;">
        <div class="col-md-2 ">
            <table style="width:100%">
                <tr>
                    <th>User Name</th>
                    <td> <asp:TextBox ID="txtusername" CssClass="form-control input-sm" runat="server"
                                    MaxLength="15" TabIndex="1"></asp:TextBox></td>
                </tr>
               
            </table>
        </div>
        <div class="col-md-2">
         
                  
              
        </div>


    </div>
    <div class="col-sm-4" style="background-color:lavenderblush;">.col-sm-4</div>
    <div class="col-sm-4" style="background-color:lavender;">.col-sm-4</div>

             
   




                </div>
            </div>--%>
   

