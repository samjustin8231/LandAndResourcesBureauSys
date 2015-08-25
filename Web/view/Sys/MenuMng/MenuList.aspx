<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="Maticsoft.Web.View.MenuMng.MenuList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜单列表</title>

    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet"
        type="text/css" />

    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        var dlg;  //添加/修改 弹出的对话框
        var fm; //添加修改中的form

        var treegrid;   //
        var editRow = undefined;
        var iconData;

        iconData = [{
            value: '',
            text: '默认'
        }, {
            value: 'icon-account',
            text: 'icon-account'
        }, {
            value: 'icon-balance',
            text: 'icon-balance'
        }, {
            value: 'icon-plan',
            text: 'icon-plan'
        }, {
            value: 'icon-batch',
            text: 'icon-batch'
        }, {
            value: 'icon-compasate',
            text: 'icon-compasate'
        }, {
            value: 'icon-type',
            text: 'icon-type'
        }, {
            value: 'icon-farm',
            text: 'icon-farm'
        }, {
            value: 'icon-man',
            text: 'icon-man'
        }, {
            value: 'icon-large-clipart',
            text: 'icon-large-clipart'
        }, {
            value: 'icon-large-chart',
            text: 'icon-large-chart'
        }, {
            value: 'icon-large-smartart',
            text: 'icon-large-smartart'
        }, {
            value: 'icon-setting',
            text: 'icon-setting'
        }, {
            value: 'icon-sendmoney',
            text: 'icon-sendmoney'
        }, {
            value: 'icon-cut',
            text: 'icon-cut'
        }, {
            value: 'icon-ok',
            text: 'icon-ok'
        }, {
            value: 'icon-no',
            text: 'icon-no'
        }, {
            value: 'icon-notice',
            text: 'icon-notice'
        }, {
            value: 'icon-reload',
            text: 'icon-reload'
        }, {
            value: 'icon-search',
            text: 'icon-search'
        }, {
            value: 'icon-print',
            text: 'icon-print'
        }, {
            value: 'icon-help',
            text: 'icon-help'
        }, {
            value: 'icon-undo',
            text: 'icon-undo'
        }, {
            value: 'icon-redo',
            text: 'icon-redo'
        }, {
            value: 'icon-back',
            text: 'icon-back'
        }, {
            value: 'icon-sum',
            text: 'icon-sum'
        }, {
            value: 'icon-tip',
            text: 'icon-tip'
        }];

        $(function () {

            $("#icon").combobox({
                valueField: 'value',
                textField: 'text',
                width:150,
                data: iconData,
                formatter: function (v) {
                    return myFormatString('<span class="{0}" style="display:inline-block;vertical-align:middle;width:16px;height:16px;"></span>&nbsp;&nbsp;{1}', v.value, v.text);
                }
            });
        });

        //添加
        function add() {
            //清空内容
            fm = $('#fm').form('clear');


            dlg = $('#dlg').dialog({
                title: '添加菜单',
                modal: true,
                onLoad: function () {

                }
            }).dialog("open");

            document.getElementById("method").value = "add";
        }

        //修改
        function edit() {
            //先获取选择行
            var rows = $('#tt').treegrid("getSelections");
            //如果只选择了一行则可以进行修改，否则不操作
            if (rows.length == 1) {
                var row = rows[0];
                //获取要修改的字段
                $('#name').textbox("setValue", row.text);

                dlg = $('#dlg').dialog({
                    title: '修改菜单信息',
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
            if (method == "add") {
                //提交之前验证
                if (!$("#fm").form('validate')) {
                    return false;
                }

                $.getJSON('MenuHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').treegrid('reload'); // reload the user data
                        $("#tt").treegrid('unselectAll');

                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });

                        //刷新菜单
                        refreshWest();
                    } else {
                        $.messager.show({
                            title: 'Error',
                            msg: result.msg
                        });
                    }
                });
            }
            else//修改
            {
                var row = $('#tt').treegrid('getSelected');
                if (row) {
                    //获取要修改的字段
                    var id = row.id;
                }

                //提交之前验证
                if (!$("#fm").form('validate')) {
                    return false;
                }

                $.getJSON('MenuHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').treegrid('reload'); // reload the user data
                        $("#tt").treegrid('unselectAll');

                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });

                        //刷新菜单
                        refreshWest();
                    } else {
                        $.messager.show({
                            title: 'Error',
                            msg: result.msg
                        });
                    }
                });
            }
        }

        //删除
        function del() {
            var method = document.getElementById("method").value = "delete";
            var rows = $('#tt').treegrid('getSelections');
            console.info(rows);
            console.info(rows.length);
            if (rows == null || rows.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return;
            }

            //取消ajax的异步
            $.ajaxSettings.async = false;

            var flag = false;
            for (var i = 0; i < rows.length; i++) {
                $.getJSON('MenuHandler.ashx?method=IsUsed', { id: rows[i].id }, function (result) {
                    if (result.flag) {
                        flag = true;
                    }

                });
            }

            if (flag) {
                alert("该菜单中已经添加了权限，请先删除！"); return;
            }

            if (rows) {
                $.messager.confirm('提示', '你确定要删除【' + rows.length + '】条记录吗？', function (r) {
                    if (r) {
                        var ids = [];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);
                        }

                        $.ajax({
                            url: 'MenuHandler.ashx?method=delete',
                            data: {
                                ids: ids.join(",")
                            },
                            method: 'post',
                            dataType: 'json',
                            success: function (result) {
                                if (result.flag) {
                                    $('#dlg').dialog('close'); 	// close the dialog
                                    $('#tt').treegrid('reload'); // reload the user data
                                    $.messager.show({
                                        title: '提示',
                                        msg: result.msg
                                    });

                                    //刷新菜单
                                    refreshWest();
                                } else {
                                    $.messager.show({
                                        title: 'Error',
                                        msg: result.msg
                                    });
                                }

                            }
                        });
                    }
                })
            }
        }

        //获取参数     
        function getQueryParams(queryParams) {

            /*
            var name = $("#fm_search").find("input[name='name']").val();
            console.info(name);
            queryParams.name = name;
            */

            return queryParams;

        }

        //增加查询参数，重新加载表格
        function reloadgrid() {
            //查询参数直接添加在queryParams中    
            var queryParams = $('#tt').treegrid('options').queryParams;
            getQueryParams(queryParams);
            $('#tt').treegrid('options').queryParams = queryParams;
            $("#tt").treegrid('reload');
            $("#tt").treegrid('unselectAll');
        }

        //刷新west页面
        function refreshWest() {
            //刷新左侧导航菜单，暂时只能做成刷新整个页面，待完善
            window.parent.location.reload();
        }

        //刷新
        function reload() {
            $('#tt').treegrid('reload'); // reload the user data
            $('#tt').treegrid('unselectAll');

        }

        //取消选中
        function unselectall() {
            $('#tt').treegrid('unselectAll');
        }


        function formatterStateF(value, rowData, rowIndex) {
            if (rowData.isDeleted == "1") {
                return "<a class='isDeleted'  href='#' onclick='lock(" + rowData.id + ")'></a>";
            } else {
                return "<a class='isNotDeleted'  href='#' onclick='lock(" + rowData.id + ")'></a>";
            }
        }

        function lock(id) {
            $("#tt").treegrid("select",id)
            var row = $("#tt").treegrid("getSelected");
            $.getJSON('MenuHandler.ashx?method=Lock&id=' + row.id, function (result) {
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
                reload();
            });
        }

        //加载dagargid中图片
        function loadLinkButton(data) {
            $(".isDeleted").linkbutton({ text: '关闭', plain: true, iconCls: 'icon-lock' });
            $(".isNotDeleted").linkbutton({ text: '启用', plain: true, iconCls: 'icon-open' });
        }

        //清空
        function clearData() {
            $("#tb").find("input").val("");

            //重新加载数据
            reloadgrid();
        }

        function refreshMenu() {
            $("#myaccrdion", window.parent.document).html("");
            //$("#myaccrdion", window.parent.document).html($("#accrdion_hidden"));


        }

        function onRowContextMenuF(e, row) {
            //阻止浏览器响应
            e.preventDefault();
            //console.info(rowData);
            $('#tt').treegrid("unselectAll");
            $('#tt').treegrid("select", row.id);
            $("#menu").menu("show", {
                left: e.pageX,
                top: e.pageY
            });
        }
    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="菜单列表" fit="true" border="false" class="easyui-treegrid" style="width: auto; height: 500px;" pagination="true" 
        data-options="ctrlSelect:true,onDblClickRow:edit,iconCls:'icon-tip',idField:'id',treeField:'text',rownumbers:true,url:'MenuHandler.ashx?method=query',pagination : false,method:'get',toolbar:'#tb',striped:true,nowrap:false,onLoadSuccess:loadLinkButton,onContextMenu:onRowContextMenuF" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'text',width:100,align:'center',sortable:'true'">名称</th>
                <th data-options="field:'url',width:120,align:'center',sortable:'true'">路径</th>
                <th data-options="field:'icon',width:120,align:'center',sortable:'true'">图标</th>   
                <th data-options="field:'_sort',width:120,align:'center',sortable:'true'">排序</th>   
                <th data-options="field:'isDeleted',width:50,align:'center',sortable:'true',formatter:formatterStateF">状态</th>  
                <th data-options="field:'description',width:120,align:'center',sortable:'true'">描述</th>   
            </tr>
        </thead>
         <%--表格内容--%>
    </table>
    <%--功能区--%>
    <div id="tb" style="padding: 5px; height: auto">
        <%-- 包括添加、修改、删除、刷新、全部选中、取消全部选中 --%>
        <div style="margin-bottom: 5px">
            <a href="javascript:void(0)" onclick="add()" title="添加" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-add',plain:true"></a>
            <a href="javascript:void(0)" onclick="edit() " title="修改" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-edit',plain:true"></a>
            <a href="javascript:void(0)" onclick="del()" title="删除" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-remove',plain:true"></a>
            <a href="javascript:void(0)" onclick="reload()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
            <a href="javascript:void(0)" onclick="unselectall()" title="取消选中" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-undo',plain:true"></a>

            <!--
            <a href="javascript:void(0)" onclick="refreshMenu()" title="刷新" class="easyui-linkbutton easyui-tooltip" data-options="iconCls:'icon-reload',plain:true"></a>
            -->
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons',onOpen:function(){$('#pid').combotree('reload','../MenuMng/MenuHandler.ashx?method=getTreeList'); }"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">菜单信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>名称：</label>
                <input id="method" name="method" type="hidden" />  
                <input id="id" name="id" type="hidden" />  
                <input id="name" name="name" class="easyui-textbox" data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>路径：</label>
                <input id="url" name="url"/>
            </div>
            <div class="fitem">
                <label>上级菜单：</label>
                <select id="pid" name="pid" class="easyui-combotree" style="width:150px;" data-options="url:'../MenuMng/MenuHandler.ashx?method=getTreeList',lines: true"></select>
            </div>
            <div class="fitem">
                <label>排序：</label>
                <input name="_sort" class="easyui-numberspinner" style="width:150px;" required="required" data-options="min:1,max:100,editable:false">
            </div>
            <div class="fitem">
                <label>图标：</label>
                <input id="icon" name="icon"/>
            </div>
            <div class="fitem">
                <label>描述：</label>
                <textarea id="description" name="description" style="width:150px;"></textarea>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close')">关闭</a>
    </div>

    <div id="menu" class="easyui-menu" style="width: 120px; display: none;">
        <div onclick="edit();" iconcls="icon-edit">
            编辑</div>
        <div onclick="del();" iconcls="icon-remove">
            删除</div>
    </div>
</body>
</html>