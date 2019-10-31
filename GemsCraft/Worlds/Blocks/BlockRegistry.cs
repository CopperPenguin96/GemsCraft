using System;
using System.Collections.Generic;
using System.Threading;
using GemsCraft.AppSystem.Logging;

namespace GemsCraft.Worlds.Blocks
{
    public class BlockRegistry
    {
        public static List<Block> Blocks = new List<Block>();

        /// <summary>
        /// Loads all the block types into the registry
        /// </summary>
        public static void Load()
        {
            Logger.Write("Load the Block Registry...", LogType.System);

            #region Blocks

            Blocks.Add(Air);
            Blocks.Add(Stone);
            Blocks.Add(Granite);
            Blocks.Add(PolishedGranite);
            Blocks.Add(Diorite);
            Blocks.Add(PolishedDiorite);
            Blocks.Add(Andesite);
            Blocks.Add(PolishedAndesite);
            Blocks.Add(Grass);
            Blocks.Add(Dirt);
            Blocks.Add(CoarseDirt);
            Blocks.Add(Podzol);
            Blocks.Add(Cobblestone);
            Blocks.Add(OakWoodPlank);
            Blocks.Add(SpruceWoodPlank);
            Blocks.Add(BirchWoodPlank);
            Blocks.Add(JungleWoodPlank);
            Blocks.Add(AcaciaWoodPlank);
            Blocks.Add(DarkOakWoodPlank);
            Blocks.Add(OakSapling);
            Blocks.Add(SpruceSapling);
            Blocks.Add(BirchSapling);
            Blocks.Add(JungleSapling);
            Blocks.Add(AcaciaSapling);
            Blocks.Add(DarkOakSapling);
            Blocks.Add(Bedrock);
            Blocks.Add(FlowingWater);
            Blocks.Add(StillWater);
            Blocks.Add(FlowingLava);
            Blocks.Add(StillLava);
            Blocks.Add(Sand);
            Blocks.Add(RedSand);
            Blocks.Add(Gravel);
            Blocks.Add(GoldOre);
            Blocks.Add(IronOre);
            Blocks.Add(CoalCore);
            Blocks.Add(OakWood);
            Blocks.Add(SpruceWood);
            Blocks.Add(BirchWood);
            Blocks.Add(JungleWood);
            Blocks.Add(OakLeaves);
            Blocks.Add(SpruceLeaves);
            Blocks.Add(BirchLeaves);
            Blocks.Add(JungleLeaves);
            Blocks.Add(Sponge);
            Blocks.Add(WetSponge);
            Blocks.Add(Glass);
            Blocks.Add(LapisLazuliOre);
            Blocks.Add(LapisLazuliBlock);
            Blocks.Add(Dispenser);
            Blocks.Add(Sandstone);
            Blocks.Add(ChiseledSandstone);
            Blocks.Add(SmoothSandstone);
            Blocks.Add(NoteBlock);
            Blocks.Add(BedBlock);
            Blocks.Add(PoweredRail);
            Blocks.Add(DetectorRail);
            Blocks.Add(StickyPiston);
            Blocks.Add(Cobweb);
            Blocks.Add(DeadShrub);
            Blocks.Add(Grass);
            Blocks.Add(Fern);
            Blocks.Add(DeadBush);
            Blocks.Add(Piston);
            Blocks.Add(PistonHead);
            Blocks.Add(WhiteWool);
            Blocks.Add(OrangeWool);
            Blocks.Add(MagentaWool);
            Blocks.Add(LightBlueWool);
            Blocks.Add(YellowWool);
            Blocks.Add(LimeWool);
            Blocks.Add(PinkWool);
            Blocks.Add(GrayWool);
            Blocks.Add(LightGrayWool);
            Blocks.Add(CyanWool);
            Blocks.Add(PurpleWool);
            Blocks.Add(BlueWool);
            Blocks.Add(BrownWool);
            Blocks.Add(RedWool);
            Blocks.Add(BlackWool);
            Blocks.Add(Dandelion);
            Blocks.Add(Poppy);
            Blocks.Add(BlueOrchid);
            Blocks.Add(Allium);
            Blocks.Add(AzureBluet);
            Blocks.Add(RedTulip);
            Blocks.Add(OrangeTulip);
            Blocks.Add(WhiteTulip);
            Blocks.Add(PinkTulip);
            Blocks.Add(OxeyeDaisy);
            Blocks.Add(BrownMushroom);
            Blocks.Add(RedMushroom);
            Blocks.Add(GoldBlock);
            Blocks.Add(IronBlock);
            Blocks.Add(DoubleStoneSlab);
            Blocks.Add(DoubleSandstoneSlab);
            Blocks.Add(DoubleWoodenSlab);
            Blocks.Add(DoubleCobblestoneSlab);
            Blocks.Add(DoubleBrickSlab);
            Blocks.Add(DoubleStoneBrickSlab);
            Blocks.Add(DoubleNetherBrickSlab);
            Blocks.Add(DoubleQuartzSlab);
            Blocks.Add(StoneSlab);
            Blocks.Add(SandstoneSlab);
            Blocks.Add(WoodenSlab);
            Blocks.Add(CobblestoneSlab);
            Blocks.Add(BrickSlab);
            Blocks.Add(StoneBrickSlab);
            Blocks.Add(NetherBrickSlab);
            Blocks.Add(QuartzSlab);
            Blocks.Add(Bricks);
            Blocks.Add(TNT); // And KABOOM!
            Blocks.Add(Bookshelf);
            Blocks.Add(MossStone);
            Blocks.Add(Obsidian);
            Blocks.Add(Torch);
            Blocks.Add(Fire);
            Blocks.Add(MonsterSpawner);
            Blocks.Add(OakWoodStairs);
            Blocks.Add(Chest);
            Blocks.Add(RedstoneWire);
            Blocks.Add(DiamondOre);
            Blocks.Add(DiamondBlock);
            Blocks.Add(CraftingTable);
            Blocks.Add(WheatCrops);
            Blocks.Add(Farmland);
            Blocks.Add(Furnace);
            Blocks.Add(BurningFurnace);
            Blocks.Add(StandingSignBlock);
            Blocks.Add(OakDoorBlock);
            Blocks.Add(Ladder);
            Blocks.Add(Rail);
            Blocks.Add(CobblestoneStairs);
            Blocks.Add(WallMountedSignBlock);
            Blocks.Add(Lever);
            Blocks.Add(StonePressurePlate);
            Blocks.Add(IronDoorBlock);
            Blocks.Add(WoodenPressurePlate);
            Blocks.Add(RedstoneOre);
            Blocks.Add(GlowingRedstoneOre);
            Blocks.Add(RedstoneTorchOff);
            Blocks.Add(RedstoneTorchOn);
            Blocks.Add(StoneButton);
            Blocks.Add(Snow);
            Blocks.Add(Ice);
            Blocks.Add(SnowBlock);
            Blocks.Add(Cactus);
            Blocks.Add(Clay);
            Blocks.Add(SugarCanes);
            Blocks.Add(JukeBox);
            Blocks.Add(OakFence);
            Blocks.Add(Pumpkin);
            Blocks.Add(Netherrack);
            Blocks.Add(SoulSand);
            Blocks.Add(Glowstone);
            Blocks.Add(NetherPortal);
            Blocks.Add(JackOLantern);
            Blocks.Add(CakeBlock);
            Blocks.Add(RedstoneRepeaterOff);
            Blocks.Add(RedstoneRepeaterOn);
            Blocks.Add(WhiteStainedGlass);
            Blocks.Add(OrangeStainedGlass);
            Blocks.Add(MagentaStainedGlass);
            Blocks.Add(LightBlueStainedGlass);
            Blocks.Add(YellowStainedGlass);
            Blocks.Add(LimeStainedGlass);
            Blocks.Add(PinkStained);
            Blocks.Add(GrayStainedGlass);
            Blocks.Add(LightGrayStainedGlass);
            Blocks.Add(CyanStainedGlass);
            Blocks.Add(PurpleStainedGlass);
            Blocks.Add(BlueStainedGlass);
            Blocks.Add(BrownStainedGlass);
            Blocks.Add(GreenStainedGlass);
            Blocks.Add(RedStainedGlass);
            Blocks.Add(BlackStainedGlass);
            Blocks.Add(WoodenTrapdoor);
            Blocks.Add(StoneMonsterEgg);
            Blocks.Add(CobblestoneMonsterEgg);
            Blocks.Add(StoneBrickMonsterEgg);
            Blocks.Add(MossyStoneBrickMonsterEgg);
            Blocks.Add(CrackedStoneBrickMonsterEgg);
            Blocks.Add(StoneBricks);
            Blocks.Add(MossyStoneBricks);
            Blocks.Add(CrackedStoneBricks);
            Blocks.Add(ChiseledStoneBricks);
            Blocks.Add(BrownMushroom);
            Blocks.Add(RedMushroom);
            Blocks.Add(IronBars);
            Blocks.Add(GlassPane);
            Blocks.Add(MelonBlock);
            Blocks.Add(PumpkinStem);
            Blocks.Add(MelonStem);
            Blocks.Add(Vines);
            Blocks.Add(OakFenceGate);
            Blocks.Add(BrickStairs);
            Blocks.Add(StoneBrickStairs);
            Blocks.Add(Mycelium);
            Blocks.Add(LilyPad);
            Blocks.Add(NetherBrick);
            Blocks.Add(NetherBrickFence);
            Blocks.Add(NetherBrickStairs);
            Blocks.Add(NetherWart);
            Blocks.Add(EnchantmentTable);
            Blocks.Add(BrewingStand);
            Blocks.Add(CauldronBlock);
            Blocks.Add(EndPortal);
            Blocks.Add(EndPortalFrame);
            Blocks.Add(EndStone);
            Blocks.Add(DragonEgg);
            Blocks.Add(RedstoneLampInactive);
            Blocks.Add(RedstoneLampActive);
            Blocks.Add(DoubleOakWoodSlab);
            Blocks.Add(DoubleSpruceWoodSlab);
            Blocks.Add(DoubleBirchWoodSlab);
            Blocks.Add(DoubleJungleWoodSlab);
            Blocks.Add(DoubleAcaciaWoodSlab);
            Blocks.Add(DoubleDarkOakWoodSlab);
            Blocks.Add(OakWoodSlab);
            Blocks.Add(SpruceWoodSlab);
            Blocks.Add(BirchWoodSlab);
            Blocks.Add(JungleWoodSlab);
            Blocks.Add(AcaciaWoodSlab);
            Blocks.Add(DarkOakWoodSlab);
            Blocks.Add(Cocoa);
            Blocks.Add(SandstoneStairs);
            Blocks.Add(EmeraldOre);
            Blocks.Add(EnderChest);
            Blocks.Add(TripwireHook);
            Blocks.Add(Tripwire);
            Blocks.Add(EmeraldBlock);
            Blocks.Add(SpruceWoodStairs);
            Blocks.Add(BirchWoodStairs);
            Blocks.Add(JungleWoodStairs);
            Blocks.Add(CommandBlock);
            Blocks.Add(Beacon);
            Blocks.Add(CobblestoneWall);
            Blocks.Add(MossyCobblestoneWall);
            Blocks.Add(FlowerPot);
            Blocks.Add(Carrots);
            Blocks.Add(Potatoes);
            Blocks.Add(WoodenButton);
            Blocks.Add(MobHead);
            Blocks.Add(Anvil);
            Blocks.Add(TrappedChest);
            Blocks.Add(WeightedPressurePlateLight);
            Blocks.Add(WeightedPressurePlateHeavy);
            Blocks.Add(RedstoneComparatorInactive);
            Blocks.Add(RedstoneComparatorActive);
            Blocks.Add(DaylightSensor);
            Blocks.Add(RedstoneBlock);
            Blocks.Add(NetherQuartzOre);
            Blocks.Add(Hopper);
            Blocks.Add(QuartzBlock);
            Blocks.Add(ChiseledQuartzBlock);
            Blocks.Add(PillarQuartzBlock);
            Blocks.Add(QuartzStairs);
            Blocks.Add(ActivatorRail);
            Blocks.Add(Dropper);
            Blocks.Add(WhiteHardenedClay);
            Blocks.Add(OrangeHardenedClay);
            Blocks.Add(MagentaHardenedClay);
            Blocks.Add(LightBlueHardenedClay);
            Blocks.Add(YellowHardenedClay);
            Blocks.Add(LimeHardenedClay);
            Blocks.Add(PinkHardenedClay);
            Blocks.Add(GrayHardenedClay);
            Blocks.Add(LightGrayHardenedClay);
            Blocks.Add(CyanHardenedClay);
            Blocks.Add(PurpleHardenedClay);
            Blocks.Add(BlueHardenedClay);
            Blocks.Add(BrownHardendClay);
            Blocks.Add(GreenHardenedClay);
            Blocks.Add(RedHardenedClay);
            Blocks.Add(BlackHardenedClay);
            Blocks.Add(WhiteStainedGlassPane);
            Blocks.Add(OrangeStainedGlassPane);
            Blocks.Add(MagentaStainedGlassPane);
            Blocks.Add(LightBlueStainedGlasPane);
            Blocks.Add(YellowStianedGlassPane);
            Blocks.Add(LimeStainedGlassPane);
            Blocks.Add(PinkStainedGlassPane);
            Blocks.Add(GrayStainedGlassPane);
            Blocks.Add(LightGrayStainedGlassPane);
            Blocks.Add(CyanStainedGlassPane);
            Blocks.Add(PurpleStainedGlassPane);
            Blocks.Add(BlueStainedGlassPane);
            Blocks.Add(BrownStainedGlassPane);
            Blocks.Add(GreenStainedGlassPane);
            Blocks.Add(RedStainedGlassPane);
            Blocks.Add(BlackStainedGlassPane);
            Blocks.Add(AcaciaLeaves);
            Blocks.Add(DarkOakLeaves);
            Blocks.Add(AcaciaWood);
            Blocks.Add(DarkOakWood);
            Blocks.Add(AcaciaWoodStairs);
            Blocks.Add(DarkOakWoodStairs);
            Blocks.Add(SlimeBlock);
            Blocks.Add(Barrier);
            Blocks.Add(IronTrapdoor);
            Blocks.Add(Prismarine);
            Blocks.Add(PrismarineBricks);
            Blocks.Add(DarkPrismarine);
            Blocks.Add(SeaLantern);
            Blocks.Add(HayBale);
            Blocks.Add(WhiteCarpet);
            Blocks.Add(OrangeCarpet);
            Blocks.Add(MagentaCarpet);
            Blocks.Add(LightBlueCarpet);
            Blocks.Add(YellowCarpet);
            Blocks.Add(LimeCarpet);
            Blocks.Add(PinkCarpet);
            Blocks.Add(GrayCarpet);
            Blocks.Add(LightGrayCarpet);
            Blocks.Add(CyanCarpet);
            Blocks.Add(PurpleCarpet);
            Blocks.Add(BlueCarpet);
            Blocks.Add(BrownCarpet);
            Blocks.Add(GreenCarpet);
            Blocks.Add(RedCarpet);
            Blocks.Add(BlackCarpet);
            Blocks.Add(HardenedClay);
            Blocks.Add(BlockOfCoal);
            Blocks.Add(PackedIce);
            Blocks.Add(Sunflower);
            Blocks.Add(Lilac);
            Blocks.Add(DoubleTallgrass);
            Blocks.Add(LargeFern);
            Blocks.Add(RoseBush);
            Blocks.Add(Peony);
            Blocks.Add(FreeStandingBanner);
            Blocks.Add(WallMountedBanner);
            Blocks.Add(InvertedDaylightSensor);
            Blocks.Add(RedSandstone);
            Blocks.Add(ChiseledRedSandstone);
            Blocks.Add(SmoothRedSandstone);
            Blocks.Add(RedSandstoneStairs);
            Blocks.Add(DoubleRedSandstoneSlab);
            Blocks.Add(RedSandstoneSlab);
            Blocks.Add(SpruceFenceGate);
            Blocks.Add(BirchFenceGate);
            Blocks.Add(JungleFenceGate);
            Blocks.Add(DarkOakFenceGate);
            Blocks.Add(AcaciaFenceGate);
            Blocks.Add(SpruceFence);
            Blocks.Add(BirchFence);
            Blocks.Add(JungleFence);
            Blocks.Add(DarkOakFence);
            Blocks.Add(AcaciaFence);
            Blocks.Add(SpruceDoorBlock);
            Blocks.Add(BirchDoorBlock);
            Blocks.Add(JungleDoorBlock);
            Blocks.Add(AcaciaDoorBlock);
            Blocks.Add(DarkOakDoorBlock);
            Blocks.Add(EndRod);
            Blocks.Add(ChorusPlant);
            Blocks.Add(ChorusFlower);
            Blocks.Add(PurpurBlock);
            Blocks.Add(PurpurPillar);
            Blocks.Add(PurpurStairs);
            Blocks.Add(PurpurDoubleSlab);
            Blocks.Add(PurpurSlab);
            Blocks.Add(EndStoneBricks);
            Blocks.Add(BeetrootBlock);
            Blocks.Add(GrassPath);
            Blocks.Add(EndGateway);
            Blocks.Add(RepeatingCommandBlock);
            Blocks.Add(FrostedIce);
            Blocks.Add(MagmaBlock);
            Blocks.Add(NetherWartBlock);
            Blocks.Add(RedNetherBrick);
            Blocks.Add(BoneBlock);
            Blocks.Add(StructureVoid);
            Blocks.Add(Observer);
            Blocks.Add(WhiteShulkerBox);
            Blocks.Add(OrangeShulkerBox);
            Blocks.Add(MagentaShulkerBox);
            Blocks.Add(LightBlueShulkerBox);
            Blocks.Add(YellowShulkerBox);
            Blocks.Add(LimeShulkerBox);
            Blocks.Add(PinkShulkerBox);
            Blocks.Add(GrayShulkerBox);
            Blocks.Add(LightGrayShulkerBox);
            Blocks.Add(CyanShulkerBox);
            Blocks.Add(PurpleShulkerBox);
            Blocks.Add(BlueShulkerBox);
            Blocks.Add(BrownShulkerBox);
            Blocks.Add(GreenShulkerBox);
            Blocks.Add(RedShulkerBox);
            Blocks.Add(BlackShulkerBox);
            Blocks.Add(WhiteGlazedTerracotta);
            Blocks.Add(OrangeGlazedTerracotta);
            Blocks.Add(MagentaGlazedTerracotta);
            Blocks.Add(LightBlueGlazedTerracotta);
            Blocks.Add(YellowGlazedTerracotta);
            Blocks.Add(LimeGlazedTerracotta);
            Blocks.Add(PinkGlazedTerracotta);
            Blocks.Add(GrayGlazedTerracotta);
            Blocks.Add(LightGrayGlazedTerracotta);
            Blocks.Add(CyanGlazedTerracotta);
            Blocks.Add(PurpleGlazedTerracotta);
            Blocks.Add(BlueGlazedTerracotta);
            Blocks.Add(BrownGlazedTerracotta);
            Blocks.Add(GreenGlazedTerracotta);
            Blocks.Add(RedGlazedTerracotta);
            Blocks.Add(BlackGlazedTerracotta);
            Blocks.Add(WhiteConcrete);
            Blocks.Add(OrangeConcrete);
            Blocks.Add(MagentaConcrete);
            Blocks.Add(LightBlueConcrete);
            Blocks.Add(YellowConcrete);
            Blocks.Add(LimeConcrete);
            Blocks.Add(PinkConcrete);
            Blocks.Add(GrayConcrete);
            Blocks.Add(LightGrayConcrete);
            Blocks.Add(CyanConcrete);
            Blocks.Add(PurpleConcrete);
            Blocks.Add(BlueConcrete);
            Blocks.Add(BrownConcrete);
            Blocks.Add(GreenConcrete);
            Blocks.Add(RedConcrete);
            Blocks.Add(BlackConcrete);
            Blocks.Add(WhiteConcretePowder);
            Blocks.Add(OrangeConcretePowder);
            Blocks.Add(MagentaConcretePowder);
            Blocks.Add(LightBlueConcretePowder);
            Blocks.Add(YellowConcretePowder);
            Blocks.Add(LimeConcretePowder);
            Blocks.Add(PinkConcretePowder);
            Blocks.Add(GrayConcretePowder);
            Blocks.Add(LightGrayConcretePowder);
            Blocks.Add(CyanConcretePowder);
            Blocks.Add(PurpleConcretePowder);
            Blocks.Add(BlueConcretePowder);
            Blocks.Add(BrownConcretePowder);
            Blocks.Add(GreenConcretePowder);
            Blocks.Add(RedConcretePowder);
            Blocks.Add(BlackConcretePowder);
            Blocks.Add(StructureBlock);
            Blocks.Add(IronShovel);
            Blocks.Add(IronPickaxe);
            Blocks.Add(IronAxe);
            Blocks.Add(FlintAndSteel);
            Blocks.Add(Apple); // Windows is way better :/
            Blocks.Add(Bow);
            Blocks.Add(Arrow); // That's a good show!
            Blocks.Add(Coal); // What you're getting for Christmas
            Blocks.Add(Charcoal);
            Blocks.Add(Diamond); // Every girl's wish
            Blocks.Add(IronIngot);
            Blocks.Add(GoldIngot);
            Blocks.Add(IronSword);
            Blocks.Add(WoodenSword);
            Blocks.Add(WoodenShovel);
            Blocks.Add(WoodenPickaxe);
            Blocks.Add(WoodenAxe);
            Blocks.Add(StoneSword);
            Blocks.Add(StoneShovel);
            Blocks.Add(StonePickaxe);
            Blocks.Add(StoneAxe);
            Blocks.Add(DiamondSword);
            Blocks.Add(DiamondShovel);
            Blocks.Add(DiamondPickaxe);
            Blocks.Add(DiamondAxe);
            Blocks.Add(Stick);
            Blocks.Add(Bowl);
            Blocks.Add(MushroomStew);
            Blocks.Add(GoldenSword);
            Blocks.Add(GoldenShovel);
            Blocks.Add(GoldenPickaxe);
            Blocks.Add(GoldenAxe);
            Blocks.Add(String);
            Blocks.Add(Feather);
            Blocks.Add(Gunpowder);
            Blocks.Add(WoodenHoe); // Watch your mouth!
            Blocks.Add(StoneHoe);
            Blocks.Add(IronHoe);
            Blocks.Add(DiamondHoe);
            Blocks.Add(GoldenHoe);
            Blocks.Add(WheatSeeds);
            Blocks.Add(Wheat);
            Blocks.Add(Bread);
            Blocks.Add(LeatherHelmet);
            Blocks.Add(LeatherTunic);
            Blocks.Add(LeatherPants);
            Blocks.Add(LeatherBoots);
            Blocks.Add(ChainmailHelmet);
            Blocks.Add(ChainmailCheatplate);
            Blocks.Add(ChainmailLeggings);
            Blocks.Add(ChainmailBoots);
            Blocks.Add(IronHelmet);
            Blocks.Add(IronChestplate);
            Blocks.Add(IronLeggings);
            Blocks.Add(IronBoots);
            Blocks.Add(DiamondHelmet);
            Blocks.Add(DiamondChestplate);
            Blocks.Add(DiamondLeggings);
            Blocks.Add(DiamondBoots);
            Blocks.Add(GoldenHelmet);
            Blocks.Add(GoldenChestplate);
            Blocks.Add(GoldenLeggings);
            Blocks.Add(GoldenBoots);
            Blocks.Add(Flint);
            Blocks.Add(RawPorkchop);
            Blocks.Add(CookedPorkchop);
            Blocks.Add(Painting);
            Blocks.Add(GoldenApple);
            Blocks.Add(EnchantedGoldenApple);
            Blocks.Add(Sign);
            Blocks.Add(OakDoor);
            Blocks.Add(Bucket);
            Blocks.Add(WaterBucket);
            Blocks.Add(LavaBucket);
            Blocks.Add(Minecart);
            Blocks.Add(Saddle);
            Blocks.Add(IronDoor);
            Blocks.Add(Redstone);
            Blocks.Add(Snowball);
            Blocks.Add(Oakboat);
            Blocks.Add(Leather);
            Blocks.Add(MilkBucket);
            Blocks.Add(Brick);
            Blocks.Add(Clay);
            Blocks.Add(SugarCanes);
            Blocks.Add(Paper);
            Blocks.Add(Book);
            Blocks.Add(Slimeball);
            Blocks.Add(MinecartWithChest);
            Blocks.Add(MinecartWithFurnace);
            Blocks.Add(Egg);
            Blocks.Add(Compass);
            Blocks.Add(FishingRod);
            Blocks.Add(Clock);
            Blocks.Add(GlowstoneDust);
            Blocks.Add(RawFish);
            Blocks.Add(RawSalmon);
            Blocks.Add(Clownfish);
            Blocks.Add(Pufferfish);
            Blocks.Add(CookedFish);
            Blocks.Add(CookedSalmon);
            Blocks.Add(InkSack);
            Blocks.Add(RoseRed);
            Blocks.Add(CactusGreen);
            Blocks.Add(CocoBeans);
            Blocks.Add(LapisLazuli);
            Blocks.Add(PurpleDye);
            Blocks.Add(CyanDye);
            Blocks.Add(LightGrayDye);
            Blocks.Add(GrayDye);
            Blocks.Add(PinkDye);
            Blocks.Add(LimeDye);
            Blocks.Add(DanelionYellow);
            Blocks.Add(LightBlueDye);
            Blocks.Add(MagentaDye);
            Blocks.Add(OrangeDye);
            Blocks.Add(BoneMeal);
            Blocks.Add(Bone);
            Blocks.Add(Sugar);
            Blocks.Add(Cake);
            Blocks.Add(Bed);
            Blocks.Add(RedstoneRepeater);
            Blocks.Add(Cookie);
            Blocks.Add(Map);
            Blocks.Add(Shears);
            Blocks.Add(Melon);
            Blocks.Add(PumpkinSeeds);
            Blocks.Add(MelonSeeds);
            Blocks.Add(RawBeef);
            Blocks.Add(Steak);
            Blocks.Add(RawChicken);
            Blocks.Add(CookedChicken);
            Blocks.Add(RottenFlesh);
            Blocks.Add(EnderPearl);
            Blocks.Add(BlazeRod);
            Blocks.Add(GhostTear);
            Blocks.Add(GoldNugget);
            Blocks.Add(NetherWart);
            Blocks.Add(Potion);
            Blocks.Add(GlassBottle);
            Blocks.Add(SpiderEye);
            Blocks.Add(FermentedSpiderEye);
            Blocks.Add(BlazePowder);
            Blocks.Add(MagmaCream);
            Blocks.Add(BrewingStand);
            Blocks.Add(Cauldron);
            Blocks.Add(EyeOfEnder);
            Blocks.Add(GlisteringMelon);
            Blocks.Add(SpawnElderGuardian);
            Blocks.Add(SpawnWitherSkeleton);
            Blocks.Add(SpawnStray);
            Blocks.Add(SpawnHusk);
            Blocks.Add(SpawnZombieVillager);
            Blocks.Add(SpawnSkeletonHorse);
            Blocks.Add(SpawnZombieHorse);
            Blocks.Add(SpawnDonkey);
            Blocks.Add(SpawnMule);
            Blocks.Add(SpawnEvoker);
            Blocks.Add(SpawnVex);
            Blocks.Add(SpawnVindicator);
            Blocks.Add(SpawnCreeper); // sssssssssssssss. BOOM
            Blocks.Add(SpawnSkeleton);
            Blocks.Add(SpawnSpider);
            Blocks.Add(SpawnZombie);
            Blocks.Add(SpawnSlime);
            Blocks.Add(SpawnGhast); // Oh my ghast
            Blocks.Add(SpawnZombiePigman);
            Blocks.Add(SpawnEnderman);
            Blocks.Add(SpawnCaveSpider);
            Blocks.Add(SpawnSilverfish);
            Blocks.Add(SpawnBlaze);
            Blocks.Add(SpawnMagmaCube);
            Blocks.Add(SpawnBat);
            Blocks.Add(SpawnWitch);
            Blocks.Add(SpawnEndermite);
            Blocks.Add(SpawnElderGuardian);
            Blocks.Add(SpawnShulker);
            Blocks.Add(SpawnPig); // Oink
            Blocks.Add(SpawnSheep); // People who clone server software rather than making their own
            Blocks.Add(SpawnCow); // Holy cow!
            Blocks.Add(SpawnChicken);
            Blocks.Add(SpawnSquid);
            Blocks.Add(SpawnWolf);
            Blocks.Add(SpawnMooshroom);
            Blocks.Add(SpawnOcelot);
            Blocks.Add(SpawnHorse);
            Blocks.Add(SpawnRabbit);
            Blocks.Add(SpawnPolarBear);
            Blocks.Add(SpawnLlama); // sul sul
            Blocks.Add(SpawnParrot);
            Blocks.Add(SpawnVillager); // herm
            Blocks.Add(BottleOEnchanting);
            Blocks.Add(FireCharge);
            Blocks.Add(BookAndQuill);
            Blocks.Add(WrittenBook);
            Blocks.Add(Emerald);
            Blocks.Add(ItemFrame);
            Blocks.Add(FlowerPot);
            Blocks.Add(Carrot);
            Blocks.Add(Potato);
            Blocks.Add(BakedPotato);
            Blocks.Add(PoisonousPotato);
            Blocks.Add(EmptyMap);
            Blocks.Add(GoldenCarrot);
            Blocks.Add(MobHeadSkeleton);
            Blocks.Add(MobHeadWitherSkeleton);
            Blocks.Add(MobHeadZombie);
            Blocks.Add(MobHeadHuman);
            Blocks.Add(MobHeadCreeper);
            Blocks.Add(MobHeadDragon); // rawr
            Blocks.Add(CarrotOnAStick); // wtf?
            Blocks.Add(NetherStar);
            Blocks.Add(PumpkinPie);
            Blocks.Add(FireworkRocket);
            Blocks.Add(FireworkStar);
            Blocks.Add(EnchantedBook);
            Blocks.Add(RedstoneComparator);
            Blocks.Add(NetherBrick);
            Blocks.Add(NetherQuartzOre);
            Blocks.Add(MinecraftWithTNT);
            Blocks.Add(MinecartWithHopper);
            Blocks.Add(PrismarineShard);
            Blocks.Add(PrismarineCrystals);
            Blocks.Add(RawRabbit);
            Blocks.Add(CookedRabbit);
            Blocks.Add(RabbitStew);
            Blocks.Add(RabbitsFoot);
            Blocks.Add(ArmorStand);
            Blocks.Add(IronHorseArmor);
            Blocks.Add(GoldenHorseArmor);
            Blocks.Add(DiamondHorseArmor);
            Blocks.Add(Lead);
            Blocks.Add(NameTag);
            Blocks.Add(MinecartWithCommandBlock);
            Blocks.Add(RawMutton);
            Blocks.Add(CookedMutton);
            Blocks.Add(Banner);
            Blocks.Add(EndCrystal);
            Blocks.Add(SpruceDoor);
            Blocks.Add(BirchDoor);
            Blocks.Add(JungleDoor);
            Blocks.Add(AcaciaDoor);
            Blocks.Add(DarkOakDoor);
            Blocks.Add(ChorusFruit);
            Blocks.Add(PoppedChorusFruit);
            Blocks.Add(Beetroot); // uh wut?
            Blocks.Add(BeetrootSeeds);
            Blocks.Add(BeetrootSoup);
            Blocks.Add(DragonsBreath);
            Blocks.Add(SplashPotion);
            Blocks.Add(SpectralArrow);
            Blocks.Add(TippedArrow);
            Blocks.Add(LingeringPotion);
            Blocks.Add(Shield);
            Blocks.Add(Elytra);
            Blocks.Add(SpruceBoat);
            Blocks.Add(BirchBoat);
            Blocks.Add(JungleBoat);
            Blocks.Add(AcaciaBoat);
            Blocks.Add(DarkOakBoat);
            Blocks.Add(TotemOfUndying);
            Blocks.Add(ShulkerShell);
            Blocks.Add(IronNugget);
            Blocks.Add(KnowledgeBook);
            Blocks.Add(Disc13);
            Blocks.Add(CatDisc);
            Blocks.Add(BlocksDisc);
            Blocks.Add(ChirpDisc);
            Blocks.Add(FarDisc);
            Blocks.Add(MallDisc);
            Blocks.Add(MellohiDisc);
            Blocks.Add(StalDisc);
            Blocks.Add(WardDisc);
            Blocks.Add(Disc11);
            Blocks.Add(WaitDisc);

            #endregion
        }

