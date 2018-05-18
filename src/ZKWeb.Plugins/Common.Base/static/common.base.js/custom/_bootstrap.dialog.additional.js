﻿/*
	额外的显示模态框函数
*/

/* 显示包含远程页面的模态框 */
BootstrapDialog.showRemote = function (title, url, extendParameters) {
	// 创建页面内容的容器
	var $contents = $("<div class='remote-contents'>").attr("href", url);
	$contents.on("reload", function () {
		var url = $contents.attr("href");
		$contents.empty().addClass("loading").load(url, function () {
			// 触发动态加载事件
			$.dynamicLoaded($contents);
			$contents.removeClass("loading");
		});
	});
	// 弹出模态框
	BootstrapDialog.show($.extend({
		type: "type-primary",
		size: "size-wide",
        //size: "size-fullscreen",
		title: title,
		message: $contents
	}, extendParameters || {}));
	// 等待模态框加到DOM后再读取远程数据
	setTimeout(function () { $contents.trigger("reload"); }, 1);
};

/* 防止误关模态框 */
//BootstrapDialog.defaultOptions.closeByBackdrop = false;
//BootstrapDialog.defaultOptions.closeByKeyboard = false;

/* 点击模块框外部关闭窗口，按ESC关闭窗口*/
BootstrapDialog.defaultOptions.closeByBackdrop = true;
BootstrapDialog.defaultOptions.closeByKeyboard = true;

/* 设置关闭图标 */
BootstrapDialog.defaultOptions.closeIcon = "<i class='fa fa-remove'></i>";
