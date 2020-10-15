<%@ Page Title="VerificacionPPL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="VerificacionPPL.aspx.cs" Inherits="VerificacionPPL" Debug="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
     <h2>Verificacion PPL<span runat="server" id="_id" /></h2>

        <div>
        <h2>Area</h2>
        <br />
        <asp:TextBox ID="area" runat="server"  Width="302px"></asp:TextBox><br />
        <br />
        <h2>Causa</h2>
        <br />
        <asp:TextBox ID="causa" runat="server"  Width="302px"></asp:TextBox><br /><br />
        <br />
        <h2>Observaciones</h2>
        <br />
        <asp:TextBox ID="observaciones" runat="server" TextMode="MultiLine" Width="302px"></asp:TextBox>
        <br />
        <h2>Quien Reporto</h2>
        <br />
        <asp:TextBox ID="quienreporta" runat="server"  Width="292px"></asp:TextBox>
        <br />
        <h2>Codigo de Seguridad</h2>
        <br />
        <asp:TextBox ID="codigo" runat="server"  Width="292px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Verificar" runat="server" OnClick="VerificarPPL_Click" Text="Verificar" />
    
    </div>
   

</asp:Content>