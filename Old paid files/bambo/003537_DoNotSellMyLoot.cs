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


    public void Initialize()
    {
		
		DoNotSellMyLootSettings.Load();
		robotManager.Helpful.Logging.Write("DoNotSellMyLoot v1.0.5 by Bambo is being loaded...");



     try
	 	{

if (DoNotSellMyLootSettings.CurrentSetting.settingForceSelling == true)	

		{
		wManager.wManagerSetting.CurrentSetting.Selling = true;
		}

if (DoNotSellMyLootSettings.CurrentSetting.settingForceSellingGrey == true)	

		{
		wManager.wManagerSetting.CurrentSetting.SellGray = true;
		}

if (DoNotSellMyLootSettings.CurrentSetting.settingForceSellingWhite == true)	

		{
		wManager.wManagerSetting.CurrentSetting.SellWhite = true;
		}

if (DoNotSellMyLootSettings.CurrentSetting.settingForceSellingGreen == true)	

		{
		wManager.wManagerSetting.CurrentSetting.SellGreen = true;
		}



		        //ORES
        if (DoNotSellMyLootSettings.CurrentSetting.sellCheapOres == false)
            {
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Copper Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Copper Ore");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tin Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tin Ore");
	    	    }

            }  

        if (DoNotSellMyLootSettings.CurrentSetting.sellExpensiveOres == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Iron Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Iron Ore");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silver Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Silver Ore");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gold Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Gold Ore");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mithril Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Mithril Ore");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thorium Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thorium Ore");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Truesilver Ore"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Truesilver Ore");
	    	    }

            }

        if (DoNotSellMyLootSettings.CurrentSetting.sellQuestOres == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Umbral"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Umbral");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Elunite"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Elunite");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Rethban"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Rethban");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Incendicite"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Incendicite");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lesser Bloodstone"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lesser Bloodstone");
	    	    }

                if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Indurium"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Indurium");
	    	    }

            }



