using Checkmate.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace CheckmateAPI.Services
{
    public class MailService
    {
        private MailConfig _config;
        private SmtpClient _client;
        public MailService(MailConfig config, SmtpClient client)
        {
            _config = config;
            _client = client;
            _client.Host = config.Host;
            _client.Port = _config.Port;
            _client.Credentials = new NetworkCredential(_config.Email, _config.Password);
            _client.EnableSsl = true;
        }

        public void Send(string subject, string content, string email)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_config.Email);
            message.To.Add(email);
            message.Body = content;
            message.Subject = subject;
            _client.Send(message);
        }

        //public void SendTournanementAlert(Tournament tournament, params string[] membersMail)
        //{
        //    string subject = $"Création du tournoi {tournament.Name}";
        //    string content = $"Nom Tournoi : {tournament.Name}\n" +
        //                     $"Lieu : {tournament.Location}\n" +
        //                     $"Fin des enregistrements : {tournament.RegistrationEndDate}\n" +
        //                     $"Joueurs min : {tournament.MinElo}\n" +
        //                     $"Joueurs max : {tournament.MaxElo}";
        //    foreach(string email in membersMail)
        //    {
        //        Send(subject, content, email);
        //    }
        //}

        public void SendMemberSub(Member member, string password)
        {
            string subject = $"Création de votre compte";
            string content = $"Votre login : {member.Nickname}\n" +
                             $"Votre mot de passe : {password}\n";
            try
            {
                Send(subject, content, member.Email);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
