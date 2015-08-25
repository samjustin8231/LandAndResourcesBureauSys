<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministratorAeraList.aspx.cs" Inherits="Maticsoft.Web.View.Business.AdministratorAreaMng.AdministratorAeraList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>行政区列表</title>

    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />

    <link href="../../../js/kindeditor-4.1.10/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form

        var datagrid;   //
        var editRow = undefined;

        $(function () {
            $('#search_name').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    reloadgrid();
                }
            });
        });

        //添加
        function add() {
            //清空内容
            fm = $('#fm').form('clear');


            dlg = $('#dlg').dialog({
                title: '添加行政区',
                modal: true,
                onLoad: function () {

                }
            }).dialog("open");

            document.getElementById("method").value = "add";
        }

        //修改
        function edit() {
            //先获取选择行
            var rows = $('#tt').datagrid("getSelections");
            //如果只选择了一行则可以进行修改，否则不操作
            if (rows.length == 1) {
                var row = rows[0];
                //获取要修改的字段


                dlg = $('#dlg').dialog({
                    title: '修改行政区信息',
                    modal: true,
                    onLoad: function () {

                    }
                }).dialog("open");

                document.getElementById("method").value = "modify";
                fm = $('#fm').form('load', row);

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

                $.getJSON('AdministratorAreaHandler.ashx', $("#fm").serialize(), function (result) {
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
                $.getJSON('AdministratorAreaHandler.ashx', $("#fm").serialize(), function (result) {
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
            if (rows == null || rows.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return;
            }

            //取消ajax的异步
            $.ajaxSettings.async = false;

            var flag = false;
            for (var i = 0; i < rows.length; i++) {
                $.getJSON('AdministratorAreaHandler.ashx?method=IsUsed', { id: rows[i].id }, function (result) {
                    if (result.flag) {
                        flag = true;
                    }

                });
            }

            if (flag) {
                alert("该行政区中已经添加了计划或者占补平衡或者批次，请先删除！"); return;
            }

            if (rows) {
                $.messager.confirm('提示', '你确定要删除【' + rows.length + '】条记录吗？', function (r) {
                    if (r) {
                        var ids = [];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);
                        }

                        $.ajax({
                            url: 'AdministratorAreaHandler.ashx?method=delete',
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

            var name = $("#fm_search").find("input[name='name']").val();
            queryParams.name = name;

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
            $("#tb").find("input").val("");

            //重新加载数据
            reloadgrid();
        }

    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="行政区列表" class="easyui-datagrid" border="false" fit="true" style="width: auto; height: 500px;"        
        idfield="id" pagination="true" data-options="iconCls:'icon-tip',rownumbers:true,url:'AdministratorAreaHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'name',width:100,align:'center',sortable:'true'">名称</th>
                <th data-options="field:'pingyin',width:100,align:'center',sortable:'true'">拼音</th>
                <th data-options="field:'initial',width:100,align:'center',sortable:'true'">缩写</th>
                <th data-options="field:'sort',width:100,align:'center',sortable:'true'">排序</th>
                <th data-options="field:'des',width:120,align:'center',sortable:'true'">描述</th>   
            </tr>
        </thead>
         <%--表格内容--%>
    </table>
    <%--功能区--%>
    <div id="tb" style="padding: 5px; height: auto">
        <%-- 包括添加、修改、删除、刷新、全部选中、取消全部选中 --%>
        <div style="margin-bottom: 5px">
            <% if (list_privilege_cur_page.Contains("200")) { 
                    %>
                    <a href="javascript:void(0)" onclick="add() " title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
                    <%    
            } %>
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
            <input id="search_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'计划名称'" style="width:160px">&nbsp;&nbsp;
            
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>
            </form>
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">行政区信息</div>
        <form id="fm" name="fm" method="post">
            <div class="fitem">
                <label>名称：</label>
                <input id="method" name="method" type="hidden" />  
                <input name="id" type="hidden" />  
                <input name="name" class="easyui-validatebox" data-options="required:true"/>
                
            </div>
            <div class="fitem">
                <label>排序：</label>
                <input id="sort" name="sort" class="easyui-numberspinner" style="width:80px;"required="required" data-options="min:0,max:100">
            </div>
            <div class="fitem">
                <label>描述：</label>
                <textarea name="des" style="width:150px;"></textarea>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close')">关闭</a>
    </div>
</body>
</html>