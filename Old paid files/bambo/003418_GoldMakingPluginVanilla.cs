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
		GMPVanillaSettings.Load();
		robotManager.Helpful.Logging.Write("Gold Making Plugin Vanilla by Bambo is being loaded. V1.0.0");
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
    GMPVanillaSettings.CurrentSetting.Save();
    isLaunched = false;
    }


    public void Settings()
    {
        GMPVanillaSettings.Load();
        GMPVanillaSettings.CurrentSetting.ToForm();
        GMPVanillaSettings.CurrentSetting.Save();
    }


    public static void EnableSelling(){

        if (GMPVanillaSettings.CurrentSetting.settingSelling)
            {

            wManager.wManagerSetting.CurrentSetting.Selling = true;
            wManager.wManagerSetting.CurrentSetting.SellGreen = true;
            wManager.wManagerSetting.CurrentSetting.SellWhite = true;
            wManager.wManagerSetting.CurrentSetting.SellGray = true;

            }


        
    }

    public void ClearDoNotSellList()
    {
        
        if (GMPVanillaSettings.CurrentSetting.farmCopperOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Copper Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Copper Ore");
            }

        if (GMPVanillaSettings.CurrentSetting.farmTinOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tin Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Tin Ore");
            }

        if (GMPVanillaSettings.CurrentSetting.farmSilverOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silver Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Silver Ore");
            }

        if (GMPVanillaSettings.CurrentSetting.farmGoldOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gold Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Gold Ore");
            }

        if (GMPVanillaSettings.CurrentSetting.farmIronOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Iron Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Iron Ore");
            }

        if (GMPVanillaSettings.CurrentSetting.farmMithrilOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mithril Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Mithril Ore");
            }
        if (GMPVanillaSettings.CurrentSetting.farmThoriumOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thorium Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Thorium Ore");
            }
        if (GMPVanillaSettings.CurrentSetting.farmTruesilverOre == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Truesilver Ore"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Truesilver Ore");
            }

        if (GMPVanillaSettings.CurrentSetting.farmPeacebloom == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Peacebloom"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Peacebloom");
            }

        if (GMPVanillaSettings.CurrentSetting.farmEarthroot == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Earthroot"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Earthroot");
            }
        if (GMPVanillaSettings.CurrentSetting.farmSilverleaf == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silverleaf"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Silverleaf");
            }
        if (GMPVanillaSettings.CurrentSetting.farmMageroyal == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mageroyal"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Mageroyal");
            }
        if (GMPVanillaSettings.CurrentSetting.farmSwiftthistle == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Swiftthistle"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Swiftthistle");
            }
        if (GMPVanillaSettings.CurrentSetting.farmBriarthorn == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Briarthorn"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Briarthorn");
            }
        if (GMPVanillaSettings.CurrentSetting.farmStranglekelp == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Stranglekelp"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Stranglekelp");
            }

        if (GMPVanillaSettings.CurrentSetting.farmBruiseweed == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Bruiseweed"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Bruiseweed");
            }
        if (GMPVanillaSettings.CurrentSetting.farmWildSteelbloom == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wild Steelbloom"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Wild Steelbloom");
            }
        if (GMPVanillaSettings.CurrentSetting.farmGraveMoss == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Grave Moss"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Grave Moss");
            }
        if (GMPVanillaSettings.CurrentSetting.farmKingsblood == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Kingsblood"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Kingsblood");
            }
        if (GMPVanillaSettings.CurrentSetting.farmLiferoot == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Liferoot"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Liferoot");
            }
        if (GMPVanillaSettings.CurrentSetting.farmFadeleaf == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Fadeleaf"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Fadeleaf");
            }
        if (GMPVanillaSettings.CurrentSetting.farmGoldthorn == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Goldthorn"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Goldthorn");
            }
        if (GMPVanillaSettings.CurrentSetting.farmKhadgarsWhisker == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Khadgar's Whisker"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Khadgar's Whisker");
            }
        if (GMPVanillaSettings.CurrentSetting.farmWintersbite == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wintersbite"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Wintersbite");
            }
        if (GMPVanillaSettings.CurrentSetting.farmFirebloom == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Firebloom"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Firebloom");
            }
        if (GMPVanillaSettings.CurrentSetting.farmPurpleLotus == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Purple Lotus"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Purple Lotus");
            }
        if (GMPVanillaSettings.CurrentSetting.farmWildvine == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wildvine"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Wildvine");
            }
        if (GMPVanillaSettings.CurrentSetting.farmArthasTears == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Arthas' Tears"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Arthas' Tears");
            }
        if (GMPVanillaSettings.CurrentSetting.farmGhostMushroom == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("GhostMushroom"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("GhostMushroom");
            }
        if (GMPVanillaSettings.CurrentSetting.farmGromsblood == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gromsblood"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Gromsblood");
            }
        if (GMPVanillaSettings.CurrentSetting.farmGoldenSansam == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("GoldenSansam"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("GoldenSansam");
            }
        if (GMPVanillaSettings.CurrentSetting.farmDreamfoil == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Dreamfoil"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Dreamfoil");
            }
        if (GMPVanillaSettings.CurrentSetting.farmMountainSilversage == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mountain Silversage"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Mountain Silversage");
            }
        if (GMPVanillaSettings.CurrentSetting.farmPlaguebloom == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plaguebloom"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Plaguebloom");
            }
        if (GMPVanillaSettings.CurrentSetting.farmIcecap == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Icecap"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Icecap");
            }
        if (GMPVanillaSettings.CurrentSetting.farmBlackLotus == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Black Lotus"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Black Lotus");
            }
        if (GMPVanillaSettings.CurrentSetting.farmRuinedLeatherScraps == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ruined Leather Scraps"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Ruined Leather Scraps");
            }
        if (GMPVanillaSettings.CurrentSetting.farmLightLeather == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Light Leather"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Light Leather");
            }
        if (GMPVanillaSettings.CurrentSetting.farmMediumLeather == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Medium Leather"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Medium Leather");
            }
        if (GMPVanillaSettings.CurrentSetting.farmHeavyLeather == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Heavy Leather"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Heavy Leather");
            }
        if (GMPVanillaSettings.CurrentSetting.farmRuggedLeather == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Rugged Leather"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Rugged Leather");
            }
        if (GMPVanillaSettings.CurrentSetting.farmThickLeather == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thick Leather"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Thick Leather");
            }
        if (GMPVanillaSettings.CurrentSetting.farmLinenCloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Linen Cloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Linen Cloth");
            }
        if (GMPVanillaSettings.CurrentSetting.farmWoolCloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wool Cloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Wool Cloth");
            }
        if (GMPVanillaSettings.CurrentSetting.farmSilkCloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silk Cloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Silk Cloth");
            }
        if (GMPVanillaSettings.CurrentSetting.farmMageweaveCloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mageweave Cloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Mageweave Cloth");
            }
        if (GMPVanillaSettings.CurrentSetting.farmRunecloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Runecloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Runecloth");
            }
        if (GMPVanillaSettings.CurrentSetting.farmFelcloth == 0 && wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Felcloth"))
            {
            wManager.wManagerSetting.CurrentSetting.DoNotSellList.Remove("Felcloth");
            }

    robotManager.Helpful.Logging.Write("[Bambo_GMPVanilla_Plugin]: Do Not Sell list updated");

        
    }




    
}
// More info here: http://wrobot.eu/topic/374-how-to-create-quest-profile/ (please don't add using and add only one quest)