//HERBS

        if (DoNotSellMyLootSettings.CurrentSetting.sellCheapHerbs == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Peacebloom"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Peacebloom");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silverleaf"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Silverleaf");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Earthroot"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Earthroot");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mageroyal"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Mageroyal");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Bruiseweed"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Bruiseweed");
	    	    }

            }

        if (DoNotSellMyLootSettings.CurrentSetting.sellExpensiveHerbs == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Swiftthistle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Swiftthistle");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Briarthorn"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Briarthorn");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Stranglekelp"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Stranglekelp");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wild Steelbloom"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Wild Steelbloom");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Grave Moss"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Grave Moss");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Kingsblood"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Kingsblood");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Liferoot"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Liferoot");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Fadeleaf"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Fadeleaf");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Goldthorn"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Goldthorn");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Khadgar's Whisker"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Khadgar's Whisker");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wintersbite"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Wintersbite");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Firebloom"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Firebloom");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Purple Lotus"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Purple Lotus");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wildvine"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Wildvine");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Arthas' Tears"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Arthas' Tears");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Sungrass"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Sungrass");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Blindweed"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Blindweed");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ghost Mushroom"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Ghost Mushroom");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gromsblood"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Gromsblood");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Golden Sansam"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Golden Sansam");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Dreamfoil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Dreamfoil");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mountain Silversage"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Mountain Silversage");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plaguebloom"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plaguebloom");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Icecap"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Icecap");
	    	    }

			}
			

        if (DoNotSellMyLootSettings.CurrentSetting.sellBlackLotusHerbs == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Black Lotus"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Black Lotus");
	    	    }

            }

        //Leather

        if (DoNotSellMyLootSettings.CurrentSetting.sellCheapLeather == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Light Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Light Leather");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ruined Leather Scraps"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Ruined Leather Scraps");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Medium Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Medium Leather");
	    	    }
	    

            }


        if (DoNotSellMyLootSettings.CurrentSetting.sellExpensiveLeather == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Heavy Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Heavy Leather");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Rugged Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Rugged Leather");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thick Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thick Leather");
	    	    }
	    

            }

        if (DoNotSellMyLootSettings.CurrentSetting.sellSpecialLeather == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thin Kodo"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thin Kodo");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Devilsaur Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Devilsaur Leather");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Warbear Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Warbear Leather");
	    	    }


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Primal Bat Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Primal Bat Leather");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Primal Tiger Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Primal Tiger Leather");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Core Leather"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Core Leather");
	    	    }
	    

            }
		

  if (DoNotSellMyLootSettings.CurrentSetting.sellHidesLeather == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Light Hide"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Light Hide");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Medium Hide"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Medium Hide");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Heavy Hide"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Heavy Hide");
	    	    }


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thick Hide"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thick Hide");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Rugged Hide"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Rugged Hide");
	    	    }
	    

            }

  if (DoNotSellMyLootSettings.CurrentSetting.sellCheapCloth == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Linen Cloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Linen Cloth");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Wool Cloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Wool Cloth");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Silk Cloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Silk Cloth");
	    	    }


            }

		

  if (DoNotSellMyLootSettings.CurrentSetting.sellExpensiveCloth == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mageweave Cloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Mageweave Cloth");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Runecloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Runecloth");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Felcloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Felcloth");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mooncloth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Mooncloth");
	    	    }


            }

  if (DoNotSellMyLootSettings.CurrentSetting.sellBags6and8 == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Kodo Hide Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Kodo Hide Bag");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Linen Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Linen Bag");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Red Linen Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Red Linen Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Small Black Pouch"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Small Black Pouch");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Small Red Pouch"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Small Red Pouch");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Small Blue Pouch"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Small Blue Pouch");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Small Green Pouch"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Small Green Pouch");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Small Brown Pouch"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Small Brown Pouch");
	    	    }


            }







  if (DoNotSellMyLootSettings.CurrentSetting.sellBags10and12 == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Black Silk Pack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Black Silk Pack");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Green Silk Pack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Green Silk Pack");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Small Silk Pack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Small Silk Pack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Fel Steed Saddlebags"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Fel Steed Saddlebags");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Large Blue Sack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Large Blue Sack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Large Brown Sack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Large Brown Sack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Large Green Sack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Large Green Sack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Large Red Sack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Large Red Sack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Large Rucksack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Large Rucksack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Murloc Skin Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Murloc Skin Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Snakeskin Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Snakeskin Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Heavy Brown Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Heavy Brown Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Mageweave Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Mageweave Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Red Mageweave Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Red Mageweave Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Large Knapsack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Large Knapsack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Sturdy Lunchbox"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Sturdy Lunchbox");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Huge Brown Sack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Huge Brown Sack");
	    	    }


            }

  if (DoNotSellMyLootSettings.CurrentSetting.sellBags14 == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Runecloth Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Runecloth Bag");
	    	    }
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Journeyman's Backpack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Journeyman's Backpack");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Toll-hide Bag"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Toll-hide Bag");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Explorer's Knapsack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Explorer's Knapsack");
	    	    }

				if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thawpelt Sack"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thawpelt Sack");
	    	    }


            }


  if (DoNotSellMyLootSettings.CurrentSetting.sellRingsMonkey == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Onyx Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Onyx Ring of the Monkey");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Opal Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Opal Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Vermilion Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Vermilion Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Granite Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Granite Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Frigid Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Frigid Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jungle Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jungle Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Peridot Circle of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Peridot Circle of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Multicolored Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Multicolored Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Hardened Stone Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Hardened Stone Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Quicksilver Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Quicksilver Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jasper Link of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jasper Link of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Obsidian Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Obsidian Band of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Swamp Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Swamp Ring of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Selenium Loop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Selenium Loop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Serpentine Loop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Serpentine Loop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Marble Circle of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Marble Circle of the Monkey");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Arctic Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Arctic Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Sardonyx Knuckle of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Sardonyx Knuckle of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Vanadium Loop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Vanadium Loop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Onyx Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Onyx Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Desert Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Desert Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Topaz Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Topaz Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tellurium Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tellurium Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Aquamarine Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Aquamarine Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lodestone Hoop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lodestone Hoop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Marsh Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Marsh Ring of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Hematite Link of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Hematite Link of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Iridium Circle of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Iridium Circle of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jet Loop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jet Loop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Forest Hoop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Forest Hoop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Carnelian Loop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Carnelian Loop of the Monkey");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thallium Hoop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thallium Hoop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Greenstone Circle of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Greenstone Circle of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Amethyst Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Amethyst Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Fen Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Fen Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Cerulean Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Cerulean Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Basalt Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Basalt Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Spinel Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Spinel Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tundra Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tundra Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gnomeregan Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Gnomeregan Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Cobalt Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Cobalt Ring of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jacinth Circle of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jacinth Circle of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ivory Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Ivory Band of the Monkey");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Savannah Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Savannah Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Amber Hoop of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Amber Hoop of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Chrome Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Chrome Ring of the Monkey");
	    	    }	
            

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Coral Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Coral Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Prairie Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Prairie Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Viridian Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Viridian Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Zircon Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Zircon Band of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Clay Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Clay Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Meadow Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Meadow Ring of the Monkey");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lead Band of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lead Band of the Monkey");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Quartz Ring of the Monkey"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Quartz Ring of the Monkey");
	    	    }	



            }


