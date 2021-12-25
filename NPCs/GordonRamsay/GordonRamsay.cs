using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace GordonRamsay.NPCs.GordonRamsay
{
    [AutoloadBossHead]
    class GordonRamsay : ModNPC
    {
        private float timer
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private float coolTime = 240;
        private float chaseTime = 180;
        private float attackTime = 180;
        private int state = 0;
        /*
         0: Movement/cooldown
         1: Chase attack
         */
        private float maxSpeed = 8f;
        private float speed = 5f;
        Random rand = new Random();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gordon Ramsay");
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return true;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lifeMax = 4400;
            npc.defense = 10;
            npc.damage = 32;
            npc.knockBackResist = 0f;
            npc.width = 86;
            npc.height = 124;
            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.npcSlots = 6f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GordonBoss");
        }
        public override void AI()
        {
            Player p = Main.player[npc.target];
            if (p.dead || !p.active)
            {
                state = 3;
            }
            // Logic
            switch (state)
            {
                case 0: // Idle state
                    maxSpeed = 8f;
                    timer++;
                    if (timer > coolTime)
                    {
                        timer = 0;
                        int newState = rand.Next(1, 3);
                        state = newState;
                    }
                    break;
                case 1: // Chase state
                    maxSpeed = 5f;
                    timer++;
                    if (timer > chaseTime)
                    {
                        timer = 0;
                        state = 0;
                    }
                    break;
                case 2: // Attack state
                    timer++;
                    if (timer > attackTime)
                    {
                        timer = 0;
                        state = 0;
                    }
                    break;
                case 3: // Flee state
                    maxSpeed = 8f;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    break;
            }
            // Movement
            Vector2 distance = new Vector2(0f, -250f);
            Vector2 moveTo;
            if (state == 1)
                moveTo = p.Center;
            else if (state != 3)
                moveTo = p.Center + distance;
            else
                moveTo = p.Center + new Vector2(0f, -1000f);
            Vector2 direction = (moveTo - npc.Center).SafeNormalize(Vector2.UnitX);
            speed = Vector2.Distance(moveTo, npc.Center) / 20;
            if (speed > maxSpeed) speed = maxSpeed;
            npc.velocity = (direction * speed);
            // Falling Knives Attack
            if (state == 2 && timer % 30 == 0)
            {
                int displacement = rand.Next(-300, 300);
                float projectileX = p.Center.X + displacement;
                float projectileY = p.Center.Y - 700;
                Projectile.NewProjectile(projectileX, projectileY, 0, 15f, ModContent.ProjectileType<Projectiles.GordonKnife>(), npc.damage, 10);
            }

        }
        public override void NPCLoot()
        {
            int weapon = Main.rand.Next(2);
            switch(weapon)
            {
                case 0:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.KitchenKnife>());
                    break;
                case 1:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.SwearWords>());
                    break;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
