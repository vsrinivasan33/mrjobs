using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mrjobs.Pages
{
	public partial class JobNotesPage : PopupPage
	{
		public JobNotesPage(string notes)
		{
			InitializeComponent();
			editorJobNotes.Text = notes;
		}

		public JobNotesPage()
		{
			InitializeComponent();
		}

		void ButtonClose_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send<JobNotesPage, string>(this, "JobNotes", editorJobNotes.Text);
			PopupNavigation.PopAsync();
		}
	}
}