if (DoNotSellMyLootSettings.CurrentSetting.sellRingsOwl == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Onyx Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Onyx Ring of the Owl");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Opal Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Opal Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Vermilion Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Vermilion Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Granite Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Granite Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Frigid Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Frigid Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jungle Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jungle Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Peridot Circle of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Peridot Circle of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Multicolored Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Multicolored Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Hardened Stone Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Hardened Stone Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Quicksilver Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Quicksilver Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jasper Link of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jasper Link of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Obsidian Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Obsidian Band of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Swamp Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Swamp Ring of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Selenium Loop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Selenium Loop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Serpentine Loop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Serpentine Loop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Marble Circle of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Marble Circle of the Owl");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Arctic Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Arctic Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Sardonyx Knuckle of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Sardonyx Knuckle of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Vanadium Loop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Vanadium Loop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Onyx Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Onyx Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Desert Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Desert Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Topaz Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Topaz Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tellurium Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tellurium Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Aquamarine Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Aquamarine Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lodestone Hoop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lodestone Hoop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Marsh Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Marsh Ring of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Hematite Link of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Hematite Link of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Iridium Circle of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Iridium Circle of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jet Loop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jet Loop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Forest Hoop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Forest Hoop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Carnelian Loop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Carnelian Loop of the Owl");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thallium Hoop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thallium Hoop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Greenstone Circle of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Greenstone Circle of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Amethyst Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Amethyst Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Fen Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Fen Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Cerulean Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Cerulean Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Basalt Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Basalt Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Spinel Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Spinel Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tundra Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tundra Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gnomeregan Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Gnomeregan Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Cobalt Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Cobalt Ring of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jacinth Circle of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jacinth Circle of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ivory Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Ivory Band of the Owl");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Savannah Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Savannah Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Amber Hoop of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Amber Hoop of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Chrome Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Chrome Ring of the Owl");
	    	    }	
            

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Coral Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Coral Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Prairie Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Prairie Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Viridian Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Viridian Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Zircon Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Zircon Band of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Clay Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Clay Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Meadow Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Meadow Ring of the Owl");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lead Band of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lead Band of the Owl");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Quartz Ring of the Owl"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Quartz Ring of the Owl");
	    	    }	



            }



  if (DoNotSellMyLootSettings.CurrentSetting.sellRingsEagle == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Onyx Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Onyx Ring of the Eagle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Opal Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Opal Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Vermilion Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Vermilion Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Granite Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Granite Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Frigid Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Frigid Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jungle Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jungle Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Peridot Circle of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Peridot Circle of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Multicolored Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Multicolored Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Hardened Stone Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Hardened Stone Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Quicksilver Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Quicksilver Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jasper Link of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jasper Link of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Obsidian Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Obsidian Band of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Swamp Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Swamp Ring of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Selenium Loop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Selenium Loop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Serpentine Loop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Serpentine Loop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Marble Circle of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Marble Circle of the Eagle");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Arctic Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Arctic Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Sardonyx Knuckle of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Sardonyx Knuckle of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Vanadium Loop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Vanadium Loop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Onyx Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Onyx Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Desert Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Desert Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Topaz Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Topaz Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tellurium Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tellurium Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Aquamarine Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Aquamarine Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lodestone Hoop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lodestone Hoop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Marsh Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Marsh Ring of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Hematite Link of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Hematite Link of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Iridium Circle of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Iridium Circle of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jet Loop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jet Loop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Forest Hoop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Forest Hoop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Carnelian Loop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Carnelian Loop of the Eagle");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Thallium Hoop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Thallium Hoop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Greenstone Circle of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Greenstone Circle of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Amethyst Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Amethyst Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Fen Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Fen Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Cerulean Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Cerulean Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Basalt Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Basalt Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Spinel Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Spinel Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Tundra Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Tundra Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Gnomeregan Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Gnomeregan Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Cobalt Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Cobalt Ring of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Jacinth Circle of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Jacinth Circle of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Ivory Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Ivory Band of the Eagle");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Savannah Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Savannah Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Amber Hoop of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Amber Hoop of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Chrome Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Chrome Ring of the Eagle");
	    	    }	
            

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Coral Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Coral Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Prairie Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Prairie Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Viridian Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Viridian Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Zircon Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Zircon Band of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Clay Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Clay Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Meadow Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Meadow Ring of the Eagle");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Lead Band of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Lead Band of the Eagle");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Quartz Ring of the Eagle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Quartz Ring of the Eagle");
	    	    }	



            }


  if (DoNotSellMyLootSettings.CurrentSetting.sellRingsOthers == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Stardust Band"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Stardust Band");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Band of the Unicorn"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Band of the Unicorn");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Falcon's Hook"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Falcon's Hook");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Welken Ring"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Welken Ring");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Monkey Ring"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Monkey Ring");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Defias Mage Ring"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Defias Mage Ring");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Defias Renegade Ring"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Defias Renegade Ring");
	    	    }	



            }



  if (DoNotSellMyLootSettings.CurrentSetting.sellRecipesEnchanting == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Chest - Minor Mana"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Chest - Minor Mana");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Minor Wizard Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Minor Wizard Oil");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Minor Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Minor Versatility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Minor Strength"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Minor Strength");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Chest - Lesser Mana"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Chest - Lesser Mana");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Minor Beastslayer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Minor Beastslayer");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant 2H Weapon - Lesser Intellect"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant 2H Weapon - Lesser Intellect");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant 2H Weapon - Lesser Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant 2H Weapon - Lesser Versatility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Cloak - Minor Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Cloak - Minor Agility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Shield - Lesser Protection"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Shield - Lesser Protection");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Lesser Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Lesser Versatility");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Boots - Minor Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Boots - Minor Agility");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Lesser Strength"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Lesser Strength");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Fishing"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Fishing");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Herbalism"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Herbalism");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Mining"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Mining");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Minor Mana Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Minor Mana Oil");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Lesser Dodge"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Lesser Dodge");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Lesser Beastslayer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Lesser Beastslayer");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Lesser Elemental Slayer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Lesser Elemental Slayer");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Boots - Lesser Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Boots - Lesser Versatility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Winter's Might"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Winter's Might");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Shield - Lesser Parry"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Shield - Lesser Parry");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Lesser Wizard Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Lesser Wizard Oil");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Skinning"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Skinning");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Shield - Stamina"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Shield - Stamina");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Advanced Mining"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Advanced Mining");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Greater Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Greater Versatility");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Cloak - Lesser Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Cloak - Lesser Agility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Advanced Herbalism"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Advanced Herbalism");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Demonslaying"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Demonslaying");
	    	    }	
            
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Dodge"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Dodge");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Greater Stamina"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Greater Stamina");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Lesser Mana Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Lesser Mana Oil");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Riding Skill"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Riding Skill");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Boots - Greater Stamina"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Boots - Greater Stamina");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Fiery Weapon"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Fiery Weapon");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Shield - Greater Stamina"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Shield - Greater Stamina");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Smoking Heart of the Mountain"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Smoking Heart of the Mountain");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Superior Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Superior Versatility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Wizard Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Wizard Oil");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Boots - Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Boots - Versatility");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Chest - Major Health"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Chest - Major Health");
	    	    }	



	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Icy Chill"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Icy Chill");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Cloak - Superior Defense"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Cloak - Superior Defense");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Argent Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Argent Versatility");
	    	    }	
            

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant 2H Weapon - Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant 2H Weapon - Agility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Agility");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Strength"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Strength");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant 2H Weapon - Superior Impact"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant 2H Weapon - Superior Impact");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Unholy"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Unholy");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Superior Strength"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Superior Strength");
	    	    }	


	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Brilliant Mana Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Brilliant Mana Oil");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Brilliant Wizard Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Brilliant Wizard Oil");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant 2H Weapon - Major Intellect"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant 2H Weapon - Major Intellect");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant 2H Weapon - Major Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant 2H Weapon - Major Versatility");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Healing Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Healing Power");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Bracer - Superior Stamina"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Bracer - Superior Stamina");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Chest - Greater Stats"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Chest - Greater Stats");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Fire Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Fire Power");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Frost Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Frost Power");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Healing Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Healing Power");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Gloves - Shadow Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Gloves - Shadow Power");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Crusader"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Crusader");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Healing Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Healing Power");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Lifestealing"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Lifestealing");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Mighty Intellect"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Mighty Intellect");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Mighty Versatility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Mighty Versatility");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Spellpower"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Spellpower");
	    	    }	

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Formula: Enchant Weapon - Superior Striking"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Formula: Enchant Weapon - Superior Striking");
	    	    }	


            }



  if (DoNotSellMyLootSettings.CurrentSetting.sellRecipesBlacksmithing == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Copper Chain Vest"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Copper Chain Vest");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heavy Copper Longsword"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heavy Copper Longsword");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runed Copper Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runed Copper Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ironforge Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ironforge Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Deadly Bronze Poniard"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Deadly Bronze Poniard");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Silvered Bronze Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Silvered Bronze Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Silvered Bronze Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Silvered Bronze Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Iridescent Hammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Iridescent Hammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Green Iron Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Green Iron Boots");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Mighty Iron Hammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Mighty Iron Hammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Green Iron Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Green Iron Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Iron Shield Spike"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Iron Shield Spike");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Silvered Bronze Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Silvered Bronze Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Barbaric Iron Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Barbaric Iron Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Barbaric Iron Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Barbaric Iron Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Green Iron Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Green Iron Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Hardened Iron Shortsword"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Hardened Iron Shortsword");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Iron Counterweight"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Iron Counterweight");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Golden Iron Destroyer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Golden Iron Destroyer");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Golden Scale Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Golden Scale Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Barbaric Iron Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Barbaric Iron Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Golden Scale Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Golden Scale Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Jade Serpentblade"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Jade Serpentblade");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Barbaric Iron Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Barbaric Iron Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Moonsteel Broadsword"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Moonsteel Broadsword");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Barbaric Iron Gloves"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Barbaric Iron Gloves");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Massive Iron Axe"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Massive Iron Axe");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Polished Steel Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Polished Steel Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Edge of Winter"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Edge of Winter");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Searing Golden Blade"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Searing Golden Blade");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Steel Weapon Chain"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Steel Weapon Chain");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Golden Scale Cuirass"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Golden Scale Cuirass");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Frost Tiger Blade"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Frost Tiger Blade");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Golden Scale Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Golden Scale Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Shadow Crescent Axe"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Shadow Crescent Axe");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Inlaid Mithril Cylinder"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Inlaid Mithril Cylinder");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Golden Scale Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Golden Scale Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heavy Mithril Pants"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heavy Mithril Pants");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Mithril Scale Bracers"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Mithril Scale Bracers");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Mithril Shield Spike"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Mithril Shield Spike");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Blue Glittering Axe"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Blue Glittering Axe");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Wicked Mithril Blade"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Wicked Mithril Blade");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Orcish War Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Orcish War Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Mithril Scale Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Mithril Scale Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Mithril Spurs"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Mithril Spurs");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dazzling Mithril Rapier"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dazzling Mithril Rapier");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heavy Mithril Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heavy Mithril Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runed Mithril Hammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runed Mithril Hammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Armor"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Armor");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Belt");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ebon Shiv"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ebon Shiv");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Bracers"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Bracers");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Radiant Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Radiant Belt");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Pulverizer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Pulverizer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Belt");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Wildthorn Mail"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Wildthorn Mail");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Mail"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Mail");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Bracers"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Bracers");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Radiant Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Radiant Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Sunderer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Sunderer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dawn's Edge"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dawn's Edge");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ornate Thorium Handaxe"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ornate Thorium Handaxe");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Shield Spike"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Shield Spike");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Blazing Rapier"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Blazing Rapier");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Enchanted Battlehammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Enchanted Battlehammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Huge Thorium Battleaxe"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Huge Thorium Battleaxe");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Helm");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Plate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Plate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Demon Forged Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Demon Forged Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Serenity"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Serenity");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Radiant Gloves"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Radiant Gloves");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Corruption"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Corruption");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dawnbringer Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dawnbringer Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Fiery Plate Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Fiery Plate Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Girdle of the Dawn"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Girdle of the Dawn");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heavy Timbermaw Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heavy Timbermaw Belt");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Radiant Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Radiant Boots");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Volcanic Hammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Volcanic Hammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Bracers"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Bracers");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Fiery Chain Girdle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Fiery Chain Girdle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Storm Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Storm Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Radiant Circlet"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Radiant Circlet");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Black Amnesty"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Black Amnesty");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Black Grasp of the Destroyer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Black Grasp of the Destroyer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Blackfury"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Blackfury");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Blackguard"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Blackguard");
	    	    }
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ebon Hand"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ebon Hand");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Fiery Chain Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Fiery Chain Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Invulnerable Mail"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Invulnerable Mail");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Jagged Obsidian Shield"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Jagged Obsidian Shield");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Lionheart Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Lionheart Helm");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Nightfall"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Nightfall");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Obsidian Mail Tunic"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Obsidian Mail Tunic");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Persuader"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Persuader");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Sageblade"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Sageblade");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Stronghold Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Stronghold Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Sulfuron Hammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Sulfuron Hammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thick Obsidian Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thick Obsidian Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Titanic Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Titanic Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Annihilator"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Annihilator");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Arcanite Champion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Arcanite Champion");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Arcanite Reaper"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Arcanite Reaper");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Bloodsoul Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Bloodsoul Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Bloodsoul Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Bloodsoul Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Bloodsoul Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Bloodsoul Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Destroyer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Destroyer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Dark Iron Reaver"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Dark Iron Reaver");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Darkrune Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Darkrune Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Darkrune Gauntlets"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Darkrune Gauntlets");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Darkrune Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Darkrune Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Darksoul Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Darksoul Breastplate");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Darksoul Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Darksoul Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Darksoul Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Darksoul Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Enchanted Thorium Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Enchanted Thorium Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Enchanted Thorium Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Enchanted Thorium Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Enchanted Thorium Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Enchanted Thorium Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Frostguard"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Frostguard");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Gloves of the Dawn"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Gloves of the Dawn");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Hammer of the Titans"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Hammer of the Titans");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heartseeker"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heartseeker");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heavy Obsidian Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heavy Obsidian Belt");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Heavy Timbermaw Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Heavy Timbermaw Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Helm of the Great Chief"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Helm of the Great Chief");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ironvine Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ironvine Belt");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ironvine Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ironvine Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Ironvine Gloves"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Ironvine Gloves");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Light Obsidian Belt"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Light Obsidian Belt");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Masterwork Stormhammer"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Masterwork Stormhammer");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Whitesoul Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Whitesoul Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Elemental Sharpening Stone"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Elemental Sharpening Stone");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Chest"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Chest");
	    	    }
				
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Imperial Plate Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Imperial Plate Leggings");
	    	    }
	
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Radiant Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Radiant Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runic Breastplate"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runic Breastplate");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runic Plate Boots"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runic Plate Boots");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runic Plate Helm"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runic Plate Helm");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runic Plate Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runic Plate Leggings");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Runic Plate Shoulders"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Runic Plate Shoulders");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Plans: Thorium Leggings"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Plans: Thorium Leggings");
	    	    }






            }




 if (DoNotSellMyLootSettings.CurrentSetting.sellRecipesAlchemy == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Discolored Healing Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Discolored Healing Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Minor Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Minor Agility");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Swiftness Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Swiftness Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Rage Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Rage Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Giant Growth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Giant Growth");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Holy Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Holy Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Minor Magic Resistance Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Minor Magic Resistance Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Potion of Curing"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Potion of Curing");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Shadow Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Shadow Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Lesser Agility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Lesser Agility");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Ogre's Strength"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Ogre's Strength");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Free Action Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Free Action Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Fire Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Fire Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Shadow Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Shadow Oil");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Fortitude"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Fortitude");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Great Rage Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Great Rage Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Mighty Troll's Blood Elixir"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Mighty Troll's Blood Elixir");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Frost Power"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Frost Power");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Frost Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Frost Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Nature Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Nature Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Detect Lesser Invisibility"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Detect Lesser Invisibility");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Frost Oil"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Frost Oil");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Magic Resistance Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Magic Resistance Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Goblin Rocket Fuel"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Goblin Rocket Fuel");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Lesser Stoneshield Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Lesser Stoneshield Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Philosopher's Stone"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Philosopher's Stone");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Wildvine Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Wildvine Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Transmute Iron to Gold"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Transmute Iron to Gold");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Transmute Mithril to Truesilver"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Transmute Mithril to Truesilver");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Invisibility Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Invisibility Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Gift of Arthas"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Gift of Arthas");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Dream Vision"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Dream Vision");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Giants"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Giants");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Greater Firepower"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Greater Firepower");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Limited Invulnerability Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Limited Invulnerability Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Mighty Rage Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Mighty Rage Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of the Sages"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of the Sages");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Transmute Undeath to Water"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Transmute Undeath to Water");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Transmute Water to Undeath"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Transmute Water to Undeath");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Transmute Life to Earth"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Transmute Life to Earth");
	    	    }
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Transmute Earth to Life"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Transmute Earth to Life");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of Brute Force"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of Brute Force");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Stoneshield Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Stoneshield Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Elixir of the Mongoose"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Elixir of the Mongoose");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Arcane Elixir"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Arcane Elixir");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Fire Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Fire Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Frost Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Frost Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Nature Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Nature Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Arcane Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Arcane Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Greater Shadow Protection Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Greater Shadow Protection Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Major Rejuvenation Potion"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Major Rejuvenation Potion");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Recipe: Potion of Petrification"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Recipe: Potion of Petrification");
	    	    }



            }



 if (DoNotSellMyLootSettings.CurrentSetting.sellRecipesEngineering == false)
            {

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Mechanical Squirrel Box"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Mechanical Squirrel Box");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: EZ-Thro Dynamite"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: EZ-Thro Dynamite");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Small Seaforium Charge"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Small Seaforium Charge");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Lovingly Crafted Boomstick"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Lovingly Crafted Boomstick");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Shadow Goggles"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Shadow Goggles");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Flame Deflector"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Flame Deflector");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Small Blue Rocket"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Small Blue Rocket");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Small Green Rocket"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Small Green Rocket");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Small Red Rocket"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Small Red Rocket");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Gnomish Universal Remote"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Gnomish Universal Remote");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Minor Recombobulator"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Minor Recombobulator");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Moonsight Rifle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Moonsight Rifle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Blue Firework"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Blue Firework");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Green Firework"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Green Firework");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Red Firework"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Red Firework");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Ice Deflector"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Ice Deflector");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Discombobulator Ray"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Discombobulator Ray");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Goblin Jumper Cables"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Goblin Jumper Cables");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Portable Bronze Mortar"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Portable Bronze Mortar");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Blue Rocket"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Blue Rocket");
	    	    }
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Green Rocket"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Green Rocket");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Red Rocket"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Red Rocket");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Bright-Eye Goggles"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Bright-Eye Goggles");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Accurate Scope"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Accurate Scope");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Flash Bomb"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Flash Bomb");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Craftsman's Monocle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Craftsman's Monocle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Snowmaster 9000"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Snowmaster 9000");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Goblin Land Mine"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Goblin Land Mine");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: EZ-Thro Dynamite II"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: EZ-Thro Dynamite II");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Seaforium Charge"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Seaforium Charge");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Gnomish Cloaking Device"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Gnomish Cloaking Device");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Mechanical Dragonling"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Mechanical Dragonling");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Lil' Smoky"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Lil' Smoky");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Pet Bombling"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Pet Bombling");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Deadly Scope"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Deadly Scope");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Catseye Ultra Goggles"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Catseye Ultra Goggles");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Mithril Heavy-Bore Rifle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Mithril Heavy-Bore Rifle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Firework Launcher"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Firework Launcher");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Parachute Cloak"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Parachute Cloak");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Spellpower Goggles Xtreme"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Spellpower Goggles Xtreme");
	    	    }
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Blue Rocket Cluster"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Blue Rocket Cluster");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Green Rocket Cluster"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Green Rocket Cluster");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Red Rocket Cluster"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Red Rocket Cluster");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Sniper Scope"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Sniper Scope");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Snake Burst Firework"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Snake Burst Firework");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Mithril Mechanical Dragonling"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Mithril Mechanical Dragonling");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Gyrofreeze Ice Reflector"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Gyrofreeze Ice Reflector");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Thorium Rifle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Thorium Rifle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Lifelike Mechanical Toad"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Lifelike Mechanical Toad");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Fused Wiring"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Fused Wiring");
	    	    }
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Blue Rocket Cluster"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Blue Rocket Cluster");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Green Rocket Cluster"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Green Rocket Cluster");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Large Red Rocket Cluster"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Large Red Rocket Cluster");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Powerful Seaforium Charge"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Powerful Seaforium Charge");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Steam Tonk Controller"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Steam Tonk Controller");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Major Recombobulator"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Major Recombobulator");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Cluster Launcher"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Cluster Launcher");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Dark Iron Rifle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Dark Iron Rifle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Masterwork Target Dummy"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Masterwork Target Dummy");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Dark Iron Bomb"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Dark Iron Bomb");
	    	    }
	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Delicate Arcanite Converter"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Delicate Arcanite Converter");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Hyper-Radiant Flame Reflector"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Hyper-Radiant Flame Reflector");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Voice Amplification Modulator"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Voice Amplification Modulator");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Arcane Bomb"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Arcane Bomb");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Arcanite Dragonling"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Arcanite Dragonling");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Biznicks 247x128 Accurascope"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Biznicks 247x128 Accurascope");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Bloodvine Goggles"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Bloodvine Goggles");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Bloodvine Lens"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Bloodvine Lens");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Core Marksman Rifle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Core Marksman Rifle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Field Repair Bot 74A"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Field Repair Bot 74A");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Flawless Arcanite Rifle"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Flawless Arcanite Rifle");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Force Reactive Disk"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Force Reactive Disk");
	    	    }

	        	if (!wManager.wManagerSetting.CurrentSetting.DoNotSellList.Contains("Schematic: Ultra-Flash Shadow Reflector"))
	        	{
			    wManager.wManagerSetting.CurrentSetting.DoNotSellList.Add("Schematic: Ultra-Flash Shadow Reflector");
	    	    }



            }




		}
		
		catch { }

            
	}

    public void Dispose()
    {
    
    }

    public void Settings()
    {
        DoNotSellMyLootSettings.Load();
        DoNotSellMyLootSettings.CurrentSetting.ToForm();
        DoNotSellMyLootSettings.CurrentSetting.Save();
    }
}

