
var cn = $.extend({},cn);  /*定义全局对象*/
/*
//获取根路径
cn.getRootPath = function() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}

cn.serializeObject = function (form) {
    var o = {};
    $.each(form.serializeArray(), function (index) {
        if (o[this['name']]) {
            o[this['name']] = o[this['name']] + "," + this['value'];
        } else {
            o[this['name']] = this['value'];
        }
    });
    return o;
};  
*/


//添加tab页面
/*
node.attributes.url
node.text
node.iconCls
*/
function addTab(subtitle, url) {

    var jq = top.jQuery;
    var flag = 0;
    if (jq("#centerTabs").tabs('exists', subtitle)) {
        if (subtitle == "公告内容") {
            flag = 1
        } else {
            jq("#centerTabs").tabs('select', subtitle);
        }
        
    } else {
        flag = 2;
    }

    if (flag != 0) {
        if (flag == 1) {
            jq("#centerTabs").tabs('close', subtitle)
        }

        if (url && url.length > 0) {
            if (url.indexOf('!druid.action') == -1) {/*数据源监控页面不需要开启等待提示*/
                $.messager.progress({
                    text: '页面加载中....',
                    interval: 100
                });
                window.setTimeout(function () {
                    try {
                        $.messager.progress('close');
                    } catch (e) {
                    }
                }, 500);
            }
            jq('#centerTabs').tabs('add', {
                title: subtitle,
                closable: true,
                //iconCls: node.iconCls,
                content: '<iframe src="' + url + '" frameborder="0" style="border:0;width:100%;height:99.4%;"></iframe>',
                tools: [{
                    iconCls: 'icon-mini-refresh',
                    handler: function () {
                        refreshTab(subtitle);
                    }
                }]
            });
        } 
    }
}

function closeTab(subtitle) {

    var jq = top.jQuery;
    var flag = 0;
    if (jq("#centerTabs").tabs('exists', subtitle)) {
        jq("#centerTabs").tabs('close', subtitle)
    }
}

function refreshTab(title) {
    var jq = top.jQuery;
    var tab = jq('#centerTabs').tabs('getTab', title);
    jq('#centerTabs').tabs('update', {
        tab: tab,
        options: tab.panel('options')
    });
}

//获取根路径
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}

//jquery 将form中的元素序列化成对象
function serializeObject(form) {
    var o = {};
    $.each(form.serializeArray(), function (index) {
        if (o[this['name']]) {
            o[this['name']] = o[this['name']] + "," + this['value'];
        } else {
            o[this['name']] = this['value'];
        }
    });
    return o;
};

/**
* @author sam
* 增加formatString功能
* 使用方法：myFormatString('字符串{0}字符串{1}字符串','第一个变量','第二个变量');
* @returns 格式化后的字符串
*/
function myFormatString(str) {
    for (var i = 0; i < arguments.length - 1; i++) {
        str = str.replace("{" + i + "}", arguments[i + 1]);
    }
    return str;
};

Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, // 月
        "d+": this.getDate(), // 天
        "h+": this.getHours(), // 时
        "m+": this.getMinutes(), // 分
        "s+": this.getSeconds(), // 秒
        "q+": Math.floor((this.getMonth() + 3) / 3), // 刻
        "S": this.getMilliseconds() //毫秒
        // millisecond
    }
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "")
				.substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k]
					: ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

function formatDatebox(value) {
    if (value == null || value == '') {
        return '';
    }
    var dt;
    if (value instanceof Date) {
        dt = value;
    } else {

        dt = new Date(value);

    }

    return dt.format("yyyy-MM-dd"); //扩展的Date的format方法(上述插件实现)
}

function getMaxIdByTableName(tableName) {
    var maxId = 300;
    $.ajax({
        url: '',
        success: function (r) { 
            
        }
    });
    console.info(maxId);
    return maxId;
}


