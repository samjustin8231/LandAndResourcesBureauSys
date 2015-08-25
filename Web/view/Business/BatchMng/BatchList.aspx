<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchList.aspx.cs" Inherits="Maticsoft.Web.View.Business.BatchMng.BatchList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批次列表</title>

    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/datagrid-detailview.js" type="text/javascript"></script>
    <link href="../../../js/kindeditor-4.1.10/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>
    <link href="../../../css/members/css.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        fieldset{border:1px solid #238914;}
        
        .green{color:Green !important;}
        .red{color:red !important;}
        .fitem label {
            display: inline-block;
            width: 140px;
            text-align:right;
        }
        .label_center{text-align:center !important;}
    </style>

    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form
        var _dialog;

        var dlg_plans;
        var dlg_ds_balances;
        var dlg_block;

        var dlg_plan;

        var ISOneRow =1;//判断是否是 datagrid的一行被触发  1表示是一行被点击，0表示超链接被点击;
        /*---------------------------------------------------------1、通用操作 -------------------------------------------------------*/

        $(function(){
            $('#search_name').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    reloadgrid();
                }
            });

            $('#search_plan_name').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    reloadgridPlanDG();
                }
            });

            $('#search_ds_balance_name').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    reloadgridDSBalance();
                }
            });
        });

        //添加
        function add() {
            //清空内容
            fm = $('#fm').form('clear');

            dlg = $('#dlg').dialog({
                title: '添加批次',
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

                if(parseFloat(row.totalUsed)!=0){
                    alert("该批次已经使用了地块，请先删除关系！"); return;
                }
                if(parseFloat(row.agriArea)!=0){
                    alert("该批次已经使用了计划，请先删除关系！"); return;
                }
                if(parseFloat(row.arabUsed)!=0){
                    alert("该批次已经使用了耕地，请先删除关系！"); return;
                }

                dlg = $('#dlg').dialog({
                    title: '修改批次信息',
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
            //提交之前验证
            if (!$("#fm").form('validate')) {
                return false;
            }

            //验证数量大小关系
            var totalArea = $("#fm").find("input[name='totalArea']").val();
            var addConsArea = $("#fm").find("input[name='addConsArea']").val();
            var consArea = $("#fm").find("input[name='consArea']").val();
            var agriArea = $("#fm").find("input[name='agriArea']").val();
            var arabArea = $("#fm").find("input[name='arabArea']").val();
            var unusedArea = $("#fm").find("input[name='unusedArea']").val();

            if (parseFloat(totalArea) != parseFloat(addConsArea) + parseFloat(consArea)) {
                alert("【用地总面积=新增建设用地+建设用地面积】不符合"); return false;
            }
            if (parseFloat(addConsArea) != parseFloat(agriArea) + parseFloat(unusedArea)) {
                alert("【新增建设用地=农用地面积+未利用地面积】不符合"); return false;
            }
            if (parseFloat(consArea) < parseFloat(agriArea)) {
                alert("【建设用地面积>=农用地面积】不符合"); return false;
            }
            if (parseFloat(consArea) < parseFloat(arabArea)) {
                alert("【建设用地面积>=耕地面积】不符合"); return false;
            }
            if (parseFloat(agriArea) < parseFloat(arabArea)) {
                alert("【农用地面积>=耕地面积】不符合"); return false;
            }

            if (method == "add") {
                $.getJSON('BatchHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').datagrid('reload'); // reload the user data
                        $("#tt").datagrid('unselectAll');

                        $.messager.show({ title: '提示', msg: result.msg });
                    } else {
                        $.messager.show({ title: '提示', msg: result.msg });
                    }
                });
            }
            else//修改
            {
                var row = $('#tt').datagrid('getSelected');
                $.getJSON('BatchHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').datagrid('reload'); // reload the user data
                        $("#tt").datagrid('unselectAll');

                        $.messager.show({ title: '提示', msg: result.msg });
                    } else {
                        $.messager.show({ title: '提示', msg: result.msg });
                    }
                });
            }
        }

        //删除
        function del() {
            var flagTotal = false;
            var flagAgri = false;
            var flagArab = false;
            var method = document.getElementById("method").value = "delete";
            var rows = $('#tt').datagrid('getSelections');
            if (rows == null || rows.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return;
            }

            //取消ajax的异步
            $.ajaxSettings.async = false;
            var flag = false;
            for (var i = 0; i < rows.length; i++) {
                if(parseFloat(rows[i].totalUsed)!=0){
                    flagTotal = true;
                }
                if(parseFloat(rows[i].agriArea)!=0){
                    flagAgri = true;
                }
                if(parseFloat(rows[i].arabUsed)!=0){
                    flagArab = true
                }

                $.getJSON('BatchHandler.ashx?method=IsUsedLevy', { id: rows[i].id }, function (result) {
                    if (result.flag) {
                        flag = true;
                    }

                });
            }

            //判断该计划是否被批次使用了
            if (flag) {
                alert("该批次中已经被征收补偿使用，请先删除！"); return;
            }
            if(flagTotal){
                alert("该批次已经使用了地块，请先删除关系！"); return;
            }
            if(flagAgri){
                alert("该批次已经使用了计划，请先删除关系！"); return;
            }
            if(flagArab){
                alert("该批次已经使用了耕地，请先删除关系！"); return;
            }
            

            if (rows) {
                $.messager.confirm('提示', '你确定要删除【' + rows.length + '】条记录吗？', function (r) {
                    if (r) {
                        var ids = [];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);

                        }

                        $.ajax({
                            url: 'BatchHandler.ashx?method=delete',
                            data: {
                                ids: ids.join(",")
                            },
                            method: 'post',
                            dataType: 'json',
                            success: function (result) {
                                if (result.flag) {
                                    $('#dlg').dialog('close'); 	// close the dialog
                                    $('#tt').datagrid('reload'); // reload the user data
                                    $('#tt').datagrid('unselectAll'); // reload the user data
                                    $.messager.show({ title: '提示', msg: result.msg });
                                } else {
                                    $.messager.show({title: '提示',msg: result.msg});
                                }

                            }
                        });
                    }
                })
            }
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

        //获取参数     
        function getQueryParams(queryParams) {
            var name = $("#fm_search").find("input[name='name']").val();
            var year = $("#fm_search").find("input[name='year']").val();
            var administratorArea = $("#fm_search").find("input[name='administratorArea']").val();
            var batchTypeId = $("#batchTypeId").combobox("getValue");
            var isDeleted = $("#isDeleted").combobox("getValue");

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
            queryParams.batchTypeId = batchTypeId;
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

            $("#layout_batch").layout("collapse", "south"); 
        }

        //取消选中
        function unselectall() {
            $('#tt').datagrid('unselectAll');
            $("#layout_batch").layout("collapse", "south"); 
        }

        //清空
        function clearData() {
            $("#fm_search").form("clear");
            $("#isDeleted").combobox("setValue", -1);
            $("#batchTypeId").combobox("setValue", 0);

            //重新加载数据
            reloadgrid();
        }


        

        /*------------------------------------------------------事件----------------------------------------------------------*/

        //datagrid单击函数
        function onClickRowTTF() {
            $("#layout_batch").layout("expand", "south");

            //刷新相关的plan、block Datagrid
            var rows = $('#tt').datagrid('getSelections');

            if (rows.length == 1) {
                if (ISOneRow == 1) {
                    onSelectTTF($('#tt').datagrid('getSelected'),rows[0]);
                }
                else {
                    ISOneRow = 1;
                }

                
            }
        }

        //datagrid选中一行事件
        function onSelectTTF(index,row){
                //回显农用地信息
                /*  todo:
                
                $("#tb_batch_plan").find("span[name='totalArea']").text(row.agriArea);
                $.getJSON("../BatchMng/BatchHandler.ashx?method=getRemainArea", { batchId: row['id'] }, function (result) {
                    $("#tb_batch_plan").find("span[name='remainArea']").text(result.agriArea);
                    //耕地
                    $("#tb_batch_balance").find("span[name='remainArea']").text(result.arabArea);
                    
                });

                //回显耕地信息
                $("#tb_batch_balance").find("span[name='totalArea']").text(row.arabArea);

                //回显地块
                $("#tb_land_block").find("span[name='totalArea']").text(row.totalArea);
                $.getJSON("../LandBlockMng/LandBlockHandler.ashx?method=getRemainLandBlock", { batchId: row['id'] }, function (result) {
                    $("#tb_land_block").find("span[name='remainArea']").text(result.msg);
                });
                */

                
                //加载批次关联的计划
                reloadgridPlan(row["id"]);
                //加载批次关联的占补平衡
                reloadgridBatchDSBalance(row["id"]);
                //加载批次关联的地块
                reloadgridLandBlock(row["id"]);

                /*------------------回显批次计划信息------------------*/
                //回显批次信息
                $("#fieldset_plan_batch").find("span[name='totalConsArea']").text(row.consArea);
                $("#fieldset_plan_batch").find("span[name='totalAgriArea']").text(row.agriArea);
                $("#fieldset_plan_batch").find("span[name='totalArabArea']").text(row.arabArea);
                $("#fieldset_plan_batch").find("span[name='totalQuota']").text(row.agriArea);

                $.getJSON("/View/Business/BatchMng/BatchHandler.ashx?method=GetRemainPlanArea", { batchId: row.id }, function (result) {
                    $("#fieldset_plan_batch").find("span[name='remainConsArea']").text(result.consArea);
                    $("#fieldset_plan_batch").find("span[name='remainAgriArea']").text(result.agriArea);
                    $("#fieldset_plan_batch").find("span[name='remainArabArea']").text(result.arabArea);
                    $("#fieldset_plan_batch").find("span[name='remainQuota']").text(result.issuedQuota);
                });

                //动态显示隐藏计划
                if (row.batchTypeId == "4") {//增减挂钩批次
                    /*-------------------------------------------------------plan------------------------------------------------------------*/

                    /*********************fieldset*******************************/
                    //动态显示隐藏计划
                    $("#fieldset_plan_batch").find("a[name='span1']").hide();
                    $("#fieldset_batch_plan").find("a[name='span1']").hide();
                    $("#fieldset_plan_batch").find("a[name='span2']").show();
                    $("#fieldset_batch_plan").find("a[name='span2']").show();

                     /*********************datagrid column  tt_batch_plan*******************************/
                    $("#tt_batch_plan").datagrid("hideColumn", "consArea");
                    $("#tt_batch_plan").datagrid("hideColumn", "agriArea");
                    $("#tt_batch_plan").datagrid("hideColumn", "arabArea");
                    $("#tt_batch_plan").datagrid("showColumn", "issuedQuota");

                    /*********************datagrid column  plan*******************************/
                    $("#tt_plan").datagrid("hideColumn", "consArea");
                    $("#tt_plan").datagrid("hideColumn", "agriArea");
                    $("#tt_plan").datagrid("hideColumn", "arabArea");

                    $("#tt_plan").datagrid("showColumn", "issuedQuota");

                    /*********************datagrid column  fm_plan_edit*******************************/
                    
                    $("#fm_plan_edit").find("div[name='consAreaDiv']").hide();
                    $("#fm_plan_edit").find("div[name='agriAreaDiv']").hide();
                    $("#fm_plan_edit").find("div[name='arabAreaDiv']").hide();
                    $("#fm_plan_edit").find("div[name='issuedQuotaDiv']").show();
                    
                    //验证移除
                    $("#consAreaPlan").numberbox("disableValidation");
                    $("#agriAreaPlan").numberbox("disableValidation");
                    $("#arabAreaPlan").numberbox("disableValidation");
                    $("#issuedQuotaPlan").numberbox("enableValidation");

                    /*-------------------------------------------------------balance------------------------------------------------------------*/
                    /*********************fieldset*******************************/
                    //动态隐藏复垦fieldset_ds_balance_balance
                    $("#fieldset_ds_balance_batch").find("a[name='span1']").show();
                    $("#fieldset_ds_balance_balance").find("a[name='span1']").show();

                    

                    /*********************datagrid column  tt_batch_balance*******************************/
                    $("#tt_batch_balance").datagrid("showColumn", "agriArea");

                    /*********************datagrid column  tt_ds_balance*******************************/
                    $("#tt_ds_balance").datagrid("showColumn", "agriArea");
                    
                    /*********************datagrid column  dlg_ds_balance_edit*******************************/
                    
                    $("#fm_ds_balance_edit").find("div[name='agriAreaDiv']").show();
                    
                    //验证移除
                    $("#agriAreaDSBalance").numberbox("enableValidation");

                } else {

                    
                    /*-------------------------------------------------------plan------------------------------------------------------------*/

                    /*********************fieldset*******************************/
                    $("#fieldset_batch_plan").find("a[name='span2']").hide();
                    $("#fieldset_plan_batch").find("a[name='span2']").hide();
                    $("#fieldset_batch_plan").find("a[name='span1']").show();
                    $("#fieldset_plan_batch").find("a[name='span1']").show();

                    /*********************datagrid column  tt_batch_plan*******************************/
                    $("#tt_batch_plan").datagrid("showColumn", "consArea");
                    $("#tt_batch_plan").datagrid("showColumn", "agriArea");
                    $("#tt_batch_plan").datagrid("showColumn", "arabArea");
                    $("#tt_batch_plan").datagrid("hideColumn", "issuedQuota");
                    

                    /*********************datagrid column  plan*******************************/
                    $("#tt_plan").datagrid("showColumn", "consArea");
                    $("#tt_plan").datagrid("showColumn", "agriArea");
                    $("#tt_plan").datagrid("showColumn", "arabArea");
                    $("#tt_plan").datagrid("hideColumn", "issuedQuota");
                    
                    /*********************datagrid column  fm_plan_edit*******************************/
                    
                    $("#fm_plan_edit").find("div[name='consAreaDiv']").show();
                    $("#fm_plan_edit").find("div[name='agriAreaDiv']").show();
                    $("#fm_plan_edit").find("div[name='arabAreaDiv']").show();
                    $("#fm_plan_edit").find("div[name='issuedQuotaDiv']").hide();
                    
                    //验证移除
                    $("#consAreaPlan").numberbox("enableValidation");
                    $("#agriAreaPlan").numberbox("enableValidation");
                    $("#arabAreaPlan").numberbox("enableValidation");
                    $("#issuedQuotaPlan").numberbox("disableValidation");

                    /*-------------------------------------------------------balance------------------------------------------------------------*/
                    /*********************fieldset*******************************/
                    $("#fieldset_ds_balance_batch").find("a[name='span1']").hide();
                    $("#fieldset_ds_balance_balance").find("a[name='span1']").hide();

                    

                    /*********************datagrid column  tt_batch_balance*******************************/
                    $("#tt_batch_balance").datagrid("hideColumn", "agriArea");

                    /*********************datagrid column  tt_ds_balance*******************************/
                    $("#tt_ds_balance").datagrid("hideColumn", "agriArea");
                    
                    /*********************datagrid column  dlg_ds_balance_edit*******************************/
                    
                    $("#fm_ds_balance_edit").find("div[name='agriAreaDiv']").hide();
                    
                    //验证移除
                    $("#agriAreaDSBalance").numberbox("disableValidation");
                    
                }

        }

        function onLoadSuccessTTF() {
            $(".note").tooltip({
                onShow: function () {
                    $(this).tooltip('tip').css({
                        boxShadow: '1px 1px 3px #292929'
                    });
                }
            });
            loadLinkButton();
        }

        /*------------------------------------------------------字段样式----------------------------------------------------------*/

        function formatterStateF(value, rowData, rowIndex) {
            if (rowData.isDeleted == "1") {
                return "<a class='isDeleted'  href='#' onclick='lock(" + rowIndex + ")'></a>";
            } else {
                return "<a class='isNotDeleted'  href='#' onclick='lock(" + rowIndex + ")'></a>";
            }
        }

        function lock(rowIndex) {
            //先赋值
            ISOneRow = 0;

            var index;
            if (rowIndex == undefined) {
                index = $("#tt").datagrid("getRowIndex", $("#tt").datagrid("getSelected"));
            } else {
                index = rowIndex;
            }
            var rows = $("#tt").datagrid("getRows");
            var row = rows[index];

            if(parseFloat(row.totalUsed)!=0){
                alert("该批次已经使用了地块，请先删除关系！"); return;
            }
            if(parseFloat(row.agriArea)!=0){
                alert("该批次已经使用了计划，请先删除关系！"); return;
            }
            if(parseFloat(row.arabUsed)!=0){
                alert("该批次已经使用了耕地，请先删除关系！"); return;
            }


            $.getJSON('BatchHandler.ashx?method=Lock&id=' + row.id, function (result) {
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
                reloadgrid();
                unselectall();
            });
        }

        //加载dagargid中图片
        function loadLinkButton() {
            $(".isDeleted").linkbutton({ text: '关闭', plain: true, iconCls: 'icon-lock'});
            $(".isNotDeleted").linkbutton({ text: '启用', plain: true, iconCls: 'icon-open' });
            $(".add").linkbutton({ text: '添加核销', plain: true, iconCls: 'icon-add' });
        }

        function verifFormatter(value, rowData, rowIndex) {
            if (rowData.batchTypeId == "3") {
                
                return "<a class='add' href='#' onclick='addVerif()'></a>";
            } else {
                return "";
            }
        }

        function styleTotalAreaF(value, rowData, rowIndex) {
            if((parseFloat(rowData.totalArea)-parseFloat(rowData.totalUsed))==0){
                return 'background-color:#ADFF2F;';
            }else if(parseFloat(rowData.totalUsed)==0){
                return 'background-color:#EE6A50;';
            }else{
                return 'background-color:#EEB422;';
            }
        }

        /*批次农用地样式*/
        function styleAgriAreaF(value, rowData, rowIndex) {

            if((parseFloat(rowData.agriArea)-parseFloat(rowData.agriUsed))==0){
                return 'background-color:#ADFF2F;';
            }else if(parseFloat(rowData.agriUsed)==0){
                return 'background-color:#EE6A50;';
            }else{
                return 'background-color:#EEB422;';
            }
        }

        /*批次耕地样式*/
        function styleArabAreaF(value, rowData, rowIndex) {
            if((parseFloat(rowData.arabArea)-parseFloat(rowData.arabUsed))==0){
                return 'background-color:#ADFF2F;';
            }else if(parseFloat(rowData.arabUsed)==0){
                return 'background-color:#EE6A50;';
            }else{
                return 'background-color:#EEB422;';
            }
        }

        function formatterNameF(value, row, rowIndex) {
            var c = "<ul>";
            c += "<li>还需农用地面积："+(parseFloat(row.agriArea)-parseFloat(row.agriUsed))+"</li>";
            c += "<li>还需耕地面积："+(parseFloat(row.arabArea)-parseFloat(row.arabUsed))+"</li>";
            c += "<li>还需地块面积："+(parseFloat(row.totalArea)-parseFloat(row.totalUsed))+"</li>";
            c += "</ul>";
            var content = '<a href="#" title="' + c + '" class="note" style="font-weight:bold;">' + value + '</a>';
            return content;
        }

        function formatterTypeF(value, rowData, rowIndex) {
             if (rowData.batchTypeId == "1") {
                return "<span style='font-weight:bold;color:green;'>" + value + "</span>";
            } else if (rowData.batchTypeId == "2") {
                return "<span style='font-weight:bold;color:black;'>" + value + "</span>";
            } else if (rowData.batchTypeId == "3") {
                return "<span style='font-weight:bold;color:blue;'>" + value + "</span>";
            } else if (rowData.batchTypeId == "4") {
                return "<span style='font-weight:bold;color:red;'>" + value + "</span>";
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
        
        /*------------------------------------------------------批次相关地块、占补平衡、计划的操作----------------------------------------------------------*/
       
       /*--------------计划-------------------*/
        //重新加载批次相关的计划
        function reloadgridPlan(batchId) {
            //查询参数直接添加在queryParams中    
            var queryParams = $('#tt_batch_plan').datagrid('options').queryParams;

            queryParams.batchId = batchId;

            $('#tt_batch_plan').datagrid('options').queryParams = queryParams;
            $("#tt_batch_plan").datagrid('reload');
            $("#tt_batch_plan").datagrid('unselectAll');
        }

        //刷新
        function reloadPlan() {
            $('#tt_batch_plan').datagrid('reload'); // reload the user data
            $('#tt_batch_plan').datagrid('unselectAll');
        }

        function unselectallPlan() {
            $('#tt_batch_plan').datagrid('unselectAll');
        }

        /*--------------地块-------------------*/

        //刷新
        function reloadgridLandBlock(batchId) {
            //查询参数直接添加在queryParams中    
            var queryParams = $('#tt_land_block').datagrid('options').queryParams;

            queryParams.batchId = batchId;

            $('#tt_land_block').datagrid('options').queryParams = queryParams;
            $("#tt_land_block").datagrid('reload');
            $("#tt_land_block").datagrid('unselectAll');
        }

        /*--------------占补平衡-------------------*/
        //重新加载批次相关的占补平衡
        function reloadgridBatchDSBalance(batchId) {
            //查询参数直接添加在queryParams中    
            var queryParams = $('#tt_batch_balance').datagrid('options').queryParams;

            queryParams.batchId = batchId;

            $('#tt_batch_balance').datagrid('options').queryParams = queryParams;
            $("#tt_batch_balance").datagrid('reload');
            $("#tt_batch_balance").datagrid('unselectAll');
        }

        //刷新
        function reloadBatchDSBalance() {
            $('#tt_batch_balance').datagrid('reload'); // reload the user data
            $('#tt_batch_balance').datagrid('unselectAll');
        }

        //删除
        function delBatchDSBalance() {
            var rows = $('#tt_batch_balance').datagrid('getSelections');
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
                            url: 'BatchHandler.ashx?method=deleteDemandSupplyBalanceOfBatch',
                            data: { ids: ids.join(",") },
                            method: 'post',
                            dataType: 'json',
                            success: function (result) {
                                if (result.flag) {
                                    $('#tt_batch_balance').datagrid('reload'); // reload the user data
                                    $('#tt').datagrid('reload'); // reload the user data
                                    //$('#tt').datagrid('selectRow', $('#tt').datagrid('getRowIndex', $("#tt").datagrid("getSelected")));
                                }

                                $.messager.show({ title: '提示', msg: result.msg });
                            }
                        });
                    }
                })
            }
        }

        /*******************************************************基本农田核销开始***************************************************/

        //添加基本农田多划
        function addVerif() {
            var p = dialog({
                title: '添加基本农田信息',
                href: 'AddVerif.aspx',
                modal: true,
                width: 360,
                onLoad: function () {
                    //回显年份、行政区
                    var rows = $('#tt').datagrid("getSelections");
                    
                    var year = rows[0].year;var administratorArea = rows[0].administratorArea;
                    var batchId = rows[0].id;
                    $("#fm_Verif").find("input[name='batchId']").val(batchId);
                    $("#fm_Verif").find("label[name='year']").text(rows[0].year);
                    $("#fm_Verif").find("input[name='year']").val(rows[0].year);
                    //获取行政区名称
                    $.getJSON('../../Sys/ajax/UtilHandler.ashx?method=getAdministratorNameById', { id: administratorArea }, function (data) {
                        $("#fm_Verif").find("label[name='administratorArea']").text(data._name);
                        $("#fm_Verif").find("input[name='administratorArea']").val(data._name);
                    });
                    //获取 该年份该行政区 多划信息
                    $.getJSON('../VerifMng/VerifHandler.ashx?method=getModelByYearAndArea', { year: year, administratorArea: administratorArea }, function (data) {
                        $("#fm_Verif").find("label[name='divisionArea']").text(data._divisionarea);
                        $("#fm_Verif").find("input[name='verifId']").val(data._id);
                    });
                    //获取多划剩余面积


                    //该批次下寻找是否已经添加
                    $.getJSON('../VerifBatchMng/VerifBatchHandler.ashx?method=getModelByBatchId', {batchId:batchId}, function (data) {
                        $("#fm_Verif").find("input[name='id']").val(data._id);
                        $("#fm_Verif").find("input[name='verifProvArea']").val(data._verifprovarea);
                        $("#fm_Verif").find("input[name='verifProvArableArea']").val(data._verifprovarablearea);
                        $("#fm_Verif").find("input[name='verifSelfArea']").val(data._verifselfarea);
                        $("#fm_Verif").find("input[name='verifSelfArableArea']").val(data._verifselfarablearea);
                    });
                },
                buttons:[{
				    text:'保存',
				    handler:function(){
                        $.getJSON('../VerifBatchMng/VerifBatchHandler.ashx?method=add', $("#fm_Verif").serialize(), function (result) {
                            if (result.flag) {
                                $('#dlg').dialog('close'); 	// close the dialog
                                $('#tt').datagrid('reload'); // reload the user data
                                $("#tt").datagrid('unselectAll');
                                $.messager.show({title: '提示',msg: result.msg});
                                p.dialog("close");
                            } else {
                                $.messager.show({title: '提示',msg: result.msg});
                            }
                        });
                        
                    }
			    },{
				    text:'取消',
				    handler:function(){
                        p.dialog('close');
                    }
			    }],
            });
        }

        
    </script>

