using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicStudio
{
	public partial class EditClient : System.Web.UI.Page
	{
		public int Id => int.Parse(Request.Params["EditClientId"]);
		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += BtnSave_Click;
			btnCancel.Click += BtnCancel_Click;
			clientEditor.ClientId = Id;
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("MainPage.aspx");
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			using (var context = new musicstudiodbEntities())
			{
				var client = context.Clients.First(a => a.Id == clientEditor.ClientId);
				try
				{
					client.Name = clientEditor.FirstName;
					client.Surname = clientEditor.Surname;
					client.SecondName = clientEditor.SecondName;
					client.PhoneNumber = clientEditor.Phone;	
				}
				catch (EntityDataSourceValidationException tbErr)
				{
					Response.Write(tbErr.Message);
					return;
				}
				try
				{
					context.SaveChanges();
					Response.Redirect("MainPage.aspx");
				}
				catch (DbEntityValidationException ex)
				{
					// case of catching errors on saving new data in db
					String message = "";
#if DEBUG
					// show full err sequence to devs
					foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
					{
						message += "Object: " + validationError.Entry.Entity.ToString() + "\t";
						foreach (DbValidationError err in validationError.ValidationErrors)
							message += err.ErrorMessage.ToString() + " ";
					}
#else
                        // do not show extra info to user
                        message = "Server error occured"
#endif
					Response.Write(message);
					return;
				}
			}
		}
	}
} 