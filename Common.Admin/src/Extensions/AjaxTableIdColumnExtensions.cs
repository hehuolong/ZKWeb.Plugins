﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZKWeb.Core;
using ZKWeb.Plugins.Common.Admin.src.Model;
using ZKWeb.Plugins.Common.Base.src.Extensions;
using ZKWeb.Plugins.Common.Base.src.Model;
using ZKWeb.Plugins.Common.Base.src.TypeTraits;
using ZKWeb.Utils.Extensions;

namespace ZKWeb.Plugins.Common.Admin.src.Extensions {
	/// <summary>
	/// Ajax表格Id列的扩展函数
	/// </summary>
	public static class AjaxTableIdColumnExtensions {
		/// <summary>
		/// 添加删除相关的按钮
		/// 如果数据类型可以回收，则添加批量删除或批量恢复和永久删除
		/// 如果数据类型不可以回收，则添加批量永久删除
		/// </summary>
		/// <typeparam name="TApp">后台应用的类型</typeparam>
		/// <param name="column">Id列</param>
		/// <param name="request">搜索请求</param>
		public static void AddDeleteActionsForAdminApp<TApp>(
			this AjaxTableIdColumn column, AjaxTableSearchRequest request)
			where TApp : class, IAdminAppBuilder, new() {
			// 判断需要添加哪些操作
			var app = new TApp();
			bool addBatchDelete = false;
			bool addBatchRecover = false;
			bool addBatchDeleteForever = false;
			if (IsRecyclable.Value(app.GetDataType())) {
				var deleted = request.Conditions.GetOrDefault<bool>("Deleted");
				addBatchDelete = !deleted;
				addBatchRecover = deleted;
				addBatchDeleteForever = deleted;
			} else {
				addBatchDeleteForever = true;
			}
			// 添加批量删除
			var typeName = new T(app.TypeName);
			if (addBatchDelete) {
				column.AddConfirmActionForMultiChecked(
					new T("Batch Delete"),
					"fa fa-recycle",
					string.Format(new T("Please select {0} to delete"), typeName),
					new T("Batch Delete"),
					ScriptStrings.ConfirmMessageTemplateForMultiSelected(
						string.Format(new T("Sure to delete following {0}?"), typeName), "ToString"),
					ScriptStrings.PostConfirmedActionForMultiSelected(
						"Id", app.BatchUrl + "?action=delete"),
					new { type = "type-danger" });
			}
			// 添加批量恢复
			if (addBatchRecover) {
				column.AddConfirmActionForMultiChecked(
					new T("Batch Recover"),
					"fa fa-history",
					string.Format(new T("Please select {0} to recover"), typeName),
					new T("Batch Recover"),
					ScriptStrings.ConfirmMessageTemplateForMultiSelected(
						string.Format(new T("Sure to recover following {0}?"), typeName), "ToString"),
					ScriptStrings.PostConfirmedActionForMultiSelected(
						"Id", app.BatchUrl + "?action=recover"));
			}
			// 添加批量永久删除
			if (addBatchDeleteForever) {
				column.AddConfirmActionForMultiChecked(
					new T("Batch Delete Forever"),
					"fa fa-remove",
					string.Format(new T("Please select {0} to delete"), typeName),
					new T("Batch Delete Forever"),
					ScriptStrings.ConfirmMessageTemplateForMultiSelected(
						string.Format(new T("Sure to delete following {0} forever?"), typeName), "ToString"),
					ScriptStrings.PostConfirmedActionForMultiSelected(
						"Id", app.BatchUrl + "?action=delete_forever"),
					new { type = "type-danger" });
			}
		}
	}
}