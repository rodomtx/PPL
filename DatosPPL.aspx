<%@ Page Language="C#" Title="DatosPPL"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="DatosPPL.aspx.cs" Inherits="DatosPPL" Debug="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Datos para iniciar PPL</h1>
        <br />
        <br />
        <h2>Selecciona el area.</h2>
        <br />
        <br />
        <asp:DropDownList ID="listaAreas" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <h2>Selecciona la causa.</h2>
        <br />
        <asp:DropDownList ID="listaRubro" runat="server">
        </asp:DropDownList>
        <br />
        <br />
         <h2>Observaciones</h2>
        <br />
        <asp:TextBox ID="observacioones" runat="server" TextMode="MultiLine" Width="302px"></asp:TextBox>
        <br />
        <br />
         <h2>Quien Reporta</h2>
        <br />
        <asp:TextBox ID="quienreporta" runat="server" OnTextChanged="TextBox2_TextChanged" Width="292px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="iniciarPPL" runat="server" OnClick="Button1_Click" Text="Iniciar PPL" />
    
    </div>
   </asp:Content>