        public static readonly Block String = new Block(287, 0)
        {
            FullID = new FullID("minecraft", "string"),
            Name = "String"
        };

        public static readonly Block Barrier = new Block(165, 0)
        {
            FullID = new FullID("minecraft", "barrier"),
            Name = "Barrier"
        };

        public static readonly Block Air = new Block(0, 0)
        {
            FullID = new FullID("minecraft", "air"),
            Name = "Air",
            Hardness = 0,
            MinStateID = new BlockID(0, 0),
            MaxStateID = new BlockID(0, 0),
            Drops = new []
            {
                new BlockID(0, 0)
            },
            Diggable = true,
            Transparent = true,
            FilterLight = 0,
            EmitLight = 0,
            BoundingBox = "empty",
            StackSize = 0
        };

        public static readonly Block Stone = new Block(1, 0)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Stone",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 0),
            MaxStateID = new BlockID(1, 0),
            Drops = new[]
            {
                new BlockID(4, 0), 
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new[]
            {
                new BlockID(270, 0), // Wooden pickaxe
                new BlockID(274, 0), // Stone pickaxe
                new BlockID(267, 0), // Iron pickaxe
                new BlockID(278, 0), // Diamond pickaxe
                new BlockID(285, 0)  // Golden pickaxe 
            }
        };

