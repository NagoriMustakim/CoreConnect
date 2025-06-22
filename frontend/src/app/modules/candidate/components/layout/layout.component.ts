import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { FormsModule } from '@angular/forms';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
import { HeaderComponent } from '../../components/header/header.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule,
    RouterOutlet,
    NzLayoutModule,
    NzMenuModule,
    NzInputModule,
    NzGridModule,
    NzIconModule,
    NzSelectModule,
    FormsModule,
    NzAutocompleteModule,
    HeaderComponent
  ],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css', '/src/styles.css']
})

export class LayoutComponent {

}
