using System;
using System.Web.UI;

namespace July09v31.UI
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/home");
        }
    }
}