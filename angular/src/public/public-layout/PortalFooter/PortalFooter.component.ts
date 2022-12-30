import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-PortalFooter',
  templateUrl: './PortalFooter.component.html',
  styleUrls: ['./PortalFooter.component.scss']
})
export class PortalFooterComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit() {
  }

}
