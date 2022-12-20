using Abp.AspNetZeroCore.Net;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Visitor.Appointment;
using Visitor.Authorization.Users.Profile.Dto;
using Visitor.Dto;
using Visitor.Storage;

namespace Visitor.Web.Controllers
{
    public abstract class AppointmentControllerBase : VisitorControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IAppointmentsAppService _appointmentsAppService;

        private const int MaxPictureSize = 5242880; //5MB

        public AppointmentControllerBase(ITempFileCacheManager tempFileCacheManager, IAppointmentsAppService appointmentsAppService)
        {
            _tempFileCacheManager = tempFileCacheManager;
            _appointmentsAppService = appointmentsAppService;
        }

        [HttpPost]
        public UploadPictureOutput UploadAppointmentPicture(FileDto input)
        {
            try
            {
                var pictureFile = Request.Form.Files.First();

                if ( pictureFile == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if ( pictureFile.Length > MaxPictureSize )
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit",
                        AppConsts.MaxProfilePictureBytesUserFriendlyValue));
                }

                byte[] fileBytes;

                using (var stream = pictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes(); // get all bytes by method stream
                }

                using (var image = Image.Load(fileBytes, out IImageFormat format))
                {
                    if (!format.IsIn(JpegFormat.Instance, PngFormat.Instance, GifFormat.Instance))
                    {
                        throw new UserFriendlyException(L("IncorrectImageFormat"));
                    }

                    _tempFileCacheManager.SetFile(input.FileToken, fileBytes);

                    return new UploadPictureOutput
                    {
                        FileToken = input.FileToken,
                        FileName = input.FileName,
                        FileType = input.FileType,
                        Width = image.Width,
                        Height = image.Height
                    };
                }
            }
            catch (UserFriendlyException ex)
            {
                return new UploadPictureOutput(new ErrorInfo(ex.Message));
            }
        }

        [HttpGet]
        public async Task<FileResult> GetPictureByAppointment(Guid appId)
        {
            var output = await _appointmentsAppService.GetPictureByAppointment(appId);
            return File(Convert.FromBase64String(output.Picture), MimeTypeNames.ImageJpeg);
        }
    }
}
