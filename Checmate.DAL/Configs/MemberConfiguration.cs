using Checkmate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Checkmate.DAL.Configs
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasIndex(m => m.Nickname).IsUnique(true);
            builder.HasIndex(m => m.Email).IsUnique(true);
            builder.HasIndex(m => m.Salt).IsUnique();

            SHA512CryptoServiceProvider service = new SHA512CryptoServiceProvider();

            Guid guid = Guid.NewGuid();
            byte[] pwdToHash = Encoding.UTF8.GetBytes("verysecret" + guid.ToString());
            byte[] pwdHashed = service.ComputeHash(pwdToHash);

            Guid guid1 = Guid.NewGuid();
            byte[] pwdToHash1 = Encoding.UTF8.GetBytes("leo123=" + guid1.ToString());
            byte[] pwdHashed1 = service.ComputeHash(pwdToHash1);

            Guid guid2 = Guid.NewGuid();
            byte[] pwdToHash2 = Encoding.UTF8.GetBytes("andre123=" + guid2.ToString());
            byte[] pwdHashed2 = service.ComputeHash(pwdToHash2);

            Guid guid3 = Guid.NewGuid();
            byte[] pwdToHash3 = Encoding.UTF8.GetBytes("romuald123=" + guid3.ToString());
            byte[] pwdHashed3 = service.ComputeHash(pwdToHash3);

            Guid guid4 = Guid.NewGuid();
            byte[] pwdToHash4 = Encoding.UTF8.GetBytes("sarah123=" + guid4.ToString());
            byte[] pwdHashed4 = service.ComputeHash(pwdToHash4);

            Guid guid5 = Guid.NewGuid();
            byte[] pwdToHash5 = Encoding.UTF8.GetBytes("dana123=" + guid5.ToString());
            byte[] pwdHashed5 = service.ComputeHash(pwdToHash5);

            Guid guid6 = Guid.NewGuid();
            byte[] pwdToHash6 = Encoding.UTF8.GetBytes("bob123=" + guid6.ToString());
            byte[] pwdHashed6 = service.ComputeHash(pwdToHash6);


            builder.HasData(new Member
            {
                Id = 1,
                Nickname = "Admin",
                Email = "admin@mail.be",
                EncodedPassword = pwdHashed,
                Salt = guid,
                BirthDate = DateTime.Now,
                Gender = GenderType.Male,
                Elo = 3000,
                Role = RoleType.Admin
            });
            builder.HasData(new Member
            {
                Id = 2,
                Nickname = "Leo",
                Email = "leo@mail.be",
                EncodedPassword = pwdHashed1,
                Salt = guid1,
                BirthDate = DateTime.Now,
                Gender = GenderType.Male,
                Elo = 1200,
                Role = RoleType.Other
            });
            builder.HasData(new Member
            {
                Id = 3,
                Nickname = "André",
                Email = "andre@mail.be",
                EncodedPassword = pwdHashed2,
                Salt = guid2,
                BirthDate = DateTime.Now,
                Gender = GenderType.Male,
                Elo = 2500,
                Role = RoleType.Other
            });
            builder.HasData(new Member
            {
                Id = 13,
                Nickname = "Romuald",
                Email = "romuald@mail.be",
                EncodedPassword = pwdHashed3,
                Salt = guid3,
                BirthDate = new DateTime(2000, 01, 01),
                Gender = GenderType.Male,
                Elo = 1500,
                Role = RoleType.Other
            });
            builder.HasData(new Member
            {
                Id = 14,
                Nickname = "Sarah",
                Email = "sarah@mail.be",
                EncodedPassword = pwdHashed4,
                Salt = guid4,
                BirthDate = new DateTime(2000, 01, 01),
                Gender = GenderType.Female,
                Elo = 2500,
                Role = RoleType.Other
            });
            builder.HasData(new Member
            {
                Id = 15,
                Nickname = "Dana",
                Email = "dana@mail.be",
                EncodedPassword = pwdHashed5,
                Salt = guid5,
                BirthDate = new DateTime(1950, 01, 01),
                Gender = GenderType.Female,
                Elo = 1000,
                Role = RoleType.Other
            });
            builder.HasData(new Member
            {
                Id = 16,
                Nickname = "Bob",
                Email = "bob@mail.be",
                EncodedPassword = pwdHashed6,
                Salt = guid6,
                BirthDate = new DateTime(1920, 01, 01),
                Gender = GenderType.Male,
                Elo = 2999,
                Role = RoleType.Other
            });
        }
    }
}
