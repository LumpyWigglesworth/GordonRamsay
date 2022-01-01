using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
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
        private int state = 1;
        private float knifeSpeed = 10f;
        /*
         0: Movement/cooldown
         1: Chase attack
         */
        private float maxSpeed = 8f;
        private float speed = 5f;
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
                npc.TargetClosest(false);
                p = Main.player[npc.target];
                if (p.dead || !p.active)
                {
                    state = 0;
                }
            }
            // Logic
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                switch (state)
                {
                    case 0: // Flee state
                        maxSpeed = 8f;
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        break;
                    case 1: // Idle state
                        npc.TargetClosest(false);
                        p = Main.player[npc.target];
                        maxSpeed = 8f;
                        timer++;
                        if (timer > coolTime)
                        {
                            timer = 0;
                            int newState = WorldGen.genRand.Next(2, 4);
                            state = newState;
                        }
                        break;
                    case 2: // Attack state
                        npc.TargetClosest(false);
                        p = Main.player[npc.target];
                        timer++;
                        if (timer > attackTime)
                        {
                            timer = 0;
                            state = 1;
                        }
                        break;
                    case 3: // Chase state
                        npc.TargetClosest(false);
                        p = Main.player[npc.target];
                        maxSpeed = 5f;
                        timer++;
                        if (timer > chaseTime)
                        {
                            timer = 0;
                            state = 1;
                        }
                        break;
                }
            }
            // Movement
            Vector2 distance = new Vector2(0f, -250f);
            Vector2 moveTo;
            if (state == 3)
                moveTo = p.Center;
            else if (state == 1 || state == 2)
                moveTo = p.Center + distance;
            else
                moveTo = p.Center + new Vector2(0f, -1000f);
            Vector2 direction = (moveTo - npc.Center).SafeNormalize(Vector2.UnitX);
            speed = Vector2.Distance(moveTo, npc.Center) / 20;
            if (speed > maxSpeed) speed = maxSpeed;
            if(Main.netMode != NetmodeID.MultiplayerClient)
                npc.velocity = (direction * speed);
            // Falling Knives Attack
            if (state == 2 && timer % knifeSpeed == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int displacement = WorldGen.genRand.Next(-400, 400);
                float projectileX = p.Center.X + displacement;
                float projectileY = p.Center.Y - 700;
                int knife = Projectile.NewProjectile(projectileX, projectileY, 0, 15f, ModContent.ProjectileType<Projectiles.GordonKnife>(), npc.damage, 10);
                Main.projectile[knife].netUpdate = true;
            }
            npc.netUpdate = true;
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
