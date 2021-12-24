using System;
using GordonRamsay.NPCs.GordonRamsay;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GordonRamsay.Items
{
    public class LambSauce : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a chef's worst nightmare.");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13;
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 26;
			item.maxStack = 20;
			item.rare = ItemRarityID.Cyan;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.consumable = true;
		}
        
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NPCs.GordonRamsay.GordonRamsay>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.GordonRamsay.GordonRamsay>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddTile(TileID.BoneWelder);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
