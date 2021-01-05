using System;
using System.Collections.Generic;

namespace ADSB.NET.BaseStation
{
	public enum MessageField
	{
		MessageType = 0,
		TransmissionType = 1,
		SessionId = 2,
		AircraftId = 3,
		HexIdent = 4,
		FlightId = 5,
		DateMsgGenerated = 6,
		TimeMsgGenerated = 7,
		DateMsgLogged = 8,
		TimeMsgLogged = 9
	}

	[Flags]
	public enum TransmissionField
	{
		Callsign = 1 << 0,
		Altitude = 1 << 1,
		GroundSpeed = 1 << 2,
		GroundTrackAngle = 1 << 3,
		Latitude = 1 << 4,
		Longitude = 1 << 5,
		VerticalRate = 1 << 6,
		Squawk = 1 << 7,
		Alert = 1 << 8,
		Emergency = 1 << 9,
		SpecialPositionIndicator = 1 << 10,
		IsOnGround = 1 << 11
	}

	internal static class MessageUtil
	{
		private static readonly Dictionary<TransmissionField, int> FieldOffsets = new Dictionary<TransmissionField, int>
		{
			{ TransmissionField.Callsign, 10 },
			{ TransmissionField.Altitude, 11 },
			{ TransmissionField.GroundSpeed, 12 },
			{ TransmissionField.GroundTrackAngle, 13 },
			{ TransmissionField.Latitude, 14 },
			{ TransmissionField.Longitude, 15 },
			{ TransmissionField.VerticalRate, 16 },
			{ TransmissionField.Squawk, 17 },
			{ TransmissionField.Alert, 18 },
			{ TransmissionField.Emergency, 19 },
			{ TransmissionField.SpecialPositionIndicator, 20 },
			{ TransmissionField.IsOnGround, 21 }
		};

		private static readonly Dictionary<TransmissionType, TransmissionField> FieldMap =
			new Dictionary<TransmissionType, TransmissionField>
			{
				{ TransmissionType.IdentityAndCategory, TransmissionField.Callsign },
				{
					TransmissionType.SurfacePosition,
					TransmissionField.Altitude | TransmissionField.GroundSpeed | TransmissionField.GroundTrackAngle |
					TransmissionField.Latitude | TransmissionField.Longitude | TransmissionField.IsOnGround
				},
				{
					TransmissionType.AirbornePosition,
					TransmissionField.Altitude | TransmissionField.Latitude | TransmissionField.Longitude |
					TransmissionField.Alert | TransmissionField.Emergency | TransmissionField.SpecialPositionIndicator |
					TransmissionField.IsOnGround
				},
				{
					TransmissionType.AirborneVelocity,
					TransmissionField.GroundSpeed | TransmissionField.GroundTrackAngle | TransmissionField.VerticalRate
				},
				{
					TransmissionType.SurveillanceAltitude,
					TransmissionField.Altitude | TransmissionField.Alert | TransmissionField.SpecialPositionIndicator |
					TransmissionField.IsOnGround
				},
				{
					TransmissionType.SurveillanceIdentity,
					TransmissionField.Altitude | TransmissionField.Squawk | TransmissionField.Alert |
					TransmissionField.Emergency | TransmissionField.SpecialPositionIndicator |
					TransmissionField.IsOnGround
				},
				{ TransmissionType.AirToAir, TransmissionField.Altitude | TransmissionField.IsOnGround },
				{ TransmissionType.AllCallReply, TransmissionField.IsOnGround }
			};

		public static int GetIntField(string[] parts, MessageField field)
		{
			var str = GetField(parts, field);
			return str == null ? default : int.Parse(str);
		}

		public static int? GetIntField(string[] parts, TransmissionField field)
		{
			var str = GetField(parts, field);
			return str == null ? default : int.Parse(str);
		}

		public static bool GetBoolField(string[] parts, MessageField field)
		{
			return GetField(parts, field) == "-1";
		}

		public static bool? GetBoolField(string[] parts, TransmissionField field)
		{
			return GetField(parts, field) == "-1";
		}

		public static double? GetDoubleField(string[] parts, MessageField field)
		{
			var str = GetField(parts, field);
			return str == null ? default : double.Parse(str);
		}

		public static double? GetDoubleField(string[] parts, TransmissionField field)
		{
			var str = GetField(parts, field);
			return str == null ? default : double.Parse(str);
		}

		public static string GetField(string[] parts, MessageField field)
		{
			return GetField(parts, (int)field);
		}

		public static string GetField(string[] parts, TransmissionField field)
		{
			return GetField(parts, FieldOffsets[field]);
		}

		private static string GetField(string[] parts, int field)
		{
			var part = parts[field];
			return string.IsNullOrEmpty(part) ? null : part;
		}

		public static TransmissionField GetTransmissionFields(TransmissionType transmissionType)
		{
			return FieldMap[transmissionType];
		}
	}
}