        public static readonly Block Granite = new Block(1, 1)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Granite",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 1),
            MaxStateID = new BlockID(1, 1),
            Drops = new[]
            {
                new BlockID(1, 1)
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new []
            {
                new BlockID(270, 0),
                new BlockID(274, 0),
                new BlockID(267, 0),
                new BlockID(278, 0),
                new BlockID(285, 0)
            }
        };

        public static readonly Block PolishedGranite = new Block(1, 2)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Polished Granite",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 2),
            MaxStateID = new BlockID(1, 2),
            Drops = new []
            {
                new BlockID(1, 2) 
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new[]
            {
                new BlockID(270, 0),
                new BlockID(274, 0),
                new BlockID(267, 0),
                new BlockID(278, 0),
                new BlockID(285, 0)
            }
        };

        public static readonly Block Diorite = new Block(1, 3)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Diorite",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 3),
            MaxStateID = new BlockID(1, 3),
            Drops = new[]
            {
                new BlockID(1, 3) 
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new []
            {
                new BlockID(270, 0),
                new BlockID(274, 0),
                new BlockID(267, 0),
                new BlockID(278, 0),
                new BlockID(285, 0)
            }
        };

        public static readonly Block PolishedDiorite = new Block(1, 4)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Polished Diorite",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 4),
            MaxStateID = new BlockID(1, 4),
            Drops = new[]
            {
                new BlockID(1, 4) 
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new[]
            {
                new BlockID(270, 0),
                new BlockID(274, 0),
                new BlockID(267, 0),
                new BlockID(278, 0),
                new BlockID(285, 0)
            }
        };