</head>
<body id="layout_batch" class="easyui-layout">
    <div data-options="region:'center'">
        <%----------------------------------------------------------批次表格显示区----------------------------------------------------------%>

        <table id="tt" title="批次列表" class="easyui-datagrid" border="false" fit="true" fitColumns="true" style="width: auto; height: 500px;"        
            idfield="id" pagination="true" data-options="onRowContextMenu:onRowContextMenuF,ctrlSelect:true,onDblClickRow:edit,iconCls:'icon-tip',singleSelect:true,rownumbers:true,url:'BatchHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onClickRow:onClickRowTTF,onLoadSuccess:onLoadSuccessTTF,onSelect:onSelectTTF" fitcolumns="true"> <%--striped="true"--%>
            <%-- 表格标题--%>
            <thead>
                <tr>
                    <th data-options="field:'id',checkbox:true"></th>
                    <th data-options="field:'year',width:120,align:'center',sortable:'true'">年份</th>   
                    <th data-options="field:'administratorAreaName',width:170,align:'center',sortable:'true',formatter:formatterAdministratorAreaF">行政区</th>  
                    <th data-options="field:'name',width:160,align:'center',sortable:'true',formatter:formatterNameF">批次名称</th>
                    <th data-options="field:'approvalNo',width:180,align:'center',sortable:'true'">批准文号</th>
                    
                    <th data-options="field:'batchTypeName',width:120,align:'center',sortable:'true',formatter:formatterTypeF">批次类型</th>  
                    <th data-options="field:'totalArea',width:120,align:'center',sortable:'true',styler:styleTotalAreaF">用地总面积</th>   
                    <th data-options="field:'hasLevyArea',width:120,align:'center',sortable:'true'">已征收面积</th>   
                    <th data-options="field:'consArea',align:'center',sortable:'true'">建设用地面积</th>
                    <th data-options="field:'agriArea',width:120,align:'center',sortable:'true',styler:styleAgriAreaF">农用地面积</th>   
                    <th data-options="field:'arabArea',width:120,align:'center',sortable:'true',styler:styleArabAreaF">耕地面积</th>   
                    <!-- <th data-options="field:'unusedArea',align:'center',sortable:'true'">未利用地面积</th>   
                    <th data-options="field:'addConsArea',align:'center',sortable:'true'">新增建设用地面积</th>
                    <th data-options="field:'des',align:'center',sortable:'true'">描述</th>  
                    <th data-options="field:'approvalTime',width:200,align:'center',sortable:'true'">批准时间</th>
                    <th data-options="field:'createTime',align:'center',sortable:'true'">创建时间</th>  
                    <th data-options="field:'consArea',align:'center',sortable:'true'">建设用地面积</th>-->   
                      
                    <th data-options="field:'isDeleted',width:100,align:'center',sortable:'true',formatter:formatterStateF">状态</th>    
                    <th data-options="field:'opt',width:200,align:'center',sortable:'true',formatter:verifFormatter">操作</th>  
                </tr>
            </thead>
            <thead frozen="true">
                <tr>
                    
                </tr>
            </thead>
        </table>
        

        <%----------------------------------------------------------批次toolbar----------------------------------------------------------%>
        <div id="tb" style="padding: 5px; height: auto">
            <%-- 操作区 --%>
            <div style="margin-bottom: 5px">
                <!-- <a href="javascript:void(0)" onclick="add()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>-->
                
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

                <a href="javascript:void(0)" onclick="reloadgrid()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
                <a href="javascript:void(0)" onclick="unselectall()" title="取消选中" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-undo',plain:true"></a>

                <span style="float:right;margin-right:150px;">                      
                    <a style="margin-top:3px;display:inline-block;width:11px;height:11px;background-color:#EE6A50;"></a>&nbsp;未使用&nbsp;
                    <a style="margin-top:3px;display:inline-block;width:11px;height:11px;background-color:#EEB422;"></a>&nbsp;使用了一部分&nbsp;
                    <a style="margin-top:3px;display:inline-block;width:11px;height:11px;background-color:#ADFF2F;"></a>&nbsp;全部用完
                </span>
            </div>
            <%-- 搜索区 --%>
            <div>
               <form id="fm_search" method="post">
               名称: 
                <input id="search_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'批次名称'" style="width:160px">&nbsp;&nbsp;
                年份: 
                <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
                行政区: 
                <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">&nbsp;&nbsp;
                批次类型: 
                <input id="batchTypeId" name="batchTypeId" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../BatchTypeMng/BatchTypeHandler.ashx?method=getList',value:'0'">&nbsp;&nbsp;
            
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

        <%---------------------------------------------------------- 批次编辑操作框----------------------------------------------------------%>
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
            data-options="closed:true,collapsible:true,buttons:'#dlg-buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
            <div class="ftitle">批次信息</div>
            <form id="fm" name="fm" method="post">
                <div class="fitem">
                    <label>批次类型：</label>
                    <input name="batchTypeId" class="easyui-combobox" style="width:150px;" disabled="disabled"  data-options="valueField:'id',textField:'name',required:true,url:'../BatchTypeMng/BatchTypeHandler.ashx?method=getList'">
                </div>
                <div class="fitem">
                    <label>名称：</label>
                    <input id="method" name="method" type="hidden" />  
                    <input  name="id" type="hidden" />  
                    <input name="name" class="easyui-validatebox" data-options="required:true"/>
                </div>
                
                <div class="fitem">
                    <label>年份：</label>
                    <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
                </div>
                <div class="fitem">
                    <label>行政区：</label>
                    <input name="administratorArea" class="easyui-combobox" style="width:150px;"  data-options="valueField:'id',textField:'name',required:true,url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
                </div>
                <div class="fitem">
                    <label>用地总面积：</label>
                    <input name="totalArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
                </div>

                <div class="fitem">
                    <label>新增建设用地面积：</label>
                    <input name="addConsArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
                </div>
                <div class="fitem">
                    <label>建设用地面积：</label>
                    <input name="consArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
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
                    <label>未利用地面积：</label>
                    <input name="unusedArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
                </div>
                <div class="fitem">
                    <label>批准文号：</label>
                    <input name="approvalNo" class="easyui-textbox" style="width:150px;" >
                </div>
                <div class="fitem">
                    <label>批准机关：</label>
                    <input name="approvalAuthority" class="easyui-textbox" style="width:150px;" >
                </div>
                <div class="fitem">
                    <label>批准时间：</label>
                    <input name="releaseTime" class="easyui-datebox" style="width:150px;">
                </div>
                <div class="fitem">
                    <label>描述：</label>
                    <textarea name="des" style="width:150px;"></textarea>
                </div>
            </form>
        </div>
        <%--------buttons-------%>
        <div id="dlg-buttons">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="save()">保存</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close')">关闭</a>
        </div>

        <div id="menu" class="easyui-menu" style="width: 120px; display: none;">
            <div onclick="edit();" iconcls="icon-edit">
                编辑</div>
            <div onclick="del();" iconcls="icon-remove">
                删除</div>
        </div>
    </div>


    <!-- ---------------------------------------------------------------与批次关联的区域---------------------------------------------------------------------->
    <div data-options="region:'south',split:false,collapsed:true" style="width: 100%; height: 240px;
        padding-top: 2px; background-color: #fff; overflow: auto" >
        <div id="otherRelation">
            <%----------------------------------------------------------底部关联地块----------------------------------------------------------%>
            <div style="float:left;width:400px;height:210px;margin:5px;">
                <table id="tt_land_block" title="地块列表" class="easyui-datagrid" fit="true" border="true" style="width: 100%; height: auto;"        
                    idfield="id"  data-options="rownumbers:true,url:'../LandBlockMng/LandBlockHandler.ashx?method=getList',method:'get',toolbar:'#tb_land_block',striped:true,nowrap:false" fitcolumns="false"> <%--striped="true"--%>
                    <thead>
                        <tr>
                            <th data-options="field:'id',checkbox:true"></th>
                            <th data-options="field:'name',width:100,align:'center',sortable:'true'">名称</th>
                            <th data-options="field:'area',width:120,align:'center',sortable:'true'">面积</th>   
                            <th data-options="field:'isused',width:120,align:'center',sortable:'true'">是否使用</th>   
                            <th data-options="field:'des',width:120,align:'center',sortable:'true'">开发用途</th>   
                        </tr>
                    </thead>
                </table>
                <%-- toolbar --%>
                <div id="tb_land_block" style="padding: 5px; height: auto">
                    <div style="margin-bottom: 5px">
                        <a href="javascript:void(0)" onclick="addLandBlock()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
                        <a href="javascript:void(0)" onclick="delLandBlock()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
                        <a href="javascript:void(0)" onclick="reloadLandBlock()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
                        <!-- 总需地块:<span name="totalArea" class="red">50.0000</span><span class="red">公顷</span>
                        还需地块:<span name="remainArea" class="red">50.0000</span><span class="red">公顷</span>     -->     
                    </div>
                </div>
            </div>

            

            <%----------------------------------------------------------底部关联计划----------------------------------------------------------%>
            <div style="float:left;width:400px;height:210px;margin:5px; ">
                <table id="tt_batch_plan" title="计划列表" class="easyui-datagrid" fit="true" border="true" style="width: 100%; height: auto;"        
                    idfield="id"  data-options="rownumbers:true,url:'BatchHandler.ashx?method=getPlanListByBatchId',method:'get',toolbar:'#tb_batch_plan',striped:true,nowrap:false" fitcolumns="false"> <%--striped="true"--%>
                    <thead>
                        <tr>
                            <th data-options="field:'id',checkbox:true"></th>
                            <th data-options="field:'planName',width:100,align:'center',sortable:'true'">名称</th>
                            <th data-options="field:'typeName',width:150,align:'center',sortable:'true'">类型</th>   
                            <th data-options="field:'consArea',width:100,align:'center',sortable:'true'">建设用地</th>   
                            <th data-options="field:'agriArea',width:100,align:'center',sortable:'true'">农用地</th>   
                            <th data-options="field:'arabArea',width:100,align:'center',sortable:'true'">耕地</th>   
                            <th data-options="field:'issuedQuota',width:120,align:'center',sortable:'true'">使用指标</th>   
                        </tr>
                    </thead>
                </table>
                <%--toolbar--%>
                <div id="tb_batch_plan" style="padding: 5px; height: auto">
                    <div style="margin-bottom: 5px">
                        <a href="javascript:void(0)" onclick="addPlan()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
                        <a href="javascript:void(0)" onclick="delPlan()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
                        <a href="javascript:void(0)" onclick="reloadPlan()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
                    </div>
                </div>
            </div>

            <%----------------------------------------------------------底部关联占补平衡----------------------------------------------------------%>
            <div style="float:left;width:400px;height:210px;margin:5px;">
                <table id="tt_batch_balance" title="占补平衡列表" class="easyui-datagrid" fit="true" border="true" style="width: 100%; height: auto;"        
                    idfield="id"  data-options="rownumbers:true,url:'BatchHandler.ashx?method=getDemandSupplyBalanceListByBatchId',method:'get',toolbar:'#tb_batch_balance',striped:true,nowrap:false" fitcolumns="false"> <%--striped="true"--%>
                    <thead>
                        <tr>
                            <th data-options="field:'id',checkbox:true"></th>
                            <th data-options="field:'balanceName',width:100,align:'center',sortable:'true'">名称</th>
                            <th data-options="field:'typeName',width:150,align:'center',sortable:'true'">类型</th> 
                            <th data-options="field:'agriArea',width:120,align:'center',sortable:'true'">农用地</th>    
                            <th data-options="field:'arabArea',width:120,align:'center',sortable:'true'">耕地</th>  
                        </tr>
                    </thead>
                </table>
                <%--toolbar--%>
                <div id="tb_batch_balance" style="padding: 5px; height: auto">
                    <div style="margin-bottom: 5px">
                        <a href="javascript:void(0)" onclick="addBatchDSBalance()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
                        <a href="javascript:void(0)" onclick="delBatchDSBalance()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
                        <a href="javascript:void(0)" onclick="reloadBatchDSBalance()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">


    /* ---------------------------------------------------------------地块操作----------------------------------------------------------------------*/
    function addLandBlock() {
        var rows = $("#tt").datagrid("getSelections");
        if (rows.length == 1) {
            //弹出添加地块对话框
            var dialog_land_block = dialog({
                title: '添加地块',
                href: 'LandBlock.aspx',
                width: 400,
                height: 360,
                doSize: true,
                resizable: true,
                modal: true,
                onLoad: function () {
                    //获取批次信息回显
                    var rows = $("#tt").datagrid("getSelections");
                    if (rows.length == 1) {
                        var row = rows[0];
                        console.info(row);
                        //回显batchId
                        $("#fm_Land_Block").find("input[name='batchId']").val(row["id"]);

                        //回显用地总面积
                        $("#fieldset_landblock").find("span[name='totalArea']").text(row["totalArea"]);

                        //回显剩余地块面积
                        $.getJSON("../LandBlockMng/LandBlockHandler.ashx?method=getRemainLandBlock", { batchId: row['id'] }, function (result) {
                            $("#fieldset_landblock").find("span[name='remainArea']").text(result.msg);
                        });

                        //回显地块名称，地块名称自动+1
                        $.getJSON("../LandBlockMng/LandBlockHandler.ashx?method=getNextLandBlock", { batchId: row['id'] }, function (result) {
                            $("#fm_Land_Block").find("input[name='name']").val(result.msg);
                        });

                    } else {
                        $.messager.show({
                            title: '提示',
                            msg: '请选择一行添加',
                            timeout: 3000,
                            showType: 'slide'
                        });

                    }
                },
                onClose: function () {
                    dialog_land_block.dialog("destroy");
                },
                buttons: [{
                    text: "保存",
                    handler: function () {
                        //提交之前验证
                        if (!$("#fm_Land_Block").form('validate')) {
                            return false;
                        }

                        //验证数量大小关系
                        var area = $("#area").numberbox("getValue");
                        var remainArea = $("#fieldset_landblock").find("span[name='remainArea']").text();


                        if (parseFloat(area) > parseFloat(remainArea)) {
                            alert("【使用的地块面积<=还需地块面积】不成立"); return;
                        }

                        $.getJSON('../LandBlockMng/LandBlockHandler.ashx?method=add',
                            { id: $("#fm_Land_Block").find("input[name='id']").val(), batchId: $("#fm_Land_Block").find("input[name='batchId']").val(), name: $("#fm_Land_Block").find("input[name='name']").val(), area: $("#area").numberbox("getValue"), des: $("#fm_Land_Block").find("input[name='des']").val() },
                            function (result) {
                                if (result.flag) {
                                    dialog_land_block.dialog('close'); 	// close the dialog
                                    $('#tt_land_block').datagrid('reload'); // reload the user data
                                    $("#tt_land_block").datagrid('unselectAll');

                                    $('#tt').datagrid('reload'); // reload the user data
                                    //$('#tt').datagrid('selectRow', $('#tt').datagrid('getRowIndex', $("#tt").datagrid("getSelected")));
                                }
                                $.messager.show({ title: '提示', msg: result.msg });
                            });
                    }
                }, {
                    text: "关闭",
                    handler: function () {
                        dialog_land_block.dialog("close");
                    }
                }]
            });

        } else {
            //
            $.messager.show({
                title: '提示',
                msg: '请选择一行添加',
                timeout: 3000,
                showType: 'slide'
            });

        }
    }
    function delLandBlock() {
        var flag = false;

        var rows = $('#tt_land_block').datagrid('getSelections');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i].isused == "true") {
                flag = true;
            }
        }

        if (flag) {
            alert("存在地块已经被征收，请先删除征收补偿信息！"); return;
        }

        
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
                        if (rows[i].isused == "true") {
                            flag = true;
                        }
                    }

                    $.ajax({
                        url: '../LandBlockMng/LandBlockHandler.ashx?method=delete',
                        data: { ids: ids.join(",") },
                        method: 'post',
                        dataType: 'json',
                        success: function (result) {
                            if (result.flag) {
                                $('#tt_land_block').datagrid('reload'); // reload the user data
                                $('#tt').datagrid('reload'); // reload the user data
                                $('#tt').datagrid('selectRow', $('#tt').datagrid('getRowIndex', $("#tt").datagrid("getSelected")));
                            }
                            $.messager.show({ title: '提示', msg: result.msg });
                        }
                    });
                }
            })
        }


    }
    function unselectallLandBlock() {
        $("#tt_land_block").datagrid('unselectAll');
    }

    //重新加载批次相关的地块
    function reloadgridLandBlock(batchId) {
        //查询参数直接添加在queryParams中    
        var queryParams = $('#tt_land_block').datagrid('options').queryParams;

        queryParams.batchId = batchId;

        $('#tt_land_block').datagrid('options').queryParams = queryParams;
        $("#tt_land_block").datagrid('reload');
        $("#tt_land_block").datagrid('unselectAll');
    }

    //刷新
    function reloadLandBlock() {
        $('#tt_land_block').datagrid('reload'); // reload the user data
        $('#tt_land_block').datagrid('unselectAll');
    }

