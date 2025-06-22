import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { Component, OnInit } from '@angular/core';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzAutocompleteModule, NzAutocompleteOptionComponent } from 'ng-zorro-antd/auto-complete';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { BasicDetailService } from '../../services/basic-detail.service';
import { IBasicDetail } from '../../interfaces/IBasicDetail';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { RoutesConstants } from '../../constants/RoutesConstants';
import { UserSearchService } from '../../../../shared/services/user-search.service';
import { environment } from '../../../../../environments/environment';
import { UrlConstants } from '../../constants/UrlConstants';
import { Subscription } from 'rxjs';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';

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
    NzIconModule,
    NzAvatarModule,
    NzDropDownModule,
    NzModalModule
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css', '/src/styles.css'],
})
export class HeaderComponent implements OnInit {
  keywords: string = '';
  listOfOption: Array<{ value: string; text: string }> = [];
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

  onSelectionChange(event: KeyboardEvent) {
    let text = (event.target as HTMLInputElement).value;
    (event.target as HTMLInputElement).value = text?.replace(/[<>]/ig, "");

    if (event.key === DocumentConstants.ENTER) {
      this.router.navigate([RoutesConstants.CANDIDATE_SEARCH], {
        queryParams: {
          currentCount: 0,
          keywords: (event.target as HTMLInputElement).value.toString(),
          data: null,
        },
      });
    }
  }

  getDetails() {
    this.basicDetailService.getBasicDetails().subscribe((response: IBasicDetail) => {
      this.basicDetails = response;
    });
  }

  showMenu() {
    this.isMenuModalVisible = true;
  }

  onCancel() {
    this.isMenuModalVisible = false;
  }

  logout() {
    localStorage.removeItem(DocumentConstants.TOKEN);
    this.router.navigate([RoutesConstants.AUTH]);
  }
}