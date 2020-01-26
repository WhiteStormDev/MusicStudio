using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicStudioApplication
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Authenticated())
			{
				Response.Redirect("MainPage.aspx");
			}

			btnSubmit.Click += BtnSubmit_Click;
		}

		private bool Authenticated()
		{
			if (Request.Cookies["token"] != null && !string.IsNullOrEmpty(Request.Cookies["token"].Value))
			{
				var token = MakeToken(ConfigurationManager.AppSettings["adminLogin"],
					ConfigurationManager.AppSettings["adminPassword"]);
				return token == Request.Cookies["token"].Value;
			}
			return false;
		}

		private void BtnSubmit_Click(object sender, EventArgs e)
		{
			if (string.Equals(txtLogin.Text, ConfigurationManager.AppSettings["adminLogin"])
				&&
				string.Equals(txtPassword.Text, ConfigurationManager.AppSettings["adminPassword"]))
			{
				Response.Cookies["token"].Value = MakeToken(txtLogin.Text, txtPassword.Text);
				Response.Redirect("MainPage.aspx");
				return;
			}

			lblOutput.Text = "User with such password does not exist";
		}

		private string MakeToken(string login, string password)
		{
			var alg = MD5.Create();
			var bytes = Encoding.ASCII.GetBytes(login + password);
			return Convert.ToBase64String(alg.ComputeHash(bytes));
		}
	}
}