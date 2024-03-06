<%@ Page Title="ProductosPaginados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoPaginado.aspx.cs" Inherits="ASPFramework.Pages.ProductoPaginado" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <style type="text/css">
    .Table1 {font-size:14px;color:#333333;width:100%;border-width: 1px;border-color: #87ceeb;border-collapse: collapse;}
    .Table1 th {font-size:18px;background-color:#87ceeb;border-width: 1px;padding: 8px;border-style: solid;border-color: #87ceeb;text-align:left;}
    .Table1 tr {background-color:#ffffff;}
    .Table1 td {font-size:14px;border-width: 1px;padding: 8px;border-style: solid;border-color: #87ceeb;}
    .Table1 tr:hover {background-color:#e0ffff;}
         .auto-style2 {
             width: 145px;
         }
         .auto-style5 {
             width: 579px;
         }
         .auto-style6 {
             width: 102px;
         }
         .auto-style7 {
             width: 27px;
         }
         .auto-style8 {
             width: 482px;
         }
   </style>
    <p>
        <asp:TextBox ID="PrevURL" runat="server" Visible="false" Enabled="false" Text=""></asp:TextBox>
        <asp:TextBox ID="NextURL" runat="server" Visible="false" Enabled="false" Text=""></asp:TextBox>
        <asp:TextBox ID="ActURL" runat="server" Visible="false" Enabled="false" Text=""></asp:TextBox>
        <br />
    </p>
    <table style="width: 100%;">
        <tr>
            <td class="auto-style2">
                <asp:Button ID="BtnBusca" runat="server" Text="Buscar" OnClick="BtnBusca_Click" />
            </td>
            <td class="auto-style8">
                <asp:TextBox ID="TxtBuscar" runat="server" Width="440px" ></asp:TextBox>
            </td>
            <td>
                <asp:RadioButton ID="RdbBuscaTit" runat="server" Checked="True" GroupName="Buscar"  Text="Buscar productos" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style8">
                &nbsp;</td>
            <td>
                <asp:RadioButton ID="RdbBuscaCod" runat="server" GroupName="Buscar" Text="Buscar por código de producto"  />
            </td>
        </tr>
    </table>
    <p> &nbsp;</p>
    <table class="w-100">
        <tr>
            <td class="auto-style7"> 
                <asp:Label ID="Label1" runat="server" Text="Pagina" Enabled="false" Visible="true"></asp:Label>
            </td>
            <td class="auto-style5"> 
                <asp:Label ID="Label2" runat="server" Text="0" Enabled="false" Visible="true"></asp:Label>
            </td>
            <td class="auto-style6">&nbsp;</td> 
        </tr> 
        <tr>
            <td class="auto-style7"> 
                <asp:Button ID="Prev" runat="server" OnClick="Prev_Click" Text="Prev" /> 
            </td>
            <td class="auto-style5"> 
                <asp:Button ID="Next" runat="server" OnClick="Next_Click" Text="Next" />           
            </td>
            <td class="auto-style6">&nbsp;</td> 
        </tr>  
        <tr>
            <td class="auto-style7">
                <asp:Label ID="Label3" runat="server" Enabled="False" Text="Orden:"></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:RadioButton ID="RdbTitAsc" runat="server" GroupName="Ordenar" Text="Titulo Ascendente" Value="tituloasc" AutoPostBack="True" OnCheckedChanged="Rdb_CheckedChanged" />
                <asp:RadioButton ID="RdbTitDsc" runat="server" GroupName="Ordenar" Text="Titulo Descendente" Values="titulodesc" AutoPostBack="True" OnCheckedChanged="Rdb_CheckedChanged" />
                <asp:RadioButton ID="RdbCodAsc" runat="server" GroupName="Ordenar" Text="Codigo Ascendente" Values="codigoasc" AutoPostBack="True" OnCheckedChanged="Rdb_CheckedChanged" />
                <asp:RadioButton ID="RdbCodDsc" runat="server" GroupName="Ordenar" Text="Codigo Descendente" Values="codigodesc" AutoPostBack="True" OnCheckedChanged="Rdb_CheckedChanged" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
        
    <p>   
        <asp:Table ID="Table1" runat="server" Visible="False" CellPadding="10" CellSpacing="10" CssClass="Table1" HorizontalAlign="Center">
            <asp:TableHeaderRow HorizontalAlign="Center">          
                <asp:TableHeaderCell> Foto </asp:TableHeaderCell>
                <asp:TableHeaderCell> Codigo Producto </asp:TableHeaderCell>
                <asp:TableHeaderCell> Titulo </asp:TableHeaderCell>
                <asp:TableHeaderCell> Precio </asp:TableHeaderCell> 
         </asp:TableHeaderRow>
        </asp:Table>
        &nbsp;
    </p>  
</asp:Content>
