using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostSharp.Samples.SessionState
{
    public partial class Default : System.Web.UI.Page
    {
        [SessionState]
        private int sessionCounter;

        [ViewState]
        private int viewStateCounter;
 
        protected void incrementButton_OnClick(object sender, EventArgs e)
        {
            this.sessionCounter++;
            this.viewStateCounter++;
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.sessionCounterLabel.Text = this.sessionCounter.ToString();
            this.pageViewCounterLabel.Text = this.viewStateCounter.ToString();
        }

     
    }
}