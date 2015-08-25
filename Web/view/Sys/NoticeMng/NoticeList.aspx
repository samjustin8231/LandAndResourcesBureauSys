<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeList.aspx.cs" Inherits="Maticsoft.Web.View.NoticeMng.NoticeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告列表</title>

    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    
    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/kindeditor-4.1.10/themes/default/default.css" rel="stylesheet"
        type="text/css" />
	<link rel="stylesheet" href="../../../js/kindeditor-4.1.10/plugins/code/prettify.css" />
	<script type="text/javascript" charset="utf-8" src="../../../js/kindeditor-4.1.10/kindeditor.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../../js/kindeditor-4.1.10/lang/zh_CN.js"></script>
	<script type="text/javascript" charset="utf-8" src="../../../js/kindeditor-4.1.10/plugins/code/prettify.js"></script>

    

    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form

        var datagrid;   //
        var editRow = undefined;
        var editor;

        $(function(){
            $('#search_name').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    reloadgrid();
                }
            });
        });

        KindEditor.ready(function (K) {
            editor = K.create('#content', {
                cssPath: '../../../js/kindeditor-4.1.10/plugins/code/prettify.css',
                uploadJson: '../../../js/kindeditor-4.1.10/asp.net/upload_json.ashx',
                fileManagerJson: '../../../js/kindeditor-4.1.10/asp.net/file_manager_json.ashx',
                allowFileManager: true
            });
            prettyPrint();
        });

        //添加
        function add() {
            //清空内容
            fm = $('#fm').form('clear');


            dlg = $('#dlg').dialog({
                title: '添加产品',
                modal: true,
                onLoad: function () {

                }
            }).dialog("open");

            document.getElementById("method").value = "add";
        }

        //修改
        function edit() {
            //验证权限
            <% if (!list_privilege_cur_page.Contains("300")) { 
                    %>
                    alert("对不起，没有权限！");return;
                    <%    
            } %>

            //先获取选择行
            var rows = $('#tt').datagrid("getSelections");
            //如果只选择了一行则可以进行修改，否则不操作
            if (rows.length == 1) {
                var row = rows[0];
                //获取要修改的字段
                

                dlg = $('#dlg').dialog({
                    title: '修改产品信息',
                    modal: true,
                    onLoad: function () {

                    }
                }).dialog("open");

                document.getElementById("method").value = "modify";
                fm = $('#fm').form('load', row);

                //获取公告内容

                $.getJSON('NoticeHandler.ashx?method=getContentById', { id: $("#fm").find("#id").val() }, function (result) {
                    console.info(result);
                    if (result.flag) {
                        editor.html(result.msg);
                    } else {
                        editor.html(result.msg);
                    }
                });

                //
            } else {
                $.messager.alert("提示", "请选择要一行修改！", "info");
            }
        }

        //保存信息
        function save() {
            var method = $("#method").val();
            if (method == "add") {

                //提交之前验证
                if (!$("#fm").form('validate')) {
                    return false;
                }

                if ($("#title").val() == "sam") {

                    return false;
                }

                $.getJSON('NoticeHandler.ashx?method=add', { title: $("#fm").find("#title").val(), content: editor.html() }, function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').datagrid('reload'); // reload the user data
                        $("#tt").datagrid('unselectAll');

                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });
                    } else {
                        $.messager.show({
                            title: 'Error',
                            msg: result.msg
                        });
                    }
                });
            }
            else//修改
            {
                var row = $('#tt').datagrid('getSelected');

                //提交之前验证
                if (!$("#fm").form('validate')) {
                    return false;
                }




                $.getJSON('NoticeHandler.ashx?method=modify', { id: row.id, title: $("#fm").find("#title").val(),isDeleted: $("#fm").find("input[name='isDeleted']").val(), content: editor.html() }, function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').datagrid('reload'); // reload the user data
                        $("#tt").datagrid('unselectAll');

                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });
                    } else {
                        $.messager.show({
                            title: 'Error',
                            msg: result.msg
                        });
                    }
                });
            }
        }

        //删除
        function del() {
            var method = document.getElementById("method").value = "delete";
            var rows = $('#tt').datagrid('getSelections');
            console.info(rows);
            console.info(rows.length);
            if (rows == null||rows.length==0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return;
            }
            if (rows) {
                $.messager.confirm('提示', '你确定要删除【' + rows.length + '】条记录吗？', function (r) {
                    if (r) {
                        var ids = [];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);
                        }

                        $.ajax({
                            url: 'NoticeHandler.ashx?method=delete',
                            data: {
                                ids: ids.join(",")
                            },
                            method: 'post',
                            dataType: 'json',
                            success: function (result) {
                                if (result.flag) {
                                    $('#dlg').dialog('close'); 	// close the dialog
                                    $('#tt').datagrid('reload'); // reload the user data
                                    $.messager.show({
                                        title: '提示',
                                        msg: result.msg
                                    });
                                } else {
                                    $.messager.show({
                                        title: 'Error',
                                        msg: result.msg
                                    });
                                }

                            }
                        });
                    }
                })
            }
        }

        //获取参数     
        function getQueryParams(queryParams) {

            var title = $("#fm_search").find("input[name='title']").val();
            var isDeleted = $("#isDeleted").combobox("getValue");

            queryParams.title = title;
            queryParams.isDeleted = isDeleted;
            return queryParams;
        }

        //增加查询参数，重新加载表格
        function reloadgrid() {
            //查询参数直接添加在queryParams中    
            var queryParams = $('#tt').datagrid('options').queryParams;
            getQueryParams(queryParams);
            $('#tt').datagrid('options').queryParams = queryParams;
            $("#tt").datagrid('reload');
            $("#tt").datagrid('unselectAll');
        }

        //刷新
        function reload() {
            $('#tt').datagrid('reload'); // reload the user data
            $('#tt').datagrid('unselectAll');
        }

        //取消选中
        function unselectall() {
            $('#tt').datagrid('unselectAll');
        }

        //清空
        function clearData() {
            $("#fm_search").form("clear");
            $("#isDeleted").combobox("setValue",-1);

            //重新加载数据
            reloadgrid();
        }

        function formatterTitleF(value, rowData, rowIndex) {
            return "<a href='#' onclick=addTab('公告内容'," + "'NoticeMng/notice.aspx?id_notice=" + rowData.id + "')>"+value+"</a>";
        }

        function formatterStateF(value, rowData, rowIndex) {
            if (rowData.isDeleted == "1") {
                return "<a class='isDeleted'  href='#' onclick='lock(" + rowIndex + ")'></a>";
            } else {
                return "<a class='isNotDeleted'  href='#' onclick='lock(" + rowIndex + ")'></a>";
            }
        }

        function lock(rowIndex) {
            var index;
            if (rowIndex == undefined) {
                index = $("#tt").datagrid("getRowIndex", $("#tt").datagrid("getSelected"));
            } else {
                index = rowIndex;
            }
            var rows = $("#tt").datagrid("getRows");
            var row = rows[index];

            $.getJSON('NoticeHandler.ashx?method=Lock&id=' + row.id, function (result) {
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
                reload();
            });
        }

        //加载dagargid中图片
        function loadLinkButton(data) {
            $(".isDeleted").linkbutton({ text: '关闭', plain: true, iconCls: 'icon-lock'});
            $(".isNotDeleted").linkbutton({ text: '启用', plain: true, iconCls: 'icon-open' });
        }

        function onRowContextMenuF(e, rowIndex, rowData) {
            //阻止浏览器响应
            e.preventDefault();
            //console.info(rowData);
            $('#tt').datagrid("unselectAll");
            $('#tt').datagrid("selectRow", rowIndex);
            $("#menu").menu("show", {
                left: e.pageX,
                top: e.pageY
            });
        }

        function onClickCellTitleF(index,field,value){
           
            var rows = $("#tt").datagrid("getRows");
            if(field=="title"){
                addTab('公告内容','NoticeMng/notice.aspx?id_notice='+ rows[index].id);   
            }
        }
    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="公告列表" class="easyui-datagrid" border="false" fit="true" fitcolumns="false" style="width: auto; "        
        idfield="id" pagination="true" data-options="onRowContextMenu:onRowContextMenuF,ctrlSelect:true,onDblClickRow:edit,iconCls:'icon-tip',rownumbers:true,url:'NoticeHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,onLoadSuccess:loadLinkButton,onClickCell:onClickCellTitleF" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'title',width:600,align:'left',sortable:'true',formatter:formatterTitleF">标题</th>
                <!-- <th data-options="field:'content',width:120,align:'center',sortable:'true'">内容</th> -->
                <th data-options="field:'create_time',width:300,align:'left',sortable:'true'">发布时间</th>   
                <th data-options="field:'isDeleted',width:80,align:'center',sortable:'true',formatter:formatterStateF">状态</th> 
                <th data-options="field:'user_name',width:200,align:'left',sortable:'true'">发布人</th>   
            </tr>
        </thead>
         <%--表格内容--%>
    </table>
    <%--功能区--%>
    <div id="tb" style="padding: 5px; height: auto">
        <%-- 包括添加、修改、删除、刷新、全部选中、取消全部选中 --%>
        <div style="margin-bottom: 5px">
            <% if (list_privilege_cur_page.Contains("300")) { 
                  %>
                  <a href="javascript:void(0)" onclick="edit() " title="修改" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-edit',plain:true"></a>
                  <%    
            } %>
            <% if (list_privilege_cur_page.Contains("400")) { 
                  %>
                  <a href="javascript:void(0)" onclick="del()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
                  <%    
            } %>
            
            <a href="javascript:void(0)" onclick="reload()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
            <a href="javascript:void(0)" onclick="unselectall()" title="取消选中" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-undo',plain:true"></a>
        </div>
        <%-- 查找Account信息，根据注册时间、姓名 --%>
        <div>
           <form id="fm_search" method="post">
           名称: 
            <input id="search_name" name="title" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'公告标题'" style="width:160px">&nbsp;&nbsp;

            <select id="isDeleted" class="easyui-combobox" name="dept" style="width:100px;">    
                <option value="-1">-- 请选择 --</option>    
                <option  value="0">启用</option>    
                <option  value="1">关闭</option>
            </select>&nbsp;&nbsp;&nbsp;&nbsp;
            
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>
            </form>
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 850px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">公告信息</div>
        <form id="fm" name="fm" method="post">
            <div class="fitem">
                <label>标题：</label>
                <input id="method" name="method" type="hidden" />  
                <input id="id" name="id" type="hidden" />  
                <input id="title" name="title" class="easyui-validatebox" data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>状态：</label>
                <select id="isDeleted" class="easyui-combobox" name="isDeleted" style="width:100px;">    
                    <option  value="0">启用</option>    
                    <option  value="1">关闭</option>
                </select>
            </div>
            <div class="fitem">
                <label>内容：</label>
                <textarea id="content" name="content" cols="200" rows="50" style="width:650px;height:370px;visibility:hidden;"></textarea>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close')">关闭</a>
    </div>

    <div id="menu" class="easyui-menu" style="width: 120px; display: none;">
        <% if (list_privilege_cur_page.Contains("300")) { 
                %>
                <div onclick="edit();" iconcls="icon-edit">
            编辑</div>
                <%    
        } %>
        <% if (list_privilege_cur_page.Contains("400")) { 
                %>
                <div onclick="del();" iconcls="icon-remove">
            删除</div>
                <%    
        } %>
        
        
    </div>
</body>
</html>

