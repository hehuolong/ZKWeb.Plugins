﻿$(function () {
	$("input[name=RememberLogin]").closest(".form-group").addClass("rememberLogin").appendTo(".form-actions");
	$("[name=AdminLoginForm]").on("success", function () {
		var url = $(this).find("[name=RedirectAfterLogin]").val();
		location.href = url;
	});
});