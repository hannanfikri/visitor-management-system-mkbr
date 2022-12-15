import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';

import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask'; import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PopoverModule } from 'ngx-bootstrap/popover';

import { BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import { PublicRoutingModule } from './public-routing.module';


import { HttpClientModule } from '@angular/common/http';
import { HttpClientJsonpModule } from '@angular/common/http';

import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { TextMaskModule } from 'angular2-text-mask';
import { NgxSpinnerModule, NgxSpinnerComponent } from 'ngx-spinner';
import { AppBsModalModule } from '@shared/common/appBsModal/app-bs-modal.module';

import { NgxCaptchaModule } from 'ngx-captcha';
import { LocaleMappingService } from '@shared/locale-mapping.service';
import { FormWizardComponent } from './Pages/appointment/form-wizard/form-wizard.component';
import { CreateOrEditAppointmentDto, PortalsServiceProxy } from '@shared/service-proxies/service-proxies';


export function getRecaptchaLanguage(): string {
    return new LocaleMappingService().map('recaptcha', abp.localization.currentLanguage.name);
}

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        TabsModule,
        ModalModule,
        TooltipModule,
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot(),
        BsDatepickerModule.forRoot(),
        FileUploadModule,
        UtilsModule,
        AppCommonModule,
        TableModule,
        PaginatorModule,
        PerfectScrollbarModule,
        TextMaskModule,
        NgxSpinnerModule,
        AppBsModalModule,
        AutoCompleteModule,
        EditorModule,
        InputMaskModule,
        PublicRoutingModule,
        CountoModule,
        NgxCaptchaModule,
    ],
    declarations: [

        FormWizardComponent,

    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale },
        [PortalsServiceProxy]
    ],
    entryComponents: [NgxSpinnerComponent]
})
export class PublicModule { }
