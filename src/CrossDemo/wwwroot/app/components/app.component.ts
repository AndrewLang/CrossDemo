﻿import {Component}                          from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES}     from 'angular2/router';

import {InstrumentsComponent}               from './instrument/instruments.component';
import {ChassisComponent}                   from './chassis/chassis.component';
import {SettingsComponent}                  from './setting/settings.component';
import {HelpComponent}                      from './help/help.component';

import {SidebarService}                     from './services/SidebarService';

@Component({
    selector: 'app',
    templateUrl: '/Views/Home/',
    directives: [ROUTER_DIRECTIVES ],
    providers: [SidebarService]
})
@RouteConfig([
        { path: '/instruments/...', name: 'Instruments',    component: InstrumentsComponent, useAsDefault: true },
        { path: '/chassies/...',    name: 'Chassises',      component: ChassisComponent },
        { path: '/settings/...',    name: 'Settings',       component: SettingsComponent },
        { path: '/help/...',        name: 'Help',           component: HelpComponent },
        { path: '/**', redirectTo: ['Instruments'] }
])
export class AppComponent {
    
    constructor(private mSidebarService: SidebarService) {
        
    }

    toggleSidebar() {
        console.log("Toggle side bar " + this.IsSidebarVisible);
        
    }

    IsSidebarVisible(): boolean {
        return this.mSidebarService.IsSidebarVisible;
    }
}