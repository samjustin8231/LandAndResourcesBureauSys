<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchSummaryList.aspx.cs" Inherits="Maticsoft.Web.View.Business.BatchMng.BatchSummaryList" %>

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
    <script src="../../../js/jquery-easyui-1.4.1/datagrid-detailview.js" type="text/javascript"></script>

    <style type="text/css">
        fieldset{border:1px solid #238914;}
        
        .green{color:Green !important;}
        .red{color:red !important;}
    </style>

    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form
        var _dialog;

        var dlg_plans;
        var dlg_ds_balances;
        var dlg_block;

        var dlg_plan;


        /*---------------------------------------------------------1、通用操作 -------------------------------------------------------*/
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
            //先获取选择行
            var rows = $('#tt').datagrid("getSelections");
            //如果只选择了一行则可以进行修改，否则不操作
            if (rows.length == 1) {
                var row = rows[0];
                //获取要修改的字段



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
            var method = document.getElementById("method").value = "delete";
            var rows = $('#tt').datagrid('getSelections');
            if (rows == null || rows.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return;
            }
            if (rows) {
                $.messager.confirm('提示', '你确定要删除【' + rows.length + '】条记录吗？一旦删除，与该批次相关的计划、占补平衡、地块关系将删除。', function (r) {
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

        //获取参数     
        function getQueryParams(queryParams) {
            var name = $("#fm_search").find("input[name='name']").val();
            var year = $("#fm_search").find("input[name='year']").val();
            var administratorArea = $("#fm_search").find("input[name='administratorArea']").val();
            var batchTypeId = $("#fm_search").find("input[name='batchTypeId']").val();

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
            queryParams.batchTypeId = batchTypeId;

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
            $("#tb").find("input").val("");

            //重新加载数据
            reloadgrid();
        }


        

        /*------------------------------------------------------事件----------------------------------------------------------*/

        //datagrid单击函数
        function onClickRowTTF() {
           
        }

        //datagrid选中一行事件
        function onSelectTTF(index,row){
    
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

        //加载dagargid中图片
        function loadLinkButton() {
            $(".add").linkbutton({ text: '添加基本农田', plain: true, iconCls: 'icon-add' });
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
       
        /*******************************************************基本农田核销开始***************************************************/

        //添加基本农田多划
        function addVerif() {
            var p = dialog({
                title: '添加基本农田信息',
                href: 'AddVerif.aspx',
                modal: true,
                width: 320,
                height: 450,
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


        function detailFormatterF(index, row) {
            return '<div style="padding:2px"><table id="ddv-' + index + '"></table></div>';  
        }

        function onExpandRowF(index, row) {
            
            $('#ddv-'+index).datagrid({  
                url:'/View/Business/BatchMng/BatchHandler.ashx?method=GetBatchListByDSId',
                fitColumns:true,  
                singleSelect:true,  
                height:'auto',  
                columns:[[  
                    {field:'id',title:'id',align:'center'},  
                    {field:'batchName',title:'批次名称',width:50,align:'center'},  
                    {field:'administratorAreaName',title:'行政区',align:'center'},  
                    {field:'batchTypeName',title:'批次类型',width:100,align:'center'},  
                    {field:'totalArea',title:'用地总面积',width:150,align:'center'}  
                ]],  
                onResize:function(){  
                    $('#tt').datagrid('fixDetailRowHeight',index);  
                },  
                onLoadSuccess:function(){  
                    setTimeout(function(){  
                        $('#tt').datagrid('fixDetailRowHeight',index);  
                    },0);  
                }
            });
            $('#tt').datagrid('fixDetailRowHeight', index);  
        }
        
    </script>

</head>
<body id="layout_batch" class="easyui-layout">
    <div data-options="region:'center'">
        <%----------------------------------------------------------批次表格显示区----------------------------------------------------------%>

        <table id="tt" title="批次列表" class="easyui-datagrid" border="false" fit="true" fitColumns="true" style="width: auto; height: 500px;"        
            idfield="id" pagination="true" data-options="view: detailview,iconCls:'icon-tip',singleSelect:true,rownumbers:true,url:'BatchHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onClickRow:onClickRowTTF,onLoadSuccess:onLoadSuccessTTF,onSelect:onSelectTTF,detailFormatter:detailFormatterF,onExpandRow:onExpandRowF" fitcolumns="true"> <%--striped="true"--%>
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
                <a href="javascript:void(0)" onclick="edit() " title="修改" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-edit',plain:true"></a>
                <a href="javascript:void(0)" onclick="del()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
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
                <input name="name"/>&nbsp;
                年份: 
                <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
                行政区: 
                <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
                批次类型: 
                <input name="batchTypeId" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../BatchTypeMng/BatchTypeHandler.ashx?method=getList'">
            
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
    </div>

</body>
</html>