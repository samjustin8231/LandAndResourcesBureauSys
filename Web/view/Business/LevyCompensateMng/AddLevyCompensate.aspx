<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddLevyCompensate.aspx.cs" Inherits="Maticsoft.Web.View.Business.LevyCompensateMng.AddLevyCompensate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加征收补偿</title>
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

    <style type="text/css">
        #fm th{width:120px;}
        
        #table tr{text-align:left;}
        #table tr,th{padding:2px;}
        #table th{width: 86px;line-height: 34px;}
        #table td{width: 347px;height: 34px;}
    </style>

    <script type="text/javascript" charset="utf-8">
        var batchTypeId ;
        var batchId;
        var landblockId;

        $(function () {
            batchTypeId = $("#batchTypeId").combobox({
                url: '/View/Business/BatchTypeMng/BatchTypeHandler.ashx?method=getList',
                valueField: 'id',
                textField: 'name',
                required: 'true',
                editable: true,
                value: "0",
                onLoadSuccess: function () {

                },
                onSelect: function (record) {
                    if (record.id == 0 || record.id == "0") {
                        $("#fm").find("span[name='year']").text("未选择");
                        $("#fm").find("span[name='administratorAreaName']").text("未选择");

                        $("#fm").find("span[name='totalArea']").text("未选择");
                        $("#fm").find("span[name='remainArea']").text("未选择");
                    } 

                    batchId = $('#batchId').combobox({
                        valueField: 'id',
                        textField: 'name',
                        url: "/View/Business/BatchMng/BatchHandler.ashx?method=getListByTypeId&isDeleted=0&typeId=" + record.id,
                        panelHeight: 'auto',
                        required: true,
                        editable: true,
                        value: "0",
                        onLoadSuccess: function () {

                        },
                        onSelect: function (record) {
                            //回显
                            if (record.id == 0 || record.id == "0") {
                                $("#fm").find("span[name='year']").text("未选择");
                                $("#fm").find("span[name='administratorAreaName']").text("未选择");

                                $("#fm").find("span[name='totalArea']").text("未选择");
                                $("#fm").find("span[name='remainArea']").text("未选择");
                            } else {
                                $("#fm").find("span[name='year']").text(record.year);

                                $("#fm").find("input[name='year']").val(record.year);
                                $("#fm").find("input[name='administratorArea']").val(record.administratorarea);
                                //获取行政区
                                $.getJSON("/View/Sys/ajax/UtilHandler.ashx?method=getAdministratorNameById&id=" + record.administratorarea, function (data) {
                                    $("#fm").find("span[name='administratorAreaName']").text(data._name);

                                });
                            }

                            landblockId = $('#landblockId').combobox({
                                valueField: 'id',
                                textField: 'name',
                                url: "/View/Business/LandBlockMng/LandBlockHandler.ashx?method=GetListForCombo&batchId=" + record.id,
                                panelHeight: 'auto',
                                required: true,
                                editable: true,
                                value: "0",
                                onSelect: function (record) {
                                    if (record.id == 0 || record.id == "0") {
                                        $("#fm").find("span[name='totalArea']").text("未选择");
                                        $("#fm").find("span[name='remainArea']").text("未选择");
                                    } else {
                                        //回显
                                        $("#fm").find("span[name='totalArea']").text(record.area);

                                        //回显剩余面积
                                        //ajax获取二级
                                        $.getJSON("../LandBlockMng/LandBlockHandler.ashx?method=getRemainLandBlock", { landblockId: record.id }, function (data) {
                                            $("#fm").find("span[name='remainArea']").text(data.msg);
                                        });
                                    }


                                }
                            });
                        }
                    });
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

            var remainArea = $("#fm").find("span[name='remainArea']").text();
            var planLevyArea = $("#planLevyArea").numberbox("getValue");
            var levyNationalLandArea = $("#levyNationalLandArea").numberbox("getValue");
            var levyColletLandArea = $("#levyColletLandArea").numberbox("getValue");
            console.info(planLevyArea);
            if (parseFloat(planLevyArea) > parseFloat(remainArea)) {
                alert("【地块剩余面积 >= 拟征收面积】不符合"); return false;
            }
            if (parseFloat(planLevyArea) != parseFloat(levyNationalLandArea) + parseFloat(levyColletLandArea)) {
                alert("【拟征收面积 = 收回国有土地 + 征收集体土地】不符合"); return false;
            }

            $.getJSON('LevyCompensateHandler.ashx?method=add', $("#fm").serialize(), function (result) {
                if (result.flag) {
                    clearData();
                }
                alert(result.msg);
            });
        }

        function clearData() {
            $("#fm").form("clear");

            //$("#batchTypeId").combobox("setValue", 0);
            //$("#batchId").combobox("reload");
            //$("#landblockId").combobox("clear");
        }

    
    </script>
</head>
<body>
    <div class="formbody">
    
        <div class="formtitle"><span>基本信息</span></div>
    
        <form id="fm" runat="server">
            <table id="table" width="99%" cellspacing="0" cellpadding="1" border="0" align="center">
                <tr>
                    <input  name="id" type="hidden" />
                    <input  name="year" type="hidden" />
                    <input  name="administratorArea" type="hidden" />    

                    <th>批次种类</th>
                    <td><select id="batchTypeId" name="batchTypeId" class="dfinput easyui-combobox" style=width:347px;height:34px;" data-options="required:true"/></td>
                    <th>批次号</th>
                    <td><select id="batchId" name="batchId" class="dfinput easyui-combobox" style=width:347px;height:34px;" data-options="required:true"/></td>
                    
                </tr>
                
                <tr>
                    <th>地块</th>
                    <td><select id="landblockId" name="landblockId" class="dfinput easyui-combobox" style=width:347px;height:34px;" data-options="required:true"/></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="width:347px;height:28px;padding-top:8px;">
                            年份：<span  name="year" style="color:Red;" >未选择</span>&nbsp;
                            行政区：<span name="administratorAreaName" style="color:Red;">未选择</span>
                        </div></td>
                    <td colspan="2">
                        <div style="width:347px;height:34px;">
                        地块总面积：<span name="totalArea" style="color:Red;" >未选择</span>&nbsp;
                        剩余面积：<span name="remainArea" style="color:Red;">未选择</span>
                        </div></td>
                </tr>
                <tr>
                    <th>拟征收面积</th>
                    <td><input id="planLevyArea" name="planLevyArea" class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;" data-options="required:true"/></td>
                    <th>收回国有土地</th>
                    <td><input id="levyNationalLandArea" name="levyNationalLandArea"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;" data-options="required:true"/></td>
                    
                </tr>
                <tr>
                    <th>征收集体土地</th>
                    <td><input id="levyColletLandArea" name="levyColletLandArea"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;" data-options="required:true"/></td>
                    <th>乡镇</th>
                    <td><input name="town" type="text"  class="easyui-textbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                    
                </tr>
                <tr>
                    <th>村（社区、单位）</th>
                    <td><input name="village"  class="easyui-textbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>小组</th>
                    <td><input name="_group"  class="easyui-textbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                    
                </tr>
                <tr>
                    <th>已征收</th>
                    <td><input name="hasLevyArea"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>乡下社保资金</th>
                    <td><input name="countrySocialSecurityFund"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                </tr>
                <tr>
                    <th>安置人数</th>
                    <td><input name="totalPeopleNumber"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>区级水利设施补偿</th>
                    <td><input name="areaWaterFacilitiesCompensate"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                </tr>
                <tr>
                    <th>省市农重金</th>
                    <td><input name="provHeavyAgriculturalFunds"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>省市新增费</th>
                    <td><input name="provAdditionalFee"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                </tr>
                <tr>
                    <th>省市开垦费</th>
                    <td><input name="provReclamationFee"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>省市耕占税</th>
                    <td><input name="provArableLandTax"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                </tr>
                <tr>
                    <th>省市勘测费</th>
                    <td><input name="provSurveyFee"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>省市服务费</th>
                    <td><input name="provServiceFee"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    
                </tr>
                <tr>
                    <th>征地预存款</th>
                    <td><input name="levyPreDeposit"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                    <th>乡下对下补偿</th>
                    <td><input name="countryCompensate"  class="easyui-numberbox dfinput" type="text" style="width:347px;height:34px;"/></td>
                </tr>
                <tr>
                    <th>备注</th>
                    <td colspan="3"><textarea name="des" style="width:920px;"></textarea></td>
                </tr>
                <tr align="center">
                    <th></th>
                    <td colspan="2">
                        <input type="button" class="btn" value="确认保存" onclick="save()"/>&nbsp;&nbsp;<input type="button" class="btn" value="清空" onclick="clearData()"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
