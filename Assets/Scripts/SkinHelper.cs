using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Skinner
{
	[XmlAttribute("index")] public int index;
	[XmlAttribute("price")] public int price;
	[XmlAttribute("unlock")] public bool unlock;
	[XmlAttribute("equip")] public bool equip;

	public Skinner() {}

	public Skinner(int index, int price, bool unlock, bool equip)
	{
		this.index = index;
		this.price = price;
		this.unlock = unlock;
		this.equip = equip;
	}
}

public class SkinHelper
{
	/* parameters */
	public static string path = Application.persistentDataPath + "/skinner.xml";
	private const string filename = "Skins";

	private static SkinHelper singleton;
	private Sprite[] skins = null;
	private Skinner[] skinners = null;

	public static int[] prices = { 0, 2, 3, 3 };

	private SkinHelper() {}

	public static ref SkinHelper GetInstance()
	{
		if (singleton == null)
		{
			singleton = new SkinHelper();
		}
		return ref singleton;
	}

	public ref Sprite[] GetSkins()
	{
		if (skins == null)
		{
			Object[] res = Resources.LoadAll(filename, typeof(Sprite));
			skins = new Sprite[res.Length];
			for (int i=0; i<res.Length; i++)
			{
				skins[i] = (Sprite)res[i];
			}
		}
		return ref skins;
	}

	// must call after GetSkins() //
	public ref Skinner[] GetSkinners()
	{
		skinners = XMLHelper.Load<Skinner>(path);
		if (skinners.Length == 0)
		{
			skinners = new Skinner[prices.Length];
			for (int i=0; i<prices.Length; i++)
			{
				skinners[i] = new Skinner(i, prices[i], false, false);
			}
			skinners[0].unlock = true;
			skinners[0].equip = true;
		}
		return ref skinners;
	}

	public void SaveSkinners()
	{
		if (skinners != null)
		{
			XMLHelper.Save<Skinner>(ref skinners, path);
		}
	}

	public ref Sprite GetEquippedSkin()
	{
		GetSkins();
		
		if (skinners == null)
		{
			GetSkinners();
		}

		int i = 0;
		for (; i<skinners.Length; i++)
		{
			if (skinners[i].equip)
			{
				return ref skins[i];
			}
		}
		return ref skins[0];
	}
}