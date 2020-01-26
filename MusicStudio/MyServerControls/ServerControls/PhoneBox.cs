using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using DBLayer;

namespace MyServerControls.ServerControls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:PhoneTextBox runat=server></{0}:PhoneTextBox >")]
	public class PhoneBox : TextBox
	{
		public PhoneBox()
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
					var count = (from c in context.Clients where c.PhoneNumber == res select c).Count();
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