public class DoNotSellMyLootSettings : Settings
{
    public DoNotSellMyLootSettings()
    {
		settingForceSelling = true;
		settingForceSellingGrey = true;
		settingForceSellingWhite = true;
		settingForceSellingGreen = true;

        sellCheapOres = true;
        sellExpensiveOres = false;
        sellQuestOres = true;

        sellExpensiveLeather = false;
        sellCheapLeather = true;
        sellSpecialLeather = false;
        sellHidesLeather = false;

        sellExpensiveHerbs = false;
        sellCheapHerbs = true;
        sellBlackLotusHerbs = false;

		sellExpensiveCloth = false;
		sellCheapCloth = true;

		sellBags6and8 = true;
		sellBags10and12 = false;
		sellBags14 = false;

		sellRingsMonkey = false;
		sellRingsOwl = false;
		sellRingsEagle = false;
		sellRingsOthers = false;

		sellRecipesEnchanting = true;
		sellRecipesBlacksmithing = true;
		sellRecipesAlchemy = true;
		sellRecipesEngineering = true;


    }

    public static DoNotSellMyLootSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("DoNotSellMyLoot", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("DoNotSellMyLootSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("DoNotSellMyLoot", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<DoNotSellMyLootSettings>(AdviserFilePathAndName("DoNotSellMyLoot",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new DoNotSellMyLootSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("DoNotSellMyLoot => Load(): " + e);
        }
        return false;
    }





