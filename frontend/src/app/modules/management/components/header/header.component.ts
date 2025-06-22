import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { Component, OnInit } from '@angular/core';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { UserSearchService } from '../../../../shared/services/user-search.service';
import { ManagementDocumentConstants } from '../../constants/ManagementDocumentConstants';
import { ManagementRoutesConstants } from '../../constants/ManagementRoutesConstants';
import { BasicDetailService } from '../../services/basic-detail.service';
import { IBasicDetail } from '../../interfaces/IBasicDetail';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { environment } from '../../../../../environments/environment';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    NzLayoutModule,
    NzGridModule,
    FormsModule,
    NzInputModule,
    NzAutocompleteModule,
    NzMenuModule,
    RouterLink,
    RouterLinkActive,
    NzButtonModule,
    NzAvatarModule,
    NzIconModule,
    NzModalModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css', '/src/styles.css'],
})
export class HeaderComponent implements OnInit {
  keywords: string = '';
  filteredOptions: string[] = [];
  basicDetails = {} as IBasicDetail;
  isMenuModalVisible = false;
  imageUrl = environment.imageUrl;
  subscription!: Subscription;

  constructor(private basicDetailService: BasicDetailService,
    private router: Router,
    private userSearchService: UserSearchService,
    private eventEmitterService: EventEmitterService
  ) {

  }

  ngOnInit(): void {
    this.subscription = this.eventEmitterService.getObservable().subscribe(() => {
      this.getDetails();
    })
  }

  getDetails() {
    this.basicDetailService.getBasicDetails().subscribe((response: IBasicDetail) => {
      this.basicDetails = response;
    });
  }

  onSelectionChange(event: KeyboardEvent) {
    let text = (event.target as HTMLInputElement).value;
    (event.target as HTMLInputElement).value = text?.replace(/[<>]/ig, "");

    if (event.key === ManagementDocumentConstants.ENTER) {
      this.router.navigate([ManagementRoutesConstants.MANAGEMENT_SEARCH], {
        queryParams: {
          currentCount: 0,
          keywords: (event.target as HTMLInputElement).value.toString(),
          data: null,
        },
      });
    }
  }

  showMenu() {
    this.isMenuModalVisible = true;
  }

  onCancel() {
    this.isMenuModalVisible = false;
  }

  logout() {
    localStorage.removeItem(ManagementDocumentConstants.TOKEN);
    this.router.navigate([ManagementRoutesConstants.AUTH]);
  }
}