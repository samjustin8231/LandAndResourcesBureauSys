<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="center.aspx.cs" Inherits="Maticsoft.Web.View.layout.center" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<style type="text/css">
    /*--------公告栏样式-------*/
    td li
    {
        height:20px;
        background: transparent url("../../Images/newslibg.GIF") no-repeat scroll left center !important;
        padding:5px;
        padding-left: 16px;
        font-size:10pt;
        border-bottom: 1px dotted #EEE;
    }
    
    .datagrid-header tr{height:35px !important;}
    
    span{display: inline;}
    td select,td input{font-size:8pt;font-family: "微软雅黑";}
    
   
</style>

<script type="text/javascript" charset="utf-8">
    var centerTabs;
    var tabsMenu;
    $(function () {
        
        tabsMenu = $('#tabsMenu').menu({
            onClick: function (item) {
                var curTabTitle = $(this).data('tabTitle');
                var type = $(item.target).attr('type');

                if (type === 'refresh') {
                    refreshTab(curTabTitle);
                    return;
                }

                if (type === 'close') {
                    var t = $('#centerTabs').tabs('getTab', curTabTitle);
                    if (t.panel('options').closable) {
                        $('#centerTabs').tabs('close', curTabTitle);
                    }
                    return;
                }

                var allTabs = $('#centerTabs').tabs('tabs');
                var closeTabsTitle = [];

                $.each(allTabs, function () {
                    var opt = $(this).panel('options');
                    if (opt.closable && opt.title != curTabTitle && type === 'closeOther') {
                        closeTabsTitle.push(opt.title);
                    } else if (opt.closable && type === 'closeAll') {
                        closeTabsTitle.push(opt.title);
                    }
                });

                for (var i = 0; i < closeTabsTitle.length; i++) {
                    $('#centerTabs').tabs('close', closeTabsTitle[i]);
                }
            }
        });


        $('#centerTabs').tabs({
            fit: true,
            border: false,
            onContextMenu: function (e, title) {
                e.preventDefault();
                tabsMenu.menu('show', {
                    left: e.pageX,
                    top: e.pageY
                }).data('tabTitle', title);
            }
        });


    });

    

    

    function refreshTab(title) {
        var tab = $('#centerTabs').tabs('getTab', title);
        $('#centerTabs').tabs('update', {
            tab: tab,
            options: tab.panel('options')
        });
    }

    function addNoticeTab(id) {

        addTab("公告内容", "NoticeMng/notice.aspx?id_notice=" + id);
    }

    function formatterTitleF(value, row, index) {

        return "<li style='margin-left:10px;'>" + value + "</li>";
    }

    function formatterTimeF(value, row, index) {

        return "<span style='margin-right:20px;'>" + value + "</span>";
    }

    function onClickRowF(index, row) {
        addNoticeTab(row.id);
    }
</script>

<div id="centerTabs">
    <div title="首页" class="easyui-layout" style="width:100%;height:100%;background:#fafafa;fit:true;">   
        
        <div data-options="region:'center',split:false" border="false">
            <table id="tt" title="公告栏" class="easyui-datagrid" border="false" fit="true" fitcolumns="true" style="width: auto; height: 500px;"        
                idfield="id" pagination="true" data-options="singleSelect:'true',url:'/View/Sys/NoticeMng/NoticeHandler.ashx?method=query&isDeleted=0',pageSize:20, pageList:[10,20,30,40,50],method:'get',onClickRow:onClickRowF"> <%--striped="true"--%>
                <%-- 表格标题--%>
                <thead>
                    <tr>
                        <th data-options="field:'title',width:400,align:'left',sortable:'true',formatter:formatterTitleF">标题</th>
                        <th data-options="field:'create_time',width:220,align:'right',sortable:'true',formatter:formatterTimeF">发布时间</th>   
                    </tr>
                </thead>
                    <%--表格内容--%>
            </table> 
        </div>

        <div data-options="region:'east',split:false,title:'辅助工具'" border="true" style="width: 280px; height: 100%;
            padding-top: 2px; background-color: #fff; overflow: auto" >
            <div style="float:right;width:250px;">
                <div class="lm02">
                    <div class="title"><img class="icon" src="../../images/members/dataicon.jpg" />
                    <h2>日历</h2>
                    </div>
                    <div class="detail"><div id="cc" class="easyui-calendar" style="width:212px;height:218px;"></div></div>
                </div>
                <div class="lm03">
                    <div class="title"><img style="padding-right:5px;" class="icon" src="../../images/members/weaicon.jpg" />
                    <h2>天气</h2>
                    </div>
                    <div class="detail"><iframe src="http://www.thinkpage.cn/weather/weather.aspx?uid=U556564267&cid=CHJS020000&l=zh-CHS&p=SMART&a=1&u=C&s=1&m=2&x=1&d=3&fc=&bgc=&bc=&ti=0&in=0&li=&ct=iframe" frameborder="0" scrolling="no" width="212" height="218" allowTransparency="true"></iframe></div>
                </div>
            </div>
        </div>
    </div>

</div>

<div id="tabsMenu" style="width: 120px;display:none;">
	<div type="refresh">刷新</div>
	<div class="menu-sep"></div>
	<div type="close">关闭</div>
	<div type="closeOther">关闭其他</div>
	<div type="closeAll">关闭所有</div>
</div>