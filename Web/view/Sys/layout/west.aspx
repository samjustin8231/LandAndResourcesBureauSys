<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="west.aspx.cs" Inherits="Maticsoft.Web.View.layout.west" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script src="../../../js/jsUtil.js" type="text/javascript"></script>

<style tyle="text/css">
    .menuson li:hover{background-color:#48B7EA;color:White;}
    .menuson li a:hover{color:White !important;}
</style>

<script type="text/javascript" charset="utf-8">



    $(function () {
        //导航切换
        $(".menuson li").click(function () {

            $(".menuson li.active").removeClass("active")
            $(this).addClass("active");

            
            //创建node对象
            var node = new Object();

            //node.attributes仍然是对象
            node.attributes = new Object();
            node.text = $(this).find("a").text();
            node.attributes.url = $(this).find("a").attr("rel");
            
            //node.iconCls = "icon-add";

            //addTab(node);
            addTab($(this).find("a").text(), $(this).find("a").attr("rel"));
            //console.info(node);
        });



    });
</script>
<style type="text/css">
    


.panel-title {
    color: #0e2d5f;
    font-size: 13px;
    font-weight: bold;
    height: 20px;
    line-height: 20px;    
}
.nav-accorad .panel-title
{
     font-size: 14px;
     font-weight: bold;
 }
</style>

<div id="myaccrdion" border="false" fit="true" class="easyui-accordion" headerCls="nav-accorad" style="background:#f0f9fd;">
    <%foreach(String name in map_menu.Keys){%>
        <div title="<%=name%>" data-options="iconCls:'<%=map_one[name].icon %>'" style="overflow:hidden;padding-top:10px;background:#f0f9fd;" >
            <ul class="menuson">
                <%if (map_menu.ContainsKey(name)) { %>
                        <%foreach (Maticsoft.Model.T_Menu t_menu in map_menu[name]) { %>
                        <li><cite></cite><a rel="<%=t_menu.url %>"><%=t_menu.name %></a><i></i></li>
                        <%}%>
                    <%}%>
            </ul>
        </div>
    <%}%>
</div>