using Abp.Web.Models;

namespace Visitor.Appointment.Dto
{
    public class UploadProfilePictureOutputs :ErrorInfo
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FileToken { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public UploadProfilePictureOutputs()
        {

        }

        public UploadProfilePictureOutputs(ErrorInfo error)
        {
            Code = error.Code;
            Details = error.Details;
            Message = error.Message;
            ValidationErrors = error.ValidationErrors;
        }
    }
}