	// Settings
    [Category("00) Settings")]
    [DisplayName("01) Force Enable Selling")]
    [Description("Makes sure that every bot has Selling enabled.")]
    public bool settingForceSelling { get; set; }	

    [Category("00) Settings")]
    [DisplayName("02) Sell Grey Items")]
    [Description("Makes sure that every bot sells its grey items.")]
    public bool settingForceSellingGrey { get; set; }	

	[Category("00) Settings")]
    [DisplayName("03) Sell White Items")]
    [Description("Makes sure that every bot sells its white items.")]
    public bool settingForceSellingWhite { get; set; }	

	[Category("00) Settings")]
    [DisplayName("04) Sell Green Items")]
    [Description("Makes sure that every bot sells its green items.")]
    public bool settingForceSellingGreen { get; set; }




     // ORES
    [Category("01) Sell Ore?")]
    [DisplayName("01) Cheap Ore")]
    [Description("Copper Ore, Tin Ore")]
    public bool sellCheapOres { get; set; }

    [Category("01) Sell Ore?")]
    [DisplayName("02) Expensive Ore")]
    [Description("Iron Ore, Silver Ore, Gold Ore, Mithril Ore, Thorium Ore, Truesilver Ore")]
    public bool sellExpensiveOres { get; set; }

    [Category("01) Sell Ore?")]
    [DisplayName("03) Quest Ore")]
    [Description("Umbral, Elunite, Rethban, Incendicite, Lesser Bloodstone, Indurium")]
    public bool sellQuestOres { get; set; }

