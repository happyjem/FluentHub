using FluentHub.App.Utils;
using System.Globalization;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;


namespace FluentHub.App.Services
{
	public class ToastService
	{
		public ToastService(ILogger logger = null) => _logger = logger;

		private readonly ILogger _logger;

		public void ShowToastNotification(string title, string text, string activationArgs = "", string appLogoOverrideImage = null, string heroImage = null, string inlineImage = null)
		{
			try
			{
				var builder = new AppNotificationBuilder()
					.AddText(title.Truncate(50))
					.AddText(text.Truncate(100));

				if (!string.IsNullOrEmpty(activationArgs))
					builder.AddArgument("args", activationArgs); // 활성 인자

				if (Uri.TryCreate(appLogoOverrideImage, UriKind.RelativeOrAbsolute, out var uri))
					builder.SetAppLogoOverride(uri);
				if (Uri.TryCreate(heroImage, UriKind.RelativeOrAbsolute, out uri))
					builder.SetHeroImage(uri);
				if (Uri.TryCreate(inlineImage, UriKind.RelativeOrAbsolute, out uri))
					builder.SetInlineImage(uri);

				var notification = builder.BuildNotification();
				AppNotificationManager.Default.Show(notification);
			}
			catch (Exception e)
			{
				_logger?.Error(e);
				throw;
			}
		}

		public void UpdateBadgeGlyph(BadgeGlyphType glyphType, int? number)
		{
			XmlDocument badgeXml;
			string badgeGlyphValue;

			if (glyphType == BadgeGlyphType.Number)
			{
				if (number is null)
					throw new ArgumentNullException(nameof(number));
				// Get the blank badge XML payload for a badge glyph
				badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
				badgeGlyphValue = number.ToString();
			}
			else
			{
				badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);
				badgeGlyphValue = glyphType.ToString().ToLower();
			}

			// Set the value of the badge in the XML to our glyph value
			XmlElement badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
			badgeElement.SetAttribute("value", badgeGlyphValue);

			// Create the badge notification
			BadgeNotification badge = new(badgeXml);

			// Create the badge updater for the application
			BadgeUpdater badgeUpdater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();

			// And update the badge
			badgeUpdater.Update(badge);
		}

		public string GetBadgeString()
		{
			// Returns a default badge XML string for a glyph (e.g., "activity")
			return "<badge value=\"activity\"/>";
		}

		public void ShowToastNotificationWithProgress(string tag, string group, string title, string progressTitle, string leftText, string rightText, double progress)
		{
			// Build App Notification with a progress bar (0.0 ~1.0)
			var builder = new AppNotificationBuilder()
				.AddText(title)
				.AddProgressBar(new AppNotificationProgressBar
				{
					Title = progressTitle,
					Value = progress,
					Status = leftText,
					ValueStringOverride = rightText
				});

			// Create notification and set identifiers for later updates
			var notification = builder.BuildNotification();
			notification.Tag = tag;
			notification.Group = group;

			// Show the toast notification
			AppNotificationManager.Default.Show(notification);
		}

		public void UpdateToastWithProgress(string tag, string group, string leftText, string rightText, double progress, uint sequenceNumber)
		{
			// Update using AppNotificationProgressData (sequence enforces ordering)
			var data = new AppNotificationProgressData(sequenceNumber)
			{
				Value = progress,
				Status = leftText,
				ValueStringOverride = rightText
			};

			// Async update API; fire and forget here
			_ = AppNotificationManager.Default.UpdateAsync(data, tag, group);
		}
	}
}
