<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVerif.aspx.cs" Inherits="Maticsoft.Web.View.Business.BatchMng.AddVerif" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<div id="dlgVerif" style="padding: 10px 20px">
    <div class="ftitle">基本农田信息</div>
    <form id="fm_Verif" name="fm" method="post">
        <div class="fitem">
            <label>年份：</label>
            <input  name="id" type="hidden" />  
            <input  name="verifId" type="hidden" />  
            <input  name="batchId" type="hidden" />  
            <input  name="year" type="hidden" />  
            <input  name="administratorArea" type="hidden" />  
            <label class="label_center" name="year" style="color:Red;"></label>
        </div>
        <div class="fitem">
            <label>行政区：</label>
            <label class="label_center" name="administratorArea" style="color:Red;"></label>
        </div>
        <div class="fitem">
            <label>多划基本农田面积：</label>
            <label class="label_center" name="divisionArea" style="color:Red;"></label>
        </div>
        <div class="fitem">
            <label>剩余多划基本农田面积：</label>
            <label class="label_center" name="remainDivisionArea" style="color:Red;"></label>
        </div>
        <div class="fitem">
            <label>核销省级面积：</label>
            <input name="verifProvArea" style="width:150px;" required="required" data-options="">
        </div>
        <div class="fitem">
            <label>核销省级耕地面积：</label>
            <input name="verifProvArableArea" style="width:150px;" required="required" data-options="">
        </div>
        <div class="fitem">
            <label>核销本级面积：</label>
            <input name="verifSelfArea" style="width:150px;" required="required" data-options="">
        </div>
        <div class="fitem">
            <label>核销本级耕地面积：</label>
            <input id="verifSelfArableArea" name="verifSelfArableArea" style="width:150px;" required="required" data-options="">
        </div>
    </form>

</div>
