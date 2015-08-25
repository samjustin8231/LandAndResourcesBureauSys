<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevyCompensateList.aspx.cs" Inherits="Maticsoft.Web.View.Business.LevyCompensateMng.LevyCompensateList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>征收补偿列表</title>

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

    <style type="text/css">
        #fm th{width:120px;}
    </style>

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
                title: '添加征收补偿',
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
                    title: '修改征收补偿信息',
                    modal: true,
                    onLoad: function () {

                    }
                }).dialog("open");

                document.getElementById("method").value = "modify";
                editRow = row;


                $.ajaxSettings.async = false;
                BindProvinceCity();
                //fm = $('#fm').form('load', row);

                $.getJSON("/View/Business/LevyCompensateMng/LevyCompensateHandler.ashx?method=getModelById&id=" + row.id, function (info) {
                    $("#fm").find("input[type=hidden]").val(info._id);
                    $("#fm").find("input[name='year']").val(info._year);
                    $("#fm").find("input[name='administratorArea']").val(info._administratorarea);
                    document.getElementById("method").value = "modify";

                    $("#batchTypeId").combobox('setValue', info._batchtypeid);
                    $("#batchId").combobox('setValue', info._batchid);
                    $("#landblockId").combobox('setValue', info._landblockid);

                    $("#planLevyArea").numberbox('setValue', info._planlevyarea);
                    $("#levyNationalLandArea").numberbox('setValue', info._levynationallandarea);
                    $("#levyColletLandArea").numberbox('setValue', info._levycolletlandarea);

                    $("#town").textbox('setValue', info._town);
                    $("#village").textbox('setValue', info._village);

                    $("#_group").textbox('setValue', info.__group);

                    $("#hasLevyArea").numberbox('setValue', info._haslevyarea);
                    $("#countrySocialSecurityFund").numberbox('setValue', info._countrysocialsecurityfund);
                    $("#totalPeopleNumber").numberbox('setValue', info._totalpeoplenumber);
                    $("#areaWaterFacilitiesCompensate").numberbox('setValue', info._areawaterfacilitiescompensate);
                    $("#provHeavyAgriculturalFunds").numberbox('setValue', info._provheavyagriculturalfunds);
                    $("#provAdditionalFee").numberbox('setValue', info._provadditionalfee);

                    $("#provReclamationFee").numberbox('setValue', info._provreclamationfee);
                    $("#provArableLandTax").numberbox('setValue', info._countrysocialsecurityfund);
                    $("#provSurveyFee").numberbox('setValue', info._provsurveyfee);
                    $("#provServiceFee").numberbox('setValue', info._provservicefee);
                    $("#levyPreDeposit").numberbox('setValue', info._levypredeposit);
                    $("#countryCompensate").numberbox('setValue', info._countrycompensate);

                    $("#des").text(info._des);
                });

            } else {
                $.messager.alert("提示", "请选择要一行修改！", "info");
            }
        }

        //保存信息
        function save() {
            var method = $("#method").val();

            //提交之前验证
            if (!$("#fm").form('validate')) {
                return false;
            }

            if($("#batchTypeId").combobox('getValue')=="0"){
                alert("请选择批次类型");
                return false;
            }
            if($("#batchId").combobox('getValue')=="0"){
                alert("请选择批次");return false;
            }
            if($("#landblockId").combobox('getValue')=="0"){
                alert("请选择地块");return false;
            }

            //验证数量大小关系
            
            var planLevyArea = $("#planLevyArea").numberbox('getValue');
            var levyNationalLandArea = $("#levyNationalLandArea").numberbox('getValue');
            var levyColletLandArea = $("#levyColletLandArea").numberbox('getValue');
            var remainArea = $("#fm").find("span[name='remainArea']").text();

            if (parseFloat(planLevyArea) != parseFloat(levyNationalLandArea)+parseFloat(levyColletLandArea)) {
                alert("【拟征收面积 = 收回国有土地 + 征收集体土地】不符合"); return false;
            }
            if (parseFloat(planLevyArea) > parseFloat(remainArea)) {
                alert("【拟征收面积 <= 地块剩余面积】不符合"); return false;
            }
            if (method == "add") {

                $.getJSON('LevyCompensateHandler.ashx', $("#fm").serialize(), function (result) {
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

                $.getJSON('LevyCompensateHandler.ashx', $("#fm").serialize(), function (result) {
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
                            url: 'LevyCompensateHandler.ashx?method=delete',
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
            var isDeleted = $("#isDeleted").combobox("getValue");

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
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

        }


        /*datagrid打开之前*/
        function beforeDataGridOpen() {
            //
        }


        function editHasLevy() {
            //先获取选择行
            var rows = $('#tt').datagrid("getSelections");
            //如果只选择了一行则可以进行修改，否则不操作
            if (rows.length == 1) {
                $('#dlg_haslevy').dialog({
                    title: '修改已征收面积',
                    modal: true,
                    onLoad: function () {
                        
                    }
                }).dialog("open");
                console.info(rows[0]);
                //$("#fm_haslevy").find("input[name='planLevyArea']").val(row[0].hasLevyArea);
                $('#fm_haslevy').form('load', rows[0]);
            } else {
                $.messager.alert("提示", "请选择要一行修改！", "info");
            }
        }

        function saveHasLevy() {

            //提交之前验证
            if (!$("#fm_haslevy").form('validate')) {
                return false;
            }

            //验证数量大小关系

            var planLevyArea = $("#fm_haslevy").find("input[name='planLevyArea']").val();
            var hasLevyArea = $("#hasLevyArea").numberbox("getValue");

            if (parseFloat(hasLevyArea) > parseFloat(planLevyArea)) {
                alert("【已征收面积 <= 拟征收面积】不符合"); return false;
            }

            $.getJSON('LevyCompensateHandler.ashx?method=EditHasLevyArea', $("#fm_haslevy").serialize(), function (result) {
                if (result.flag) {
                    $('#dlg_haslevy').dialog('close'); 	// close the dialog
                    $('#tt').datagrid('reload'); // reload the user data
                    $("#tt").datagrid('unselectAll');
                }
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
            });
        }
        function formatterPlanLevyF(val, row, index) {
            return "<span style='color:#056DAB'>" + val + "</span>";
        }

        function formatterHasLevyF(val, row, index) {
            return "<span style='color:green'>" + val + "</span>";
        }

        function formatterLevyRateF(val, row, index) {
            var rate = parseFloat(row['hasLevyArea']) / parseFloat(row['planLevyArea']) * 100;
            return "<span style='color:red'>" + rate+"%" + "</span>";
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

            $.getJSON('LevyCompensateHandler.ashx?method=Lock&id=' + row.id, function (result) {
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
                reload();
            });
        }

        //加载dagargid中图片
        function loadLinkButton() {
            $(".isDeleted").linkbutton({ text: '关闭', plain: true, iconCls: 'icon-lock' });
            $(".isNotDeleted").linkbutton({ text: '启用', plain: true, iconCls: 'icon-open' });
        }

        function onDblClickRowF(index, row) {
            edit();
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
    <table id="tt" title="征收补偿列表" class="easyui-datagrid" border="false" fit="true" fitColumns="false" style="width: auto; height: 500px;"        
        idfield="id" pagination="true" data-options="ctrlSelect:true,iconCls:'icon-tip',rownumbers:true,url:'LevyCompensateHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onBeforeOpen:beforeDataGridOpen,onDblClickRow:onDblClickRowF,onRowContextMenu:onRowContextMenuF,onLoadSuccess:loadLinkButton" fitcolumns="false"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'administratorAreaName',width:80,align:'center',sortable:'true'">行政区</th>  
                <th data-options="field:'batchName',width:100,align:'center',sortable:'true'">批次名称</th>
                <th data-options="field:'batchTypeName',width:100,align:'center',sortable:'true'">批次类型</th>
                <th data-options="field:'planLevyArea',align:'center',width:100,sortable:'true',formatter:formatterPlanLevyF">拟征收土地</th> 
                <th data-options="field:'hasLevyArea',align:'center',sortable:'true',width:100,formatter:formatterHasLevyF">已征收土地</th>   
                <th data-options="field:'hasLevyRate',align:'center',sortable:'true',width:100,formatter:formatterLevyRateF">征收率</th>   
                <th data-options="field:'levyNationalLandArea',width:100,align:'center',sortable:'true'">征收国有土地</th>   
                <th data-options="field:'levyColletLandArea',width:100,align:'center',sortable:'true'">征收集体土地地</th>   
                <th data-options="field:'totalPeopleNumber',width:100,align:'center',sortable:'true',formatter:formatterHasLevyF">安置人数</th>   
                <th data-options="field:'isDeleted',width:100,align:'center',sortable:'true',formatter:formatterStateF">状态</th>
                <th data-options="field:'town',align:'center',width:100,sortable:'true'">乡镇</th>   
                <th data-options="field:'village',align:'center',width:100,sortable:'true'">村</th>   
                <th data-options="field:'_group',align:'center',width:100,sortable:'true'">村中小组</th>   
                
                <th data-options="field:'levyPreDeposit',align:'center',width:100,sortable:'true'">征地预存款</th>  
                <th data-options="field:'countrySocialSecurityFund',width:100,align:'center',sortable:'true'">乡下社保资金</th>  
                <th data-options="field:'countryCompensate',align:'center',width:100,sortable:'true'">乡下对下补偿</th>  
                <th data-options="field:'areaWaterFacilitiesCompensate',width:100,align:'center',sortable:'true'">区级水利设施补偿</th>  
                <th data-options="field:'provHeavyAgriculturalFunds',align:'center',width:100,sortable:'true'">省市农重金</th>  
                <th data-options="field:'provAdditionalFee',align:'center',width:100,sortable:'true'">省市新增费</th>  
                <th data-options="field:'provReclamationFee',align:'center',width:100,sortable:'true'">省市开垦费</th>  
                <th data-options="field:'provArableLandTax',align:'center',width:100,sortable:'true'">省市耕占税</th>  
                <th data-options="field:'provSurveyFee',align:'center',width:100,sortable:'true'">省市勘测费</th>  
                <th data-options="field:'provServiceFee',align:'center',width:100,sortable:'true'">省市服务费</th>  
                
                <!--<th data-options="field:'createUserName',align:'center',sortable:'true'">创建人</th>    
                <th data-options="field:'createTime',align:'center',sortable:'true'">创建时间</th>    
                <th data-options="field:'des',align:'center',sortable:'true'">描述</th>  -->
            </tr>

        </thead>
        <thead frozen="true">
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'year',width:80,align:'center',sortable:'true'">年份</th>   
                
                <th data-options="field:'landBlockName',width:100,align:'center',sortable:'true'">地块名称</th>
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
           <input id="search_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'征收补偿名称'" style="width:160px">&nbsp;&nbsp;

            年份: 
            <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
            行政区: 
            <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">&nbsp;&nbsp;
            
            <select id="isDeleted" class="easyui-combobox" name="dept" style="width:100px;">    
                <option value="-1">-- 请选择 --</option>    
                <option  value="0">启用</option>    
                <option  value="1">关闭</option>
            </select>&nbsp;&nbsp;&nbsp;&nbsp;

            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>&nbsp;&nbsp;&nbsp;&nbsp;

            
            <% if (list_privilege_cur_page.Contains("1300")) { 
                %>
                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-edit'" onclick="editHasLevy()">修改已征收面积</a>
                <%    
            } %>
            </form>
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 850px; height: auto; padding: 10px 20px"
        data-options="collapsible:true,closed:true,buttons:'#dlg-buttons',onBeforeOpen:beforeDialogOpen"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">征收补偿信息</div>
        <form id="fm" name="fm" method="post">
            <table width="99%" cellspacing="0" cellpadding="1" border="0" align="center">
                <tr>
                    <input id="method" name="method" type="hidden" />  
                    <input  name="id" type="hidden" />
                    <input  name="year" type="hidden" />
                    <input  name="administratorArea" type="hidden" />    

                    <th>批次种类</th>
                    <td><select id="batchTypeId" name="batchTypeId" style="width:120px;" class="easyui-combobox" data-options="required:true"></select></td>
                    <th>批次号</th>
                    <td><select id="batchId" name="batchId" style="width:120px;" class="easyui-combobox" data-options="required:true"></select></td>
                    <th>地块</th>
                    <td><select id="landblockId" name="landblockId" style="width:120px;" class="easyui-combobox" data-options="required:true"></select></td>
                </tr>
                <tr>
                    <td colspan="2">
                        年份：<span name="year" style="color:Red;" >未选择</span>&nbsp;
                        行政区：<span name="administratorAreaName" style="color:Red;">未选择</span></td>
                    <td colspan="2">
                        地块总面积：<span name="totalArea" style="color:Red;" >未选择</span>&nbsp;
                        剩余面积：<span name="remainArea" style="color:Red;">未选择</span></td>
                </tr>
                <tr>
                    <th>拟征收面积</th>
                    <td><input id="planLevyArea" name="planLevyArea" type="text" style="width:120px;" class="easyui-numberbox" data-options="required:true"/></td>
                    <th>收回国有土地</th>
                    <td><input id="levyNationalLandArea" name="levyNationalLandArea" type="text" style="width:120px;" class="easyui-numberbox" data-options="required:true"/></td>
                    <th>征收集体土地</th>
                    <td><input id="levyColletLandArea" name="levyColletLandArea" type="text" style="width:120px;" class="easyui-numberbox" data-options="required:true"/></td>
                </tr>
                <tr>
                    <th>乡镇</th>
                    <td><input id="town" name="town" type="text" style="width:120px;" class="easyui-textbox"/></td>
                    <th>村（社区、单位）</th>
                    <td><input id="village" name="village" type="text" style="width:120px;" class="easyui-textbox"/></td>
                    <th>小组</th>
                    <td><input id="_group" name="_group" type="text" style="width:120px;" class="easyui-textbox"/></td>
                </tr>
                <tr>
                    <th>已征收</th>
                    <td><input id="hasLevyArea" name="hasLevyArea" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>乡下社保资金</th>
                    <td><input id="countrySocialSecurityFund" name="countrySocialSecurityFund" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>安置人数</th>
                    <td><input id="totalPeopleNumber" name="totalPeopleNumber" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    
                </tr>
                <tr>
                    <th>区级水利设施补偿</th>
                    <td><input id="areaWaterFacilitiesCompensate" name="areaWaterFacilitiesCompensate" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>省市农重金</th>
                    <td><input id="provHeavyAgriculturalFunds" name="provHeavyAgriculturalFunds" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>省市新增费</th>
                    <td><input id="provAdditionalFee" name="provAdditionalFee" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                </tr>
                <tr>
                    <th>省市开垦费</th>
                    <td><input id="provReclamationFee" name="provReclamationFee" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>省市耕占税</th>
                    <td><input id="provArableLandTax" name="provArableLandTax" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>省市勘测费</th>
                    <td><input id="provSurveyFee" name="provSurveyFee" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                </tr>
                <tr>
                    <th>省市服务费</th>
                    <td><input id="provServiceFee" name="provServiceFee" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>征地预存款</th>
                    <td><input id="levyPreDeposit" name="levyPreDeposit" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                    <th>乡下对下补偿</th>
                    <td><input id="countryCompensate" name="countryCompensate" type="text" style="width:120px;" class="easyui-numberbox"/></td>
                </tr>
                <tr>
                    <th>备注</th>
                    <td colspan="4"><textarea id="des" name="des" style="width:300px;"></textarea></td>
                </tr>
            </table>

            
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close');">关闭</a>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg_haslevy" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-haslevy-buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">已征收信息</div>
        <form id="fm_haslevy" method="post">
            <div class="fitem">
                <label>拟征收面积：</label>
                <input id="id" name="id" type="hidden" />
                <input name="planLevyArea" type="text" disabled="disabled">  
            </div>
            <div class="fitem">
                <label>已征收面积：</label>
                <input id="hasLevyArea" name="hasLevyArea" class="easyui-numberbox" style="width:154px;" required="required" data-options="">
            </div>
            <div class="fitem">
                <label>已安置人口：</label>
                <input name="totalPeopleNumber" class="easyui-numberbox" style="width:154px;" required="required" data-options="">
            </div>
        </form>
    </div>
    <div id="dlg-haslevy-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="saveHasLevy()">保存</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_haslevy').dialog('close')">关闭</a>
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
        <% if (list_privilege_cur_page.Contains("1300")) { 
                %>
                <div onclick="editHasLevy();" iconcls="icon-edit1">
            修改已征收面积</div>
                <%    
        } %>
        


    </div>
</body>
</html>

<script type="text/javascript">
    var batchTypeId;
    var batchId;
    var landblockId;

    $(function () {
        
        
    });

    //绑定省份、城市、行政区信息
    function BindProvinceCity() {


        var batchTypeId = $('#batchTypeId').combobox({
            valueField: 'id', //值字段
            textField: 'name', //显示的字段
            url: '/View/Business/BatchTypeMng/BatchTypeHandler.ashx?method=getList',
            editable: true,
            onChange: function (newValue, oldValue) {
                $.get('/View/Business/BatchMng/BatchHandler.ashx?method=getListByTypeId', { typeId: newValue }, function (data) {
                    batchId.combobox("clear").combobox('loadData', data).combobox("setValue","0");
                    landblockId.combobox("clear").combobox("setValue", "0"); ;

                    
                }, 'json');
            }
        });

        var batchId = $('#batchId').combobox({
            valueField: 'id', //值字段
            textField: 'name', //显示的字段
            editable: true,
            onChange: function (newValue, oldValue) {

                //获取批次年份、行政区
                if (newValue == 0 || newValue == "0") {
                    $("#fm").find("span[name='year']").text("未选择");
                    $("#fm").find("span[name='administratorAreaName']").text("未选择");

                    $("#fm").find("input[name='year']").val("");
                    $("#fm").find("input[name='administratorArea']").val("");
                }else{
                    $.get('/View/Business/BatchMng/BatchHandler.ashx?method=getModelById', { id: newValue }, function (data) {
                        $("#fm").find("span[name='year']").text(data._year);
                        $("#fm").find("input[name='year']").val(data._year);
                        //获取行政区
                        $.getJSON("/View/Sys/ajax/UtilHandler.ashx?method=getAdministratorNameById&id=" + data._administratorarea, function (data) {
                            $("#fm").find("span[name='administratorAreaName']").text(data._name);
                            $("#fm").find("input[name='administratorArea']").val(data._id);
                        });
                    }, 'json');

                    $.get('/View/Business/LandBlockMng/LandBlockHandler.ashx?method=GetListForCombo', { batchId: newValue }, function (data) {
                        landblockId.combobox("clear").combobox('loadData', data).combobox("setValue", "0");
                    }, 'json');
                }

                
            }
        });

        var landblockId = $('#landblockId').combobox({
            valueField: 'id', //值字段
            textField: 'name', //显示的字段
            editable: true,
            onChange: function (newValue, oldValue) {
                //获取批次年份、行政区
                if (newValue == 0 || newValue == "0") {
                    $("#fm").find("span[name='totalArea']").text("未选择");
                    $("#fm").find("span[name='remainArea']").text("未选择");
                } else {
                    
                    $.get('/View/Business/LandBlockMng/LandBlockHandler.ashx?method=getModelById', { id: newValue }, function (data) {
                        $("#fm").find("span[name='totalArea']").text(data._area);
                    }, 'json');

                    //回显剩余面积
                    $.getJSON("../LandBlockMng/LandBlockHandler.ashx?method=getRemainLandBlock", { landblockId: newValue }, function (data) {
                        $("#fm").find("span[name='remainArea']").text(data.msg);
                    });
                }
            }
        });
    }

</script>