[Serializable] 
public class GMPVanillaSettings : Settings
{
    public GMPVanillaSettings()
    {
        settingSelling = true;
        settingMultiProduct = false;

        farmCopperOre = 0;
        farmTinOre = 0;
        farmSilverOre = 0;
        farmIronOre = 0;
        farmGoldOre = 0;
        farmMithrilOre = 0;
        farmTruesilverOre = 0;
        farmThoriumOre = 0;
        farmPeacebloom = 0;
        farmSilverleaf = 0;
        farmEarthroot = 0;
        farmMageroyal = 0;
        farmSwiftthistle = 0;
        farmBriarthorn = 0;
        farmStranglekelp = 0;
        farmBruiseweed = 0;
        farmWildSteelbloom = 0;
        farmGraveMoss = 0;
        farmKingsblood = 0;
        farmLiferoot = 0;
        farmFadeleaf = 0;
        farmGoldthorn = 0;
        farmKhadgarsWhisker = 0;
        farmWintersbite = 0;
        farmFirebloom = 0;
        farmPurpleLotus = 0;
        farmWildvine = 0;
        farmArthasTears = 0;
        farmGhostMushroom = 0;
        farmGromsblood = 0;
        farmGoldenSansam = 0;
        farmDreamfoil = 0;
        farmMountainSilversage = 0;
        farmPlaguebloom = 0;
        farmIcecap = 0;
        farmBlackLotus = 0;
        farmRuinedLeatherScraps = 0;
        farmLightLeather = 0;
        farmMediumLeather = 0;
        farmHeavyLeather = 0;
        farmThickLeather = 0;
        farmRuggedLeather = 0;
        farmLinenCloth = 0;
        farmWoolCloth = 0;
        farmSilkCloth = 0;
        farmMageweaveCloth = 0;
        farmRunecloth = 0;
        farmFelcloth = 0;
        farmRawGold30 = 0;
        farmRawGold40 = 0;
        farmRawGold50 = 0;
        farmRawGold60 = 0;
        farmBOE60 = false;















    }