    // LEATHER
    [Category("03) Sell Leather?")]
    [DisplayName("01) Cheap Leather")]
    [Description("Ruined Leather Scraps, Light Leather, Medium Leather")]
    public bool sellCheapLeather { get; set; }

    [Category("03) Sell Leather?")]
    [DisplayName("02) Expensive Leather")]
    [Description("Heavy Leather, Thick Leather, Rugged Leather")]
    public bool sellExpensiveLeather { get; set; }

    [Category("03) Sell Leather?")]
    [DisplayName("03) Special Leather")]
    [Description("Thin Kodo Leather, Devilsaur Leather, Warbear Leather, Primal Bat Leather, Primal Tiger Leather, Core Leather")]
    public bool sellSpecialLeather { get; set; }

    [Category("03) Sell Leather?")]
    [DisplayName("04) Hides")]
    [Description("Light Hide, Medium Hide, Heavy Hide, Thick Hide, Rugged Hide")]
    public bool sellHidesLeather { get; set; }


    // HERBS
    [Category("02) Sell Herbs?")]
    [DisplayName("01) Cheap Herbs")]
    [Description("Peacebloom, Silverleaf, Earthroot, Mageroyal, Bruiseweed")]
    public bool sellCheapHerbs { get; set; }

    [Category("02) Sell Herbs?")]
    [DisplayName("02) Expensive Herbs")]
    [Description("Swiftthistle, Briarthorn, Stranglekelp, Wild Steelbloom, Grave Moss, Kingsblood, Liferoot, Fadeleaf, Goldthorn, Khdgars Whisker, Wintersbite, Firebloom, Purple Lotus, Wildvine, Arthas' Tears, Sungrass, Blindweed, Ghost Mushroom, Gromsblood, Golden Sansam, Deamfoil, Mountain Silversage, Plaguebloom, Icecap")]
    public bool sellExpensiveHerbs { get; set; }

