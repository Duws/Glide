using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class SoundManager : PersistentSingleton<SoundManager>
	{	
		public bool MusicOn=true;
		public bool SfxOn=true;
		[Range(0,1)]
		public float MusicVolume=0.3f;
		[Range(0,1)]
		public float SfxVolume=1f;

	    protected AudioSource _backgroundMusic;	
			
		public virtual void PlayBackgroundMusic(AudioSource Music)
		{
			if (!MusicOn)
				return;
			if (_backgroundMusic!=null)
				_backgroundMusic.Stop();
			_backgroundMusic=Music;
			_backgroundMusic.volume=MusicVolume;
			_backgroundMusic.loop=true;
			_backgroundMusic.Play();		
		}	

		public virtual AudioSource GetBackgroundMusic()
		{
			return _backgroundMusic;
		}
		
		public virtual AudioSource PlaySound(AudioClip Sfx, Vector3 Location)
		{
			if (!SfxOn)
				return null;
			GameObject temporaryAudioHost = new GameObject("TempAudio");
			temporaryAudioHost.transform.position = Location;
			AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource; 
			audioSource.clip = Sfx; 
			audioSource.volume = SfxVolume;
			audioSource.Play(); 
			Destroy(temporaryAudioHost, Sfx.length);
			return audioSource;
		}
	}
}