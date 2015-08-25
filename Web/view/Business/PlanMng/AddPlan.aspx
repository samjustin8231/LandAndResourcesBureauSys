<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPlan.aspx.cs" Inherits="Maticsoft.Web.View.Business.PlanMng.AddPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加计划</title>
    <link href="../../../css/members/css.css" rel="stylesheet" type="text/css" />
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
        $(function () {
            var artChanged = false;
            var selectRow;
            $('#administratorArea').combogrid({
                panelWidth: 400, idField: 'id', textField: 'name',
                url: '/View/Business/AdministratorAreaMng/AdministratorAreaHandler.ashx?method=getList',
                fitColumns: true,
                required:true,
                striped: true,
                pagination: true,           //是否分页
                columns: [[
                    { field: 'id', title: 'id', hidden: true, width: 60 },
                    { field: 'name', title: '名称', width: 100 },
                    { field: 'initial', title: '缩写', width: 100 },
                ]],
                onChange: function (newValue, oldValue) {
                    artChanged = true; //记录是否有改变（当手动输入时发生)  
                },
                onHidePanel: function () {

                    var t = $(this).combogrid('getValue');
                    if (artChanged) {
                        if (selectRow == null || t != selectRow.id) {//没有选择或者选项不相等时清除内容  
                            alert('请选择，不要直接输入');
                            $(this).combogrid('setValue', '');
                        } else {
                            //do something...  
                        }
                    }
                    artChanged = false;
                    selectRow = null;

                },
                onSelect: function (index, row) {
                    selectRow = row;
                },
                keyHandler: {
                    up: function () {
                        //取得选中行
                        var selected = $('#administratorArea').combogrid('grid').datagrid('getSelected');
                        if (selected) {
                            //取得选中行的rowIndex
                            var index = $('#administratorArea').combogrid('grid').datagrid('getRowIndex', selected);
                            //向上移动到第一行为止
                            if (index > 0) {
                                $('#administratorArea').combogrid('grid').datagrid('selectRow', index - 1);
                            }
                        } else {
                            var rows = $('#administratorArea').combogrid('grid').datagrid('getRows');
                            $('#administratorArea').combogrid('grid').datagrid('selectRow', rows.length - 1);
                        }
                    },
                    down: function () {
                        //取得选中行
                        var selected = $('#administratorArea').combogrid('grid').datagrid('getSelected');
                        if (selected) {
                            //取得选中行的rowIndex
                            var index = $('#administratorArea').combogrid('grid').datagrid('getRowIndex', selected);
                            //向下移动到当页最后一行为止
                            if (index < $('#administratorArea').combogrid('grid').datagrid('getData').rows.length - 1) {
                                $('#administratorArea').combogrid('grid').datagrid('selectRow', index + 1);
                            }
                        } else {
                            $('#administratorArea').combogrid('grid').datagrid('selectRow', 0);
                        }
                    },
                    enter: function () {
                        //选中后让下拉表格消失
                        $('#administratorArea').combogrid('hidePanel');
                        selectRow = $('#administratorArea').combogrid('grid').datagrid('getSelected');
                    },
                    query: function (q) {
                        //动态搜索  
                        $('#administratorArea').combogrid("grid").datagrid("reload", { 'keyword': q });
                        $('#administratorArea').combogrid("setValue", q);
                    }
                }
            });
        });

        //保存信息
        function save() {

            //提交之前验证
            if (!$("#fm").form('validate')) {
                return false;
            }

            //验证数量大小关系
            var consArea = $("#fm").find("input[name='consArea']").val();
            var agriArea = $("#fm").find("input[name='agriArea']").val();
            var arabArea = $("#fm").find("input[name='arabArea']").val();

            if (parseFloat(consArea) < parseFloat(agriArea)) {
                alert("【计划下达新增建设用地>=计划下达农用地】不符合"); return false;
            }
            if (parseFloat(consArea) < parseFloat(arabArea)) {
                alert("【计划下达新增建设用地>=计划下达耕地】不符合"); return false;
            }
            if (parseFloat(agriArea) < parseFloat(arabArea)) {
                alert("【计划下达农用地>=计划下达耕地】不符合"); return false;
            }

            $.getJSON('PlanHandler.ashx?method=add', $("#fm").serialize(), function (result) {
                if (result.flag) {
                    clearData();
                }
                alert(result.msg);
            });
        }

        function clearData() {
            $("#fm").form("clear");
        }

        function onSelectPlanTypeF(record) {
            console.info(record);

            if (record.id == "6") {//增减挂钩计划字段不一样
                $("#consArea").numberbox("disableValidation");
                $("#agriArea").numberbox("disableValidation");
                $("#arabArea").numberbox("disableValidation");

                $("#issuedQuota").numberbox("enableValidation");

                $("#fm").find("input[name='consArea']").parent().parent().hide();
                $("#fm").find("input[name='agriArea']").parent().parent().hide();
                $("#fm").find("input[name='arabArea']").parent().parent().hide();

                $("#fm").find("input[name='issuedQuota']").parent().parent().show();
                
            } else {
                $("#issuedQuota").numberbox("disableValidation");

                $("#consArea").numberbox("enableValidation");
                $("#agriArea").numberbox("enableValidation");
                $("#arabArea").numberbox("enableValidation");

                $("#fm").find("input[name='consArea']").parent().parent().show();
                $("#fm").find("input[name='agriArea']").parent().parent().show();
                $("#fm").find("input[name='arabArea']").parent().parent().show();

                $("#fm").find("input[name='issuedQuota']").parent().parent().hide();
                
            }
        }
    </script>
