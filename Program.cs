using System;

namespace AdapterPattern
{
	class Program
	{
		static void Main(string[] args)
		{
			var player = new AudioPlayer();
			player.Play(AudioType.Vlc, "c:\a.vlc");
			player.Play(AudioType.Mp4, "c:\a.mp4");
			player.Play(AudioType.Other, "c:\a.mp3");
		}
	}

	public interface IMediaPlayer
	{
		void Play(AudioType audioType, string fileName);
	}

	public interface IMp4PLayer
	{
		void PlayMp4(string fileName);
	}

	public interface IVlcPlayer
	{
		void PlayVlc(string fileName);
	}

	public class AudioPlayer : IMediaPlayer
	{
		private readonly MediaAdapter _adapter = new MediaAdapter();
		public void Play(AudioType audioType, string fileName)
		{
			_adapter.Play(audioType, fileName);	
		}
	}

	public class Mp4Player : IMp4PLayer
	{
		public void PlayMp4(string fileName)
		{
			Console.WriteLine("Playing MP4...");
		}
	}

	public class VlcPlayer : IVlcPlayer
	{
		public void PlayVlc(string fileName)
		{
			Console.WriteLine("Playing VLC...");
		}
	}

	public class MediaAdapter : IMediaPlayer
	{
		private IMp4PLayer _mp4Player = null;
		private IVlcPlayer _vlcPlayer = null;

		public void Play(AudioType audioType, string fileName)
		{
			if (audioType == AudioType.Mp4)
			{
				_mp4Player = new Mp4Player();
				_mp4Player.PlayMp4(fileName);
			}
			else if(audioType == AudioType.Vlc)
			{
				 _vlcPlayer = new VlcPlayer();
				_vlcPlayer.PlayVlc(fileName);
			}
			else
			{
				Console.WriteLine("Playing other format...");
			}
		}
	}

	[Flags]
	public enum AudioType
	{
		Mp4,
		Vlc,
		Other
	}
}