</script>

<!-- ---------------------------------------------------plan弹出框--------------------------------------------------------------->
<div id="dlg_plan" class="easyui-dialog" style="width: 800px; height: 560px;"
    data-options="closed:true,collapsible:true,onBeforeOpen:onBeforeOpenDlgPlanF"> 
    <table id="tt_plan" class="easyui-datagrid" border="true" fit="true" fitColumns="true" style="width: auto; height:100px"        
        idfield="id" pagination="true" data-options="url:'/View/Business/PlanMng/PlanHandler.ashx?method=query&isDeleted=0',iconCls:'icon-tip',rownumbers:true,singleSelect:true,pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb_plan',striped:true,nowrap:false,onClickRow:onClickRowPlanF"> <%--striped="true"--%>
        <thead>
            <tr>
                <th data-options="field:'id',hidden:true"></th>
                <th data-options="field:'name',width:80,align:'center',sortable:'true'">名称</th>
                <th data-options="field:'year',width:80,align:'center',sortable:'true'">年份</th>   
                <th data-options="field:'administratorAreaName',width:80,align:'center',sortable:'true'">行政区</th>      
                <th data-options="field:'consArea',width:160,align:'center',sortable:'true'">建设用地面积</th>   
                <th data-options="field:'agriArea',width:160,align:'center',sortable:'true'">农用地面积</th>   
                <th data-options="field:'arabArea',width:160,align:'center',sortable:'true'">耕地面积</th>   
                <th data-options="field:'issuedQuota',width:200,align:'center',sortable:'true'">下达指标</th>   
                <th data-options="field:'planTypeName',align:'center',sortable:'true'">计划类型</th>   
                <!-- <th data-options="field:'createTime',align:'center',sortable:'true'">创建时间</th>   -->  
            </tr>
        </thead>
    </table>
    <%--toolbar--%>
    <div id="tb_plan" style="padding: 5px; height: auto">
        <div style="margin-bottom: 5px">
            <a href="javascript:void(0)" onclick="addPlanEdit()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
            <a href="javascript:void(0)" onclick="reloadgridPlanDG()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>&nbsp;&nbsp;&nbsp;&nbsp;
            <span style="font-weight:bold;color:Red;">请选择下面的计划后再点击“添加”</span>
        </div>
        <div>
            <form id="fm_search_plan" method="post">
            名称: 
            <input id="search_plan_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'计划名称'" style="width:160px">&nbsp;&nbsp;
            年份: 
            <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
            行政区: 
            <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
            计划类型: 
            <input name="planTypeId" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../PlanTypeMng/PlanTypeHandler.ashx?method=getList'">
            
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgridPlanDG()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearDataPlanDG()">清空</a>
            </form>
        </div>

        <div>
            <fieldset id="fieldset_plan_batch" style="width:740px;text-align:center;font-weight:bold;">
                <legend>该批次信息</legend>
                <div>
                    <a name="span1">
                        总建设地面积:<span name="totalConsArea" class="blue">50.0000</span><span class="blue">公顷</span>&nbsp&nbsp;&nbsp;
                        总农用地面积:<span name="totalAgriArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                        总耕地面积:<span name="totalArabArea" class="blue">50.0000</span><span class="blue">公顷</span>
                    </a>
                    <a name="span2">
                        总下达指标:<span name="totalQuota" class="red">50.0000</span><span class="red">公顷</span>
                    </a>
                </div>
                <div>
                    <a name="span1">
                        剩余建设地面积:<span name="remainConsArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                        剩余农用地面积:<span name="remainAgriArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                        剩余耕地面积:<span name="remainArabArea" class="green">50.0000</span><span class="green">公顷</span>
                    </a>
                    <a name="span2">
                        剩余下达指标:<span name="remainQuota" class="green">50.0000</span><span class="green">公顷</span>
                    </a>
                </div>
            </fieldset>
            <fieldset id="fieldset_batch_plan" style="width:740px;text-align:center;font-weight:bold;display:none;">
                <legend>选中计划信息</legend>
                <div>
                    <a name="span1">
                        总建设地面积:<span name="totalConsArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                        总农用地面积:<span name="totalAgriArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                        总耕地面积:<span name="totalArabArea" class="red">50.0000</span><span class="red">公顷</span>
                    </span>
                    <a name="span2">
                        总下达指标:<span name="totalQuota" class="red">50.0000</span><span class="red">公顷</span>
                    </a>
                </div>
                <div>
                    <a name="span1">
                        剩余建设地面积:<span name="remainConsArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                        剩余农用地面积:<span name="remainAgriArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                        剩余耕地面积:<span name="remainArabArea" class="green">50.0000</span><span class="green">公顷</span>
                    </a>
                    <a name="span2">
                        剩余下达指标:<span name="remainQuota" class="green">50.0000</span><span class="green">公顷</span>
                    </a>
                </div>
            </fieldset>
        </div>
    </div>
