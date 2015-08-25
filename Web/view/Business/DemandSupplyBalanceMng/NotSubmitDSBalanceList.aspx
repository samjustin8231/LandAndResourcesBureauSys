<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotSubmitDSBalanceList.aspx.cs" Inherits="Maticsoft.Web.View.Business.DemandSupplyBalanceMng.NotSubmitDSBalanceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>占补平衡列表</title>

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
    <link href="../../../css/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/members/css.css" rel="stylesheet" type="text/css" />

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
                title: '添加占补平衡',
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
                    title: '修改占补平衡信息',
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

            //验证数量大小关系
            var scale = $("#fm").find("input[name='scale']").val();
            var agriArea = $("#fm").find("input[name='agriArea']").val();
            var arabArea = $("#fm").find("input[name='arabArea']").val();

            if (parseFloat(scale) < parseFloat(agriArea)) {
                alert("【规模>=计划下达农用地】不符合"); return false;
            }
            if (parseFloat(scale) < parseFloat(arabArea)) {
                alert("【规模>=计划下达耕地】不符合"); return false;
            }
            if (parseFloat(agriArea) < parseFloat(arabArea)) {
                alert("【计划下达农用地>=计划下达耕地】不符合"); return false;
            }

            if (method == "add") {

                $.getJSON('DemandSupplyBalanceHandler.ashx', $("#fm").serialize(), function (result) {
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

                $.getJSON('DemandSupplyBalanceHandler.ashx', $("#fm").serialize(), function (result) {
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
                            url: 'DemandSupplyBalanceHandler.ashx?method=delete',
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
            var year = $("#fm_search").find("input[name='year']").val();
            var administratorArea = $("#fm_search").find("input[name='administratorArea']").val();
            var typeId = $("#fm_search").find("input[name='typeId']").val();

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
            queryParams.typeId = typeId;

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
        }

        function formatterIsSubmitedF(val, row, index) {
            if (val == 0)
                return "<font color='green' onclick='submitRecord(" + row.id + ")'>未提交</font>";
            else if (val == 1)
                return "<a href='#' class='isLock' >已提交</a>";
            else
                return val;
        }

        function submitRecord(id) {

            if (id == "" || id == undefined) {
                var rows = $('#tt').datagrid("getSelections");
                var n = 0;
                var ids = "";
                if (rows.length <= 0) {
                    $.messager.alert('警告', '请选择记录提交！');
                    return;
                }

                $.messager.confirm('提示', '你确定要提交【' + rows.length + '】条记录吗？', function (r) {
                    if (r) {
                        if (rows.length > 0) {
                            var nos_ids = "";
                            for (var i = 0; i < rows.length; i++) {
                                nos_ids += rows[i].id + ",";
                            }

                            nos_ids = nos_ids.substring(0, nos_ids.length - 1);   //1,3,5,6,7
                            $.getJSON('DemandSupplyBalanceHandler.ashx?method=submit', { ids: nos_ids }, function (result) {
                                if (result.flag) {
                                    $('#tt').datagrid('reload'); // reload the user data
                                }
                                $.messager.show({ title: '提示', msg: result.msg });
                            });
                        }

                    } else { }
                });
            } else {
                $.messager.confirm('提示', '你确定要提交该记录吗？', function (r) {
                    if (r) {

                        $.getJSON('DemandSupplyBalanceHandler.ashx?method=submit', { ids: id }, function (result) {
                            if (result.flag) {
                                $('#tt').datagrid('reload'); // reload the user data
                            }
                            $.messager.show({ title: '提示', msg: result.msg });
                        });
                    }
                });
            }


        }

        function loadLinkButton(data) {
            $(".isLock").linkbutton({ text: '已提交', plain: true, iconCls: 'icon-lock' });
        }

    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="占补平衡列表" class="easyui-datagrid" border="false" fit="true" style="width: auto; height: 500px;"        
        idfield="id" pagination="true" data-options="iconCls:'icon-tip',rownumbers:true,url:'DemandSupplyBalanceHandler.ashx?method=query&isSubmited=0',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onBeforeOpen:beforeDataGridOpen,onLoadSuccess:loadLinkButton" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'name',width:100,align:'center',sortable:'true'">名称</th>
                <th data-options="field:'year',width:120,align:'center',sortable:'true'">年份</th>   
                <th data-options="field:'administratorAreaName',width:120,align:'center',sortable:'true'">行政区</th>  
                <th data-options="field:'typeName',width:120,align:'center',sortable:'true'">占补平衡类型</th>   
                <th data-options="field:'scale',width:120,align:'center',sortable:'true'">规模</th>   
                <th data-options="field:'agriArea',width:120,align:'center',sortable:'true'">农用地面积</th>   
                <th data-options="field:'arabArea',width:120,align:'center',sortable:'true'">耕地面积</th>   
                <th data-options="field:'occupyArea',width:120,align:'center',sortable:'true'">异地调剂已使用</th>   
                <th data-options="field:'remainArea',width:120,align:'center',sortable:'true'"></th>   
                <th data-options="field:'position',width:120,align:'center',sortable:'true'">位置</th>  
                <th data-options="field:'acceptUnit',width:120,align:'center',sortable:'true'">验收单位</th> 
                <th data-options="field:'acceptNo',width:120,align:'center',sortable:'true'">验收文号</th>   
                <th data-options="field:'isSubmited',width:120,align:'center',sortable:'true',formatter:formatterIsSubmitedF">是否提交</th>  
                <th data-options="field:'acceptTime',width:120,align:'center',sortable:'true'">验收时间</th>    
                <th data-options="field:'createUserName',width:120,align:'center',sortable:'true'">创建人</th>    
                <th data-options="field:'createTime',width:120,align:'center',sortable:'true'">创建时间</th>    
                <th data-options="field:'des',width:120,align:'center',sortable:'true'">描述</th>    
            </tr>
        </thead>
         <%--表格内容--%>
    </table>
    <%--功能区--%>
    <div id="tb" style="padding: 5px; height: auto">
        <%-- 包括添加、修改、删除、刷新、全部选中、取消全部选中 --%>
        <div style="margin-bottom: 5px">
            <a href="javascript:void(0)" onclick="edit() " title="修改" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-edit',plain:true"></a>
            <a href="javascript:void(0)" onclick="del()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
            <a href="javascript:void(0)" onclick="reload()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
            <a href="javascript:void(0)" onclick="unselectall()" title="取消选中" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-undo',plain:true"></a>

            <a href="javascript:void(0)" onclick="submitRecord() " title="提交" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-lock',plain:true"></a>
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
            占补平衡类型: 
            <input name="typeId" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',url:'../DemandSupplyBalanceTypeMng/DemandSupplyBalanceTypeHandler.ashx?method=getList'">
            
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>
            </form>
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons',onBeforeOpen:beforeDialogOpen"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">占补平衡信息</div>
        <form id="fm" name="fm" method="post">
            <div class="fitem">
                <label>名称：</label>
                <input id="method" name="method" type="hidden" />  
                <input  name="id" type="hidden" />  
                <input name="name" class="easyui-validatebox" data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>占补平衡类型：</label>
                <input name="typeId" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../DemandSupplyBalanceTypeMng/DemandSupplyBalanceTypeHandler.ashx?method=getList'">
            </div>
            <div class="fitem">
                <label>年份：</label>
                <input name="year" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
            </div>
            <div class="fitem">
                <label>行政区：</label>
                <input name="administratorArea" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
            </div>
            

            <div class="fitem">
                <label>规模：</label>
                <input name="scale" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>农用地面积：</label>
                <input name="agriArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>耕地面积：</label>
                <input name="arabArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>异地调剂已使用：</label>
                <input name="occupyArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
            </div>
            
            <div class="fitem">
                <label>位置：</label>
                <input name="position" class="easyui-textbox" style="width:150px;" >
            </div>
            <div class="fitem">
                <label>验收单位：</label>
                <input name="acceptUnit" class="easyui-textbox" style="width:150px;" >
            </div>
            <div class="fitem">
                <label>验收文号：</label>
                <input name="acceptNo" class="easyui-textbox" style="width:150px;" >
            </div>
            <div class="fitem">
                <label>验收时间：</label>
                <input name="acceptTime" class="easyui-datebox" style="width:150px;">
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
