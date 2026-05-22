using PRN232.LMS.Models.RequestModel;

namespace PRN232.LMS.Services.Validators
{
    public static class StudentValidator
    {
        public static void ValidateStudentRequest(string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("FullName is required");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required");
            }
        }

        public static void ValidateStudentRequest(CreateStudentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Request is required");
            }

            ValidateStudentRequest(request.FullName, request.Email);
        }

        public static void ValidateStudentRequest(UpdateStudentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Request is required");
            }

            ValidateStudentRequest(request.FullName, request.Email);
        }
    }
}