</div>
<%-- 计划修改框--%>
<div id="dlg_plan_edit" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
    data-options="closed:true,collapsible:true,buttons:'#dlg_plan_edit_buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
    <div class="ftitle">添加使用情况</div>
    <form id="fm_plan_edit" name="fm" method="post">
        <div class="fitem">
            <input name="id" type="hidden" />
        </div>
        <div name="consAreaDiv" class="fitem">
            <label>建设用地面积：</label>
            <input id="consAreaPlan" name="consArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="" />
        </div>
        <div name="agriAreaDiv" class="fitem">
            <label>农用地面积：</label>
            <input id="agriAreaPlan" name="agriArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="" />
        </div>
        <div name="arabAreaDiv" class="fitem">
            <label>耕地面积：</label>
            <input id="arabAreaPlan" name="arabArea" class="easyui-numberbox" style="width:150px;" required="required" data-options="" />
        </div>
        <div name="issuedQuotaDiv" class="fitem">
            <label>使用下达指标：</label>
            <input id="issuedQuotaPlan" name="issuedQuota" class="easyui-numberbox" style="width:150px;" required="required" data-options="" />
        </div>
    </form>
</div>
<div id="dlg_plan_edit_buttons">
    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="savePlanDialog()">保存</a>
    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_plan_edit').dialog('close');">关闭</a>
