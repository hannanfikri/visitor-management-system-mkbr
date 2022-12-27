import { DOCUMENT } from '@angular/common';
import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { ThemesLayoutBaseComponent } from '@app/shared/layout/themes/themes-layout-base.component';
// import { ToggleOptions } from '@metronic/app/core/_base/layout/directives/toggle.directive';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppConsts } from '@shared/AppConsts';
import { UrlHelper } from '@shared/helpers/UrlHelper';

import { Component, Inject, Injector, OnInit } from '@angular/core';
import { filter as _filter } from 'lodash-es';

@Component({
  templateUrl: './PortalTheme.component.html',
  selector: 'app-PortalTheme',
  styleUrls: ['./PortalTheme.component.scss'],
  animations: [appModuleAnimation()]
})
export class PortalThemeComponent extends ThemesLayoutBaseComponent implements OnInit {

  // userMenuToggleOptions: ToggleOptions = {
  //   target: this.document.body,
  //   targetState: 'topbar-mobile-on',
  //   toggleState: 'active'
  // };

  asideToggler;

  remoteServiceBaseUrl: string = AppConsts.remoteServiceBaseUrl;
  isHome = true;
  currentUrl = '';


  currentLanguage: abp.localization.ILanguageInfo;
  languages: abp.localization.ILanguageInfo[] = [];

  constructor(
    injector: Injector,
    @Inject(DOCUMENT) private document: Document,
    _dateTimeService: DateTimeService,
    private router: Router
  ) {
    super(injector, _dateTimeService);

    //subscribe route

    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.currentUrl = location.pathname;
        this.isHome = this.currentUrl == '/home' ? true : this.currentUrl == '/' ? true : false;
        this.currentUrl = this.currentUrl.replace('/', '');
      }
    });

  }

  ngOnInit() {
    this.installationMode = UrlHelper.isInstallUrl(location.href);
    this.defaultLogo = AppConsts.appBaseUrl + '/assets/bkrm/bkrm-logo.png';
    this.asideToggler = new KTOffcanvas(this.document.getElementById('kt_header_navs'), {
      overlay: true,
      baseClass: 'header-navs',
      toggleBy: 'kt_header_mobile_toggle'
    });
    this.languages = _filter(abp.localization.languages, l => (<any>l).isDisabled === false);

    this.currentLanguage = abp.localization.currentLanguage;
    console.log(this.currentLanguage);
    console.log(this.languages);

  }

  changeLanguage(language: abp.localization.ILanguageInfo) {
    abp.localization.currentLanguage = language;

    abp.utils.setCookieValue(
      'Abp.Localization.CultureName',
      language.name,
      new Date(new Date().getTime() + 5 * 365 * 86400000), // 5 year
      abp.appPath
    );

    location.reload();
  }

}
