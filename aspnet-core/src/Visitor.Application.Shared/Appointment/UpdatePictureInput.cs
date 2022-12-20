using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visitor.Appointment
{
    public class UpdatePictureInput : ICustomValidate
    {
        [MaxLength(400)]
        public string FileToken { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (FileToken.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(FileToken));
            }
        }
    }
}
