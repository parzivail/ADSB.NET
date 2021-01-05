using System;

namespace ADSB.NET.BaseStation
{
	public class TrackedPlane
	{
		public int AircraftId;
		public string HexIdent;
		public int FlightId;
		public string Callsign;

		public double Altitude;
		public double GroundSpeed;
		public double GroundTrackAngle;
		public double Latitude;
		public double Longitude;
		public double VerticalRate;
		public double Squawk;
		public bool Alert;
		public bool Emergency;
		public bool SpecialPositionIndicator;
		public bool IsOnGround;

		public DateTime LastMessage;

		public TrackedPlane(TelemetryMessage message)
		{
			AircraftId = message.AircraftId;
			HexIdent = message.HexId;
			FlightId = message.FlightId;

			if (message is TransmissionMessage transmissionMessage)
				LoadMessage(transmissionMessage);
		}

		public void LoadMessage(TransmissionMessage message)
		{
			var fields = MessageUtil.GetTransmissionFields(message.TransmissionType);

			LastMessage = message.DateTimeLogged;

			if (fields.HasFlag(TransmissionField.Callsign))
				Callsign = message.Callsign;
			if (fields.HasFlag(TransmissionField.Altitude))
				Altitude = message.Altitude;
			if (fields.HasFlag(TransmissionField.GroundSpeed))
				GroundSpeed = message.GroundSpeed;
			if (fields.HasFlag(TransmissionField.GroundTrackAngle))
				GroundTrackAngle = message.GroundTrackAngle;
			if (fields.HasFlag(TransmissionField.Latitude))
				Latitude = message.Latitude;
			if (fields.HasFlag(TransmissionField.Longitude))
				Longitude = message.Longitude;
			if (fields.HasFlag(TransmissionField.VerticalRate))
				VerticalRate = message.VerticalRate;
			if (fields.HasFlag(TransmissionField.Squawk))
				Squawk = message.Squawk;
			if (fields.HasFlag(TransmissionField.Alert))
				Alert = message.Alert;
			if (fields.HasFlag(TransmissionField.Emergency))
				Emergency = message.Emergency;
			if (fields.HasFlag(TransmissionField.SpecialPositionIndicator))
				SpecialPositionIndicator = message.SpecialPositionIndicator;
			if (fields.HasFlag(TransmissionField.IsOnGround))
				IsOnGround = message.IsOnGround;
		}
	}
}