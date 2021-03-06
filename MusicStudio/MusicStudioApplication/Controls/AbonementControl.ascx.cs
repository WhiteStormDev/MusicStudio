﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLayer;

namespace MusicStudioApplication.Controls
{
	public partial class AbonementControl : System.Web.UI.UserControl
	{
		public int AbonementId { get; set; } = 0;
		public int ClientId { get; set; } = 0;
		public DateTime DateStart
		{
			get => calendarStart.SelectedDate;
			set => calendarStart.SelectedDate = value;
		}
		public DateTime DateEnd
		{
			get => calendarEnd.SelectedDate;
			set => calendarEnd.SelectedDate = value;
		}
		public int SubjectId
		{
			get
			{
				return int.Parse(ddlSubject.SelectedValue);
			}
			set
			{
				using (var context = new musicstudiodbContext())
				{
					ddlSubject.DataSource = context.Subjects.ToList();
					ddlSubject.DataBind();
					ddlSubject.SelectedValue = value.ToString();

					ddlTeacher.DataSource = context.Teachers.Where(t => t.SubjectId == value).ToList();
					ddlTeacher.DataBind();
					ddlTeacher.SelectedValue = value.ToString();
				}
			}
		}
		public int TeacherId
		{
			get => int.Parse(ddlTeacher.SelectedValue);
			set
			{
				using (var context = new musicstudiodbContext())
				{
					ddlTeacher.DataSource = context.Teachers.ToList();
					ddlTeacher.DataBind();
					ddlTeacher.SelectedValue = value.ToString();
				}
			}
		}

		public int LessonsCount 
		{ 
			get => int.Parse(txtLessonCount.Text);
			set => txtLessonCount.Text = value.ToString();
		}

		public bool EditMode => AbonementId != 0;

		private void BindDropDownLists()
		{
			using (var context = new musicstudiodbContext())
			{
				ddlTeacher.DataSource = context.Teachers.ToList();
				ddlTeacher.DataBind();

				ddlSubject.DataSource = context.Subjects.ToList();
				ddlSubject.DataBind();
			}
		}
		private void FillData()
		{
			using (var context = new musicstudiodbContext())
			{
				var abonement = (from c in context.Abonements where c.Id == AbonementId select c).First();
				DateStart = abonement.DateStart;
				DateEnd = abonement.DateEnd;
				LessonsCount = abonement.LessonsCount;
				SubjectId = abonement.Teacher.SubjectId;
				TeacherId = abonement.TeacherId;

				
				context.SaveChanges();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindDropDownLists();
				if (EditMode)
					FillData();
			}
		}
	}
}