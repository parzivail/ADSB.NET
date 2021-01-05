namespace ADSB.NET.BaseStation
{
	public class SelectionChangeMessage : TelemetryMessage
	{
		public string Callsign;

		public SelectionChangeMessage(string[] parts) : base(MessageOpcode.SelectionChange, parts)
		{
			Callsign = MessageUtil.GetField(parts, TransmissionField.Callsign);
		}
	}
}