</div>

<script type="text/javascript">
    /* ---------------------------------------------------------------计划操作----------------------------------------------------------------------*/
    function addPlan() {
        //至少选中一行
        var rows = $("#tt").datagrid("getSelections");
        if (rows.length == 1) {
            
            var dlgDemandSupplyBalance = $('#dlg_plan').dialog({
                title: '添加计划使用信息',
                modal: true,
                onLoad: function () {

                }
            }).dialog("open");
        } else {
            $.messager.show({ title: '提示', msg: '请选择一行添加', timeout: 3000, showType: 'slide' });
        }
    }

    function delPlan() {
        var rows = $('#tt_batch_plan').datagrid('getSelections');
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
                        url: 'BatchHandler.ashx?method=deletePlanOfBatch',
                        data: { ids: ids.join(",") },
                        method: 'post',
                        dataType: 'json',
                        success: function (result) {
                            if (result.flag) {
                                $('#tt_batch_plan').datagrid('reload'); // reload the user data
                                $('#tt').datagrid('reload'); // reload the user data
                                //$('#tt').datagrid('selectRow', $('#tt').datagrid('getRowIndex', $("#tt").datagrid("getSelected")));
                            }
                            //重新选中该批次
                            //$("#tt").datagrid("selectRow", $("#tt").datagrid("getSelected"));
                            $.messager.show({ title: '提示', msg: result.msg });
                        }
                    });
                }
            })
        }
    }

    //DSBlance对话框打开事件
    function onBeforeOpenDlgPlanF() {
        var row = $("#tt").datagrid("getSelected");



        /*
        //回显批次信息
        $("#fieldset_plan_batch").find("span[name='totalAgriArea']").text(row.agriArea);

        $.getJSON("/View/Business/BatchMng/BatchHandler.ashx?method=GetRemainPlanArea", { batchId: row.id }, function (result) {
            $("#fieldset_plan_batch").find("span[name='remainAgriArea']").text(result.agriArea);
        });
        */

        //刷新
        reloadgridPlanDG();
    }

    //获取参数     
    function getQueryParamsPlan(queryParams) {
        var name = $("#fm_search_plan").find("input[name='name']").val();
        var year = $("#fm_search_plan").find("input[name='year']").val();
        var administratorArea = $("#fm_search_plan").find("input[name='administratorArea']").val();
        var planTypeId = $("#fm_search_plan").find("input[name='planTypeId']").val();

        queryParams.name = name;
        queryParams.year = year;
        queryParams.administratorArea = administratorArea;
        queryParams.planTypeId = planTypeId;
        queryParams.batchTypeId = $('#tt').datagrid("getSelected").batchTypeId;
        queryParams.isSubmited = "1";

        return queryParams;
    }

    //增加查询参数，重新加载表格
    function reloadgridPlanDG() {
        //查询参数直接添加在queryParams中    
        var queryParams = $('#tt_plan').datagrid('options').queryParams;
        getQueryParamsPlan(queryParams);

        $('#tt_plan').datagrid('options').queryParams = queryParams;
        $("#tt_plan").datagrid('reload');

        $('#tt_plan').datagrid('unselectAll');

        $("#fieldset_batch_plan").hide();
    }

    //清楚查询条件
    function clearDataPlanDG() {
        $("#tb_plan").find("input").val("");

        //重新加载数据
        reloadgridPlanDG();
    }

    //单击事件
    function onClickRowPlanF() {
        //回显选中占补平衡信息
        var rows = $("#tt_plan").datagrid("getSelections");

        if (rows.length == 1) {
            $("#fieldset_batch_plan").show();
            var row = rows[0];

            $("#fieldset_batch_plan").find("span[name='totalConsArea']").text(row["consArea"]);
            $("#fieldset_batch_plan").find("span[name='totalAgriArea']").text(row["agriArea"]);
            $("#fieldset_batch_plan").find("span[name='totalArabArea']").text(row["arabArea"]);
            $("#fieldset_batch_plan").find("span[name='totalQuota']").text(row["issuedQuota"]);

            $.getJSON("../PlanMng/PlanHandler.ashx?method=getRemainArea", { planId: row['id'] }, function (result) {
                //回显信息  
                $("#fieldset_batch_plan").find("span[name='remainConsArea']").text(result.consArea);
                $("#fieldset_batch_plan").find("span[name='remainAgriArea']").text(result.agriArea);
                $("#fieldset_batch_plan").find("span[name='remainArabArea']").text(result.arabArea);

                $("#fieldset_batch_plan").find("span[name='remainQuota']").text(result.issuedQuota);
            });
        } else {
            $.messager.show({ title: '提示', msg: '请选择一行添加' });
        }
    }

    function addPlanEdit() {
        var rows = $("#tt_plan").datagrid("getSelections");
        if (rows.length == 1) {
            var row = rows[0];
            
            var dlg_plan = $('#dlg_plan_edit').dialog({
                title: '添加',
                modal: true,
                onLoad: function () {

                }
            }).dialog("open");
        } else {
            $.messager.show({ title: '提示', msg: '请选择一行添加' });
        }
    }

    //保存信息
    function savePlanDialog() {
        //提交之前验证
        if (!$("#fm_plan_edit").form('validate')) {
            return false;
        }

        //数据合法性验证
        //验证数量大小关系
        var consArea = $("#consAreaPlan").numberbox("getValue");
        var agriArea = $("#agriAreaPlan").numberbox("getValue");
        var arabArea = $("#arabAreaPlan").numberbox("getValue");
        var issuedQuota = $("#issuedQuotaPlan").numberbox("getValue");

        var rows = $("#tt").datagrid("getSelections");
        if (rows[0].batchTypeId == "4") {
            var remainQuota_batch = $("#fieldset_plan_batch").find("span[name='remainQuota']").text();
            var remainQuota_plan = $("#fieldset_batch_plan").find("span[name='remainQuota']").text();

            if (parseFloat(issuedQuota) > parseFloat(remainQuota_batch)) {
                alert("【下达指标<=批次剩余指标面积】不符合"); return false;
            }
            if (parseFloat(issuedQuota) > parseFloat(remainQuota_plan)) {
                alert("【下达指标<=计划剩余指标面积】不符合"); return false;
            }
        } else {
            var remainConsArea_batch = $("#fieldset_plan_batch").find("span[name='remainConsArea']").text();
            var remainAgriArea_batch = $("#fieldset_plan_batch").find("span[name='remainAgriArea']").text();
            var remainArabArea_batch = $("#fieldset_plan_batch").find("span[name='remainArabArea']").text();

            var remainConsArea_plan = $("#fieldset_batch_plan").find("span[name='remainConsArea']").text();
            var remainAgriArea_plan = $("#fieldset_batch_plan").find("span[name='remainAgriArea']").text();
            var remainArabArea_plan = $("#fieldset_batch_plan").find("span[name='remainArabArea']").text();

            if (parseFloat(consArea) > parseFloat(remainConsArea_batch)) {
                alert("【建设用地面积<=批次剩余建设用地面积】不符合"); return false;
            }
            if (parseFloat(consArea) > parseFloat(remainConsArea_plan)) {
                alert("【建用地面积<=计划剩余建设用地面积】不符合"); return false;
            }
            if (parseFloat(agriArea) > parseFloat(remainAgriArea_batch)) {
                alert("【农用地面积<=批次剩余农用地面积】不符合"); return false;
            }
            if (parseFloat(agriArea) > parseFloat(remainAgriArea_plan)) {
                alert("【农用地面积<=计划剩余农用地面积】不符合"); return false;
            }
            if (parseFloat(arabArea) > parseFloat(remainArabArea_batch)) {
                alert("【耕地面积<=批次剩余耕地面积】不符合"); return false;
            }
            if (parseFloat(arabArea) > parseFloat(remainArabArea_plan)) {
                alert("【耕地面积<=计划剩余耕地面积】不符合"); return false;
            }
        }

        var rowsBatch = $('#tt').datagrid("getSelections");
        var rowsPlan = $('#tt_plan').datagrid("getSelections");
        if (rowsBatch.length != 1 && rowsPlan.length != 1) { alert("请返回选择批次及对应的计划再添加！"); return; }
        $.getJSON('BatchHandler.ashx?method=addPlanToBatch', { batchId: rowsBatch[0].id, planId: rowsPlan[0].id, consArea: $('#consAreaPlan').textbox('getValue'), agriArea: $('#agriAreaPlan').textbox('getValue'), arabArea: $('#arabAreaPlan').textbox('getValue'), issuedQuota: $('#issuedQuotaPlan').textbox('getValue') }, function (result) {
            if (result.flag) {
                $('#fm_plan_edit').form('clear');
                $('#dlg_plan_edit').dialog('close');

                $('#dlg_plan').dialog('close'); 	// close the dialog

                $('#tt').datagrid('reload'); // reload the user data
                //$('#tt').datagrid('selectRow', $('#tt').datagrid('getRowIndex', $("#tt").datagrid("getSelected")));

                $('#tt_batch_plan').datagrid('reload'); // reload the user data
                $("#tt_batch_plan").datagrid('unselectAll');
            }
            $.messager.show({ title: '提示', msg: result.msg });
        });
    }