</head>
<body>
    <div class="formbody">
    
        <div class="formtitle"><span>基本信息</span></div>
    
        <form id="fm" runat="server">
            <input id="id" name="id" type="hidden" />  
            <ul class="forminfo">
                <li><label>计划类型</label><input id="planTypeId" name="planTypeId" class="dfinput easyui-combobox" style=width:347px;height:34px;"  data-options="onSelect:onSelectPlanTypeF,valueField:'id',textField:'name',required:true,url:'../PlanTypeMng/PlanTypeHandler.ashx?method=getList'"> <i><span style="color:red;">*</span> 请选择计划类型</i></li>
                <li><label>名称</label><input id="name" name="name" class="easyui-textbox dfinput" style="width:347px;height:34px;" data-options="required:true"/> <i><span style="color:red;">*</span> 计划名称</i></li>
                <li><label>年份</label><input id="year" name="year" class="dfinput easyui-combobox" style="width:347px;height:34px;"  data-options="valueField:'id',textField:'name',required:true,url:'../../Sys/ajax/UtilHandler.ashx?method=getYearList'"> <i><span style="color:red;">*</span> 请选择年份</i></li>
                <li><label>行政区</label><input id="administratorArea" name="administratorArea" value="" style="width:347px;height:34px;"> <i><span style="color:red;">*</span> 支持模糊查询</i></li>
                <li><label>是否提交</label>
                    <select id="isSubmited" class="easyui-combobox" name="isSubmited" style="width:100px;">    
                        <option  value="1">是</option>
                        <option  value="0">否</option>  
                    </select>
                </li>
                <li><label>建设用地面积</label><input id="consArea" name="consArea" class="easyui-numberbox dfinput" style="width:347px;height:34px;" data-options="required:true"/> <i><span style="color:red;">*</span> 请填写数字<span style="color:red;">（新增建设用地≥计划下达农用地≥计划下达耕地）</span></i></li>
                <li><label>农用地面积</label><input id="agriArea" name="agriArea" class="easyui-numberbox dfinput" style="width:347px;height:34px;" data-options="required:true"/> <i><span style="color:red;">*</span> 请填写数字</i></li>
                <li><label>耕地面积</label><input id="arabArea" name="arabArea" class="easyui-numberbox dfinput" style="width:347px;height:34px;" data-options="required:true"/> <i><span style="color:red;">*</span> 请填写数字</i></li>
                <li style="display:none;"><label>下达指标</label><input id="issuedQuota" name="issuedQuota" class="easyui-numberbox dfinput" style="width:347px;height:34px;" data-options="required:true"/> <i><span style="color:red;">*</span> 请填写数字</i></li>
                <li><label>文号</label><input id="releaseNo" name="releaseNo" class="easyui-textbox dfinput" style="width:347px;height:34px;" data-options="required:true"/> <i><span style="color:red;">*</span> 填写文号</i></li>
                <li><label>文号时间</label><input id="releaseTime" name="releaseTime" style="width:347px;height:34px" class="easyui-datebox" editable="false" data-options="required:true"/><i><span style="color:red;">*</span> 选择注册时间</i></li>
                <li><label>备注</label><textarea id="remarks" name="remarks" cols="10" rows="5" class="dfinput" style="width:500;height:100px;" ></textarea></li>
                <li><label>&nbsp;</label>
                    <input type="button" class="btn" value="确认保存" onclick="save()"/>&nbsp;&nbsp;<input type="button" class="btn" value="清空" onclick="clearData()"/>
                </li>
            </ul>
        </form>
    </div>
</body>
</html>
