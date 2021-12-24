using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using GordonRamsay.Projectiles;

namespace GordonRamsay.Items
{
    public class SwearWords : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gordon's Book of Swear Words"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Damages everything around you");
        }

		public override void SetDefaults()
		{
			item.damage = 20;
			item.magic = true;
			item.noMelee = true;
			item.mana = 10;
			item.width = 30;
			item.height = 30;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 5;
			item.knockBack = 0;
			item.value = 1000000;
			item.rare = 5;
			item.shoot = ModContent.ProjectileType<SwearWord>();
			item.shootSpeed = 0f;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/TVBleep75");
			item.autoReuse = false;
		}
        public override bool UseItem(Player player)
        {
			Dust dust;
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			Vector2 position = Main.LocalPlayer.Center - new Vector2(-400, -400);
			dust = Main.dust[Dust.NewDust(position, 800, 800, 50, 0f, 0f, 0, new Color(0, 0, 0), 1f)];
			return true;
		}
	}
}