    public static GMPVanillaSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("GMPVanilla", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("GMPVanillaSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("GMPVanilla", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<GMPVanillaSettings>(AdviserFilePathAndName("GMPVanilla",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new GMPVanillaSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("GMPVanilla => Load(): " + e);
        }
        return false;
    }


	// Settings
	[Category("00) Settings")]
    [DisplayName("00) Multi IP")]
    [Description("If you set this to true, you need to use an order id with Multi IP Status")]
    public bool settingMultiProduct { get; set; }

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

    [Category("01) Mining Tasks")]
    [DisplayName("03) Silver Ore")]
    [Description("Requires: Mining Skill on 75")]
    public int farmSilverOre { get; set; }	

    [Category("01) Mining Tasks")]
    [DisplayName("04) Iron Ore")]
    [Description("Requires: Mining Skill on 125")]
    public int farmIronOre { get; set; }	

    [Category("01) Mining Tasks")]
    [DisplayName("05) Gold Ore")]
    [Description("Requires: Mining Skill on 155")]
    public int farmGoldOre { get; set; }
	
    [Category("01) Mining Tasks")]
    [DisplayName("06) Mithril Ore")]
    [Description("Requires: Mining Skill on 175")]
    public int farmMithrilOre { get; set; }	
	
    [Category("01) Mining Tasks")]
    [DisplayName("07) Truesilver Ore")]
    [Description("Requires: Mining Skill on 230")]
    public int farmTruesilverOre { get; set; }	

    [Category("01) Mining Tasks")]
    [DisplayName("08) Thorium Ore")]
    [Description("Requires: Mining Skill on 270")]
    public int farmThoriumOre { get; set; }
	
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

    [Category("02) Herbalism Tasks")]
    [DisplayName("04) Mageroyal")]
    [Description("Requires: Herbalism Skill on 50")]
    public int farmMageroyal { get; set; }

    [Category("02) Herbalism Tasks")]
    [DisplayName("05) Swiftthistle")]
    [Description("Requires: Herbalism Skill on 70")]
    public int farmSwiftthistle { get; set; }

