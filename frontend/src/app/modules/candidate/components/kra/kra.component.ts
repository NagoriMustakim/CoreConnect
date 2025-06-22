import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Kra } from './../../interfaces/Kra';
import { KraService } from './../../services/kra.service';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzInputModule } from 'ng-zorro-antd/input';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, NonNullableFormBuilder } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { Component, Input } from '@angular/core';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { NzTypographyModule } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-kra',
  standalone: true,
  imports: [
    NzFormModule,
    FormsModule,
    CommonModule,
    NzInputModule,
    NzInputNumberModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzGridModule,
    NzModalModule,
    NzIconModule,
    NzListModule,
    NzCommentModule,
    NzFormModule,
    NzFlexModule,
    NzSelectModule,
    NzLayoutModule,
    NzModalModule,
    NzTimelineModule,
    NzTimePickerModule,
    NzButtonModule,
    NzSpaceModule,
    NzTagModule,
    NzSkeletonModule,
    NzCardModule,
    NzEmptyModule,
    NzTypographyModule
  ],
  templateUrl: './kra.component.html',
  styleUrls: ['./kra.component.css', '/src/styles.css']
})

export class KraComponent {
  @Input() userId: string = '';

  loadingMore = false;
  initLoading = false;
  isKRAModalVisible = false;
  kra: Kra[] = [];
  constructor(private kraService: KraService) { }

  ngOnInit() {
    this.getkras()
  }
  getkras() {
    this.kraService.getKras(this.userId).subscribe((response: Kra[]) => {
      this.kra = response;
    })
  }
}
