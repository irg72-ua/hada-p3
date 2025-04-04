<%@ Page Title="Products Management" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .form-container {
            max-width: 400px;
            margin: 20px auto;
            display: flex;
            flex-direction: column;
            gap: 15px;
        }

        label {
            font-weight: bold;
        }

        .input-box, .dropdown {
            width: 100%;
            padding: 8px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .button-container {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            justify-content: center;
            margin-top: 15px;
        }

        .btn {
            background-color: #0094ff;
            color: white;
            border: none;
            padding: 10px 15px;
            cursor: pointer;
            border-radius: 5px;
            transition: 0.3s;
        }

        /* Botones específicos */
        .btn-create {
            background-color: #28a745;
        }

        .btn-create:hover {
            background-color: #218838;
        }

        .btn-update {
            background-color: #ffc107;
            color: black;
        }

        .btn-update:hover {
            background-color: #e0a800;
        }

        .btn-delete {
            background-color: #dc3545;
        }

        .btn-delete:hover {
            background-color: #c82333;
        }

        .mensaje-exito, .mensaje-error {
            display: block;
            text-align: center;
            font-weight: bold;
            margin-bottom: 10px;
            padding: 10px;
            border-radius: 6px;
        }

        .mensaje-exito {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .mensaje-error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
    </style>

    <h2 class="title" style="text-align:center; font-size: 24px; margin: 30px 0;">Products Management</h2>

    <asp:Label ID="EtiquetaExito" runat="server" CssClass="mensaje-exito" Visible="false" Text="Operación realizada con éxito." />
    <asp:Label ID="EtiquetaFallo" runat="server" CssClass="mensaje-error" Visible="false" />

    <div class="form-container">
        <label for="txtCode">Code:</label>
        <asp:TextBox ID="txtCode" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="txtName">Name:</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="txtAmount">Amount:</label>
        <asp:TextBox ID="txtAmount" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="ddlCategory">Category:</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdown">
        </asp:DropDownList>

        <label for="txtPrice">Price:</label>
        <asp:TextBox ID="txtPrice" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="txtCreationDate">Creation Date:</label>
        <asp:TextBox ID="txtCreationDate" runat="server" CssClass="input-box"></asp:TextBox>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        <div class="button-container">
            <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn btn-create" OnClick="btnCreate_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-delete" OnClick="btnDelete_Click" />
        </div>

        <div class="button-container read-buttons">
            <asp:Button ID="btnRead" runat="server" Text="Read" CssClass="btn" OnClick="btnRead_Click" />
            <asp:Button ID="btnReadFirst" runat="server" Text="Read First" CssClass="btn" OnClick="btnReadFirst_Click" />
            <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" CssClass="btn" OnClick="btnReadPrev_Click" />
            <asp:Button ID="btnReadNext" runat="server" Text="Read Next" CssClass="btn" OnClick="btnReadNext_Click" />
        </div>

    </div>

</asp:Content>