    [Category("02) Sell Herbs?")]
    [DisplayName("03) Black Lotus")]
    [Description("Black Lotus")]
    public bool sellBlackLotusHerbs { get; set; }

	    // CLOTH
    [Category("04) Sell Cloth?")]
    [DisplayName("01) Cheap Cloth")]
    [Description("Linen Cloth, Wool Cloth, Silk Cloth")]
    public bool sellCheapCloth { get; set; }

    [Category("04) Sell Cloth?")]
    [DisplayName("02) Expensive Cloth")]
    [Description("Mageweave Cloth, Runecloth, Felcloth, Mooncloth")]
    public bool sellExpensiveCloth { get; set; }

	// BAGS
    [Category("05) Sell Bags?")]
    [DisplayName("01) 6 and 8 Slot Bags")]
    [Description("All 6 and 8 Slot Bags")]
    public bool sellBags6and8 { get; set; }	

	[Category("05) Sell Bags?")]
    [DisplayName("02) 10 and 12 Slot Bags")]
    [Description("All 10 and 12 Slot Bags")]
    public bool sellBags10and12 { get; set; }

	[Category("05) Sell Bags?")]
    [DisplayName("03) 14 Slot Bags")]
    [Description("All 14 Slot Bags")]
    public bool sellBags14 { get; set; }	

