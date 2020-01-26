using DBLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicStudioApplication
{
	public partial class CreateAbonement : System.Web.UI.Page
	{
		public int Id => int.Parse(Request.Params["CreateAbonementClientId"]);
		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += BtnSave_Click;
			btnCancel.Click += BtnCancel_Click;
			abonementEditor.ClientId = Id;
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
					
					var abonement = new Abonement();
					//abonement.ClientId = client.Id;
					//abonement.DateEnd = calendarEnd.SelectedDate;
					//abonement.DateStart = calendarStart.SelectedDate;
					//abonement.LessonsCount = txtLessonCount.Text == "" ? 0 : int.Parse(txtLessonCount.Text);
					//abonement.TeacherId = int.Parse(ddlTeacher.SelectedValue);
					abonement.ClientId = abonementEditor.ClientId;
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
	}
}