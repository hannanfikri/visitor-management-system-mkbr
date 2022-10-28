import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'notification-detail-modal',
  templateUrl: './notification-detail-modal.component.html',
})
export class NotificationDetailModalComponent extends AppComponentBase {
  @ViewChild('messageDetailModal', { static: true }) modal: ModalDirective;

  content: string;
  isHtml: boolean;

  constructor(injector: Injector) {
    super(injector);
  }

  show(text: string, isHtml: boolean) {
    this.content = text;
    this.isHtml = isHtml;
    this.modal.show();
  }

  close() {
    this.content = '';
    this.isHtml = false;
    this.modal.hide();
  }
}
