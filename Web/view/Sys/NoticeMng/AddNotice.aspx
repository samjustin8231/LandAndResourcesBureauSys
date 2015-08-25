<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNotice.aspx.cs" Inherits="Maticsoft.Web.View.NoticeMng.AddNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <!--<link href="../../css/members/css.css" rel="stylesheet" type="text/css" />-->
    <link href="../../../css/mystyle.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../../js/kindeditor-4.1.10/plugins/code/prettify.css" />
	<script type="text/javascript" charset="utf-8" src="../../../js/kindeditor-4.1.10/kindeditor.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../../js/kindeditor-4.1.10/lang/zh_CN.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../../js/kindeditor-4.1.10/plugins/code/prettify.js"></script>

    <style type="text/css">
        .btn{border:0px;}
    </style>

    <script type="text/javascript" charset="utf-8">
        KindEditor.ready(function (K) {
            editor = K.create('#content', {
                cssPath: '../../../js/kindeditor-4.1.10/plugins/code/prettify.css',
                uploadJson: '../../../js/kindeditor-4.1.10/asp.net/upload_json.ashx',
                fileManagerJson: '../../../js/kindeditor-4.1.10/asp.net/file_manager_json.ashx',
                allowFileManager: true
            });
            prettyPrint();
        });

        //保存信息
        function save() {
            var method = $("#method").val();
            method = "add";

            //提交之前验证
            if (!$("#fm").form('validate')) {
                return false;
            }

            $.getJSON('NoticeHandler.ashx?method=add', { title: $("#fm").find("input[name='title']").val(), isDeleted: $("#fm").find("input[name='isDeleted']").val(), content: editor.html() }, function (result) {
                if (result.flag) {
                    clearData();
                }
                alert(result.msg);
            });
        }

        function clearData() {
            $("#fm").form("clear");
            editor.html("");
        }
    </script>
</head>
    

<body>
  
    
    <div class="formbody">
    
        <div class="formtitle"><span>基本信息</span></div>
    
        <form id="fm" runat="server">
            <ul class="forminfo">
                
                <li><label>标题</label>
                    <input id="method" name="method" type="hidden" />  
                    <input id="id" name="id" type="hidden" />  

                    <input name="title" type="text" class="dfinput easyui-validatebox" data-options="required:true"/><i>*必填</i></li>
                <li><label>状态</label>
                    <select id="isDeleted" class="easyui-combobox" name="isDeleted" style="width:100px;">    
                        <option  value="0">启用</option>    
                        <option  value="1">关闭</option>
                    </select>
                </li>
                <li><label>内容</label>
                    <textarea id="content" name="content" cols="100" rows="8" class="dfinput" style="width:700px;height:410px;visibility:hidden;" ></textarea>
                </li>

                <li><label>&nbsp;</label>
                    <input type="button" class="btn" value="确认保存" onclick="save()"/>&nbsp;&nbsp;
                    <input type="button" class="btn" value="清空" onclick="clearData()"/>
                </li>
            </ul>
        </form>
    </div>
</body>
</html>
