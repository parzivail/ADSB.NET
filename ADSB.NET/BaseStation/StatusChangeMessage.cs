using System.IO;

namespace ADSB.NET.BaseStation
{
	public class StatusChangeMessage : TelemetryMessage
	{
		public readonly TrackingStatus Status;

		public StatusChangeMessage(string[] parts) : base(MessageOpcode.StatusChange, parts)
		{
			var statusType = MessageUtil.GetField(parts, TransmissionField.Callsign);
			Status = statusType switch
			{
				"PL" => TrackingStatus.PositionLost,
				"SL" => TrackingStatus.SignalLost,
				"RM" => TrackingStatus.Remove,
				"AD" => TrackingStatus.Delete,
				"OK" => TrackingStatus.Ok,
				_ => throw new InvalidDataException(string.Format(Lang.UnknownStatus, statusType))
			};
		}
	}
}