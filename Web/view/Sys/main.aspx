<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Maticsoft.Web.view.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=sysName%></title>
    <script src="../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../js/jsUtil.js" type="text/javascript"></script>
    
    <link href="../../css/base.css" rel="stylesheet" type="text/css" />
    <link href="../../css/mystyle.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
         /*--------日历和天气样式-------*/
        .title {
	    height:35px;
	    line-height:35px;
	    border-bottom:1px dashed #C8C7C7;
        }
        .detail {
	    border-top: 1px dashed #C8C7C7;
	    margin-top: 2px;
	    padding: 5px;
        }
        .lm02 .icon, .lm03 .icon {
	        float:left;
	        padding-right: 11px;
	        padding-top: 11px;
        }
        .lm02 h2, .lm03 h2 {
	        float:left;
	        font-size:14px;
        }
    </style>


    <script type="text/javascript">
        var menuPanel;
        $(function () {
            //刷新菜单
            menuPanel = $('#menuPanel').panel({
                tools: [{
                    iconCls: 'icon-reload',
                    handler: function () {
                        var accrdion = $("#myaccrdion");

                        //获取菜单信息
                        $.ajax({
                            url: 'MenuMng/GetMenuInfo.ashx',
                            success: function (r) {
                                if (r) {
                                    //console.info(r);
                                    accrdion.html(r);
                                } else {
                                    return;
                                }
                            }
                        });


                    }
                }]
            });
        });

        

        

        
    </script>
</head>
<body id="layout" class="easyui-layout" data-options="fit:true" >   
    <div data-options="region:'north',href:'layout/north.aspx',split:false" style="height:90px;overflow:hidden;""></div>   
    <div data-options="region:'west',href:'layout/west.aspx',split:false,title:'导航菜单'" style="width: 190px; height: 100%;
        padding-top: 2px; background:#f0f9fd; overflow:hidden;"></div>   
    <div data-options="region:'center',href:'layout/center.aspx'" style="background:#eee;overflow:hidden;"></div>   
    <div data-options="region:'south',href:'layout/south.aspx',split:false" style="height: 25px;"></div>   
    
    
    
</body>
</html>
