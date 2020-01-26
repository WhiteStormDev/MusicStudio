using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLayer;

namespace MusicStudioApplication
{
	public partial class CreateClient : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += BtnSave_Click;
			btnCancel.Click += BtnCancel_Click;
			FillData();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("MainPage.aspx");
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			using (var context = new musicstudiodbContext())
			{
				try
				{
					var client = new Client();
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
						context.Clients.Add(client);
						context.SaveChanges();
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
					var abonement = new Abonement();
					//abonement.ClientId = client.Id;
					//abonement.DateEnd = calendarEnd.SelectedDate;
					//abonement.DateStart = calendarStart.SelectedDate;
					//abonement.LessonsCount = txtLessonCount.Text == "" ? 0 : int.Parse(txtLessonCount.Text);
					//abonement.TeacherId = int.Parse(ddlTeacher.SelectedValue);
					abonement.ClientId = client.Id;
					abonement.DateEnd = abonementEditor.DateEnd;
					abonement.DateStart = abonementEditor.DateStart;
					abonement.LessonsCount = abonementEditor.LessonsCount;
					//txtLessonCount.Text == "" ? 0 : int.Parse(txtLessonCount.Text);
					abonement.TeacherId = abonementEditor.TeacherId;
					context.Abonements.Add(abonement);
					//client.CarMakeID = int.Parse(ddlMake.SelectedValue);
					//client.CarYear = int.Parse(txtYear.Text);
					//client.Model = txtModel.Text;
					context.SaveChanges();
				}
				catch (DbUpdateException dbex)
				{
					Response.Write("<script>alert('Заполните все поля')</script>");
					return;
				}
				
				
				Response.Redirect("MainPage.aspx");
			}
		}

		private void FillData()
		{
			using (var context = new musicstudiodbContext())
			{
				//ddlTeacher.DataSource = context.Teachers.ToList();
				//ddlTeacher.DataBind();

				//ddlSubject.DataSource = context.Subjects.ToList();
				//ddlSubject.DataBind();

			}
		}
	}
}