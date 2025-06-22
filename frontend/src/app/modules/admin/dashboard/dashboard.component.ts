import { NzFlexModule } from 'ng-zorro-antd/flex';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
import { NzInputModule } from 'ng-zorro-antd/input';
import { PronounsComponent } from './pronouns/pronouns.component';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { Component } from '@angular/core';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzContentComponent, NzFooterComponent } from 'ng-zorro-antd/layout';
import { RouterOutlet, RouterModule, Router } from '@angular/router';
import { AdminRoutesConstants } from '../constants/AdminRoutesConstants';
import { AdminDocumentConstants } from '../constants/AdminDocumentConstants';
import { NzCardModule } from 'ng-zorro-antd/card';
import { EventEmitterService } from '../service/event-emitter.service';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { AdminUrlConstants } from '../constants/AdminUrlConstants';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzListModule } from 'ng-zorro-antd/list';
import { NotificationService } from '../service/notification.service';
import { AuthService } from '../../auth/service/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    NzFooterComponent,
    NzContentComponent,
    NzInputModule,
    NzLayoutModule,
    NzIconModule,
    NzMenuModule,
    NzCardModule,
    CommonModule,
    FormsModule,
    NzFlexModule,
    NzAutocompleteModule,
    PronounsComponent,
    NzGridModule,
    NzButtonModule,
    NzModalModule,
    NzListModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css', '/src/styles.css'],
})
export class DashboardComponent {
  isCollapsed = false;
  filteredOptions: string[] = [];
  keywords: string = '';
  logoUrl = AdminUrlConstants.LOGO_TEXT;
  isNotificationsModalVisible = false;
  userId: string;
  notifications: any[] = [];


  constructor(
    private router: Router,
    private eventEmitterService: EventEmitterService,
    private notificationService: NotificationService,
    private authService: AuthService
  ) {
    this.userId = this.authService.getUserId();

    this.notificationService.getNotifications(this.userId).subscribe({
      next: (response: any) => { this.notifications = response; console.log("noti", response) },
      error: error => console.error(error)
    })
  }

  onSelectionChange(event: KeyboardEvent) {
    let text = (event.target as HTMLInputElement).value;
    (event.target as HTMLInputElement).value = text?.replace(/[<>]/ig, "");

    if (event.key === AdminDocumentConstants.ENTER) {
      this.router.navigate([AdminRoutesConstants.DASHBOARD_SEARCH], {
        queryParams: {
          currentCount: 0,
          keywords: (event.target as HTMLInputElement).value.toString(),
          data: null,
        },
      });
    }
  }

  logout() {
    localStorage.removeItem(AdminDocumentConstants.TOKEN);
    this.router.navigate([AdminRoutesConstants.AUTH]);
  }

  onCollapsedChange() {
    this.isCollapsed = !this.isCollapsed;
    this.isCollapsed ? this.logoUrl = AdminUrlConstants.LOGO : this.logoUrl = AdminUrlConstants.LOGO_TEXT;
    this.eventEmitterService.resetCharts();
  }

  showNotificationModal() {
    this.isNotificationsModalVisible = true;
  }

  hideNotificationModal() {
    this.isNotificationsModalVisible = false;
  }
}
