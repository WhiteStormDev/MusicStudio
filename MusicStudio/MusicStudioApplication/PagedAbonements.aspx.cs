using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MusicStudioApplication
{
    public partial class PagedAbonements : System.Web.UI.Page
    {
        protected string APIServerURL
        {
            get
            {
                return "https://localhost:44346/";//ConfigurationManager.AppSettings["APIServerURL"];
            }
        }
        protected string PageLen
        {
            get
            {
                return ConfigurationManager.AppSettings["PageLen"] ?? "5";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}