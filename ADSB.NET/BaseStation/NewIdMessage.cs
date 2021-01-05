namespace ADSB.NET.BaseStation
{
	public class NewIdMessage : TelemetryMessage
	{
		public string Callsign;

		public NewIdMessage(string[] parts) : base(MessageOpcode.NewId, parts)
		{
			Callsign = MessageUtil.GetField(parts, TransmissionField.Callsign);
		}
	}
}