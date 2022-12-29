import { Component, Injector, OnInit, ViewEncapsulation } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-Portal',
  templateUrl: './portal.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class PortalComponent extends AppComponentBase implements OnInit {

  constructor(
    injector: Injector
  ) {
    super(injector)
  }

  ngOnInit() {
  }

}
