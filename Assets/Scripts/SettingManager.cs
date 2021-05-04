using UnityEngine;
using System.Xml.Serialization;

public class SettingManager
{
	[XmlAttribute("music")] public bool music;
	[XmlAttribute("tips")] public bool tips;

	private SettingManager() {}
	private static SettingManager[] container;
	public static string path = Application.persistentDataPath + "/settings.xml";

	public static ref SettingManager GetInstance()
	{
		if (container == null)
		{
			container = XMLHelper.Load<SettingManager>(path);
			if (container.Length == 0)
			{
				container = new SettingManager[1];
				container[0] = new SettingManager();
				container[0].music = true;
				container[0].tips = true;
			}
		}
		return ref container[0];
	}

	public void SaveSetting()
	{
		XMLHelper.Save<SettingManager>(ref container, path);
	}

	public void LoadSetting()
	{
		SettingManager[] tmp = XMLHelper.Load<SettingManager>(path);
		if (tmp.Length == 1)
		{
			container[0].music = tmp[0].music;
			container[0].tips = tmp[0].tips;
		}
	}

	public void ApplySetting()
	{
		AudioSource bgm = GlobalControl.Instance.GetComponent<AudioSource>();
		if (music == false)
		{
			bgm.Pause();
		}
		else if (!bgm.isPlaying)
		{
			bgm.Play();
		}
		SaveSetting();
	}

	public void MusicOn()
	{

	}

	public void MusicOff()
	{

	}
}
