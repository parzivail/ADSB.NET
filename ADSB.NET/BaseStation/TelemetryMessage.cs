using System;

namespace ADSB.NET.BaseStation
{
	public class TelemetryMessage
	{
		public MessageOpcode MessageType;
		public int TransmissionTypeId;
		public TransmissionType TransmissionType;
		public int SessionId;
		public int AircraftId;
		public string HexId;
		public int FlightId;
		public DateTime DateTimeGenerated;
		public DateTime DateTimeLogged;

		public TelemetryMessage(MessageOpcode typeMessageType, string[] parts)
		{
			MessageType = typeMessageType;

			TransmissionTypeId = MessageUtil.GetIntField(parts, MessageField.TransmissionType);
			TransmissionType = (TransmissionType)TransmissionTypeId;
			SessionId = MessageUtil.GetIntField(parts, MessageField.SessionId);
			AircraftId = MessageUtil.GetIntField(parts, MessageField.AircraftId);
			HexId = MessageUtil.GetField(parts, MessageField.HexIdent);
			FlightId = MessageUtil.GetIntField(parts, MessageField.FlightId);

			DateTimeGenerated =
				DateTime.Parse(
					$"{MessageUtil.GetField(parts, MessageField.DateMsgGenerated)} {MessageUtil.GetField(parts, MessageField.TimeMsgGenerated)}");
			DateTimeLogged =
				DateTime.Parse(
					$"{MessageUtil.GetField(parts, MessageField.DateMsgLogged)} {MessageUtil.GetField(parts, MessageField.TimeMsgLogged)}");
		}

		public override string ToString()
		{
			return
				$"{MessageType}[{TransmissionType}, {nameof(AircraftId)}: {AircraftId}, {nameof(HexId)}: {HexId}, {nameof(FlightId)}: {FlightId}, {nameof(DateTimeGenerated)}: {DateTimeGenerated}, {nameof(DateTimeLogged)}: {DateTimeLogged}]";
		}
	}
}