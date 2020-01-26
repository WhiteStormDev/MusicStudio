using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CSFControls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:PhoneTextBox runat=server></{0}:PhoneTextBox >")]
	public class PhoneTextBox : TextBox
	{
		public PhoneTextBox()
		{
			BackColor = Color.LightBlue;
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("+79037776655")]
		[Localizable(true)]

		public string Value
		{
			get
			{
				var res = Text;
				using (var context = new musicstudiodbEntities())
				{
					var count = (from c in context.User where c.Mail == res select c).Count();
					if (count > 0)
						throw new EntityDataSourceValidationException("Mail '" + res + "' is already in use!");
				}
				return res;
			}
			set
			{
				this.Text = value.ToString();
			}
		}
	}
}
