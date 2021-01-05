using System.IO;

namespace ADSB.NET.BaseStation
{
	public class ClickMessage : TelemetryMessage
	{
		public ClickMessage(string[] parts) : base(MessageOpcode.Click, parts)
		{
			if (!MessageUtil.GetBoolField(parts, MessageField.AircraftId) ||
			    !MessageUtil.GetBoolField(parts, MessageField.FlightId))
				throw new InvalidDataException(Lang.ClickFieldsWereNotNull);
		}
	}
}