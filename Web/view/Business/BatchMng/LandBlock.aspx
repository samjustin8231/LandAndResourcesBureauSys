<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandBlock.aspx.cs" Inherits="Maticsoft.Web.View.Business.BatchMng.LandBlock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<div id="dlgLandBlock" style=" padding: 10px 20px">
    <fieldset id="fieldset_landblock" style="text-align:center;font-weight:bold;">
        <legend>批次地块信息</legend>
        <div>
            申请用地面积:<span name="totalArea" style="color:Red;">50.0000</span><span style="color:Red;">公顷</span>&nbsp;&nbsp;&nbsp;
            还需用地面积:<span name="remainArea" style="color:green;">50.0000</span><span style="color:green;">公顷</span>
        </div>
    </fieldset>

    <div class="ftitle">地块信息</div>
    <form id="fm_Land_Block" name="fm" method="post">
        <div class="fitem">
            <label>名称：</label>
            <input  name="id" type="hidden" />  
            <input  name="batchId" type="hidden" />  
            <input name="name" disabled="disabled"/>
        </div>
        
        <div class="fitem">
            <label>面积：</label>
            <input id="area" name="area" class="easyui-numberbox" style="width:150px;" required="required" data-options="">
        </div>
        
        <div class="fitem">
            <label>用途：</label>
            <textarea name="des" style="width:150px;"></textarea>
        </div>
    </form>

    
</div>
