<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Please input the html to generate PDF (all html tags should be properly formed and closed):
        </div>
        <div>
            <asp:TextBox ID="txtHTML" runat="server" TextMode="MultiLine" Height="150px" />
        </div>
        <div>
            <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
        </div>
        <div>
            <asp:Label ID="lblMsg" runat="server" Font-Size="Small" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