/**
 * @author sam
 * 
 * @requires jQuery,EasyUI
 * 
 * 扩展datagrid的editor
 * 
 * 增加带复选框的下拉树
 * 
 * 增加日期时间组件editor
 * 
 * 增加多选combobox组件
 */
$.extend($.fn.datagrid.defaults.editors, {
	combocheckboxtree : {
		init : function(container, options) {
			var editor = $('<input />').appendTo(container);
			options.multiple = true;
			editor.combotree(options);
			return editor;
		},
		destroy : function(target) {
			$(target).combotree('destroy');
		},
		getValue : function(target) {
			return $(target).combotree('getValues').join(',');
		},
		setValue : function(target, value) {
			$(target).combotree('setValues', sy.getList(value));
		},
		resize : function(target, width) {
			$(target).combotree('resize', width);
		}
	},
	datetimebox : {
		init : function(container, options) {
			var editor = $('<input />').appendTo(container);
			editor.datetimebox(options);
			return editor;
		},
		destroy : function(target) {
			$(target).datetimebox('destroy');
		},
		getValue : function(target) {
			return $(target).datetimebox('getValue');
		},
		setValue : function(target, value) {
			$(target).datetimebox('setValue', value);
		},
		resize : function(target, width) {
			$(target).datetimebox('resize', width);
		}
	},
	multiplecombobox : {
		init : function(container, options) {
			var editor = $('<input />').appendTo(container);
			options.multiple = true;
			editor.combobox(options);
			return editor;
		},
		destroy : function(target) {
			$(target).combobox('destroy');
		},
		getValue : function(target) {
			return $(target).combobox('getValues').join(',');
		},
		setValue : function(target, value) {
			$(target).combobox('setValues', sy.getList(value));
		},
		resize : function(target, width) {
			$(target).combobox('resize', width);
		}
	}
});

/**
* @author sam
* 
* @requires jQuery,EasyUI
* 
* 扩展datagrid，添加动态增加或删除Editor的方法
* 
* 例子如下，第二个参数可以是数组
* 
* datagrid.datagrid('removeEditor', 'cpwd');
* 
* datagrid.datagrid('addEditor', [ { field : 'ccreatedatetime', editor : { type : 'datetimebox', options : { editable : false } } }, { field : 'cmodifydatetime', editor : { type : 'datetimebox', options : { editable : false } } } ]);
* 
*/
$.extend($.fn.datagrid.methods, {
    addEditor: function (jq, param) {
        if (param instanceof Array) {
            $.each(param, function (index, item) {
                var e = $(jq).datagrid('getColumnOption', item.field);
                e.editor = item.editor;
            });
        } else {
            var e = $(jq).datagrid('getColumnOption', param.field);
            e.editor = param.editor;
        }
    },
    removeEditor: function (jq, param) {
        if (param instanceof Array) {
            $.each(param, function (index, item) {
                var e = $(jq).datagrid('getColumnOption', item);
                e.editor = {};
            });
        } else {
            var e = $(jq).datagrid('getColumnOption', param);
            e.editor = {};
        }
    }
});

function messagerShow(options) {
    return $.messager.show(options);
};

function messagerAlert(title, msg, icon, fn) {
    return $.messager.alert(title, msg, icon, fn);
};

/**
* @author sam
* 
* 接收一个以逗号分割的字符串，返回List，list里每一项都是一个字符串
* 
* @returns list
*/
function getList(value) {
    if (value != undefined && value != '') {
        var values = [];
        var t = value.split(',');
        for (var i = 0; i < t.length; i++) {
            values.push('' + t[i]); /* 避免他将ID当成数字 */
        }
        return values;
    } else {
        return [];
    }
};

/**
* @author sam
* 
* @requires jQuery,EasyUI
* 
* @param options
*/
function dialog (options) {
    var opts = $.extend({
        modal: true,
        onClose: function () {
            $(this).dialog('destroy');
        }
    }, options);
    return $('<div/>').dialog(opts);
};

