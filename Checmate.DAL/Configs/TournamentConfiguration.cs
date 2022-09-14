using Checkmate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.DAL.Configs
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasCheckConstraint("CK_MINMAX_PLAYERS", "MinPlayers <= MaxPlayers");
            builder.HasCheckConstraint("CK_MINMAX_ELO", "MinElo <= MaxElo");
            builder.HasCheckConstraint("CK_OKMINVALUE_MINPLAYERS", "MinPlayers >= 2");
            builder.HasCheckConstraint("CK_OKMAXVALUE_MAXPLAYERS", "MaxPlayers <= 32");
            builder.HasCheckConstraint("CK_OKMAXVALUE_MINELO", "MinElo <= 0");
            builder.HasCheckConstraint("CK_OKMAXVALUE_MINELO", "MinElo <= 3000");


            builder.HasData(new Tournament
            {
                Id = 1,
                Name = "Tournoi originel",
                Location = "Jardin d'Eden",
                MinPlayers = 2,
                MaxPlayers = 32,
                MinElo = 1000,
                MaxElo = 3000,
                Category = TournamentCategoryType.Veteran,
                Status = TournamentStatusType.WaitingForPlayers,
                Round = 0,
                WomenOnly = true,
                RegistrationEndDate = new DateTime(2023, 11, 27),
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now
            });
        }
    }
}
