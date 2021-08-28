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
            Tooltip.SetDefault("Shoots a swear word.");
        }

		public override void SetDefaults()
		{
			item.damage = 50;
			item.magic = true;
			item.noMelee = true;
			item.mana = 10;
			item.width = 30;
			item.height = 30;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = 5;
			item.knockBack = 20;
			item.value = 1000000;
			item.rare = 5;
			item.shoot = ModContent.ProjectileType<SwearWord>();
			item.shootSpeed = 10f;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/TVBleep75");
			item.autoReuse = false;
		}
	}
}