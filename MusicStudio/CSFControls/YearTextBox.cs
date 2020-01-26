using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace CSFControls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:YearTextBox runat=server></{0}:YearTextBox >")]
	public class YearTextBox : TextBox
	{
		public YearTextBox()
		{
			BackColor = Color.LightGreen;
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("2019")]
		[Localizable(true)]
		public int Year
		{
			get
			{
				if (!int.TryParse(Text, out int res))
					return DateTime.Now.Year;
				if (res < MinYear)
					return MinYear;
				if (res > MaxYear)
					return MaxYear;
				return res;
			}
			set
			{
				this.Text = value.ToString();
			}
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("2019")]
		[Localizable(true)]
		public int MaxYear
		{
			get;
			set;
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("1900")]
		[Localizable(true)]
		public int MinYear
		{
			get;
			set;
		}
	}
}
