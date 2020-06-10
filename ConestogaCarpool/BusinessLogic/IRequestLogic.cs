using ConestogaCarpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.BusinessLogic
{
    public interface IRequestLogic
    {
        Task<List<Request>> GetAllRequests();
        Task<List<Request>> GetDriverRequests(int? driverId);
        Task<List<Request>> GetPassengerRequests(int? passengerId);
        Task<Request> GetSingleRequest(int? requestId);
        Task<Request> GetPost(int? postId);
        void CreateRequest(Request request);
        void UpdateRequest(Request request);
        void AcceptRequest(Request request);
        void DeclineRequest(Request request);
        void DeleteRequest(int? requestId);
        Task Save();
        void SendEmail(string subject, string body, string email);
        bool RequestExists(int id);
    }
}
