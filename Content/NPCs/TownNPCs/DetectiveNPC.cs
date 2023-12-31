﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using TheSkeletronMod.Common.Systems;

namespace TheSkeletronMod.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class DetectiveNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25;

            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 1;
            NPCID.Sets.AttackTime[Type] = 30;
            NPCID.Sets.AttackAverageChance[Type] = 5 ;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPC.Happiness
                //.SetBiomeAffection<TaintedBiome>(AffectionLevel.Love) This doesnt exist yet.
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Hate)

                .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Like)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
                .SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Like)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Clothier, AffectionLevel.Like)
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Pirate, AffectionLevel.Hate)
            ;
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 11;
            NPC.height = 25;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10;
            NPC.defense = 33;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.knockBackResist = 1f;

            AnimationType = NPCID.Guide;
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return toKingStatue;
        }
        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Casual1"), 1);
            chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Casual2"), 1);
            chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Casual3"), 1);
            chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Casual4"), 1);
            chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Casual5"), 1);

            if (Main.LocalPlayer.ZoneSnow)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Snow1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Snow2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Snow3"), 0.5);
            }

            /*
            if (Main.LocalPlayer.ZoneTainted)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Tainted1"), 2);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Tained2"), 1.5);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Tainted3"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Tainted3"), 0.5);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Tainted3"), 0.5);
            }
            */
            if (Main.LocalPlayer.ZoneForest)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Forest1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Forest2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Forest3"), 0.5);
            }
            if (Main.LocalPlayer.ZoneNormalUnderground || Main.LocalPlayer.ZoneNormalCaverns || Main.LocalPlayer.ZoneUnderworldHeight == true)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Underground1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Underground2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Underground3"), 0.5);
            }
            if (Main.LocalPlayer.ZoneBeach)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Ocean1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Ocean2"), 1);
            }
            if (Main.LocalPlayer.ZoneHallow)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Hallow1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Hallow2"), 1);
            }
            if (Main.LocalPlayer.ZoneDesert)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Desert"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Desert2"), 1);
            }
            if (Main.LocalPlayer.ZoneJungle)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Jungle1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Jungle"), 1);
            }

            //events
            if (Main.bloodMoon)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Bloodmoon1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Bloodmoon2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Bloodmoon3"), 1);
            }
            if(Main.snowMoon)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Frostmoon1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Frostmoon2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Frostmoon3"), 1);
            }
            if(Main.pumpkinMoon)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Pumpmoon1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Pumpmoon2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.Pumpmoon3"), 1);
            }
            if(Main.invasionType == InvasionID.GoblinArmy)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.GoblinInv1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.GoblinInv2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.GoblinInv3"), 1);
            }
            if (Main.invasionType == InvasionID.MartianMadness)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.MartianInv1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.MartianInv2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.MartianInv3"), 1);
            }
            if (Main.invasionType == InvasionID.PirateInvasion)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.PirateInv1"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.PirateInv2"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.PirateInv3"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.PirateInv4"), 1);
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.PirateInv5"), 1);
            }
            if (Main.invasionType == InvasionID.SnowLegion)
            {
                chat.Add(Language.GetTextValue("Mods.TheSkeletronMod.NPCs.DetectiveNPC.Dialogue.GangstaInv1"), 1);
            }

            return chat;
        }
        public override void HitEffect(NPC.HitInfo hitInfo)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                SoundEngine.PlaySound(SkelSound.DetectiveDeath);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DetectiveGoreHead").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DetectiveGoreArm").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DetectiveGoreArm").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DetectiveGoreLeg").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DetectiveGoreLeg").Type);
            }
        }
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Sherlock Bones",
                "Bones",
                "Skelelock Bones"
            };
        }

            public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
            {
            projType = ProjectileID.MeteorShot; 
            attackDelay = 1; 

            if (Main.hardMode)
            {
                projType = ProjectileID.ChlorophyteBullet;
            }
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 24f; 
            randomOffset = 0f; 
        }

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            if (!Main.hardMode)
            {
                Main.GetItemDrawFrame(ItemID.Handgun, out Texture2D itemTexture, out Rectangle itemRectangle);

                item = itemTexture;
                itemFrame = itemRectangle;
                scale = 0.75f; 
                horizontalHoldoutOffset = -7; 

                return; 
            }
            Main.GetItemDrawFrame(ItemID.Uzi, out Texture2D itemTexture2, out Rectangle itemRectangle2);
            item = itemTexture2;
            itemFrame = itemRectangle2;
            scale = 0.70f;
            horizontalHoldoutOffset = -7;
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 25;
            knockback = 3f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 10;
        }
    }
}