        public static readonly Block Andesite = new Block(1, 5)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Andesite",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 5),
            MaxStateID = new BlockID(1, 5),
            Drops = new[]
            {
                new BlockID(1, 5) 
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new[]
            {
                new BlockID(270, 0),
                new BlockID(274, 0),
                new BlockID(267, 0),
                new BlockID(278, 0),
                new BlockID(285, 0)
            }
        };

        public static readonly Block PolishedAndesite = new Block(1, 6)
        {
            FullID = new FullID("minecraft", "stone"),
            Name = "Polished Andesite",
            Hardness = 1.5,
            MinStateID = new BlockID(1, 6),
            MaxStateID = new BlockID(1, 6),
            Drops = new[]
            {
                new BlockID(1, 6) 
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "rock",
            HarvestTools = new[]
            {
                new BlockID(270, 0),
                new BlockID(274, 0),
                new BlockID(267, 0),
                new BlockID(278, 0),
                new BlockID(285, 0)
            }
        };

        public static readonly Block Grass = new Block(2, 0)
        {
            FullID = new FullID("minecraft", "grass"),
            Name = "Grass",
            Hardness = 0.6,
            MinStateID = new BlockID(2, 0),
            MaxStateID = new BlockID(2, 1),
            States = new[]
            {
                new BlockState
                {
                    Name = "Snowy",
                    Type = "bool",
                    NumValues = 2
                },
            },
            Drops = new[]
            {
                new BlockID(2, 0)
            },
            Diggable = true,
            Transparent = false,
            FilterLight = 15,
            EmitLight = 0,
            BoundingBox = "block",
            StackSize = 64,
            Material = "plant"
        };

        public static readonly Block Dirt = new Block(3, 0)
        {
            FullID = new FullID("minecraft", "dirt"),
            Name = "Dirt",
            StackSize = 64
        };

        public static readonly Block CoarseDirt = new Block(3, 1)
        {
            FullID = new FullID("minecraft", "dirt"),
            Name = "Coarse Dirt",
            StackSize = 64
        };

        public static readonly Block Podzol = new Block(3, 2)
        {
            FullID = new FullID("minecraft", "dirt"),
            Name = "Podzol",
            StackSize = 64
        };

        public static readonly Block Cobblestone = new Block(4, 0)
        {
            FullID = new FullID("minecraft", "cobblestone"),
            Name = "Cobblestone",
            StackSize = 64
        };

        public static readonly Block OakWoodPlank = new Block(5, 0)
        {
            FullID = new FullID("minecraft", "planks"),
            Name = "Oak Wood Plank",
            StackSize = 64
        };

        public static readonly Block SpruceWoodPlank = new Block(5, 1)
        {
            FullID = new FullID("minecraft", "planks"),
            Name = "Spruce Wood Plank",
            StackSize = 64
        };

        public static readonly Block BirchWoodPlank = new Block(5, 2)
        {
            FullID = new FullID("minecraft", "planks"),
            Name = "Birch Wood Plank",
            StackSize = 64
        };

        public static readonly Block JungleWoodPlank = new Block(5, 3)
        {
            FullID = new FullID("minecraft", "planks"),
            Name = "Jungle Wood Plank",
            StackSize = 64
        };

        public static readonly Block AcaciaWoodPlank = new Block(5, 4)
        {
            FullID = new FullID("minecraft", "planks"),
            Name = "Acacia Wood Plank",
            StackSize = 64
        };

        public static readonly Block DarkOakWoodPlank = new Block(5, 5)
        {
            FullID = new FullID("minecraft", "planks"),
            Name = "Dark Oak Wood Plank",
            StackSize = 64
        };

        public static readonly Block OakSapling = new Block(6, 0)
        {
            FullID = new FullID("minecraft", "sapling"),
            Name = "Oak Sapling",
            StackSize = 64
        };

        public static readonly Block SpruceSapling = new Block(6, 1)
        {
            FullID = new FullID("minecraft", "sapling"),
            Name = "Spruce Sapling",
            StackSize = 64
        };

        public static readonly Block BirchSapling = new Block(6, 2)
        {
            FullID = new FullID("minecraft", "sapling"),
            Name = "Birch Sapling",
            StackSize = 64
        };

        public static readonly Block JungleSapling = new Block(6, 3)
        {
            FullID = new FullID("minecraft", "sapling"),
            Name = "Jungle Sapling",
            StackSize = 64
        };

        public static readonly Block AcaciaSapling = new Block(6, 4)
        {
            FullID = new FullID("minecraft", "sapling"),
            Name = "Acacia Sapling",
            StackSize = 64
        };

        public static readonly Block DarkOakSapling = new Block(6, 5)
        {
            FullID = new FullID("minecraft", "sapling"),
            Name = "Dark Oak Sapling",
            StackSize = 64
        };

        public static readonly Block Bedrock = new Block(7, 0)
        {
            FullID = new FullID("minecraft", "bedrock"),
            Name = "Bedrock",
            StackSize = 64
        };

        public static readonly Block FlowingWater = new Block(8, 0)
        {
            FullID = new FullID("minecraft", "flowing_water"),
            Name = "Flowing Water",
            StackSize = 0
        };
    }
}
