﻿using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Locations;

namespace MonsterMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod, IAssetLoader
    {
        Monster monster = null;
        int chestPop = 1;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.Player.Warped += this.PlayerWarped;
            helper.Events.GameLoop.OneSecondUpdateTicked += this.SecondUpdate;
            //helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;

            //Game1.player.chestConsumedMineLevels[10] = false;

        }

        /// <summary>Get whether this instance can load the initial version of the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanLoad<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Maps/Mines/10");
        }

        /// <summary>Load a matched asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public T Load<T>(IAssetInfo asset)
        {
            return this.Helper.Content.Load<T>("assets/10.tbin");
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window
            //this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }
        /*
        private void OnSaveLoaded(object sender, SaveLoadedEventArgs args)
        {
            // get the internal asset key for the map file
            string mapAssetKey = this.Helper.Content.GetActualAssetKey("assets/10.tbin", ContentSource.ModFolder);

            // add the location
            GameLocation mine10 = new GameLocation(mapAssetKey, "RevisedMineshaftTen") { IsOutdoors = false, IsFarm = false };
            Game1.locations.Add(mine10);
        }
        */

        private void SecondUpdate(object sender, OneSecondUpdateTickedEventArgs e)
        {
            int xTile = Game1.player.getTileX();
            int yTile = Game1.player.getTileY();

            Warp w = new Warp(9, 10, "UndergroundMine10", 9, 10, false);

            if (monster != null && monster.health > 0)
            {
                this.Monitor.Log("There's a monster alive somewhere!", LogLevel.Debug);
                this.Monitor.Log($"Its health is {monster.health}", LogLevel.Debug);
            }

            if (monster != null && monster.health <= 0 && chestPop == 1)
            {
                chestPop = 0;
                this.Monitor.Log("The monster is dead!", LogLevel.Debug);
                Game1.player.chestConsumedMineLevels.Remove(10);
                Game1.player.warpFarmer(w);
            }

            bool isAltPressed = this.Helper.Input.IsDown(SButton.LeftAlt);
            if (isAltPressed)
            {
                this.Monitor.Log("You are pressing left alt", LogLevel.Debug);
            }
            else this.Monitor.Log("Alt is not being pressed", LogLevel.Debug);
        }


        private void PlayerWarped(object sender, WarpedEventArgs e)
        {
            //prints that the character has warped
            this.Monitor.Log($"The player has warped to {e.NewLocation.name}!", LogLevel.Debug);
            string bannerNot = "The player has warped to " + e.NewLocation.name;
            Game1.addHUDMessage(new HUDMessage(bannerNot, 3));

            //Game1.player.chestConsumedMineLevels[10] = false;
            //StardewValley.Locations.MineShaft.permanentMineChanges[10].chestsLeft = -1;

            //MineShaft.permanentMineChanges[this.mineLevel].chestsLeft > numberSoFar;

            if (Game1.player.chestConsumedMineLevels.ContainsKey(10))
            {
                this.Monitor.Log("Theres a key to get rid of", LogLevel.Debug);
            }
            else this.Monitor.Log("The key is gone", LogLevel.Debug);

            //Game1.player.chestConsumedMineLevels.Remove(10);

            if (Game1.player.chestConsumedMineLevels.ContainsKey(10))
            {
                this.Monitor.Log("Theres a key to get rid of", LogLevel.Debug);
            }
            else this.Monitor.Log("The key is gone", LogLevel.Debug);



            //bool canAddChest = Game1.player.chestConsumedMineLevels[10];
            //int chestsLeft = StardewValley.Locations.MineShaft.permanentMineChanges[10].chestsLeft;

            //this.Monitor.Log($"The chestsLeft Val for mineshaft 10 is {chestsLeft}!", LogLevel.Debug);
            //this.Monitor.Log($"The chest consumed value for mineshaft 10 is {canAddChest}!", LogLevel.Debug);




            // String currentLocation = e.NewLocation;

            if (e.NewLocation.name == "UndergroundMine10")
            {

                Game1.player.position.X = 19;
                Game1.player.position.Y = 20;

                Monster monster1 = null;
                Vector2 spawnSpot = Game1.player.getTileLocation();
                monster1 = new GreenSlime(spawnSpot, 0);
                monster1.DamageToFarmer = 10;

                Monster monster2 = null;
                monster2 = new GreenSlime(spawnSpot, 0);
                monster2.DamageToFarmer = 10;

                Monster monster3 = null;
                monster3 = new GreenSlime(spawnSpot, 0);
                monster3.DamageToFarmer = 10;


                
                spawnSpot.X = 15;
                spawnSpot.Y = 10;
                this.monster = AddMonster("GreenSlime", spawnSpot);
                /*
                spawnSpot.X = 14;
                spawnSpot.Y = 10;
                monster2.currentLocation = location;
                monster2.setTileLocation(spawnSpot);
                location.addCharacter(monster2);

                spawnSpot.X = 14;
                spawnSpot.Y = 11;
                monster3.currentLocation = location;
                monster3.setTileLocation(spawnSpot);
                location.addCharacter(monster3);
                */

                //this.Monitor.Log("The player is in level 6 of the mines!", LogLevel.Debug);
            }

            //StardewValley.Monsters.Monster instance = (StardewValley.Monsters.Monster)Activator.CreateInstance(monsterData.Type, constructorArgs);
            //instance.currentLocation = Game1.currentLocation;

            //MonsterData monsterData = MonsterData.GetMonsterData(monster);

            if (e.NewLocation.name == "Farm")
            {
                this.Monitor.Log("The player on the farm, trying to load a monster.", LogLevel.Debug);

                Vector2 farmSpot = Game1.player.getTileLocation();
                this.Monitor.Log($"The position is {farmSpot}", LogLevel.Debug);


                Vector2 spawnSpot = Game1.player.getTileLocation();
                spawnSpot.Y += 3;
                this.monster = this.AddMonster("GreenSlime", spawnSpot);

                /*
                Monster monster = null;
                Vector2 spawnSpot = Game1.player.getTileLocation();
                monster = new Bat(spawnSpot, 0);
                monster.DamageToFarmer = 10;

                spawnSpot.Y += 5;
                GameLocation location = Game1.currentLocation;
                monster.currentLocation = location;
                monster.setTileLocation(spawnSpot);
                location.addCharacter(monster);
                */

                /*
                Vector2 spawnSpot = Game1.player.getTileLocation();
                string typeOfMonster = "Green Slime";
                object[] constructorArgs;
                constructorArgs[0] = spawnSpot;
                
                StardewValley.Monsters.Monster instance = (StardewValley.Monsters.Monster)Activator.CreateInstance(typeOfMonster, spawnSpot);
                instance.currentLocation = Game1.currentLocation;
                */
            }



        }

        public Monster AddMonster(string monsterName, Vector2 monsterLocation)
        {
            Monster monster = null;
            

            if (monsterName == "GreenSlime")
            {
                monster = new GreenSlime(monsterLocation, 0);
                GameLocation location = Game1.currentLocation;
                monster.currentLocation = location;
                monster.setTileLocation(monsterLocation);
                location.addCharacter(monster);

                return monster;
            }
            else return monster;


        }
    }

}