using Terraria.ID;
using Terraria.ModLoader;

namespace GordonRamsay.Items
{
	public class KitchenKnife : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("KitchenKnife"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Gordon Ramsay\'s genral use kitchen knife.");
		}

		public override void SetDefaults() 
		{
			item.damage = 50;
			item.melee = true;
			item.width = 27;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 3;
			item.knockBack = 2;
			item.value = 1000000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}
		
	}
}