    [Category("02) Herbalism Tasks")]
    [DisplayName("06) Briarthorn")]
    [Description("Requires: Herbalism Skill on 70")]
    public int farmBriarthorn { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("07) Stranglekelp")]
    [Description("Requires: Herbalism Skill on 85")]
    public int farmStranglekelp { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("08) Bruiseweed")]
    [Description("Requires: Herbalism Skill on 100")]
    public int farmBruiseweed { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("09) Wild Steelbloom")]
    [Description("Requires: Herbalism Skill on 115")]
    public int farmWildSteelbloom { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("10) Grave Moss")]
    [Description("Requires: Herbalism Skill on 120")]
    public int farmGraveMoss { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("11) Kingsblood")]
    [Description("Requires: Herbalism Skill on 125")]
    public int farmKingsblood { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("12) Liferoot")]
    [Description("Requires: Herbalism Skill on 150")]
    public int farmLiferoot { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("13) Fadeleaf")]
    [Description("Requires: Herbalism Skill on 160 and Location: Eastern Kingdoms")]
    public int farmFadeleaf { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("14) Goldthorn")]
    [Description("Requires: Herbalism Skill on 170")]
    public int farmGoldthorn { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("15) Khadgars Whisker")]
    [Description("Requires: Herbalism Skill on 185 and Location: Eastern Kingdoms")]
    public int farmKhadgarsWhisker { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("16) Wintersbite")]
    [Description("Requires: Herbalism Skill on 195 and Location: Eastern Kingdoms")]
    public int farmWintersbite { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("17) Firebloom")]
    [Description("Requires: Herbalism Skill on 205")]
    public int farmFirebloom { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("18) Purple Lotus")]
    [Description("Requires: Herbalism Skill on 210 and Location: Kalimdor")]
    public int farmPurpleLotus	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("19) Wildvine")]
    [Description("Requires: Herbalism Skill on 210 and Location: Kalimdor")]
    public int farmWildvine	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("20) Arthas' Tears")]
    [Description("Requires: Herbalism Skill on 220")]
    public int farmArthasTears	 { get; set; }	


    [Category("02) Herbalism Tasks")]
    [DisplayName("21) Ghost Mushroom")]
    [Description("Requires: Herbalism Skill on 245")]
    public int farmGhostMushroom	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("22) Gromsblood")]
    [Description("Requires: Herbalism Skill on 250")]
    public int farmGromsblood	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("23) Golden Sansam")]
    [Description("Requires: Herbalism Skill on 260")]
    public int farmGoldenSansam	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("24) Dreamfoil")]
    [Description("Requires: Herbalism Skill on 270")]
    public int farmDreamfoil	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("25) Mountain Silversage")]
    [Description("Requires: Herbalism Skill on 280 and Location: Kalimdor")]
    public int farmMountainSilversage	 { get; set; }	

    [Category("02) Herbalism Tasks")]
    [DisplayName("26) Plaguebloom")]
    [Description("Requires: Herbalism Skill on 285")]
    public int farmPlaguebloom	 { get; set; }	    

    [Category("02) Herbalism Tasks")]
    [DisplayName("27) Icecap")]
    [Description("Requires: Herbalism Skill on 290 and Location: Kalimdor")]
    public int farmIcecap	 { get; set; }	 

    [Category("02) Herbalism Tasks")]
    [DisplayName("28) Black Lotus")]
    [Description("Requires: Herbalism Skill on 300")]
    public int farmBlackLotus	 { get; set; }	 
	
	// Skinning
    [Category("03) Skinning Tasks")]
    [DisplayName("01) Ruined Leather Scraps")]
    [Description("Requires: Skinning Skill on 1")]
    public int farmRuinedLeatherScraps { get; set; }	

    [Category("03) Skinning Tasks")]
    [DisplayName("02) Light Leather")]
    [Description("Requires: Skinning Skill on 70")]
    public int farmLightLeather { get; set; }	

    [Category("03) Skinning Tasks")]
    [DisplayName("03) Medium Leather")]
    [Description("Requires: Skinning Skill on 140")]
    public int farmMediumLeather { get; set; }	

    [Category("03) Skinning Tasks")]
    [DisplayName("04) Heavy Leather")]
    [Description("Requires: Skinning Skill on 170")]
    public int farmHeavyLeather { get; set; }	

    [Category("03) Skinning Tasks")]
    [DisplayName("05) Thick Leather")]
    [Description("Requires: Skinning Skill on 230")]
    public int farmThickLeather { get; set; }	

    [Category("03) Skinning Tasks")]
    [DisplayName("06) Rugged Leather")]
    [Description("Requires: Skinning Skill on 280")]
    public int farmRuggedLeather { get; set; }	
	
    [Category("04) Cloth Tasks")]
    [DisplayName("01) Linen Cloth")]
    [Description("Requires: Level 15")]
    public int farmLinenCloth { get; set; }		

    [Category("04) Cloth Tasks")]
    [DisplayName("02) Wool Cloth")]
    [Description("Requires: Level 25")]
    public int farmWoolCloth { get; set; }	

    [Category("04) Cloth Tasks")]
    [DisplayName("03) Silk Cloth")]
    [Description("Requires: Level 35")]
    public int farmSilkCloth { get; set; }	

    [Category("04) Cloth Tasks")]
    [DisplayName("04) Mageweave Cloth")]
    [Description("Requires: Level 45")]
    public int farmMageweaveCloth { get; set; }	

    [Category("04) Cloth Tasks")]
    [DisplayName("05) Runecloth")]
    [Description("Requires: Level 55")]
    public int farmRunecloth { get; set; }	

    [Category("04) Cloth Tasks")]
    [DisplayName("06) Felcloth")]
    [Description("Requires: Level 60 and position on Kalimdor")]
    public int farmFelcloth { get; set; }	

	// RawGold
    [Category("05) Raw Gold Tasks")]
    [DisplayName("01) LVL30+ Raw Gold")]
    [Description("Set to amount of gold you want it to stop on. Requires: Character Level at 30")]
    public int farmRawGold30 { get; set; }	

    [Category("05) Raw Gold Tasks")]
    [DisplayName("02) LVL40+ Raw Gold")]
    [Description("Set to amount of gold you want it to stop on. Requires: Character Level at 40")]
    public int farmRawGold40 { get; set; }	

    [Category("05) Raw Gold Tasks")]
    [DisplayName("03) LVL50+ Raw Gold")]
    [Description("Set to amount of gold you want it to stop on. Requires: Character Level at 50")]
    public int farmRawGold50 { get; set; }	

    [Category("05) Raw Gold Tasks")]
    [DisplayName("04) LVL60 Raw Gold")]
    [Description("Set to amount of gold you want it to stop on. Requires: Character Level at 60")]
    public int farmRawGold60 { get; set; }	
	
	// BOEs
    [Category("06) BOE Tasks BETA")]
    [DisplayName("01) LVL 60 BOE Farm")]
    [Description("Requires: Character Level at 60")]
    public bool farmBOE60 { get; set; }	
	
}