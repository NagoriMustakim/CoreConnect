import { Router } from '@angular/router';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { IInternalProgram } from './../../interfaces/IInternalProgram';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { InternalProgramsService } from './../../service/internal-programs.service';
import { NzCardModule } from 'ng-zorro-antd/card';
import { CommonModule } from '@angular/common';
import { NzListModule } from 'ng-zorro-antd/list';
import { Component, OnInit } from '@angular/core';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { catchError, of } from 'rxjs';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzGridModule } from 'ng-zorro-antd/grid';
@Component({
  selector: 'app-internal-programs-decision',
  standalone: true,
  imports: [NzListModule, NzEmptyModule, CommonModule, NzCardModule, NzListModule, NzButtonModule, CommonModule, NzFlexModule, NzEmptyModule, NzGridModule],
  templateUrl: './internal-programs-decision.component.html',
  styleUrls: ['./internal-programs-decision.component.css', '/src/styles.css'],
})
export class InternalProgramsDecisionComponent implements OnInit {
  nominations: any[] = [];
  internalPrograms: IInternalProgram[] = [];

  constructor(private internalProgramsService: InternalProgramsService, private router: Router) { }

  ngOnInit(): void {
    this.internalProgramsService.getInternalPrograms().pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.internalPrograms = res.result;
      });
  }

  NominationDetails(internalProgram: IInternalProgram) {
    this.router.navigateByUrl(AdminDocumentConstants.ROUTE_DASHBOARD_NOMINATION + internalProgram.internalProgramGuid, { state: { program: internalProgram.internalProgramName } })
  }
}
