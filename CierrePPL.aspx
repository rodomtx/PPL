<%@ Page Title="Cierre PPL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CierrePPL.aspx.cs" Inherits="CierrePPL" Debug="true" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
     <h2>Cerrar PPL <span runat="server" id="_id" /></h2>

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
        <asp:Button ID="CerrarPPL" runat="server" OnClick="CerrarPPL_Click" Text="Cerrar PPL" />
    
    </div>
   

</asp:Content>