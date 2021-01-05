namespace ADSB.NET.BaseStation
{
	public class TransmissionMessage : TelemetryMessage
	{
		public string Callsign;
		public int Altitude;
		public double GroundSpeed;
		public double GroundTrackAngle;
		public double Latitude;
		public double Longitude;
		public double VerticalRate;
		public int Squawk;
		public bool Alert;
		public bool Emergency;
		public bool SpecialPositionIndicator;
		public bool IsOnGround;

		public TransmissionMessage(string[] parts) : base(MessageOpcode.TransmissionMessage, parts)
		{
			var fields = MessageUtil.GetTransmissionFields(TransmissionType);

			if (fields.HasFlag(TransmissionField.Callsign))
				Callsign = MessageUtil.GetField(parts, TransmissionField.Callsign);
			if (fields.HasFlag(TransmissionField.Altitude))
				Altitude = MessageUtil.GetIntField(parts, TransmissionField.Altitude) ?? Altitude;
			if (fields.HasFlag(TransmissionField.GroundSpeed))
				GroundSpeed = MessageUtil.GetDoubleField(parts, TransmissionField.GroundSpeed) ?? GroundSpeed;
			if (fields.HasFlag(TransmissionField.GroundTrackAngle))
				GroundTrackAngle = MessageUtil.GetDoubleField(parts, TransmissionField.GroundTrackAngle) ??
				                   GroundTrackAngle;
			if (fields.HasFlag(TransmissionField.Latitude))
				Latitude = MessageUtil.GetDoubleField(parts, TransmissionField.Latitude) ?? Latitude;
			if (fields.HasFlag(TransmissionField.Longitude))
				Longitude = MessageUtil.GetDoubleField(parts, TransmissionField.Longitude) ?? Longitude;
			if (fields.HasFlag(TransmissionField.VerticalRate))
				VerticalRate = MessageUtil.GetDoubleField(parts, TransmissionField.VerticalRate) ?? VerticalRate;
			if (fields.HasFlag(TransmissionField.Squawk))
				Squawk = MessageUtil.GetIntField(parts, TransmissionField.Squawk) ?? Squawk;
			if (fields.HasFlag(TransmissionField.Alert))
				Alert = MessageUtil.GetBoolField(parts, TransmissionField.Alert) ?? Alert;
			if (fields.HasFlag(TransmissionField.Emergency))
				Emergency = MessageUtil.GetBoolField(parts, TransmissionField.Emergency) ?? Emergency;
			if (fields.HasFlag(TransmissionField.SpecialPositionIndicator))
				SpecialPositionIndicator =
					MessageUtil.GetBoolField(parts, TransmissionField.SpecialPositionIndicator) ??
					SpecialPositionIndicator;
			if (fields.HasFlag(TransmissionField.IsOnGround))
				IsOnGround = MessageUtil.GetBoolField(parts, TransmissionField.IsOnGround) ?? IsOnGround;
		}
	}
}