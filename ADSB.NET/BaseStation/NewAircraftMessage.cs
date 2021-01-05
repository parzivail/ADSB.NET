namespace ADSB.NET.BaseStation
{
	public class NewAircraftMessage : TelemetryMessage
	{
		public NewAircraftMessage(string[] parts) : base(MessageOpcode.NewAircraft, parts)
		{
		}
	}
}