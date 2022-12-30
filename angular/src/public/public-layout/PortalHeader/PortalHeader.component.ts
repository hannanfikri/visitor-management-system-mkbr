import { Component, Inject, Injector, OnInit, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { NavigationEnd, Router } from '@angular/router';
import { PermissionCheckerService } from 'abp-ng2-module';
import { filter } from 'rxjs/operators';
import * as objectPath from 'object-path';
import { AppMenu } from '@app/shared/layout/nav/app-menu';
import { AppNavigationService } from '@app/shared/layout/nav/app-navigation.service';
// import { MenuOptions } from '@metronic/app/core/_base/layout/directives/menu.directive';
// import { OffcanvasOptions } from '@metronic/app/core/_base/layout/directives/offcanvas.directive';
import { ThemeAssetContributorFactory } from '@shared/helpers/ThemeAssetContributorFactory';
import { ThemesLayoutBaseComponent } from '@app/shared/layout/themes/themes-layout-base.component';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { DOCUMENT } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'app-PortalHeader',
    templateUrl: './PortalHeader.component.html',
    styleUrls: ['./PortalHeader.component.scss']
})
export class PortalHeaderComponent extends ThemesLayoutBaseComponent implements OnInit {

    asideToggler;

    constructor(
        injector: Injector,
        private router: Router,
        @Inject(DOCUMENT) private document: Document,
        _dateTimeService: DateTimeService,
    ) {
        super(injector,_dateTimeService);
    }

    ngOnInit() {
        this.defaultLogo = AppConsts.appBaseUrl + '/assets/bkrm/bkrm-logo.png';
        this.asideToggler = new KTOffcanvas(this.document.getElementById('kt_header_navs'), {
            overlay: true,
            baseClass: 'header-navs',
            toggleBy: 'kt_header_mobile_toggle'
          });
    }
}
