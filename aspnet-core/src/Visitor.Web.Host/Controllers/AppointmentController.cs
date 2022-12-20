using Abp.Domain.Repositories;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Visitor.Appointment;
using Visitor.Storage;
using Abp.AspNetCore.Mvc.Authorization;

namespace Visitor.Web.Controllers
{
    public class AppointmentController : AppointmentControllerBase
    {
        /*private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<AppointmentEnt, Guid> _appointmentRepository;

        public AppointmentController(IBinaryObjectManager binaryObjectManager, IRepository<AppointmentEnt, Guid> appointmentRepository)
        {
            _binaryObjectManager = binaryObjectManager;
            _appointmentRepository = appointmentRepository;
        }

        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            try
            {
                var files = Request.Form.Files;

                if (files == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                List<UploadPictureOutput> filesOutput = new List<UploadPictureOutput>();

                foreach (var file in files)
                {
                    if (file.Length > 68700000)
                    {
                        throw new UserFriendlyException(L("File_SizeLimit_Error"));
                    }

                    byte[] fileBytes;
                    using (var stream = file.OpenReadStream())
                    {
                        fileBytes = stream.GetAllBytes();
                    }

                    var fileObject = new BinaryObject(AbpSession.TenantId, fileBytes, $"Appointment, uploaded file {DateTime.UtcNow}");
                    await _binaryObjectManager.SaveAsync(fileObject);

                    filesOutput.Add(new UploadPictureOutput
                    {
                        Id = fileObject.Id,
                        FileName = file.FileName,
                    });
                }

                return Json(new AjaxResponse(filesOutput));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetFiles(Guid id)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(id);
            return Json(new AjaxResponse(file));
        }

        [HttpGet]
        public async Task<JsonResult> DisplayImage(Guid id)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(id);
            string base64 = Convert.ToBase64String(file.Bytes);
            return Json(new AjaxResponse(base64));
        }*/


        // Upload image services (referring to profile services)
        public AppointmentController(ITempFileCacheManager tempFileCacheManager, IAppointmentsAppService appointmentsAppService) : base(tempFileCacheManager, appointmentsAppService)
        { }
    }
}
