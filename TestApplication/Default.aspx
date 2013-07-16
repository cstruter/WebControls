<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <style type="text/css">
        .target {
            border: #c0c0c0 solid 1px;
            background-color: Green;
            width: 100px;
            height: 100px;
        }
    </style>
    <script type="text/javascript">

        function Test_Click(value) {
            alert(value);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tag" class="target">
    </div>
    <CSTruter:ContextMenu ID="TextContextMenu" TargetSelector="#tag" runat="server" OnClick="TextContextMenu_Click"
        OnClientClick="Test_Click">
        <CSTruter:ContextMenuItem Active="True" CommandName="A" Text="Test A" />
        <CSTruter:ContextMenuItem Active="false" CommandName="B" Text="Test B" />
        <CSTruter:ContextMenuItem Active="True" CommandName="C" Text="Test C" />
    </CSTruter:ContextMenu>
    </form>
</body>
</html>
