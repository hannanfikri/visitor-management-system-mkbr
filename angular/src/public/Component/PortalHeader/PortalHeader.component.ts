import { Component, Injector, OnInit, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { NavigationEnd, Router } from '@angular/router';
import { PermissionCheckerService } from 'abp-ng2-module';
import { filter } from 'rxjs/operators';
import * as objectPath from 'object-path';
import { AppMenu } from '@app/shared/layout/nav/app-menu';
import { AppNavigationService } from '@app/shared/layout/nav/app-navigation.service';
import { MenuOptions } from '@metronic/app/core/_base/layout/directives/menu.directive';
import { OffcanvasOptions } from '@metronic/app/core/_base/layout/directives/offcanvas.directive';
import { ThemeAssetContributorFactory } from '@shared/helpers/ThemeAssetContributorFactory';

@Component({
    selector: 'app-PortalHeader',
    templateUrl: './PortalHeader.component.html',
    styleUrls: ['./PortalHeader.component.css']
})
export class PortalHeaderComponent extends AppComponentBase implements OnInit {

    menu: AppMenu = null;
    currentRouteUrl: any = '';
    menuDepth: 0;
    menuWrapperStyle = '';

    menuOptions: MenuOptions = {
        submenu: {
            desktop: 'dropdown',
            tablet: 'accordion',
            mobile: 'accordion',
        },

        accordion: {
            expandAll: false,
        }
    };

    offcanvasOptions: OffcanvasOptions = {
        overlay: true,
        baseClass: 'header-menu-wrapper',
        closeBy: 'kt_header_menu_mobile_close_btn',
        toggleBy: 'kt_header_mobile_toggle'
    };

    constructor(
        injector: Injector,
        private router: Router,
        public permission: PermissionCheckerService,
        private _appNavigationService: AppNavigationService) {
        super(injector);
    }

    ngOnInit() {
        this.menu = this._appNavigationService.getMenu();
        this.currentRouteUrl = this.router.url;
        this.menuWrapperStyle = ThemeAssetContributorFactory.getCurrent().getMenuWrapperStyle();

        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(event => {
                this.currentRouteUrl = this.router.url;
            });
    }

    showMenuItem(menuItem): boolean {
        return this._appNavigationService.showMenuItem(menuItem);
    }

    getItemCssClasses(item, parentItem, depth) {
        let isRootLevel = item && !parentItem;

        let cssClasses = 'menu-item menu-item-rel';

        if (objectPath.get(item, 'items.length')) {
            cssClasses += ' menu-item-submenu';
        }

        if (objectPath.get(item, 'icon-only')) {
            cssClasses += ' menu-item-icon-only';
        }

        if (this.isMenuItemIsActive(item)) {
            cssClasses += ' menu-item-active';
        }

        if (item.items.length) {
            cssClasses += ' menu-item-submenu menu-item-rel';
            if (depth && depth === 1) {
                cssClasses += ' menu-item-open-dropdown';
            }
        } else if (item.items.length) {
            if (depth && depth >= 1) {
                cssClasses += ' menu-item-submenu';
            } else {
                cssClasses += ' menu-item-rel';
            }
        }

        return cssClasses;
    }

    getAnchorItemCssClasses(item, parentItem): string {
        let isRootLevel = item && !parentItem;
        let cssClasses = 'menu-link';

        if (isRootLevel || item.items.length) {
            cssClasses += ' menu-toggle';
        }

        return cssClasses;
    }

    getSubmenuCssClasses(item, parentItem, depth): string {
        let cssClasses = 'menu-submenu menu-submenu-classic';
        return cssClasses += ' menu-submenu-' + (depth >= 1 ? 'right' : 'left');
    }

    isMenuItemIsActive(item): boolean {
        if (item.items.length) {
            return this.isMenuRootItemIsActive(item);
        }

        if (!item.route) {
            return false;
        }

        return this.currentRouteUrl.replace(/\/$/, '') === item.route.replace(/\/$/, '');
    }

    isMenuRootItemIsActive(item): boolean {
        if (item.items) {
            for (const subItem of item.items) {
                if (this.isMenuItemIsActive(subItem)) {
                    return true;
                }
            }
        }

        return false;
    }

    getItemAttrSubmenuToggle(menuItem, parentItem, depth) {
        if (depth && depth >= 1) {
            return 'hover';
        } else {
            return 'click';
        }
    }

    isMobileDevice(): any {
        return KTUtil.isMobileDevice();
    }
}
