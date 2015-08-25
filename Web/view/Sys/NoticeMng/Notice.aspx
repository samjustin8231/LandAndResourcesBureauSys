<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notice.aspx.cs" Inherits="Maticsoft.Web.View.NoticeMng.Notice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <link href="../../../css/members/css.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        function returnBack() {
            addTab("首页", "");
        }

        function closePage() {
            closeTab("公告内容");
        }
    </script>
</head>
<body>
    <div class="conttxt">
        <div class="contitle">
            <h1 style="font-size:20pt;"><%=t_notice.title %></h1>
            <div class="time">
                    发布时间：2015-4-19 15:44:17
            </div>
            <div>
                <%=t_notice.content %>
            </div>

            <div style="margin:10px;">
                <input type="button" class="btn" value="返回首页" onclick="returnBack()"/>
            </div>
            <!--
            <form id="Form1" runat="server">
                <div>
                    <asp:Button ID="btnPrev" runat="server" Text="上一篇" onclick="btnPrev_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="下一篇" onclick="btnNext_Click" />
                </div>
            </form>
            -->
        </div>
    </div>
        
</body>
</html>