using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicStudio.Controls
{
	public partial class ClientControl : System.Web.UI.UserControl
	{
		public int ClientId { get; set; }
		public string FirstName { get => txtName.Text; set => txtName.Text = value; }
		public string Surname { get => txtSurname.Text; set => txtSurname.Text = value; }
		public string SecondName { get => txtSecondName.Text; set => txtSecondName.Text = value; }
		public string Phone { get => txtPhone.Value; set => txtPhone.Text = value; }

		public bool EditMode => ClientId != 0;

		private void FillData()
		{
			using (var context = new musicstudiodbEntities())
			{
				var client = (from c in context.Clients where c.Id ==  ClientId select c).First();
				FirstName = client.Name;
				SecondName = client.SecondName;
				Surname = client.Surname;
				Phone = client.PhoneNumber;
				//txtName.Text = client.Name;
				//txtSurname.Text = client.Surname;
				//txtPhone.Text = client.PhoneNumber;
				//txtSecondName.Text = client.SecondName;
				context.SaveChanges();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (EditMode)
					FillData();
			}
		}
	}
}