function ChangeToTable(rows) {
    var tableString = "<table border=1 cellspacing=0>";
    //获取列号
    //拼接title
    tableString += '\n<tr>';
    for (var key in rows[0]) {//key  value
        tableString += '\n<td style="vertical-align:middle; text-align:center;">';
        tableString += '\n' + key;
        tableString += '\n</td>';
        count++;
    }
    tableString += '\n</tr>';

    //console.info(rows[0]);
    for (var i = 0; i < rows.length; ++i) {
        tableString += '\n<tr>';
        var count = 0;
        for (var key in rows[i]) {//key  value

            tableString += '\n<td style="vertical-align:middle; text-align:center;">';


            tableString += '\n' + rows[i][key];
            tableString += '\n</td>';
            count++;
        }

        tableString += '\n</tr>';
    }
    tableString += '\n</table>';
    console.info(tableString);
    return tableString;
}
/*
 * 使用方法: 
 * 开启:MaskUtil.mask(); 
 * 关闭:MaskUtil.unmask(); 
 *  
 * MaskUtil.mask('其它提示文字...'); 
 */  
var MaskUtil = (function(){  
      
    var $mask,$maskMsg;  
      
    var defMsg = '正在处理，请稍待。。。';  
      
    function init(){  
        if(!$mask){  
            $mask = $("<div class=\"datagrid-mask mymask\"></div>").appendTo("body");  
        }  
        if(!$maskMsg){  
            $maskMsg = $("<div class=\"datagrid-mask-msg mymask\">"+defMsg+"</div>")  
                .appendTo("body").css({'font-size':'12px'});  
        }  
          
        $mask.css({width:"100%",height:$(document).height()});  
          
        var scrollTop = $(document.body).scrollTop();  
          
        $maskMsg.css({  
            left:( $(document.body).outerWidth(true) - 190 ) / 2  
            ,top:( ($(window).height() - 45) / 2 ) + scrollTop  
        });   
                  
    }  
      
    return {  
        mask:function(msg){  
            init();  
            $mask.show();  
            $maskMsg.html(msg||defMsg).show();  
        }  
        ,unmask:function(){  
            $mask.hide();  
            $maskMsg.hide();  
        }  
    }

} ());

/*两个密码是否相同*/
$.extend($.fn.validatebox.defaults.rules, {
    /*必须和某个字段相等*/
    equalTo: { validator: function (value, param) { return $(param[0]).val() == value; }, message: '字段不匹配' }
});

/**
* EasyUI DataGrid根据字段动态合并单元格
* 参数 tableID 要合并table的id
* 参数 colList 要合并的列,用逗号分隔(例如："name,department,office");
*/
function mergeCellsByField(tableID, colList) {
    var ColArray = colList.split(",");
    var tTable = $("#" + tableID);
    var TableRowCnts = tTable.datagrid("getRows").length;
    var tmpA;
    var tmpB;
    var PerTxt = "";
    var CurTxt = "";
    var alertStr = "";
    for (j = ColArray.length - 1; j >= 0; j--) {
        PerTxt = "";
        tmpA = 1;
        tmpB = 0;

        for (i = 0; i <= TableRowCnts; i++) {
            if (i == TableRowCnts) {
                CurTxt = "";
            }
            else {
                CurTxt = tTable.datagrid("getRows")[i][ColArray[j]];
            }
            if (PerTxt == CurTxt) {
                tmpA += 1;
            }
            else {
                tmpB += tmpA;

                tTable.datagrid("mergeCells", {
                    index: i - tmpA,
                    field: ColArray[j], //合并字段
                    rowspan: tmpA,
                    colspan: null
                });
                tTable.datagrid("mergeCells", { //根据ColArray[j]进行合并
                    index: i - tmpA,
                    field: "Ideparture",
                    rowspan: tmpA,
                    colspan: null
                });

                tmpA = 1;
            }
            PerTxt = CurTxt;
        }
    }
}