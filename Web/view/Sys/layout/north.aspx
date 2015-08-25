<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="north.aspx.cs" Inherits="Maticsoft.Web.View.layout.north" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<link href="../../../css/base.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
.m-btn .l-btn-left .l-btn-text:hover{
    margin-right: 20px;
    color:#000;
}

.topleft img
{
    width: 80px;
height: 80px;
margin: 6px 20px;
    }
.sys_title {
    position: absolute;
left: 70px;
top: -10px;
color: white;
font-size: 37px;
font-weight: bold;
font-family: "宋体",Arial;
/*text-shadow: 5px 5px 7px #B2D1FA;*/
margin-left: 50px;
}
</style>
<script type="text/javascript" charset="utf-8">


    $.extend($.fn.validatebox.defaults.rules, {
        /*必须和某个字段相等*/
        equalTo: {
            validator: function (value, param) {
                return $(param[0]).val() == value;
            },
            message: '两次密码不一致！'
        }

    });

    //修改密码
    function editPassword(id) {
        $("#dlg_password").dialog("open");


    }

    //退出
    function logout(id) {
        if (id) {
            $.messager.confirm('<font color="#0e2d5f">确认</font>', '<strong>您确认退出系统吗？<strong>', function (r) {
                if (r) {
                    //清空session

                    //打开进度条
                    MaskUtil.mask();

                    //登陆标记
                    $.ajax({
                        url: 'UserMng/UserHandler.ashx?method=logout', 
                        dataType: 'json',
                        success: function (r) {
                            if (r.flag) {
                                location.href = "login.aspx";
                                MaskUtil.unmask();
                            } else {
                                MaskUtil.unmask();
                                return;
                            }
                        }
                    });

                } else {

                }
            });
        } else {//重新登陆 

        }
    }

    function info(id) {
        $("#dlg_info").dialog("open");

        

        $.getJSON('UserMng/UserHandler.ashx?method=getModelById', { id: id }, function (result) {
            $('#fm_info').find("input[name='id']").val(result._id);

            $("#name").validatebox({
                validType: "remote['/View/Sys/UserMng/UserHandler.ashx?method=checkName&id=" + id + "','name']"
            });

            $('#fm_info').find("input[name='name']").val(result._name);
            $('#birthday').datebox("setValue", result._birthday);
            $('#telephone').numberbox("setValue", result._telephone);
            $('#fm_info').find("input[name='remarks']").val(result._remarks);

        });
    }

    function saveInfo() {
        //提交之前验证
        if (!$("#fm_info").form('validate')) {
            return false;
        }

        $.getJSON('UserMng/UserHandler.ashx?method=modify', $("#fm_info").serialize(), function (result) {
            if (result.flag) {
                $('#dlg_info').dialog('close');
            }
            $.messager.show({
                title: '提示',
                msg: result.msg
            });
        });
    }

    function savePassword() {
        if (!$("#fm").form('validate')) {
            return false;
        }

        $.getJSON('UserMng/UserHandler.ashx?method=EditPassword', $("#fm").serialize(), function (result) {
            if (result.flag) {
                $('#dlg_password').dialog('close');
            }
            $.messager.show({
                title: '提示',
                msg: result.msg
            });
        });
    }
</script>

<div style="background:url(../../Images/topbg.gif) repeat-x; height:90px">
    <div class="topleft">
        <img src="../../../Images/logo.png"  title="系统首页"/>
        <p class="sys_title">镇江市国土资源耕地保护系统</p>
    </div>

    <div class="topright">    
    <ul>
    <li><span><img src="../../../Images/help.png" title="修改密码"  class="helpimg"/></span><a href="#" onclick="editPassword(<%=Session["user_id"] %>)">修改密码</a></li>
    <li><a href="#" onclick="info(<%=Session["user_id"] %>);">个人信息</a></li>
    <li><a href="#" onclick="logout(<%=Session["user_id"] %>);">退出</a></li>
    </ul>
     
    <div class="user">
    <span> [<strong><%= Session["user_name"]%></strong>]，身份[<strong><%= Session["role_name"]%></strong>]，欢迎您！</span>
    </div>    
    
    </div>
    <%-- 弹出操作框--%>
    <div id="dlg_password" class="easyui-dialog" title="修改密码" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg_password_buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">修改密码</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>新密码：</label>
                <input name="newpassword" id="newpassword" type="password" class="easyui-validatebox" data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>密码确认：</label>
                <input name="repassword" type="password" class="easyui-validatebox" required="required" validType="equalTo['#newpassword']"/>
            </div>
        </form>
    </div>
    <div id="dlg_password_buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="savePassword()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_password').dialog('close')">关闭</a>
    </div>


    <%-- 弹出操作框--%>
    <div id="dlg_info" class="easyui-dialog" title="个人信息" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg_info_buttons'">
        <div class="ftitle">会员信息</div>
        <form id="fm_info" method="post">
            <div class="fitem">
                <input name="id" type="hidden"/>
                <label>用户名：</label>
                <input id="name" name="name" class="easyui-validatebox" validtype="remote['UserHandler.ashx?method=checkName','name']" invalidMessage="用户名已存在"/>
            </div>
            <div class="fitem">
                <label>密码：</label>
                <input id="password" name="password"  class="easyui-validatebox" validType="length[3,20]"  type="password"  data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>密码确认：</label>
                <input name="repassword"  class="easyui-validatebox"  type="password" data-options="required:true" validType="equalTo['#password']" invalidMessage="两次输入密码不匹配"/>
            </div>
            <div class="fitem">
                <label>生日：</label>
                <input id="birthday" name="birthday"  class="easyui-datebox" editable="false"/>
            </div>
            <div class="fitem">
                <label>手机号：</label>
                <input id="telephone" name="telephone" class="easyui-numberbox"/>
            </div>
            <div class="fitem">
                <label>地址：</label>
                <input id="address" name="address"/>
            </div>
            <div class="fitem">
                <label>备注:</label>
                <textarea  id="remarks" name="remarks" style="width:150px;"></textarea>
            </div>
        </form>
    </div>
    <div id="dlg_info_buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="saveInfo()">保存</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg_info').dialog('close')">关闭</a>
    </div>
</div>


