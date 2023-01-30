import { DatePipe } from '@angular/common';
import { Component, Injector, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { PortalsServiceProxy, AppointmentsServiceProxy, StatusType, GetAppointmentForViewDto, GetExpiredUrlForViewDto } from '@shared/service-proxies/service-proxies';


@Component({
  selector: 'app-appointment-cancel',
  templateUrl: './cancel.component.html',
  styleUrls: ['./cancel.component.scss',],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class CancelComponent extends AppComponentBase implements OnInit {

  isExpired: Boolean;
  nowDT = new Date();
  expDateTime = new Date();
  expiredUrlObj: GetExpiredUrlForViewDto = new GetExpiredUrlForViewDto();

  appointmentRefNo: string = 'BR210479011';
  appointment: GetAppointmentForViewDto = new GetAppointmentForViewDto();
  isSubmit = false;
  StatusType = StatusType;
  appointmentId: string = '';
  action: string = ''


  constructor(injector: Injector,
    private _portalService: PortalsServiceProxy,
    private _appointmentService: AppointmentsServiceProxy,
    private _router: Router,
    private route: ActivatedRoute,
  ) {
    super(injector);
  }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.appointmentId = params.appointmentId;
        this.action = params.Item;
      });
    console.log(this.appointmentId);
    this._appointmentService.getAppointmentForView(this.appointmentId)
      .subscribe(result => {
        this.appointment = result;
        this.expiredOrNot(this.appointmentId, this.action);
      });
  }

  cancelLink(appointmentId) {
    this._router.navigate(['/check'], { queryParams: { appointmentRef: this.appointment.appointment.appRefNo }, fragment: 'buttonClick' });
  }

  expiredOrNot(appointmentId, action) {
    this._appointmentService.checkExpiredUrl(appointmentId, action).subscribe(result => {
      this.expiredUrlObj = result;
      this.expDateTime = new Date(this.expiredUrlObj.expiredUrl.urlExpiredDate);

      if (this.nowDT > this.expDateTime) {
        this.isExpired = false;
      } else {
        this.isExpired = true;
      }
    });
  }

  cancelAppointment(appointmentId) {
    this.message.confirm("", this.l("AreYouSure"), (isConfirmed) => {
      if (isConfirmed) {
        this._portalService.confirmCancelAppointment(appointmentId).subscribe(() => {
          this._router.navigate(['/']);
          this.notify.success(this.l("AppointmentCancelledSuccessfully"));
        });
      }
    });
  }

  // reschedule(appointmentId) {
  //   this.rescheduleModal.show(appointmentId);
  //     // this._router.navigate(['/']);
  //     // this.notify.success(this.l("SuccessfullyDeleted"));
  // }

  // closeReschedule() {
  //   this._router.navigate(['/']);
  //   this.notify.success(this.l("SuccessfullyReschedule"));
  // }

  redirectToNext() {
    this._router.navigate(['/']);
  }

}
