<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmlandAdjustmentList.aspx.cs" Inherits="Maticsoft.Web.View.Business.FarmlandAdjustmentMng.FarmlandAdjustmentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>基本农田调整列表</title>

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

        //添加
        function add() {
            //清空内容
            fm = $('#fm').form('clear');

            dlg = $('#dlg').dialog({
                title: '添加基本农田调整',
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
                    title: '修改基本农田调整信息',
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

        function onChangeF(newValue, oldValue) {
            //判断该行政区的、该年份的是否已经添加
            var year = $("#year").combobox("getValue");
            var administratorArea = $("#administratorArea").combobox("getValue")
            if (year != "" && administratorArea != "") {
                document.getElementById("method").value = "IfHasAddSameCondition";
                $.getJSON('FarmlandAdjustmentHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        alert("该行政区的同一年份核销信息已经添加，请重新选择");
                        $("#year").combobox("setValue", ""); $("#administratorArea").combobox("setValue", ""); return;
                    }
                });
            }
        }

        //保存信息
        function save() {
            var method = $("#method").val();

            //提交之前验证
            if (!$("#fm").form('validate')) {
                return false;
            }

            //验证数量大小关系
            var consArea = $("#fm").find("input[name='consArea']").val();
            var agriArea = $("#fm").find("input[name='agriArea']").val();
            var arabArea = $("#fm").find("input[name='arabArea']").val();

            if (parseFloat(consArea) < parseFloat(agriArea)) {
                alert("【基本农田调整下达新增建设用地>=基本农田调整下达农用地】不符合"); return false;
            }
            if (parseFloat(consArea) < parseFloat(arabArea)) {
                alert("【基本农田调整下达新增建设用地>=基本农田调整下达耕地】不符合"); return false;
            }
            if (parseFloat(agriArea) < parseFloat(arabArea)) {
                alert("【基本农田调整下达农用地>=基本农田调整下达耕地】不符合"); return false;
            }

            var id = $("#fm").find("input[name='id']").val();
            if (id == "") {
                document.getElementById("method").value = "add";
                $.getJSON('FarmlandAdjustmentHandler.ashx', $("#fm").serialize(), function (result) {
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
                document.getElementById("method").value = "modify";
                $.getJSON('FarmlandAdjustmentHandler.ashx', $("#fm").serialize(), function (result) {
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
            if (rows == null || rows.length == 0) {
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
                            url: 'FarmlandAdjustmentHandler.ashx?method=delete',
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
                                    $.messager.show({ title: '提示', msg: result.msg });
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
            var year = $("#fm_search").find("input[name='year']").val();
            var administratorArea = $("#fm_search").find("input[name='administratorArea']").val();

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
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

        /*dialog打开之前*/
        function beforeDialogOpen() {

        }


        /*datagrid打开之前*/
        function beforeDataGridOpen() {
            //
        }

    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="基本农田调整列表" class="easyui-datagrid" border="false" fit="true" style="width: auto; height: 500px;"        
        idfield="id" pagination="true" data-options="iconCls:'icon-tip',ctrlSelect:true,onDblClickRow:edit,rownumbers:true,url:'FarmlandAdjustmentHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onBeforeOpen:beforeDataGridOpen" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'year',align:'center',sortable:'true'">年份</th>   
                <th data-options="field:'administratorAreaName',align:'center',sortable:'true'">行政区</th>  
                <th data-options="field:'verifId',align:'center',sortable:'true'">verifId</th>   
                <th data-options="field:'divisionArea',align:'center',sortable:'true'">多划基本农田面积</th>   

                <th data-options="field:'taskArea',align:'center',sortable:'true'">基本农田保护任务</th> 
                <th data-options="field:'adjustBeforeArea',align:'center',sortable:'true'">调整前土地面积</th> 
                <th data-options="field:'adjustBeforeArableArea',align:'center',sortable:'true'">调整前耕地面积</th> 
                <th data-options="field:'adjustOutArea',align:'center',sortable:'true'">调出土地面积</th> 
                <th data-options="field:'adjustOutArableArea',align:'center',sortable:'true'">调出耕地面积</th> 
                <th data-options="field:'adjustInArea',align:'center',sortable:'true'">调入土地面积</th> 
                <th data-options="field:'adjustInArableArea',align:'center',sortable:'true'">调入耕地面积</th> 

                <th data-options="field:'adjustAfterArea',align:'center',sortable:'true'">调整后土地面积</th> 
                <th data-options="field:'adjustAfterArableArea',align:'center',sortable:'true'">调整后耕地面积</th> 

                <!-- <th data-options="field:'createUserName',align:'center',sortable:'true'">创建人</th>    
                <th data-options="field:'createTime',align:'center',sortable:'true'">创建时间</th>    
                <th data-options="field:'des',align:'center',sortable:'true'">描述</th>    -->
            </tr>
        </thead>

        <thead frozen="true">
            <tr>
                
                
            </tr>
        </thead>
         <%--表格内容--%>
    </table>
    <%--功能区--%>
    <div id="tb" style="padding: 5px; height: auto">
        <%-- 包括添加、修改、删除、刷新、全部选中、取消全部选中 --%>
        <div style="margin-bottom: 5px">
            <a href="javascript:void(0)" onclick="add()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
            <a href="javascript:void(0)" onclick="edit() " title="修改" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-edit',plain:true"></a>
            <a href="javascript:void(0)" onclick="del()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
            <a href="javascript:void(0)" onclick="reload()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
            <a href="javascript:void(0)" onclick="unselectall()" title="取消选中" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-undo',plain:true"></a>

            
        </div>
        <%-- 查找Account信息，根据注册时间、姓名 --%>
        <div>
           <form id="fm_search" method="post">
           名称: 
            <input name="name"/>&nbsp;
            年份: 
            <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
            行政区: 
            <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>
            </form>
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons',onBeforeOpen:beforeDialogOpen"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">基本农田调整信息</div>
        <form id="fm" name="fm" method="post">
            <div class="fitem">
                <input id="method" name="method" type="hidden" />  
                <input  name="id" type="hidden" />  
            </div>
            <div class="fitem">
                <label>年份：</label>
                <input id="year" name="year" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList',onChange:onChangeF">
            </div>
            <div class="fitem">
                <label>行政区：</label>
                <input id="administratorArea" name="administratorArea" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList',onChange:onChangeF">
            </div>
            <div class="fitem">
                <label>基本农田核销：</label>
                <input name="verifId" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../VerifMng/VerifHandler.ashx?method=getList'">
            </div>

            <div class="fitem">
                <label>基本农田保护任务：</label>
                <input name="taskArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>调整前土地面积：</label>
                <input name="adjustBeforeArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>调整前耕地面积：</label>
                <input name="adjustBeforeArableArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>

            <div class="fitem">
                <label>调出土地面积：</label>
                <input name="adjustOutArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>调出耕地面积：</label>
                <input name="adjustOutArableArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>

            <div class="fitem">
                <label>调入土地面积：</label>
                <input name="adjustInArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>调入耕地面积：</label>
                <input name="adjustInArableArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>描述：</label>
                <textarea name="des" style="width:150px;"></textarea>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close');">关闭</a>
    </div>
</body>
</html>
