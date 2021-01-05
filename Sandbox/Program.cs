using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ADSB.NET.BaseStation;

namespace Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new TcpClient();
			client.Connect("192.168.191.99", 30003);

			var stream = client.GetStream();

			var spt = new SimplePlaneTracker();

			var sle = new StreamLineEmitter(stream);
			sle.Line += (sender, s) =>
			{
				var message = BaseStation.Parse(s);
				spt.Consume(message);
				spt.Heartbeat();
				Print(spt);
			};

			sle.Start();

			var mres = new ManualResetEventSlim();
			mres.Wait();
		}

		private static void Print(SimplePlaneTracker spt)
		{
			Console.SetCursorPosition(0, 0);
			Console.WriteLine($"{"Hex",-7}{"Call",-8}{"Lat",-9}{"Long",-9}{"Since",-9}");

			var now = DateTime.Now;

			foreach (var plane in spt.Planes)
				Console.WriteLine(
					$"{plane.HexIdent,-7}{plane.Callsign,-8}{plane.Latitude,-9}{plane.Longitude,-9}{Math.Floor((now - plane.LastMessage).TotalSeconds),9}");
		}
	}

	class StreamLineEmitter
	{
		private readonly StringBuilder _stringBuilder = new StringBuilder();
		private readonly Stream _stream;
		private readonly BinaryReader _reader;

		private bool _firstFlush = true;
		private bool _running;

		public event EventHandler<string> Line;

		public StreamLineEmitter(Stream stream)
		{
			_stream = stream;
			_reader = new BinaryReader(_stream);
		}

		public void Start()
		{
			_running = true;
			_stream.BeginRead(Array.Empty<byte>(), 0, 0, ReadCallback, null);
		}

		private void ReadCallback(IAsyncResult ar)
		{
			_stream.EndRead(ar);

			while (true)
			{
				var c = _reader.ReadChar();
				if (c == '\n')
				{
					if (!_firstFlush)
						Line?.Invoke(this, _stringBuilder.ToString());
					else
						_firstFlush = false;

					_stringBuilder.Clear();
					break;
				}

				if (c == '\r')
					continue;

				_stringBuilder.Append(c);
			}

			if (_running)
				_stream.BeginRead(Array.Empty<byte>(), 0, 0, ReadCallback, null);
		}

		public void Stop()
		{
			_running = false;
		}
	}
}