﻿using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ZKWeb.Plugins.Common.Base.src.TemplateFilters;

namespace ZKWeb.Plugins.Common.Base.src.TemplateTags {
	/// <summary>
	/// 设置网站标题
	/// 需要配合标签"render_title"使用
	/// 例子
	/// {% use_title "Plain Text Title" %}
	/// {% use_title variable_title %}
	/// </summary>
	public class UseTitle : Tag {
		/// <summary>
		/// 设置标题到变量中
		/// </summary>
		public override void Render(Context context, TextWriter result) {
			context.Environments[0][RenderTitle.Key] =
				Filters.WebsiteTitle(context[Markup.Trim()] as string);
		}
	}
}
