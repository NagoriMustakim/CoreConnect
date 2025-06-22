import { LocationType } from './../../interfaces/locationType';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CommonModule } from '@angular/common';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzInputModule } from 'ng-zorro-antd/input';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { Component, OnInit } from '@angular/core';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { catchError, of } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { LocationTypeService } from '../../service/location-type.service';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-location-type',
  standalone: true,
  imports: [
    NzFlexModule,
    NzEmptyModule,
    NzListModule,
    NzButtonModule,
    NzFormModule,
    CommonModule,
    NzModalModule,
    ReactiveFormsModule,
    NzInputModule,
    NzIconModule,
    NzTypographyModule
  ],
  templateUrl: './location-type.component.html',
  styleUrls: ['./location-type.component.css','/src/styles.css'],
})
export class LocationTypeComponent implements OnInit {
  isModelVisible = false;
  isEditModalVisible = false;
  locationTypes: LocationType[] = [];
  constructor(
    public service: LocationTypeService,
    private message: NzMessageService,
    private modal: NzModalService
  ) { }

  ngOnInit() {
    this.getData();
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getData() {
    this.service
      .getLocaitonTypes()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.locationTypes = res;
      });
  }

  showModal() {
    this.isModelVisible = true;
  }

  edit(item: any) {
    this.service.populateEditForm(item);

    this.isEditModalVisible = true;
  }

  delete(id: any) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.LOCATION_TYPE_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteLocationType(id).subscribe((res: any) => {
          this.getData();
          this.message.success(AdminDocumentConstants.LOCATION_TYPE_DELETED_MSG)
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  onSubmit() {
    if (this.service.createLocationTypeModel.invalid) {
      Object.values(this.service.createLocationTypeModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createLocationType().subscribe({
        next: (res) => {
          this.getData();
          this.message.success(AdminDocumentConstants.LOCATION_TYPE_CREATED_MSG);
          this.service.createLocationTypeModel.reset();
          this.isModelVisible = false;
        },
        error: (err) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.LOCATION_TYPE_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_LOCATION_TYPE_CREATED_MSG);
            this.isModelVisible = false;
            this.service.createLocationTypeModel.reset();
          }
        }
      });
    }
  }

  updateLocationType(): void {
    if (this.service.editLocationTypeModel.valid) {
      this.service.updateLocationType().subscribe({
        next: () => {
          this.message.success(AdminDocumentConstants.LOCATION_TYPE_UPDATED_MSG);
          this.getData();
          this.handleCancel();
        },
        error: (err: any) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.LOCATION_TYPE_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_LOCATION_TYPE_UPDATED_MSG);
          }
        }
      });
    } else {
      Object.values(this.service.editLocationTypeModel.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleCancel() {
    this.isModelVisible = false;
    this.isEditModalVisible = false;
  }
}
