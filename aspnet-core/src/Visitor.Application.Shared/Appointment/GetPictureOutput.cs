using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment
{
    public class GetPictureOutput
    {
        public string Picture { get; set; }

        public GetPictureOutput(string picture)
        {
            Picture = picture;
        }
    }
}
