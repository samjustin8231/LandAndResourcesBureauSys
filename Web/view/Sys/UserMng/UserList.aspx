<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Maticsoft.Web.View.Sys.UserMng.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员列表</title>

    <script src="../../../js/jquery-easyui-1.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="../../../js/jquery-easyui-1.4.1/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../../../js/jquery-easyui-1.4.1/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui-1.4.1/themes/default/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../css/base.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/jquery-easyui-1.4.1/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../../../js/jsUtil.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">

        $(function () {
            $('#search_name').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    reloadgrid();
                }
            });
        });

        //获取参数     
        function getQueryParams(queryParams) {

            var name = $("#fm_search").find("input[name='name']").val();
            var startTime = $("#startTime").datebox("getValue");
            var endTime = $("#endTime").datebox("getValue");
            var isDeleted = $("#isDeleted").combobox("getValue");

            queryParams.name = name;
            queryParams.startTime = startTime;
            queryParams.endTime = endTime;
            queryParams.isDeleted = isDeleted;
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

        //添加管理员
        function add() {
            //清空内容
            $('#fm').form('clear');

            //加载上级用户信息
            //loadPidList();

            //给用户角色设置默认值
            $("#id_role").combobox("setValue", "5");  //一般用户 角色  id:5

            $('#dlg').dialog({
                title: '添加会员',
                modal: true,
                onLoad: function () {
                    //加载上级用户信息


                }
            }).dialog("open");

            $("#name").validatebox({
                validType: "remote['UserHandler.ashx?method=checkName','name']"
            });

            document.getElementById("method").value = "add";
        }

        //修改管理员
        function edit() {
            //先获取选择行
            var rows = $('#tt').datagrid("getSelections");

            //如果只选择了一行则可以进行修改，否则不操作
            console.info(rows.length);
            if (rows.length == 1) {
                //加载上级用户信息
                //loadPidList(rows[0].id);

                //给用户角色设置默认值
                $("#id_role").combobox("setValue", "5");

                var row = rows[0];
                //获取要修改的字段
                $('#name').val(row.name);
                $('#age').val(row.age);
                $('#score').val(row.score);
                $('#telephone').val(row.telephone);
                $('#address').val(row.address);
                $('#remarks').val(row.remarks);
                //$('#pid').combobox('setValue', row.pid);

                $('#dlg').dialog({
                    title: '修改会员',

                    modal: true,
                    onLoad: function () {

                    }
                }).dialog("open");

                document.getElementById("method").value = "modify";

                $('#fm').form('load', row);

                console.info($("#id").val());
                $("#name").validatebox({
                    validType: "remote['UserHandler.ashx?method=checkName&id=" + $("#id").val() + "','name']"
                });
            } else {
                $.messager.alert("提示", "请选择要一行修改！", "info");
            }
        }

        //加载上级列表
        function loadPidList(id) {
            var url = "";
            if (id != undefined && id != "") {
                url = "UserHandler.ashx?method=getTreeList&id=" + id;
            } else {
                url = "UserHandler.ashx?method=getTreeList";
            }

            $("#pid").combobox({
                url: url,
                valueField: 'id',
                textField: 'name',
                onSelect: function (record) {

                }
            });
        }

        //删除用户
        function del() {
            var method = document.getElementById("method").value = "delete";
            var rows = $('#tt').datagrid('getSelections');
            if (rows == null || rows.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return;
            }
            if (rows) {
                $.messager.confirm('提示', '你确定要删除【' + rows.length + '】条记录吗？', function (r) {
                    if (r) {
                        var ids = [];
                        for (var i = 0; i < rows.length; i++) {
                            ids.push(rows[i].id);
                        }

                        $.ajax({
                            url: 'UserHandler.ashx?method=delete',
                            data: {
                                ids: ids.join(",")
                            },
                            method: 'post',
                            dataType: 'json',
                            success: function (result) {
                                if (result.flag) {
                                    $('#dlg').dialog('close'); 	// close the dialog
                                    $('#tt').datagrid('reload'); // reload the user data

                                    $.messager.show({
                                        title: '提示',
                                        msg: result.msg
                                    });
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


        //保存信息
        function save() {
            var method = $("#method").val();
            //提交之前验证
            if (!$("#fm").form('validate')) {
                return false;
            }
            if (method == "add") {
                $.getJSON('UserHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').datagrid('reload'); // reload the user data
                        $("#tt").datagrid('unselectAll');

                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });
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
                var row = $('#tt').datagrid('getSelected');
                if (row) {
                    //获取要修改的字段
                    var id = row.id;
                }

                $.getJSON('UserHandler.ashx', $("#fm").serialize(), function (result) {
                    if (result.flag) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tt').datagrid('reload'); // reload the user data
                        $("#tt").datagrid('unselectAll');

                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });
                    } else {
                        $.messager.show({
                            title: 'Error',
                            msg: result.msg
                        });
                    }
                });
            }
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
            $("#fm_search").form("clear");
            $("#isDeleted").combobox("setValue",-1);

            //重新加载数据
            reloadgrid();
        }

        function formatterPasswordF(value, row, index) {
            return "******";
        }

        //查看个人信息
        function formatterOptF(value, rowData, rowIndex) {
            return "<a class='initpassword'  href='#' onclick='initpassword(" + rowIndex + ")'></a>";
        }

        function formatterStateF(value, rowData, rowIndex) {
            if (rowData.isDeleted == "1") {
                return "<a class='isDeleted'  href='#' onclick='lock(" + rowIndex + ")'></a>";
            } else {
                return "<a class='isNotDeleted'  href='#' onclick='lock(" + rowIndex + ")'></a>";
            }
        }

        function lock(rowIndex) {
            var index;
            if (rowIndex == undefined) {
                index = $("#tt").datagrid("getRowIndex", $("#tt").datagrid("getSelected"));
            } else {
                index = rowIndex;
            }
            var rows = $("#tt").datagrid("getRows");
            var row = rows[index];

            if (row.name =="admin") {
                alert("管理员账户不允许禁用！"); return;
            }

            $.getJSON('UserHandler.ashx?method=Lock&id=' + row.id, function (result) {
                $.messager.show({
                    title: '提示',
                    msg: result.msg
                });
                reload();
            });
        }

        //加载dagargid中图片
        function loadLinkButton(data) {
            $(".initpassword").linkbutton({ text: '初始化密码', plain: true, iconCls: 'icon-redo' });
            $(".isDeleted").linkbutton({ text: '关闭', plain: true, iconCls: 'icon-lock'});
            $(".isNotDeleted").linkbutton({ text: '启用', plain: true, iconCls: 'icon-open' });
        }

        function formatterDateF(value, rowData, rowIndex) {
            return formatDatebox(value);
        }

        function initpassword(rowIndex) {
            var index;
            if (rowIndex == undefined) {
                index = $("#tt").datagrid("getRowIndex", $("#tt").datagrid("getSelected"));
            } else {
                index = rowIndex;
            }
            var rows = $("#tt").datagrid("getRows");
            var row = rows[index];

            $.messager.confirm('提示', '你确定要将【' + row.name + '】的密码初始化为【123】吗？', function (r) {
                if (r) {
                    $.getJSON('UserHandler.ashx?method=InitPassword&id=' + row.id, function (result) {
                        $.messager.show({
                            title: '提示',
                            msg: result.msg
                        });
                    });
                }
            });
        }

        function onRowContextMenuF(e, rowIndex, rowData) {
            //阻止浏览器响应
            e.preventDefault();
            //console.info(rowData);
            $('#tt').datagrid("unselectAll");
            $('#tt').datagrid("selectRow", rowIndex);
            $("#menu").menu("show", {
                left: e.pageX,
                top: e.pageY
            });
        }

        //导出Excel数据
        function showExport() {
            var url = "UserHandler.ashx?method=query&isExport=1";

            $.getJSON(url,$("#fm_search").serialize(), function (data) {
                var tableString = ChangeToTable(data.rows);

                var f = $('<form action="/FileUpload/DownLoadFile.aspx" method="post" id="fm1"></form>');
                var i = $('<input type="hidden" id="txtContent" name="txtContent"/>');
                var l = $('<input type="hidden" id="txtName" name="txtName"/>');
                i.val(tableString);
                i.appendTo(f);
                l.val(data.fileName);
                l.appendTo(f);
                f.appendTo(document.body).submit();
                document.body.removeChild(f);
            });
        }

    </script>

</head>
<body>
    <%--表格显示区--%>
    <table id="tt" title="会员列表" class="easyui-datagrid" border="false" fit="true"        
        idfield="id" pagination="true" data-options="ctrlSelect:true,onDblClickRow:edit,iconCls:'icon-tip',rownumbers:true,url:'UserHandler.ashx?method=query',pageSize:20, pageList:[10,20,30,40,50],method:'get',onLoadSuccess:loadLinkButton,toolbar:'#tb',striped:true,onRowContextMenu:onRowContextMenuF" fitcolumns="true"> <%--striped="true"--%>
        <%-- 表格标题--%>
        <thead>
            <tr>
                <th data-options="field:'id',checkbox:true"></th>
                <th data-options="field:'name',width:100,align:'center',sortable:'true'">用户名</th>
                <th data-options="field:'password',width:100,align:'center',sortable:'true',formatter:formatterPasswordF">密码</th>
                <th data-options="field:'telephone',width:100,align:'center',sortable:'true'">手机号</th>   
                <th data-options="field:'birthday',width:100,align:'center',sortable:'true',formatter:formatterDateF">生日</th>   
                <!--<th data-options="field:'create_time',width:100,align:'center',sortable:'true'">创建时间</th> -->
                
                <th data-options="field:'remarks',width:100,align:'center',sortable:'true'">备注</th>   
                <th data-options="field:'isDeleted',width:50,align:'center',sortable:'true',formatter:formatterStateF">状态</th>  
                <th data-options="field:'opt',width:100,align:'center',sortable:'true',formatter:formatterOptF">操作</th>               
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

            
        </div>
        <%-- 查找Account信息，根据注册时间、姓名 --%>
        <div>
           
           <form id="fm_search" method="post">
            用户名: 
            <input id="search_name" name="name" class="easyui-textbox" data-options="iconCls:'icon-search',prompt:'用户名'" style="width:160px">&nbsp;&nbsp;
            时间从:  
            <input id="startTime" class ="easyui-datebox" style="width: 100px" />  
           到:  
            <input id="endTime" class="easyui-datebox" style="width: 100px" />   &nbsp;&nbsp;

            <select id="isDeleted" class="easyui-combobox" name="isDeleted" style="width:100px;">    
                <option value="-1">-- 请选择 --</option>    
                <option  value="0">启用</option>    
                <option  value="1">关闭</option>
            </select>&nbsp;&nbsp;&nbsp;&nbsp;

            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="reloadgrid()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-clear'" onclick="clearData()">清空</a>

            <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-excel'" onclick="showExport()">导出</a>
           </form>
            
        </div>
    </div>

    <%-- 弹出操作框--%>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: auto; padding: 10px 20px"
        data-options="closed:true,buttons:'#dlg-buttons'"> <%--closed="true" buttons="#dlg-buttons"--%>
        <div class="ftitle">会员信息</div>
        <form id="fm" method="post">
            <div class="fitem">
                <label>用户名：</label>
                <input id="method" name="method" type="hidden" />  
                <input id="id" name="id" type="hidden" />  

                <input id="name" name="name" class="easyui-validatebox" validtype="remote['UserHandler.ashx?method=checkName','name']" invalidMessage="用户名已存在"/>
            </div>
            <!--
            <div class="fitem">
                <label>密码：</label>
                <input id="password" name="password"  class="easyui-validatebox" validType="length[3,20]"  type="password"  data-options="required:true"/>
            </div>
            <div class="fitem">
                <label>密码确认：</label>
                <input name="repassword"  class="easyui-validatebox"  type="password" data-options="required:true" validType="equalTo['#password']" invalidMessage="两次输入密码不匹配"/>
            </div>
            -->
            <div class="fitem">
                <label>生日：</label>
                <input name="birthday"  class="easyui-datebox" editable="false"/>
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
                <label>用户角色：</label>
                <input id="id_role" class="easyui-combobox" name="id_role"  style="width:150px;"  data-options="valueField:'id',textField:'name',url:'../RoleMng/RoleHandler.ashx?method=getList'">
            </div>

            <div class="fitem">
                <label>备注:</label>
                <textarea  id="remarks" name="remarks" style="width:150px;"></textarea>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="save()">保存</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#dlg').dialog('close')">关闭</a>
    </div>
    <div id="menu" class="easyui-menu" style="width: 120px; display: none;">
        <div onclick="edit();" iconcls="icon-edit">
            编辑</div>
        <div onclick="del();" iconcls="icon-remove">
            删除</div>
        <div onclick="initpassword();" iconcls="icon-redo">
            初始化密码</div>
    </div>
</body>
</html>

