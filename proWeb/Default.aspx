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
            gap: 15px; /* Más espacio entre elementos */
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

        .btn:hover {
            background-color: #007acc;
        }
    </style>

    <h2 class="title">Products Management</h2>

    <div class="form-container">
        <label for="txtCode">Code:</label>
        <asp:TextBox ID="txtCode" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="txtName">Name:</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="txtAmount">Amount:</label>
        <asp:TextBox ID="txtAmount" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="ddlCategory">Category:</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdown">
            <asp:ListItem Text="Computing" Value="Computing"></asp:ListItem>
            <asp:ListItem Text="Electronics" Value="Electronics"></asp:ListItem>
            <asp:ListItem Text="Clothing" Value="Clothing"></asp:ListItem>
        </asp:DropDownList>

        <label for="txtPrice">Price:</label>
        <asp:TextBox ID="txtPrice" runat="server" CssClass="input-box"></asp:TextBox>

        <label for="txtCreationDate">Creation Date:</label>
        <asp:TextBox ID="txtCreationDate" runat="server" CssClass="input-box"></asp:TextBox>

        <div class="button-container">
            <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" />
            <asp:Button ID="btnRead" runat="server" Text="Read" CssClass="btn" />
            <asp:Button ID="btnReadFirst" runat="server" Text="Read First" CssClass="btn" />
            <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" CssClass="btn" />
            <asp:Button ID="btnReadNext" runat="server" Text="Read Next" CssClass="btn" />
        </div>
    </div>
</asp:Content>
