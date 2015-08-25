<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifLogList.aspx.cs" Inherits="Maticsoft.Web.View.Business.VerifMng.VerifLogList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>基本农田核销列表</title>

    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />

    <link href="../../../js/kindeditor-4.1.10/themes/default/default.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/datagrid-detailview.js" type="text/javascript"></script>
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>


    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form

        var datagrid;   //
        var editRow = undefined;

       

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

        function formatterNameF(value, rowData, rowIndex) {
            return "<span style='font-weight:bold;'>" + value + "</span>";
        }

        function onBeforeOpenBatchDlg() {

            var rows = $("#tt").datagrid("getRows");
            var row = rows[rowIndexPlan];

            if (row.verifTypeId == "6") {//增减挂钩批次
                $("#fieldset_batch").find("a[name='span1']").hide();
                $("#fieldset_verif").find("a[name='span1']").hide();
                $("#fieldset_batch").find("a[name='span2']").show();
                $("#fieldset_verif").find("a[name='span2']").show();
            } else {
                $("#fieldset_batch").find("a[name='span2']").hide();
                $("#fieldset_verif").find("a[name='span2']").hide();
                $("#fieldset_batch").find("a[name='span1']").show();
                $("#fieldset_verif").find("a[name='span1']").show();
            }

            //根据verifId动态获取批次信息
            var queryParams = $('#tt_batch').datagrid('options').queryParams;
            queryParams.verifId = row['id'];
            $('#tt_batch').datagrid('options').queryParams = queryParams;
            $("#tt_batch").datagrid('reload');
            $("#tt_batch").datagrid('unselectAll');

            //隐藏批次信息
            $("#fieldset_batch").hide();

            //更新核销信息
            $("#fieldset_verif").find("span[name='totalConsArea']").text(row["consArea"]);
            $("#fieldset_verif").find("span[name='remainConsArea']").text(row["remainConsArea"]);
            $("#fieldset_verif").find("span[name='totalAgriArea']").text(row["agriArea"]);
            $("#fieldset_verif").find("span[name='remainAgriArea']").text(row["remainAgriArea"]);
            $("#fieldset_verif").find("span[name='totalArabArea']").text(row["arabArea"]);
            $("#fieldset_verif").find("span[name='remainArabArea']").text(row["remainArabArea"]);

            $("#fieldset_verif").find("span[name='totalIssuedQuota']").text(row["issuedQuota"]);
            $("#fieldset_verif").find("span[name='remainIssuedQuota']").text(row["remainIssuedQuota"]);
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

        function onClickRowBatchTT(index, row) {
            $("#fieldset_batch").show()

            if (true) {//增减挂钩批次

            } else {

            }

            //更新批次信息;
            $("#fieldset_batch").find("span[name='totalConsArea']").text(row["consAreaBatch"]);
            $("#fieldset_batch").find("span[name='totalAgriArea']").text(row["agriAreaBatch"]);
            $("#fieldset_batch").find("span[name='totalArabArea']").text(row["arabAreaBatch"]);
            $("#fieldset_batch").find("span[name='totalArabArea']").text(row["arabAreaBatch"]);
            $("#fieldset_batch").find("span[name='totalIssuedQuota']").text(row["arabAreaBatch"]);

            $.getJSON("/View/Business/BatchMng/BatchHandler.ashx?method=GetRemainPlanArea", { batchId: row['batchId'] }, function (result) {
                //回显信息 
                $("#fieldset_batch").find("span[name='remainConsArea']").text(result.consArea);
                $("#fieldset_batch").find("span[name='remainAgriArea']").text(result.agriArea);
                $("#fieldset_batch").find("span[name='remainArabArea']").text(result.arabArea);
                $("#fieldset_batch").find("span[name='remainIssuedQuota']").text(result.issuedQuota);
            });
        }

        function formatterDetailF(value, rowData, rowIndex) {
            return "<a href='#' style='text-decoration: none;' onclick='lookDetail(" + rowIndex + ")'>[ 查看使用情况 ]</a>";
        }

        function lookDetail(rowIndex) {
            if (rowIndex == undefined) {
                rowIndexPlan = $("#tt").datagrid("getRowIndex", $("#tt").datagrid("getSelected"));
            } else {
                rowIndexPlan = rowIndex;
            }

            if (rowIndexPlan != undefined) {
                $('#dlg_batch').dialog({
                    title: '使用该核销的批次信息',
                    modal: true,
                    onLoad: function () {//没用
                        alert("a");
                        //刷新批次列表


                    },
                    onClose: function () {

                    }
                }).dialog("open");

            }
        }

        //增加查询参数，重新加载表格
        function reloadgrid_batch() {
            //查询参数直接添加在queryParams中    
            var queryParams = $('#tt_batch').datagrid('options').queryParams;
            getQueryParamsBatch(queryParams);
            $('#tt_batch').datagrid('options').queryParams = queryParams;
            $("#tt_batch").datagrid('reload');
            $("#fieldset_batch").hide()
            $("#tt_batch").datagrid('unselectAll');
        }

        //获取参数     
        function getQueryParamsBatch(queryParams) {
            var name = $("#fm_search_batch").find("input[name='name']").val();
            var year = $("#fm_search_batch").find("input[name='year']").val();
            var administratorArea = $("#fm_search_batch").find("input[name='administratorArea']").val();
            var batchTypeId = $("#fm_search_batch").find("input[name='batchTypeId']").val();

            queryParams.name = name;
            queryParams.year = year;
            queryParams.administratorArea = administratorArea;
            queryParams.batchTypeId = batchTypeId;

            return queryParams;

        }

        function onDblClickRowF(index, row) {
            lookDetail(index);
        }

        function detailFormatterF(index, row) {
            return '<div style="padding:2px"><div><table id="ddv-' + index + '"></table></div>';
        }

        function onExpandRowF(index, row) {

            $('#ddv-' + index).datagrid({
                url: '/View/Business/BatchMng/BatchHandler.ashx?method=GetBatchListByVirifId&verifId=' + row.id,
                fitColumns: false,
                singleSelect: true,
                height: 'auto',
                columns: [[
                    { field: 'id', title: 'id', hidden: 'true', width: 100, align: 'center' },
                    { field: 'batchName', title: '批次名称', width: 200, align: 'center' },
                    { field: 'administratorAreaName', title: '行政区', width: '120', align: 'center' },
                    { field: 'batchTypeName', title: '批次类型', width: 100, align: 'center' },
                    { field: 'totalArea', title: '用地总面积', width: 150, align: 'center' },
                    { field: 'hasLevyArea', title: '已征收面积', width: 150, align: 'center' },
                    { field: 'agriAreaBatch', title: '农用地面积', width: 150, align: 'center' },
                    { field: 'arabAreaBatch', title: '耕地面积', width: 150, align: 'center' },
                ]],
                onResize: function () {
                    $('#tt').datagrid('fixDetailRowHeight', index);
                },
                onLoadSuccess: function () {
                    setTimeout(function () {
                        $('#tt').datagrid('fixDetailRowHeight', index);
                    }, 0);
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
    <table id="tt" title="基本农田核销列表" class="easyui-datagrid" border="false" fit="true" style="width: auto; height: 500px;"        
        idfield="id" pagination="true" data-options="onRowContextMenu:onRowContextMenuF,singleSelect:true,onDblClickRow:onDblClickRowF,iconCls:'icon-tip',url:'../VerifBatchMng/VerifBatchHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb',striped:true,nowrap:false,onBeforeOpen:beforeDataGridOpen,view: detailview,detailFormatter:detailFormatterF,onExpandRow:onExpandRowF" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'year',width:120,align:'center',sortable:'true'">年份</th>   
                <th data-options="field:'administratorAreaName',width:120,align:'center',sortable:'true'">行政区</th>  
                <th data-options="field:'divisionArea',width:120,align:'center',sortable:'true'">基本农田多划面积</th>   

                <th data-options="field:'verifProvArea',width:120,align:'center',sortable:'true'">核销省级土地面积</th>   
                <th data-options="field:'verifProvArableArea',width:120,align:'center',sortable:'true'">核销省级耕地面积</th>   
                <th data-options="field:'verifSelfArea',width:120,align:'center',sortable:'true'">核销本级土地面积</th>   
                <th data-options="field:'verifSelfArableArea',width:120,align:'center',sortable:'true'">核销本级耕地面积</th>   

                <!--<th data-options="field:'createUserName',width:120,align:'center',sortable:'true'">创建人</th>    
                <th data-options="field:'createTime',width:120,align:'center',sortable:'true'">创建时间</th>
                <th data-options="field:'des',width:120,align:'center',sortable:'true'">描述</th>        -->
                <th data-options="field:'opt',align:'center',width:200,sortable:'true',formatter:formatterDetailF">操作</th> 
            </tr>
        </thead>
         <%--表格内容--%>
    </table>
    <%--功能区--%>
    <div id="tb" style="padding: 5px; height: auto">
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
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>&nbsp;&nbsp;&nbsp;&nbsp;

            <a href="javascript:void(0)" onclick="reloadgrid()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
            </form>
        </div>
    </div>


    <%-- 弹出操作框--%>
    <div id="dlg_batch" class="easyui-dialog" style="width: 970px; height: 560px;"
        data-options="collapsible:true,closed:true,buttons:'#dlg-buttons',onBeforeOpen:onBeforeOpenBatchDlg"> <%--closed="true" buttons="#dlg-buttons"--%>
        
        <table id="tt_batch" class="easyui-datagrid" border="false" fit="true" fitColumns="true" style="width: auto; height: 500px;"        
            idfield="id" pagination="true" data-options="iconCls:'icon-tip',singleSelect:true,rownumbers:true,url:'/View/Business/BatchMng/BatchHandler.ashx?method=GetBatchListByVirifId',pageSize:20, pageList:[10,20,30,40,50],method:'get',toolbar:'#tb_batch',striped:true,nowrap:false,onClickRow:onClickRowBatchTT"> <%--striped="true"--%>
            <%-- 表格标题--%>
            <thead>
                <tr>
                    <th data-options="field:'id',hidden:true">批次号</th>
                    <th data-options="field:'batchName',width:200,align:'center',sortable:'true',formatter:formatterNameF">批次名称</th>
                    <th data-options="field:'administratorAreaName',width:120,align:'center',sortable:'true',formatter:formatterAdministratorAreaF">行政区</th>  
                    <!-- <th data-options="field:'approvalNo',align:'center',sortable:'true'">批准文号</th>
                    <th data-options="field:'approvalTime',align:'center',sortable:'true'">批准时间</th>
                    <th data-options="field:'addConsArea',align:'center',sortable:'true'">新增建设用地面积</th>
                    <th data-options="field:'unusedArea',align:'center',sortable:'true'">未利用地面积</th>   
                    <th data-options="field:'hasLevyArea',width:100,align:'center',sortable:'true'">已征收面积</th> 
                    <th data-options="field:'consArea',align:'center',sortable:'true'">建设用地面积</th>      -->

                    <th data-options="field:'batchTypeName',width:120,align:'center',sortable:'true'">批次类型</th>  
                    <th data-options="field:'totalArea',width:100,align:'center',sortable:'true'">用地总面积</th>   
                    <th data-options="field:'consAreaBatch',align:'center',sortable:'true'">建设用地面积</th>
                    <th data-options="field:'agriAreaBatch',width:100,align:'center',sortable:'true'">农用地面积</th>   
                    <th data-options="field:'arabAreaBatch',width:100,align:'center',sortable:'true'">耕地面积</th>   
                    
                </tr>
            </thead>
        </table>
        

        <%--功能区--%>
        <div id="tb_batch" style="padding: 5px; height: auto">
            <%-- 查找Account信息，根据注册时间、姓名 --%>
            <div>
               <form id="fm_search_batch" method="post">
               名称: 
                <input name="name"/>&nbsp;
                年份: 
                <input name="year" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'">
                行政区: 
                <input name="administratorArea" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList'">
                批次类型: 
                <input name="batchTypeId" class="easyui-combobox" style="width:100px;"  data-options="valueField:'id',textField:'name',url:'../BatchTypeMng/BatchTypeHandler.ashx?method=getList'">
            
                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid_batch()">查询</a>
                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearDataBatch()">清空</a>
                <a href="javascript:void(0)" onclick="reloadgrid_batch()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
                </form>
            </div>
            <%--信息回显 --%>
            <div>
                
                <fieldset id="fieldset_verif" style="width:900px;;text-align:center;font-weight:bold;">
                    <legend>选中核销信息</legend>
                    <div>
                        <a name="span1">
                            总建设地面积:<span name="totalConsArea" class="blue">50.0000</span><span class="blue">公顷</span>&nbsp&nbsp;&nbsp;
                            总农用地面积:<span name="totalAgriArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                            总耕地面积:<span name="totalArabArea" class="blue">50.0000</span><span class="blue">公顷</span>
                        </a>
                        <a name="span2">
                            总下达指标:<span name="totalIssuedQuota" class="red">50.0000</span><span class="red">公顷</span>
                        </a>
                    </div>
                    <div>
                        <a name="span1">
                            剩余建设地面积:<span name="remainConsArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                            剩余农用地面积:<span name="remainAgriArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                            剩余耕地面积:<span name="remainArabArea" class="green">50.0000</span><span class="green">公顷</span>
                        </a>
                        <a name="span2">
                            剩余下达指标:<span name="remainIssuedQuota" class="green">50.0000</span><span class="green">公顷</span>
                        </a>
                    </div>
                </fieldset>

                <fieldset id="fieldset_batch" style="width:900px;text-align:center;font-weight:bold;display:none;">
                    <legend>该批次信息</legend>
                    <div>
                        <a name="span1">
                            总建设地面积:<span name="totalConsArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                            总农用地面积:<span name="totalAgriArea" class="red">50.0000</span><span class="red">公顷</span>&nbsp&nbsp;&nbsp;
                            总耕地面积:<span name="totalArabArea" class="red">50.0000</span><span class="red">公顷</span>
                        </span>
                        <a name="span2">
                            总下达指标:<span name="totalIssuedQuota" class="red">50.0000</span><span class="red">公顷</span>
                        </a>
                    </div>
                    <div>
                        <a name="span1">
                            剩余建设地面积:<span name="remainConsArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                            剩余农用地面积:<span name="remainAgriArea" class="green">50.0000</span><span class="green">公顷</span>&nbsp&nbsp;&nbsp;
                            剩余耕地面积:<span name="remainArabArea" class="green">50.0000</span><span class="green">公顷</span>
                        </a>
                        <a name="span2">
                            剩余下达指标:<span name="remainIssuedQuota" class="green">50.0000</span><span class="green">公顷</span>
                        </a>
                    </div>
                </fieldset>
            </div>
        </div>
        
    </div>
    <div id="menu" class="easyui-menu" style="width: 120px; display: none;">
        <div onclick="lookDetail();" iconcls="icon-search">
            查看使用情况</div>
    </div>
</body>
</html>
