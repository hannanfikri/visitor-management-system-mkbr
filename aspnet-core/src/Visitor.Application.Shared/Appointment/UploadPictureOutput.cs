using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment
{
    public class UploadPictureOutput : ErrorInfo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        public string FileToken { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public UploadPictureOutput()
        {}
        public UploadPictureOutput(ErrorInfo error)
        {
            Code = error.Code;
            Details = error.Details;
            Message = error.Message;
            ValidationErrors = error.ValidationErrors;
        }
    }
}
