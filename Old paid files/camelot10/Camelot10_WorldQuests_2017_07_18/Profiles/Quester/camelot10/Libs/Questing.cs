﻿// QUESTING START
#if VISUAL_STUDIO
using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using wManager.Wow.Enums;
#endif

public sealed class Questing
{
	public delegate bool BoolDelegate();

	static Questing()
	{
		ResetSettings();
		Var.SetVar("Cameleto10Questing", true);
		//		var questing = robotManager.Helpful.Var.Exist("Cameleto10Questing") && robotManager.Helpful.Var.GetVar<bool>("Cameleto10Questing");
		//		var r= RequiredQuest == -1; if (r) RequiredQuest = 0; return r;
	}

	public static void Log(string text)
	{
		Logging.Write("[Questing] " + text);
	}


	#region QUESTS
	public static bool ObjectiveComplete(QuestClass quest, int objectiveNum = 1)
	{
		foreach (var questId in quest.QuestId)
		{
			if (Quest.IsObjectiveComplete(objectiveNum, questId))
				return true;
		}
		return false;
	}

	public static int ObjectiveProgress(QuestClass quest, int obj)
	{
		foreach (var questId in quest.QuestId)
		{
			var toRun = string.Format(@"local text, type, unknown, progress, progressTotal = GetQuestObjectiveInfo({0}, {1}, false) if progressTotal > 0 then return progress end return -1", questId, obj);
			var result = Lua.LuaDoString<int>(toRun);
			if (result >= 0)
				return result;
		}
		return -1;
	}

