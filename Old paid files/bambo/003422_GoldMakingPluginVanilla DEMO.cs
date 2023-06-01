using System;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading;
using System.Xml.Serialization;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Plugin;
using wManager.Wow.Bot.States;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

using System.Windows.Forms;
using Math = robotManager.Helpful.Math;
using Timer = robotManager.Helpful.Timer;

public class Main : wManager.Plugin.IPlugin
{
    private static bool isLaunched;

    public void Initialize()
    {
		isLaunched = true;
		GMPVanillaDemoSettings.Load();
		robotManager.Helpful.Logging.Write("Gold Making Plugin Vanilla by Bambo is being loaded. DEMO");
        ClearDoNotSellList();
        EnableSelling();

        while(isLaunched & Products.IsStarted)
	{
		Thread.Sleep(500);
		Dispose();
	}
            
	}

    public void Dispose()
    {
    GMPVanillaDemoSettings.CurrentSetting.Save();
    isLaunched = false;
    }


    public void Settings()
    {
        GMPVanillaDemoSettings.Load();
        GMPVanillaDemoSettings.CurrentSetting.ToForm();
        GMPVanillaDemoSettings.CurrentSetting.Save();
    }


    public static void EnableSelling(){

        if (GMPVanillaDemoSettings.CurrentSetting.settingSelling)
            {

            wManager.wManagerSetting.CurrentSetting.Selling = true;
            wManager.wManagerSetting.CurrentSetting.SellGreen = true;
            wManager.wManagerSetting.CurrentSetting.SellWhite = true;
            wManager.wManagerSetting.CurrentSetting.SellGray = true;

            }


        
    }

    public void ClearDoNotSellList()
    {
        
        if (GMPVanillaDemoSettings.CurrentSetting.farmCopperOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Copper Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Copper Ore");
            }

        if (GMPVanillaDemoSettings.CurrentSetting.farmTinOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tin Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Tin Ore");
            }

        if (GMPVanillaDemoSettings.CurrentSetting.farmPeacebloom == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Peacebloom"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Peacebloom");
            }

        if (GMPVanillaDemoSettings.CurrentSetting.farmEarthroot == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Earthroot"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Earthroot");
            }
        if (GMPVanillaDemoSettings.CurrentSetting.farmSilverleaf == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silverleaf"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Silverleaf");
            }
        if (GMPVanillaDemoSettings.CurrentSetting.farmRuinedLeatherScraps == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ruined Leather Scraps"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Ruined Leather Scraps");
            }
        if (GMPVanillaDemoSettings.CurrentSetting.farmLightLeather == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Light Leather"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Light Leather");
            }
        if (GMPVanillaDemoSettings.CurrentSetting.farmLinenCloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Linen Cloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Linen Cloth");
            }
        if (GMPVanillaDemoSettings.CurrentSetting.farmWoolCloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wool Cloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Wool Cloth");
            }

    robotManager.Helpful.Logging.Write("[Bambo_GMPVanillaDemo_Plugin]: Do Not Sell list updated");

        
    }




    
}
// More info here: http://wrobot.eu/topic/374-how-to-create-quest-profile/ (please don't add using and add only one quest)












[Serializable] 
public class GMPVanillaDemoSettings : Settings
{
    public GMPVanillaDemoSettings()
    {
        settingSelling = true;

        farmCopperOre = 0;
        farmTinOre = 0;
        farmPeacebloom = 0;
        farmSilverleaf = 0;
        farmEarthroot = 0;
        farmRuinedLeatherScraps = 0;
        farmLightLeather = 0;
        farmLinenCloth = 0;
        farmWoolCloth = 0;
        farmRawGold30 = 0;















    }

    public static GMPVanillaDemoSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("GMPVanillaDemo", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("GMPVanillaDemoSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("GMPVanillaDemo", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<GMPVanillaDemoSettings>(AdviserFilePathAndName("GMPVanillaDemo",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new GMPVanillaDemoSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("GMPVanillaDemo => Load(): " + e);
        }
        return false;
    }


	// Settings

	[Category("00) Settings")]
    [DisplayName("01) Force Enable Selling")]
    [Description("If you set this to true, the bot will always be allowed to sell grey, white and green items.")]
    public bool settingSelling { get; set; }



	// Mining
    [Category("01) Mining Tasks")]
    [DisplayName("01) Copper Ore")]
    [Description("Requires: Mining Skill on 1")]
    public int farmCopperOre { get; set; }	

    [Category("01) Mining Tasks")]
    [DisplayName("02) Tin Ore")]
    [Description("Requires: Mining Skill on 65")]
    public int farmTinOre { get; set; }	

	
	// Herbalism
    [Category("02) Herbalism Tasks")]
    [DisplayName("01) Peacebloom")]
    [Description("Requires: Herbalism Skill on 1")]
    public int farmPeacebloom { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("02) Silverleaf")]
    [Description("Requires: Herbalism Skill on 1")]
    public int farmSilverleaf { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("03) Earthroot")]
    [Description("Requires: Herbalism Skill on 15")]
    public int farmEarthroot { get; set; }	
	
	// Skinning
    [Category("03) Skinning Tasks")]
    [DisplayName("01) Ruined Leather Scraps")]
    [Description("Requires: Skinning Skill on 1")]
    public int farmRuinedLeatherScraps { get; set; }	

    [Category("03) Skinning Tasks")]
    [DisplayName("02) Light Leather")]
    [Description("Requires: Skinning Skill on 70")]
    public int farmLightLeather { get; set; }	

	
    [Category("04) Cloth Tasks")]
    [DisplayName("01) Linen Cloth")]
    [Description("Requires: Level 15")]
    public int farmLinenCloth { get; set; }		

    [Category("04) Cloth Tasks")]
    [DisplayName("02) Wool Cloth")]
    [Description("Requires: Level 25")]
    public int farmWoolCloth { get; set; }	

  

	// RawGold
    [Category("05) Raw Gold Tasks")]
    [DisplayName("01) LVL30+ Raw Gold")]
    [Description("Set to amount of gold you want it to stop on. Requires: Character Level at 30")]
    public int farmRawGold30 { get; set; }	

}