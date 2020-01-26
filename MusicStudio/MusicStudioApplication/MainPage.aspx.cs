using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLayer;
using MusicStudio;

namespace MusicStudioApplication
{
	public partial class MainPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//AddUpdateAppSettings("adminLogin", "admin");
			//AddUpdateAppSettings("adminPassword", "password");
			FillClients();

			dgClients.SelectedIndexChanged += DgClients_SelectedIndexChanged;
			dgClients.EditCommand += DgClients_EditCommand;
			dgClients.DeleteCommand += DgClients_DeleteCommand;
			buttonDel.Click += ButtonDel_Click;
			buttonAddClient.ServerClick += ButtonAddClient_ServerClick;
			
			rptClientAbonements.ItemCommand += RptClientAbonements_ItemCommand;
		}

		private void DgClients_EditCommand(object source, DataGridCommandEventArgs e)
		{
			var clientModels = dgClients.DataSource as List<ClientModel>;
			if (clientModels == null || clientModels.Count == 0)
				return;

			var id = clientModels[e.Item.ItemIndex].Id;
			Response.Redirect("EditClient.aspx?EditClientId=" + id.ToString());
			
		}

		private void RptClientAbonements_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Click":
					if (int.TryParse((string)e.CommandArgument, out int id))
					{
						ViewState["RepeaterAbonementId"] = id;
						calendarNextDate.Visible = true;
					}
					break;
			}
		}

		private void SetAbonementDateById(int abonementId)
		{
			using (var context = new musicstudiodbEntities())
			{
				var abon = context.Abonements.First(a => a.Id == abonementId);
				if (abon == null)
					return;

				//abon.NextDate = 
			}
		}

		protected void CalendarNextDate_SelectionChanged(object sender, EventArgs e)
		{
			if (ViewState["RepeaterAbonementId"] == null)
				return;

			var id = (int)ViewState["RepeaterAbonementId"];
			using (var context = new musicstudiodbEntities())
			{
				var abon = context.Abonements.First(a => a.Id == id);
				if (abon == null) return;

				abon.NextDate = calendarNextDate.SelectedDate;
				context.SaveChanges();

				FillAbonementsByClientId(abon.ClientId);
				calendarNextDate.Visible = false;
			}

			

			//calendarNextDate.SelectedDate
			//if (calendarNextDate.SelectedDate != DateTime.MinValue)
			//	lblSelected.Text = "The date selected is " +
			//	Calendar1.SelectedDate.ToShortDateString();

			//lblCountUpdate();
		}

		private void ButtonAddClient_ServerClick(object sender, EventArgs e)
		{
			Response.Redirect("CreateClient.aspx");
		}

		private void DgClients_SelectedIndexChanged(object sender, EventArgs e)
		{
			var clientModels = dgClients.DataSource as List<ClientModel>;
			if (clientModels == null || clientModels.Count == 0)
				return;
			var clientId = clientModels[dgClients.SelectedIndex].Id;

			FillAbonementsByClientId(clientId);
		}

		private void ButtonDel_Click(object sender, EventArgs e)
		{
			if (ViewState["VictimID"] == null)
				return;

			int id = (int)ViewState["VictimID"];

			if (DeleteClient(id, true))
				Response.Redirect("MainPage.aspx");
		}

		//ViewState -хранение информации на странице (у клиента) 
		// + не зависит от состояния сервера, недостаток в том что инфа постоянно гуляет туда обратно клиент серв страница. Что утяжеляет страницу
		//Session - хранение на сервере. если с сервом что то произошло то она теряется
		// между клиентм и сервером гуляет только идентификатор
		// идея в том чтобы ассоциировать номер нашего контрола с 
		private void DgClients_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			var clientModels = dgClients.DataSource as List<ClientModel>;
			if (clientModels == null || clientModels.Count == 0)
				return;

			var idForDelete = clientModels[e.Item.ItemIndex].Id;
			try
			{
				if (DeleteClient(idForDelete, false))
					Response.Redirect("MainPage.aspx");
			}
			catch (Exception ex)
			{
				lblMsg.Text = ex.Message;
				errPanel.Visible = true;
			}
			//страница может сделать повторный постбэк. Тем временем айдишники сдвинутся и мы можем удалить еще одного клиента

			//НО ЕСЛИ МЫ ПОЛУЧИЛИ ОШИБКУ РЕДИРЕКТИТЬ НЕЛЬЗЯ ПОТОМУ ЧТО МЫ ПРОСТО ЗАГРУЗИМ ЕЕ ЗАНОГО

			// то есть 
			// client ->get-> server
			// page <-200<-  server
			//   -> POST -> S
			//     <-302<- (MainPage)
			// page -> get -> S

			//можно вывести предупреждение что у него есть абонементы и предупредить что абонементы так же удалятся
		}

		static void AddUpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = System.Web.HttpContext.Current == null ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) : System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
				var settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error writing app settings");
			}
		}

		private bool DeleteClient(int idForDelete, bool delAbonements)
		{
			errPanel.Visible = false;
			buttonDel.Visible = false;

			try
			{
				using (var cont = new musicstudiodbEntities())
				{
					if (delAbonements)
					{
						cont.Abonements.RemoveRange(from a in cont.Abonements where a.ClientId == idForDelete select a);
					}
					var client = (from c in cont.Clients
								  where c.Id == idForDelete
								  select c).First();
					cont.Clients.Remove(client);
					cont.SaveChanges();
				}
			}
			//если мы не поставили каскадный update конечно
			catch (DbUpdateException dbex)
			{
				var entries = dbex.Entries.ToArray();
				if (entries.Length > 0)
				{
					//dbexLblMsg.Text = (entries[0] as Teacher).Surname + " не поддается...";
					//dbexLblMsg.Text = (entries[0].Entity as Teacher).Surname + " не поддается...";
					//нужно юзать reflection (get property name by reflection)
					var obj = entries[0].Entity;
					var property = obj.GetType().GetProperty("Surname");
					var surname = property.GetValue(obj, null);
					dbexLblMsg.Text = surname + " не поддается...";

					// try add button for delete with abonements
					// need to save id value. MB by using Session
					// решениее хорошо тем что id не полетит на клиент и останется в памяти сервера.
					// и когда мы получим событие нажатия мы из session его достанем
					// при этом сессия ассоциирована именно с нашем сеансом работы
					// но у сессии есть и минусы
					// если мы балансим серверы то может получится так что запрос придет на разгруженный сервер и там не будет нашей сессии
					// даже если сервер один - сервер подвержен перезагрузкам. Recycle Pool
					// внутри сервера IIS и внутри IIS - пулы. Как правило один пул на приложение
					// есть определеный recycle interval после которого производится recycle pool чтобы он не жрал много памяти. и сессии удаляются
					// также есть timeout и у сессии
					// есть решение - сохранять сессию в БД

					// можно избежать работу с сессией и использовать ViewState
					// это тоже Dictionary как и сессия
					// ViewState помещает зашифрованную коллекцию в hidden поле на странице
					// Он не удаляется как сессия и все гарантированно доедет до сервера
					// Минус viewState в том что hidden разрастается и повышается нагрузка на сеть
					// В том числе существует form value limit у разных браузеров
					if (!delAbonements)
					{
						buttonDel.Visible = true;
						ViewState["VictimID"] = idForDelete;
					}

				}
				else
				{
					dbexLblMsg.Text = "Ошибка удаления из БД (произошло что-то совсем нехорошее)";
				}
				errPanel.Visible = true;
				return false;
			}
			return true;
		}

		private void FillAbonementsByClientId(int clientId)
		{
			using (var context = new musicstudiodbEntities())
			{

				var ents = from a in context.Abonements
						   where a.ClientId == clientId
						   select new AbonementModel()
						   {
							   Id = a.Id,
							   DateStart = a.DateStart,
							   DateEnd = a.DateEnd,
							   DateNext = a.NextDate.Value,
							   TeacherSurname = a.Teacher.Surname,
							   LessonsCount = a.LessonsCount,
							   ClientId = a.ClientId
						   };
				var entList = ents.ToList();
				rptClientAbonements.DataSource = entList;
				rptClientAbonements.DataBind();
			}
		}

		private void FillClients()
		{
			using (var cont = new musicstudiodbEntities())
			{

				var clients = (from c in cont.Clients
							   select new ClientModel
							   {
								   Id = c.Id,
								   FirstName = c.Name,
								   SecondName = c.SecondName,
								   Surname = c.Surname,
								   PhoneNumber = c.PhoneNumber,
								   RemainingAbonementsCount = c.Abonements.Count(
									   a =>
										   a.LessonsCount > a.VisitDates.Count() &&
										   a.DateEnd > DateTime.Now)
							   }).ToList();
				dgClients.DataSource = clients;
				dgClients.DataBind();

				//FillAbonementsByClientId(clients[0].Id);
			}
		}

	}
}