	public static bool Complete(QuestClass quest)
	{
		return Complete(quest.QuestId.ToArray());
	}
	public static bool Complete(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			//if (Quest.IsObjectiveComplete(1, questId))
			if (Quest.GetQuestCompleted(questId))
				return true;
		}
		return false;
	}

	public static bool Has(QuestClass quest)
	{
		return Has(quest.QuestId.ToArray());
	}
	public static bool Has(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			if (Quest.HasQuest(questId))
				return true;
		}
		return false;
	}

	public static bool Do(QuestClass quest)
	{
		return Do(quest.QuestId.ToArray());
	}
	public static bool Do(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			if (Has(questId) && !Complete(questId))
				return true;
		}
		return false;
	}

	public static bool Need(QuestClass quest)
	{
		return Need(quest.QuestId.ToArray());
	}
	public static bool Need(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			if (!Has(questId) && !Complete(questId))
				return true;
		}
		return false;
	}

	public static bool NotComplete(QuestClass quest) //Breadcrumb
	{
		return NotComplete(quest.QuestId.ToArray());
	}
	public static bool NotComplete(params int[] questIds) //Breadcrumb
	{
		foreach (var questId in questIds)
		{
			if (Complete(questId))
				return false;
		}
		return true;
	}
	public static bool NotCompleteAll(QuestClass quest)
	{
		return NotCompleteAll(quest.QuestId.ToArray());
	}

	public static bool NotCompleteAll(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			if (!Complete(questId))
				return true;
		}
		return false;
	}
	public static bool Done(QuestClass quest)
	{
		return Done(quest.QuestId.ToArray());
	}
	public static bool Done(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			if (Has(questId) && Complete(questId))
				return true;
		}
		return false;
	}
	public static bool Recieved(QuestClass quest)
	{
		return Recieved(quest.QuestId.ToArray());
	}
	public static bool Recieved(params int[] questIds)
	{
		foreach (var questId in questIds)
		{
			if (Has(questId) || Complete(questId))
				return true;
		}
		return false;
	}

	public static bool Abandon(int questID)
	{
		var lua = @"
for questIndex = 1, GetNumQuestLogEntries() do
	local title, level, tag, suggestedGroup, isHeader, isCollapsed, isComplete, questID = GetQuestLogTitle(questIndex)
	if questID ~= 0 and questID == {0} then
		SelectQuestLogEntry(questIndex)
		SetAbandonQuest()
		AbandonQuest()
		return true
	end
end
return false
";
		var runCode = string.Format(lua, questID);
		return Lua.LuaDoString<bool>(runCode);
	}

	#endregion QUESTS


	#region HELPERS
	public static void Goto(Vector3 position)
	{
		var dist = ObjectManager.Me.Position.DistanceTo(position);
		if (dist > 100 && Lua.LuaDoString<bool>("return IsFlyableArea()"))
		{
			Log("goto (long move): " + position);
			MovementManager.StopMove();
			LongMove.StopLongMove();
			MountTask.Mount(true, MountTask.MountCapacity.Fly);
			Thread.Sleep(1 * 1000);
			LongMove.LongMoveGo(position);
			Thread.Sleep(1 * 1000);
		}
		else if (dist > 10)
		{
			Log("goto: " + position);
			MovementManager.StopMove();
			LongMove.StopLongMove();
			GoToTask.ToPosition(position);
		}
	}

	public static bool InteractNPC(Vector3 p, int npcId, float range = 4.5f, int gossip = 1)
	{
		if (GoToTask.ToPosition(p, range))
		{
			//var npc = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(npcId));
			var npc = ObjectManager.GetObjectWoWUnit().Where(u => u != null && u.IsValid && u.Entry == npcId).FirstOrDefault();
			if (npc != null)
			{
				Interact.InteractGameObject(npc.GetBaseAddress);
				Log("interact " + npc.Name);
				Thread.Sleep(200 + Usefuls.Latency);
				Usefuls.SelectGossipOption(gossip);
				Thread.Sleep(Others.Random(500, 1000));
				return true;
			}
		}
		return false;
	}

	public static bool FindNPC(NPCQuest npc, List<Vector3> hotspots)
	{
		var p = hotspots[Others.Random(0, hotspots.Count - 1)];
		GoToTask.ToPosition(p, 3.5f, false, (c) =>
		{
			var n = FindUnit(npc.Id);
			if (n != null && n.IsValid)
			{
				Logging.Write("located npc=" + n.Entry + " at " + n.Position);
				return false;
			}
			return true;
		});
		var npcFound = FindUnit(npc.Id);
		if (npcFound != null && npcFound.IsValid)
		{
			Log("found npc=" + npcFound.Entry + " at " + npcFound.Position);
			npc.Position = npcFound.Position;
			return true;
		}
		return false;
	}

	public static void PickUp(Vector3 p, int npcId, float range = 4.5f, int gossip = 1)
	{
		if (InteractNPC(p, npcId, range, gossip))
		{
			var gossips = Quest.GetNumGossipAvailableQuests();
			if (gossips > 0)
			{
				Quest.SelectGossipAvailableQuest(1);
				Thread.Sleep(Usefuls.Latency + 1000);
			}
			//* OLD
			Quest.AcceptQuest();
			Log("accepted quest");
			//*/
		}
	}

	public static void TurnIn(Vector3 p, int npcId, float range = 4.5f, int gossip = 1)
	{
		if (InteractNPC(p, npcId, range, gossip))
		{
			Quest.CompleteQuest(gossip);
			Log("turnin quest");
		}
	}

	public static void TurnInQuest()
	{
		Lua.LuaDoString("QuestFrameCompleteButton:Click(); QuestFrameCompleteQuestButton:Click();");
	}

	public static void CloseGossip()
	{
		Lua.LuaDoString("ClearTarget(); GossipFrameCloseButton:Click(); GossipFrameGreetingGoodbyeButton:Click();");
		Thread.Sleep(Others.Random(1000, 2000));
	}

	public static void CloseQuest()
	{
		Lua.LuaDoString("ClearTarget(); QuestFrameGoodbyeButton:Click(); QuestFrameCloseButton:Click();");
		Thread.Sleep(Others.Random(1000, 2000));
	}

	public delegate bool GrindDelegate(WoWUnit unit);
	public static void Grind(List<int> mobs, List<Vector3> hotspots = null, GrindDelegate condition = null)
	{
		if (hotspots == null || hotspots.Count < 1)
			hotspots = new List<Vector3>() { ObjectManager.Me.Position };

		if (condition == null)
			condition = u => { return true; };

		var mob = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(mobs));
		if (mob != null && mob.IsAlive && mob.IsAttackable && mob.IsValid && condition(mob))
		{
			Interact.InteractGameObject(mob.GetBaseAddress);
			Fight.StartFight(mob.Guid);
		}
		GoToTask.ToPosition(hotspots[Others.Random(0, hotspots.Count - 1)], 3.5f, false, (c) =>
		{
			var aggro = ObjectManager.GetUnitAttackPlayer().FirstOrDefault();
			if (aggro != null && aggro.IsValid && aggro.IsAlive && aggro.IsAttackable)
			{
				Interact.InteractGameObject(aggro.GetBaseAddress);
				Fight.StartFight(aggro.Guid);
				return false;
			}
			var mob2 = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(mobs));
			if (mob2 != null && mob2.IsAlive && mob2.IsAttackable && mob2.IsValid && condition(mob2))
			{
				Interact.InteractGameObject(mob2.GetBaseAddress);
				Fight.StartFight(mob2.Guid);
				return false;
			}
			return true;
		});
	}

	public static bool Equip(params int[] itemIds)
	{
		var result = false;
		foreach (var itemId in itemIds)
		{
			if (ItemsManager.IsEquippableItem(itemId) && ItemsManager.HasItemById((uint)itemId) && !Lua.LuaDoString<bool>("return IsEquippedItem(" + itemId + ")"))
			{
				var itemName = ItemsManager.GetNameById(itemId);
				if (!string.IsNullOrEmpty(itemName))
				{
					ItemsManager.EquipItemByName(itemName);
					Log("Equip " + itemName);
					Thread.Sleep(Others.Random(500, 1000));
					result = true;
				}
			}
		}
		return result;
	}

	public static bool Gather(int objectID, float minDist = 5f)
	{
		wManager.wManagerSetting.ClearBlacklistOfCurrentProductSession();
		var targetObject = ObjectManager.GetObjectWoWGameObject().Where(o => o.IsValid && o.Entry == objectID).OrderBy(o => o.GetDistance).FirstOrDefault();
		if (targetObject != null)
		{
			if (targetObject.GetDistance > minDist)
			{
				GoToTask.ToPosition(targetObject.Position, minDist - 0.1f);
			}

			Interact.InteractGameObject(targetObject.GetBaseAddress);
			Log("Gather " + targetObject.Name);
			Usefuls.WaitIsCasting();
			Thread.Sleep(Others.Random(500, 1000));
			return true;
		}
		return false;
	}

	public static bool Gather(Vector3 position, int objectID, float minDist = 5)
	{
		wManager.wManagerSetting.ClearBlacklistOfCurrentProductSession();
		if (ObjectManager.Me.Position.DistanceTo(position) > minDist)
			GoToTask.ToPosition(position, minDist);

		var targetObject = ObjectManager.GetObjectWoWGameObject().Where(o => o.IsValid && o.Entry == objectID).OrderBy(o => o.GetDistance).FirstOrDefault();
		if (targetObject != null)
		{
			Interact.InteractGameObject(targetObject.GetBaseAddress);
			Log("Gather " + targetObject.Name);
			Usefuls.WaitIsCasting();
			Thread.Sleep(Others.Random(500, 1000));
			return true;
		}
		return false;
	}

	public static void Use(params int[] itemIds)
	{
		foreach (var itemId in itemIds)
		{
			var itemName = ItemsManager.GetNameById(itemId);
			if (!string.IsNullOrEmpty(itemName))
			{
				ItemsManager.UseItem(itemName);
				Log("Use " + itemName);
				Usefuls.WaitIsCasting();
				Thread.Sleep(Others.Random(500, 1000));
			}
		}
	}

	public static bool Attack(params int[] mobIDs)
	{
		var mob = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(new List<int>(mobIDs)));
		if (mob != null && mob.IsValid && mob.IsAlive && mob.IsAttackable)
		{
			Interact.InteractGameObject(mob.GetBaseAddress);
			Fight.StartFight(mob.Guid);
			return true;
		}
		return false;
	}

	public static void Repair(Vector3 position, int npcId)
	{
		if (ObjectManager.Me.GetDurabilityPercent > 99)
			return;

		if (GoToTask.ToPositionAndIntecractWithNpc(position, npcId))
		{
			Vendor.RepairAllItems();
			Log("Repair");
		}
	}

	public static void Buy(Vector3 position, int npcId, int itemId, int amount = 1)
	{
		if (GoToTask.ToPositionAndIntecractWithNpc(position, npcId))
		{
			Usefuls.SelectGossipOption(wManager.Wow.Enums.GossipOptionsType.vendor);
			Thread.Sleep(Others.Random(500, 700));
			while (amount-- > 0 && Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause)
			{
				var itemName = ItemsManager.GetNameById(itemId);
				if (!string.IsNullOrEmpty(itemName))
				{
					Vendor.BuyItem(itemName, 1);
					Log("Buy " + itemName);
					Thread.Sleep(1 * 1000);
				}
			}
		}
	}

	public static void UpdateMounts(bool isForced = false)
	{
		var settings = wManager.wManagerSetting.CurrentSetting;

		if (isForced || string.IsNullOrEmpty(settings.GroundMountName))
			settings.GroundMountName = SpellManager.GetGroudMountName();

		if (isForced || string.IsNullOrEmpty(settings.FlyingMountName))
			settings.FlyingMountName = SpellManager.GetFlyMountName();

		if (isForced || string.IsNullOrEmpty(settings.AquaticMountName))
			settings.AquaticMountName = SpellManager.GetAquaticMountName();

		Log("Mounts updated. ground: " + settings.GroundMountName + " flying: " + settings.FlyingMountName + " aquatic: " + settings.AquaticMountName);
	}

	public static void CancelCutscene(int timeout = 500)
	{
		Thread.Sleep(1500 + timeout);
		//Lua.LuaDoString("MovieFrame:StopMovie(); CinematicFrame_CancelCinematic(); StopCinematic();");
		var lua = @"
if MovieFrame:IsVisible() then
	MovieFrame:StopMovie();
else
	CinematicFrame_CancelCinematic();
end
GameMovieFinished();
StopCinematic();
";
		Lua.LuaDoString(lua);
		Log("Cutscene cancel");
		Thread.Sleep(timeout);
	}

	public static bool UseExtraButton(int num = 1)
	{
		if (ExtraButton())
		{
			Lua.LuaDoString("ExtraActionButton" + num + ":Click()");
			return true;
		}
		return false;
	}

	public static bool ExtraButton()
	{
		var lua = @"return HasExtraActionBar()";
		return Lua.LuaDoString<bool>(lua);
	}

	public static bool ExtraButton(uint spellID)
	{
		return ExtraButton((int)spellID);
	}

	public static bool ExtraButton(int spellId)
	{
		//local slot = extraPage*12 + 11
		var lua = @"
local extraPage = GetExtraBarIndex()
local slot = extraPage * 12 - 11
if HasExtraActionBar() then
	local action, id, subType, spellID = GetActionInfo(slot)
	if id then
		return id
	end
end
return 0";

		var extraSpellId = Lua.LuaDoString<int>(lua);
		if (extraSpellId == 0)
		{
			return false;
		}

		if (spellId != extraSpellId)
		{
			return false;
		}
		return true;
	}

	public static bool Gossip(int num = 1)
	{
		var buttonName = "GossipTitleButton" + num;
		if (IsVisible(buttonName))
		{
			Lua.LuaDoString(buttonName + ":Click()");
			Thread.Sleep(Others.Random(500, 1500));
			return true;
		}
		return false;
	}

	public static bool QuestButton(int num = 1)
	{
		var buttonName = "QuestTitleButton" + num;
		if (IsVisible(buttonName))
		{
			Lua.LuaDoString(buttonName + ":Click()");
			Thread.Sleep(Others.Random(500, 1500));
			return true;
		}
		return false;
	}

	public static bool ChooseQuest(int num = 1)
	{
		if (IsVisible("QuestChoiceFrameOption" + num))
		{
			Lua.LuaDoString("QuestChoiceFrameOption" + num + ".OptionButton:Click();");
			Thread.Sleep(Others.Random(1000, 1500));
			ClickStaticPopup(2);
			return true;
		}
		return false;
	}

	public static bool ClickStaticPopup(int buttonNum, int staticNum = 1)
	{
		var buttonName = "StaticPopup" + staticNum + "Button" + buttonNum;
		if (IsVisible(buttonName))
		{
			Lua.LuaDoString(buttonName + ":Click();");
			Thread.Sleep(Others.Random(1000, 1500));
			return true;
		}
		return false;
	}

	public static bool PossedButton(int num = 1)
	{
		if (Lua.LuaDoString<bool>("return PossessButton" + num + ":IsVisible()"))
		{
			Lua.LuaDoString("PossessButton" + num + ":Click()");
			return true;
		}
		return false;
	}

	public static void PathFromNear(List<Vector3> path, bool ignoreFight = false, BoolDelegate condition = null)
	{
		if (condition == null)
			condition = () => true;

		var closest = path.OrderBy(v => ObjectManager.Me.Position.DistanceTo(v)).FirstOrDefault();
		var clampedPath = new List<Vector3>();
		bool include = false;
		for (int i = 0; i < path.Count; i++)
		{
			if (!include)
			{
				include = path[i].DistanceTo(closest) < 3.5f;
			}
			else
			{
				clampedPath.Add(path[i]);
			}
		}

		Path(clampedPath, ignoreFight, condition);
	}

	public static void Path(List<Vector3> path, bool ignoreFight = false, BoolDelegate condition = null)
	{
		if (condition == null)
			condition = () => true;

		var oldIgnore = Conditions.ForceIgnoreIsAttacked;
		if (ignoreFight)
			Conditions.ForceIgnoreIsAttacked = true;

		MovementManager.Go(path);
		//while (MovementManager.InMovement && Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && !Conditions.IsAttackedAndCannotIgnore && condition())
		do
		{
			Thread.Sleep(1000);
		}
		while (MovementManager.InMovement && Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && !Conditions.IsAttackedAndCannotIgnore && condition());
		MovementManager.StopMove();
		if (ignoreFight)
			Conditions.ForceIgnoreIsAttacked = oldIgnore;
	}

	public static void PathAsPet(List<Vector3> path, float precision = 3.5f, BoolDelegate condition = null)
	{
		if (condition == null)
			condition = () => true;

		var pet = ObjectManager.Pet;
		Log("move pet path=" + path.Count);
		foreach (var p in path)
		{
			ClickMove(p);
			while (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && condition() && pet != null && pet.IsValid && pet.IsAlive && pet.Position.DistanceTo(p) > precision)
			{
				Thread.Sleep(1000);
			}
		}
		Log("move pet end " + path.Count);
	}

	public static void PathClickToMove(List<Vector3> path, float precision = 3.5f, bool ignoreFight = false, BoolDelegate runCondition = null)
	{
		Log("move click path=" + path.Count);
		var useLuaToMove = wManager.wManagerSetting.CurrentSetting.UseLuaToMove;
		wManager.wManagerSetting.CurrentSetting.UseLuaToMove = false;
		var oldIgnore = Conditions.ForceIgnoreIsAttacked;
		if (ignoreFight)
			Conditions.ForceIgnoreIsAttacked = true;

		if (runCondition == null)
			runCondition = () => true;

		foreach (var p in path)
		{
			ClickMove(p);
			while (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && ObjectManager.Me.Position.DistanceTo(p) > precision && runCondition())
			{
				Thread.Sleep(1000);
			}
		}
		if (ignoreFight)
			Conditions.ForceIgnoreIsAttacked = oldIgnore;

		wManager.wManagerSetting.CurrentSetting.UseLuaToMove = useLuaToMove;
		Log("move click end " + path.Count);
	}

	public static void ClickMove(Vector3 p, float precision = 3.5f)
	{
		ClickToMove.CGPlayer_C__ClickToMove(p.X, p.Y, p.Z, MemoryRobot.Int128.Zero(), (int)wManager.Wow.Enums.ClickToMoveType.Move, precision);
	}

	public static void ClickMove(WoWUnit unit, float precistion = 0.5f)
	{
		ClickToMove.CGPlayer_C__ClickToMove(unit.Position.X, unit.Position.Y, unit.Position.Z, unit.Guid, (int)wManager.Wow.Enums.ClickToMoveType.Move, precistion);
	}
	public static void WaitIsCasting(WoWUnit unit)
	{
		//copy from Usefuls.WaitIsCasting
		var timer = new robotManager.Helpful.Timer((double)(Usefuls.Latency + 200));
		while (!timer.IsReady && !unit.IsCast && Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause)
			Thread.Sleep(5);
		while (unit.IsCast && Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause)
			Thread.Sleep(30);
	}

	public static bool CompleteAutoCompletableQuest(int questId)
	{
		if (!Quest.HasQuest(questId))
			return false;

		var lua = @"
for questIndex = 1, GetNumQuestLogEntries() do
	local title, level, tag, suggestedGroup, isHeader, isCollapsed, isComplete, questID = GetQuestLogTitle(questIndex)
	if questID ~= 0 and questID == {0} then
		ShowQuestComplete(questIndex);
		return true
	end
end
return false
";
		var runCode = string.Format(lua, questId);
		if (Lua.LuaDoString<bool>(runCode))
		{
			CompleteQuestFrame();
		}
		return true;
	}

	public static bool IsVisible(string gui)
	{
		return Lua.LuaDoString<bool>("return " + gui + ":IsVisible()");
	}

	public static bool CompleteQuestFrame()
	{
		Thread.Sleep(Others.Random(500, 1000));
		Lua.LuaDoString("CompleteQuest()");
		Thread.Sleep(Others.Random(500, 1000));
		Lua.LuaDoString("GetQuestReward()");
		return true;
	}

	public static bool NoCooldownSpell(int spellId)
	{
		return NoCooldownSpell((uint)spellId);
	}

	public static bool NoCooldownSpell(uint spellId)
	{
		var lua = @"start, duration, enabled = GetSpellCooldown('{0}'); return start;";
		var runCode = string.Format(lua, spellId);
		var cd = Lua.LuaDoString<int>(runCode);
		return cd == 0;
	}

	public static bool VehicleButton(int num)
	{
		Lua.RunMacroText("/click OverrideActionBarButton" + num);
		Thread.Sleep(Usefuls.Latency * 2);
		return true;
	}

	//DONT USE THIS
	static bool _PopupTurninOrPickup()
	{
		var lua = @"
for i = 1, GetNumAutoQuestPopUps() do
	local id, type = GetAutoQuestPopUp(i)
	local title = GetQuestLogTitle(GetQuestLogIndexByID(id))
	if title and title ~= '' then
		local block = AUTO_QUEST_POPUP_TRACKER_MODULE:GetBlock(id)
		AutoQuestPopUpTracker_OnMouseDown(block)
		return true
		if type == 'COMPLETE' then
			--
		elseif type == 'OFFER' then
			--
		end
	end
end
return false
";
		int antiloop = 10;
		while (antiloop-- > 0 && Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && Lua.LuaDoString<bool>(lua))
		{
			Lua.LuaDoString("AcceptQuest()");
			Lua.LuaDoString("CompleteQuest()");
			Log("accept/complete popup quest");
			Thread.Sleep(Others.Random(300, 500));
		}
		return true;
	}

	public static void RemoveItem(uint itemID)
	{
		var lua = @"
for i=0,4 do
	for j=1,36 do
		if GetContainerItemID(i,j) == {0} then
			PickupContainerItem(i,j)
			DeleteCursorItem();
			return
		end
	end
end
";
		var runCode = string.Format(lua, itemID);
		Lua.LuaDoString(runCode);
	}
	public static WoWUnit FindUnit(params int[] npcIDs)
	{
		return FindUnit(new List<int>(npcIDs));
	}
	public static WoWUnit FindUnit(List<int> npcIDs)
	{
		return ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(npcIDs));
	}

	public static WoWUnit FindUnitIncludeDead(params int[] npcIDs)
	{
		return ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(ObjectManager.GetObjectWoWUnit(), new List<int>(npcIDs), true));
	}

	public static WoWGameObject FindObject(params int[] objIDs)
	{
		return ObjectManager.GetNearestWoWGameObject(ObjectManager.GetWoWGameObjectByEntry(new List<int>(objIDs)));
	}

	public static bool Attack(WoWUnit unit)
	{
		if (unit == null || !unit.IsValid || unit.IsDead || !unit.IsAttackable)
			return false;

		Interact.InteractGameObject(unit.GetBaseAddress);
		Fight.StartFight(unit.Guid);
		return true;
	}

	public static int ZoneId
	{
		get
		{
			return Lua.LuaDoString<int>("local mapID, isContinent = GetCurrentMapAreaID(); return mapID;");
		}
	}

	/// <summary>
	/// 0 = friendRep, 1 = friendMaxRep, 2 = friendName, 3 = friendText,
	/// 4 = friendThreshold, 5 = nextFriendThreshold
	/// </summary>
	/// <param name="factionID"></param>
	/// <returns></returns>
	public static List<string> GetFriendReputation(int factionID)
	{
		var lua = @"
friendID, friendRep, friendMaxRep, friendName, friendText, friendTexture, friendTextLevel, friendThreshold, nextFriendThreshold = GetFriendshipReputation({0});
return friendRep .. '{1}' .. friendMaxRep .. '{1}' .. friendName .. '{1}' .. friendText .. '{1}' .. friendThreshold .. '{1}' .. nextFriendThreshold;
";
		var runCode = string.Format(lua, factionID, Lua.ListSeparator);
		var result = Lua.LuaDoString<List<string>>(runCode);
		return result;
	}

	/// <summary>
	/// 0 = name, 1 = description
	/// 2 = standingID (1 - Hated, 2 - Hostile, 3 - Unfriendly, 4 - Neutral, 5 - Friendly, 6 - Honored, 7 - Revered, 8 - Exalted),
	/// 3 = barMin, 4 = barMax, 5 = barValue
	/// </summary>
	/// <param name="factionID"></param>
	/// <returns></returns>
	public static List<string> GetReputation(int factionID)
	{
		var lua = @"
local name, description, standingID, barMin, barMax, barValue, atWarWith, canToggleAtWar, isHeader, isCollapsed, hasRep, isWatched, isChild, factionID, hasBonusRepGain, canBeLFGBonus = GetFactionInfoByID({0});
return name .. '{1}' .. description .. '{1}' .. standingID .. '{1}' .. barMin .. '{1}' .. barMax .. '{1}' .. barValue;
";
		var runCode = string.Format(lua, factionID, Lua.ListSeparator);
		var result = Lua.LuaDoString<List<string>>(runCode);
		return result;
	}
	public static int Currency(int currencyID)
	{
		var lua = @"local name, amount, texturePath, earnedThisWeek, weeklyMax, totalMax, isDiscovered, quality = GetCurrencyInfo('currency: {0}'); return amount;";
		return Lua.LuaDoString<int>(string.Format(lua, currencyID));
	}
	public static int CurrencyMax(int currencyID)
	{
		var lua = @"local name, amount, texturePath, earnedThisWeek, weeklyMax, totalMax, isDiscovered, quality = GetCurrencyInfo('currency: {0}'); return totalMax;";
		return Lua.LuaDoString<int>(string.Format(lua, currencyID));
	}
	public static int ItemCooldown(int itemID)
	{
		return ItemCooldown((uint)itemID);
	}
	public static int ItemCooldown(uint itemID)
	{
		return Lua.LuaDoString<int>("local startTime, duration, enable = GetItemCooldown(" + itemID + "); return startTime + duration - GetTime();");
	}
	public static bool ItemOnCooldown(uint itemID)
	{
		return ItemCooldown(itemID) > 0;
	}
	public static bool ItemOnCooldown(int itemID)
	{
		return ItemOnCooldown((uint)itemID);
	}

	/// <summary>
	/// WARNING! PRODUCT RESTART, USE WITH CAUTION
	/// </summary>
	/// <param name="file"></param>
	public static void LoadProfile(string file)
	{
		Quester.Bot.QuesterSetting.CurrentSetting.ProfileName = file;
		robotManager.Products.Products.ProductRestart();
	}

	public static void DiableFlying()
	{
		wManager.wManagerSetting.CurrentSetting.UseFlyingMount = false;
		MountTask.DismountMount();
	}

	public static void DisableMount()
	{
		wManager.wManagerSetting.CurrentSetting.UseMount = false;
		MountTask.DismountMount();
	}


	static bool _movementDisabled = false;
	public static bool MovementDisabled
	{
		get
		{
			return _movementDisabled;
		}
		set
		{
			if (value == _movementDisabled)
				return;

			_movementDisabled = value;
			Log("movement disabled = " + _movementDisabled);
			if (_movementDisabled)
			{
				wManager.Events.MovementEvents.OnMovementPulse += _OnMovementPulse;
				wManager.Events.MovementEvents.OnMoveToPulse += _OnMoveToPulse;
			}
			else
			{
				wManager.Events.MovementEvents.OnMovementPulse -= _OnMovementPulse;
				wManager.Events.MovementEvents.OnMoveToPulse -= _OnMoveToPulse;
			}
		}
	}
	static void _OnMoveToPulse(Vector3 point, System.ComponentModel.CancelEventArgs cancelable)
	{
		cancelable.Cancel = true;
	}
	static void _OnMovementPulse(List<Vector3> points, System.ComponentModel.CancelEventArgs cancelable)
	{
		cancelable.Cancel = true;
	}
	public static void WaitAreaIDChange(int prevAreaID, int minutes = 3)
	{
		var timer = new robotManager.Helpful.Timer(minutes * 60 * 1000);
		while (robotManager.Products.Products.IsStarted)
		{
			if (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause)
			{
				if (timer.IsReady)
					break;

				if (Usefuls.AreaId != prevAreaID)
				{
					break;
				}
			}
			Thread.Sleep(1000);
		}
	}

	public static Vector3 GetPositionFrom(Vector3 pos, Vector3 from, float range)
	{
		var delta = pos - from;
		var deltaMag = delta.Magnitude();
		var deltaNormalized = delta / deltaMag;
		var deltaNeed = deltaNormalized * range;
		return pos + deltaNeed;
	}

	public static void GatherOrInteract(List<Vector3> hotspots, List<int> IDs)
	{
		var go = ObjectManager.GetNearestWoWGameObject(ObjectManager.GetWoWGameObjectByEntry(IDs));
		if (go != null && go.IsValid)
		{
			Gather(go.Position, go.Entry);
			return;
		}
		var npc = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(IDs));
		if (npc != null && npc.IsValid)
		{
			if (npc.IsAttackable)
			{
				Attack(npc);
			}
			else
			{
				InteractNPC(npc.Position, npc.Entry);
			}
			return;
		}
		var p = hotspots[Others.Random(0, hotspots.Count - 1)];
		GoToTask.ToPosition(p, 3.5f, false, (c) =>
		{
			var go2 = ObjectManager.GetNearestWoWGameObject(ObjectManager.GetWoWGameObjectByEntry(IDs));
			if (go2 != null && go2.IsValid)
				return false;

			var npc2 = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(IDs));
			if (npc2 != null && npc2.IsValid)
				return false;

			return true;
		});
	}

	public static bool ChangeQuestNPCPosition(int npcID, Vector3 pos)
	{
		var npc = Quest.NPCList.FirstOrDefault(n => n.Id == npcID);
		if (npc == null)
			return false;

		npc.Position = pos;
		Log("npc " + npc.Name + " changed to " + npc.Position);
		return true;
	}

	#endregion HELPERS


	#region SETTINGS
	public static void ResetSettings()
	{
		wManager.wManagerSetting.ClearBlacklistOfCurrentProductSession();
		wManager.wManagerSetting.CurrentSetting.LootMobs = true;
		wManager.wManagerSetting.CurrentSetting.LootChests = true;
		wManager.wManagerSetting.CurrentSetting.UseMail = false;
		wManager.wManagerSetting.CurrentSetting.SellGray = true;
		wManager.wManagerSetting.CurrentSetting.SellGreen = false;
		wManager.wManagerSetting.CurrentSetting.SellWhite = false;
		wManager.wManagerSetting.CurrentSetting.SellBlue = false;
		wManager.wManagerSetting.CurrentSetting.Repair = true;
		wManager.wManagerSetting.CurrentSetting.Selling = true;
		wManager.wManagerSetting.CurrentSetting.UseMammoth = true;
		wManager.wManagerSetting.CurrentSetting.AvoidBlacklistedZonesPathFinder = true;
		wManager.wManagerSetting.CurrentSetting.CloseIfPlayerTeleported = false;
		wManager.wManagerSetting.CurrentSetting.MaxUnitsNear = 99;
		wManager.wManagerSetting.CurrentSetting.CanAttackUnitsAlreadyInFight = true;
		wManager.wManagerSetting.CurrentSetting.AttackElite = true;
		wManager.wManagerSetting.CurrentSetting.AttackBeforeBeingAttacked = false;
		wManager.wManagerSetting.CurrentSetting.SearchRadius = 100;
		wManager.wManagerSetting.CurrentSetting.SkinNinja = false;
		wManager.wManagerSetting.CurrentSetting.SkinMobs = false;
		wManager.wManagerSetting.CurrentSetting.HarvestHerbs = false;
		wManager.wManagerSetting.CurrentSetting.HarvestMinerals = false;
		wManager.wManagerSetting.CurrentSetting.HarvestTimber = false;
		wManager.wManagerSetting.CurrentSetting.UseFlyingMount = true;
		wManager.wManagerSetting.CurrentSetting.UseGroundMount = true;
		wManager.wManagerSetting.CurrentSetting.UseMount = true;
		wManager.wManagerSetting.CurrentSetting.IgnoreFightGoundMount = false;
		wManager.wManagerSetting.CurrentSetting.SkipInOutDoors = false;
		wManager.wManagerSetting.CurrentSetting.RandomJumping = true;
		wManager.wManagerSetting.CurrentSetting.FlightMasterTaxiUse = true; //default wrobot
		wManager.wManagerSetting.CurrentSetting.FlightMasterTaxiUseOnlyIfNear = false; //default wrobot
		wManager.wManagerSetting.CurrentSetting.FlightMasterDiscoverRange = 150f; //default wrobot
		wManager.wManagerSetting.CurrentSetting.FlightMasterTaxiDistance = 1000f; //default wrobot
		wManager.wManagerSetting.CurrentSetting.HarvestAvoidPlayersRadius = 0; //ignore players near quest items
		wManager.wManagerSetting.CurrentSetting.SecurityPauseBotIfNerbyPlayer = false;
		wManager.wManagerSetting.CurrentSetting.SecurityPauseBotIfNerbyPlayerRadius = 0;
		wManager.wManagerSetting.CurrentSetting.BlackListIfNotCompletePath = true;
		wManager.wManagerSetting.CurrentSetting.AvoidWallWithRays = true;
		wManager.wManagerSetting.CurrentSetting.IgnoreCombatWithPet = true;
		wManager.wManagerSetting.CurrentSetting.AssignTalents = true;
		wManager.Wow.Helpers.CVar.SetCVar("autoDismount", "1");
		wManager.Wow.Helpers.CVar.SetCVar("autoDismountFlying", "1");
		wManager.Wow.Helpers.CVar.SetCVar("autoLootDefault", "1");
		wManager.Wow.Helpers.CVar.SetCVar("autounshift", "1");
		wManager.Wow.Helpers.Lua.LuaDoString("SetAutoDeclineGuildInvites(true)");
		wManager.Wow.Helpers.Conditions.ForceIgnoreIsAttacked = false;
		wManager.wManagerSetting.CurrentSetting.FoodPercent = 35;
		wManager.wManagerSetting.CurrentSetting.DrinkPercent = 35;
		wManager.wManagerSetting.CurrentSetting.NpcMailboxSearchRadius = 1000;
		//wManager.wManagerSetting.CurrentSetting.CalcuCombatRange = true;

		if (wManager.Wow.Bot.Tasks.FishingTask.IsLaunched)
			wManager.Wow.Bot.Tasks.FishingTask.StopLoopFish();

		if (wManager.Wow.ObjectManager.ObjectManager.Me.PlayerUsingVehicle)
			wManager.Wow.Helpers.Usefuls.EjectVehicle();

		BlacklistBadNPC();
		Log("reset settings");
	}
	#endregion SETTINGS


	#region SCENARIO
	public static class Scenario
	{
		public static bool IsButtonEnterPressed
		{
			get
			{
				var gui = "LFGDungeonReadyDialogEnterDungeonButton";
				if (Lua.LuaDoString<bool>("return " + gui + ":IsVisible()"))
				{

					Lua.LuaDoString(gui + ":Click()");
					return true;
				}
				return false;
			}
		}
		public static int Step
		{
			get
			{
				var lua = @"
local stageName, stageDescription, numCriteria, _, _, _, numSpells, spellInfo, weightedProgress = C_Scenario.GetStepInfo();
return numCriteria;
";
				return Lua.LuaDoString<int>(lua);
			}
		}

		public static int Stage
		{
			get
			{
				var lua = @"
local scenarioName, currentStage, numStages, flags, _, _, _, xp, money, scenarioType = C_Scenario.GetInfo();
return currentStage;
";
				return Lua.LuaDoString<int>(lua);
			}
		}

		public static int MaxStage
		{
			get
			{
				var lua = @"
local scenarioName, currentStage, numStages, flags, _, _, _, xp, money, scenarioType = C_Scenario.GetInfo();
return numStages;
";
				return Lua.LuaDoString<int>(lua);
			}
		}

		static string luaCriteria = @"
local criteriaString, criteriaType, completed, quantity, totalQuantity, flags, assetID, quantityString, criteriaID, duration, elapsed, _, isWeightedProgress = C_Scenario.GetCriteriaInfo({0});
";
		public static bool CriteriaComplete(int criteria)
		{
			var runCode = string.Format(luaCriteria + " return completed;", criteria);
			return Lua.LuaDoString<bool>(runCode);
		}
		public static int CriteriaQuantity(int criteria)
		{
			var runCode = string.Format(luaCriteria + " return quantity;", criteria);
			return Lua.LuaDoString<int>(runCode);
		}
		public static int CriteriaTotalQuantity(int criteria)
		{
			var runCode = string.Format(luaCriteria + " return totalQuantity;", criteria);
			return Lua.LuaDoString<int>(runCode);
		}

	}
	#endregion SCENARIO

	#region ACHIEVEMENT
	public static class Achievement
	{
		//11446 - Legion Flying

		public static bool Done(int achievID)
		{
			var lua = @"local id, name, points, completed, month, day, year, description, flags, icon, rewardText, isGuildAch, wasEarnedByMe, earnedBy = GetAchievementInfo({0}); return completed;";
			var runCode = string.Format(lua, achievID);
			return Lua.LuaDoString<bool>(runCode);
		}
		public static bool DoneCriteria(int achievID, int criteria)
		{
			var lua = @"local description, type, completed, quantity, requiredQuantity, characterName, flags, assetID, quantityString, criteriaID = GetAchievementCriteriaInfo({0}, {1}); return completed;";
			var runCode = string.Format(lua, achievID, criteria);
			return Lua.LuaDoString<bool>(runCode);
		}

	}


	//print("can fly in legion:" .. tostring(completed))
	#endregion ACHIEVEMENT

	#region VEHICLE
	public static class Vehicle
	{
		public static void Aim(WoWUnit unit, float precision = 0.5f, float agleDelta = 0f)
		{
			ClickToMove.CGPlayer_C__ClickToMove(unit.Position.X, unit.Position.Y, unit.Position.Z, unit.Guid, (int)ClickToMoveType.Move, precision);
			Thread.Sleep(Usefuls.Latency * 2);

			var dist = ObjectManager.Me.Position.DistanceTo2D(unit.Position);
			var height = unit.Position.Z - ObjectManager.Me.Position.Z;
			var angle = Math.GetAngle(dist, height);
			var radians = Math.DegreeToRadian(angle + agleDelta);
			AimAngle(radians);
		}
		static string _luaAngleGet = "return VehicleAimGetAngle();";
		static float _keybindingsSpeed = 0.0345576f; //per 1 ms press 0,0345576
		static void AimAngle(float radians)
		{
			var currentAngle = Lua.LuaDoString<float>(_luaAngleGet);
			var delta = radians - currentAngle;
			var timeMs = (int) System.Math.Abs(delta / _keybindingsSpeed);
			if (delta > 0)
				wManager.Wow.Helpers.Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.PITCHUP, timeMs);
			else if (delta < 0)
				wManager.Wow.Helpers.Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.PITCHDOWN, timeMs);
		}
	}
	#endregion VEHICLE

	static void BlacklistBadNPC()
	{
		wManager.wManagerSetting.AddBlackListNpcEntry(93974, true); //val'sharah, repair only for tailors, wrobot trying to repair but fail http://www.wowhead.com/npc=93974/leyweaver-erenyi

		//wManager.wManagerSetting.AddBlackListNpcEntry(99905, true); //(A) highmountain, vendor in thundertotem, can't fly/elevators http://www.wowhead.com/npc=99905/shale-greyfeather
		NpcDB.ListNpc.RemoveAll(npc => npc.Entry == 99905);
	}

}
// QUESTING END