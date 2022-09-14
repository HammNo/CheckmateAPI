
using Checkmate.DAL.Configs;
using Checkmate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.DAL
{
    public class CheckmateContext : DbContext
    {
        public DbSet<Member> Members { get { return Set<Member>(); } }
        public DbSet<Tournament> Tournaments { get { return Set<Tournament>(); } }

        public CheckmateContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MemberConfiguration());
            builder.ApplyConfiguration(new TournamentConfiguration());
        }
    }
}
