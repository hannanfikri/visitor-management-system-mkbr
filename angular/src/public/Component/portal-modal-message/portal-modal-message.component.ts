import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-portal-modal-message',
  templateUrl: './portal-modal-message.component.html',
  styleUrls: ['./portal-modal-message.component.scss']
})
export class PortalModalMessageComponent extends AppComponentBase implements OnInit {
  active = false;
  title: string;
  bookingRef: string;
  key: string;

  @ViewChild("portalModalMessage", { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
  }

  show(key?: string, bookingRef?: string) {
    this.key = key;
    if (key == 'Submit') {
      this.bookingRef = bookingRef;
    }
    this.modal.show();
  }

  close(): void {
    this.active = false;
    this.modalSave.emit(null);
    this.modal.hide();
  }

  resetValue() {
    this.key = null;
    this.title = null;
    this.bookingRef = null;
  }

}
