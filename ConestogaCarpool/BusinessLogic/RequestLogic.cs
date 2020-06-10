using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using EASendMail;

namespace ConestogaCarpool.BusinessLogic
{
    public class RequestLogic : IRequestLogic
    {
        private IRequestRepository _requestRepository;

        public RequestLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<List<Request>> GetAllRequests()
        {
            List<Request> requests = await _requestRepository.GetAllRequests();
            return requests;
        }

        public async Task<List<Request>> GetDriverRequests(int? driverId)
        {
            List<Request> driverRequests = await _requestRepository.GetDriverRequests(driverId);
            return driverRequests;
        }

        public async Task<List<Request>> GetPassengerRequests(int? passengerId)
        {
            List<Request> passengerRequests = await _requestRepository.GetPassengerRequests(passengerId);
            return passengerRequests;
        }

        public async Task<Request> GetSingleRequest(int? requestId)
        {
            Request request = await _requestRepository.GetSingleRequest(requestId);
            return request;
        }

        public async Task<Request> GetPost(int? postId)
        {
            Request request = await _requestRepository.GetPost(postId);

            return request;
        }

        public void CreateRequest(Request request)
        {
            _requestRepository.CreateRequest(request);
        }

        public void UpdateRequest(Request request)
        {
            _requestRepository.UpdateRequest(request);
        }

        public void AcceptRequest(Request request)
        {
            _requestRepository.AcceptRequest(request);
        }

        public void DeclineRequest(Request request)
        {
            _requestRepository.DeclineRequest(request);
        }

        public void DeleteRequest(int? requestId)
        {
            _requestRepository.DeleteRequest(requestId);
        }

        public async Task Save()
        {
            await _requestRepository.Save();
        }

        public void SendEmail(string subject, string body, string email)
        {
            SmtpMail mail = new SmtpMail("TryIt");

            // Your gmail email address
            mail.From = "carpoolconestoga@gmail.com";

            // Set recipient email address
            mail.To = email;

            // Set email subject
            mail.Subject = subject;

            // Set email body
            mail.HtmlBody = body;

            // Gmail SMTP server address
            SmtpServer server = new SmtpServer("smtp.gmail.com");

            // Gmail user authentication
            // For example: your email is "gmailid@gmail.com", then the user should be the same
            server.User = "carpoolconestoga@gmail.com";
            server.Password = "pucaeqetmwcojzdf";

            // set 587 SSL port
            server.Port = 465;

            // detect SSL/TLS automatically
            server.ConnectType = SmtpConnectType.ConnectSSLAuto;

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(server, mail);
        }

        public bool RequestExists(int id)
        {
            return _requestRepository.RequestExists(id);
        }
    }
}
