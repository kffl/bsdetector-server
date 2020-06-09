using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BSDetector.Models
{
    public class StatsContext : DbContext
    {
        public DbSet<Stat> Stats { get; set; }

        public StatsContext() { }
        public StatsContext(DbContextOptions<StatsContext> options)
            : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite("Data Source=stats.db");
        //    base.OnConfiguring(options);
        //}

        public async Task IncrementKeyAsync(string key)
        {
            var item = await this.Stats.FirstAsync(k => k.key == key);
            item.value += 1;
            this.SaveChanges();
        }

        public async Task AddToKeyAsync(string key, int value)
        {
            var item = await this.Stats.FirstAsync(k => k.key == key);
            item.value += value;
            this.SaveChanges();
        }
    }

    public class Stat
    {
        [Key]
        public string key { get; set; }
        public int value { get; set; }
    }
}