</script>



<!-- ---------------------------------------------------dsbalance弹出框--------------------------------------------------------------->
<%-- dsbalance弹出框--%>
<div id="dlg_ds_balance" class="easyui-dialog" style="width: 800px; height: 560px;"
    data-options="closed:true,collapsible:true,onBeforeOpen:onBeforeOpenDlgDSBlanceF"> 

    <table id="tt_ds_balance" class="easyui-datagrid" border="true" fit="true" fitColumns="true" style="width: auto; height:100px"        
        idfield="id" pagination="true"  
        data-options="url:'/View/Business/DemandSupplyBalanceMng/DemandSupplyBalanceHandler.ashx?method=query&isDeleted=0',iconCls:'icon-tip',rownumbers:true,singleSelect:true,pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb_ds_balance',striped:true,nowrap:false,onClickRow:onClickRowDSBlanceF"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',hidden:true"></th>
                <th data-options="field:'name',width:'120',align:'center',sortable:'true'">名称</th>
                <th data-options="field:'year',align:'center',width:'120',sortable:'true'">年份</th>   
                <th data-options="field:'administratorAreaName',width:'120',align:'center',sortable:'true'">行政区</th>  
                <th data-options="field:'agriArea',width:'120',align:'center',sortable:'true'">农用地面积</th>   
                <th data-options="field:'arabArea',width:'120',align:'center',sortable:'true'">耕地面积</th>   
                <th data-options="field:'typeName',width:'120',align:'center',sortable:'true'">占补平衡类型</th>   
            </tr>
        </thead>
    </table>
    <%--功能区--%>
    <div id="tb_ds_balance" style="padding: 5px; height: auto">
        <%-- 包括添加、修改、删除、刷新、全部选中、取消全部选中 --%>
        <div style="margin-bottom: 5px">
            <a href="javascript:void(0)" onclick="addDialogDSBalance()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
            <a href="javascript:void(0)" onclick="reloadgridDSBalance()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>&nbsp;&nbsp;&nbsp;
            <span style="font-weight:bold;color:Red;">请选择下面的占补平衡后再点击“添加”</span>
        </div>
        <%-- 查找Account信息，根据注册时间、姓名 --%>
        <div>
            <form id="fm_search_ds_balance" method="post">
            名称: 
            <input id="search_ds_balance_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'占补平衡名称'" style="width:160px">&nbsp;&nbsp;
            年份: 
            <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
            行政区: 
            <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
            占补平衡类型: 
            <input name="typeId" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../DemandSupplyBalanceTypeMng/DemandSupplyBalanceTypeHandler.ashx?method=getList'">
            
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgridDSBalance()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearDataDSBlance()">清空</a>
            </form>
        </div>

        <%--信息回显 --%>
        <div>
            <fieldset id="fieldset_ds_balance_batch" style="width:740px;text-align:center;font-weight:bold;">
                <legend style="color:Green;">该批次信息</legend>
                <div>
                    <a name="span1">
                        总农用地面积:<span name="totalAgriArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                    </a>
                    <a name="span2">
                        总耕地面积:<span name="totalArabArea" class="red">50.0000</span><span class="red">公顷</span>
                    </a>
                </div>
                <div>
                    <a name="span1">
                        剩余农用地面积:<span name="remainAgriArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    </a>
                    <a name="span2">
                        剩余耕用地面积:<span name="remainArabArea" class="green">50.0000</span><span class="green">公顷</span>
                    </a>
                </div>
            </fieldset>
            <fieldset id="fieldset_ds_balance_balance" style="width:740px;text-align:center;font-weight:bold;display:none;">
                <legend style="color:Green;">选中占补平衡信息</legend>
                <div>
                    <a name="span1">
                        总农用地面积:<span name="totalAgriArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                    </a>
                    <a name="span2">
                        总耕地面积:<span name="totalArabArea" class="red">50.0000</span><span class="red">公顷</span>
                    </a>
                </div>
                <div>
                    <a name="span1">
                        剩余农用地面积:<span name="remainAgriArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    </a>
                    <a name="span2">
                        剩余耕用地面积:<span name="remainArabArea" class="green">50.0000</span><span class="green">公顷</span>
                    </a>
                </div>
            </fieldset>
        </div>

        <%-- 弹出操作框--%>
        <div id="dlg_ds_balance_edit" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
            data-options="closed:true,collapsible:true,buttons:'#dlg_ds_balance_edit_buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
            <div class="ftitle">添加使用情况</div>
            <form id="fm_ds_balance_edit" name="fm" method="post">
                <div name="agriAreaDiv" class="fitem">
                    <label>农用地面积：</label>
                    <input name="id" type="hidden" />  
                    <input name="batchId" type="hidden" />  
                    <input name="demandSupplyBalanceId" type="hidden" /> 

                    <input id="agriAreaDSBalance" name="agriArea" class="easyui-numberbox" style="width:150px;" required="required" data-options=""/>
                </div>
                <div class="fitem">
                    <label>耕地面积：</label>
                    <input id="arabAreaDSBalance" name="arabArea" class="easyui-numberbox" style="width:150px;" required="required" data-options=""/>
                </div>
            </form>
        </div>
        <div id="dlg_ds_balance_edit_buttons">
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok',dissabled:true" onclick="saveDSBalanceDialog()">保存</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_ds_balance_edit').dialog('close')">关闭</a>
        </div>
    </div>
