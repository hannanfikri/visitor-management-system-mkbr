import { Component, Injector, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetAppointmentForEditOutput, PortalsServiceProxy, AppointmentsServiceProxy, StatusType } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-appointment-cancel',
  templateUrl: './cancel.component.html',
  styleUrls: ['./cancel.component.scss',],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class CancelComponent extends AppComponentBase implements OnInit {

  isExpired: boolean = false;
  appointmentRefNo: string = 'BR210479011';
  appointment: GetAppointmentForEditOutput = new GetAppointmentForEditOutput();
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
    this._appointmentService.getAppointmentForEdit(this.appointmentId)
      .subscribe(result => {
        this.appointment = result;
      });
    this._portalService.checkUrlExpiring(this.appointmentId, this.action).subscribe(res => {
      this.isExpired = true;
      console.log(this.isExpired);
    });
  }

  cancelLink(appointmentId) {
    this._router.navigate(['/check'], { queryParams: { appointmentRef: this.appointment.appointment.appRefNo }, fragment: 'buttonClick' });
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