	// BAGS
    [Category("06) Sell Rings?")]
    [DisplayName("01) of the Monkey")]
    [Description("All Rings with Monkey Spec.")]
    public bool sellRingsMonkey { get; set; }	

    [Category("06) Sell Rings?")]
    [DisplayName("02) of the Owl")]
    [Description("All Rings with Owl Spec.")]
    public bool sellRingsOwl { get; set; }	

    [Category("06) Sell Rings?")]
    [DisplayName("03) of the Eagle")]
    [Description("All Rings with Eagle Spec.")]
    public bool sellRingsEagle { get; set; }	

    [Category("06) Sell Rings?")]
    [DisplayName("04) Others")]
    [Description("All Rings that do not have a random spec.")]
    public bool sellRingsOthers { get; set; }	

    [Category("07) Sell Recipes?")]
    [DisplayName("01) Enchanting")]
    [Description("Every enchanting recipe.")]
    public bool sellRecipesEnchanting { get; set; }	

    [Category("07) Sell Recipes?")]
    [DisplayName("02) Blacksmithing")]
    [Description("Every blacksmithing recipe.")]
    public bool sellRecipesBlacksmithing { get; set; }	

    [Category("07) Sell Recipes?")]
    [DisplayName("03) Alchemy")]
    [Description("Every alchemy recipe.")]
    public bool sellRecipesAlchemy { get; set; }	

    [Category("07) Sell Recipes?")]
    [DisplayName("04) Engineering")]
    [Description("Every engineering recipe.")]
    public bool sellRecipesEngineering { get; set; }

}