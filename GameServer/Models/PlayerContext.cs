﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Temp> PlayersPOST_ID { get; set; }
        public DbSet<Temp2> PlayersGET_ID { get; set; }
        public DbSet<TempMap> Map { get; set; }
    }
}