<%@ Page Title="Iniciar PPL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="IniciarPPL.aspx.cs" Inherits="About" Debug="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Iniciar Protocolo de Paro de Linea</h3>
    <p>Presione el boton para iniciar el PPL</p>
<p>
    
        <asp:ImageButton ID="ImageButton1" runat="server" Height="250px" ImageUrl="~/red_button.jpg" OnClick="redbuttonIMG_Click" Width="250px" />
    
    </p>
</asp:Content>
