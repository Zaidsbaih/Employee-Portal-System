using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IHRRequestServices
    {
        // Employee
        void SubmitRequest(
            int employeeId,
            HRRequestModel model);

        HRRequestEmployeeVM GetEmployeeRequests(
            int employeeId);


        // Admin / HR
        HRRequestAdminVM GetAdminRequests(
            string? employeeName,
            string? requestType,
            string? status);

        void UpdateRequest(
            int requestId,
            string status,
            string? comment,
            IFormFile? attachmentFile);
    }
}