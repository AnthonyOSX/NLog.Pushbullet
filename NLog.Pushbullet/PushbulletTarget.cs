using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System.ComponentModel;

namespace NLog.Pushbullet {

	[Target("Pushbullet")]
	public sealed class PushbulletTarget : TargetWithLayout {

		[RequiredParameter]
		public string AccessToken { get; set; }

		[DefaultValue("Message from NLog on ${machinename}")]
		public Layout NoteTitle { get; set; }
		
		public PushbulletTarget() {
			NoteTitle = "Message from NLog on ${machinename}";
		}

		protected override void Write(LogEventInfo logEvent) {
			string logMessage = Layout.Render(logEvent);
			dynamic result = Pushbullet.PushNote(AccessToken, NoteTitle.Render(logEvent), logMessage);

			if (result.GetType().GetProperty("error") != null) {
				throw new PushbulletException("{0} - {1}", result.error.type, result.error.message);
			}
		}
		
	}

}
