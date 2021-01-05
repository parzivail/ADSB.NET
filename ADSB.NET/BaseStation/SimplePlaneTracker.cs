using System;
using System.Collections.Generic;
using System.Linq;

namespace ADSB.NET.BaseStation
{
	public class SimplePlaneTracker
	{
		public List<TrackedPlane> Planes { get; set; } = new List<TrackedPlane>();

		public void Consume(TelemetryMessage message)
		{
			var target = Planes.FirstOrDefault(plane => plane.HexIdent == message.HexId);

			if (target == null)
			{
				target = new TrackedPlane(message);
				Planes.Add(target);
			}

			switch (message.MessageType)
			{
				case MessageOpcode.NewId:
					target.Callsign = ((NewIdMessage)message).Callsign;
					break;
				case MessageOpcode.StatusChange:
					switch (((StatusChangeMessage)message).Status)
					{
						case TrackingStatus.PositionLost:
						case TrackingStatus.SignalLost:
						case TrackingStatus.Remove:
						case TrackingStatus.Delete:
							Planes.RemoveAll(plane => plane.HexIdent == message.HexId);
							break;
					}

					break;
				case MessageOpcode.TransmissionMessage:
					target.LoadMessage((TransmissionMessage)message);
					break;
			}
		}

		public void Heartbeat()
		{
			var now = DateTime.Now;
			var timeout = TimeSpan.FromSeconds(60);
			Planes.RemoveAll(plane => now - plane.LastMessage > timeout);
		}
	}
}