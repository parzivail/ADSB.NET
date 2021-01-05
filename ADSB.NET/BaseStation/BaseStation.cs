using System.IO;
using System.Linq;

namespace ADSB.NET.BaseStation
{
	public static class BaseStation
	{
		public static TelemetryMessage Parse(string data)
		{
			var parts = data.Split(',').Select(s => s.Trim().ToUpper()).ToArray();

			if (parts.Length < 10)
				return null;

			var messageType = MessageUtil.GetField(parts, MessageField.MessageType);

			return messageType switch
			{
				"SEL" => new SelectionChangeMessage(parts),
				"ID" => new NewIdMessage(parts),
				"AIR" => new NewAircraftMessage(parts),
				"STA" => new StatusChangeMessage(parts),
				"CLK" => new ClickMessage(parts),
				"MSG" => new TransmissionMessage(parts),
				_ => throw new InvalidDataException(string.Format(Lang.UnknownMessageType, messageType))
			};
		}
	}
}