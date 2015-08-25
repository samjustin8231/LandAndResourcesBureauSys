<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandSupplyBalanceList.aspx.cs" Inherits="Maticsoft.Web.View.Business.DemandSupplyBalanceMng.DemandSupplyBalanceList" %>

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
    <script src="../../../js/jquery-easyui-1.4.1/datagrid-detailview.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form

        var datagrid;   //
        var editRow = undefined;

        $(function(){
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
                title: '添加占补平衡',
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

                //如果由增减挂钩拆旧调整至补充耕地
                if (row.batchId != "0" && row.batchId != "") {
                    alert("该复垦为拆旧信息自动转入，不允许修改");return;
                }

                //判断该计划是否被批次使用了
                if (row.isUsed == "True") {
                    alert("该记录已经被批次使用，请先删除关系！"); return;
                }

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
            if (method == "add") {

                //提交之前验证
                if (!$("#fm").form('validate')) {
                    return false;
                }

                if ($("#title").val() == "sam") {

                    return false;
                }

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

                //提交之前验证
                if (!$("#fm").form('validate')) {
                    return false;
                }
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
            var flag = false;
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

                            if (rows[i].isUsed == "True") {
                                flag = true;
                            }
                        }

                        //判断该记录是否被批次使用了
                        if (flag) {
                            alert("存在记录已经被批次使用，请先删除关系！"); return;
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
            var isDeleted = $("#isDeleted").combobox("getValue");

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
            queryParams.typeId = typeId;
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
            $("#isDeleted").combobox("setValue", -1);

            //重新加载数据
            reloadgrid();
        }

        /*dialog打开之前*/
        function beforeDialogOpen() {
            //combobox加载年份
            loadYearForCombobox($("#year"));
        }

        /*datagrid打开之前*/
        function beforeDataGridOpen() {
            //combobox加载年份

            loadYearForCombobox($("#searchYear"));
        }

        function formatterIsSubmitedF(val, row, index) {
            if (val == 0)
                return "<font color='green' onclick='submitRecord(" + row.id + ")'>未提交</font>";
            else if (val == 1)
                return "<a href='#' class='isLock' >已提交</a>";
            else
                return val;
        }

        function formatterNameF(value, rowData, rowIndex) {
            if (rowData.batchId != "0") {//增减挂钩计划字段不一样
                return "<span style='font-weight:bold;color:red;'>" + value + "</span>";
            } else {
                return "<span style='font-weight:bold;'>" + value + "</span>";
            }
            
        }

        function formatterAdministratorAreaF(value, rowData, rowIndex) {
            if (rowData.administratorArea == "1") {
                return "<span style='font-weight:bold;color:#9ACD32;'>" + value + "</span>";
            } else if (rowData.administratorArea == "2") {
                return "<span style='font-weight:bold;color:black;'>" + value + "</span>";
            } else if (rowData.administratorArea == "3") {
                return "<span style='font-weight:bold;color:#FF8C00;'>" + value + "</span>";
            } else if (rowData.administratorArea == "4") {
                return "<span style='font-weight:bold;color:#8E388E;'>" + value + "</span>";
            } else if (rowData.administratorArea == "5") {
                return "<span style='font-weight:bold;color:#8B6914;'>" + value + "</span>";
            } else if (rowData.administratorArea == "6") {
                return "<span style='font-weight:bold;color:#7171C6;'>" + value + "</span>";
            } else if (rowData.administratorArea == "7") {
                return "<span style='font-weight:bold;color:#4169E1;'>" + value + "</span>";
            } else {
                return "<span>" + value + "</span>";
            }

        }

        function formatterNumF(val, row, index) {
            if (row.typeId !== "3") {//增减挂钩计划字段不一样
                return "--";
            } else {
                return val;
            }
        }

        function formatterAgriF(val, row, index) {
            if (row.typeId == "3") {//增减挂钩计划字段不一样
                return val;
            } else {
                return "--";
            }
        }

        function formatterAdjustF(val, row, index) {
            if (row.batchId == "0") {//增减挂钩计划字段不一样
                return "<span class='isNo'></span>";
            } else {
                return "<span class='isOk'></span>";
            }
        }

        function stylerNameF(value, row, index) {
            if (row.isUsed == "True") {
                return 'background-color:#EE6A50;';
            } else {
                return 'background-color:#ADFF2F;';
            }
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

            //判断该计划是否被批次使用了
            if (row.batchId != "0" && row.batchId != "") {
                alert("该复垦为拆旧信息自动转入，不允许修改"); return;
            } else {
                //判断该计划是否被批次使用了
                if (row.isUsed == "True") {
                    alert("该计划已经被批次使用，请先删除关系！"); return;
                }
            }

            

            $.getJSON('DemandSupplyBalanceHandler.ashx?method=Lock&id=' + row.id, function (result) {
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
                reload();
            });
        }

        function loadLinkButton(data) {
            $(".isLock").linkbutton({ text: '已提交', plain: true, iconCls: 'icon-lock' });
            $(".isDeleted").linkbutton({ text: '关闭', plain: true, iconCls: 'icon-lock' });
            $(".isNotDeleted").linkbutton({ text: '启用', plain: true, iconCls: 'icon-open' });
            $(".isOk").linkbutton({ text: '', plain: true, iconCls: 'icon-ok' });
            $(".isNo").linkbutton({ text: '', plain: true, iconCls: 'icon-no' });
        }

        /*为obj对象加载年份*/
        function loadYearForCombobox(obj) {
            console.info(obj);
            var date = new Date();
            var curYear = date.getFullYear();
            var minYear = 1995;

            var data, json;
            json = "";
            json += '[';
            for (var i = curYear; i >= minYear; i--) {
                json += '{"id":"' + i + '","name":"' + i + '"},';
            }
            if (json.lastIndexOf(",") > 0) {
                var end = json.lastIndexOf(",");
                json = json.substr(0, end);
            }
            json += ']';
            data = $.parseJSON(json);
            obj.combobox("loadData", data);
        }

        function detailFormatterF(index, row) {
            return '<div style="padding:2px"><div><table id="ddv-' + index + '"></table></div>';  
        }

        function onExpandRowF(index, row) {
            
            $('#ddv-'+index).datagrid({  
                url:'/View/Business/BatchMng/BatchHandler.ashx?method=GetBatchListByDSId&balanceId='+row.id,
                fitColumns:true,  
                singleSelect:true,  
                height:'auto',  
                columns:[[  
                    {field:'id',title:'id',hidden:'true',align:'center'},  
                    {field:'batchName',title:'批次名称',width:200,align:'center'},  
                    {field:'administratorAreaName',title:'行政区',width:'120',align:'center'},  
                    {field:'batchTypeName',title:'批次类型',width:100,align:'center'},  
                    {field:'totalArea',title:'用地总面积',width:150,align:'center'},
                    {field:'hasLevyArea',title:'已征收面积',width:150,align:'center'},
                    {field:'agriAreaBatch',title:'农用地面积',width:150,align:'center'},
                    {field:'arabAreaBatch',title:'耕地面积',width:150,align:'center'},  
                ]],  
                onResize:function(){  
                    $('#tt').datagrid('fixDetailRowHeight',index);  
                },  
                onLoadSuccess:function(){  
                    setTimeout(function(){  
                        $('#tt').datagrid('fixDetailRowHeight',index);  
                    },0);  
                    if (row.typeId != "3") {
                        $('#ddv-' + index).datagrid("hideColumn", "agriAreaBatch");
                    }
                }
            });
            $('#tt').datagrid('fixDetailRowHeight', index);
        }


        //右键菜单响应事件
        function onRowContextMenuF(e, index, row) {
            //阻止浏览器响应
            e.preventDefault();
            //console.info(rowData);
            $("#tt").datagrid("unselectAll");
            $("#tt").datagrid("selectRow", index);
            $("#menu").menu("show", {
                left: e.pageX,
                top: e.pageY
            });
        }
    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="占补平衡列表" class="easyui-datagrid" border="false" fit="true" style="width: auto; height: 500px;"        
        idfield="id" pagination="true" data-options="onRowContextMenu:onRowContextMenuF,onDblClickRow:edit,ctrlSelect:true,view: detailview,iconCls:'icon-tip',url:'DemandSupplyBalanceHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onBeforeOpen:beforeDataGridOpen,onLoadSuccess:loadLinkButton,detailFormatter:detailFormatterF,onExpandRow:onExpandRowF" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'no',width:150,align:'center',sortable:'true'">项目编号</th>
                <th data-options="field:'name',width:100,align:'center',sortable:'true',styler:stylerNameF,formatter:formatterNameF">名称</th>
                <th data-options="field:'year',width:120,align:'center',sortable:'true'">年份</th>   
                <th data-options="field:'administratorAreaName',width:120,align:'center',sortable:'true',formatter:formatterAdministratorAreaF">行政区</th>  
                <th data-options="field:'typeName',width:120,align:'center',sortable:'true'">占补平衡类型</th>   
                <th data-options="field:'scale',width:120,align:'center',sortable:'true'">规模</th>   
                <th data-options="field:'agriArea',width:120,align:'center',sortable:'true',formatter:formatterAgriF">农用地面积</th>   
                <th data-options="field:'arabArea',width:120,align:'center',sortable:'true'">耕地面积</th>   
                <th data-options="field:'occupyArea',width:120,align:'center',sortable:'true'">异地调剂已使用</th>   
                
                <th data-options="field:'acceptNo',width:120,align:'center',sortable:'true'">验收文号</th>   
                <th data-options="field:'acceptTime',width:120,align:'center',sortable:'true'">验收时间</th> 
                <th data-options="field:'isDeleted',width:100,align:'center',sortable:'true',formatter:formatterStateF">状态</th>  
                <!-- <th data-options="field:'createUserName',width:120,align:'center',sortable:'true'">创建人</th>    
                <th data-options="field:'des',width:120,align:'center',sortable:'true'">备注</th>
                <th data-options="field:'adjustArea',width:150,align:'center',sortable:'true',formatter:formatterNumF">调整至补充至耕地</th>   
                <th data-options="field:'isAdjust',width:120,align:'center',sortable:'true',formatter:formatterAdjustF">是否由拆旧调整</th> 
                <th data-options="field:'batchId',width:120,align:'center',sortable:'true'">批次Id</th> 
                <th data-options="field:'acceptUnit',width:120,align:'center',sortable:'true'">验收单位</th> 
                <th data-options="field:'position',width:120,align:'center',sortable:'true'">位置</th>  
                <th data-options="field:'isSubmited',width:150,align:'center',sortable:'true',formatter:formatterIsSubmitedF">是否提交</th>   
                <th data-options="field:'createTime',width:120,align:'center',sortable:'true'">创建时间</th>    
                <th data-options="field:'des',width:120,align:'center',sortable:'true'">描述</th> -->   
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

            <span style="float:right;margin-right:50px;">                      
                <a style="margin-top:3px;display:inline-block;width:11px;height:11px;background-color:#ADFF2F;"></a>&nbsp;未使用&nbsp;
                <a style="margin-top:3px;display:inline-block;width:11px;height:11px;background-color:#EE6A50;"></a>&nbsp;已使用
            </span>
        </div>
        <%-- 查找Account信息，根据注册时间、姓名 --%>
        <div>
           <form id="fm_search" method="post">
           名称: 
            <input id="search_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'占补平衡名称'" style="width:160px">&nbsp;&nbsp;
            年份: 
            <input id="searchYear" name="year" class="easyui-combobox" style="width:100px;" data-options="valueField:'id',textField:'name'">
            行政区: 
            <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">&nbsp;&nbsp;
            占补平衡类型: 
            <input name="typeId" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',url:'../DemandSupplyBalanceTypeMng/DemandSupplyBalanceTypeHandler.ashx?method=getList'">&nbsp;&nbsp;
            
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
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons',onBeforeOpen:beforeDialogOpen"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">占补平衡信息</div>
        <form id="fm" name="fm" method="post">
            <div class="fitem">
                <label>占补平衡类型：</label>
                <input name="typeId" class="easyui-combobox" style="width:150px;" disabled="disabled" data-options="valueField:'id',textField:'name',required:true,url:'../DemandSupplyBalanceTypeMng/DemandSupplyBalanceTypeHandler.ashx?method=getList'">
            </div>
            <div class="fitem">
                <label>项目编号：</label>
                <input name="no" class="easyui-validatebox" data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>名称：</label>
                <input id="method" name="method" type="hidden" />  
                <input  name="id" type="hidden" />  
                <input name="name" class="easyui-validatebox" data-options="required:true"/>
            </div>
            
            <div class="fitem">
                <label>年份：</label>
                <input id="year" name="year" class="easyui-combobox" style="width:150px;" data-options="valueField:'id',textField:'name',required:true">
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
                <textarea name="des" style="width:150px;height:40px;"></textarea>
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