</div>


<!-------------------------------------------dsbalance 操作----------------------------------------------->
<script type="text/jscript">

    function addBatchDSBalance() {
        //至少选中一行
        var rows = $("#tt").datagrid("getSelections");
        if (rows.length == 1) {
            var dlgDemandSupplyBalance = $('#dlg_ds_balance').dialog({
                title: '添加占补平衡使用信息',
                modal: true,
                onLoad: function () {

                }
            }).dialog("open");
        } else {
            $.messager.show({ title: '提示', msg: '请选择一行添加', timeout: 3000, showType: 'slide' });
        }
    }

    //DSBlance对话框打开事件
    function onBeforeOpenDlgDSBlanceF() {
        var row = $("#tt").datagrid("getSelected");

        //回显批次信息
        $("#fieldset_ds_balance_batch").find("span[name='totalConsArea']").text(row.consArea);
        $("#fieldset_ds_balance_batch").find("span[name='totalAgriArea']").text(row.agriArea);
        $("#fieldset_ds_balance_batch").find("span[name='totalArabArea']").text(row.arabArea);

        $.getJSON("/View/Business/BatchMng/BatchHandler.ashx?method=GetRemainDSArea", { batchId: row.id }, function (result) {
            $("#fieldset_ds_balance_batch").find("span[name='remainConsArea']").text(result.consArea);
            $("#fieldset_ds_balance_batch").find("span[name='remainAgriArea']").text(result.agriArea);
            $("#fieldset_ds_balance_batch").find("span[name='remainArabArea']").text(result.arabArea);
        });

        //刷新
        reloadgridDSBalance();
    }

    //获取参数     
    function getQueryParamsDSBalance(queryParams) {

        var name = $("#fm_search_ds_balance").find("input[name='name']").val();
        var year = $("#fm_search_ds_balance").find("input[name='year']").val();
        var administratorArea = $("#fm_search_ds_balance").find("input[name='administratorArea']").val();
        var typeId = $("#fm_search_ds_balance").find("input[name='typeId']").val();

        queryParams.name = name;
        queryParams.year = year;
        queryParams.administratorArea = administratorArea;
        queryParams.typeId = typeId;
        queryParams.batchTypeId = $('#tt').datagrid("getSelected").batchTypeId;
        queryParams.isSubmited = "1";

        return queryParams;

    }

    //增加查询参数，重新加载表格
    function reloadgridDSBalance() {
        //查询参数直接添加在queryParams中    
        var queryParams = $('#tt').datagrid('options').queryParams;
        getQueryParamsDSBalance(queryParams);

        $('#tt_ds_balance').datagrid('options').queryParams = queryParams;
        $("#tt_ds_balance").datagrid('reload');

        $('#tt_ds_balance').datagrid('unselectAll');

        $("#fieldset_ds_balance_balance").hide();
    }

    //清楚查询条件
    function clearDataDSBlance() {
        $("#tb_ds_balance").find("input").val("");

        //重新加载数据
        reloadgridDSBalance();
    }

    //单击事件
    function onClickRowDSBlanceF() {
        //回显选中占补平衡信息
        var rows = $("#tt_ds_balance").datagrid("getSelections");

        if (rows.length == 1) {
            $("#fieldset_ds_balance_balance").show();
            var row = rows[0];

            $("#fieldset_ds_balance_balance").find("span[name='totalAgriArea']").text(row["agriArea"]);
            $("#fieldset_ds_balance_balance").find("span[name='totalArabArea']").text(row["arabArea"]);

            $.getJSON("../DemandSupplyBalanceMng/DemandSupplyBalanceHandler.ashx?method=getRemainArea", { dsBalanceId: row['id'] }, function (result) {
                //回显信息  
                $("#fieldset_ds_balance_balance").find("span[name='remainAgriArea']").text(result.agriArea);
                $("#fieldset_ds_balance_balance").find("span[name='remainArabArea']").text(result.arabArea);
            });
        } else {
            $.messager.show({ title: '提示', msg: '请选择一行添加' });
        }
    }

    //添加修改DSBlance对话框
    function addDialogDSBalance() {
        var rows = $("#tt_ds_balance").datagrid("getSelections");
        if (rows.length == 1) {
            var row = rows[0];

            var dlgDemandSupplyBalance = $('#dlg_ds_balance_edit').dialog({
                modal: true,
                title: '添加数据',
                onLoad: function () {

                }
            }).dialog("open");
        } else {
            $.messager.show({
                title: '提示',
                msg: '请选择一项占补平衡添加',
                timeout: 3000,
                showType: 'slide'
            });

        }
    }

    function saveDSBalanceDialog() {
        //提交之前验证
        if (!$("#fm_ds_balance_edit").form('validate')) {
            return false;
        }

        //数据合法性验证
        //验证数量大小关系
        var agriArea = $("#agriAreaDSBalance").numberbox("getValue");
        var arabArea = $("#arabAreaDSBalance").numberbox("getValue");

        var rows = $("#tt").datagrid("getSelections");

        var remainArabArea_batch = $("#fieldset_ds_balance_batch").find("span[name='remainArabArea']").text();
        var remainArabArea_balance = $("#fieldset_ds_balance_balance").find("span[name='remainArabArea']").text();

        if (parseFloat(arabArea) > parseFloat(remainArabArea_batch)) {
            alert("【耕地面积<=批次剩余耕地面积】不符合"); return false;
        }
        if (parseFloat(arabArea) > parseFloat(remainArabArea_balance)) {
            alert("【耕地面积<=占补平衡剩余耕地面积】不符合"); return false;
        }

        if (rows[0].batchTypeId == "4") {
            var remainAgriArea_batch = $("#fieldset_ds_balance_batch").find("span[name='remainAgriArea']").text();
            var remainAgriArea_balance = $("#fieldset_ds_balance_balance").find("span[name='remainAgriArea']").text();

            if (parseFloat(agriArea) > parseFloat(remainAgriArea_batch)) {
                alert("【农用地面积<=批次剩余农用地面积】不符合"); return false;
            }
            if (parseFloat(agriArea) > parseFloat(remainAgriArea_balance)) {
                alert("【农用地面积<=占补平衡剩余农用地面积】不符合"); return false;
            }
        }

        //判断题型补充至耕地
        if (parseFloat($("#fieldset_balance").find("span[name='remainAgriArea']").text()) == 0 && parseFloat($("#fieldset_balance").find("span[name='remainArabArea']").text()) > 0) {
            $.messager.confirm('提示', '该拆旧农用地=0 耕地>0,确定保存,补充至耕地吗？', function (r) {
                if (r) {
                    
                }
            });
        } else {
            var rowsBatch = $('#tt').datagrid("getSelections");
            var rowsDSBalance = $('#tt_ds_balance').datagrid("getSelections");
            if (rowsBatch.length != 1 && rowsDSBalance.length != 1) { alert("请返回选择批次及对应的占补平衡再添加！"); return; }
            $.getJSON('BatchHandler.ashx?method=addDemandSupplyBalanceToBatch', { batchId: rowsBatch[0].id, dsBalanceId: rowsDSBalance[0].id, agriArea: $('#agriAreaDSBalance').textbox('getValue'), arabArea: $('#arabAreaDSBalance').textbox('getValue') }, function (result) {
                if (result.flag) {
                    $('#fm_ds_balance_edit').form('clear');
                    $('#dlg_ds_balance_edit').dialog('close'); 	// close the dialog
                    $('#dlg_ds_balance').dialog('close');

                    $('#tt_batch_balance').datagrid('reload'); // reload the user data
                    $("#tt_batch_balance").datagrid('unselectAll');

                    reloadgrid();
                    //$('#tt').datagrid('reload'); // reload the user data
                    //$('#tt').datagrid('selectRow', $('#tt').datagrid('getRowIndex', $("#tt").datagrid("getSelected")));
                }
                $.messager.show({ title: '提示', msg: result.msg });
            });
        }

    }
</script>