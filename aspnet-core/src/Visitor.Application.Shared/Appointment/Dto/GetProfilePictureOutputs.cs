using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment.Dto
{
    public class GetProfilePictureOutputs
    {
        public string ProfilePicture { get; set; }

        public GetProfilePictureOutputs(string profilePicture)
        {
            ProfilePicture = profilePicture;
        }
    }
}
